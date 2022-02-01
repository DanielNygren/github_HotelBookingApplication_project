using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Inlämning3ApplikationHotelBooking.Data;
using Inlämning3ApplikationHotelBooking.Models;

namespace Inlämning3ApplikationHotelBooking.Pages.Bookings
{
    public class DetailsModel : PageModel
    {
        private readonly ApplicationDbContext database;

        public DetailsModel(ApplicationDbContext database)
        {
            this.database = database;
        }

        public Booking Booking { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Booking = await database.Booking.Include(b => b.Room).ThenInclude(r => r.Hotel).FirstOrDefaultAsync(m => m.ID == id);

            if (Booking == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
