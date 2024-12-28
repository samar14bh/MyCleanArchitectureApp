using MyCleanArchitectureApp.Domain;

namespace MyCleanArchitectureApp.UI.ViewModel
{
    public class MovieDetailsViewModel
    {
        public Movie Movie { get; set; }
        public IEnumerable<Review> Reviews { get; set; }
    }
}
