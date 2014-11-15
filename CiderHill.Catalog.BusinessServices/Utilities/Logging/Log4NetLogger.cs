using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CiderHill.Catalog.Utilities.Logging
{
    public class Log4NetLogger : ILogger
    {
        private ILog log;

        public Log4NetLogger()
        {
            log4net.Config.XmlConfigurator.Configure();
            this.log = LogManager.GetLogger(this.GetType());
        }

        public void Error(Exception exception)
        {
            this.log.Error(exception.Message, exception);
        }

        public void Error(string message, Exception exception)
        {
            this.log.Error(message, exception);
        }

        public void Info(string message)
        {
            this.log.Info(message);
        }

        public void Warn(string message)
        {
            this.log.Warn(message);
        }
    }
}