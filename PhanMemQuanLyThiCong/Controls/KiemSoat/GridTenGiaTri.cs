using DevExpress.XtraEditors;
using PhanMemQuanLyThiCong.Common.Constant;
using PhanMemQuanLyThiCong.Constant;
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

namespace PhanMemQuanLyThiCong.Controls.KiemSoat
{
    public partial class GridTenGiaTri : DevExpress.XtraEditors.XtraUserControl
    {
        public GridTenGiaTri()
        {
            InitializeComponent();
        }
        public bool Type { get; set; } = false;
        public bool TypeVLMTCNC { get; set; } = false;
        private void tl_TienNhaThau_NodeCellStyle(object sender, DevExpress.XtraTreeList.GetCustomNodeCellStyleEventArgs e)
        {
            if (Type)
            {
                switch (e.Node.Level)
                {
                    case 0:
                        e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold);
                        break;
                    case 1:
                        e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold);
                        e.Appearance.ForeColor = Color.Red;
                        break;  
                    case 2:
                        e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold);
                        e.Appearance.ForeColor = MyConstant.color_Row_CongTrinh;
                        break;
                    case 3:
                        e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold);
                        e.Appearance.ForeColor = MyConstant.color_Row_HangMuc;
                        break;
                    case 4:
                        e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold);
                        break;
                    default:
                        break;
                }
            }
            else if(!Type&&!TypeVLMTCNC)
            {
                switch (e.Node.Level)
                {
                    case 0:
                        e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold);
                        break;
                    case 1:
                        e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold);
                        e.Appearance.ForeColor = MyConstant.color_Row_CongTrinh;
                        break;
                    case 2:
                        e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold);
                        e.Appearance.ForeColor = MyConstant.color_Row_HangMuc;
                        break;
                    case 3:
                        e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold);
                        break;
                    default:
                        break;
                }
            }
            else
            {
                switch (e.Node.Level)
                {
                    case 0:
                        e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold);
                        e.Appearance.ForeColor = MyConstant.color_Row_CongTrinh;
                        break;
                    case 1:
                        e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold);
                        e.Appearance.ForeColor = MyConstant.color_Row_HangMuc;
                        break;
                    case 2:
                        e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold);
                        break;
                    default:
                        break;
                }
            }
        }

        public List<KLTTHangNgay> DataSource
        {
            get
            {
                return tl_TienNhaThau.DataSource as List<KLTTHangNgay>;
            }
            set
            {
                tl_TienNhaThau.DataSource = value;
            }
        
        }

        public void ExpandAll()
        {
            tl_TienNhaThau.ExpandAll();
        }

    }
}
