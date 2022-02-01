using GeographyTools;
using Inlämning3ApplikationHotelBooking.Data;
using Inlämning3ApplikationHotelBooking.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AddressBook.Data
{
    public class DbInitializer
    {
        public static async Task InitializeAsync(ApplicationDbContext database)
        {
            if (database.Hotel.Any())
            {
            }
            else
            {
                using (var reader = new StreamReader(@"Data\hotels.csv"))
                {
                    while (!reader.EndOfStream)
                    {
                        var line = reader.ReadLine();
                        var values = line.Split(',');

                        var location = new Coordinate
                        {
                            Latitude = double.Parse(values[4]),
                            Longitude = double.Parse(values[5])
                        };

                        var hotel = new Hotel
                        {
                            Name = values[0],
                            Country = values[1],
                            City = values[2],
                            ImgPath = values[3],
                            Coordinate = location

                        };

                        database.Hotel.Add(hotel);
                    }
                }
            }

            if (database.Restaurants.Any())
            {
            }
            else
            {
                using (var reader = new StreamReader(@"Data\restaurants.csv"))
                {
                    while (!reader.EndOfStream)
                    {

                        var line = reader.ReadLine();
                        var values = line.Split(',');

                        var location = new Coordinate
                        {
                            Latitude = double.Parse(values[2]),
                            Longitude = double.Parse(values[3])
                        };

                        var resturant = new Restaurant
                        {
                            Name = values[0],
                            Cuisine = (Cuisine)Enum.Parse(typeof(Cuisine), values[1], true),
                            Coordinate = location
                        };

                        database.Restaurants.Add(resturant);
                    }
                }
            }

            if(database.Room.Any())
            {
            }
            else
            {
                var hotels = database.Hotel.ToList();

                for (int i = 0; i < hotels.Count; i++)
                {
                    for (int j = 1; j <= 5; j++)
                    {
                        var singleRoom = new Room()
                        {
                            BedSize = (BedSize)j,
                            RoomType = (RoomType)1,
                            Price = 500,
                            Hotel = hotels[i]
                        };

                        database.Room.Add(singleRoom);
                    }

                    for (int k = 1; k <= 5; k++)
                    {
                        var twinRoom = new Room()
                        {
                            BedSize = (BedSize)k,
                            RoomType = (RoomType)2,
                            Price = 500,
                            Hotel = hotels[i]
                        };
                        database.Room.Add(twinRoom);

                    }
                    for (int l = 1; l <= 5; l++)
                    {
                        var doubleRoom = new Room()
                        {
                            BedSize = (BedSize)l,
                            RoomType = (RoomType)3,
                            Price = 500,
                            Hotel = hotels[i]
                        };

                        database.Room.Add(doubleRoom);
                    }
                    for (int m = 1; m <= 5; m++)
                    {
                        var kingRoom = new Room()
                        {
                            BedSize = (BedSize)m,
                            RoomType = (RoomType)4,
                            Price = 500,
                            Hotel = hotels[i]
                        };

                        database.Room.Add(kingRoom);
                    }
                }

            }

            await database.SaveChangesAsync();
        }
    }
}