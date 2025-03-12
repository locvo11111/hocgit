using DevExpress.XtraEditors;
using PhanMemQuanLyThiCong.Common.Constant;
using PhanMemQuanLyThiCong.Common.Enums;
using PhanMemQuanLyThiCong.Common.Helper;
using PhanMemQuanLyThiCong.Model.DuAn;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static PhanMemQuanLyThiCong.Common.Helper.DuAnHelper;

namespace PhanMemQuanLyThiCong
{
    public partial class XtraForm_ThuyetMinhDuAn : DevExpress.XtraEditors.XtraForm
    {
        public XtraForm_ThuyetMinhDuAn()
        {
            InitializeComponent();
        }
        private void Fcn_UpDateData()
        {
            string dbString = $"SELECT  CASE WHEN TM.CodeParent = NULL THEN 0 ELSE CodeParent END AS CodeParent," +
                $"TM.FileDinhKem as HinhAnhDiKem, " +
                $"TM.*,'Hình ảnh đính kèm' as FileDinhKem FROM {MyConstant.TBL_THONGTINDUAN_ThuyetMinh} TM WHERE" +
                $" TM.CodeDuAn='{SharedControls.slke_ThongTinDuAn.EditValue}' "+
            $"GROUP BY\r\n\tTM.Code ORDER BY TM.SortId ASC";
            List<ThuyetMinhDuAn> TM = DataProvider.InstanceTHDA.ExecuteQueryModel<ThuyetMinhDuAn>(dbString);
            tl_ThuyetMinh.DataSource = TM;
            tl_ThuyetMinh.RefreshDataSource();
            tl_ThuyetMinh.ExpandAll();
        }

        private void sb_AddMucLon_Click(object sender, EventArgs e)
        {
            List<ThuyetMinhDuAn> TM = tl_ThuyetMinh.DataSource as List<ThuyetMinhDuAn>;
            string CodeParent = Guid.NewGuid().ToString();
            int STT = TM.Any() ? TM.Where(x => x.CodeParent == "0").Count() + 1 : 1;
            TM.Add(new ThuyetMinhDuAn
            {
                Code = CodeParent,
                CodeParent ="0",
                NoiDung="Nội dung mục lớn thuyết minh",
                STT=STT.ToString(),
                FileDinhKem= "Hình ảnh đính kèm",
                SortId =STT
            });     
            TM.Add(new ThuyetMinhDuAn
            {
                Code = Guid.NewGuid().ToString(),
                CodeParent = CodeParent,
                NoiDung=$"Nội dung thuyết minh",                
            });
            tl_ThuyetMinh.DataSource = TM;
            tl_ThuyetMinh.RefreshDataSource();
            tl_ThuyetMinh.ExpandAll();
            //tl_ThuyetMinh.Nodes[1].TreeList.Height = 100;
        }

        private void sb_Ok_Click(object sender, EventArgs e)
        {
            List<ThuyetMinhDuAn> TM = tl_ThuyetMinh.DataSource as List<ThuyetMinhDuAn>;
            foreach(var item in TM)
            {
                if (item.CodeParent == "0")
                    item.CodeParent = null;
                item.CodeDuAn = SharedControls.slke_ThongTinDuAn.EditValue.ToString();
                item.FileDinhKem = item.HinhAnhDiKem;
            }
            ListtoDataTableConverter converter = new ListtoDataTableConverter();
            DataTable dt = converter.ToDataTableList(TM);
            DataProvider.InstanceTHDA.UpdateDataTableFromOtherSource(dt, MyConstant.TBL_THONGTINDUAN_ThuyetMinh);
            this.Close();
        }

        private void XtraForm_ThuyetMinhDuAn_Load(object sender, EventArgs e)
        {
            Fcn_UpDateData();
        }

        private void tl_ThuyetMinh_CustomDrawNodeCell(object sender, DevExpress.XtraTreeList.CustomDrawNodeCellEventArgs e)
        {
            DevExpress.XtraTreeList.TreeList treeList = sender as DevExpress.XtraTreeList.TreeList;
            if (treeList.FocusedColumn == e.Column && treeList.FocusedNode == e.Node)
                return;
            if ((e.Column.FieldName == "FileDinhKem"|| e.Column.FieldName == "Ngay"||
                e.Column.FieldName == "Xoa")&& e.Node.Level==1)
            {
                e.Appearance.FillRectangle(e.Cache, e.Bounds);
                e.Handled = true;
            }
        }

        private void tl_ThuyetMinh_NodeCellStyle(object sender, DevExpress.XtraTreeList.GetCustomNodeCellStyleEventArgs e)
        {
            if (e.Node.Level == 0)
            {
                e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold);
                e.Appearance.ForeColor = Color.Green;
            }
            
                
        }

        private void tl_ThuyetMinh_ShowingEditor(object sender, CancelEventArgs e)
        {
            if(tl_ThuyetMinh.FocusedColumn.FieldName== "FileDinhKem"|| tl_ThuyetMinh.FocusedColumn.FieldName == "Ngay"||
                tl_ThuyetMinh.FocusedColumn.FieldName == "Xoa")
            {
                if (tl_ThuyetMinh.FocusedNode.Level == 1)
                    e.Cancel = true;
            }
        }
        private void Fcn_UpdataHinhAnh(string [] CodeHinhAnh)
        {
            string lstcode = string.Join(",",CodeHinhAnh);
            tl_ThuyetMinh.SetRowCellValue(tl_ThuyetMinh.FocusedNode, "HinhAnhDiKem", lstcode);

        }
        private void repositoryItemHyperLinkEdit1_Click(object sender, EventArgs e)
        {
            ThuyetMinhDuAn TM = tl_ThuyetMinh.GetFocusedRow() as ThuyetMinhDuAn;
            XtraForm_LuaChonHinhAnhDuAn frm = new XtraForm_LuaChonHinhAnhDuAn();
            frm.de_TranHinhAnh= new XtraForm_LuaChonHinhAnhDuAn.DE_TransHinhAnh(Fcn_UpdataHinhAnh);
            string [] Code = TM.HinhAnhDiKem is null ? null : TM.HinhAnhDiKem.Split(',');
            frm.Fcn_Update(/*TM.Code,*/ Code);
            frm.ShowDialog();

        }

        private void sb_Thoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void sb_ThemFileDuAn_Click(object sender, EventArgs e)
        {
            FormLuaChon luachon = new FormLuaChon(SharedControls.slke_ThongTinDuAn.EditValue.ToString(), FileManageTypeEnum.ThongTinDuAn, SharedControls.slke_ThongTinDuAn.Text);
            luachon.ShowDialog();
            //Fcn_UpDateData();
        }

        private void repositoryItemButtonEdit1_Click(object sender, EventArgs e)
        {
            ThuyetMinhDuAn TM = tl_ThuyetMinh.GetFocusedRow() as ThuyetMinhDuAn;
            DialogResult rs=MessageShower.ShowYesNoQuestion("Bạn có muốn xóa dòng thuyết minh này không??????");
            if (rs == DialogResult.Yes)
            {
                string CodeCon=tl_ThuyetMinh.FocusedNode.Nodes.FirstOrDefault().GetValue("Code").ToString();
                DuAnHelper.DeleteDataRows(MyConstant.TBL_THONGTINDUAN_ThuyetMinh, new string[] { CodeCon });
                DuAnHelper.DeleteDataRows(MyConstant.TBL_THONGTINDUAN_ThuyetMinh, new string[] { TM.Code });
                tl_ThuyetMinh.DeleteSelectedNodes();
            }
        }

        private void tl_ThuyetMinh_CalcNodeHeight(object sender, DevExpress.XtraTreeList.CalcNodeHeightEventArgs e)
        {
            //if (e.Node.Level == 1)
            //    e.NodeHeight = 400;
        }
    }
}