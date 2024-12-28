using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCleanArchitectureApp.Domain
{
    public class Movie
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int GenreId { get; set; } 
        public Genre Genre { get; set; }
        public List<Review> Reviews { get; set; }
        public List<Customer> FavoriteCustomers { get; set; }
        public double AverageRating { get; set; } 
    }
}
