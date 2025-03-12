using Dapper;
using DevExpress.XtraRichEdit.Layout;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhanMemQuanLyThiCong.Common.Helper
{
    public class NullableDateTimeHandler : SqlMapper.TypeHandler<DateTime?>
    {
        public override void SetValue(System.Data.IDbDataParameter parameter, DateTime? value)
        {
            if (value.HasValue)
                parameter.Value = value.Value;
            else
                parameter.Value = DBNull.Value;
        }

        public override DateTime? Parse(object value)
        {
            if (value == null || value is DBNull)
                return null;
            if (value is string stringValue && DateTime.TryParse(stringValue, out DateTime dateValue))
                return dateValue;
            return (DateTime?)value;
        }
    }
    
    public class DoubleHandler : SqlMapper.TypeHandler<double?>
    {
        public override void SetValue(System.Data.IDbDataParameter parameter, double? value)
        {
            if (value.HasValue)
                parameter.Value = value.Value;
            else
                parameter.Value = DBNull.Value;
        }

        public override double? Parse(object value)
        {
            if (value.GetType() != typeof(double?))
            {
                var isDb = double.TryParse(value?.ToString(), out double dbVal);
                if (isDb)
                    return dbVal;
                else return null;
            }
            else return (double?)value;
        }
    }
    
    public class GuidHandler : SqlMapper.TypeHandler<Guid?>
    {
        public override void SetValue(System.Data.IDbDataParameter parameter, Guid? value)
        {
            if (value.HasValue)
                parameter.Value = value.Value;
            else
                parameter.Value = DBNull.Value;
        }

        public override Guid? Parse(object value)
        {
            if (value.GetType() != typeof(Guid?))
            {
                var isDb = Guid.TryParse(value?.ToString(), out Guid dbVal);
                if (isDb)
                    return dbVal;
                else return null;
            }
            else return (Guid?)value;
        }
    }
}
