using CiderHill.Catalog.BusinessServices.Services;
using CiderHill.Catalog.Utilities.Logging;
using System;
using System.Reflection;

namespace CiderHill.Catalog.BusinessServices
{
    public class BaseService
    {
        public ILogger Logger { get; set; }
        public String User { get; set; }

        internal BaseService(ILogger logger, String user)
        {
            Logger = logger;
            User = user;
        }

        protected ServiceResponse<T> Execute<T>(Func<T> func)
        {
            var response = new ServiceResponse<T>();
            try
            {
                response.Result = func.Invoke();
            }
            catch (Exception ex)
            {
                // Don't log service validation exceptions!
                if (!(ex is ServiceValidationException))
                    this.Logger.Error(ex);

                response.Result = default(T);
                response.Exception = ex;
            }

            return response;
        }

        protected void LogStart(string methodName)
        {
            Logger.Info("Started " + this.GetType().Name + " - " + methodName);
        }

        protected void LogFinish(string methodName)
        {
            Logger.Info("Finished " + this.GetType().Name + " - " + methodName);
        }

        /// <summary>
        /// Map to dbObject from disconnectedObject
        /// </summary>
        /// <typeparam name="T">Entity Type</typeparam>
        /// <param name="dbObject">To/DB Entity</param>
        /// <param name="disconnectObject">From/Disconnected Entity</param>
        /// <returns>To/DB Entity</returns>
        public T MapEntity<T>(T dbObject, T disconnectObject)
        {
            FieldInfo[] fis = disconnectObject.GetType().GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

            foreach (FieldInfo fi in fis)
            {
                if (fi.FieldType.Namespace != disconnectObject.GetType().Namespace)
                {
                    Logger.Info(fi.Name);
                    fi.SetValue(dbObject, fi.GetValue(disconnectObject));
                }
            }

            return dbObject;
        }
    }
}
