using DevExpress.XtraLayout;
using DevExpress.XtraLayout.Utils;
using PhanMemQuanLyThiCong.Common.Constant;
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
using VChatCore.ViewModels;
using VChatCore.ViewModels.SyncSqlite;

namespace PhanMemQuanLyThiCong
{
    public partial class ctrl_XemFileNhieuCuaSo : UserControl
    {
        List<FileViewModel> _files = new List<FileViewModel> { };

        public ctrl_XemFileNhieuCuaSo()
        {
            InitializeComponent();
        }
        int SoCuaSo;
        //public string[] Files
        //{
        //    get { return Files; }
        //    set { Files = value; setFiles(); }
        //}

        public void setFiles(string[] files, string tblFile = null)
        {
            if (files != null)
            {
                //FileViewModel[] filesVM;
                if (tblFile != null)
                {
                    string[] filesName = files.Select(x => Path.GetFileName(x)).ToArray();
                    string dbString = $"SELECT * FROM {tblFile} " +
                        $"WHERE Code IN ({MyFunction.fcn_Array2listQueryCondition(filesName)})";
                    var filesDb = DataProvider.InstanceTHDA.ExecuteQueryModel<GeneralFileDinhKemViewModel>(dbString);
                    _files = new List<FileViewModel>();


                    foreach (var file in files)
                    {
                        var inDb = filesDb.SingleOrDefault(y => y.Code == Path.GetFileName(file));
                        _files.Add(new FileViewModel()
                        {
                            FilePath = file,
                            FileNameInDb = inDb?.FileDinhKem,
                            Table = tblFile,
                            Code = inDb?.Code,
                            Checksum = inDb?.Checksum,
                        });
                    }

                }
                else
                    _files = files.Select(x => new FileViewModel(x)).ToList();
            }
            else
            {
                _files = null;
            }
            foreach (LayoutControlItem item in Root.Items)
            {
                ctrl_XemFile ctrl = item.Control as ctrl_XemFile;
                ctrl.LoadFiles(_files);
            }
        }

        private void chiaLaiCuaSo()
        {
            layoutControl1.BeginUpdate();
            if (Root.Items.Count > 2)
            {
                for (int i = Root.Items.Count -1; i >= 2; i--)
                {
                    (Root.Items[i] as LayoutControlItem).Control.Dispose();
                    //Root.Items[i].Dispose();
                    //Root.Items.RemoveAt(i);
                }
                //Root.Items.Clear();
            }
            switch (cbb_SoCuaSo.SelectedIndex + 1)
            {
                case 1:
                    Root.Items[1].Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                    break;
                case 2:
                    Root.Items[1].Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                    break;
                case 3:
                    Root.Items[1].Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;

                    LayoutControlItem item3 = TaoItemXemFile("Cửa sổ 3");
                    Root.AddItem(item3, Root.Items[1], InsertType.Bottom);
                    //((ctrl_XemFile)item3.Control).LoadFiles(1);
                    break;
                case 4:
                    Root.Items[1].Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;

                    item3 = TaoItemXemFile("Cửa sổ 3");
                    LayoutControlItem item4 = TaoItemXemFile("Cửa sổ 4");
                    Root.AddItem(item3, Root.Items[1], InsertType.Bottom);
                    Root.AddItem(item4, Root.Items[0], InsertType.Bottom);
                    //Root.AddItem(item4, item3, InsertType.Right);
                    
                    break;
                default:
                    break;
            }
            layoutControl1.BestFit();
            layoutControl1.EndUpdate();

        }

        private void ctrl_XemFileNhieuCuaSo_Load(object sender, EventArgs e)
        {

            // Create a new layout item.
            LayoutControlItem item1 = TaoItemXemFile("Cửa sổ 1");
            LayoutControlItem item2 = TaoItemXemFile("Cửa sổ 2");
            item2.Visibility = LayoutVisibility.Never;
            Root.AddItem(item1);
            Root.AddItem(item2, Root.Items[0], InsertType.Right);
            //Root.Items.Where(x => Root.Items.IndexOf(x) > 1).ToList().ForEach(y => { Root.Items.RemoveAt});
            
        }

        private LayoutControlItem TaoItemXemFile(string Caption)
        {
            LayoutControlItem item = new LayoutControlItem();
            // Set the item's Control and caption.
            //item.Name = "File1";
            ctrl_XemFile FileView = new ctrl_XemFile();
            FileView.Name = Caption.Replace(" ", "");
            FileView.LoadFiles(_files);
            item.Control = FileView;
            item.Text = Caption;
            item.TextLocation = DevExpress.Utils.Locations.Top;
            
            return item;
        }

        private void cbb_SoCuaSo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbb_SoCuaSo.SelectedIndex < 0)
                return;
            chiaLaiCuaSo();
        }
    }
}
