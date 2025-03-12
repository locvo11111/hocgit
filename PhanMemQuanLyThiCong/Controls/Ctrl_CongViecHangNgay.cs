using DevExpress.XtraEditors;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Columns;
using PhanMemQuanLyThiCong.Common.Constant;
using PhanMemQuanLyThiCong.Model.TDKH;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PhanMemQuanLyThiCong.Controls
{
    public partial class Ctrl_CongViecHangNgay : DevExpress.XtraEditors.XtraUserControl
    {
        public Ctrl_CongViecHangNgay()
        {
            InitializeComponent();
        }

        public List<KLTTHangNgay> DataSource
        {
            get
            {
                return tl_CongViecDangThucHien.DataSource as List<KLTTHangNgay>;
            }
            set
            {
                tl_CongViecDangThucHien.FormatRules.BeginUpdate();
                tl_CongViecDangThucHien.DataSource = value;
                tl_CongViecDangThucHien.FormatRules.EndUpdate();
            }
        }

        public void ExpandToLevel(int level)
        {
            tl_CongViecDangThucHien.ExpandToLevel(level);
        }

        public void ShowPrintPreview()
        {
            tl_CongViecDangThucHien.ShowPrintPreview();
        }


        public void CollapseAll()
        {
            tl_CongViecDangThucHien.CollapseAll();
        }

        public void ExpandAll()
        {
            tl_CongViecDangThucHien.ExpandAll();
        }

        private void Ctrl_CongViecHangNgay_Load(object sender, EventArgs e)
        {

        }

        private void tl_CongViecDangThucHien_Load(object sender, EventArgs e)
        {

        }

        private void tl_CongViecDangThucHien_NodeCellStyle(object sender, GetCustomNodeCellStyleEventArgs e)
        {
            if (e.Node.Level == 0)
            {
                e.Appearance.ForeColor = MyConstant.color_Row_HangMuc;
                e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold);
            }
            else if (e.Node.Level == 1)
            {
                e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold);
            }
            else if (e.Node.Level == 2)
                e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Italic);
        }

        #region Custom Properties

        //private bool ReadOnly = false;
        [DisplayName("ReadOnly")]
        public bool ReadOnly
        {
            get
            {
                return tl_CongViecDangThucHien.OptionsBehavior.ReadOnly;
            }

            set
            {
                tl_CongViecDangThucHien.OptionsBehavior.ReadOnly = value;
            }
        }


        private bool _KhoiLuongAsThanhTien = false;
        [DisplayName("KhoiLuongAsThanhTien")]
        public bool KhoiLuongAsThanhTien
        {
            get
            {
                return _KhoiLuongAsThanhTien;
            }

            set
            {
                _KhoiLuongAsThanhTien = value;
                col_ChenhLechKhoiLuong.Format.FormatString
                    = col_KhoiLuongKeHoach.Format.FormatString
                    = col_KhoiLuongThiCong.Format.FormatString
                    = (value) ? "c" : "n2";

            }
        }

        [DisplayName("Visible")]
        [Category("Cột kế hoạch")]
        public bool colKeHoachVisible
        {
            get {
                return col_KhoiLuongKeHoach.Visible;
            }

            set {
                col_KhoiLuongKeHoach.Visible = value;
            }
        }



        /// <summary>
        /// Cột kế hoạch
        /// </summary>
        [DisplayName("Caption")]
        [Category("Cột kế hoạch")]
        public string colKeHoachCaption
        {
            get {
                return col_KhoiLuongKeHoach.Caption;
            }

            set {
                col_KhoiLuongKeHoach.Caption = value;
            }
        }


        /// <summary>
        /// Cột thi công
        /// </summary>
        [DisplayName("Visible")]
        [Category("Cột thi công")]
        public bool colThiCongVisible
        {
            get
            {
                return col_KhoiLuongThiCong.Visible;
            }

            set
            {
                col_KhoiLuongThiCong.Visible = value;
            }
        }

        [DisplayName("Caption")]
        [Category("Cột thi công")]
        public string colThiCongCaption
        {
            get
            {
                return col_KhoiLuongThiCong.Caption;
            }

            set
            {
                col_KhoiLuongThiCong.Caption = value;
            }
        }

        /// <summary>
        /// Cột thi công
        /// </summary>
        [DisplayName("Visible")]
        [Category("Cột ngày bắt đầu")]
        public bool colNgayBatDauVisible
        {
            get
            {
                return colNgayBatDau.Visible;
            }

            set
            {
                colNgayBatDau.Visible = value;
            }
        }

        [DisplayName("Caption")]
        [Category("Cột ngày bắt đầu")]
        public string colNgayBatDauCaption
        {
            get
            {
                return colNgayBatDau.Caption;
            }

            set
            {
                colNgayBatDau.Caption = value;
            }
        }

        /// <summary>
        /// Cột thi công
        /// </summary>
        [DisplayName("Visible")]
        [Category("Cột ngày kết thúc")]
        public bool colNgayKetThucVisible
        {
            get
            {
                return colNgayKetThuc.Visible;
            }

            set
            {
                colNgayKetThuc.Visible = value;
            }
        }

        [DisplayName("Caption")]
        [Category("Cột ngày kết thúc")]
        public string colNgayKetThucCaption
        {
            get
            {
                return colNgayKetThuc.Caption;
            }

            set
            {
                colNgayKetThuc.Caption = value;
            }
        }

        /// <summary>
        /// Cột thi công
        /// </summary>
        [DisplayName("Visible")]
        [Category("Cột ngày hiện tại")]
        public bool colNgayHienTaiVisible
        {
            get
            {
                return col_NgayHienTai.Visible;
            }

            set
            {
                col_NgayHienTai.Visible = value;
            }
        }

        [DisplayName("Caption")]
        [Category("Cột ngày hiện tại")]
        public string colNgayHienTaiCaption
        {
            get
            {
                return col_NgayHienTai.Caption;
            }

            set
            {
                col_NgayHienTai.Caption = value;
            }
        }

        /// <summary>
        /// Cột chênh lêch
        /// </summary>
        [DisplayName("Visible")]
        [Category("Cột chênh lệch")]
        public bool colChenhLechVisible
        {
            get
            {
                return col_ChenhLechKhoiLuong.Visible;
            }

            set
            {
                col_ChenhLechKhoiLuong.Visible = value;
            }
        }

        [DisplayName("Caption")]
        [Category("Cột chênh lệch")]
        public string colChenhLechCaption
        {
            get
            {
                return col_ChenhLechKhoiLuong.Caption;
            }

            set
            {
                col_ChenhLechKhoiLuong.Caption = value;
            }
        }

        /// <summary>
        /// Cột chênh lêch
        /// </summary>
        [DisplayName("Visible")]
        [Category("Cột Đơn giá kế hoạch")]
        public bool colDonGiaKeHoach
        {
            get
            {
                return col_DonGiaKeHoach.Visible;
            }

            set
            {
                col_DonGiaKeHoach.Visible = value;
            }
        }

        [DisplayName("Caption")]
        [Category("Cột Đơn giá kế hoạch")]
        public string colDonGiaKeHoachCaption
        {
            get
            {
                return col_DonGiaKeHoach.Caption;
            }

            set
            {
                col_DonGiaKeHoach.Caption = value;
            }
        }

        /// <summary>
        /// Cột chênh lêch
        /// </summary>
        [DisplayName("Visible")]
        [Category("Cột Thành tiền kế hoạch")]
        public bool colThanhTienKeHoachVisible
        {
            get
            {
                return col_ThanhTienKeHoach.Visible;
            }

            set
            {
                col_ThanhTienKeHoach.Visible = value;
            }
        }

        [DisplayName("Caption")]
        [Category("Cột Thành tiền kế hoạch")]
        public string colThanhTienKeHoachCaption
        {
            get
            {
                return col_ThanhTienKeHoach.Caption;
            }

            set
            {
                col_ThanhTienKeHoach.Caption = value;
            }
        }

        /// <summary>
        /// Cột chênh lêch
        /// </summary>
        [DisplayName("Visible")]
        [Category("Cột Đơn giá thi công")]
        public bool colDonGiaThiCongVisible
        {
            get
            {
                return col_DonGiaThiCong.Visible;
            }

            set
            {
                col_DonGiaThiCong.Visible = value;
            }
        }

        [DisplayName("Caption")]
        [Category("Cột Đơn giá thi công")]
        public string colDonGiaThiCongCaption
        {
            get
            {
                return col_DonGiaThiCong.Caption;
            }

            set
            {
                col_DonGiaThiCong.Caption = value;
            }
        }

        /// <summary>
        /// Cột chênh lêch
        /// </summary>
        [DisplayName("Visible")]
        [Category("Cột Thành tiền thi công")]
        public bool colThanhTienThiCongVisible
        {
            get
            {
                return col_ThanhTienThiCong.Visible;
            }

            set
            {
                col_ThanhTienThiCong.Visible = value;
            }
        }

        [DisplayName("Caption")]
        [Category("Cột Thành tiền thi công")]
        public string colThanhTienThiCongCaption
        {
            get
            {
                return col_ThanhTienThiCong.Caption;
            }

            set
            {
                col_ThanhTienThiCong.Caption = value;
            }
        }


        #endregion

        #region Custom Handles
        public event EventHandler CustomSelectionChanged
        {
            add
            {
                tl_CongViecDangThucHien.SelectionChanged += value;
            }
            remove
            {
                tl_CongViecDangThucHien.SelectionChanged -= value;

            }
        }
        #endregion

        #region Custom Methods
        public TreeList Treelist
        {
            get { return tl_CongViecDangThucHien; }
        }
        #endregion

        private void tl_CongViecDangThucHien_DataSourceChanged(object sender, EventArgs e)
        {
            tl_CongViecDangThucHien.ExpandAll();

            col_DonGiaKeHoach.Visible = BaseFrom.IsShowDonGiaKeHoach;

            col_DonGiaKeHoach.Visible = BaseFrom.IsShowKhoiLuongKeHoach;

            if (!BaseFrom.IsShowKhoiLuongKeHoach || !BaseFrom.IsShowDonGiaKeHoach)
                col_KhoiLuongKeHoach.Visible = false;
        }
        
        public void RefreshDataSource()
        {
            tl_CongViecDangThucHien.RefreshDataSource();
        }
    }
}