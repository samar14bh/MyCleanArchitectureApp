using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using MyCleanArchitectureApp.Applications;
using MyCleanArchitectureApp.Applications.ServicesContracts;
using MyCleanArchitectureApp.Domain;
namespace MyCleanArchitectureApp.Web.Controllers
{
    public class MovieController : Controller
    {
        private readonly IMoviesService _moviesService;
        private readonly IReviewService _reviewService;

        public MovieController(IMoviesService moviesService, IReviewService reviewService)
        {
            _moviesService = moviesService;
            _reviewService = reviewService;
        }

        // Action to list all movies
        public async Task<IActionResult> Index()
        {
            var movies = await _moviesService.GetAllMoviesAsync();
            return View(movies);
        }

        // Action to classify movies by genre
        public async Task<IActionResult> ByGenre(string genre)
        {
            var moviesByGenre = await _moviesService.GetMoviesByGenreAsync(genre);
            return View(moviesByGenre);
        }

        // Action to add a review for a movie
        [HttpPost]
        public async Task<IActionResult> AddReview(int movieId, string reviewText, int rating)
        {
            var review = new Review
            {
                MovieId = movieId,
                Text = reviewText,
                Rating = rating,
                
            };

            await _reviewService.AddReviewAsync(review);
            return RedirectToAction("Details", new { id = movieId });
        }

        // Action to calculate the average rating for a movie
        public async Task<IActionResult> AverageRating(int movieId)
        {
            var reviews = await _reviewService.GetReviewsByMovieIdAsync(movieId);
            var averageRating = reviews.Any() ? reviews.Average(r => r.Rating) : 0;

            ViewData["AverageRating"] = averageRating;
            return View("Details", movieId);
        }
    }
}
