using Microsoft.EntityFrameworkCore;
using MyCleanArchitectureApp.Domain.RepositoryContracts;
using MyCleanArchitectureApp.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCleanArchitectureApp.Infrastructure.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly AppDbContext _context;

        public CustomerRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Customer>> GetAllAsync()
        {
            return await _context.Customers.ToListAsync();
        }

        public async Task<Customer> GetByIdAsync(int id)
        {
            return await _context.Customers.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task AddAsync(Customer customer)
        {
            await _context.Customers.AddAsync(customer);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Customer customer)
        {
            _context.Customers.Update(customer);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Customer customer)
        {
            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();
        }
        public async Task AddMovieToFavoriteAsync(int customerId, int movieId)
        {
            var customer = await _context.Customers
                .Include(c => c.FavoriteMovies)
                .FirstOrDefaultAsync(c => c.Id == customerId);

            var movie = await _context.Movies
                .FirstOrDefaultAsync(m => m.Id == movieId);

            if (customer == null || movie == null)
            {
                throw new ArgumentException("Customer or Movie not found.");
            }

            // Add movie to favorites if not already added
            if (!customer.FavoriteMovies.Contains(movie))
            {
                customer.FavoriteMovies.Add(movie);
                await _context.SaveChangesAsync();
            }
        } 
    }


}

