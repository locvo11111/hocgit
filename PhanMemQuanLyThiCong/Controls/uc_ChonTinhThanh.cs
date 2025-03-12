using DevExpress.XtraEditors;
using PhanMemQuanLyThiCong.Common.Constant;
using PhanMemQuanLyThiCong.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.RightsManagement;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MSETTING = PhanMemQuanLyThiCong.Properties.Settings;

namespace PhanMemQuanLyThiCong.Controls
{
    public partial class uc_ChonTinhThanh : DevExpress.XtraEditors.XtraUserControl
    {
        public uc_ChonTinhThanh()
        {
            InitializeComponent();

        }

        public event EventHandler ValueChanged;

        protected void OnValueChanged()
        {
            ValueChanged?.Invoke(this, EventArgs.Empty);

        }



        public void init()
        {
            ce_DonGiaTheoTinh.Checked = BaseFrom.ProvincesHaveDonGia.Contains(MSETTING.Default.Province);
            slke_TinhThanh.Properties.DataSource = BaseFrom.Provinces.Where(x => x.HaveDonGia).ToList();
            pushProvinces();
        }

        public void pushProvinces()
        {
            var source = BaseFrom.Provinces.Where(x => x.HaveDonGia).ToList();
            slke_TinhThanh.Properties.DataSource = source;
            slke_TinhThanh.EditValue = source.Select(x => x.TenKhongDau)
                .FirstOrDefault(x => x == MSETTING.Default.Province.Replace("*", "")) ?? MyConstant.DefaultProvince;
        }

        private void ce_DonGiaTheoTinh_CheckedChanged(object sender, EventArgs e)
        {
            slke_TinhThanh.Enabled = ce_DonGiaTheoTinh.Checked;
            if (!ce_DonGiaTheoTinh.Checked && !MSETTING.Default.Province.Contains("*"))
            {
                MSETTING.Default.Province = $"*{MSETTING.Default.Province}";
                MSETTING.Default.Save();
            }
        }

        private void slke_TinhThanh_EditValueChanged(object sender, EventArgs e)
        {
            var tt = slke_TinhThanh.GetSelectedDataRow() as Provinces;
            if (tt is null)
            {
                if (!MSETTING.Default.Province.Contains("*"))
                {
                    MSETTING.Default.Province = $"*{MSETTING.Default.Province}";
                    MSETTING.Default.Save();
                }
            }
            else
            {
                MSETTING.Default.Province = tt.TenKhongDau;
                MSETTING.Default.Save();
            }
            OnValueChanged();
        }
    }
}
