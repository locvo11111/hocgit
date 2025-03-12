using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Views.Base;
using PhanMemQuanLyThiCong.Common.Constant;
using PhanMemQuanLyThiCong.Common.Helper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PhanMemQuanLyThiCong
{
    public partial class Form_CaiDatDotHopDong : DevExpress.XtraEditors.XtraForm
    {
        static bool _Check = false;
        static string _CodeHD, _CodeDot;
        public Form_CaiDatDotHopDong()
        {
            InitializeComponent();
        }
        public void Fcn_Update(string CodeHD,string CodeDot)
        {
            _CodeHD = CodeHD;
            _CodeDot = CodeDot;
            string dbString = $"SELECT * FROM {MyConstant.TBL_HopDongCaiDatDot} " +
                  $"INNER JOIN {MyConstant.TBL_TaoHopDongMoi} " +
                  $"ON {MyConstant.TBL_HopDongCaiDatDot}.CodeHopDongCon = {MyConstant.Tbl_TAOMOIHOPDONG}.Code  " +
                  $"WHERE {MyConstant.TBL_HopDongCaiDatDot}.CodeDotMe='{CodeDot}'";
            DataTable dt = DataProvider.InstanceTHDA.ExecuteQuery(dbString);

            dbString = $"SELECT * FROM {MyConstant.Tbl_TAOMOIHOPDONG} " +
                 $"INNER JOIN {MyConstant.Tbl_TAOMOIHOPDONG_MECON} " +
                 $"ON {MyConstant.Tbl_TAOMOIHOPDONG_MECON}.CodeCon = {MyConstant.Tbl_TAOMOIHOPDONG}.Code  " +
                 $"WHERE {MyConstant.Tbl_TAOMOIHOPDONG_MECON}.CodeMe='{CodeHD}'";
            DataTable dthd = DataProvider.InstanceTHDA.ExecuteQuery(dbString);

            string lstCode = MyFunction.fcn_Array2listQueryCondition(dthd.AsEnumerable().Select(x => x["Code"].ToString()).ToArray());
            dbString = $"SELECT *  FROM {MyConstant.TBL_HopDong_DotHopDong} WHERE \"CodeHd\" IN ({lstCode}) ";
            DataTable Dt_Dot = DataProvider.InstanceTHDA.ExecuteQuery(dbString);
            List<ModelDot> ListDot = DuAnHelper.ConvertToList<ModelDot>(Dt_Dot);
            ListDot.ForEach(x => x.Code = x.Ten);
            Ricce_Dot.DataSource = ListDot;
            Ricce_Dot.ValueMember = "Code";
            Ricce_Dot.DisplayMember = "Ten";
            if (dt.Rows.Count == 0)
            {         
                List<Model> ModelHd = new List<Model>();
                foreach (DataRow item in dthd.Rows)
                {
                    dbString = $"SELECT *  FROM {MyConstant.TBL_HopDong_DotHopDong} WHERE \"CodeHd\"='{item["Code"]}' ";    
                    DataTable Dt_DotChiTiet = DataProvider.InstanceTHDA.ExecuteQuery(dbString);
                    List<ModelDot> ListDotCT = DuAnHelper.ConvertToList<ModelDot>(Dt_DotChiTiet);
                    ModelHd.Add(new Model
                    {
                        Code = Guid.NewGuid().ToString(),
                        CodeHopDongCon =item["Code"].ToString(),
                        TenHopDong = item["TenHopDong"].ToString(),
                        Dot = "Đợt 1"
                    }) ;
                }
                gc_ThongTinHD.DataSource = ModelHd;
            }
            else
            {
                List<Model> ModelHd = new List<Model>();
                foreach (DataRow item in dt.Rows)
                {
                    ModelHd.Add(new Model
                    {
                        Code = item["Code"].ToString(),
                        CodeHopDongCon =item["CodeHopDongCon"].ToString(),
                        TenHopDong = item["TenHopDong"].ToString(),
                        Dot = item["CodeDotCon"].ToString(),
                        Inssert=false
                    });
                }
                gc_ThongTinHD.DataSource = ModelHd;
            }
        }
        public class Model
        {
            public string Code { get; set; }
            public string CodeHopDongCon { get; set; }
            public string TenHopDong { get; set; }
            public string Dot { get; set; }
            public bool Inssert { get; set; } = true;
        }  
        public class ModelDot
        {
            public string Code { get; set; }
            public string Ten { get; set; }
        }

        private void gv_ThongTinHopDong_ShowingEditor(object sender, CancelEventArgs e)
        {

        }

        private void sb_Ok_Click(object sender, EventArgs e)
        {
            List<Model> Lst = gc_ThongTinHD.DataSource as List<Model>;
            string dbString = "";
            foreach (Model item in Lst)
            {
                if (item.Inssert)
                {
                    dbString = $"INSERT INTO '{MyConstant.TBL_HopDongCaiDatDot}' (\"Code\", \"CodeDotMe\", \"CodeHopDongCon\", \"CodeDotCon\") VALUES " +
$"('{Guid.NewGuid()}', '{_CodeDot}', '{item.CodeHopDongCon}', '{item.Dot}')";
                }
                else
                {
                    dbString = $"UPDATE {MyConstant.TBL_HopDongCaiDatDot} SET \"CodeHopDongCon\" = '{item.CodeHopDongCon}', \"CodeDotCon\" = '{item.Dot}' WHERE Code = '{item.Code}'";
                }
                DataProvider.InstanceTHDA.ExecuteNonQuery(dbString);
            }
            this.Close();
        }

        private void gv_ThongTinHopDong_ShownEditor(object sender, EventArgs e)
        {
            ColumnView view = (ColumnView)sender;
            Model Focus = gv_ThongTinHopDong.GetFocusedRow() as Model;
            if (view.FocusedColumn.FieldName == "Dot")
            {
                CheckedComboBoxEdit editor = (CheckedComboBoxEdit)view.ActiveEditor;
                string dbString = $"SELECT *  FROM {MyConstant.TBL_HopDong_DotHopDong} WHERE \"CodeHd\"='{Focus.CodeHopDongCon}' ";
                DataTable Dt_DotChiTiet = DataProvider.InstanceTHDA.ExecuteQuery(dbString);
                List<ModelDot> ListDotCT = DuAnHelper.ConvertToList<ModelDot>(Dt_DotChiTiet);
                ListDotCT.ForEach(x => x.Code=x.Ten);
                editor.Properties.DataSource = ListDotCT;
                editor.Properties.ValueMember = "Code";
                editor.Properties.DisplayMember = "Ten";
            }
        }
    }
}