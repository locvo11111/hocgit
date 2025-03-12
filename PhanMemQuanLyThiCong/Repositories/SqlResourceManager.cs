namespace PhanMemQuanLyThiCong.Repositories
{
    internal class SqlResourceManager
    {
        private static SqlResourceManager _sqlResourceManager = new SqlResourceManager();

        /// <summary>
        /// </summary>
        private SqlResourceManager()
        {
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns>SqlResourceManager</returns>
        public static SqlResourceManager GetInstance()
        {
            return _sqlResourceManager;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public string GetSql(string key)
        {
            string fullKey = "PhanMemQuanLyThiCong.FileSql." + key;
            return LoadSql(fullKey);
        }

        /// <summary>
        /// </summary>
        /// <param name="fullKey"></param>
        /// <returns></returns>
        private string LoadSql(string fullKey)
        {
            System.Reflection.Assembly myAssembly =
                System.Reflection.Assembly.GetExecutingAssembly();

            System.IO.Stream targetStream = myAssembly.GetManifestResourceStream(fullKey + ".SQL");

            System.IO.StreamReader sr =
                new System.IO.StreamReader(targetStream,
                System.Text.Encoding.GetEncoding("Unicode"));

            string s = sr.ReadToEnd();

            sr.Close();

            //string ret = s.Replace("\r\n", " ");

            return s;
        }

        /// <summary>
        /// </summary>
        /// <returns></returns>
        public string[] GetSqlKeys()
        {
            System.Reflection.Assembly myAssembly =
                System.Reflection.Assembly.GetExecutingAssembly();

            return myAssembly.GetManifestResourceNames();
        }
    }
}