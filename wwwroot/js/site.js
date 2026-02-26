// site.js - Custom JavaScript for the CarInsurance app
// Most functionality is handled by Bootstrap, this is just extra polish

// Wait for the page to fully load before running any JavaScript
document.addEventListener('DOMContentLoaded', function () {

    // ---- Auto-dismiss alerts after 5 seconds ----
    // This makes success/error messages disappear automatically
    const alerts = document.querySelectorAll('.alert-dismissible');
    alerts.forEach(function (alert) {
        setTimeout(function () {
            // Fade out the alert smoothly
            alert.style.transition = 'opacity 0.5s ease';
            alert.style.opacity = '0';
            setTimeout(function () {
                alert.remove();
            }, 500);
        }, 5000); // 5000ms = 5 seconds
    });

    // ---- Highlight the current nav link ----
    // Adds 'active' class to the nav link that matches the current page URL
    const currentPath = window.location.pathname.toLowerCase();
    const navLinks = document.querySelectorAll('.navbar-nav .nav-link');

    navLinks.forEach(function (link) {
        const linkPath = link.getAttribute('href');
        if (linkPath && currentPath.includes(linkPath.toLowerCase()) && linkPath !== '/') {
            link.classList.add('active');
        }
        // Special case for home page
        if (currentPath === '/' && linkPath === '/') {
            link.classList.add('active');
        }
    });

    // ---- Confirm before delete ----
    // Extra safety check on the delete confirmation page
    const deleteForm = document.querySelector('form[action*="Delete"]');
    if (deleteForm) {
        const deleteBtn = deleteForm.querySelector('button[type="submit"]');
        if (deleteBtn) {
            deleteBtn.addEventListener('click', function (e) {
                // Double-check they want to delete
                if (!confirm('Are you absolutely sure you want to delete this quote? This cannot be undone.')) {
                    e.preventDefault(); // Stop the form from submitting
                }
            });
        }
    }

    // ---- Form validation feedback ----
    // Shows Bootstrap's validation styles when form is submitted
    const forms = document.querySelectorAll('.needs-validation');
    forms.forEach(function (form) {
        form.addEventListener('submit', function (event) {
            if (!form.checkValidity()) {
                event.preventDefault();
                event.stopPropagation();
            }
            form.classList.add('was-validated');
        }, false);
    });

    // ---- Show validation summary if it has errors ----
    // The create form hides the validation summary by default
    // but we need to show it if there are actually errors
    const valSummary = document.querySelector('[data-valmsg-summary]');
    if (valSummary) {
        const errors = valSummary.querySelectorAll('li');
        if (errors.length > 0) {
            valSummary.classList.remove('d-none');
        }
    }

});
