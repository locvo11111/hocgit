using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhanMemQuanLyThiCong.Common.Constant
{
    public class FormatString
    {
        public const string DispTenCongTac = "({0}) {1}";//Mã công tác, tên công tác
        public const string CodeDonViThucHien = "COALESCE({0}.CodeNhaThau, {0}.CodeNhaThauPhu, {0}.CodeToDoi)";//Mã công tác, tên công tác
        public const string CodeDonViNhanThau = "COALESCE({0}.CodeNhaThauPhu, {0}.CodeToDoi)";//Mã công tác, tên công tác
    }
}
