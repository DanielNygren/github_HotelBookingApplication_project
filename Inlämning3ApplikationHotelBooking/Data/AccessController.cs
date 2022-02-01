using Inlämning3ApplikationHotelBooking.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Inlämning3ApplikationHotelBooking.Data
{
    public class AccessControl
    {
        public string LoggedInUserID { get; private set; }

        public AccessControl(UserManager<IdentityUser> userManager, IHttpContextAccessor httpContextAccessor)
        {
            LoggedInUserID = userManager.GetUserId(httpContextAccessor.HttpContext.User);
        }

        // We can add more methods like this one to control access to different kinds of data.
        public bool UserCanAccess(Account account)
        {
            return account.UserID == LoggedInUserID;
        }
    }
}
