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

namespace Inlämning3ApplikationHotelBooking.Pages.Rooms
{
    [Authorize(Roles = "admin")]
    public class EditModel : PageModel
    {
        private readonly ApplicationDbContext database;

        public EditModel(ApplicationDbContext database)
        {
            this.database = database;
        }

        public Room Room { get; set; }


        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Room = await database.Room.Include(h=>h.Hotel).FirstOrDefaultAsync(m => m.ID == id);

            if (Room == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id, Room room)
        {
            Room = await database.Room.Include(h=>h.Hotel).FirstOrDefaultAsync(m => m.ID == id);
            if (!ModelState.IsValid)
            {
                return Page();
            }

            Room.BedSize = room.BedSize;
            Room.RoomType = room.RoomType;
            Room.Price = room.Price;

            await database.SaveChangesAsync();

            return RedirectToPage("/Hotels/Details", new { id = Room.Hotel.ID });
        }

        private bool RoomExists(int id)
        {
            return database.Room.Any(e => e.ID == id);
        }
    }
}
