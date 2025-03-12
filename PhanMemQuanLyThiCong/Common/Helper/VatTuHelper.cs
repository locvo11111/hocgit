using PhanMemQuanLyThiCong.Common.Constant;
using PhanMemQuanLyThiCong.Common.Enums;
using PhanMemQuanLyThiCong.Model.TDKH;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhanMemQuanLyThiCong.Common.Helper
{
    public class VatTuHelper
    {

        public static List<KLTTHangNgay> LoadVatTuHangNgayGiaoViec(TypeKLHN type, string codeCongTac)
        {
            string dbString = "";

            //var dt = DinhMucHelper.fcn_GetTblHaoPhiVatTuHienTai(type, codeCongTac);

            if (type == TypeKLHN.HaoPhiGiaoViecCon)
            {
                string mainStr = $"SELECT * FROM {TDKH.Tbl_HaoPhiVatTu} " +
                    $"JOIN {GiaoViec.TBL_CONGVIECCON} " +
                    $"ON {TDKH.Tbl_HaoPhiVatTu}.CodeCongViecCha = {GiaoViec.TBL_CONGVIECCON}.CodeCongViecCha " +
                    $"WHERE {GiaoViec.TBL_CONGVIECCON}.CodeCongViecCon = '{codeCongTac}'";

                DataTable dtMain = DataProvider.InstanceTHDA.ExecuteQuery(mainStr);

            }    

            return null;
        }
    }

}
