using CiderHill.Catalog.BusinessServices.Services;
using CiderHill.Catalog.Data.Entities;
using CiderHill.Catalog.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CiderHill.Catalog.Web.Controllers
{
    public class TreeController : BaseController
    {
        private readonly TreeService treeService;
        private readonly VendorService vendorService;

        public TreeController()
        {
            this.treeService = new TreeService(this.Logger, this.User);
            this.vendorService = new VendorService(this.Logger, this.User);
        }

        public ActionResult Index()
        {
            var treeView = new TreeViewModel();
            treeView.Trees = treeService.GetAllTrees().Result;

            return View(treeView);
        }

    }
}
