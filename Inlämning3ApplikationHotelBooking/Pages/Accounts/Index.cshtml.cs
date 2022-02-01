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
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext database;
        private readonly AccessControl accessControl;


        public IndexModel(ApplicationDbContext database, AccessControl accessControl)
        {
            this.database = database;
            this.accessControl = accessControl;

        }

        public Account Account { get;set; }
        public IList<Booking> BookingList { get; set; }

        public async Task OnGetAsync()
        {
            Account = await database.Account
                .Where(c => c.User.Id == accessControl.LoggedInUserID)
                .Include(a => a.User).FirstOrDefaultAsync();

            var query = database.Booking
                .Where(c => c.Account.User.Id == accessControl.LoggedInUserID)
                .Include(h => h.Room)
                .ThenInclude(h => h.Hotel);

            BookingList = await query.ToListAsync();
        }
    }
}
