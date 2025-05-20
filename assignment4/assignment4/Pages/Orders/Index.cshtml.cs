using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using assignment4.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace assignment4.Pages.Orders
{
    public class IndexModel : PageModel
    {
        private readonly assignment4.Models.NorthwindContext _context;

        public IndexModel(assignment4.Models.NorthwindContext context)
        {
            _context = context;
        }

        public IList<Order> Order { get;set; } = default!;

        public async Task OnGetAsync(int? employeeId)
        {
            var employeesFullName = from e in _context.Employees
                                    select (new { EmployeeId = e.EmployeeId, FullName = e.FirstName + " " + e.LastName });
            ViewData["EmployeeNames"] = new SelectList(employeesFullName, "EmployeeId", "FullName");
            Order = await _context.Orders
                .Where(o => o.Freight >= 250)
                .Include(o => o.Employee)
                .Include(o => o.ShipViaNavigation).ToListAsync();
            if (employeeId != null)
            {
                Order = Order.Where(o => o.EmployeeId == employeeId).ToList();
            }
        }
    }
}
