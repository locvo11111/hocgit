using System;
using System.Data.SQLite;

namespace PhanMemQuanLyThiCong.Repositories
{
    public class SqLiteBaseRepository
    {
        public static SQLiteConnection _conn;
        public static string path = AppDomain.CurrentDomain.BaseDirectory + @"\pm360.db3";
        public static string sqlKEY = $"PRAGMA key='TBT@@1!360';";
        public static string sqlREKEY = "PRAGMA rekey='TBT@@1!360';";
        public static string sqlLIC = "PRAGMA lic='13453-563-7092387-29456';";

        public static string DbFile
        {
            get { return path; }
        }

        public static string GetConnectionString()
        {
            return String.Format("Data Source={0}", DbFile);
        }

        public static SQLiteConnection GetConnection(DataProvider dtp)
        {
            string linkDb = dtp.m_NameDb;
            _conn = new SQLiteConnection(dtp.m_conString);
            _conn.Open();
            if (dtp.isEncrypted)
            {
                SQLiteCommand command = new SQLiteCommand(sqlLIC, _conn);
                command.ExecuteNonQuery();

                //command = new SQLiteCommand(sqlREKEY, _conn);
                //command.ExecuteNonQuery();
                ////_conn.Close();

                //_conn.Open();
                command = new SQLiteCommand(sqlKEY, _conn);
                command.ExecuteNonQuery();


                //command = new SQLiteCommand(sqlLIC, _conn);
                //command.ExecuteNonQuery();
                //_conn.Open();
                //_conn.Close();
            }

            return _conn;
        }
    }
}