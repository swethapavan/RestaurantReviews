using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrueFit.Core;

namespace TrueFitService.Tests
{
    public class ReviewMockBuilder
    {
        public static IEnumerable<Review> GetReviews(string userId)
        {
            List<Review> reviewList = new List<Review>();
            for(int i =0; i< 5; i++)
            {
                Review review = GetReview(userId,i+1);

                reviewList.Add(review);
            }
            return reviewList;
        }

        public static Review GetReview(string userId,int restaurantId)
        {
            return new Review
            {
                Title = "Testing123",
                Description = "testtesttesttesttesttestjjh",
                Rating = 5,
                RestaurantId = restaurantId,
                UserId = userId
            };
        }
    }

}
