using CarInsurance.Data;
using CarInsurance.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CarInsurance.Controllers
{
    public class InsureeController : Controller
    {
        private readonly InsuranceContext _context;

        public InsureeController(InsuranceContext context)
        {
            _context = context;
        }

        // GET: Insuree
        public async Task<IActionResult> Index()
        {
            return View(await _context.Insurees.ToListAsync());
        }

        // GET: Insuree/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var insuree = await _context.Insurees
                .FirstOrDefaultAsync(m => m.Id == id);

            if (insuree == null)
            {
                return NotFound();
            }

            return View(insuree);
        }

        // GET: Insuree/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Insuree/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FirstName,LastName,EmailAddress,DateOfBirth,CarYear,CarMake,CarModel,DUI,SpeedingTickets,CoverageType")] Insuree insuree)
        {
            if (ModelState.IsValid)
            {
                // calulate the quote before we save it to database
                insuree.Quote = CalculateQuote(insuree);

                _context.Add(insuree);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(insuree);
        }

        // GET: Insuree/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var insuree = await _context.Insurees.FindAsync(id);
            if (insuree == null)
            {
                return NotFound();
            }
            return View(insuree);
        }

        // POST: Insuree/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FirstName,LastName,EmailAddress,DateOfBirth,CarYear,CarMake,CarModel,DUI,SpeedingTickets,CoverageType")] Insuree insuree)
        {
            if (id != insuree.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // recalculate quote when editing
                    insuree.Quote = CalculateQuote(insuree);

                    _context.Update(insuree);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InsureeExists(insuree.Id))
                        return NotFound();
                    else
                        throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(insuree);
        }

        // GET: Insuree/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var insuree = await _context.Insurees
                .FirstOrDefaultAsync(m => m.Id == id);

            if (insuree == null)
            {
                return NotFound();
            }

            return View(insuree);
        }

        // POST: Insuree/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var insuree = await _context.Insurees.FindAsync(id);

            if (insuree != null)
            {
                _context.Insurees.Remove(insuree);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Insuree/Admin
        public async Task<IActionResult> Admin()
        {
            return View(await _context.Insurees.ToListAsync());
        }

        // this method calculate the monthly insurance quote
        private decimal CalculateQuote(Insuree insuree){


            decimal quote = 50m;

            // we need to figgure out how old the person is
            int age = DateTime.Today.Year - insuree.DateOfBirth.Year;
            // int age = (DateTime.Now - insuree.DateOfBirth).Days / 365; // tried this first but didnt work
            if (insuree.DateOfBirth.Date > DateTime.Today.AddYears(-age))
            {
                age--;
            }

            // add cost based on age
            if (age <= 18)
            {
                quote += 100m;
            }
            else if (age <= 25)
            {
                quote += 50m;
            }
            else
            {
                quote += 25m;
            }

            // car year
            if (insuree.CarYear < 2000)
            {
                quote += 25m;
            }

            if (insuree.CarYear > 2015)
            {
                quote += 25m;
            }

            // porshe cars are more expensive than regular cars
            if (insuree.CarMake.ToLower() == "porsche")
            {
                quote += 25m;

                // 911 carrera get extra charge
                if (insuree.CarModel.ToLower() == "911 carrera")
                {
                    quote += 25m;
                }
            }

            // for every speeding ticket add ten dollars
            quote += insuree.SpeedingTickets * 10m;

            // if they have dui we add extra 25 percent
            if (insuree.DUI)
            {
                quote = quote * 1.25m;
            }

            // full coverage means we add 50% more to the total
            if (insuree.CoverageType)
            {
                quote = quote * 1.50m;
            }

            return quote;
        }

        private bool InsureeExists(int id){
            return _context.Insurees.Any(e => e.Id == id);
        }
    }
}
