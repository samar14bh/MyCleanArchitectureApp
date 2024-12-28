using Microsoft.AspNetCore.Mvc;
using MyCleanArchitectureApp.Applications;

namespace MyCleanArchitectureApp.Web.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        // Action to list all customers
        public async Task<IActionResult> Index()
        {
            var customers = await _customerService.GetAllCustomersAsync();
            return View(customers);
        }

        // Action to view favorite movies of a customer
        public async Task<IActionResult> FavoriteMovies(int customerId)
        {
            var favoriteMovies = await _customerService.GetFavoriteMoviesAsync(customerId);
            return View(favoriteMovies);
        }
    }
}
