using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Inlämning3ApplikationHotelBooking.Data;
using Inlämning3ApplikationHotelBooking.Models;
using Microsoft.AspNetCore.Authorization;

namespace Inlämning3ApplikationHotelBooking.Pages.Bookings
{
    [Authorize(Roles = "admin")]
    public class DeleteModel : PageModel
    {
        private readonly ApplicationDbContext database;

        public DeleteModel(ApplicationDbContext database)
        {
            this.database = database;
        }

        [BindProperty]
        public Booking Booking { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Booking = await database.Booking.FirstOrDefaultAsync(m => m.ID == id);

            if (Booking == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Booking = await database.Booking.FindAsync(id);

            if (Booking != null)
            {
                database.Booking.Remove(Booking);
                await database.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
