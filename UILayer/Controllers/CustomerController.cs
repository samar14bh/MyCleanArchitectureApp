using Microsoft.AspNetCore.Mvc;
using MyCleanArchitectureApp.Applications.Services;
using MyCleanArchitectureApp.Domain;
using MyCleanArchitectureApp.Web.Models;
using MyCleanArchitectureApp.Applications.ServicesContracts;
using MyCleanArchitectureApp.UI.ViewModel;
namespace MyCleanArchitectureApp.Web.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ICustomerService _customerService;
        private readonly IMoviesService _movieService;

        public CustomerController(ICustomerService customerService, IMoviesService movieService)
        {
            _customerService = customerService;
            _movieService = movieService;
        }

        public async Task<IActionResult> Index()
        {
            var customers = await _customerService.GetAllCustomersAsync();

            

            return View(customers);
        }


        [Route("Customer/Details/{customerId}")]

        public async Task<IActionResult> Details(int customerId)
        {
            var customer = await _customerService.GetCustomerByIdAsync(customerId);

            if (customer == null)
            {
                return NotFound();
            }

            var favoriteMovies = await _customerService.GetFavoriteMoviesAsync(customerId);

            var model = new CustomerDetailsViewModel
            {
                Customer = customer,
                FavoriteMovies = favoriteMovies.ToList()
            };

            return View(model);
        }

        public IActionResult AddCustomer()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddCustomer(string name)
        {
            if (!string.IsNullOrEmpty(name))
            {
                var newCustomer = new Customer
                {
                    Name = name
                };

                await _customerService.AddCustomerAsync(newCustomer);
                return RedirectToAction(nameof(Index)); // Redirect to the customer list after adding
            }

            // If the name is invalid, return the same view with an error
            ModelState.AddModelError("", "Name is required");
            return View();
        }

        [Route("Customer/Delete/{customerId}")]
        public async Task<IActionResult> DeleteCustomer(int customerId)
        {
            var customer = await _customerService.GetCustomerByIdAsync(customerId);

            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        [HttpPost, ActionName("DeleteCustomer")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int customerId)
        {
            await _customerService.DeleteCustomerAsync(customerId);
            return RedirectToAction(nameof(Index)); // Redirect to the customer list after deletion
        }


        [Route("Customer/AddMovieToFavorite/{customerId}")]
        public async Task<IActionResult> AddMovieToFavorite(int customerId)
        {
            var customer = await _customerService.GetCustomerByIdAsync(customerId);
            if (customer == null)
            {
                return NotFound();
            }

            var movies = await _movieService.GetAllMoviesAsync();

            var model = new AddMovieToFavoriteViewModel
            {
                CustomerId = customerId,
                Movies = movies
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Customer/AddMovieToFavorite/{customerId}")]
        public async Task<IActionResult> AddMovieToFavorite(int customerId, int movieId)
        {
            if (movieId == 0)
            {
                return BadRequest("Movie ID is required.");
            }

            var customer = await _customerService.GetCustomerByIdAsync(customerId);
            var movie = await _movieService.GetMovieByIdAsync(movieId);

            if (customer == null || movie == null)
            {
                return NotFound();
            }

            await _customerService.AddMovieToFavoriteAsync(customerId, movieId);

            return RedirectToAction("Details", new { customerId });
        }

    }
}

