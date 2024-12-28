
using MyCleanArchitectureApp.Domain;

namespace MyCleanArchitectureApp.Applications.ServicesContracts
{
    public  interface ICustomerService
    {
        Task<IEnumerable<Customer>> GetAllCustomersAsync();

        Task<Customer> GetCustomerByIdAsync(int customerId);

        Task<IEnumerable<Movie>> GetFavoriteMoviesAsync(int customerId);
        Task DeleteCustomerAsync(int customerId);
        Task AddCustomerAsync(Customer customer);
        Task AddMovieToFavoriteAsync(int customerId, int movieId);

    }
}
