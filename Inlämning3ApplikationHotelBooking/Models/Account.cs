using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Inlämning3ApplikationHotelBooking.Models
{
    public enum Rating { One, Two, Three, Four, Five }

    public class Account
    {
        public int ID { get; set; }
        [Display(Name = "First name")]
        public string FirstName { get; set; }
        [Display(Name = "Last name")]
        public string LastName { get; set; }
        [Required]
        //[RegularExpression(@"^.+@.+\..+$")]
        [EmailAddress]
        [Display(Name = "Email")]
        public string EmailAddress { get; set; }
        [Required]
        public string UserID { get; set; }
        public IdentityUser User { get; set; }
    }

    public class Review
    {
        public int ID { get; set; }
        public Account Account { get; set; }
        public Hotel Hotel { get; set; }
        public string Comment { get; set; }
        [Column(TypeName = "nvarchar(30)")]
        public Rating Rating { get; set; }
    }

    public class Booking
    {
        public int ID { get; set; }
        [Required]
        public Room Room { get; set; }
        public Account Account { get; set; }
        [Required]
        public DateTime DateBooked { get; set; }
    }
}
