using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Inlämning3ApplikationHotelBooking.Data;
using Inlämning3ApplikationHotelBooking.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Inlämning3ApplikationHotelBooking.Pages.Hotels
{
    [AllowAnonymous]
    public class UserIndexModel : PageModel
    {
        private readonly ApplicationDbContext database;

        public UserIndexModel(ApplicationDbContext database)
        {
            this.database = database;
        }

        public IList<Hotel> Hotels { get; set; }
        [FromQuery]
        public string SortCountry { get; set; }
        public SelectList SortCountryList { get; set; }
        [FromQuery]
        public string SortCity { get; set; }
        public SelectList SortCityList { get; set; }
        [FromQuery]
        public string SearchTerm { get; set; }
        public Review Review { get; set; }
        public List<string> Raitings { get; set; }

        public async Task OnGetAsync()
        {
            var query = database.Hotel.AsNoTracking();
            var countrys = query.Select(c => c.Country).Distinct();
            var citys = query.Select(c => c.City).Distinct();
            var rating = database.Review.AsNoTracking();
            SortCountryList = new SelectList(countrys);

            Raitings = new List<string>();

            if (SearchTerm != null)
            {
                query = query.Where(c =>
                    c.Name.ToLower().Contains(SearchTerm.ToLower()) ||
                    c.Country.ToLower().Contains(SearchTerm.ToLower()) ||
                    c.City.ToLower().Contains(SearchTerm.ToLower())
                );
            }

            if (SortCountry != null)
            {
                query = query.Where(c => c.Country == SortCountry);
                citys = query.Select(c => c.City).Distinct();
            }
            SortCityList = new SelectList(citys);
            if (SortCity != null)
            {
                query = query.Where(c => c.City == SortCity);
            }

            Hotels = await query.ToListAsync();

            for(var i = 0; i < Hotels.Count; i++)
            {
                double math = 0;
                var scors = rating.Where(h => h.Hotel == Hotels[i]).ToList();
                if(scors.Count != 0)
                {
                    int count = 0;
                    foreach(var s in scors)
                    {
                        count++;
                        if(s.Rating.ToString() == "One")
                        {
                            math = (math + 1);
                        }
                        if (s.Rating.ToString() == "Two")
                        {
                            math = (math + 2);
                        }
                        if (s.Rating.ToString() == "Three")
                        {
                            math = (math + 3);
                        }
                        if (s.Rating.ToString() == "Four")
                        {
                            math = (math + 4);
                        }
                        if (s.Rating.ToString() == "Five")
                        {
                            math = (math + 5);
                        }
                    }
                    var score = "";
                    for(var x = 0; x < (Math.Round(math / count, 1)); x++)
                    {
                        score = score + '⭐'.ToString();
                    }
                    Raitings.Insert(i, score);
                }
                else
                {
                    Raitings.Add("No Rating");
                }
            }
        }
    }
}
