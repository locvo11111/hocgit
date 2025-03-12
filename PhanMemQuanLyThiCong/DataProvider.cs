using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
//using System.Windows.Forms;
using System.Data.SQLite;
using System.IO;
using System.Diagnostics;
using System.Data.Common;
//using DevExpress.DataAccess.DataFederation;
using System.Text.RegularExpressions;
using PhanMemQuanLyThiCong.Common.Helper;
using PhanMemQuanLyThiCong;
using DevExpress.XtraRichEdit.Fields;
using System.Xml;
using PhanMemQuanLyThiCong.Common.Constant;
using MSETTING = PhanMemQuanLyThiCong.Properties.Settings;
using StackExchange.Profiling.Internal;
using DevExpress.XtraRichEdit.Mouse;
using Dapper;
using Newtonsoft.Json;
using PhanMemQuanLyThiCong.Common.MLogging;
using PhanMemQuanLyThiCong.Repositories;
using System.Reflection;
using DevExpress.Xpo.Helpers;
using DevExpress.XtraCharts.Designer.Native;

//using PhanMemQuanLyThiCong.Common.Helper;
//using PhanMemQuanLyThiCong;
//using PM360.Common.Helper;
//using DevExpress.XtraRichEdit.Fields;

public class DataProvider
{
    private static DataProvider instanceTHDA, instanceTBT, instanceServer, instanceBaoCao, instanceTemp;
    //SQLiteConnection m_con = new SQLiteConnection();
    public string m_NameDb = "";
    public string m_conString = "";
    public bool isEncrypted = true;

    const string TBTkey = "TBT@@1!360";
    //string TBTlic = "13453-563-7092387-29456";

    static string sqlKEY = $"PRAGMA key='{TBTkey}';";
    string sqlREKEY = "PRAGMA rekey='{0}';";

    string sqlLIC = "PRAGMA lic='13453-563-7092387-29456';";

    string patternParam = @"(@\w+)(,|\)|\s|;)";

    string queryTable = "SELECT name FROM sqlite_schema WHERE type = 'table' ORDER BY name";
    public static DataProvider InstanceTHDA
    {
        get { if (instanceTHDA == null) instanceTHDA = new DataProvider(); return DataProvider.instanceTHDA; }
        private set { DataProvider.instanceTHDA = value; }
    }

    public static DataProvider InstanceTBT
    {
        get { if (instanceTBT == null) instanceTBT = new DataProvider(); return DataProvider.instanceTBT; }
        private set { DataProvider.instanceTBT = value; }
    }

    public static DataProvider InstanceServer
    {
        get { if (instanceServer == null) instanceServer = new DataProvider(); return DataProvider.instanceServer; }
        private set { DataProvider.instanceServer = value; }
    }
    public static DataProvider InstanceBaoCao
    {
        get { if (instanceBaoCao == null) instanceBaoCao = new DataProvider(); return DataProvider.instanceBaoCao; }
        private set { DataProvider.instanceBaoCao = value; }
    }

    public static DataProvider InstanceTemp
    {
        get { if (instanceTemp == null) instanceTemp = new DataProvider(); return DataProvider.instanceTemp; }
        private set { DataProvider.instanceTemp = value; }
    }

    public DataProvider()
    {
        m_conString = $"Data Source={m_NameDb};Version=3;foreign keys=true";
        //m_con.ConnectionString = m_conString;

        if (!File.Exists(m_NameDb))
        {
            Debug.WriteLine("Database not exist!");
        }
        else
        {
            Debug.WriteLine("Database existed!");
        }
    }

    public bool Encrypt()
    {
        try
        {
            ExecuteQuery(queryTable, false);
        }
        catch
        {
            return false;
        }

        using (SQLiteConnection db = new SQLiteConnection(m_conString))
        {
            db.Open();
            SQLiteCommand command = new SQLiteCommand(sqlLIC, db);
            command.ExecuteNonQuery();

            command = new SQLiteCommand(string.Format(sqlREKEY, TBTkey), db);
            command.ExecuteNonQuery();
            command.Dispose();
            db.Close();
        }

        try
        {
            ExecuteQuery(queryTable, true);
        }
        catch
        {
            return false;
        }

        return true;
    }
    public bool Decrypt()
    {
        try
        {
            ExecuteQuery(queryTable, true);
        }
        catch
        {
            return false;
        }


        using (SQLiteConnection db = new SQLiteConnection(m_conString))
        {
            db.Open();
            SQLiteCommand command = new SQLiteCommand(sqlLIC, db);
            command.ExecuteNonQuery();

            command = new SQLiteCommand(sqlKEY, db);
            command.ExecuteNonQuery();

            command = new SQLiteCommand(string.Format(sqlREKEY, ""), db);
            command.ExecuteNonQuery();
            command.Dispose();
            db.Close();
        }

        try
        {
            ExecuteQuery(queryTable, false);
        }
        catch
        {
            return false;
        }

        return true;
    }

    public void changePath(string path, bool Encrypt = true)
    {
        SqlMapper.AddTypeHandler(new NullableDateTimeHandler());
        SqlMapper.AddTypeHandler(new DoubleHandler());
        SqlMapper.AddTypeHandler(new GuidHandler());

        m_NameDb = path;
        m_conString = $"Data Source={path};Version=3;foreign keys=true";
        //m_con.ConnectionString = m_conString;

        isEncrypted = Encrypt;
        //if (Encrypt)
        //{
        //    SQLiteConnection db = new SQLiteConnection(m_conString);
        //    //SQLiteConnection db = new SQLiteConnection("Data Source=DatabaseTBT.sqlite;Version=3;");

        //    db.Open();

        //    SQLiteCommand command = new SQLiteCommand(sqlLIC, db);
        //    command.ExecuteNonQuery();


        //    command = new SQLiteCommand(sqlREKEY, db);
        //    command.ExecuteNonQuery();
        //    db.Close();
        //    db.Dispose();
        //}    

        if (!File.Exists(m_NameDb))
        {
            Debug.WriteLine("Database not exist!");
        }
        else
        {
            Debug.WriteLine("Database existed!");
        }
    }


    public int CheckTableIsExsit(string table_name, string database_name)
    {
        string result = ExecuteScalar($"IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = N'{table_name}') BEGIN select 1 as dem END else begin select 0 as dem end").ToString();
        return int.Parse(result);
    }

    /// <summary>
    /// Connect to the database
    /// </summary>
    /// <returns></returns>
    public DbConnection GetDbConnection()
    {
        string Path = AppDomain.CurrentDomain.BaseDirectory + @"\sysdata.db";
        SQLiteConnection connection = new SQLiteConnection($"Data Source={Path}");
        return connection;
    }
    public int ExecuteNonQuery(string query, bool withEncrypt = true, bool isSQLFile = false, object[] parameter = null, 
        bool AddToDeletedDataTable = true, List<KeyValuePair<string, string>> Conditions = null)
    {
        TongHopHelper.CheckTimeSync();
        if (isSQLFile) query = SqlResourceManager.GetInstance().GetSql(query);

        if (Conditions != null )
        {
            foreach(var item in  Conditions)
            query = query.Replace(item.Key, item.Value);
        }

        query = query.Trim() + " ";
        string pattern = $@"('-*\d+)(,+)(\d+')";
        //Match m = Regex.Match(query, pattern, RegexOptions.IgnoreCase); 

        query = Regex.Replace(query, pattern, "$1.$3");

        int data = 0;
        Debug.WriteLine("DB Non query: " + query);


        DataTable dtDeleted = null;
        if (query.StartsWith("DELETE FROM") && AddToDeletedDataTable && m_conString == instanceTHDA.m_conString)
        {
            string patternTbl = @"(\s+)(Tbl_([a-z]|[A-Z]|_)+)(\s+)";
            Match mTbl = Regex.Match(query, patternTbl, RegexOptions.IgnoreCase);


            if (mTbl != Match.Empty)
            {


                string tbl = mTbl.Groups[2].Value;
                if (tbl == Server.Tbl_ThongTinDuAn || tbl == Server.Tbl_DeletedRecored)
                    goto BEGINEXECUTE;

                string colCode = (tbl == "Tbl_GiaoViec_CongViecCha")
                            ? "CodeCongViecCha"
                            : (tbl == "Tbl_GiaoViec_CongViecCon")
                            ? "CodeCongViecCon" : "Code";


                DataTable dt = ExecuteQuery(query.Replace("DELETE", $"SELECT {colCode}"));
                var codesDeleted = dt.AsEnumerable().Select(x => (string)x[0]);

                if (codesDeleted.Any())
                {
                    string dbString = $"SELECT * FROM {MyConstant.TBL_DeletedRecored} WHERE Code IN ({MyFunction.fcn_Array2listQueryCondition(codesDeleted)}) ";
                    dtDeleted = DataProvider.instanceTHDA.ExecuteQuery(dbString);

                    foreach (string codeDeleted in codesDeleted)
                    {
                        DataRow dr = dtDeleted.AsEnumerable().SingleOrDefault(x => (string)x["Code"] == codeDeleted);
                        if (dr is null)
                        {
                            dr = dtDeleted.NewRow();
                            dtDeleted.Rows.Add(dr);
                            dr["Code"] = codeDeleted;
                            dr["TableName"] = tbl;
                            dr["CodeDuAn"] = SharedControls.slke_ThongTinDuAn.EditValue as string;
                        }

                    }
                    UpdateDataTableFromSqliteSource(dtDeleted, Server.Tbl_DeletedRecored);
                }

            }
        }


        BEGINEXECUTE:
        using (SQLiteConnection connection = new SQLiteConnection(m_conString))
        {
            connection.Open();

            if (isEncrypted && withEncrypt)
            {
                SQLiteCommand command = new SQLiteCommand(sqlKEY, connection);
                command.ExecuteNonQuery();
                command.Dispose();

                command = new SQLiteCommand(sqlLIC, connection);
                command.ExecuteNonQuery();
                command.Dispose();
            }

            using (SQLiteTransaction transaction = connection.BeginTransaction())
            {

                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    if (parameter != null)
                    {
                        Match result = Regex.Match(query, patternParam, RegexOptions.IgnoreCase);
                        List<string> listPara = new List<string>();

                        if (result != Match.Empty)
                        {
                            do
                            {
                                //Console.WriteLine(result.ToString());
                                
                                listPara.Add(result.Groups[1].Value);
                                result = result.NextMatch(); // Chuyển qua kết quả trùng khớp kế tiếp
                            }
                            while (result != Match.Empty); // Kiểm tra xem đã hết kết quả trùng khớp chưa
                        }
                        int i = 0;
                        foreach (string item in listPara)
                        {
                            if (item.Contains("@"))
                            {
                                command.Parameters.AddWithValue(item, parameter[i]);
                                i++;
                            }
                        }
                    }

                    data = command.ExecuteNonQuery();
                }
                transaction.Commit();
            }
            connection.Close();
            
        }
        if (dtDeleted != null)
        {
            UpdateDataTableFromSqliteSource(dtDeleted, MyConstant.TBL_DeletedRecored);
        }
        return data;
    }

    public int ExecuteNonQueryFromList(List<string> queries, bool withEncrypt = true, object[] parameter = null, bool AddToDeletedDataTable = true)
    {
        TongHopHelper.CheckTimeSync();

        int data = 0;

        for (int i = 0; i < queries.Count; i++)
        {
            queries[i] = Regex.Replace(queries[i], patternParam, $"$1z{i}$2");
        }


        DataTable dtDeleted = null;

        BEGINEXECUTE:
        using (SQLiteConnection connection = new SQLiteConnection(m_conString))
        {
            connection.Open();

            if (isEncrypted && withEncrypt)
            {
                SQLiteCommand command = new SQLiteCommand(sqlKEY, connection);
                command.ExecuteNonQuery();
                command.Dispose();

                command = new SQLiteCommand(sqlLIC, connection);
                command.ExecuteNonQuery();
                command.Dispose();
            }


            using (SQLiteCommand command = new SQLiteCommand(connection))
            {
                int size = 1000;
                int ParamsEachCommand = (parameter is null) ? 0 : parameter.Length / queries.Count;
                for (int i = 0; i < queries.Count / (double)size; i++)
                {
                    //using (var trans = connection.BeginTransaction())
                    //{
                        //foreach (var query in queries.Skip(i*size).Take(size))
                        //{
                            if (parameter is null)
                                ExecuteNonQuery(string.Join(";\r\n", queries.Skip(i * size).Take(size)));
                            else
                            {
                                ExecuteNonQuery(string.Join(";\r\n", queries.Skip(i * size).Take(size)), withEncrypt, false,
                                    parameter.Skip(i * size*ParamsEachCommand).Take(size* ParamsEachCommand).ToArray());
                            }

                            //string pattern = $@"('-*\d+)(,+)(\d+')";

                            //string queryStr = Regex.Replace(query, pattern, "$1.$3");
                            //Debug.WriteLine("DB Non querys: " + queryStr);
                            //command.CommandText = queryStr;

                            //data += command.ExecuteNonQuery();
                        //}
                        //trans.Commit();
                    //}
                }

            }
            connection.Close();

        }
        if (dtDeleted != null)
        {
            UpdateDataTableFromSqliteSource(dtDeleted, MyConstant.TBL_DeletedRecored);
        }
        return data;
    }
    public DataTable ExecuteQuery(string query, bool withEncrypt = true, bool isSQLFile = false, object[] parameter = null)
    {
        TongHopHelper.CheckTimeSync();
        if (isSQLFile) query = SqlResourceManager.GetInstance().GetSql(query);

        Debug.WriteLine("DB query: " + query);
        DataTable data = new DataTable();
        query = query.Trim() + " ";
        using (SQLiteConnection connection = new SQLiteConnection(m_conString))
        {
            connection.Open();

            if (isEncrypted && withEncrypt)
            {
                SQLiteCommand command = new SQLiteCommand(sqlKEY, connection);
                command.ExecuteNonQuery();
                command.Dispose();

                command = new SQLiteCommand(sqlLIC, connection);
                command.ExecuteNonQuery();
                command.Dispose();

            }

            using (SQLiteCommand command = new SQLiteCommand(query, connection))
            {
                if (parameter != null)
                {


                    Match result = Regex.Match(query, patternParam, RegexOptions.IgnoreCase);
                    List<string> listPara = new List<string>();

                    if (result != Match.Empty)
                    {
                        do
                        {
                            //Console.WriteLine(result.ToString());
                            listPara.Add(result.Groups[1].Value);
                            result = result.NextMatch(); // Chuyển qua kết quả trùng khớp kế tiếp
                        }
                        while (result != Match.Empty); // Kiểm tra xem đã hết kết quả trùng khớp chưa
                    }
                    int i = 0;
                    foreach (string item in listPara)
                    {
                        if (item.Contains("@"))
                        {
                            //var type = parameter[i].GetType();
                            ////Type t = (type.GenericTypeArguments.Any()) ? type.GenericTypeArguments[0] : type;

                            //if (type != null && (type == typeof(DateTime) || type == typeof(DateTime?)))
                            //{
                            //    parameter[i] = ((DateTime)parameter[i]).ToString(MyConstant.CONST_DATE_FORMAT_SQLITE);
                            //}
                            command.Parameters.AddWithValue(item, parameter[i]);
                            i++;
                        }
                    }
                }

                SQLiteDataAdapter adapter = new SQLiteDataAdapter(command);

                adapter.Fill(data);
                //command.Dispose();
                //connection.Dispose();
            }
            connection.Close();
        }

        return data;
    }

    public List<T> ExecuteQueryModel<T>(string query, bool withEncrypt = true, object[] parameter = null, bool isSQLFile = false,
        bool AddToDeletedDataTable = true, List<KeyValuePair<string, string>> Conditions = null)
    {
        TongHopHelper.CheckTimeSync();

        if (isSQLFile) query = SqlResourceManager.GetInstance().GetSql(query);

        if (Conditions != null)
        {
            foreach (var item in Conditions)
                query = query.Replace(item.Key, item.Value);
        }
        Debug.WriteLine("DB query: " + query);
        List<T> data = new List<T>();

        using (SQLiteConnection connection = new SQLiteConnection(m_conString))
        {

            connection.Open();

            if (isEncrypted && withEncrypt)
            {
                SQLiteCommand command = new SQLiteCommand(sqlKEY, connection);
                command.ExecuteNonQuery();
                command.Dispose();

                command = new SQLiteCommand(sqlLIC, connection);
                command.ExecuteNonQuery();
                command.Dispose();

            }

            var result = connection.Query<T>(query, null);
            if (result.Any())
                data = result.ToList();
            else
                data = new List<T>();

            connection.Close();


        }

        return data;
        //return dt.fcn_DataTable2List<T>();
    }

    /*public DataSet ExecuteQuery_Dataset(string query, bool withEncrypt = true, object[] parameter = null)
    {

        DataSet data = new DataSet();

        using (SQLiteConnection connection = new SQLiteConnection(m_conString))
        {
            connection.Open();

            if (isEncrypted && withEncrypt)
            {
                SQLiteCommand command = new SQLiteCommand(sqlKEY, connection);
                command.ExecuteNonQuery();
                command.Dispose();

                command = new SQLiteCommand(sqlLIC, connection);
                command.ExecuteNonQuery();
                command.Dispose();
            }

            using (SQLiteCommand command = new SQLiteCommand(query, connection))
            {
                if (parameter != null)
                {
                    string[] listPara = query.Split(' ');
                    int i = 0;
                    foreach (string item in listPara)
                    {
                        if (item.Contains("@"))
                        {
                            command.Parameters.AddWithValue(item, parameter[i]);
                            i++;
                        }
                    }
                }

                SQLiteDataAdapter adapter = new SQLiteDataAdapter(command);

                adapter.Fill(data);
                //command.Dispose();
                //connection.Dispose();
            }
            connection.Close();
        }

        return data;
    }*/

    public object ExecuteScalar(string query, bool withEncrypt = true, object[] parameter = null)
    {
        object data = 0;

        using (SQLiteConnection connection = new SQLiteConnection(m_conString))
        {
            connection.Open();

            if (isEncrypted && withEncrypt)
            {
                SQLiteCommand command = new SQLiteCommand(sqlKEY, connection);
                command.ExecuteNonQuery();
                command.Dispose();

                command = new SQLiteCommand(sqlLIC, connection);
                command.ExecuteNonQuery();
                command.Dispose();
            }

            using (SQLiteCommand command = new SQLiteCommand(query, connection))
            {
                if (parameter != null)
                {
                    string[] listPara = query.Split(' ');
                    int i = 0;
                    foreach (string item in listPara)
                    {
                        if (item.Contains("@"))
                        {
                            command.Parameters.AddWithValue(item, parameter[i]);
                            i++;
                        }
                    }
                }

                data = command.ExecuteScalar();
                //command.Dispose();
                //connection.Dispose();
            }
            connection.Close();
        }

        return data;
    }

    public DataTable GetAllColumnInTable(string table_name, string database_name)
    {
        DataTable table = ExecuteQuery($"SELECT column_name as name FROM {database_name}.INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = N'{table_name}'");
        return table;

    }

    /*public string GetDatabaseProvider()
    {
        return "sqlserver";
    }

    public string GetNameDatabase()
    {
        return m_NameDb;
    }

    public void ImportDataExcel(DataTable datatable)
    {
        throw new NotImplementedException();
    }*/

    public int UpdateDataTableFromSqliteSource(DataRow[] drs, string tbl, bool isReplace = false, bool withEncrypt = true, string queryStringCondition = "")
    {
        TongHopHelper.CheckTimeSync();

        if (isReplace)
            ExecuteNonQuery($"DELETE FROM \"{tbl}\" {queryStringCondition}");
        string queryString = $"SELECT * FROM \"{tbl}\" {queryStringCondition}";

        int numOfRow = 1;



        using (SQLiteConnection connection = new SQLiteConnection(m_conString))
        {
            connection.Open();

            if (isEncrypted && withEncrypt)
            {
                SQLiteCommand command = new SQLiteCommand(sqlKEY, connection);
                command.ExecuteNonQuery();
                command.Dispose();

                command = new SQLiteCommand(sqlLIC, connection);
                command.ExecuteNonQuery();
                command.Dispose();

            }
            var eachQr = 10000;
                var num = drs.Length / eachQr;
            for (int i = 0; i <= num; i++)
            {
                var drss = drs.Skip(i*eachQr).Take(eachQr).ToArray();

                using (var transaction = connection.BeginTransaction())
                {
                    using (var sqliteAdapter = new SQLiteDataAdapter(queryString, connection))
                    {
                        var cmdBuilder = new SQLiteCommandBuilder(sqliteAdapter);
                        cmdBuilder.ConflictOption = ConflictOption.OverwriteChanges;
                        numOfRow = sqliteAdapter.Update(drss);
                        cmdBuilder.Dispose();
                    }

                    transaction.Commit();
                }
            }

            connection.Close();
        }

        return numOfRow;
    }

    public int UpdateDataTableFromSqliteSource(DataTable dt, string tbl, bool isReplace = false, bool withEncrypt = true, string queryStringCondition = "")
    {

        return UpdateDataTableFromSqliteSource(dt.AsEnumerable().ToArray(), tbl, isReplace, withEncrypt, queryStringCondition);

        return 0;
    }

    public List<string> UpdateDataTableFromOtherSource(DataTable dtSource, string tbl, bool isReplace = false, bool isCompareTime = true, bool ResetModifiedProp = false)
    {
        string pkField = (tbl == Server.Tbl_GiaoViec_CongViecCha) ? "CodeCongViecCha"
                : (tbl == Server.Tbl_GiaoViec_CongViecCon) ? "CodeCongViecCon"
                : (tbl == MyConstant.TBL_DinhMuc) ? "ID"
                : "Code";
        string colLastChange = "LastChange";
        string colCreatedOn = "CreatedOn";
        string colModified = "Modified";
        List<string> codesUpdated = new List<string>();
        if (dtSource.Rows.Count == 0)
            return codesUpdated;



        string dbString = $"SELECT * FROM {tbl} LIMIT 0";
        DataTable dtDest = ExecuteQuery(dbString);
        string[] lsCol = dtSource.Columns.Cast<DataColumn>()
                                 .Select(x => x.ColumnName)
                                 .Where(x => dtDest.Columns.Contains(x)).ToArray();

        //if (tbl.Contains("KhoiLuo"))
        List<string> queries = new List<string>();
        foreach (DataRow dr in dtSource.Rows)
        {

            string code = dr[pkField].ToString();

            var dicVal = new Dictionary<string, string>();
            var sets = new List<string>();

            if (dr.RowState == DataRowState.Deleted)
            {
                string dbString1 = $"DELETE FROM {tbl} WHERE {pkField} = '{code}'";
                queries.Add(dbString1);
            }
            else
            {
                if (ResetModifiedProp)
                {
                    dr[colModified] = false;
                }
                foreach (var col in lsCol)
                {
                    if (dr[col] != DBNull.Value)
                    {

                        string valF = dr[col].ToString();
                        string val;
                        if (dtDest.Columns[col].DataType == typeof(bool))
                        {
                            val = (bool.Parse(dr[col].ToString())) ? "1" : "0";
                        }
                        else if (DateTime.TryParse(dr[col].ToString(), out DateTime date) &&
                            (dtDest.Columns[col].DataType == typeof(DateTime) || dtSource.Columns[col].DataType == typeof(DateTime)))
                        {
                            if (MyConstant.ColWithTime.Contains(col))
                                val = $"'{date.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE_WithTime)}'";
                            else val = $"'{date.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE)}'";
                        }
                        else
                        {
                            val = $"'{dr[col].ToString().Replace("'", "''")}'";
                        }

                        dicVal.Add(col, val);
                        sets.Add($"{col} = {val}");
                    }

                    else if (col != colLastChange)
                    {
                        string val = "NULL";
                        dicVal.Add(col, val);
                        sets.Add($"{col} = {val}");
                    }

                  

                }
                string dbString1 = $"INSERT OR IGNORE INTO {tbl} ({string.Join(", ", dicVal.Keys)}) VALUES ({string.Join(", ", dicVal.Values)})";
                queries.Add(dbString1);


                dbString1 = $"UPDATE {tbl} SET {string.Join(", ", sets)} WHERE {pkField} = '{code}'";
                if (dtSource.Columns.Contains(colLastChange) && dr[colLastChange] != DBNull.Value && isCompareTime)
                {
                    if (DateTime.TryParse(dr[colLastChange].ToString(), out DateTime date))
                        dbString1 += $" AND ({colLastChange} IS NULL OR {colLastChange} < '{date.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE_WithTime)}')";
                }
                queries.Add(dbString1);
            }

        }
        int num = ExecuteNonQueryFromList(queries);

        //if (num <= (queries.Count - 1) / 2)
        //    MessageShower.ShowError($"Lỗi cập nhật bảng {tbl}");

        //DataProvider.instanceTHDA.ExecuteNonQuery(string.Join(";\r\n", queries));



        /*string colsQuery = string.Join(", ", lsCol);

        string con = MyFunction.fcn_Array2listQueryCondition(dtSource.AsEnumerable().Select(x => x[pkField].ToString()).ToArray());

        if (isReplace)
            ExecuteNonQuery($"DELETE FROM \"{tbl}\"");

        string dbString = $"SELECT * FROM {tbl} WHERE {pkField} IN ({con})";
        DataTable dtDest = ExecuteQuery(dbString);

        var drsDest = dtDest.AsEnumerable().ToArray();

        foreach (DataRow drSource in dtSource.Rows)
        {
            DataRow drDest = drsDest.AsEnumerable().SingleOrDefault(x => x[pkField].ToString() == drSource[pkField].ToString());

            if (drDest is null)
            {
                drDest = dtDest.NewRow();
                dtDest.Rows.Add(drDest);


                foreach (string col in lsCol)
                {
                    if (dtDest.Columns.Contains(col))
                        drDest[col] = drSource[col];
                }
            }
            else
            {
                if (lsCol.Contains(colLastChange) && isCompareTime && tbl != Server.Tbl_ThongTinDuAn)
                {
                    bool isSrcDateTime = DateTime.TryParse((drSource[colLastChange] as string), out DateTime timeSource);
                    bool isDescDateTime = DateTime.TryParse((drDest[colLastChange] as string), out DateTime timeDesc);

                    if (!isSrcDateTime && isDescDateTime)
                        continue;
                    else if (isSrcDateTime && !isDescDateTime)
                    {
                        drDest.ItemArray = drSource.ItemArray;
                        codesUpdated.Add(drDest[pkField].ToString());
                        continue;
                    }
                    else if (!isDescDateTime && !isSrcDateTime)
                    {
                        drDest.ItemArray = drSource.ItemArray;
                        drDest[colLastChange] = null;
                        codesUpdated.Add(drDest[pkField].ToString());

                        continue;
                    }
                    else if (timeSource > timeDesc)
                    {
                        drDest.ItemArray = drSource.ItemArray;
                        codesUpdated.Add(drDest["0"].ToString());
                    }

                }
                else
                {
                    foreach (string col in lsCol)
                    {
                        if (dtDest.Columns.Contains(col))
                            drDest[col] = drSource[col];
                    }
                }
            }
        }
        UpdateDataTableFromSqliteSource(dtDest, tbl, isReplace);*/

        return codesUpdated;

    }



    /*public List<string> UpdateDataTableFromOtherSource<T>(List<object> dtSource, string tbl, string pkField, bool isReplace = false, bool isCompareTime = true)
    {
        string colLastChange = "LastChange";
        List<string> codesUpdated = new List<string>();
        if (dtSource.Rows.Count == 0)
            return codesUpdated;


        string[] lsCol = dtSource.GetPropertie()
                                 .Select(x => x.ColumnName)
                                 .ToArray();

        string dbString = $"SELECT * FROM {tbl} LIMIT 0";
        DataTable dtDest = ExecuteQuery(dbString);

        //if (tbl.Contains("KhoiLuo"))
        List<string> queries = new List<string>();
        foreach (DataRow dr in dtSource.Rows)
        {
            string code = dr[pkField].ToString();

            var dicVal = new Dictionary<string, string>();
            var sets = new List<string>();
            foreach (var col in lsCol.Where(x => dtDest.Columns.Contains(x)))
            {
                if (dr[col] != DBNull.Value)
                {

                    string valF = dr[col].ToString();
                    string val;
                    if (dtDest.Columns[col].DataType == typeof(bool))
                    {
                        val = ((bool)dr[col]) ? "1" : "0";
                    }
                    else if (dtDest.Columns[col].DataType == typeof(DateTime))
                    {
                        val = $"'{((DateTime)dr[col]).ToString(MyConstant.CONST_DATE_FORMAT_SQLITE)}'";
                    }
                    else
                        val = $"'{dr[col].ToString().Replace("'", "''")}'";

                    dicVal.Add(col, val);
                    sets.Add($"{col} = {val}");
                }

                else if (col != colLastChange)
                {
                    string val = "NULL";
                    dicVal.Add(col, val);
                    sets.Add($"{col} = {val}");
                }

            }
            string dbString1 = $"INSERT OR IGNORE INTO {tbl} ({string.Join(", ", dicVal.Keys)}) VALUES ({string.Join(", ", dicVal.Values)})";
            queries.Add(dbString1);


            dbString1 = $"UPDATE {tbl} SET {string.Join(", ", sets)} WHERE {pkField} = '{code}'";
            if (dr[colLastChange] != DBNull.Value)
            {
                dbString1 += $" AND ({colLastChange} IS NULL OR {colLastChange} < '{dr[colLastChange]}')";
            }
            queries.Add(dbString1);

        }
        ExecuteNonQueryFromList(queries);

        //DataProvider.instanceTHDA.ExecuteNonQuery(string.Join(";\r\n", queries));



        *//*string colsQuery = string.Join(", ", lsCol);

        string con = MyFunction.fcn_Array2listQueryCondition(dtSource.AsEnumerable().Select(x => x[pkField].ToString()).ToArray());

        if (isReplace)
            ExecuteNonQuery($"DELETE FROM \"{tbl}\"");

        string dbString = $"SELECT * FROM {tbl} WHERE {pkField} IN ({con})";
        DataTable dtDest = ExecuteQuery(dbString);

        var drsDest = dtDest.AsEnumerable().ToArray();

        foreach (DataRow drSource in dtSource.Rows)
        {
            DataRow drDest = drsDest.AsEnumerable().SingleOrDefault(x => x[pkField].ToString() == drSource[pkField].ToString());

            if (drDest is null)
            {
                drDest = dtDest.NewRow();
                dtDest.Rows.Add(drDest);


                foreach (string col in lsCol)
                {
                    if (dtDest.Columns.Contains(col))
                        drDest[col] = drSource[col];
                }
            }
            else
            {
                if (lsCol.Contains(colLastChange) && isCompareTime && tbl != Server.Tbl_ThongTinDuAn)
                {
                    bool isSrcDateTime = DateTime.TryParse((drSource[colLastChange] as string), out DateTime timeSource);
                    bool isDescDateTime = DateTime.TryParse((drDest[colLastChange] as string), out DateTime timeDesc);

                    if (!isSrcDateTime && isDescDateTime)
                        continue;
                    else if (isSrcDateTime && !isDescDateTime)
                    {
                        drDest.ItemArray = drSource.ItemArray;
                        codesUpdated.Add(drDest[pkField].ToString());
                        continue;
                    }
                    else if (!isDescDateTime && !isSrcDateTime)
                    {
                        drDest.ItemArray = drSource.ItemArray;
                        drDest[colLastChange] = null;
                        codesUpdated.Add(drDest[pkField].ToString());

                        continue;
                    }
                    else if (timeSource > timeDesc)
                    {
                        drDest.ItemArray = drSource.ItemArray;
                        codesUpdated.Add(drDest["0"].ToString());
                    }

                }
                else
                {
                    foreach (string col in lsCol)
                    {
                        if (dtDest.Columns.Contains(col))
                            drDest[col] = drSource[col];
                    }
                }
            }
        }
        UpdateDataTableFromSqliteSource(dtDest, tbl, isReplace);*//*

        return codesUpdated;

    }*/

    /*    public int UpdateDataTable(DataRow[] rows, string tbl, bool isReplace = false, bool withEncrypt = true, string queryString = "")
        {

            if (queryString == "")
                queryString = $"SELECT * FROM \"{tbl}\"";

            int numOfRow;
            using (SQLiteConnection connection = new SQLiteConnection(m_conString))
            {
                connection.Open();

                if (isEncrypted && withEncrypt)
                {
                    SQLiteCommand command = new SQLiteCommand(sqlKEY, connection);
                    command.ExecuteNonQuery();
                    command.Dispose();

                    command = new SQLiteCommand(sqlLIC, connection);
                    command.ExecuteNonQuery();
                    command.Dispose();

                }

                using (var sqliteAdapter = new SQLiteDataAdapter(queryString, connection))
                {
                    var cmdBuilder = new SQLiteCommandBuilder(sqliteAdapter);
                    if (isReplace)
                        ExecuteNonQuery($"DELETE FROM \"{tbl}\"");
                    numOfRow = sqliteAdapter.Update(rows);
                    sqliteAdapter.Fill(rows[0].Table);
                    cmdBuilder.Dispose();
                }

                connection.Close();
            }

            return numOfRow;
        }*/


    public int InsertDataTable(DataTable dt, string tbl, bool withEncrypt = true)
    {
        TongHopHelper.CheckTimeSync();
        int numOfRow;
        DataTable dtGoc = new DataTable();
        using (SQLiteConnection connection = new SQLiteConnection(m_conString))
        {
            connection.Open();

            if (isEncrypted && withEncrypt)
            {
                SQLiteCommand command = new SQLiteCommand(sqlKEY, connection);
                command.ExecuteNonQuery();
                command.Dispose();

                command = new SQLiteCommand(sqlLIC, connection);
                command.ExecuteNonQuery();
                command.Dispose();
            }

            using (var sqliteAdapter = new SQLiteDataAdapter($"SELECT * FROM \"{tbl}\"", connection))
            {

                var cmdBuilder = new SQLiteCommandBuilder(sqliteAdapter);
                sqliteAdapter.Fill(dtGoc);
                dtGoc.Merge(dt);
                //dtGoc.AcceptChanges();
                numOfRow = sqliteAdapter.Update(dtGoc);
                cmdBuilder.Dispose();
            }

            connection.Close();
        }

        return numOfRow;
    }

    /// <summary>
    /// Kiểm tra trong bảng tbl xem cột col đã có giá trị val hay chưa
    /// </summary>
    /// <param name="tbl"></param>
    /// <param name="col"></param>
    /// <param name="val"></param>
    /// <returns>true nếu có giá trị, false nếu col không có giá trị val nào</returns>
    public bool isHaveData(string tbl, string col, string val)
    {
        string dbString = $"SELECT \"{col}\" FROM \"{tbl}\" WHERE \"{col}\" = '{val}'";
        DataTable dt = ExecuteQuery(dbString);
        return (dt.Rows.Count > 0) ? true : false;
    }

    public int AddOrUpdate<T>(string query, List<T> item, bool transection = false, bool withEncrypt = true, bool isSQLFile = true)
    {

        int result = -1;
        TongHopHelper.CheckTimeSync();
        Debug.WriteLine("DB query: " + query);
        List<T> data = new List<T>();

        using (SQLiteConnection connection = new SQLiteConnection(m_conString))
        {

            try
            {
                connection.Open();

                if (isEncrypted && withEncrypt)
                {
                    SQLiteCommand command = new SQLiteCommand(sqlKEY, connection);
                    command.ExecuteNonQuery();
                    command.Dispose();

                    command = new SQLiteCommand(sqlLIC, connection);
                    command.ExecuteNonQuery();
                    command.Dispose();

                }
                if (isSQLFile) query = SqlResourceManager.GetInstance().GetSql(query);
                result = connection.Execute(query, item);
                //if (transection)
                //    Commit();
                //Logging.Info(string.Format(CommonConstants.LOG_SQL, query, JsonConvert.SerializeObject(item)), MethodBase.GetCurrentMethod().DeclaringType.ToString());
                connection.Close();
                return result;
            }
            catch (Exception ex)
            {
                string err = string.Format(CommonConstants.LOG_SQL, query, JsonConvert.SerializeObject(item));
                Logging.Error(err, ex, MethodBase.GetCurrentMethod().DeclaringType.ToString());
                AlertShower.ShowInfo(err);
                //Rollback();
                //_conn.Close();
                return result;
            }
        }
    }

    public int AddOrUpdate(string query, DataTable item, bool transection = false, bool isSQLFile = true)
    {

        int result = -1;
        TongHopHelper.CheckTimeSync();
        Debug.WriteLine("DB query: " + query);
        //List<T> /*data*/ = new List<T>();

        using (SQLiteConnection connection = new SQLiteConnection(m_conString))
        {
            try
            {
                if (isSQLFile) query = SqlResourceManager.GetInstance().GetSql(query);
                result = connection.Execute(query, item);
                //if (transection)
                //    Commit();
                //Logging.Info(string.Format(CommonConstants.LOG_SQL, query, JsonConvert.SerializeObject(item)), MethodBase.GetCurrentMethod().DeclaringType.ToString());
                connection.Close();
                return result;
            }
            catch (Exception ex)
            {
                string err = string.Format(CommonConstants.LOG_SQL, query, JsonConvert.SerializeObject(item));
                Logging.Error(err, ex, MethodBase.GetCurrentMethod().DeclaringType.ToString());
                AlertShower.ShowInfo(err);
                //Rollback();
                //_conn.Close();
                return result;
            }
        }
    }

    public int Execute(string query, object p = null, bool withEncrypt = true, bool isSQLFile = true)
    {
        int result = -1;
        TongHopHelper.CheckTimeSync();
        Debug.WriteLine("DB query: " + query);
        using (SQLiteConnection connection = new SQLiteConnection(m_conString))
        {
            try
            {
                connection.Open();

                if (isEncrypted && withEncrypt)
                {
                    SQLiteCommand command = new SQLiteCommand(sqlKEY, connection);
                    command.ExecuteNonQuery();
                    command.Dispose();

                    command = new SQLiteCommand(sqlLIC, connection);
                    command.ExecuteNonQuery();
                    command.Dispose();

                }
                if (isSQLFile) query = SqlResourceManager.GetInstance().GetSql(query);
                result = connection.Execute(query, p);
                connection.Close();
                return result;
            }
            catch (Exception ex)
            {
                string err = string.Format(CommonConstants.LOG_SQL, query, JsonConvert.SerializeObject(p));
                Logging.Error(err, ex, MethodBase.GetCurrentMethod().DeclaringType.ToString());
                AlertShower.ShowInfo(err);
                return result;
            }
        }
    }
}


