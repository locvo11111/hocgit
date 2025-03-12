using DevExpress.Mvvm.Native;
using DevExpress.XtraEditors;
using PhanMemQuanLyThiCong.Model.ChamCong;
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
    public partial class ctrl_DanhSachNhanVien : DevExpress.XtraEditors.XtraUserControl
    {
        public delegate void DE_TRUYENDATANHANVIEN(List<TamUngModel> Lst);
        public DE_TRUYENDATANHANVIEN m_DataChonNV;
        public string m_NameCot { get; set; }
        public ctrl_DanhSachNhanVien()
        {
            InitializeComponent();
        }
        public void Fcn_Update(List<TamUngModel> Lst)
        {
            gc_DanhSachNhanVien.DataSource = Lst;
        }
        public void Fcn_LoadTenCot(string NameCot)
        {
            m_NameCot = NameCot;
        }

        private void sb_Thoat_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void sb_All_Click(object sender, EventArgs e)
        {
            List < TamUngModel > TU = gc_DanhSachNhanVien.DataSource as List<TamUngModel>;
            TU.ForEach(x => x.Chon = true);
        }

        private void sb_Huy_Click(object sender, EventArgs e)
        {
            List<TamUngModel> TU = gc_DanhSachNhanVien.DataSource as List<TamUngModel>;
            TU.ForEach(x => x.Chon = false);
        }

        private void sb_Ok_Click(object sender, EventArgs e)
        {
            List<TamUngModel> TU = gc_DanhSachNhanVien.DataSource as List<TamUngModel>;
            List<TamUngModel> Chon = TU.Where(x => x.Chon == true).ToList();
            m_DataChonNV(Chon);
            this.Hide();
        }
    }
}
