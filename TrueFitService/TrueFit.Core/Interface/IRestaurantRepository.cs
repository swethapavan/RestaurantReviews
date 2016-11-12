using System;
using System.Collections.Generic;


namespace TrueFit.Core
{
    public interface IRestaurantRepository :IDisposable
    {
        Result<Restaurant> AddRestaurant(Restaurant restaurant);
        Result<IEnumerable<Restaurant>> GetRestaurantsByCity(string city);
    }
}
