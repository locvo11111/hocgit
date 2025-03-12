using DevExpress.DataAccess.Native.Sql.ConnectionStrategies;
using DevExpress.Utils.About;
using DevExpress.XtraEditors;
using DevExpress.XtraTreeList;
using PhanMemQuanLyThiCong.Common.Constant;
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

namespace PhanMemQuanLyThiCong.Controls.MTC
{
    public partial class uc_MTCs : DevExpress.XtraEditors.XtraUserControl
    {

        public delegate void DE_SENDDATA(Tbl_MTC_DanhSachMayViewModel codeMTC);
        public DE_SENDDATA sendData;

        public uc_MTCs()
        {
            InitializeComponent();
        }

        public void loadData(string codeVt)
        {
            string codeDA = SharedControls.slke_ThongTinDuAn.EditValue as string;
            string dbString = $"SELECT mtc.*\r\n" +
                $"FROM {Server.Tbl_MTC_DanhSachMay} mtc\r\n" +
                $"JOIN {Server.Tbl_MTC_DuAnInMay} daim\r\n" +
                $"ON mtc.Code = daim.CodeMay\r\n" +
                $"WHERE daim.CodeDuAn = '{codeDA}'";

            var mtcs = DataProvider.InstanceTHDA.ExecuteQueryModel<Tbl_MTC_DanhSachMayViewModel>(dbString);

            tl_MTC.DataSource = mtcs;


            dbString = $"SELECT CodeMay FROM {TDKH.TBL_KHVT_VatTu} WHERE Code = '{codeVt}'";
            DataTable dt = DataProvider.InstanceTHDA.ExecuteQuery(dbString);

            if (dt.Rows.Count > 0 )
            {
                string code = dt.Rows[0][0].ToString();
                tl_MTC.FocusedNode = tl_MTC.Nodes.SingleOrDefault(x => x.GetValue("Code").ToString() == code);
            }
        }

        private void tl_MTC_MouseDoubleClick(object sender, MouseEventArgs e)
        {

        }

        private void tl_MTC_MouseClick(object sender, MouseEventArgs e)
        {

        }

        private void repositoryItemButtonEdit1_Click(object sender, EventArgs e)
        {
            sendData(tl_MTC.GetFocusedRow() as Tbl_MTC_DanhSachMayViewModel);
        }
    }
}
