using PhanMemQuanLyThiCong.IRepositories;
using PhanMemQuanLyThiCong.Services.ThongTinTongHopDuAnServices;
using System;
using System.Linq;
using System.Windows.Forms;
using Unity;

namespace PhanMemQuanLyThiCong
{
    public partial class rbfromphanquyen : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        //private IAppGroupServices _iAppGroupServices;
        private IThongTinTongHopDuAnServices _thongTinTongHopDuAnServices;
        //private IAppRolesServices _iAppRolesServices;
        public rbfromphanquyen()
        {
            InitializeComponent();
            //_iAppGroupServices = ConfigUnity.Container.Resolve<IAppGroupServices>();
            _thongTinTongHopDuAnServices = ConfigUnity.Container.Resolve<IThongTinTongHopDuAnServices>();
            //_iAppRolesServices = ConfigUnity.Container.Resolve<IAppRolesServices>();
        }
        public void OpenForm(Type typeform)
        {
            //foreach (var frm in MdiChildren.Where(frm => frm.GetType() == typeform))
            //{
            //    frm.Activate();
            //    return;
            //}
            //var form = (Form)(Activator.CreateInstance(typeform));
            //BeginInvoke(new Action(() =>
            //{
            //    form.MdiParent = this;
            //    form.Show();
            //}));
        }

        private  void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            OpenForm(typeof(fromnhomquyen));
        }

        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            OpenForm(typeof(fromquyen));
        }

        private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
           
        }

        private  void rbfromphanquyen_MdiChildActivate(object sender, EventArgs e)
        {
         
        }
    }
}