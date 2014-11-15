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
    public class CategoryService : BaseService
    {
        public CategoryService(ILogger logger, String user)
            : base(logger, user)
        {
        }

        public ServiceResponse<Category> GetCategoryById(int id)
        {
            var methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
            LogStart(methodName);

            return this.Execute(() =>
            {
                var ids = new List<int>(new int[] { id });
                var categories = GetCategoriesById(ids);

                if (categories.HasError)
                {
                    throw categories.Exception;
                }

                return categories.Result.FirstOrDefault();
            });
        }

        public ServiceResponse<List<Category>> GetCategoriesById(List<int> ids)
        {
            var methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
            LogStart(methodName);

            return this.Execute(() =>
            {
                using (var uow = new UnitOfWork())
                {
                    return uow.CategoryRepository.All.Where(x => ids.Contains(x.Id)).ToList();
                }
            });
        }

        public ServiceResponse<int> UpdateTree(Category category)
        {
            var methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
            LogStart(methodName);

            return this.Execute(() =>
            {
                using (var uow = new UnitOfWork())
                {
                    int id = 0;
                    if(category.Id == null || category.Id == 0){
                        var dbTree = uow.CategoryRepository.Insert(category);
                        id = dbTree.Id;
                    }
                    else
                    {
                        var categoryResult = this.GetCategoryById(category.Id);
                        if (categoryResult.HasError)
                            throw categoryResult.Exception;

                        var dbCategory = categoryResult.Result;

                        dbCategory.Name = category.Name;

                        uow.Save(this.User);
                    }

                    return id;
                }
            });
        }
    }
}
