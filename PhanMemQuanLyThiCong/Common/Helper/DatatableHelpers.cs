using DevExpress.Office.Utils;
using DevExpress.XtraRichEdit.Model;
using Newtonsoft.Json;
using PhanMemQuanLyThiCong.Common.Constant;
using PhanMemQuanLyThiCong.Constant;
//using PM360.Common.Constaint;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PhanMemQuanLyThiCong.Common.Helper
{
    public static class DatatableHelper
    {
        public static DataTable fcn_convertDatatableTo2Demension(DataTable dtSource, string col_TrucX, string col_TrucY, string col_Val, string[] XArray = null, string[] YArray = null)
        {

            if (XArray == null)
            {
                XArray = dtSource.AsEnumerable().Select(x => x[col_TrucX].ToString()).Distinct().ToArray();
            }

            if (YArray == null)
            {
                YArray = dtSource.AsEnumerable().Select(y => y[col_TrucY].ToString()).Distinct().ToArray();
            }

            DataTable dtDest = new DataTable();
            dtDest.Columns.Add(col_TrucY, dtSource.Columns[col_TrucY].DataType);
            foreach (string str in XArray)
            {
                if (col_Val != null)
                    dtDest.Columns.Add(str, dtSource.Columns[col_Val].DataType);
                else
                    dtDest.Columns.Add(str, typeof(bool));
            }

            //string[] itemTrucY = dtSource.AsEnumerable().Select(x => x[col_TrucY].ToString()).Distinct().ToArray();

            foreach (string str in YArray)
            {
                DataRow newrow = dtDest.NewRow();
                newrow[col_TrucY] = str;

                DataRow[] drs = dtSource.AsEnumerable().Where(x => x[col_TrucY].ToString() == str).ToArray();

                foreach (DataRow dr in drs)
                {
                    if (col_Val != null)
                    {
                        string[] ValsArr = drs.Where(x => x[col_TrucX].ToString() == dr[col_TrucX].ToString()).Select(y => y[col_Val].ToString()).ToArray();
                        newrow[dr[col_TrucX].ToString()] = string.Join(MyConstant.separator_checkedEdit.ToString(), ValsArr);
                    }
                    else
                    {
                        newrow[dr[col_TrucX].ToString()] = true;
                    }
                }

                dtDest.Rows.Add(newrow);
            }
            return dtDest;
        }
        public static DataTable fcn_List2Datatable<T>(List<T> ls)
        {

            if (ls.Count == 0)
            {
                DataTable dt = new DataTable();
                PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
                foreach (PropertyInfo prop in Props)
                {
                    //Setting column names as Property names
                    dt.Columns.Add(prop.Name);
                }
                return dt;
            }

            string json = JsonConvert.SerializeObject(ls);
            return JsonConvert.DeserializeObject<DataTable>(json);
            //return dt;
        }

        public static DataTable fcn_ObjToDataTable(this IList obj)
        {
            try
            {
                return BuildDataTable((IList)obj);
            }
            catch
            {
                return null;
            }
        }

        public static List<T> fcn_DataTable2List<T>(this DataTable dt)
        {
            try
            {
                //return ConvertDataTable<T>(dataTable);
                if (dt is null)
                    return new List<T>();

                string json = JsonConvert.SerializeObject(dt);
                
                var settings = new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore,
                    MissingMemberHandling = MissingMemberHandling.Ignore
                };

                return JsonConvert.DeserializeObject<List<T>>(json, settings);
            }
            catch (Exception ex)
            {
                MLogging.Logging.Error($"{ex.Message}_{ex.InnerException?.Message}");
                return new List<T>();
            }
        }

        private static List<T> ConvertDataTable<T>(DataTable table) where T : new()
        {
            List<T> list = new List<T>();
            var typeProperties = typeof(T).GetProperties().Select(propertyInfo => new
            {
                PropertyInfo = propertyInfo,
                Type = Nullable.GetUnderlyingType(propertyInfo.PropertyType) ?? propertyInfo.PropertyType
            }).ToList();

            foreach (var row in table.Rows.Cast<DataRow>())
            {
                T obj = new T();
                foreach (var typeProperty in typeProperties)
                {
                    string colName = typeProperty.PropertyInfo.Name;
                    if (!table.Columns.Contains(colName))
                        continue;

                    object value = row[colName];
                    object safeValue = value == null || DBNull.Value.Equals(value)
                        ? null
                        : Convert.ChangeType(value, typeProperty.Type);

                    typeProperty.PropertyInfo.SetValue(obj, safeValue, null);
                }
                list.Add(obj);
            }
            return list;
        }
        private static T GetItem<T>(DataRow dr)
        {
            Type temp = typeof(T);
            T obj = Activator.CreateInstance<T>();

            foreach (DataColumn column in dr.Table.Columns)
            {
                foreach (PropertyInfo pro in temp.GetProperties())
                {
                    if (pro.Name == column.ColumnName)
                        pro.SetValue(obj, dr[column.ColumnName], null);
                    else
                        continue;
                }
            }
            return obj;
        }

        public static string ConvertDataTableToHTML(this DataTable dt)
        {
            var html = new StringBuilder("<table border='1px' cellpadding='1' cellspacing='1' style='border-collapse: collapse;'>");

            //html.Append("<html >");
            //html.Append("<head>");
            //html.Append("</head>");
            //html.Append("<body>");
            //html.Append("<table border='1px' cellpadding='1' cellspacing='1' style='border-collapse: collapse;'>");
            //html.Append("<tr >");
            //Header
            html.Append("<thead><tr>");
            for (int i = 0; i < dt.Columns.Count; i++)
                html.Append("<th>" + dt.Columns[i].ColumnName + "</th>");
            html.Append("</tr></thead>");

            //Body
            html.Append("<tbody>");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                html.Append("<tr>");
                for (int j = 0; j < dt.Columns.Count; j++)
                {

                    string val = dt.Rows[i][j].ToString();

                    if (dt.Columns[j].DataType == typeof(int))
                    {
                        html.Append("<td style=\"text-align:center\">" + val + "</td>");

                    }
                    else if (val.ToUpper() == true.ToString().ToUpper())
                        html.Append("<td style=\"text-align:center\">" + "x" + "</td>");
                    else if (val.ToUpper() == false.ToString().ToUpper())
                        html.Append("<td style=\"text-align:center\"></td>");
                    else
                        html.Append("<td>" + val + "</td>");
                }
                html.Append("</tr>");
            }

            html.Append("</tbody>");
            html.Append("</table>");
            return html.ToString();
        }

        public static DataTable fcn_convertListTo2Demension<T>(List<T> ls, string col_TrucX, string col_TrucY, string col_Val, string[] XArray = null, string[] YArray = null)
        {
            DataTable dtSource = fcn_List2Datatable<T>(ls);
            return fcn_convertDatatableTo2Demension(dtSource, col_TrucX, col_TrucY, col_Val, XArray, YArray);
        }

        public static DataTable fcn_2Demension2Datatable(DataTable dtSource, string XHeader, string YHeader, string valueHeader, string[] nullText)
        {
            DataTable dtDest = new DataTable();
            dtDest.Columns.Add(XHeader, typeof(string));
            dtDest.Columns.Add(YHeader, typeof(string));

            if (valueHeader != null)
                dtDest.Columns.Add(valueHeader, typeof(string));

            foreach (DataRow dr in dtSource.Rows)
            {
                foreach (DataColumn dc in dtSource.Columns)
                {
                    if (dtSource.Columns.IndexOf(dc) > 0 && dr[dc.ColumnName] != null && dr[dc.ColumnName] != DBNull.Value && !nullText.Contains(dr[dc.ColumnName].ToString()))
                    {
                        if (valueHeader != null)
                            dtDest.Rows.Add(dc.ColumnName, dr[0], dr[dc.ColumnName]);
                        else
                            dtDest.Rows.Add(dc.ColumnName, dr[0]);
                    }
                }
            }

            return dtDest;
        }

        //public static DataTable BuildDataTableWithOnlyDisplayName(IList data)
        //{
        //    //Get properties
        //    PropertyInfo[] Props = data.GetType().GenericTypeArguments[0].GetProperties(BindingFlags.Public | BindingFlags.Instance);
        //    //.Where(p => !p.GetGetMethod().IsVirtual && !p.GetGetMethod().IsFinal).ToArray(); //Hides virtual properties

        //    //Get column headers
        //    bool isDisplayNameAttributeDefined = false;
        //    Dictionary<string, int> headers = new Dictionary<string, int>();
        //    int count = 0;
        //    foreach (PropertyInfo prop in Props)
        //    {
        //        isDisplayNameAttributeDefined = Attribute.IsDefined(prop, typeof(DisplayNameAttribute));

        //        if (isDisplayNameAttributeDefined)
        //        {
        //            DisplayNameAttribute dna = (DisplayNameAttribute)Attribute.GetCustomAttribute(prop, typeof(DisplayNameAttribute));
        //            if (dna != null)
        //                headers.Add(dna.DisplayName, count++);
        //        }
        //        isDisplayNameAttributeDefined = false;
        //    }

        //    //string[] headers = ;
        //    DataTable dataTable = new DataTable();

        //    //Add column headers to datatable
        //    foreach (var header in headers.Keys)
        //        dataTable.Columns.Add(header);

        //    dataTable.Rows.Add(headers.Keys);

        //    //Add datalist to datatable
        //    foreach (object item in data)
        //    {
        //        object[] values = new object[count];
        //        int ind = 0;
        //        foreach (var pair in headers)
        //            values[ind++] = Props[pair.Value].GetValue(item, null);

        //        dataTable.Rows.Add(values);
        //    }

        //    return dataTable;
        //}

        public static DataTable BuildDataTable(IList data, bool isOnlyPropWithDisplayName = false)
        {
            //Get properties
            PropertyInfo[] Props = data.GetType().GenericTypeArguments[0].GetProperties(BindingFlags.Public | BindingFlags.Instance);
            //.Where(p => !p.GetGetMethod().IsVirtual && !p.GetGetMethod().IsFinal).ToArray(); //Hides virtual properties

            //Get column headers
            bool isDisplayNameAttributeDefined = false;
            string[] headers = new string[Props.Length];
            List<string> lsNotHaveDisplayname = new List<string>();
            int colCount = 0;
            DataTable dataTable = new DataTable();
            foreach (PropertyInfo prop in Props)
            {
                
                isDisplayNameAttributeDefined = Attribute.IsDefined(prop, typeof(DisplayNameAttribute));

                if (isDisplayNameAttributeDefined)
                {
                    DisplayNameAttribute dna = (DisplayNameAttribute)Attribute.GetCustomAttribute(prop, typeof(DisplayNameAttribute));
                    if (dna != null)
                    {

                        headers[colCount] = dna.DisplayName;
                    }
                }
                else
                {
                    //headers[colCount] = prop.Name;
                    Type t = (prop.PropertyType.GenericTypeArguments.Any()) ? prop.PropertyType.GenericTypeArguments[0] : prop.PropertyType;
                    if (t == typeof(DateTime) || t == typeof(DateTime?))
                        t = typeof(string);
                    dataTable.Columns.Add(prop.Name, t);
                    lsNotHaveDisplayname.Add(prop.Name);
                }
                colCount++;
                isDisplayNameAttributeDefined = false;
            }


/*            //Add column headers to datatable
            foreach (var header in headers)
                dataTable.Columns.Add(header);*/

            //Add datalist to datatable
            foreach (object item in data)
            {
                object[] values = new object[Props.Length];
                for (int col = 0; col < Props.Length; col++)
                {
                    var prop = Props[col];
                    var val = prop.GetValue(item, null);
                    DateTime date = default;

                    if (val != null && (Props[col].PropertyType == typeof(DateTime) || Props[col].PropertyType == typeof(DateTime?)))
                    {
                        date = (DateTime)val; 
                        
                        if (MyConstant.ColWithTime.Contains(prop.Name))
                            values[col] = date.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE_WithTime);
                        else values[col] = date.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE);
                    }
                    else
                        values[col] = val;

                }
                dataTable.Rows.Add(values);
            }

            if (isOnlyPropWithDisplayName)
            {
                foreach (string col in lsNotHaveDisplayname)
                    dataTable.Columns.Remove(col);
            }

            return dataTable;
        }

        public static void ChangeRowIndex(this DataTable dataTable, int oldInd, int newInd)
        {
            if (oldInd >= dataTable.Rows.Count || newInd >= dataTable.Rows.Count)
            {
                throw new Exception("Index lớn hơn số hàng của dt");
            }

            DataRow newRow = dataTable.NewRow();
            newRow.ItemArray = dataTable.Rows[oldInd].ItemArray;

            dataTable.Rows[oldInd].Delete();

            if (newInd < dataTable.Rows.Count - 1)
            {
                dataTable.Rows.InsertAt(newRow, newInd);
            }
            else
                dataTable.Rows.Add(newRow);
        }

        public static void AddFromRowOfAnotherTable(this DataTable dt, DataRow row)
        {
            DataRow newRow = dt.NewRow();
            foreach (DataColumn cl in row.Table.Columns)
            {
                string name = cl.ColumnName;
                if (dt.Columns.Contains(name))
                    newRow[name] = row[name];
            }
            dt.Rows.Add(newRow);
        }
    }
}
