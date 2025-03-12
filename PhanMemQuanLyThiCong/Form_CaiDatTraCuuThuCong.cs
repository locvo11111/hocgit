using DevExpress.XtraEditors;
using MoreLinq;
using PhanMemQuanLyThiCong.Common.Helper;
using PhanMemQuanLyThiCong.Model;
//using PM360.DAO.Models;
//using PM360.DAO.ViewModels;
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
    public partial class Form_CaiDatTraCuuThuCong : DevExpress.XtraEditors.XtraForm
    {
        List<CTDinhMuc> _ctDinhMucDefaults = new List<CTDinhMuc>();
        int _soCTDefault;
        //List<CongTac> _congTacs;

        public delegate void SendData(bool isMacDinh, int soCongTac, List<CTDinhMuc> cTDinhMucs);
        public SendData send;
        bool _isFirstTime = false;
        bool _isMacDinh = true;

        public Form_CaiDatTraCuuThuCong(bool isFirstTime, bool isMacDinh = true, int num = 5, List<CTDinhMuc> lsCTDM = null)
        {
            InitializeComponent();
            _isFirstTime = isFirstTime;
            rg_LoaiCaiDat.SelectedIndex = -1;
            checkList_DoUuTien.DataSource = lsCTDM ?? DinhMucMacDinh();
            nud_SoCongTac.Value = num;
            rg_LoaiCaiDat.SelectedIndex = isMacDinh ? 0 : 1;


            //bt_TraCuu.Enabled = !isFirstTime;
        }

        private void Form_CaiDatTraCuuThuCong_Load(object sender, EventArgs e)
        {

        }

        private void rg_LoaiCaiDat_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rg_LoaiCaiDat.SelectedIndex < 0)
                return;
            else if (rg_LoaiCaiDat.SelectedIndex == 0)
            {
                lc_setting.Enabled = false;
                checkList_DoUuTien.DataSource = DinhMucMacDinh();
                nud_SoCongTac.Value = 5;
            }
            else
            {
                lc_setting.Enabled = true;
                if (checkList_DoUuTien.DataSource is null)
                    checkList_DoUuTien.DataSource = DinhMucMacDinh();

            }
        }

        private List<CTDinhMuc> DinhMucMacDinh()
        {
            return new List<CTDinhMuc>()
        {
                new CTDinhMuc(){
                    chon = true,
                    ctDinhMuc = "XayDung_XD-TT12/2021"
                    },
                new CTDinhMuc(){
                    chon = true,
                    ctDinhMuc = "LÐ-TT12/2021 BXD"
                    },
                new CTDinhMuc(){
                    chon = true,
                    ctDinhMuc = "SuaChua_XD-TT12/2021"
                    },
                new CTDinhMuc(){
                    chon = true,
                    ctDinhMuc = "KhaoSat_XD-TT12/2021"
                    },
                new CTDinhMuc(){
                    chon = true,
                    ctDinhMuc = "ThiNghiem_XD-TT12/2021"
                    },
                new CTDinhMuc(){
                    chon = true,
                    ctDinhMuc = "MoiTruong_ĐT-592/2014-BXD"
                    },
                new CTDinhMuc(){
                    chon = true,
                    ctDinhMuc = "ThoatNuoc_ĐT-591/2014-BXD"
                    },
                new CTDinhMuc(){
                    chon = true,
                    ctDinhMuc = "ChieuSang_ĐT-594/2014-BXD"
                    },
                new CTDinhMuc(){
                    chon = true,
                    ctDinhMuc = "CayXanh_ĐT-593/2014-BXD"
                    },
                new CTDinhMuc(){
                    chon = true,
                    ctDinhMuc = "NuocSach_ĐT-590/2014-BXD"
                    },
                new CTDinhMuc(){
                    chon = true,
                    ctDinhMuc = "VienThong_2009/QĐ258"
                    },
                new CTDinhMuc(){
                    chon = true,
                    ctDinhMuc = "VienThong_288/QĐ_VNPT-KHĐT"
                    },
                new CTDinhMuc(){
                    chon = true,
                    ctDinhMuc = "VienThong_06/QĐ_VNPT-HĐTV-KH"
                    },
                new CTDinhMuc(){
                    chon = true,
                    ctDinhMuc = "VienThong_1595/2011_QĐ-BTTTT"
                    },
                new CTDinhMuc(){
                    chon = true,
                    ctDinhMuc = "SuaChua_DienLuc_203/2020_QĐ-EVN"
                    },
                new CTDinhMuc(){
                    chon = true,
                    ctDinhMuc = "ThiNghiem_DienLuc_32/2019_QĐ-EVN"
                    },
                new CTDinhMuc(){
                    chon = true,
                    ctDinhMuc = "DuongDay-BienAp_DienLuc_4970/2016/BCT"
                    },
                new CTDinhMuc(){
                    chon = true,
                    ctDinhMuc = "DienLuc_226/2015_QĐ-EVN"
                    },
                new CTDinhMuc(){
                    chon = true,
                    ctDinhMuc = "SuaChua_DienLuc_228/2015_QĐ-EVN"
                },
                new CTDinhMuc(){
                    chon = true,
                    ctDinhMuc = "CayXanh(TrongMoi)_ĐT-39/2002"
                }
        };
        }

        private void bt_moveUp_Click(object sender, EventArgs e)
        {
            int oldInd = checkList_DoUuTien.SelectedIndex;

            checkList_DoUuTien.DataSource = ((List<CTDinhMuc>)checkList_DoUuTien.DataSource).Move(oldInd, 1, oldInd - 1).ToList();
            checkList_DoUuTien.SelectedIndex = oldInd - 1;
        }

        private void bt_moveDown_Click(object sender, EventArgs e)
        {
            int oldInd = checkList_DoUuTien.SelectedIndex;
            checkList_DoUuTien.DataSource = ((List<CTDinhMuc>)checkList_DoUuTien.DataSource).Move(oldInd, 1, oldInd + 1).ToList();

            checkList_DoUuTien.SelectedIndex = oldInd + 1;

        }



        private void checkList_DoUuTien_SelectedIndexChanged(object sender, EventArgs e)
        {
            bt_moveUp.Enabled = (checkList_DoUuTien.SelectedIndex > 0);
            bt_moveDown.Enabled = (checkList_DoUuTien.SelectedIndex >= 0 && checkList_DoUuTien.SelectedIndex < checkList_DoUuTien.ItemCount - 1);
        }

        private void bt_Huy_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void bt_TraCuu_Click(object sender, EventArgs e)
        {
            _isFirstTime = false;
            send(rg_LoaiCaiDat.SelectedIndex == 0, (int)nud_SoCongTac.Value, ((List<CTDinhMuc>)checkList_DoUuTien.DataSource));
            this.Close();
        }

        private void Form_CaiDatTraCuuThuCong_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_isFirstTime)
            {
                MessageShower.ShowInformation("Không thể đóng trong lần cài đặt đầu tiền");
                e.Cancel = true;
            }
        }
    }
}