using MyCleanArchitectureApp.Domain;
using System.Collections.Generic;

namespace MyCleanArchitectureApp.Web.Models
{
    public class CustomerDetailsViewModel
    {
        public Customer Customer { get; set; }
        public List<Movie> FavoriteMovies { get; set; }
    }
}
