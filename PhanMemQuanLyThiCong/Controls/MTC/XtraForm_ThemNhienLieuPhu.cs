using DevExpress.XtraEditors;
using Newtonsoft.Json;
using PhanMemQuanLyThiCong.Common.Constant;
using PhanMemQuanLyThiCong.Common.Helper;
using PhanMemQuanLyThiCong.Model.MayThiCong;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PhanMemQuanLyThiCong.Controls.MTC
{
    public partial class XtraForm_ThemNhienLieuPhu : DevExpress.XtraEditors.XtraForm
    {
        List<MTC_NhienLieuPhuJson> _ChiTiet;
        public delegate void DE_TransNLP(string encrypted, string display,string CodeNhatTrinh);
        public DE_TransNLP de_TransNLP;
        public string _CodeNhatTrinh;
        public XtraForm_ThemNhienLieuPhu()
        {
            InitializeComponent();
        }
        public void Fcn_LoadData(string Json,string CodeMay, string TenMay,string CodeNhatTrinh)
        {
            gr_DanhSachNhienLieuPhuTong.Text = $"Thông tin chi tiết vật liệu phụ hiện có của Máy: {TenMay}";
            var ChiTietNLPhu =Json is null?null: JsonConvert.DeserializeObject<List<MTC_NhienLieuPhuJson>>(Json);
            _ChiTiet = ChiTietNLPhu is null?new List<MTC_NhienLieuPhuJson>():ChiTietNLPhu;
            gc_DanhSachNhienLieuPhuCongTac.DataSource = _ChiTiet;
            string Dbstring = $"SELECT ROW_NUMBER() OVER(ORDER BY NLDA.Code) AS STT,NL.Ten,NL.DonVi,NLDA.* " +
                $"FROM {MyConstant.TBL_MTC_NHIENLIEUINMAY} NLDA" +
                $" LEFT JOIN {MyConstant.TBL_MTC_NHIENLIEU} NL ON NL.Code=NLDA.CodeNhienLieu" +
                $" LEFT JOIN {MyConstant.TBL_MTC_DANHSACHMAY} May ON May.Code=NLDA.CodeMay " +
                $"LEFT JOIN {MyConstant.TBL_MTC_DUANINMAY} MAYDA ON MAYDA.CodeMay=MAY.Code" +
                $" WHERE MAYDA.Code='{CodeMay}' AND NLDA.LoaiNhienLieu=2";
            List<MTC_NhienLieuInMay> Tong = DataProvider.InstanceTHDA.ExecuteQueryModel<MTC_NhienLieuInMay>(Dbstring);
            gc_DanhSachNhienLieuPhuTong.DataSource = Tong;
            _CodeNhatTrinh = CodeNhatTrinh;
        }
        private void cb_DanhSachNhienLieuPhuTong_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox cb = sender as CheckBox;
            int indOfTableLayout = 0;
            bool isMucLon = int.TryParse(cb.Name.Substring(cb.Name.Length - 1), out indOfTableLayout); //Có phải mục lớn không: Ví dụ ĐỀ XUẤT
            string TienTo = "Hiện";/*(isMucLon) ? "Thêm " : "Thêm quy trình ";*/
            string nameSearch = cb.Name.Replace("cb", "");
            bool isHasControl = false;
            foreach (Control ctrl in cb.Parent.Controls)
            {
                if (ctrl.Name.EndsWith(nameSearch) && ctrl != cb)
                {
                    isHasControl = true;
                    ctrl.Visible = cb.Checked;


                    if (!cb.Checked)
                    {
                        cb.Text = $"Hiện {cb.Text.Replace("Ẩn ", "")}";
                        cb.ForeColor = Color.Red;


                    }
                    else
                    {
                        cb.Text = $"Ẩn {cb.Text.Replace("Hiện ", "")}";
                        cb.ForeColor = Color.Black;

                    }
                }
            }
        }

        private void sb_DanhSachNhienLieuPhuTong_Click(object sender, EventArgs e)
        {
            int[] selectedRows = gv_DanhSachNhienLieuPhuTong.GetSelectedRows();
            List<MTC_NhienLieuInMay> Tong = selectedRows.Select(x => gv_DanhSachNhienLieuPhuTong.GetRow(x) as MTC_NhienLieuInMay).ToList();
            if (Tong is null || !Tong.Any())
                return;
            string[] CodeChiTiet = _ChiTiet.Select(x => x.CodeNhienLieu).ToArray();
            int STT = CodeChiTiet.Count()+1;
            foreach(var item in Tong)
            {
                if (item is null)
                    continue;
                if (CodeChiTiet.Contains(item.Code))
                    continue;
                _ChiTiet.Add(new MTC_NhienLieuPhuJson
                {
                    //Code = Guid.NewGuid().ToString(),
                    //CodeChiTiet=item.Code,
                    CodeNhienLieu = item.CodeNhienLieu,
                    STT =STT++,
                    Ten=item.Ten,
                    DonVi=item.DonVi,
                    MucTieuThu=0,
                });
               
            }
            gc_DanhSachNhienLieuPhuCongTac.DataSource = _ChiTiet;
            gc_DanhSachNhienLieuPhuCongTac.RefreshDataSource();
            gc_DanhSachNhienLieuPhuCongTac.Refresh();
        }

        private void sb_Save_Click(object sender, EventArgs e)
        {
            //var MTCCT = new MTC_ChiTietNhienLieuPhuJson();
            //MTCCT.NLPHU = _ChiTiet;
            if (_ChiTiet is null)
                return;
            var encryptedStr = JsonConvert.SerializeObject(_ChiTiet);
            string[] TenKL = _ChiTiet.Select(x => $"{x.Ten}: {x.MucTieuThu}").ToArray();
            string Display = TenKL.Any()? string.Join(",", TenKL):string.Empty;
            de_TransNLP(encryptedStr, Display, _CodeNhatTrinh);
            this.Close();
        }

        private void bt_Delete_Click(object sender, EventArgs e)
        {
            gv_DanhSachNhienLieuPhuCongTac.DeleteSelectedRows();
        }
    }
}