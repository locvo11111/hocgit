using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhanMemQuanLyThiCong.Common.Constant
{
    public class CusFilePath
    {
        public const string TaskFileDir = "Resource/Files/Task/{0}";//{0} Is Id of Task
        public const string SQLiteFile = "Resource/Files/{0}/{1}";//{0} Is TableName, {1} Is Guid Cha
    }
}
