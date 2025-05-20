using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using assignment4.Models;
using Microsoft.AspNetCore.Http;

namespace assignment4.Pages.Employees
{
    public class CreateModel : PageModel
    {
        private readonly assignment4.Models.NorthwindContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public CreateModel(assignment4.Models.NorthwindContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult OnGet()
        {
            // Create new list to use for dropdown list in create page to display employee full name
            var employeesFullName = from e in _context.Employees
                                    select (new { EmployeeId = e.EmployeeId, FullName = e.FirstName + " " + e.LastName });
            // Put the list into the ViewData dictionary to use in the dropdown list
            ViewData["ReportsTo"] = new SelectList(employeesFullName, "EmployeeId", "FullName");
            return Page();
        }

        [BindProperty]
        public Employee Employee { get; set; } = default!;

        [BindProperty]
        public IFormFile? Upload { get; set; }

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (Upload != null)
            {
                var fileName = Path.ChangeExtension(Path.GetRandomFileName(), Path.GetExtension(Upload.FileName));
                var file = Path.Combine(_webHostEnvironment.WebRootPath, "img/northwind_employees", fileName);

                using (var fileStream = new FileStream(file, FileMode.Create))
                {
                    await Upload.CopyToAsync(fileStream);
                }

                Employee.PhotoPath = fileName;
            }

            _context.Employees.Add(Employee);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
