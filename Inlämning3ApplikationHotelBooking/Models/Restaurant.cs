using GeographyTools;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Inlämning3ApplikationHotelBooking.Models
{
    public enum Cuisine { Asian, Burgers, Italian, Local }

    public class Restaurant
    {
        public int ID { get; set; }
        public string Name { get; set; }
        [Column(TypeName = "nvarchar(30)")]
        public Cuisine Cuisine { get; set; }
        public Coordinate Coordinate { get; set; }

    }
}
