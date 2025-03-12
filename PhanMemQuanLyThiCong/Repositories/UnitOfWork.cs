using Dapper;
using Newtonsoft.Json;
using PhanMemQuanLyThiCong.Common.Constant;
using PhanMemQuanLyThiCong.Common.Helper;
using PhanMemQuanLyThiCong.Common.MLogging;
using PhanMemQuanLyThiCong.Common.Security;
using PhanMemQuanLyThiCong.IRepositories;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Reflection;

namespace PhanMemQuanLyThiCong.Repositories
{
    public class UnitOfWork : SqLiteBaseRepository, IUnitOfWork
    {
        private SQLiteConnection _conn = null;
        private SQLiteCommand cmd;
        private SQLiteDataReader sdr;
        private string sqlFile;
        private string sqlQuery;
        private IDbTransaction _transaction;
        //private string _pathDB;
        DataProvider dtp = DataProvider.InstanceTHDA;

        public UnitOfWork(DataProvider dtp)
        {
            //_pathDB = dtp.m_conString;
            //_conn = GetConnection(dtp);
        }

        private void OpenConnection()
        {
            TongHopHelper.CheckTimeSync();

            if (_conn is null || _conn.State == System.Data.ConnectionState.Closed)
                _conn = GetConnection(dtp);
        }

        public int AddOrUpdate<T>(string query, List<T> item, bool transection = false, bool isSQLFile = true)
        {
            OpenConnection();
            if (transection)
                Begin();
            int result = -1;
            try
            {
                if (isSQLFile) query = SqlResourceManager.GetInstance().GetSql(query);
                result = _conn.Execute(query, item);
                if (transection)
                    Commit();
                //Logging.Info(string.Format(CommonConstants.LOG_SQL, query, JsonConvert.SerializeObject(item)), MethodBase.GetCurrentMethod().DeclaringType.ToString());
                _conn.Close();
                return result;
            }
            catch (Exception ex)
            {
                string err = string.Format(CommonConstants.LOG_SQL, query, JsonConvert.SerializeObject(item));
                Logging.Error(err, ex, MethodBase.GetCurrentMethod().DeclaringType.ToString());
                AlertShower.ShowInfo(err);
                Rollback();
                _conn.Close();
                return result;
            }
        }

        public void Begin()
        {
            if (_transaction == null)
                _transaction = _conn.BeginTransaction();
        }

        public void Commit()
        {
            if (_transaction != null)
                _transaction.Commit();
            Dispose();
        }

        public void Dispose()
        {
            if (_transaction != null)
                _transaction.Dispose();
            _transaction = null;
        }

        public void Rollback()
        {
            if (_transaction != null)
                _transaction.Rollback();
            Dispose();
        }

        public int Execute(string query, object p = null, bool transection = false, bool isSQLFile = true)
        {
            OpenConnection();
            if (transection)
                Begin();
            int result = -1;
            try
            {
                if (isSQLFile) query = SqlResourceManager.GetInstance().GetSql(query);
                if (transection)
                    result = p != null ? _conn.Execute(query, p, transaction: _transaction, commandType: CommandType.Text) : _conn.Execute(query, transaction: _transaction, commandType: CommandType.Text);
                else
                    result = p != null ? _conn.Execute(query, p, commandType: CommandType.Text) : _conn.Execute(query, commandType: CommandType.Text);
                if (transection)
                    Commit();
                //Logging.Info(string.Format(CommonConstants.LOG_SQL, query, p == null ? "" : JsonConvert.SerializeObject(p)), MethodBase.GetCurrentMethod().DeclaringType.ToString());
                _conn.Close();
                return result;
            }
            catch (Exception ex)
            {
                Logging.Error(string.Format(CommonConstants.LOG_SQL, query, p == null ? "" : JsonConvert.SerializeObject(p)), ex, MethodBase.GetCurrentMethod().DeclaringType.ToString());
                Rollback();
                _conn.Close();
                return result;
            }
            finally
            {
                _conn.Close();
            }
        }

        public List<T> Query<T>(string query, object p = null, bool isSQLFile = true, bool isCache = true)
        {
            OpenConnection();
            try
            {
                var key = query + CacheHelper.ConvertObjectToString(p);
                var hashKey = Md5Util.Md5EnCrypt(key);
                if (isSQLFile) query = SqlResourceManager.GetInstance().GetSql(query);
                List<T> items = null;
                if (isCache && MemoryCacheHelper.CheckExistKey(hashKey)) items = MemoryCacheHelper.GetValue(hashKey) as List<T>;
                else
                {
                    var result = p != null ? _conn.Query<T>(query, p) : _conn.Query<T>(query, null);
                    if (result.Any())
                        items = result.ToList();
                    else
                        items = new List<T>();
                    //Logging.Info(string.Format(CommonConstants.LOG_SQL, query, p == null ? string.Empty : JsonConvert.SerializeObject(p)), MethodBase.GetCurrentMethod().DeclaringType.ToString());
                }
                if (isCache && !MemoryCacheHelper.CheckExistKey(hashKey) && items.Any())
                {
                    MemoryCacheHelper.Add(hashKey, items, DateTimeOffset.UtcNow.AddHours(1));
                }
                _conn.Close();
                return items;
            }
            catch (Exception ex)
            {
                Logging.Error(string.Format(CommonConstants.LOG_SQL, query, p == null ? "" : JsonConvert.SerializeObject(p)), ex, MethodBase.GetCurrentMethod().DeclaringType.ToString());
                _conn.Close();
                return new List<T>();
            }
            finally
            {
                _conn.Close();
            }
        }

        public T QueryFirst<T>(string query, object p = null, bool isSQLFile = true, bool isCache = true)
        {
            OpenConnection();
            var key = query + CacheHelper.ConvertObjectToString(p);
            var hashKey = Md5Util.Md5EnCrypt(key);
            T item = default(T);
            if (isSQLFile) query = SqlResourceManager.GetInstance().GetSql(query);
            if (isCache && MemoryCacheHelper.CheckExistKey(hashKey)) item = (T)MemoryCacheHelper.GetValue(hashKey);
            else
            {
                if (p != null) item = _conn.Query<T>(query, p).FirstOrDefault();
                else item = _conn.Query<T>(query, null).FirstOrDefault();
            }
            if (isCache && !MemoryCacheHelper.CheckExistKey(hashKey))
            {
                MemoryCacheHelper.Add(hashKey, item, DateTimeOffset.UtcNow.AddHours(1));
            }
            _conn.Close();
            return item;
        }

        public T QuerySingle<T>(string query, object p = null, bool isSQLFile = true, bool isCache = false)
        {
            OpenConnection();
            var key = query + CacheHelper.ConvertObjectToString(p);
            var hashKey = Md5Util.Md5EnCrypt(key);
            if (isSQLFile) query = SqlResourceManager.GetInstance().GetSql(query);
            T item = default(T);
            if (isCache && MemoryCacheHelper.CheckExistKey(hashKey)) item = (T)MemoryCacheHelper.GetValue(hashKey);
            else
            {
                try
                {
                    if (p != null) item = _conn.Query<T>(query, p).FirstOrDefault();
                    else item = _conn.Query<T>(query, null).SingleOrDefault();
                }
                catch (Exception ex)
                {
                    Logging.Error(string.Format(CommonConstants.LOG_SQL, query, p == null ? "" : JsonConvert.SerializeObject(p)), ex, MethodBase.GetCurrentMethod().DeclaringType.ToString());
                    _conn.Close();
                    return default(T);
                }
            }
            if (isCache && !MemoryCacheHelper.CheckExistKey(hashKey))
            {
                MemoryCacheHelper.Add(hashKey, item, DateTimeOffset.UtcNow.AddHours(1));
            }
            _conn.Close();
            return item;
        }

        public List<dynamic> QueryMultiple<T1, T2>(string query, object p = null, bool isSQLFile = true, bool isCache = true)
        {
            if (isSQLFile) query = SqlResourceManager.GetInstance().GetSql(query);
            OpenConnection();
            var key = query + CacheHelper.ConvertObjectToString(p);
            var hashKey = Md5Util.Md5EnCrypt(key);
            List<dynamic> items = null;
            if (isCache && MemoryCacheHelper.CheckExistKey(hashKey)) items = MemoryCacheHelper.GetValue(hashKey) as List<dynamic>;
            else
            {
                var queryItems = p != null ? _conn.QueryMultiple(query, p) : _conn.QueryMultiple(query, null);
                items = new List<dynamic>();
                try
                {
                    items.Add(queryItems.Read<T1>());
                    items.Add(queryItems.Read<T2>());
                    //Logging.Info(string.Format(CommonConstants.LOG_SQL, query, p == null ? string.Empty : JsonConvert.SerializeObject(p)), MethodBase.GetCurrentMethod().DeclaringType.ToString());
                }
                catch (Exception ex)
                {
                    Logging.Error(string.Format(CommonConstants.LOG_SQL, query, p == null ? "" : JsonConvert.SerializeObject(p)), ex, MethodBase.GetCurrentMethod().DeclaringType.ToString());
                }
            }
            if (isCache && !MemoryCacheHelper.CheckExistKey(hashKey))
            {
                MemoryCacheHelper.Add(hashKey, items, DateTimeOffset.UtcNow.AddHours(1));
            }
            _conn.Close();
            return items;
        }

        public List<dynamic> QueryMultipleSingle<T1, T2>(string query, object p = null, bool isSQLFile = true)
        {
            var key = query + CacheHelper.ConvertObjectToString(p);
            var hashKey = Md5Util.Md5EnCrypt(key);
            if (isSQLFile) query = SqlResourceManager.GetInstance().GetSql(query);
            OpenConnection();
            List<dynamic> items = null;
            if (MemoryCacheHelper.CheckExistKey(hashKey)) items = MemoryCacheHelper.GetValue(hashKey) as List<dynamic>;
            else
            {
                var queryItems = p != null ? _conn.QueryMultiple(query, p) : _conn.QueryMultiple(query, null);
                items = new List<dynamic>();
                try
                {
                    if (queryItems != null)
                    {
                        items.Add(queryItems.Read<T1>().FirstOrDefault());
                        items.Add(queryItems.Read<T2>().FirstOrDefault());
                        //Logging.Info(string.Format(CommonConstants.LOG_SQL, query, p == null ? string.Empty : JsonConvert.SerializeObject(p)), MethodBase.GetCurrentMethod().DeclaringType.ToString());
                    }
                }
                catch (Exception ex)
                {
                    Logging.Error(string.Format(CommonConstants.LOG_SQL, query, p == null ? "" : JsonConvert.SerializeObject(p)), ex, MethodBase.GetCurrentMethod().DeclaringType.ToString());
                }
            }
            if (!MemoryCacheHelper.CheckExistKey(hashKey))
            {
                MemoryCacheHelper.Add(hashKey, items, DateTimeOffset.UtcNow.AddHours(1));
            }
            _conn.Close();
            return items;
        }

        public List<dynamic> QueryMultipleSingle<T1, T2, T3>(string query, object oParams = null, bool isSQLFile = true)
        {
            if (isSQLFile) query = SqlResourceManager.GetInstance().GetSql(query);
            OpenConnection();
            List<dynamic> items = null;
            var queryItems = oParams != null ? _conn.QueryMultiple(query, oParams) : _conn.QueryMultiple(query, null);
            items = new List<dynamic>();
            try
            {
                items.Add(queryItems.Read<T1>().FirstOrDefault());
                items.Add(queryItems.Read<T2>().FirstOrDefault());
                items.Add(queryItems.Read<T3>().FirstOrDefault());
                //Logging.Info(string.Format(CommonConstants.LOG_SQL, query, oParams == null ? string.Empty : JsonConvert.SerializeObject(oParams)), MethodBase.GetCurrentMethod().DeclaringType.ToString());
            }
            catch (Exception ex)
            {
                Logging.Error(string.Format(CommonConstants.LOG_SQL, query, oParams == null ? "" : JsonConvert.SerializeObject(oParams)), ex, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            }
            _conn.Close();
            return items;
        }

        public List<dynamic> QueryMultiple<T1, T2, T3>(string query, object oParams = null, bool isSQLFile = true)
        {
            if (isSQLFile) query = SqlResourceManager.GetInstance().GetSql(query);
            OpenConnection();
            List<dynamic> items = null;
            var queryItems = oParams != null ? _conn.QueryMultiple(query, oParams) : _conn.QueryMultiple(query, null);
            items = new List<dynamic>();
            try
            {
                items.Add(queryItems.Read<T1>());
                items.Add(queryItems.Read<T2>());
                items.Add(queryItems.Read<T3>());
                //Logging.Info(string.Format(CommonConstants.LOG_SQL, query, oParams == null ? string.Empty : JsonConvert.SerializeObject(oParams)), MethodBase.GetCurrentMethod().DeclaringType.ToString());
            }
            catch (Exception ex)
            {
                Logging.Error(string.Format(CommonConstants.LOG_SQL, query, oParams == null ? "" : JsonConvert.SerializeObject(oParams)), ex, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            }
            _conn.Close();
            return items;
        }

        public List<dynamic> QueryMultiple<T1, T2, T3, T4>(string query, object oParams = null, bool isSQLFile = true)
        {
            if (isSQLFile) query = SqlResourceManager.GetInstance().GetSql(query);
            OpenConnection();
            List<dynamic> items = null;
            var queryItems = oParams != null ? _conn.QueryMultiple(query, oParams) : _conn.QueryMultiple(query, null);
            items = new List<dynamic>();
            try
            {
                items.Add(queryItems.Read<T1>());
                items.Add(queryItems.Read<T2>());
                items.Add(queryItems.Read<T3>());
                items.Add(queryItems.Read<T4>());
                //Logging.Info(string.Format(CommonConstants.LOG_SQL, query, oParams == null ? string.Empty : JsonConvert.SerializeObject(oParams)), MethodBase.GetCurrentMethod().DeclaringType.ToString());
            }
            catch (Exception ex)
            {
                Logging.Error(string.Format(CommonConstants.LOG_SQL, query, oParams == null ? "" : JsonConvert.SerializeObject(oParams)), ex, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            }
            _conn.Close();
            return items;
        }

        public List<dynamic> QueryMultipleSingle<T1, T2, T3, T4>(string query, object oParams = null, bool isSQLFile = true)
        {
            if (isSQLFile) query = SqlResourceManager.GetInstance().GetSql(query);
            OpenConnection();
            List<dynamic> items = null;
            var queryItems = oParams != null ? _conn.QueryMultiple(query, oParams) : _conn.QueryMultiple(query, null);
            items = new List<dynamic>();
            try
            {
                items.Add(queryItems.Read<T1>().FirstOrDefault());
                items.Add(queryItems.Read<T2>().FirstOrDefault());
                items.Add(queryItems.Read<T3>().FirstOrDefault());
                items.Add(queryItems.Read<T4>().FirstOrDefault());
                //Logging.Info(string.Format(CommonConstants.LOG_SQL, query, oParams == null ? string.Empty : JsonConvert.SerializeObject(oParams)), MethodBase.GetCurrentMethod().DeclaringType.ToString());
            }
            catch (Exception ex)
            {
                Logging.Error(string.Format(CommonConstants.LOG_SQL, query, oParams == null ? "" : JsonConvert.SerializeObject(oParams)), ex, MethodBase.GetCurrentMethod().DeclaringType.ToString());
            }
            _conn.Close();
            return items;
        }

        public void Open()
        {
            OpenConnection();
        }

        //public void changePath(string paths)
        //{
        //    _pathDB = paths;
        //    _conn = GetConnection(paths);
        //}

        public void closePath()
        {
            _conn.Close();
        }
    }
}