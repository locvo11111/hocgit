using PhanMemQuanLyThiCong.Common.Enums;
using PhanMemQuanLyThiCong.Controls;
using PhanMemQuanLyThiCong.Controls.MTC;
using PhanMemQuanLyThiCong.Controls.QLVC;

namespace PhanMemQuanLyThiCong
{
    public partial class XtraForm3 : DevExpress.XtraEditors.XtraForm
    {

        public XtraForm3(UcType type , string text)
        {
            switch (type)
            {
                case UcType.MTC_CHUSOHUU:
                    Uc_QLChuSoHuu uc_QLChuSoHuu = new Uc_QLChuSoHuu();
                    uc_QLChuSoHuu.Dock = System.Windows.Forms.DockStyle.Fill;
                    this.Controls.Add(uc_QLChuSoHuu);
                    break;
                case UcType.MTC_TRANGTHAI:
                    Uc_QLTrangThai uc_QLTrangThai = new Uc_QLTrangThai();
                    uc_QLTrangThai.Dock = System.Windows.Forms.DockStyle.Fill;
                    this.Controls.Add(uc_QLTrangThai);
                    break;       
                case UcType.MTC_LOAIMAY:
                    Uc_LoaiMayMoc uc_LoaiMay = new Uc_LoaiMayMoc();
                    uc_LoaiMay.Dock = System.Windows.Forms.DockStyle.Fill;
                    this.Controls.Add(uc_LoaiMay);
                    break;        
                case UcType.TDKH_MUITHICONG:
                    Uc_DanhSachMuiThiCong uc_MuiTC = new Uc_DanhSachMuiThiCong();
                    uc_MuiTC.Dock = System.Windows.Forms.DockStyle.Fill;
                    this.Controls.Add(uc_MuiTC);
                    break;
                case UcType.QLVT_TenKho:
                    Uc_DanhSachKhoChung uc_TenKho = new Uc_DanhSachKhoChung();
                    uc_TenKho.Dock = System.Windows.Forms.DockStyle.Fill;
                    this.Controls.Add(uc_TenKho);
                    break;
            }
            InitializeComponent();
            this.Text = text;
        }
    }
}