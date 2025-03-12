using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using PhanMemQuanLyThiCong.Common.Constant;
using PhanMemQuanLyThiCong.Common.Helper;
using PhanMemQuanLyThiCong.Common.ViewModel;
using PM360.Common;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Unity;

namespace PhanMemQuanLyThiCong
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Form" />
    public partial class frmChonDinhMuc : Form
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="lsDM">The ls dm.</param>
        public delegate void DE_TRUYENDATADINHMUC(List<int> lsDM);

        /// <summary>
        /// The m data chon dm
        /// </summary>
        public DE_TRUYENDATADINHMUC m_DataChonDM;
        private List<int> lst_DinhMucTimKiem;
        private List<CategoryDinhMuc> categoryDinhMucs;

        /// <summary>
        /// Initializes a new instance of the <see cref="frmChonDinhMuc"/> class.
        /// </summary>
        public frmChonDinhMuc()
        {
            InitializeComponent();
        }

        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_Chon_Click(object sender, EventArgs e)
        {
            List<int> m_listDauDinhMuc = new List<int>();

            //m_listDauDinhMuc = categoryDinhMucs.FindAll(x=>x.Checked).Select(x=>x.ID).ToList();
            BaseFrom.ListSelectCategoryDinhMucs = categoryDinhMucs;
            DataProvider.InstanceTBT.UpdateDataTableFromOtherSource(categoryDinhMucs.fcn_ObjToDataTable(), MyConstant.TBL_DinhMuc);
            
            this.Close();
        } 

        private void LoadGroupDinhMuc()
        {
            categoryDinhMucs = DuAnHelper.GetAllCategoryDinhMuc();
            categoryDinhMucBindingSource.DataSource = categoryDinhMucs;
            gridControl.DataSource = categoryDinhMucBindingSource;          
        }

        private void FrmChonDinhMuc_Load(object sender, EventArgs e)
        {
            LoadGroupDinhMuc();
        }

        private void gridView_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            GridView View = sender as GridView;
            if (e.RowHandle >= 0)
            {
                ColumnView view = gridControl.MainView as ColumnView;
                GridColumn colChecked = view.Columns[0];
                var obj = view.GetRowCellValue(e.RowHandle, colChecked);
                if (obj != null && (bool)obj)
                {
                    e.Appearance.BackColor = Color.Yellow;
                    e.Appearance.Font = new Font("Arial", 9, FontStyle.Bold);
                }
            }
        }
    }
}