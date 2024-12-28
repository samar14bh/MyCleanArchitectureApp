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
            // Récupérer tous les clients via le service
            var customers = await _customerService.GetAllCustomersAsync();

            // Créer un modèle de vue pour la liste des clients
            

            // Passer le modèle à la vue
            return View(customers);
        }


        // GET: Customer/Details/5
        [Route("Customer/Details/{customerId}")]

        public async Task<IActionResult> Details(int customerId)
        {
            // Récupérer le client par ID
            var customer = await _customerService.GetCustomerByIdAsync(customerId);

            if (customer == null)
            {
                return NotFound();
            }

            // Récupérer les films favoris du client
            var favoriteMovies = await _customerService.GetFavoriteMoviesAsync(customerId);

            // Créer un modèle pour la vue
            var model = new CustomerDetailsViewModel
            {
                Customer = customer,
                FavoriteMovies = favoriteMovies.ToList()
            };

            // Passer le modèle à la vue
            return View(model);
        }

        // GET: Customer/Add
        public IActionResult AddCustomer()
        {
            // Return the view to add a new customer
            return View();
        }

        // POST: Customer/Add
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

        // GET: Customer/Delete/5
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

        // POST: Customer/Delete/5
        [HttpPost, ActionName("DeleteCustomer")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int customerId)
        {
            await _customerService.DeleteCustomerAsync(customerId);
            return RedirectToAction(nameof(Index)); // Redirect to the customer list after deletion
        }


        // GET: Customer/AddMovieToFavorite/5
        [Route("Customer/AddMovieToFavorite/{customerId}")]
        public async Task<IActionResult> AddMovieToFavorite(int customerId)
        {
            var customer = await _customerService.GetCustomerByIdAsync(customerId);
            if (customer == null)
            {
                return NotFound();
            }

            // Get the list of all movies that could be added to the favorite list
            var movies = await _movieService.GetAllMoviesAsync();

            // Prepare a model for the view
            var model = new AddMovieToFavoriteViewModel
            {
                CustomerId = customerId,
                Movies = movies
            };

            return View(model);
        }

        // POST: Customer/AddMovieToFavorite/5
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

        // Other actions like Details, AddCustomer, DeleteCustomer, etc.
    }
}

