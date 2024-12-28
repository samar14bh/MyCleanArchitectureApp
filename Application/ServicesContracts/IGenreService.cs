using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyCleanArchitectureApp.Domain;


namespace MyCleanArchitectureApp.Applications.ServicesContracts
{
    public interface IGenreService
    {
        // Method to get all genres
        Task<IEnumerable<Genre>> GetAllGenresAsync();

        // Method to get a genre by its ID
        Task<Genre> GetGenreByIdAsync(int genreId);

        // Method to get movies by genre ID
        Task<IEnumerable<Movie>> GetMoviesByGenreAsync(int genreId);
    }
}
