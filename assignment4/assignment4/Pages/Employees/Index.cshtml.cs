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
    public class IndexModel : PageModel
    {
        private readonly assignment4.Models.NorthwindContext _context;

        public IndexModel(assignment4.Models.NorthwindContext context)
        {
            _context = context;
        }

        public IList<Employee> Employee { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Employee = await _context.Employees
                .Include(e => e.ReportsToNavigation).ToListAsync();
        }
    }
}
