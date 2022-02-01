using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Inlämning3ApplikationHotelBooking.Data;
using Inlämning3ApplikationHotelBooking.Models;
using Microsoft.AspNetCore.Authorization;

namespace Inlämning3ApplikationHotelBooking.Pages.Hotels
{
    [Authorize(Roles = "admin")]
    public class EditModel : PageModel
    {
        private readonly ApplicationDbContext database;

        public EditModel(ApplicationDbContext database)
        {
            this.database = database;
        }

        public Hotel Hotel { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Hotel = await database.Hotel.FirstOrDefaultAsync(m => m.ID == id);

            if (Hotel == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id, Hotel hotel)
        {
            Hotel = await database.Hotel.FirstOrDefaultAsync(m => m.ID == id);

            if (!ModelState.IsValid)
            {
                return Page();
            }




            Hotel.Name = hotel.Name;
            Hotel.Country = hotel.Country;
            Hotel.City = hotel.City;
            Hotel.ImgPath = hotel.ImgPath;

            await database.SaveChangesAsync();

            return RedirectToPage("./Index");
        }

        private bool HotelExists(int id)
        {
            return database.Hotel.Any(e => e.ID == id);
        }
    }
}
