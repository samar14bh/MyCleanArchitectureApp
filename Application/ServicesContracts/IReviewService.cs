
using MyCleanArchitectureApp.Domain;

namespace MyCleanArchitectureApp.Applications.ServicesContracts
{
    public interface IReviewService
    {
        Task AddReviewAsync(Review review);

        
        Task<IEnumerable<Review>> GetReviewsByMovieIdAsync(int movieId);

    }
}
