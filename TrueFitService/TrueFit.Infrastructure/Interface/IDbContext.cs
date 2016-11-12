using System;
using System.Collections.Generic;
using System.Data.Entity;
using TrueFit.Core;

namespace TrueFit.Core
{
    public interface IDbContext :IDisposable
    {
        DbSet<Review> Reviews
        {
            get; set;
        }
        DbSet<Restaurant> Restaurants
        {
            get; set;
        }
     
        int SaveChanges();
    }
}
