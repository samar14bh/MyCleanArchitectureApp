using MyCleanArchitectureApp.Domain;

namespace MyCleanArchitectureApp.UI.ViewModel
{
    public class AddMovieToFavoriteViewModel
    {
        public int CustomerId { get; set; }
        public IEnumerable<Movie> Movies { get; set; }
    }
}