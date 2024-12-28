using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyCleanArchitectureApp.Domain;

namespace MyCleanArchitectureApp.Applications
{
    public  interface ICustomerService
    {
        Task<List<Customer>> GetAllCustomersAsync();

        // Method to get a customer by their ID
        Task<Customer> GetCustomerByIdAsync(int customerId);

        // Method to get the favorite movies of a customer
        Task<List<Movie>> GetFavoriteMoviesAsync(int customerId);
    }
}
