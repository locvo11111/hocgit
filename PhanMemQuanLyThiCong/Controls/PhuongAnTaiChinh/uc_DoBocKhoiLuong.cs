using DevExpress.XtraEditors;
using DevExpress.XtraPrinting.Native.WebClientUIControl;
using PhanMemQuanLyThiCong.Common.Helper;
using PhanMemQuanLyThiCong.Model.PhuongAnTaiChinh;
using StackExchange.Profiling.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PhanMemQuanLyThiCong.Controls.PhuongAnTaiChinh
{
    public partial class uc_DoBocKhoiLuong : DevExpress.XtraEditors.XtraUserControl
    {

        const string nameDai = "Dai"; 
        const string nameRong = "Rong"; 
        const string nameCao = "Cao"; 
        const string nameSoBoPhanGiongNhau = "SoBoPhanGiongNhau"; 
        const string nameKhoiLuong = "KhoiLuong";
        const string nameHeSo = "HeSo";
        


        public delegate void DE_TransDoBocKL(string encrypted, string DonVi, double SoBoPhanGiongNhau, double KhoiLuong);
        public DE_TransDoBocKL de_TransDoBocKL;

        public uc_DoBocKhoiLuong()
        {
            InitializeComponent();
        }

        public void PushData(string TenCongTac, string jsonString)
        {
            spSheetDoBoc.LoadDocument($@"{BaseFrom.m_templatePath}\FileExcel\19.DoBoc1CongTac.xlsx");
            var doBoc = CryptoHelper.Base64DecodeToObject<DoBocKhoiLuong>(jsonString) ?? new DoBocKhoiLuong();
            var ws = spSheetDoBoc.ActiveWorksheet;
            lb_TenCongTac.Text = TenCongTac;
            txtDonVi.Text = doBoc.DonVi;
            ws.Range[nameDai].SetValue(doBoc.Dai);
            ws.Range[nameRong].SetValue(doBoc.Rong);
            ws.Range[nameCao].SetValue(doBoc.Cao);
            ws.Range[nameHeSo].SetValue(doBoc.HeSo);
            ws.Range[nameSoBoPhanGiongNhau].SetValue(doBoc.SoBoPhanGiongNhau);

            try
            {
                ws.Range[nameKhoiLuong].Formula = doBoc.Formula;
            }
            catch (Exception ex)
            {
                ws.Range[nameKhoiLuong].SetValue(doBoc.KhoiLuong);
            }

        }

        private void bt_huy_Click(object sender, EventArgs e)
        {
            Hide();
        }

        private void bt_ok_Click(object sender, EventArgs e)
        {
            var ws = spSheetDoBoc.ActiveWorksheet;

            var vm = new DoBocKhoiLuong()
            {
                DonVi = txtDonVi.Text,
                Dai = ws.Range[nameDai].Value.NumericValue,
                Rong = ws.Range[nameRong].Value.NumericValue,
                Cao = ws.Range[nameCao].Value.NumericValue,
                HeSo = ws.Range[nameHeSo].Value.NumericValue,
                SoBoPhanGiongNhau = ws.Range[nameSoBoPhanGiongNhau].Value.NumericValue,
                Formula = ws.Range[nameKhoiLuong].Formula,
                KhoiLuong = ws.Range[nameKhoiLuong].Value.NumericValue,
            };

            string encryptedString = CryptoHelper.Base64EncodeObject(vm);
            de_TransDoBocKL(encryptedString, txtDonVi.Text, vm.SoBoPhanGiongNhau, ws.Range[nameKhoiLuong].Value.NumericValue);
            Hide();
        }
    }
}
