using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using GeographyTools;
using Microsoft.EntityFrameworkCore;

namespace Inlämning3ApplikationHotelBooking.Models
{
    public enum BedSize { Any, Single, Twin, Double, King }
    public enum RoomType { Any, Poolview, Seaview, Suite, Basic, Honeymoon }


    public class Hotel
    {
        public int ID { get; set; }
        [Required]
        public string Name { get; set; }
        public string ImgPath { get; set; }
        [Required]
        public string Country { get; set; }
        [Required]
        public string City { get; set; }
        public Coordinate Coordinate { get; set; }
    }
    public class Room
    {
        public int ID { get; set; }
        [Required]
        [Column(TypeName = "nvarchar(30)")]
        public Hotel Hotel { get; set; }
        public BedSize BedSize { get; set; }
        [Required]
        [Column(TypeName = "nvarchar(30)")]
        public RoomType RoomType { get; set; }
        [Required]
        public int Price { get; set; }
    }
}
