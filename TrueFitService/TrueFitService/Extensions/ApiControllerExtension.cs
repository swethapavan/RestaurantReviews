using System.Net;
using System.Net.Http;
using System.Web.Http;
using TrueFit.Core;

namespace RestaurantReviews.Extensions
{
    public static class ApiControllerExtension
    {
        public static HttpResponseMessage HandleResult<T>(this ApiController controller,Result<T> result)
        {
            HttpResponseMessage response;
            if (result.HasErrors)
            {
                response = controller.Request.CreateErrorResponse(HttpStatusCode.InternalServerError, result.Error);
            }
            else if (result == null)
            {
                response = controller.Request.CreateErrorResponse(HttpStatusCode.NotFound, "Not found");
            }
            else
            {
                response = controller.Request.CreateResponse<Result<T>>(HttpStatusCode.OK, result);
            }
            return response;
        }
    }
}