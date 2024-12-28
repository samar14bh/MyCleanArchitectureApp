using MyCleanArchitectureApp.Applications.ServicesContracts;
using MyCleanArchitectureApp.Domain.RepositoryContracts;
using MyCleanArchitectureApp.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyCleanArchitectureApp.Domain.RepositoryContracts;
namespace MyCleanArchitectureApp.Applications.Services
{
    public class GenreService : IGenreService
    {
        private readonly IGenreRepository _genreRepository;
        private readonly IMovieRepository _movieRepository;

        public GenreService(IGenreRepository genreRepository, IMovieRepository movieRepository)
        {
            _genreRepository = genreRepository;
            _movieRepository = movieRepository;
        }

        public async Task<List<Genre>> GetAllGenresAsync()
        {
            return await _genreRepository.GetAllAsync();
        }

        public async Task<Genre> GetGenreByIdAsync(int genreId)
        {
            return await _genreRepository.GetByIdAsync(genreId);
        }

        public async Task<List<Movie>> GetMoviesByGenreAsync(int genreId)
        {
            return await _movieRepository.GetMoviesByGenreAsync(genreId);
        }
    }
}
