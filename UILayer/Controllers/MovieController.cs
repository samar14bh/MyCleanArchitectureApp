using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyCleanArchitectureApp.Applications.Services;
using MyCleanArchitectureApp.Applications.ServicesContracts;
using MyCleanArchitectureApp.Domain;
using MyCleanArchitectureApp.UI.ViewModel;

namespace MyCleanArchitecture.UI.Controllers
{
    // Define the MVC controller with a route pattern for standard web views
    public class MovieController : Controller
    {
        private readonly IMoviesService _movieService;
        private readonly IGenreService _genreService;
        private readonly IReviewService _reviewService;

        public MovieController(IMoviesService movieService, IGenreService genreService, IReviewService reviewService)
        {
            _movieService = movieService;
            _genreService = genreService;
            _reviewService = reviewService;
        }
        
        // GET: Movie
        public async Task<IActionResult> Index()
        {
            var movies = await _movieService.GetAllMoviesAsync(); // Using MovieService to get movies
            return View(movies);
        }
        public async Task<IActionResult> ListByGenre()
        {
            // Get all genres (this can be done through GenreService)
            var genres = await _genreService.GetAllGenresAsync();

            // Initialize a dictionary to hold movies grouped by genre
            var groupedMovies = new Dictionary<string, List<Movie>>();

            // Loop through each genre and fetch the movies by genre
            foreach (var genre in genres)
            {
                var movies = await _movieService.GetMoviesByGenreAsync(genre.Id);

                // Add movies to the dictionary, grouped by genre name
                groupedMovies[genre.Name] = movies.ToList();
            }

            // Create a model to pass to the view
            var model = new MovieListByGenreViewModel
            {
                GroupedMovies = groupedMovies
            };

            // Return the view with the model
            return View(model);
        }
        // GET: Movie/Create
        public async Task<IActionResult> Create()
        {
            var genres = await _genreService.GetAllGenresAsync();  // Fetching genres from genre repository
            ViewBag.Genres = new SelectList(genres, "Id", "Name");



            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(MovieViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var movie = new Movie
                    {
                        Name = model.Name,
                        GenreId = model.GenreId,
                        AverageRating = model.AverageRating
                    };

                    await _movieService.AddMovieAsync(movie);  // Assuming service method adds the movie
                    return RedirectToAction(nameof(Index));
                }

                // Repopulate genres dropdown in case of validation failure
                var genres = await _genreService.GetAllGenresAsync();
                ViewBag.Genres = new SelectList(genres, "Id", "Name");

                return View(model);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Error creating movie: {ex.Message}");
                var genres = await _genreService.GetAllGenresAsync();
                ViewBag.Genres = new SelectList(genres, "Id", "Name");
                return View(model);
            }
        }
        // GET: Movie/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var movie = await _movieService.GetMovieByIdAsync(id);  // Using MovieService to get movie by ID
            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _movieService.DeleteMovieAsync(id);  // Using MovieService to delete movie by ID
            return RedirectToAction(nameof(Index));
        }

        // GET: Movie/Edit/5




        // GET: Movie/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            // Fetch the movie by id
            var movie = await _movieService.GetMovieByIdAsync(id);  // Assuming this service method exists to fetch the movie by id

            if (movie == null)
            {
                return NotFound();
            }

            // Fetch genres to populate the dropdown
            var genres = await _genreService.GetAllGenresAsync();

            // Create the view model for the movie
            var model = new MovieViewModel
            {
                Id = movie.Id,
                Name = movie.Name,
                GenreId = movie.GenreId,  // Set the genre id of the movie to pre-select it in the dropdown
                AverageRating = movie.AverageRating
            };

            // Pass genres to ViewBag for the dropdown
            ViewBag.Genres = new SelectList(genres, "Id", "Name");

            return View(model);
        }

        // POST: Movie/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, MovieViewModel model)
        {
            if (id != model.Id)
            {
                return NotFound();
            }

            try
            {
                if (ModelState.IsValid)
                {
                    // Fetch the existing movie from the database
                    var movie = await _movieService.GetMovieByIdAsync(id);

                    if (movie == null)
                    {
                        return NotFound();
                    }

                    // Update the properties of the existing movie
                    movie.Name = model.Name;
                    movie.GenreId = model.GenreId;
                    movie.AverageRating = model.AverageRating;

                    // Save the updated movie back to the database
                    await _movieService.UpdateMovieAsync(movie);

                    return RedirectToAction(nameof(Index));
                }

                // Repopulate the genres dropdown in case of validation failure
                var genres = await _genreService.GetAllGenresAsync();
                ViewBag.Genres = new SelectList(genres, "Id", "Name");

                return View(model);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Error updating movie: {ex.Message}");
                var genres = await _genreService.GetAllGenresAsync();
                ViewBag.Genres = new SelectList(genres, "Id", "Name");

                return View(model);
            }
        }


        // GET: Movie/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var movie = await _movieService.GetMovieByIdAsync(id);

            if (movie == null)
            {
                return NotFound();
            }

            // Get the reviews for the movie
            var reviews = await _reviewService.GetReviewsByMovieIdAsync(id);

            // Create the model for the view
            var model = new MovieDetailsViewModel
            {
                Movie = movie,
                Reviews = reviews
            };

            // Return the view with the model
            return View(model);
        }


        public async Task<IActionResult> AddReview(int movieId)
        {
            var movie = await _movieService.GetMovieByIdAsync(movieId);
            if (movie == null)
            {
                return NotFound();
            }

            var model = new ReviewViewModel
            {
                MovieId = movieId
            };

            return View(model);
        }

        // POST: Movie/AddReview/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddReview(ReviewViewModel model)
        {
            if (ModelState.IsValid)
            {
                var review = new Review
                {
                    MovieId = model.MovieId,
                    Text = model.Text,
                    Rating = model.Rating
                };

                try
                {
                    // Call the service method to add the review
                    await _movieService.AddMovieReviewAsync(review);

                    // Redirect back to the movie details page
                    return RedirectToAction(nameof(Index));

                }
                catch (Exception ex)
                {
                    // Handle error (e.g., log the exception, return error view)
                    ModelState.AddModelError("", ex.Message);
                }
            }

            // If validation fails, return to the same page
            return View(model);
        }

    }
}