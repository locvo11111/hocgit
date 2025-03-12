using DevExpress.XtraEditors;
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
    public partial class Uc_ChonMuiThiCong : DevExpress.XtraEditors.XtraUserControl
    {
        List<ChonMuiThiCong> MTC;
        public delegate void DE_TRUYENDATAMTC(ChonMuiThiCong MuiTc,string CodeCT,bool BoChonMui=false,bool All=false);
        public DE_TRUYENDATAMTC m_DataChonMuiTC;
        private string m_CodeCT;
        private bool _All = false;
        public Uc_ChonMuiThiCong()
        {
            InitializeComponent();
        }

        private void Sb_Thoat_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
        public void Fcn_LoadData(string MuiThiCong,string CodeCT,bool All=false)
        {
            _All = All;
            m_CodeCT = CodeCT;
            string dbString = $"SELECT * FROM {TDKH.Tbl_TDKH_MuiThiCong} WHERE \"CodeGiaiDoan\"='{SharedControls.cbb_DBKH_ChonDot.SelectedValue}'";
            MTC = DataProvider.InstanceTHDA.ExecuteQueryModel<ChonMuiThiCong>(dbString);
            if (MuiThiCong != null)
            {
                MTC.Where(x => x.Code == MuiThiCong).FirstOrDefault().Chon = true;
            }
            tl_MTC.DataSource = MTC;
            tl_MTC.RefreshDataSource();
        }
        private void sb_OK_Click(object sender, EventArgs e)
        {
            ChonMuiThiCong Select = MTC.Where(x => x.Chon == true).FirstOrDefault();
            m_DataChonMuiTC(Select, m_CodeCT,All:_All);
            this.Hide();
        }

        private void sb_BoChon_Click(object sender, EventArgs e)
        {
            m_DataChonMuiTC(null, m_CodeCT,true,_All);
            this.Hide();
        }
    }
}
