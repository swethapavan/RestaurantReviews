using Microsoft.VisualStudio.TestTools.UnitTesting;
using TrueFitService.Controllers;
using System.Collections.Generic;
using Moq;
using TrueFit.Core;
using System.Net;
using System.Security.Principal;

namespace TrueFitService.Tests
{
    [TestClass()]
    public class ReviewControllerTests
    {
        Mock<IReviewRepository> _mockReviewRepository;

        [TestInitialize]
        public void Initialize()
        {
            _mockReviewRepository = new Mock<IReviewRepository>();
           
            _mockReviewRepository.Setup(
                x => x.DeleteReview(It.IsAny<Review>())).Returns(new Result<bool>(true));
        }
        [TestMethod()]
        public void ReviewControllerTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void GetTest()
        {
            _mockReviewRepository.Setup(x => x.GetReviewsByUser(It.IsAny<string>()))
               .Returns(new Result<IEnumerable<Review>>(ReviewMockBuilder.GetReviews("pittsburgh")));
            ReviewController controller = new ReviewController(_mockReviewRepository.Object);
            controller.Request = MockHttpRequestBuilder.GetRequestObject();
            var httpResponse = controller.Get("test");
            Assert.AreEqual(HttpStatusCode.OK, httpResponse.StatusCode);
        }

        [TestMethod()]
        public void GetNegativeTest()
        {
            _mockReviewRepository.Setup(x => x.GetReviewsByUser(It.IsAny<string>()))
               .Returns(GetNegativeResult<IEnumerable<Review>>());
            ReviewController controller = new ReviewController(_mockReviewRepository.Object);
            controller.Request = MockHttpRequestBuilder.GetRequestObject();
            var httpResponse = controller.Get("test");
            Assert.AreEqual(HttpStatusCode.InternalServerError, httpResponse.StatusCode);
        }

        [TestMethod()]
        public void PostTest()
        {
            Review review = ReviewMockBuilder.GetReview(null, 2);
            _mockReviewRepository.
                Setup(x => x.AddReview(It.IsAny<Review>())).
                Returns(new Result<Review>(review));
           
            ReviewController controller = new ReviewController(_mockReviewRepository.Object);
           
            controller.Request = MockHttpRequestBuilder.GetRequestObject();
            var httpResponse = controller.Post(review);
            Assert.AreEqual(HttpStatusCode.OK, httpResponse.StatusCode);

        }

        [TestMethod()]
        public void PostNegativeTest()
        {
            Review review = ReviewMockBuilder.GetReview(null, 2);
            _mockReviewRepository.
                Setup(x => x.AddReview(It.IsAny<Review>())).
                Returns(GetNegativeResult<Review>());
            ReviewController controller = new ReviewController(_mockReviewRepository.Object);
            controller.Request = MockHttpRequestBuilder.GetRequestObject();
            var httpResponse = controller.Post(review);
            Assert.AreEqual(HttpStatusCode.InternalServerError, httpResponse.StatusCode);
        }

        [TestMethod()]
        public void DeleteTest()
        {
            Review review = ReviewMockBuilder.GetReview(null, 2);
            _mockReviewRepository.
                Setup(x => x.DeleteReview(It.IsAny<Review>())).
                Returns(new Result<bool>(true));
            ReviewController controller = new ReviewController(_mockReviewRepository.Object);
            controller.Request = MockHttpRequestBuilder.GetRequestObject();
            var httpResponse = controller.Delete(review);
            Assert.AreEqual(HttpStatusCode.OK, httpResponse.StatusCode);
        }
        [TestMethod]
        public void NegativeTest()
        {
            Review review = ReviewMockBuilder.GetReview(null, 2);
            _mockReviewRepository.
                Setup(x => x.DeleteReview(It.IsAny<Review>())).
                Returns(GetNegativeResult<bool>());
            ReviewController controller = new ReviewController(_mockReviewRepository.Object);
            controller.Request = MockHttpRequestBuilder.GetRequestObject();
            var httpResponse = controller.Delete(review);
            Assert.AreEqual(HttpStatusCode.InternalServerError, httpResponse.StatusCode);
        }
        public Result<T> GetNegativeResult<T>()
        {
            Result<T> result = new Result<T>();
            result.AddError("error");
            return result;
        }
    }
}