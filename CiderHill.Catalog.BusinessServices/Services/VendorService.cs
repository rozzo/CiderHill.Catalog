using CiderHill.Catalog.Data;
using CiderHill.Catalog.Data.Entities;
using CiderHill.Catalog.Utilities.Logging;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CiderHill.Catalog.BusinessServices.Services
{
    public class VendorService : BaseService
    {
        public VendorService(ILogger logger, String user)
            : base(logger, user)
        {
        }

        public ServiceResponse<Vendor> GetVendorById(int id)
        {
            var methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
            LogStart(methodName);

            return this.Execute(() =>
            {
                var ids = new List<int>(new int[] { id });
                var Vendors = GetVendorsById(ids);

                if(Vendors.HasError)
                {
                    throw Vendors.Exception;
                }

                return Vendors.Result.FirstOrDefault();
            });
        }

        public ServiceResponse<List<Vendor>> GetVendorsById(List<int> ids)
        {
            var methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
            LogStart(methodName);

            return this.Execute(() =>
            {
                using (var uow = new UnitOfWork())
                {
                    return uow.VendorRepository.All.Where(x => ids.Contains(x.Id)).ToList();
                }
            });
        }

        public ServiceResponse<int> CreateVendor(Vendor vendor)
        {
            return UpdateVendor(vendor);
        }

        public ServiceResponse<int> UpdateVendor(Vendor vendor)
        {
            var methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
            LogStart(methodName);

            return this.Execute(() =>
            {
                using (var uow = new UnitOfWork())
                {
                    int id = 0;
                    if(vendor.Id == null || vendor.Id == 0){
                        var dbVendor = uow.VendorRepository.Insert(vendor);
                        uow.Save(this.User);
                        id = dbVendor.Id;
                    }
                    else
                    {
                        var vendorResult = this.GetVendorById(vendor.Id);
                        if (vendorResult.HasError)
                            throw vendorResult.Exception;

                        var dbVendor = vendorResult.Result;

                        this.MapEntity<Vendor>(dbVendor, vendor);
                        uow.Save(this.User);
                    }

                    return id;
                }
            });
        }

        public ServiceResponse<List<Vendor>> SearchVendors(String searchTerm)
        {
            var methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
            LogStart(methodName);

            return this.Execute(() =>
            {
                using (var uow = new UnitOfWork())
                {
                    return uow.VendorRepository.All.Where(x => x.Name.Contains(searchTerm)).ToList();
                }
            });
        }
    }
}
