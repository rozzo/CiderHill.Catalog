using CiderHill.Catalog.BusinessServices.Services;
using CiderHill.Catalog.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CiderHill.Catalog.Web.Controllers
{
    public class HomeController : BaseController
    {
        private readonly TreeService treeService;
        private readonly VendorService vendorService;

        public HomeController()
        {
            this.treeService = new TreeService(this.Logger, this.User);
            this.vendorService = new VendorService(this.Logger, this.User);
        }

        public ActionResult Index()
        {
            var tree = treeService.GetTreeById(1);
            //this.test();

            var msg = "";

            if (tree.HasError)
            {
                msg += tree.Exception;
            }
            else
            {
                if (tree.Result != null)
                    msg += tree.Result.Description;
                else
                    msg += "NULL";
            }

            ViewBag.Message = "Tree data:" + msg;

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        private void test()
        {
            var vendors = vendorService.SearchVendors("Fedco");

            if (vendors.Result.Count == 0)
            {
                Logger.Info("Creating");
                Vendor vendor = new Vendor()
                {
                    Name = "Fedco Seeds",
                    Phone = "207-426-9900",
                    Email = "",
                    WebSite = "http://www.fedcoseeds.com",
                    ProductBaseUrl = "http://www.fedcoseeds.com/seeds/search.php?item="
                };
                vendorService.CreateVendor(vendor);
            }
            else
            {
                Logger.Info("Exists");
                var vendor = vendors.Result.First();
                vendor.WebSite = "www.fedcoseeds.com";

                vendorService.UpdateVendor(vendor);
            }
        }
    }
}
