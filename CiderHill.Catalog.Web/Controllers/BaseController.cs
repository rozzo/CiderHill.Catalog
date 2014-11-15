using CiderHill.Catalog.Utilities.Logging;
using System.Web.Mvc;

namespace CiderHill.Catalog.Web.Controllers
{
    public class BaseController : Controller
    {
        public BaseController()
        {
            ServiceRoot = "/CiderHill/";
            Logger = new Log4NetLogger();
            User = "user";
        }

        public ILogger Logger { get; set; }

        public string ServiceRoot { get; private set; }

        protected void Info(string message)
        {
            this.Logger.Info(message);
        }

        protected string User { get; set; }
    }
}