using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyCleanArchitectureApp.Domain;
namespace MyCleanArchitectureApp.Applications.ServicesContracts
{
    public  interface IMoviesService
    {
        Task<IEnumerable<Movie>> GetAllMoviesAsync();
        Task<Movie> GetMovieByIdAsync(int id);
        Task AddMovieAsync(Movie movie);
        Task UpdateMovieAsync(Movie movie);
        Task<IEnumerable<Movie>> GetMoviesByGenreAsync(int genreId);
        Task AddMovieReviewAsync(Review review);
        Task DeleteMovieAsync(int id);
    }
}
