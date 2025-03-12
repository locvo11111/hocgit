using System;
using System.Collections.Generic;

namespace PhanMemQuanLyThiCong.IRepositories
{
    public interface IUnitOfWork : IDisposable
    {
        //void changePath(string path);

        void Open();

        void Begin();

        void Commit();

        void Rollback();

        List<T> Query<T>(string query, object p = null, bool isSQLFile = true, bool isCache = false);

        T QueryFirst<T>(string query, object p = null, bool isSQLFile = true, bool isCache = false);

        T QuerySingle<T>(string query, object p = null, bool isSQLFile = true, bool isCache = false);

        int Execute(string query, object p = null, bool transection = false, bool isSQLFile = true);

        int AddOrUpdate<T>(string query, List<T> item, bool transection = false, bool isSQLFile = true);

        List<dynamic> QueryMultiple<T1, T2>(string query, object oParams = null, bool isSQLFile = true, bool isCache = false);

        List<dynamic> QueryMultipleSingle<T1, T2>(string query, object oParams = null, bool isSQLFile = true);

        List<dynamic> QueryMultiple<T1, T2, T3>(string query, object oParams = null, bool isSQLFile = true);

        List<dynamic> QueryMultipleSingle<T1, T2, T3>(string query, object oParams = null, bool isSQLFile = true);

        List<dynamic> QueryMultiple<T1, T2, T3, T4>(string query, object oParams = null, bool isSQLFile = true);

        List<dynamic> QueryMultipleSingle<T1, T2, T3, T4>(string query, object oParams = null, bool isSQLFile = true);
    }
}