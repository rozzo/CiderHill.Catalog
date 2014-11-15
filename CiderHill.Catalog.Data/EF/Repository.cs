using log4net;
using System;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;

namespace CiderHill.Catalog.Data.EF
{
    public class Repository<T>
        where T : AbstractEntity
    {
        internal CatalogContext Context;
        internal IDbSet<T> DbSet;
        private ILog _log;

        public Repository(CatalogContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }
            Context = context;
            DbSet = context.Set<T>();
        }

        #region Dependencies

        public ILog Log
        {
            get
            {
                return _log ?? (_log = LogManager.GetLogger(""));
            }
            set
            {
                _log = value;
            }
        }

        #endregion

        #region Implementation of IRepository<T>

        public virtual IQueryable<T> All
        {
            get
            {
                return DbSet;
            }
        }

        public virtual IQueryable<T> AllIncluding(params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = DbSet;
            if (includeProperties != null)
            {
                foreach (var includeProperty in includeProperties)
                {
                    query = query.Include(includeProperty);
                }
            }
            return query;
        }

        public virtual int Count(Expression<Func<T, bool>> predicate)
        {
            try
            {
                return DbSet.Count(predicate);
            }
            catch (Exception ex)
            {
                string message = string.Format(CultureInfo.CurrentCulture, "Error getting count for {0}", predicate);
                Log.Error(message, ex);
                throw new RepositoryException(message, ex);
            }
        }

        public virtual void Delete(T entity)
        {
            try
            {
                DbSet.Remove(entity);
            }
            catch (Exception ex)
            {
                string message = string.Format(CultureInfo.CurrentCulture, "Error deleting type {0}", typeof(T).Name);
                Log.Error(message, ex);
                throw new RepositoryException(message, ex);
            }
        }

        public virtual T Find(params object[] key)
        {
            try
            {
                return DbSet.Find(key);
            }
            catch (Exception ex)
            {
                string message = string.Format(CultureInfo.CurrentCulture, "Error getting type {0} with key {1}", typeof(T).Name, key);
                Log.Error(message, ex);
                throw new RepositoryException(message, ex);
            }
        }

        public virtual T Insert(T entity)
        {
            try
            {
                return DbSet.Add(entity);
            }
            catch (Exception ex)
            {
                string message = string.Format(CultureInfo.CurrentCulture, "Error inserting entity of type {0}", typeof(T).Name);
                Log.Error(message, ex);
                throw new RepositoryException(message, ex);
            }
        }
        #endregion
    }
}