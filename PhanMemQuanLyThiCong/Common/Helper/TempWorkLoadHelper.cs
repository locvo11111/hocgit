using DevExpress.DataProcessing.InMemoryDataProcessor;
using DevExpress.XtraRichEdit.Fields;
using DevExpress.XtraSpellChecker;
using PhanMemQuanLyThiCong.Common.Constant;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhanMemQuanLyThiCong.Common.Helper
{
    public static class TempWorkLoadHelper
    {


        public static void CalcTempJobWorkLoad(string code = null)
        {
            WaitFormHelper.ShowWaitForm("Đang tính toán toàn bộ khối lượng công tác");
            
            if (code is null)
                DataProvider.InstanceTHDA.ExecuteNonQuery("TempWorkload.Job", isSQLFile: true, AddToDeletedDataTable: false);
            else
            {
     
                object[] prs = new object[]
                {
                    code,
                    code,
                };
                DataProvider.InstanceTHDA.ExecuteNonQuery("TempWorkload.Job_Update1", isSQLFile: true, AddToDeletedDataTable: false, parameter: prs);

//                string dbString = $"SELECT CodeGiaoThau FROM view_CongTacKeHoachThiCong WHERE Code = '{code}'";
//                var tbl = DataProvider.InstanceTHDA.ExecuteQuery(dbString);

//                if (tbl.Rows.Count > 0)
//                {
//                    var codeGT = tbl.Rows[0][0].ToString();
//                    prs = new object[]
//{
//                    codeGT,
//                    codeGT
//};
//                    DataProvider.InstanceTHDA.ExecuteNonQuery("TempWorkload.Job_Update1", isSQLFile: true, AddToDeletedDataTable: false, parameter: prs);

//                }
            }
            WaitFormHelper.CloseWaitForm();
        }
        public static void CalcTempGroupWorkLoad(string code = null)
        {
            WaitFormHelper.ShowWaitForm("Đang tính toán toàn bộ khối lượng nhóm");
            if (code is null)
                DataProvider.InstanceTHDA.ExecuteNonQuery("TempWorkload.Group", isSQLFile: true, AddToDeletedDataTable: false);
            else
            {
                object[] prs = new object[]
                {
                    code,
                    code,
                };
                DataProvider.InstanceTHDA.ExecuteNonQuery("TempWorkload.Group_Update1" , isSQLFile: true, AddToDeletedDataTable: false, parameter: prs);

//                string dbString = $"SELECT CodeNhomGiaoThau FROM {Server.Tbl_TDKH_NhomCongTac} WHERE Code = '{code}'";
//                var tbl = DataProvider.InstanceTHDA.ExecuteQuery(dbString);

//                if (tbl.Rows.Count > 0)
//                {
//                    var codeGT = tbl.Rows[0][0].ToString();
//                    prs = new object[]
//{
//                    codeGT,
//                    codeGT
//};
//                    DataProvider.InstanceTHDA.ExecuteNonQuery("TempWorkload.Group_Update1", isSQLFile: true, AddToDeletedDataTable: false, parameter: prs);

//                }
            }

            WaitFormHelper.CloseWaitForm();
        }
        public static void CalcTempMaterialWorkLoad(string code = null)
        {
            WaitFormHelper.ShowWaitForm("Đang tính toán toàn bộ khối lượng vật tư");

            if (code is null)
                DataProvider.InstanceTHDA.ExecuteNonQuery("TempWorkload.Material", isSQLFile: true, AddToDeletedDataTable: false);
            else
            {
                object[] prs = new object[]
                {
                    code,
                    code,
                };
                DataProvider.InstanceTHDA.ExecuteNonQuery("TempWorkload.Material_Update1", isSQLFile: true, AddToDeletedDataTable: false, parameter: prs);

//                string dbString = $"SELECT CodeNhomGiaoThau FROM {MyConstant.view_VatTuKeHoachThiCong} WHERE Code = '{code}'";
//                var tbl = DataProvider.InstanceTHDA.ExecuteQuery(dbString);

//                if (tbl.Rows.Count > 0)
//                {
//                    var codeGT = tbl.Rows[0][0].ToString();
//                    prs = new object[]
//{
//                    codeGT,
//                    codeGT
//};
//                    DataProvider.InstanceTHDA.ExecuteNonQuery("TempWorkload.Material_Update1", isSQLFile: true, AddToDeletedDataTable: false, parameter: prs);

//                }
            }
            WaitFormHelper.CloseWaitForm();
        }

        public static void CalcAllTempWorkload()
        {
            CalcTempJobWorkLoad();
            CalcTempGroupWorkLoad();
            CalcTempMaterialWorkLoad();
        }

    }
}
