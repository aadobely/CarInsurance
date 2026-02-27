// this is the insuree model class
using System.ComponentModel.DataAnnotations;

namespace CarInsurance.Models
{
    public class Insuree
    {
        public int Id { get; set; }


        [Required]
        [Display(Name = "First Name")]
        [StringLength(50)]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Last Name")]
        [StringLength(50)]
        public string LastName { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Email Address")]
        [EmailAddress]
        [StringLength(100)]
        public string EmailAddress { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Date of Birth")]
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }

        [Required]
        [Display(Name = "Car Year")]
        [Range(1886, 2025)]
        public int CarYear { get; set; }

        [Required]
        [Display(Name = "Car Make")]
        [StringLength(50)]
        public string CarMake { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Car Model")]
        [StringLength(50)]
        public string CarModel { get; set; } = string.Empty;

        [Display(Name = "DUI")]
        public bool DUI { get; set; }

        [Required]
        [Display(Name = "Speeding Tickets")]
        [Range(0, 100)]
        public int SpeedingTickets { get; set; }


        [Display(Name = "Full Coverage")]
        public bool CoverageType { get; set; }

        [Display(Name = "Quote")]
        [DataType(DataType.Currency)]
        public decimal Quote { get; set; }
    }
}
