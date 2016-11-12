using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrueFit.Core
{
    public class Result<T>
    {
        private List<string> _errors;

        #region public properties
        public T output { get; set; }
        public bool HasErrors { get { return _errors != null && _errors.Count > 0; } }
        public string Error { get { return _errors == null ? string.Empty : string.Join(",", _errors); } }
        #endregion

        #region constructors
        public Result(T value){
            output = value;
            }
        public Result()
        {
        }
        #endregion

        #region public methods
        public void AddError(Exception ex)
        {
            if(_errors == null)
            {
                _errors = new List<string>();
            }
            _errors.Add(ex.Message);
        }
        public void AddError(string errorMsg)
        {
            if (_errors == null)
            {
                _errors = new List<string>();
            }
            _errors.Add(errorMsg);
        }
        #endregion
    }
}
