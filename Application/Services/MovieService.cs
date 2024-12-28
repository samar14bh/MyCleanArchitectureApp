using MyCleanArchitectureApp.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyCleanArchitectureApp.Domain.RepositoryContracts;
using MyCleanArchitectureApp.Applications.ServicesContracts;
namespace MyCleanArchitectureApp.Applications.Services
{
    public  class MovieService : IMoviesService
    {
        private readonly IMovieRepository _movieRepository;
        private readonly IReviewRepository _reviewRepository;

        public MovieService(IMovieRepository movieRepository, IReviewRepository reviewRepository)
        {
            _movieRepository = movieRepository;
            _reviewRepository = reviewRepository;
        }
        
        public async Task<IEnumerable<Movie>> GetAllMoviesAsync()
        {
            return await _movieRepository.GetAllMoviesAsync();
        }

        public async Task<Movie> GetMovieByIdAsync(int id)
        {
            return await _movieRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Movie>> GetMoviesByGenreAsync(int genreID)
        {
            return await _movieRepository.GetMoviesByGenreAsync(genreID);
        }

        public async Task AddMovieAsync(Movie movie)
        {
            await _movieRepository.AddAsync(movie);
        }

        public async Task UpdateMovieAsync(Movie movie)
        {
            // Ensure that the movie is valid (e.g., exists in the database) before updating
            var existingMovie = await _movieRepository.GetByIdAsync(movie.Id);

            if (existingMovie == null)
            {
                throw new KeyNotFoundException($"Movie with ID {movie.Id} not found.");
            }

            // Update the movie
            await _movieRepository.UpdateAsync(movie);
        }
        public async Task AddMovieReviewAsync(Review review)
        {
            // Ensure movie exists
            var movie = await _movieRepository.GetByIdAsync(review.MovieId);
            if (movie == null)
            {
                throw new KeyNotFoundException("Movie not found.");
            }

            // Validate rating (between 1 and 5)
            if (review.Rating < 1 || review.Rating > 5)
            {
                throw new ArgumentOutOfRangeException("Rating must be between 1 and 5.");
            }

            // Save review
            await _reviewRepository.AddAsync(review);

            // Recalculate average rating
            var reviews = await _reviewRepository.GetReviewsByMovieIdAsync(review.MovieId);
            var averageRating = reviews.Average(r => r.Rating);
            movie.AverageRating = averageRating;

            await _movieRepository.UpdateAsync(movie);
        }

        public async Task DeleteMovieAsync(int id)
        {
            // Ensure that the movie exists before deleting
            var existingMovie = await _movieRepository.GetByIdAsync(id);

            if (existingMovie == null)
            {
                throw new KeyNotFoundException($"Movie with ID {id} not found.");
            }

            // Delete the movie
            await _movieRepository.DeleteMovieAsync(id);
        }
    }

}

