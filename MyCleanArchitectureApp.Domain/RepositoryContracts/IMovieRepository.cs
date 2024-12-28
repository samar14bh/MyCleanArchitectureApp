using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace MyCleanArchitectureApp.Domain.RepositoryContracts
{
    public interface IMovieRepository
    {
        Task<Movie> GetByIdAsync(int id);
        Task<List<Movie>> GetAllMoviesAsync();
        Task AddAsync(Movie movie);
        Task UpdateAsync(Movie movie);
        Task<List<Movie>> GetMoviesByGenreAsync(int genreId);
        Task<List<Movie>> GetFavoriteMoviesByCustomerIdAsync(int customerId);
    }
}
