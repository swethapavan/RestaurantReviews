using System.Net;
using System.Net.Http;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using RestaurantReviews.Extensions;
using TrueFit.Core;

namespace TrueFitService.Controllers
{
    [Authorize]
    public class ReviewController : ApiController
    {
        private IReviewRepository _reviewRepository;

        public ReviewController(IReviewRepository reviewRepository)
        {
            _reviewRepository = reviewRepository;
        }

        public HttpResponseMessage Get(string userId )
        {
            var result = _reviewRepository.GetReviewsByUser(userId);
            return this.HandleResult(result);
        }

        public HttpResponseMessage Post([FromBody]Review review)
        {
            review.UserId = review.UserId ?? User.Identity.GetUserId();
            if (review.UserId != User.Identity.GetUserId())
            {
                return Request.CreateErrorResponse(
                    HttpStatusCode.Forbidden
                    , "Not allowed to post on behalf of other user!!");
            }
            var result = _reviewRepository.AddReview(review);
            return this.HandleResult(result);
        }

        public HttpResponseMessage Delete([FromBody]Review review)
        {
            if (string.IsNullOrWhiteSpace(review.UserId))
            {
                review.UserId = User.Identity.GetUserId();
            }
            var result = _reviewRepository.DeleteReview(review);
            return this.HandleResult(result);
        }
    }
}