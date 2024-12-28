using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCleanArchitectureApp.Domain.RepositoryContracts
{
    public interface IGenreRepository
    {
        Task<List<Genre>> GetAllAsync();  
        Task<Genre> GetByIdAsync(int id);
        
    }
}
