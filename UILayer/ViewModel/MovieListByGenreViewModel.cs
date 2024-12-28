using MyCleanArchitectureApp.Domain;

namespace MyCleanArchitectureApp.UI.ViewModel
{
    public class MovieListByGenreViewModel
    {
        public Dictionary<string, List<Movie>> GroupedMovies { get; set; }
    }

}
