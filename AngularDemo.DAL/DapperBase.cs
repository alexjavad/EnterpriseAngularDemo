using NLog;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using Dapper;

namespace AngularDemo.DAL
{
    public abstract class DapperBase : IDisposable
    {
        static string baseConnName = string.Empty;
        static IDbConnection db;
        string procedureName = string.Empty;
        DynamicParameters paramCollection;

        bool disposed = false;
        private static ILogger Log { get; set; }

        #region "IDisposable members"
        ~DapperBase()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public virtual void Dispose(bool disposing)
        {
            if (disposed)
            {
                return;
            }

            if (disposing)
            {
                if (db != null)
                {
                    db.Dispose();
                }
            }
            disposed = true;
        }

        #endregion

        public DapperBase()
        {
            Log = LogManager.GetCurrentClassLogger();
            baseConnName = ConfigurationManager.AppSettings["DALBaseConnectionName"].ToString();
            db = new SqlConnection(ConfigurationManager.ConnectionStrings[baseConnName].ConnectionString);
        }

        protected void ClearParameters()
        {
            paramCollection = new DynamicParameters();
        }

        protected void ConfigureCommand(string procName)
        {
            try
            {
                procedureName = procName;
                paramCollection = new DynamicParameters();
            }
            catch (Exception ex)
            {
                Log.Error(string.Format("ERROR in {0}, Error Message: {1}", System.Reflection.MethodBase.GetCurrentMethod(), ex.Message));
            }
        }

        protected void AddParamWithValue(string paramName, object objValue)
        {
            try
            {
                paramCollection.Add(paramName, objValue);
            }
            catch (Exception ex)
            {
                WriteErrorToLog(ex.Message, "DapperBase.AddParamWithValue", ex);
            }
        }

        protected List<T> GetList<T>()
        {
            List<T> results = new List<T>();
            try
            {
                results = (List<T>)db.Query<T>(procedureName, paramCollection, commandType: CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                WriteErrorToLog(ex.Message, "DapperBase.GetList<T>", ex);
            }
            return results;
        }

        protected T GetSingle<T>()
        {
            //T item = (T)new object();
            try
            {
                return (T)Convert.ChangeType(db.Query<T>(procedureName, paramCollection, commandType: CommandType.StoredProcedure).AsList<T>()[0], typeof(T));
            }
            catch (Exception ex)
            {
                WriteErrorToLog(ex.Message, "DapperBase.GetSingle<T>", ex);
            }
            return default(T);
        }

        protected bool ExecuteNonQueryText()
        {
            try
            {
                db.Execute(procedureName, paramCollection);
                return true;
            }
            catch (Exception ex)
            {
                WriteErrorToLog(ex.Message, "DapperBase.ExecuteNonQuery", ex);
                return false;
            }
        }

        protected bool ExecuteNonQuery()
        {
            try
            {
                db.Execute(procedureName, paramCollection, commandType: CommandType.StoredProcedure);
                return true;
            }
            catch (Exception ex)
            {
                WriteErrorToLog(ex.Message, "DapperBase.ExecuteNonQuery", ex);
                return false;
            }
        }

        protected T ExecuteNonQueryWithOutputParam<T>(string outputParamName)
        {
            try
            {
                db.Query<T>(procedureName, paramCollection, commandType: CommandType.StoredProcedure);
                return (T)Convert.ChangeType(paramCollection.Get<T>(outputParamName), typeof(T));

            }
            catch (Exception ex)
            {
                WriteErrorToLog(ex.Message, "DapperBase.ExecuteNonQueryWithOutputParam<T>", ex);
            }
            return default(T);
        }

        protected void AddOutputParamWithValue(string paramName, object objValue, DbType paramType)
        {
            try
            {
                paramCollection.Add(paramName, objValue, paramType, ParameterDirection.Output);
            }
            catch (Exception ex)
            {
                WriteErrorToLog(ex.Message, "DapperBase.AddOutputParamWithValue", ex);
            }
        }

        protected void WriteErrorToLog(string message, string source, Exception ex)
        {
            Log.Error(string.Format("{0},{1},{2}", message, source, ex.Message));
        }
    }
}
