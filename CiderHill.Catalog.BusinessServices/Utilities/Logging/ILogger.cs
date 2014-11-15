using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CiderHill.Catalog.Utilities.Logging
{
    public interface ILogger
    {
        void Error(Exception exception);

        void Error(string message, Exception exception);

        void Info(string message);

        void Warn(string message);
    }
}
