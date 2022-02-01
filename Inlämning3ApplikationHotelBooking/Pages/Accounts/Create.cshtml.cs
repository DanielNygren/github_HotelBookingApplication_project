using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Inlämning3ApplikationHotelBooking.Data;
using Inlämning3ApplikationHotelBooking.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Inlämning3ApplikationHotelBooking.Pages.Accounts
{
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext database;
        private readonly AccessControl accessControl;

        public CreateModel(ApplicationDbContext database, AccessControl accessControl)
        {
            this.database = database;
            this.accessControl = accessControl;
        }

        public Account Account { get; set; }

        private void CreateAccount()
        {
            Account = new Account
            {
                UserID = accessControl.LoggedInUserID
            };
        }

        public void OnGet()
        {
            CreateAccount();
        }

        public async Task<IActionResult> OnPostAsync(Account account)
        {
            CreateAccount();

            if (!ModelState.IsValid)
            {
                return Page();
            }

            Account.FirstName = account.FirstName;
            Account.LastName = account.LastName;
            Account.EmailAddress = account.EmailAddress;

            await database.Account.AddAsync(Account);
            await database.SaveChangesAsync();
            return RedirectToPage("./Details", new { id = Account.ID });
        }
    }
}
