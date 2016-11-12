using System.Net.Http;
using System.Web.Http;
using RestaurantReviews.Extensions;
using TrueFit.Core;

namespace TrueFitService.Controllers
{
    [Authorize]
    public class RestaurantController : ApiController
    {
        private IRestaurantRepository _restauranRepository;

        public RestaurantController(IRestaurantRepository restaurantRepository)
        {
            _restauranRepository = restaurantRepository;
        }

        public HttpResponseMessage Get(string city)
        {
            var result = _restauranRepository.GetRestaurantsByCity(city);
            return this.HandleResult(result);            
        }

        public HttpResponseMessage Post([FromBody] Restaurant restaurant)
        {
            var result = _restauranRepository.AddRestaurant(restaurant);
            return this.HandleResult(result);         
        }
    }
}