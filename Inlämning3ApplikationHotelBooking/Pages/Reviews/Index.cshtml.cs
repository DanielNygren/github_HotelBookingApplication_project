using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Inlämning3ApplikationHotelBooking.Data;
using Inlämning3ApplikationHotelBooking.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Inlämning3ApplikationHotelBooking.Pages.Reviews
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext database;

        public IndexModel(ApplicationDbContext database)
        {
            this.database = database;
        }

        public IList<Review> Review { get;set; }
        [FromQuery]
        public string SortReviews { get; set; }
        public SelectList SortReviweaList { get; set; }
        [FromQuery]
        public string SearchTerm { get; set; }

        public async Task OnGetAsync()
        {
            var query = await database.Review.Include(a => a.Account).Include(h => h.Hotel).ToListAsync();
            if (SearchTerm != null)
            {
                var x = query.Where(c =>
                    c.Hotel.Name.ToLower().Contains(SearchTerm.ToLower()) ||
                    c.Account.FirstName.ToLower().Contains(SearchTerm.ToLower()) ||
                    c.Comment.ToLower().Contains(SearchTerm.ToLower())
                );

                query = x.ToList();
            }

            if (SortReviews != null)
            {
                if (SortReviews == "Hotel")
                {
                    query = query.OrderBy(h=>h.Hotel.Name).ToList();
                }
                else if(SortReviews == "Rating")
                {
                    query = query.OrderByDescending(r => r.Rating).ToList();
                }
                else if (SortReviews == "Reviewers Name")
                {
                    query = query.OrderBy(n => n.Account.FirstName).ToList();
                }
                
            }

            Review =  query;
        }
    }
}
