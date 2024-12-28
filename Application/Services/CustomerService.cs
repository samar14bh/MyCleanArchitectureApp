using MyCleanArchitectureApp.Domain.RepositoryContracts;
using MyCleanArchitectureApp.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyCleanArchitectureApp.Applications.ServicesContracts;

namespace MyCleanArchitectureApp.Applications.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IMovieRepository _movieRepository;

        public CustomerService(ICustomerRepository customerRepository, IMovieRepository movieRepository)
        {
            _customerRepository = customerRepository;
            _movieRepository = movieRepository;
        }

        public async Task<IEnumerable<Customer>> GetAllCustomersAsync()
        {
            return await _customerRepository.GetAllAsync();
        }

        public async Task<Customer> GetCustomerByIdAsync(int customerId)
        {
            return await _customerRepository.GetByIdAsync(customerId);
        }

        public async Task<IEnumerable<Movie>> GetFavoriteMoviesAsync(int customerId)
        {
            return await _movieRepository.GetFavoriteMoviesByCustomerIdAsync(customerId);
        }

        public async Task DeleteCustomerAsync(int customerId)
        {
            var customer = await _customerRepository.GetByIdAsync(customerId);
            await _customerRepository.DeleteAsync(customer);
        }

        public async Task AddCustomerAsync(Customer customer)
        {
            await _customerRepository.AddAsync(customer);
        }


        public async Task AddMovieToFavoriteAsync(int customerId, int movieId)
        {
            // Call repository method to add the movie to the favorite list
            await _customerRepository.AddMovieToFavoriteAsync(customerId, movieId);
        }

    }
}

