using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Inlämning3ApplikationHotelBooking.Data;
using Inlämning3ApplikationHotelBooking.Models;

namespace Inlämning3ApplikationHotelBooking.Pages.Accounts
{
    public class DetailsModel : PageModel
    {
        private readonly Inlämning3ApplikationHotelBooking.Data.ApplicationDbContext _context;

        public DetailsModel(Inlämning3ApplikationHotelBooking.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public Account Account { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Account = await _context.Account
                .Include(a => a.User).FirstOrDefaultAsync(m => m.ID == id);

            if (Account == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
