using DevExpress.XtraEditors;
using PhanMemQuanLyThiCong.Common.Constant;
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
    public partial class XtraForm_LayMayThiCong : DevExpress.XtraEditors.XtraForm
    {

        public delegate void DE_SendMTC(List<MayThiCongWithMayQuyDoi> list);
        public DE_SendMTC SendMTC;

        public XtraForm_LayMayThiCong()
        {
            InitializeComponent();

            string codeDA = SharedControls.slke_ThongTinDuAn.EditValue as string;
            
            string dbString = $"SELECT khvt.*, khvt.CodeMay AS ParentCode, mtc.Code AS CodeMayQuyDoi,mtc.Ten AS MayQuyDoi, " +
                $"mtc.DonVi AS DonViQuyDoi, mtc.GiaMuaMay, mtc.GiaCaMay, mtc.HaoPhi, mtc.TrangThai AS TrangThaiMayQuyDoi\r\n" +
                $"FROM {Server.Tbl_TDKH_KHVT_VatTu} khvt\r\n" +
                $"LEFT JOIN {Server.Tbl_MTC_DanhSachMay} mtc\r\n" +
                $"ON mtc.Code = khvt.CodeMay\r\n" +
                $"LEFT JOIN {Server.Tbl_MTC_DuAnInMay} da\r\n" +
                $"ON mtc.Code = da.CodeMay AND da.CodeDuAn = '{codeDA}'\r\n" +
                $"JOIN {MyConstant.TBL_THONGTINHANGMUC} hm\r\n" +
                $"ON khvt.CodeHangMuc = hm.Code\r\n" +
                $"JOIN {MyConstant.TBL_THONGTINCONGTRINH} ctr\r\n" +
                $"ON hm.CodeCongTrinh = ctr.Code\r\n" +
                $"WHERE ctr.CodeDuAn = '{codeDA}' AND khvt.LoaiVatTu = 'Máy thi công' AND khvt.CodeNhaThau IS NOT NULL";

            var mtcs = DataProvider.InstanceTHDA.ExecuteQueryModel<MayThiCongWithMayQuyDoi>(dbString);

            foreach (var item in mtcs.Where(x => x.ParentCode is null))
            {
                item.ParentCode = "Chưa quy đổi";
            }
            var grs = mtcs.GroupBy(x => x.ParentCode);

            
            foreach (var gr in grs)
            {
                var fst = gr.First();
                mtcs.Add(new MayThiCongWithMayQuyDoi()
                {
                    Code = gr.Key ?? "Chưa quy đổi",
                    VatTu = fst.MayQuyDoi ?? "Chưa quy đổi",
                    MaVatLieu = fst.No,
                    DonVi = fst.DonViMayQuyDoi,
                    TrangThai = fst.TrangThaiMayQuyDoi,
                    GiaMuaMay = fst.GiaMuaMay,
                    GiaCaMay = fst.GiaCaMay,
                    HaoPhi = fst.HaoPhi,

                });
            }

            tl_MTC.DataSource = mtcs;

        }

        private void bt_OK_Click(object sender, EventArgs e)
        {
            var mtcs = tl_MTC.DataSource as List<MayThiCongWithMayQuyDoi>;
            SendMTC(mtcs.Where(x => x.Chon != false).ToList());
            Close();
        }

        private void tl_MTC_NodeCellStyle(object sender, DevExpress.XtraTreeList.GetCustomNodeCellStyleEventArgs e)
        {
            if (e.Node.Level == 0)
            {
                e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold);
            }
            if (e.Node.Level == 2)
            {
                //e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold);

                //if (!colsWithForeColor.Contains(e.Column.FieldName))

            }
        }

        private void tl_MTC_DataSourceChanged(object sender, EventArgs e)
        {
            tl_MTC.ExpandAll();
        }
    }
}