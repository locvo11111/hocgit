using DevExpress.XtraCharts.Designer.Native;
using DevExpress.XtraEditors;
using DevExpress.XtraRichEdit.Model;
using PhanMemQuanLyThiCong.Model;
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
    public partial class Ctrl_DonViThucHienDuAn : DevExpress.XtraEditors.XtraUserControl
    {
        public Ctrl_DonViThucHienDuAn()
        {
            InitializeComponent();
        }

        private void ce_CheckedChanged(object sender, EventArgs e)
        {
            //slke.Enabled = ce.Checked;
        }

        public void FcnAcceptchecked()
        {
            //ce.Checked = true;
        }
        public List<DonViThucHien> DataSource {
            get
            {
                return slke.Properties.DataSource as List<DonViThucHien>;
            } 
            set 
            {
                //string oldValue = slke.EditValue as string;
                //slke.EditValue = null;
                slke.Properties.DataSource = value;
                slke.EditValue = value?.FirstOrDefault()?.CodeFk;
            } 
        }

        public DonViThucHien SelectedDVTH
        {
            get
            {
                return slke.GetSelectedDataRow() as DonViThucHien;
            } 
        }
        
        public string EditValue
        {
            get
            {
                return slke.EditValue as string;
            } 
            set
            {
                slke.EditValue = value;
            }
        }
        
        public bool IsGiaoThau
        {
            get
            {
                return SelectedDVTH?.LoaiDVTH is null;
            }
        }

        //public event EventHandler CheckedChanged
        //{
        //    add
        //    {
        //        ce.CheckedChanged += value;
        //    }
        //    remove
        //    {
        //        ce.CheckedChanged -= value;
        //    }
        //}

        public event EventHandler DVTHChanged
        {
            add
            {
                slke.EditValueChanged += value;
            }
            remove
            {
                slke.EditValueChanged -= value;
            }
        }
        public void Fcn_Setting()
        {
            searchLookUpEdit1View.OptionsSelection.CheckBoxSelectorField = "IsSoSanh";
            searchLookUpEdit1View.OptionsSelection.MultiSelect = true;
            searchLookUpEdit1View.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect;
        }
        private void searchLookUpEdit1View_CalcRowHeight(object sender, DevExpress.XtraGrid.Views.Grid.RowHeightEventArgs e)
        {
            //if (searchLookUpEdit1View.IsGroupRow(e.RowHandle) && searchLookUpEdit1View.GetGroupRowValue(e.RowHandle) is null)
            //{
            //    searchLookUpEdit1View.MakeRowVisible(e.RowHandle, true);
            //}
        }
    }
}
