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

namespace Inlämning3ApplikationHotelBooking.Pages.Accounts
{
    public class EditModel : PageModel
    {
        private readonly ApplicationDbContext database;
        private readonly AccessControl accessControl;

        public EditModel(ApplicationDbContext database, AccessControl accessControl)
        {
            this.database = database;
            this.accessControl = accessControl;
        }

        public Account Account { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Account = await database.Account.Include(a => a.User).FirstOrDefaultAsync(m => m.ID == id);

            if (Account == null)
            {
                return NotFound();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id, Account account)
        {
            Account = await database.Account.FirstOrDefaultAsync(a => a.ID == id);

            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (!accessControl.UserCanAccess(Account))
            {
                return Forbid();
            }

            Account.FirstName = account.FirstName;
            Account.LastName = account.LastName;
            Account.EmailAddress = account.EmailAddress;

            await database.SaveChangesAsync();

            return RedirectToPage("./Index");
        }

    }
}