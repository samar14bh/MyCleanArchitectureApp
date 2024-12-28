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
        Task<IEnumerable<Movie>> GetAllMoviesAsync();
        Task AddAsync(Movie movie);
        Task UpdateAsync(Movie movie);
        Task<IEnumerable<Movie>> GetMoviesByGenreAsync(int genreId);
        Task<IEnumerable<Movie>> GetFavoriteMoviesByCustomerIdAsync(int customerId);
        Task DeleteMovieAsync(int id);
    }
}
