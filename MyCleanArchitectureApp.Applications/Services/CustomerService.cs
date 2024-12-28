using MyCleanArchitectureApp.Domain.RepositoryContracts;
using MyCleanArchitectureApp.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCleanArchitectureApp.Applications.Services
{
    public class CustomerService
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IMovieRepository _movieRepository;

        public CustomerService(ICustomerRepository customerRepository, IMovieRepository movieRepository)
        {
            _customerRepository = customerRepository;
            _movieRepository = movieRepository;
        }

        public async Task<List<Customer>> GetAllCustomersAsync()
        {
            return await _customerRepository.GetAllAsync();
        }

        public async Task<Customer> GetCustomerByIdAsync(int customerId)
        {
            return await _customerRepository.GetByIdAsync(customerId);
        }

        public async Task<List<Movie>> GetFavoriteMoviesAsync(int customerId)
        {
            return await _movieRepository.GetFavoriteMoviesByCustomerIdAsync(customerId);
        }
    }
}

