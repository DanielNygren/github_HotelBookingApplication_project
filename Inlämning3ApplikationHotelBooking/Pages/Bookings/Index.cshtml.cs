using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Inlämning3ApplikationHotelBooking.Data;
using Inlämning3ApplikationHotelBooking.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;


namespace Inlämning3ApplikationHotelBooking.Pages.Bookings
{
    [AllowAnonymous]
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext database;
        private readonly AccessControl accessControl;

        public IndexModel(ApplicationDbContext database, AccessControl accessControl)
        {
            this.database = database;
            this.accessControl = accessControl;
        }

        public Account Account { get; set; }
        public DateTime SelectedDate { get; set; }

        public IList<Booking> Bookings { get; set; }

        public async Task OnGetAsync()
        {
            Account = await database.Account.Where(a => a.User.Id == accessControl.LoggedInUserID).FirstOrDefaultAsync();
            Bookings = await database.Booking.Include(h => h.Room)
                .ThenInclude(h => h.Hotel).Where(b => b.Account.User.Id == accessControl.LoggedInUserID).ToListAsync();

        }
    }
}
