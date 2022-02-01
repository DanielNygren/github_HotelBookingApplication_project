using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Inlämning3ApplikationHotelBooking.Data;
using Inlämning3ApplikationHotelBooking.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Inlämning3ApplikationHotelBooking.Pages.Shared
{
    public class LoginModel : PageModel
    {
        private readonly ApplicationDbContext database;
        private readonly AccessControl accessControl;

        public LoginModel(ApplicationDbContext database, AccessControl accessControl)
        {
            this.database = database;
            this.accessControl = accessControl;
        }

        public string UserId { get; private set; }
        public string UserEmail { get; private set; }
        public Account Account { get; private set; }

        public void OnGetAsync()
        {
            ViewData["UserID"] = new SelectList(database.Users, "Id", "Id");

            UserId = accessControl.LoggedInUserID;

            UserEmail = database.Users.Where(u => u.Id == accessControl.LoggedInUserID).Select(e => e.Email).FirstOrDefault().ToString();
            var user = database.Account.Where(u => u.EmailAddress == UserEmail).FirstOrDefault();
            if (user == null)
            {
                Account = new Account
                {
                    EmailAddress = UserEmail,
                    UserID = UserId,
                };

                database.Account.Add(Account);
                database.SaveChanges();
            }
        }
    }
}
