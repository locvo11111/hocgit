using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhanMemQuanLyThiCong.Common.Helper
{
    public static class EnumHelper
    {
        public static string GetEnumDisplayName(this Enum value)
        {
            var fi = value.GetType().GetField(value.ToString());
            var attributes = (System.ComponentModel.DataAnnotations.DisplayAttribute[])fi.GetCustomAttributes(typeof(System.ComponentModel.DataAnnotations.DisplayAttribute), false);
            if (attributes != null && attributes.Length > 0)
                return attributes[0].Name;
            else
                return null;
        }

        public static string GetEnumDisplayGroupName(this Enum value)
        {
            var fi = value.GetType().GetField(value.ToString());
            var attributes = (System.ComponentModel.DataAnnotations.DisplayAttribute[])fi.GetCustomAttributes(typeof(System.ComponentModel.DataAnnotations.DisplayAttribute), false);
            if (attributes != null && attributes.Length > 0)
                return attributes[0].GroupName;
            else
                return null;
        }

        public static string GetEnumDescription(this Enum enumValue)
        {
            var fieldInfo = enumValue.GetType().GetField(enumValue.ToString());

            var descriptionAttributes = (DescriptionAttribute[])fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false);

            return descriptionAttributes.Length > 0 ? descriptionAttributes[0].Description : null;
        }

        public static string GetEnumName(this Enum enumValue)
        {
            return Enum.GetName(enumValue.GetType(), enumValue);
        }

        public static IEnumerable<string> GetDisplayNames<T>() where T: Enum
        {
            List<string> vals = new List<string>();
            foreach (T item in Enum.GetValues(typeof(T)))
            {
                vals.Add(item.GetEnumDisplayName());
            }

            return vals;
        }
    }
}
