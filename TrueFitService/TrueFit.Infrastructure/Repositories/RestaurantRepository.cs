using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Reflection;
using log4net;
using TrueFit.Core;

namespace TrueFit.Infrastructure
{
    public class RestaurantRepository : BaseRepository, IRestaurantRepository
    {
        public RestaurantRepository(IDbContext dbContext):base(dbContext)
        {          
        }

        #region public methods
        public Result<IEnumerable<Restaurant>> GetRestaurantsByCity(string city)
        {
            Result<IEnumerable<Restaurant>> result = new Result<IEnumerable<Restaurant>>();
            try {
              result.output = _dbContext.Restaurants.ToList().Where(x => x.City == city).OrderByDescending(x => x.Rating);
            }
            catch(Exception ex)
            {
                result.AddError(ex);
                _logger.Debug(ex.Message, ex);
            }
            return result;
        }
        public Result<Restaurant> AddRestaurant(Restaurant restaurant)
        {
            Result<Restaurant> result = new Result<Restaurant>();
            try {
               
                    _dbContext.Restaurants.Add(restaurant);
                    _dbContext.SaveChanges();
                    result.output =
                        _dbContext.Restaurants.SingleOrDefault(x => x.Name == restaurant.Name
                                                && x.ZipCode == restaurant.ZipCode);
                
            }
            catch (DbEntityValidationException dbEx)
            {
                HandleValidationException(result, dbEx);
            }
            catch (Exception ex)
            {
                result.AddError(ex);
                _logger.Debug(ex.Message, ex);
            }
            return result;
        }

        #endregion
    }
}
