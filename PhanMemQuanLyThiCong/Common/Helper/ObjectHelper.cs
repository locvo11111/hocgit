using DevExpress.Charts.Native;
using Newtonsoft.Json;
using PhanMemQuanLyThiCong.Model.MayThiCong;
﻿using Newtonsoft.Json;
using PhanMemQuanLyThiCong.Common.Constant;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PM360.Common.Helper
{
    public static class ObjectHelper
    {
        public static object GetValueByPropName(this object obj, string propName)
        {
            PropertyInfo pi = obj.GetType().GetProperty(propName);
            
            if (pi == null)
                throw new Exception($"Không tồn tại property: {propName}");

            return pi.GetValue(obj);
        }

        public static void SetValueByPropName(this object obj, string propName, object value)
        {
            PropertyInfo pi = obj.GetType().GetProperty(propName);

            if (pi == null)
                throw new Exception($"Không tồn tại property: {propName}");

            obj.GetType().GetProperty(propName).SetValue(obj, value);
        }

        public static T MapToOtherModel<T>(this object obj) where T : new()
        {
            T result = new T();
            PropertyInfo[] pisDest = result.GetType().GetProperties();
            string[] pisSourceName = obj.GetType().GetProperties().Select(x => x.Name).ToArray();
            foreach (PropertyInfo pi in pisDest)
            {
                if (pisSourceName.Contains(pi.Name))
                    pi.SetValue(result, obj.GetValueByPropName(pi.Name));

            }
            return result;
        }

        public static Nullable<T> CastExactlyObject<T>(object obj) where T : struct
        {
            T t = new T();
            if (obj.GetType() != t.GetType())
                return null;
            else
                return (T)obj;
        }

        public static void SetValueToPropertyClass<T>(T item, string propertyName, DevExpress.Spreadsheet.Cell cell)
        {
            PropertyInfo propertyInfo = typeof(T).GetProperty(propertyName);
            if (propertyInfo != null)
            {
                switch (Type.GetTypeCode(propertyInfo.PropertyType))
                {
                    case TypeCode.Boolean:
                        propertyInfo.SetValue(item, cell.Value.IsBoolean ? cell.Value.BooleanValue : false);
                        break;

                    case TypeCode.Decimal:
                        propertyInfo.SetValue(item, cell.Value.IsNumeric ? (decimal?)Math.Round(cell.Value.NumericValue, 4) : null);
                        break;

                    case TypeCode.Double:
                        propertyInfo.SetValue(item, cell.Value.IsNumeric ? (double?)Math.Round(cell.Value.NumericValue, 4) : null);
                        break;

                    case TypeCode.Int16:
                        propertyInfo.SetValue(item, cell.Value.IsNumeric ? (short?)Math.Round(cell.Value.NumericValue, 4) : null);
                        break;

                    case TypeCode.Int32:
                        propertyInfo.SetValue(item, cell.Value.IsNumeric ? (int?)Math.Round(cell.Value.NumericValue, 4) : null);
                        break;

                    case TypeCode.Int64:
                        propertyInfo.SetValue(item, cell.Value.IsNumeric ? (long?)Math.Round(cell.Value.NumericValue, 4) : null);
                        break;

                    case TypeCode.String:
                        propertyInfo.SetValue(item, cell.DisplayText);
                        break;

                    case TypeCode.DateTime:
                        propertyInfo.SetValue(item, cell.Value.IsDateTime ? (DateTime?)cell.Value.DateTimeValue : null);
                        break;
                }
            }
        }
        public static string ToCustomString(this DateTime? date)
        {
            if (!date.HasValue)
                return null;
            return date.Value.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE);
        }
    }
}
