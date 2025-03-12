using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VChatCore.ViewModels.SyncSqlite;

namespace PhanMemQuanLyThiCong.Controls.QLVC
{
    public partial class Uc_LichSuDuyetHangNgay : DevExpress.XtraEditors.XtraUserControl
    {
        public Uc_LichSuDuyetHangNgay()
        {
            InitializeComponent();
        }
        public void Fcn_LoadData(string tbl,string CodeCha)
        {
            if (string.IsNullOrEmpty(CodeCha))
                return;
            string dbString = $"SELECT * FROM {tbl} WHERE \"CodeCha\"='{CodeCha}'";
            List<Tbl_QLVT_YeuCauVatTu_KhoiLuongHangNgayViewModel> LstDuyet = DataProvider.InstanceTHDA.ExecuteQueryModel<Tbl_QLVT_YeuCauVatTu_KhoiLuongHangNgayViewModel>(dbString);
            foreach(var item in LstDuyet)
            {
                if(string.IsNullOrEmpty(item.FullNameSend))
                    item.FullNameSend = BaseFrom.BanQuyenKeyInfo.FullName;
            }
            gc_LichSuDuỵet.DataSource = LstDuyet;
            gc_LichSuDuỵet.RefreshDataSource();
            gc_LichSuDuỵet.Refresh();
        }

        public class HistoryDuyet
        {
            private string Code { get; set; }
            private string CodeCha { get; set; }
            private DateTime? Ngay { get; set; }
            private string FullNameSend { get; set; } = BaseFrom.BanQuyenKeyInfo.FullName;
            private string TrangThai { get; set; }
            private double? KhoiLuong { get; set; }
        }
    }
}
