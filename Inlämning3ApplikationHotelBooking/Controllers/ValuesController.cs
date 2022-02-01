using GeographyTools;
using Inlämning3ApplikationHotelBooking.Data;
using Inlämning3ApplikationHotelBooking.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Windows.Devices.Geolocation;

namespace Inlämning3ApplikationHotelBooking.Controllers
{

    [AllowAnonymous]
    [Route("api/hotels")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly ApplicationDbContext database;

        public ValuesController(ApplicationDbContext database)
        {
            this.database = database;
        }
       

        [HttpGet("{lat}/{log}/{orderBy}")]
        public async Task<List<HotelResponse>> GetAsync(double lat, double log, string orderBy)
        {
            Coordinate userCoordinates = new Coordinate();
            userCoordinates.Latitude = lat;
            userCoordinates.Longitude = log;


            var hotelsList = new List<HotelResponse>();
            var rooms = database.Room.Include(h=>h.Hotel);
            var hotels = rooms.Select(h=>h.Hotel).Distinct();
            var ratings = database.Review;
            var bookings = database.Booking;

            foreach (var hotel in hotels)
            {
                var distance = Geography.Distance(userCoordinates, hotel.Coordinate);
                if (distance <= 100000000)
                {
                    double math = 0;
                    double Score = 0;
                    var date = DateTime.Now.Date;
                    var selectedHotel = hotel;
                    int roomsCount = await rooms.Where(h => h.Hotel == selectedHotel).CountAsync();
                    var rating = await ratings.Where(h => h.Hotel.ID == hotel.ID).Select(r => r.Rating).ToListAsync();
                    var bookedRooms = await bookings.Where(h => h.Room.Hotel.ID == hotel.ID).Where(d => d.DateBooked == date).CountAsync();
                    var lowestRoomPrice = await rooms.Select(p => p.Price).OrderBy(p => p).FirstOrDefaultAsync();
                    var highestRoomPrice = await rooms.Select(p => p.Price).OrderByDescending(p => p).FirstOrDefaultAsync();
                    if (rating.Count != 0)
                    {
                        int count = 0;
                        foreach (var s in rating)
                        {
                            count++;
                            if (s.ToString() == "One")
                            {
                                math = (math + 1);
                            }
                            if (s.ToString() == "Two")
                            {
                                math = (math + 2);
                            }
                            if (s.ToString() == "Three")
                            {
                                math = (math + 3);
                            }
                            if (s.ToString() == "Four")
                            {
                                math = (math + 4);
                            }
                            if (s.ToString() == "Five")
                            {
                                math = (math + 5);
                            }
                        }
                        Score = Math.Round(math / count, 1);
                    }

                    var response = new HotelResponse
                    {
                        ID = selectedHotel.ID,
                        WebLink = "https://localhost:44323/Hotels/Details/" + selectedHotel.ID,
                        ImgLink = selectedHotel.ImgPath,
                        Name = selectedHotel.Name,
                        Rooms = roomsCount - bookedRooms,
                        Distance =  Math.Round(distance,2),
                        Rating = Score,
                        LowPrice = lowestRoomPrice,
                        HighPrice = highestRoomPrice


                    };
                    hotelsList.Add(response);
                }
            }
            if(orderBy.ToLower() == "name")
            {
                hotelsList = (List<HotelResponse>)hotelsList.OrderBy(x=>x.Name).ToList();
            }
            else if (orderBy.ToLower() == "raiting")
            {
                hotelsList = (List<HotelResponse>)hotelsList.OrderByDescending(x => x.Rating).ToList();
            }
            else if (orderBy.ToLower() == "distance")
            {
                hotelsList = (List<HotelResponse>)hotelsList.OrderBy(x => x.Distance).ToList();
            }
            else if (orderBy.ToLower() == "lowprice")
            {
                hotelsList = (List<HotelResponse>)hotelsList.OrderBy(x => x.LowPrice).ToList();
            }
            else if (orderBy.ToLower() == "highprice")
            {
                hotelsList = (List<HotelResponse>)hotelsList.OrderByDescending(x => x.HighPrice).ToList();
            }
            else
            {
                hotelsList = (List<HotelResponse>)hotelsList.OrderBy(x => x.ID).ToList();
            }


            return hotelsList;
        }
       
        public class HotelResponse
        {
            public int ID { get; set; }
            public string WebLink { get; set; }
            public string ImgLink { get; set; }
            public string Name { get; set; }
            public int Rooms { get; set; }
            public double Distance { get; set; }
            public double Rating { get; set; }
            public double LowPrice { get; set; }
            public double HighPrice { get; set; }

        }
    }
}
