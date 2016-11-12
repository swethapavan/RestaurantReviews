using System;
using System.Data.Entity;
using TrueFit.Core;

namespace TrueFit.Infrastructure
{
    public class TrueFitContext : DbContext, IDbContext
    {
        public TrueFitContext() : base("DefaultConnection")
         {
            Database.SetInitializer<TrueFitContext>(null); 
         }
        public DbSet<Restaurant> Restaurants
        {
            get; set;
        }

        public DbSet<Review> Reviews
        {
            get; set;
        }
    }
}
