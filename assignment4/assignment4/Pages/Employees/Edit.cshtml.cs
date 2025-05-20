using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using assignment4.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace assignment4.Pages.Employees
{
    public class EditModel : PageModel
    {
        private readonly assignment4.Models.NorthwindContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public EditModel(assignment4.Models.NorthwindContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        [BindProperty]
        public Employee Employee { get; set; } = default!;

        [BindProperty]
        public IFormFile? Upload { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee =  await _context.Employees.FirstOrDefaultAsync(m => m.EmployeeId == id);
            if (employee == null)
            {
                return NotFound();
            }
            Employee = employee;
            ViewData["ReportsTo"] = new SelectList(_context.Employees, "EmployeeId", "EmployeeId");
            ViewData["FilePath"] = Employee.PhotoPath;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var employee = await _context.Employees.FindAsync(id);
            Employee = employee;

            if (Upload != null)
            {
                var fileName = Path.ChangeExtension(Path.GetRandomFileName(), Path.GetExtension(Upload.FileName));
                var file = Path.Combine(_webHostEnvironment.WebRootPath, "img/northwind_employees", fileName);

                using (var fileStream = new FileStream(file, FileMode.Create))
                {
                    await Upload.CopyToAsync(fileStream);
                }

                if (Employee.PhotoPath != null)
                {
                    file = Path.Combine(_webHostEnvironment.WebRootPath, "img/northwind_employees", Employee.PhotoPath);
                    if (System.IO.File.Exists(file))
                    {
                        System.IO.File.Delete(file);
                    }
                }
                Employee.PhotoPath = fileName;
            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmployeeExists(Employee.EmployeeId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool EmployeeExists(int id)
        {
            return _context.Employees.Any(e => e.EmployeeId == id);
        }
    }
}
