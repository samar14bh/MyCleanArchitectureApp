using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyCleanArchitectureApp.Applications.Services;
using MyCleanArchitectureApp.Applications.ServicesContracts;
using MyCleanArchitectureApp.Domain;
using MyCleanArchitectureApp.UI.ViewModel;

namespace MyCleanArchitecture.UI.Controllers
{
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
            var genres = await _genreService.GetAllGenresAsync();

            var groupedMovies = new Dictionary<string, List<Movie>>();

            foreach (var genre in genres)
            {
                var movies = await _movieService.GetMoviesByGenreAsync(genre.Id);

                groupedMovies[genre.Name] = movies.ToList();
            }

            var model = new MovieListByGenreViewModel
            {
                GroupedMovies = groupedMovies
            };

            return View(model);
        }
        public async Task<IActionResult> Create()
        {
            var genres = await _genreService.GetAllGenresAsync(); 
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
        public async Task<IActionResult> Delete(int id)
        {
            var movie = await _movieService.GetMovieByIdAsync(id); 
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
            await _movieService.DeleteMovieAsync(id);
            return RedirectToAction(nameof(Index));
        }

        
        public async Task<IActionResult> Edit(int id)
        {
            var movie = await _movieService.GetMovieByIdAsync(id);  
            if (movie == null)
            {
                return NotFound();
            }

            var genres = await _genreService.GetAllGenresAsync();

            var model = new MovieViewModel
            {
                Id = movie.Id,
                Name = movie.Name,
                GenreId = movie.GenreId,  
                AverageRating = movie.AverageRating
            };

            ViewBag.Genres = new SelectList(genres, "Id", "Name");

            return View(model);
        }

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
                    var movie = await _movieService.GetMovieByIdAsync(id);

                    if (movie == null)
                    {
                        return NotFound();
                    }

                    movie.Name = model.Name;
                    movie.GenreId = model.GenreId;
                    movie.AverageRating = model.AverageRating;

                    await _movieService.UpdateMovieAsync(movie);

                    return RedirectToAction(nameof(Index));
                }

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


        public async Task<IActionResult> Details(int id)
        {
            var movie = await _movieService.GetMovieByIdAsync(id);

            if (movie == null)
            {
                return NotFound();
            }

           
            var reviews = await _reviewService.GetReviewsByMovieIdAsync(id);

            var model = new MovieDetailsViewModel
            {
                Movie = movie,
                Reviews = reviews
            };

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
                    
                    await _movieService.AddMovieReviewAsync(review);

                    return RedirectToAction(nameof(Index));

                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }

            return View(model);
        }

    }
}