using Dapper;
using DevExpress.XtraEditors;
using DevExpress.XtraSpreadsheet.Model;
using PhanMemQuanLyThiCong.Common.Constant;
using PhanMemQuanLyThiCong.Common.Helper;
using PhanMemQuanLyThiCong.Common.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PhanMemQuanLyThiCong
{
    public partial class Xtraform_ThongTinDuAnOffline : DevExpress.XtraEditors.XtraForm
    {
        public Xtraform_ThongTinDuAnOffline()
        {
            InitializeComponent();
        }

        private void Xtraform_ThongTinDuAnOffline_Load(object sender, EventArgs e)
        {
            if (!BaseFrom.allPermission.HaveInitProjectPermission)
            {
                MessageShower.ShowWarning("Tính năng này chỉ dành cho Admin");
            }    


            string dbString = $"SELECT * FROM {MyConstant.TBL_THONGTINDUAN} " +
                //$"WHERE CreatedBySerialno = '{BaseFrom.BanQuyenKeyInfo.SerialNo}' " +
                $"ORDER BY CreatedOn DESC";
            var das = DataProvider.InstanceTHDA.ExecuteQueryModel<ThongTinDuAnExtensionViewModel>(dbString);
            tl_ThongTinDuAn.DataSource = das;
        }

        private void repobt_Xoa_Click(object sender, EventArgs e)
        {
            var da = tl_ThongTinDuAn.GetFocusedRow() as ThongTinDuAnExtensionViewModel;

            string queryStr = $"Dự án {da.TenDuAn} và các dữ liệu liên quan sẽ bị xóa!\r\n(Chỉ xóa dữ liệu Offline. Để xóa dự án online vui lòng truy cập danh sách dự án trên server)";

            if (MessageShower.ShowYesNoQuestion(queryStr) == DialogResult.Yes)
            {
                WaitFormHelper.ShowWaitForm("Đang xóa dự án!");
                DuAnHelper.DeleteDataRows(MyConstant.TBL_THONGTINDUAN, new string[] { da.Code }, AddToDeletedTable: false);
                Xtraform_ThongTinDuAnOffline_Load(null, null);
                WaitFormHelper.CloseWaitForm();
            }

        }

        private void repoIsShared_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        {

        }

        private void tl_ThongTinDuAn_CellValueChanging(object sender, DevExpress.XtraTreeList.CellValueChangedEventArgs e)
        {
            //tl_ThongTinDuAn.CloseEditor();
        }

        private void tl_ThongTinDuAn_CellValueChanged(object sender, DevExpress.XtraTreeList.CellValueChangedEventArgs e)
        {
            var da = tl_ThongTinDuAn.GetFocusedRow() as ThongTinDuAnExtensionViewModel;

            if (e.Column.FieldName == "IsShareToOtherKey")
            {
                string value = (bool)e.Value ? "1" : "0";
                string dbString = $"UPDATE {MyConstant.TBL_THONGTINDUAN} SET IsShareToOtherKey = '{value}' " +
                    $"WHERE Code = '{da.Code}'";
                DataProvider.InstanceTHDA.ExecuteNonQuery(dbString);
            }
            
            if (e.Column.FieldName == "AutoDivision")
            {
                string value = (bool)e.Value ? "1" : "0";
                string dbString = $"UPDATE {MyConstant.TBL_THONGTINDUAN} SET AutoDivision = '{value}' " +
                    $"WHERE Code = '{da.Code}'";
                DataProvider.InstanceTHDA.ExecuteNonQuery(dbString);
            }

            if (e.Column.FieldName == "IsAutoSynthetic")
            {
                string value = (bool)e.Value ? "1" : "0";
                string dbString = $"UPDATE {MyConstant.TBL_THONGTINDUAN} SET IsAutoSynthetic = '{value}' " +
                    $"WHERE Code = '{da.Code}'";
                DataProvider.InstanceTHDA.ExecuteNonQuery(dbString);
            }
        }

        private void repoIsShared_CheckedChanged(object sender, EventArgs e)
        {
            tl_ThongTinDuAn.CloseEditor();
        }

        private void tl_ThongTinDuAn_CustomDrawNodeCell(object sender, DevExpress.XtraTreeList.CustomDrawNodeCellEventArgs e)
        {
            if (e.Column.FieldName == "IsShareToOtherKey" && e.Node.GetValue("CreatedBySerialno").ToString() != BaseFrom.BanQuyenKeyInfo.SerialNo)
            {
                e.Appearance.FillRectangle(e.Cache, e.Bounds);
                e.Handled = true;
            }
        }
    }
}