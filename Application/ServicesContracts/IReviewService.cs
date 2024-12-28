using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyCleanArchitectureApp.Domain;
using MyCleanArchitectureApp.Domain.RepositoryContracts;

namespace MyCleanArchitectureApp.Applications.ServicesContracts
{
    public interface IReviewService
    {
        Task AddReviewAsync(Review review);

        // Method to get reviews by movie ID asynchronously
        Task<IEnumerable<Review>> GetReviewsByMovieIdAsync(int movieId);

    }
}
