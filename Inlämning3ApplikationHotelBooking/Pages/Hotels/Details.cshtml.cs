using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Inlämning3ApplikationHotelBooking.Data;
using Inlämning3ApplikationHotelBooking.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;

namespace Inlämning3ApplikationHotelBooking.Pages.Hotels
{
    [AllowAnonymous]
    public class DetailsModel : PageModel
    {
        private readonly ApplicationDbContext database;
        private readonly AccessControl accessControl;

        public DetailsModel(ApplicationDbContext database, AccessControl accessControl)
        {
            this.database = database;
            this.accessControl = accessControl;
        }
        public Hotel Hotel { get; set; }
        public Room Room { get; set; }
        [FromQuery]
        public DateTime Date { get; set; }
        [FromQuery]
        public RoomType SelectedRoomType { get; set; }
        [FromQuery]
        public BedSize SelectedBedSize { get; set; }
        [FromQuery]
        public DateTime SelectedDate { get; set; }
        public IList<Room> Rooms { get; set; }
        public Review Review { get; set; }
        public IList<Review> Reviews { get; set; }
        [FromForm]
        public string Rating { get; set; }
        [FromForm]
        public Rating SelectedRating { get; set; }
        [FromForm]
        public string Comment { get; set; }

        //Get Hotel
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Hotel = await database.Hotel.FirstOrDefaultAsync(m => m.ID == id);

            Reviews = await database.Review.Select(r=>r).Where(h=>h.Hotel.ID == id).ToListAsync();

            

            GetDate();
            GetHotelsStars();
            Rooms = await GetRoomAsync(id);

            return Page();
        }

        public void GetHotelsStars()
        {
            var avregeRating = Reviews.Select(r => r.Rating).ToList();
            double math = 0;
            double Score = 0;

            if (avregeRating.Count != 0)
            {
                int count = 0;
                foreach (var a in avregeRating)
                {
                    count++;
                    if (a.ToString() == "One")
                    {
                        math = (math + 1);
                    }
                    if (a.ToString() == "Two")
                    {
                        math = (math + 2);
                    }
                    if (a.ToString() == "Three")
                    {
                        math = (math + 3);
                    }
                    if (a.ToString() == "Four")
                    {
                        math = (math + 4);
                    }
                    if (a.ToString() == "Five")
                    {
                        math = (math + 5);
                    }
                }
                Score = Math.Round(math / count, 1);
            }

            var star = "⭐";
            for (var i = 0; i < Score; i++)
            {
                Rating = Rating + star;
            }
        }

        public void GetDate()
        {
            DateTime dateCheck = new DateTime();
            //IF/ELSE to set the default date to todays date
            if (SelectedDate != dateCheck)
            {
            }
            else
            {
                SelectedDate = DateTime.Today;
            }
        }

        public async Task<IList<Room>> GetRoomAsync(int? id)
        {

            var query = database.Room.Where(r => r.Hotel.ID == id).AsNoTracking();
            var queryBookings = database.Booking.Where(d => d.DateBooked == SelectedDate).Select(r => r.Room).Where(r => r.Hotel.ID == id).AsNoTracking();
            
            if (SelectedRoomType != RoomType.Any)
            {
                query = query.Where(r => r.RoomType == SelectedRoomType).AsNoTracking();
            }
            if (SelectedBedSize != BedSize.Any)
            {
                query = query.Where(r => r.BedSize == SelectedBedSize).AsNoTracking();
            }

            if (queryBookings != null)
            {
                query = query.Except(queryBookings);

            }

            return Rooms = await query.ToListAsync();
        }
        
        public async Task<IActionResult> OnPostAsync(int? id)
        {
            var account = database.Account.Where(u => u.User.Id == accessControl.LoggedInUserID).FirstOrDefault();
            if (account != null)
            {
                Hotel = await database.Hotel.FirstOrDefaultAsync(m => m.ID == id);
                Review = new Review
                {
                    Account = account,
                    Comment = Comment,
                    Hotel = Hotel,
                    Rating = SelectedRating
                };
                database.Review.Add(Review);
                await database.SaveChangesAsync();
            }
            else
            {
                return Redirect("/Login");
            }
            Reviews = await database.Review.Select(r => r).Where(h => h.Hotel.ID == id).ToListAsync();
            Rooms = await GetRoomAsync(id);
            GetDate();
            GetHotelsStars();
            
            return Page();
        }
    }
}
