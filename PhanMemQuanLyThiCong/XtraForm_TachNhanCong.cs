using DevExpress.XtraEditors;
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
using VChatCore.ViewModels.SyncSqlite;

namespace PhanMemQuanLyThiCong
{
    public partial class XtraForm_TachNhanCong : DevExpress.XtraEditors.XtraForm
    {
        IEnumerable<string> _codesCongTac;
        IEnumerable<string> _maVatTus;
        string _CodeHangMuc;
        
        //IEnumerable<string> _maVatTus;
        public XtraForm_TachNhanCong(IEnumerable<string> codesCongTac, string codeHM)
        {
            _codesCongTac = codesCongTac;
            _CodeHangMuc = codeHM;
            InitializeComponent();
        }

        private void XtraForm_TachNhanCong_Load(object sender, EventArgs e)
        {
            string dbString = $"SELECT * \r\n" +
                $"FROM {TDKH.Tbl_HaoPhiVatTu} hp\r\n" +
                $"WHERE hp.CodeCongTac IN ({MyFunction.fcn_Array2listQueryCondition(_codesCongTac)}) AND LoaiVatTu = 'Nhân công' ";

            var hps = DataProvider.InstanceTHDA.ExecuteQueryModel<Tbl_TDKH_HaoPhiVatTuViewModel>(dbString);

            _maVatTus = hps.Select(x => x.MaVatLieu);

            tl_hp.DataSource = hps.GroupBy(x => x.VatTu).Select(x => new TachVatTu()
            {
                TenGoc = x.Key,
                TenMoi = x.Key,
            }).ToList();

        }

        private void bt_OK_Click(object sender, EventArgs e)
        {
            WaitFormHelper.ShowWaitForm("Đang cập nhật");
            List<TachVatTu> hps = tl_hp.DataSource as List<TachVatTu>;

            List<string> updates = new List<string>();
            List<object> objs = new List<object>();
            foreach (var hp in hps)
            {
                string dbString = $"UPDATE {TDKH.Tbl_HaoPhiVatTu} SET VatTu = @TenMoi\r\n" +
                    $"WHERE VatTu = @TenGoc AND CodeCongTac IN ({MyFunction.fcn_Array2listQueryCondition(_codesCongTac)})";
                updates.Add(dbString);
                objs.Add(hp.TenMoi);
                objs.Add(hp.TenGoc);
            }

            if (updates.Count > 0) 
            {
                //var objs = new object[updates.Count];
                //Array.Fill
                DataProvider.InstanceTHDA.ExecuteNonQuery(string.Join(";\r\n", updates), parameter: objs.ToArray());
            }


            TDKHHelper.CapNhatAllVatTuHaoPhi(MaVatTu: _maVatTus.ToList(), CodesHangMuc: new string[] { _CodeHangMuc });
            WaitFormHelper.CloseWaitForm();
            MessageShower.ShowInformation("Đã cập nhật");
            Close();
        }
    }
}