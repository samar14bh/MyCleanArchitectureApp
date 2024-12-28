using MyCleanArchitectureApp.Domain.RepositoryContracts;
using MyCleanArchitectureApp.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
namespace MyCleanArchitectureApp.Infrastructure.Repositories
{
    public class GenreRepository : IGenreRepository
    {
        private readonly AppDbContext _context;

        public GenreRepository(AppDbContext context)
        {
            _context = context;
        }

        // Get all genres asynchronously
        public async Task<IEnumerable<Genre>> GetAllAsync()
        {
            return await _context.Genres.ToListAsync();
        }

        // Get a genre by its ID asynchronously
        public async Task<Genre> GetByIdAsync(int id)
        {
            return await _context.Genres
                .Where(g => g.Id == id)
                .FirstOrDefaultAsync();
        }
    }
}
