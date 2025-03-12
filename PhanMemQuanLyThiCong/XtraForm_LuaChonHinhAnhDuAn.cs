using DevExpress.XtraEditors;
using PhanMemQuanLyThiCong.Common.Constant;
using PhanMemQuanLyThiCong.Common.ViewModel;
using PhanMemQuanLyThiCong.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VChatCore.ViewModels.SyncSqlite;

namespace PhanMemQuanLyThiCong
{
    public partial class XtraForm_LuaChonHinhAnhDuAn : DevExpress.XtraEditors.XtraForm
    {
        //private string _CodeTM { get; set; }
        public delegate void DE_TransHinhAnh(string[] CodeHinhAnh);
        public DE_TransHinhAnh de_TranHinhAnh;
        public XtraForm_LuaChonHinhAnhDuAn()
        {
            InitializeComponent();
        }
        public void Fcn_Update(/*string CodeThuyetMinh,*/IEnumerable<string> lsCodeMain = null)
        {
            //_CodeTM = CodeThuyetMinh;
            string dbString = $"SELECT "+
               $"TM.Code,TM.FileDinhKem as Ten,TM.GhiChu FROM {MyConstant.TBL_THONGTINDUAN_FileDinhKem} TM" +
               $" LEFT JOIN {MyConstant.TBL_THONGTINDUAN} DA ON DA.Code=TM.CodeParent " +
               $" WHERE" +
               $" DA.Code='{SharedControls.slke_ThongTinDuAn.EditValue}' " +
               $"AND TM.FileDinhKem LIKE '%.jpg' OR TM.FileDinhKem LIKE '%.jpeg' OR TM.FileDinhKem LIKE '%.png' ";
            List<MayThiCongExtensionViewModel> TM = DataProvider.InstanceTHDA.ExecuteQueryModel<MayThiCongExtensionViewModel>(dbString);
            if (lsCodeMain!=null)
            {
                foreach(var item in TM)
                {
                    if (lsCodeMain.Contains(item.Code))
                        item.Chon = true;
                }
            }
            gc_HinhAnh.DataSource = TM;
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            gv_HinhAnh.SelectAll();
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            gv_HinhAnh.ClearSelection();
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            List<MayThiCongExtensionViewModel> TM = gc_HinhAnh.DataSource as List<MayThiCongExtensionViewModel>;
            string[] lstCode = TM.Where(x => x.Chon == true).Select(x => x.Code).ToArray();
            de_TranHinhAnh(lstCode);
            this.Close();
            //string updatestt = $"UPDATE  {MyConstant.TBL_THONGTINDUAN_ThuyetMinh} SET \"FileDinhKem\"='{MyFunction.fcn_Array2listQueryCondition(lstCode)}' " +
            //    $"WHERE \"Code\"='{_CodeTM}'";
            //DataProvider.InstanceTHDA.ExecuteNonQuery(updatestt);
        }

        private void repositoryItemButtonEdit1_Click(object sender, EventArgs e)
        {

            var crFile = gv_HinhAnh.GetFocusedRow() as MayThiCongExtensionViewModel;
            string m_path = Path.Combine(BaseFrom.m_FullTempathDA, "Resource", "Files", MyConstant.TBL_THONGTINDUAN_FileDinhKem, SharedControls.slke_ThongTinDuAn.EditValue.ToString(), crFile.Code);
            FileViewModel file = new FileViewModel()
            {
                FilePath = m_path,
                Table = MyConstant.TBL_THONGTINDUAN_FileDinhKem,
                Code = crFile.Code,

            };
            MyFunction.xemTruocFileCoBan(file, extension: Path.GetExtension(crFile.Ten));
        }

        private void repositoryItemButtonEdit1_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {

        }
    }
}