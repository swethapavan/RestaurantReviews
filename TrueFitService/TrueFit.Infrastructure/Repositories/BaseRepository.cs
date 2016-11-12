using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;
using TrueFit.Core;

namespace TrueFit.Infrastructure { 
    public abstract class BaseRepository :IDisposable
    {
        #region protected members
       
        protected IDbContext _dbContext;
        protected ILog _logger;
        #endregion

        public BaseRepository(IDbContext dbContext)
        {
            _dbContext = dbContext;
            _logger = LogManager.GetLogger(this.GetType());
        }

        public void Dispose()
        { 
            Dispose(true);
            // Suppress finalization.
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _dbContext.Dispose();
            }
        }
        protected void HandleValidationException<T>(Result<T> result, DbEntityValidationException dbEx)
        {
            _logger.Error("Error:", dbEx);
            foreach (var validationErrors in dbEx.EntityValidationErrors)
            {
                foreach (var validationError in validationErrors.ValidationErrors)
                {
                    result.AddError(validationError.ErrorMessage);
                }
            }
        }

    }
}
