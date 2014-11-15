using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CiderHill.Catalog.BusinessServices.Services
{
    /// <summary>
    /// Thrown when you want to return an error message back through the service,
    /// but don't want it to be logged as an actual application error
    /// </summary>
    public class ServiceValidationException : ApplicationException
    {
        /// <summary>
        /// Initializes a new instance of the ServiceValidationException class
        /// </summary>
        public ServiceValidationException(string errorMessage)
            : base(errorMessage)
        {
        }
    }
}
