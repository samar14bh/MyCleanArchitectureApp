using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MyCleanArchitectureApp.Domain;
using MyCleanArchitectureApp.Domain.RepositoryContracts;
namespace MyCleanArchitectureApp.Infrastructure.Repositories
{
   
        public class MovieRepository : IMovieRepository
        {
            private readonly AppDbContext _dbContext;

            public MovieRepository(AppDbContext dbContext)
            {
                _dbContext = dbContext;
            }

            public async Task<Movie> GetByIdAsync(int id)
            {
            return await _dbContext.Movies
             .Include(m => m.Genre)
             .FirstOrDefaultAsync(m => m.Id == id);
        }

            public async Task<IEnumerable<Movie>> GetAllMoviesAsync()
            {
                return await _dbContext.Movies.ToListAsync();
            }

            public async Task AddAsync(Movie movie)
            {
                await _dbContext.Movies.AddAsync(movie);
                await _dbContext.SaveChangesAsync();
            }

            public async Task UpdateAsync(Movie movie)
            {
                _dbContext.Movies.Update(movie);
                await _dbContext.SaveChangesAsync();
            }

            public async Task<IEnumerable<Movie>> GetMoviesByGenreAsync(int genreId)
            {
                return await _dbContext.Movies
                    .Where(m => m.GenreId == genreId)
                    .ToListAsync();
            }

            public async Task<IEnumerable<Movie>> GetFavoriteMoviesByCustomerIdAsync(int customerId)
            {
                return await _dbContext.Movies
                    .Where(m => m.FavoriteCustomers.Any(fc => fc.Id == customerId))
                    .ToListAsync();
            }


        public async Task DeleteMovieAsync(int id)
        {
            var movie = await GetByIdAsync(id);
            _dbContext.Movies.Remove(movie);
            await _dbContext.SaveChangesAsync();
        }
    }
    }
