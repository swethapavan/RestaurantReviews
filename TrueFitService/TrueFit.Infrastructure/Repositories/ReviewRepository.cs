using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using log4net;
using TrueFit.Core;

namespace TrueFit.Infrastructure
{
    public class ReviewRepository : BaseRepository,IReviewRepository
    {
        protected ILog _reviewRepologger = LogManager.GetLogger(typeof(ReviewRepository));
        public ReviewRepository(IDbContext dbContext):base(dbContext)
        {
           
        }

        #region public methods

        public Result<IEnumerable<Review>> GetReviewsByUser(string userId)
        {
            Result<IEnumerable<Review>> result = new Result<IEnumerable<Review>>();
            try
            {
                result.output = _dbContext.Reviews.ToList().Where(x => x.UserId == userId);
            }
            catch (Exception ex)
            {
                _reviewRepologger.Error("Error:", ex);
                result.AddError(ex);
            }
            return result;
        }
        public Result<Review> AddReview(Review review)
        {
            Result<Review> result = new Result<Review>();
            try
            {
                _dbContext.Reviews.Add(review);
                int noOfReviewsByRestaurant = _dbContext.Reviews.Count(x => x.RestaurantId == review.RestaurantId) + 1;
                var restaurant = _dbContext.Restaurants.SingleOrDefault(x => x.Id == review.RestaurantId);
                restaurant.Rating = (restaurant.Rating ?? 0 + review.Rating) / noOfReviewsByRestaurant;
                _dbContext.SaveChanges();
                result.output = review;
            }
            catch (DbEntityValidationException dbEx)
            {
                HandleValidationException(result, dbEx);
            }
            catch (Exception ex)
            {
                _reviewRepologger.Error("Error:", ex);
                result.AddError(ex);
            }
            return result;
        }
        public Result<bool> DeleteReview(Review review)
        {
            Result<bool> result = new Result<bool>();
            try
            {
                var res = _dbContext.Reviews.SingleOrDefault(x => x.UserId == review.UserId
                                    && x.RestaurantId == review.RestaurantId);
                if (res != null)
                {
                    _dbContext.Reviews.Remove(res);
                    _dbContext.SaveChanges();
                    result.output = true;
                }
                else
                {
                    result.output = false;
                    result.AddError("Unable to delete review");
                }
            }
            catch (Exception ex)
            {
                _reviewRepologger.Error("Error:", ex);
                result.output = false;
                result.AddError(ex);
            }
            return result;
        }
        #endregion
    }
}
