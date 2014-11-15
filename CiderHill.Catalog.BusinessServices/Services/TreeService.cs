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
    public class TreeService : BaseService
    {
        public TreeService(ILogger logger, String user)
            : base(logger, user)
        {
        }

        public ServiceResponse<List<Tree>> GetAllTrees()
        {
            var methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
            LogStart(methodName);

            return this.Execute(() =>
            {
                using (var uow = new UnitOfWork())
                {
                    return uow.TreeRepository.All.ToList();
                }
            });
        }

        public ServiceResponse<Tree> GetTreeById(int id)
        {
            var methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
            LogStart(methodName);

            return this.Execute(() =>
            {
                var ids = new List<int>(new int[] { id });
                var trees = GetTreesById(ids);

                if(trees.HasError)
                {
                    throw trees.Exception;
                }

                return trees.Result.FirstOrDefault();
            });
        }

        public ServiceResponse<List<Tree>> GetTreesById(List<int> ids)
        {
            var methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
            LogStart(methodName);

            return this.Execute(() =>
            {
                using (var uow = new UnitOfWork())
                {
                    return uow.TreeRepository.All.Where(x => ids.Contains(x.Id)).ToList();
                }
            });
        }

        public ServiceResponse<int> UpdateTree(Tree tree)
        {
            var methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
            LogStart(methodName);

            return this.Execute(() =>
            {
                using (var uow = new UnitOfWork())
                {
                    int id = 0;
                    if(tree.Id == null || tree.Id == 0){
                        var dbTree = uow.TreeRepository.Insert(tree);
                        id = dbTree.Id;
                    }
                    else
                    {
                        var treeResult = this.GetTreeById(tree.Id);
                        if (treeResult.HasError)
                            throw treeResult.Exception;

                        var dbTree = treeResult.Result;
                        this.MapEntity<Tree>(dbTree, tree);
                        /*
                        dbTree.CategoryId = tree.CategoryId;
                        dbTree.Description = tree.Description;
                        dbTree.Harvest = tree.Harvest;
                        dbTree.Name = tree.Name;
                        dbTree.ProductNumber = tree.ProductNumber;
                        dbTree.TreeNotes = tree.TreeNotes;
                        dbTree.VendoryId = tree.VendoryId;
                        dbTree.Zone = tree.Zone;
                        */
                        uow.Save(this.User);
                    }

                    return id;
                }
            });
        }
    }
}
