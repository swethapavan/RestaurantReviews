using System;
using System.Collections.Generic;


namespace TrueFit.Core
{
    public interface IReviewRepository :IDisposable
    {
        Result<Review> AddReview(Review review);
        Result<IEnumerable<Review>> GetReviewsByUser(string userId);
        Result<bool> DeleteReview(Review review);
    }
}
