using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyCleanArchitectureApp.Domain;

namespace MyCleanArchitectureApp.Applications.ServicesContracts
{
    public  interface ICustomerService
    {
        Task<IEnumerable<Customer>> GetAllCustomersAsync();

        // Method to get a customer by their ID
        Task<Customer> GetCustomerByIdAsync(int customerId);

        // Method to get the favorite movies of a customer
        Task<IEnumerable<Movie>> GetFavoriteMoviesAsync(int customerId);
        Task DeleteCustomerAsync(int customerId);
        Task AddCustomerAsync(Customer customer);
        Task AddMovieToFavoriteAsync(int customerId, int movieId); // Add this method declaration

    }
}
