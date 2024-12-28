using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCleanArchitectureApp.Domain.RepositoryContracts
{
    public interface IReviewRepository
    {
        Task AddAsync(Review review);
        Task<List<Review>> GetReviewsByMovieIdAsync(int movieId);


    }
}
