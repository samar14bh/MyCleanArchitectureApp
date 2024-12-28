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
        public int GenreId { get; set; }  // Clé étrangère vers Genre
        public Genre Genre { get; set; }  // Navigation vers Genre
        public List<Review> Reviews { get; set; }
        public List<Customer> FavoriteCustomers { get; set; }
    }
}
