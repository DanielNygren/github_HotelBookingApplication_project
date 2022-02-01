using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Inlämning3ApplikationHotelBooking.Data;
using Inlämning3ApplikationHotelBooking.Models;

namespace Inlämning3ApplikationHotelBooking.Pages.Rooms
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext database;
        private readonly AccessControl accessControl;

        public IndexModel(ApplicationDbContext database, AccessControl accessControl)
        {
            this.database = database;
            this.accessControl = accessControl;
        }

        public Room Room { get; set; }
        public Hotel Hotel { get; set; }
        public Account Account { get; set; }
        [FromQuery]
        public string SelectedDate { get; set; }
        public DateTime Date { get; set; }
        [BindProperty]
        public Booking Booking { get; set; }
        public string ReplyMessage { get; set; }
        public string ErrorMessege { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id, DateTime? selecteddate)
        {
            Date = (DateTime)selecteddate;
            SelectedDate = Date.ToString("yyyy-MM-dd");
            var query = database.Booking
                .Where(c => c.Account.User.Id == accessControl.LoggedInUserID)
                .Include(h => h.Room)
                .ThenInclude(h => h.Hotel);

            if (id == null)
            {
                return Page();
            }

            Room = await database.Room.Include(h => h.Hotel).FirstOrDefaultAsync(r => r.ID == id);
            Hotel = Room.Hotel;


            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id, DateTime? selecteddate)
        {
            var query = database.Booking
                .Where(c => c.Account.User.Id == accessControl.LoggedInUserID)
                .Include(h => h.Room)
                .ThenInclude(h => h.Hotel);

            Room = await database.Room.Include(h => h.Hotel).FirstOrDefaultAsync(r => r.ID == id);
            Hotel = Room.Hotel;

            Account = await database.Account.Include(u => u.User).Where(u => u.UserID == accessControl.LoggedInUserID).FirstOrDefaultAsync();
            if (Account != null)
            {
                Booking = new Booking
                {
                    Room = Room,
                    DateBooked = (DateTime)selecteddate,
                    Account = Account
                };

                var bookCheck = query.Select(b => b.DateBooked).Contains(Booking.DateBooked) && query.Select(b => b.Room).Contains(Booking.Room);

                if (bookCheck == false)
                {
                    database.Booking.Add(Booking);
                    ReplyMessage = "Thank you for booking with us!";

                }
                else
                {
                    ReplyMessage = "You can not book this room again on this date. Please go back and choose a different date.";
                }

                await database.SaveChangesAsync();
            }
            else
            {
                return Redirect("/Login");
            }

            return Page();
        }
    }
}