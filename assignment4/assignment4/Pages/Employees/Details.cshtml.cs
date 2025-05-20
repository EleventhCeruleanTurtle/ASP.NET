using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using assignment4.Models;

namespace assignment4.Pages.Employees
{
    public class DetailsModel : PageModel
    {
        private readonly assignment4.Models.NorthwindContext _context;

        public DetailsModel(assignment4.Models.NorthwindContext context)
        {
            _context = context;
        }

        public Employee Employee { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // As a future tip, if you see something is virtual that means to think about this solution
            var employee = await _context.Employees
                .Include(e => e.ReportsToNavigation)                // This eager loads the reportTo information so that it can be used in the view
                .FirstOrDefaultAsync(m => m.EmployeeId == id);
            if (employee == null)
            {
                return NotFound();
            }
            else
            {
                Employee = employee;
                ViewData["FilePath"] = Employee.PhotoPath;
            }
            return Page();
        }
    }
}
