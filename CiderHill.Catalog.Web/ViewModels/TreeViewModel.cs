using CiderHill.Catalog.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CiderHill.Catalog.Web.ViewModels
{
    public class TreeViewModel : BaseViewModel
    {
        public List<Tree> Trees { get; set; }
    }
}