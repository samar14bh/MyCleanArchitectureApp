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
        
        Task<IEnumerable<Genre>> GetAllGenresAsync();

        
        Task<Genre> GetGenreByIdAsync(int genreId);

        Task<IEnumerable<Movie>> GetMoviesByGenreAsync(int genreId);
    }
}
