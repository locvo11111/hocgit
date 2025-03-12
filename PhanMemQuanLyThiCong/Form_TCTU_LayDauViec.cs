using DevExpress.DataAccess.Native.Excel;
using DevExpress.Utils.Menu;
using DevExpress.XtraEditors;
using DevExpress.XtraTreeList.Nodes;
using PhanMemQuanLyThiCong.Constant;
using PhanMemQuanLyThiCong.Common.Helper;
using PhanMemQuanLyThiCong.Model.QuanLyVanChuyen;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PhanMemQuanLyThiCong.Common.Constant;

namespace PhanMemQuanLyThiCong
{
    public partial class Form_TCTU_LayDauViec : DevExpress.XtraEditors.XtraForm
    {
        public static Dictionary<string, string> Dic_Loai;
        public static Dictionary<string, string> Dic_Col;
        public static Dictionary<string, string> Dic_Code;
        public static Dictionary<string, string> Dic_Colkl;
        public static Dictionary<string, string> Dic_TBL;
        public static Dictionary<string, string> Dic_TBLHN;
        public static string m_Loai;
        public static bool _mcheckclosed = false;
        public delegate void DE__TRUYENDATAVL(string CodeCT,DataTable TH,string ParentID,string NoiDungUng,string ColThanhTien,long TT,string ColKL,string ColCode,string ngayBD,string NgayKT);
        public DE__TRUYENDATAVL m_TruyenData;
        public Form_TCTU_LayDauViec()
        {
            InitializeComponent();
            Dic_Loai = new Dictionary<string, string>()
            {
                {"Đề xuất","LuyKeYeuCau"},
                {"Nhập kho","LuyKeNhapTheoDot"},
                {"Xuất kho","LuyKeXuatTheoDot"},
                {"Chuyển kho","TonKhoChuyenDen"},
                {"Vận chuyển","LuyKeVanChuyenTheoDot"},
            };
            Dic_Col = new Dictionary<string, string>()
            {
                {"Đề xuất","ThanhTienYC"},
                {"Nhập kho","ThanhTienNK"},
                {"Xuất kho","ThanhTienXK"},
                {"Chuyển kho","ThanhTienCK"},
                {"Vận chuyển","ThanhTienVC"},
            };
            Dic_Code = new Dictionary<string, string>()
            {
                {"Đề xuất","CodeQLVCYCVT"},
                {"Nhập kho","CodeQLVCNVT"},
                {"Xuất kho","CodeQLVCXVT"},
                {"Chuyển kho","CodeQLVCCVT"},
                {"Vận chuyển","CodeQLVCVC"},
            };
            Dic_TBLHN = new Dictionary<string, string>()
            {
                {"Đề xuất",QLVT.TBL_QLVT_YEUCAUVTKLHN},
                {"Nhập kho",QLVT.TBL_QLVT_NHAPVTKLHN},
                {"Xuất kho",QLVT.TBL_QLVT_XUATVTKLHN},
                {"Chuyển kho",QLVT.TBL_QLVT_CHUYENKHOVTKLHN},
                {"Vận chuyển",QLVT.TBL_QLVT_NKVC},
            };   
            Dic_TBL = new Dictionary<string, string>()
            {
                {"Đề xuất",QLVT.TBL_QLVT_YEUCAUVT},
                {"Nhập kho",QLVT.TBL_QLVT_NHAPVT},
                {"Xuất kho",QLVT.TBL_QLVT_XUATVT},
                {"Chuyển kho",QLVT.TBL_QLVT_CHUYENKHOVT},
                {"Vận chuyển",QLVT.TBL_QLVT_QLVC},
            };     
        }
        public void Fcn_Update(string Loai)
        {
            m_Loai = Loai;
            foreach (string item in Dic_Loai.Keys)
            {
                if (item != Loai)
                    tL_QLVC_TongHop.Columns[Dic_Loai[item]].Visible = false;
            }    
            foreach (string item in Dic_Col.Keys)
            {
                if (item != Loai)
                    tL_QLVC_TongHop.Columns[Dic_Col[item]].Visible = false;
            }
        }
        private void Form_TCTU_LayDauViec_Load(object sender, EventArgs e)
        {
            WaitFormHelper.ShowWaitForm("Đang phân tích dữ liệu, Vui lòng chờ!");
            _mcheckclosed = false;
            dE_Begin.EditValueChanged -= dE_Begin_EditValueChanged;
            dE_End.EditValueChanged -= dE_End_EditValueChanged;
            dE_Begin.DateTime = dE_End.DateTime = DateTime.Now;
            dE_Begin.EditValueChanged += dE_Begin_EditValueChanged;
            dE_End.EditValueChanged += dE_End_EditValueChanged;
            List<InforCT_HM> Infor = MyFunction.InforHMCT(SharedControls.slke_ThongTinDuAn.Properties.DataSource, false,true);
            lUE_CongTrinh.Properties.DataSource = Infor;
            lUE_CongTrinh.EditValue = Infor.FirstOrDefault().ID;
            //List<TongHop> TH = SharedControls.tL_QLVC_TongHop.DataSource as List<TongHop>;
            DataTable dtCT, dtHM;
            DuAnHelper.fcn_GetDtCongTrinhHangMuc(out dtCT, out dtHM,MyConstant.TBL_THONGTINCONGTRINH,MyConstant.TBL_THONGTINHANGMUC,false);
            List<InforCT_HM> HM = SharedControls.rILUE_TenKhoTH.DataSource as List<InforCT_HM>;
            Fcn_LoadDataTongHop(dtCT, dtHM, HM);
            //TH.ForEach(x => x.Chon = false);
            //tL_QLVC_TongHop.DataSource = TH;
            //rILUE_TenKhoTH.DataSource = HM;
            tL_QLVC_TongHop.Refresh();
            //tL_QLVC_TongHop.ExpandAll();
            WaitFormHelper.CloseWaitForm();
        }
        private void Fcn_LoadDataTongHop(DataTable dtCT, DataTable dtHM, List<InforCT_HM> Infor)
        {
            string dbString = "";
            switch (m_Loai)
            {
                case "Đề xuất":
                    dbString = $"SELECT * FROM {QLVT.TBL_QLVT_YEUCAUVT} " +
                $"LEFT JOIN {QLVT.TBL_QLVT_NHAPVT} " +
                $"ON {QLVT.TBL_QLVT_NHAPVT}.CodeDeXuat={QLVT.TBL_QLVT_YEUCAUVT}.Code " +
                $"LEFT JOIN {QLVT.TBL_QLVT_XUATVT} " +
                $"ON {QLVT.TBL_QLVT_XUATVT}.CodeDeXuat={QLVT.TBL_QLVT_YEUCAUVT}.Code " +
                $"LEFT JOIN {QLVT.TBL_QLVT_CHUYENKHOVT} " +
                $"ON {QLVT.TBL_QLVT_CHUYENKHOVT}.CodeDeXuat={QLVT.TBL_QLVT_YEUCAUVT}.Code " +
                $"LEFT JOIN {QLVT.TBL_QLVT_QLVC} " +
                $"ON {QLVT.TBL_QLVT_QLVC}.CodeDeXuat={QLVT.TBL_QLVT_YEUCAUVT}.Code " +
                $" WHERE {QLVT.TBL_QLVT_YEUCAUVT}.CodeGiaiDoan='{SharedControls.cbb_DBKH_ChonDot.SelectedValue}'";
                    break;
                //case "Nhập kho":
                //    break;
                //case "Xuất kho":
                //    break;
                //case "Chuyển kho":
                //    break;
                default:
                    dbString = $"SELECT * FROM {Dic_TBL[m_Loai]} " +
                $"LEFT JOIN {QLVT.TBL_QLVT_YEUCAUVT} " +
                $"ON {Dic_TBL[m_Loai]}.CodeDeXuat={QLVT.TBL_QLVT_YEUCAUVT}.Code ";
                    foreach(string item in Dic_TBL.Keys)
                    {
                        if (item != m_Loai&&item!="Đề xuất")
                            dbString += $"LEFT JOIN {Dic_TBL[item]} ON {Dic_TBL[item]}.CodeDeXuat={QLVT.TBL_QLVT_YEUCAUVT}.Code ";
                    }
                    dbString += $" WHERE {QLVT.TBL_QLVT_YEUCAUVT}.CodeGiaiDoan='{SharedControls.cbb_DBKH_ChonDot.SelectedValue}'";
                    break;
            }
            string ngaybatdau = dE_Begin.DateTime.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE);
            string ngayketthuc = dE_End.DateTime.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE);
            DataTable dt_TH = DataProvider.InstanceTHDA.ExecuteQuery(dbString);
            string lst = MyFunction.fcn_Array2listQueryCondition(dt_TH.AsEnumerable().Select(x => x["Code"].ToString()).ToArray());
            dbString = $"SELECT * FROM {Dic_TBLHN[m_Loai]} WHERE \"CodeCha\" IN ({lst}) AND \"Ngay\">='{ngaybatdau}' AND \"Ngay\"<='{ngayketthuc}'";
            DataTable dt_HN= DataProvider.InstanceTHDA.ExecuteQuery(dbString);

            List<TongHop> T_hop = new List<TongHop>();
            foreach (var CT in dtCT.AsEnumerable().Where(x=>x["Code"].ToString()==lUE_CongTrinh.EditValue.ToString()).ToArray())
            {
                string crCodeCT = CT["Code"].ToString();

                T_hop.Add(new TongHop()
                {
                    ParentID = "0",
                    ID = crCodeCT,
                    MaVatTu = MyConstant.CONST_TYPE_CONGTRINH,
                    TenVatTu = CT["Ten"].ToString(),
                    //TenKhoNhap = ""
                });
                foreach (var HM in dtHM.Select($"[CodeCongTrinh] = '{crCodeCT}'"))
                {
                    string crCodeHM = HM["Code"].ToString();
                    T_hop.Add(new TongHop()
                    {
                        ParentID = crCodeCT,
                        ID = crCodeHM,
                        MaVatTu = MyConstant.CONST_TYPE_HANGMUC,
                        TenVatTu = HM["Ten"].ToString(),
                        //TenKhoNhap = ""
                    });

                    DataRow[] dt_vl = dt_TH.AsEnumerable().Where(x => x["CodeHangMuc"].ToString() == crCodeHM).ToArray();
                    foreach (var item in dt_vl)
                    {
                        double Kl = 0;
                        double TT = 0;
                        DataRow[] crRow =dt_HN.AsEnumerable().Where(x => x["CodeCha"].ToString() == item["Code"].ToString()).ToArray();
                        foreach(var cr in crRow)
                        {
                            if(m_Loai=="Vận chuyển")
                            {

                                Kl += double.Parse(cr["KhoiLuong_1Chuyen"].ToString())* double.Parse(cr["TongSoLuongChuyen"].ToString());
                                TT += double.Parse(cr["DonGia"].ToString()) * double.Parse(cr["KhoiLuong_1Chuyen"].ToString()) * double.Parse(cr["TongSoLuongChuyen"].ToString());
                                continue;
                            }
                            if (cr["KhoiLuong"].ToString() == "" || cr["DonGia"].ToString() == "")
                                continue;
                            Kl += double.Parse(cr["KhoiLuong"].ToString());
                            TT += double.Parse(cr["DonGia"].ToString()) * double.Parse(cr["KhoiLuong"].ToString());
                        }

                        T_hop.Add(new TongHop()
                        {
                            ParentID = crCodeHM,
                            ID = item["Code"].ToString(),
                            MaVatTu = item["MaVatTu"].ToString(),
                            TenVatTu = item["TenVatTu"].ToString(),
                            DonVi = item["DonVi"].ToString(),
                            TenKhoChuyenDen = item["TenKhoChuyenDen"].ToString(),
                            TenKhoChuyenDi = item["TenKhoChuyenDi"].ToString(),
                            HopDongKl = item["HopDongKl"]==DBNull.Value? 0 : double.Parse(item["HopDongKl"].ToString()),
                            LuyKeYeuCau = Kl,
                            ThanhTienYC=Math.Round(TT),
                            LuyKeVanChuyenTheoDot = Kl,
                            ThanhTienVC= Math.Round(TT),
                            //TonKhoChuyenDi = item["TonKhoChuyenDi"].ToString() == "" ? 0 : int.Parse(item["TonKhoChuyenDi"].ToString()),
                            TonKhoChuyenDen = Kl,
                            ThanhTienCK= Math.Round(TT),
                            LuyKeXuatTheoDot = Kl,
                            ThanhTienNK= Math.Round(TT),
                            LuyKeNhapTheoDot = Kl,
                            ThanhTienXK= Math.Round(TT),
                            Chon = false,
                            CodeDeXuat =m_Loai=="Đề xuất"?item["Code"].ToString(): item["CodeDeXuat"].ToString(),
                            CodeHd = item["CodeHd"].ToString(),
                            CodeKHVT = item["CodeKHVT"].ToString(),
                            CodeTDKH = item["CodeHd"].ToString(),
                            CodeCT=crCodeCT
                        });


                    }
                }
            }
            rILUE_TenKhoTH.DataSource = Infor;
            rILUE_TenKhoTH.DropDownRows = Infor.Count;
            tL_QLVC_TongHop.DataSource = T_hop;
            tL_QLVC_TongHop.RefreshDataSource();
            tL_QLVC_TongHop.Refresh();
            tL_QLVC_TongHop.ExpandAll();
        }
        private void tL_QLVC_TongHop_CustomDrawNodeCell(object sender, DevExpress.XtraTreeList.CustomDrawNodeCellEventArgs e)
        {
            DevExpress.XtraTreeList.TreeList treeList = sender as DevExpress.XtraTreeList.TreeList;
            if (treeList.FocusedColumn == e.Column && treeList.FocusedNode == e.Node)
                return;
            if (object.Equals(e.CellValue,(double)0) || (object.Equals(e.CellValue, false) && (e.Node.Level < 2)))
            {
                e.Appearance.FillRectangle(e.Cache, e.Bounds);
                e.Handled = true;
            }
        }

        private void tL_QLVC_TongHop_NodeCellStyle(object sender, DevExpress.XtraTreeList.GetCustomNodeCellStyleEventArgs e)
        {
            if (e.Node.Level == 0)
            {
                e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold);
                e.Appearance.ForeColor = Color.Green;
            }
            else if (e.Node.Level == 1)
            {
                e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold);
                e.Appearance.ForeColor = Color.LightSeaGreen;
            }
        }

        private void tL_QLVC_TongHop_CellValueChanging(object sender, DevExpress.XtraTreeList.CellValueChangedEventArgs e)
        {
            if (e.Node.Level == 0 || e.Node.Level == 1)
            {
                tL_QLVC_TongHop.CancelCurrentEdit();
                return;
            }
        }

        private void tL_QLVC_TongHop_ShowingEditor(object sender, CancelEventArgs e)
        {
            if (tL_QLVC_TongHop.FocusedColumn.FieldName != "Chon")
                e.Cancel = true;
        }

        private void sB_ok_Click(object sender, EventArgs e)
        {
            string ngaybatdau = dE_Begin.DateTime.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE);
            string ngayketthuc = dE_End.DateTime.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE);
            List<TongHop> TH = (tL_QLVC_TongHop.DataSource as List<TongHop>).FindAll(x => x.Chon && x.MaVatTu != "HM" && x.MaVatTu != "CTR");
            if (TH.Count() == 0)
            {
                MessageShower.ShowWarning("Vui lòng chọn chi phí muốn thu chi");
                return;
            }
            _mcheckclosed = true;
            //TH.ForEach(x => x.Chon = false);
            DataTable dt = DatatableHelper.fcn_List2Datatable<TongHop>(TH);
            long GiaTri = 0;
            string TenGhep="";
            foreach (DataRow item in dt.Rows)
            {
                GiaTri += long.Parse(item[Dic_Col[m_Loai]].ToString());
                TenGhep += $",{item["TenVatTu"].ToString()}";
            }

            string TenNoiDung = XtraInputBox.Show("", "Tên nội dung ứng", "");
            if (TenNoiDung == ""|| TenNoiDung==null)
                TenNoiDung = TenGhep.Remove(0, 1);
            m_TruyenData(TH.FirstOrDefault().CodeCT, dt, TH.FirstOrDefault().ParentID, TenNoiDung, Dic_Col[m_Loai], GiaTri, Dic_Loai[m_Loai], ngaybatdau, ngayketthuc, Dic_Code[m_Loai]);
            this.Close();
        }

        private void sB_Cancel_Click(object sender, EventArgs e)
        {
            tL_QLVC_TongHop.UncheckAll();
            //TreeListNode[] lst = tL_QLVC_TongHop.FindNodes(x => x.Level == 2);
            //if (lst.Count() == 0)
            //    return;
            //foreach (TreeListNode item in lst)
            //    item.SetValue("Chon", false);
        }

        private void tL_QLVC_TongHop_PopupMenuShowing(object sender, DevExpress.XtraTreeList.PopupMenuShowingEventArgs e)
        {
            DevExpress.XtraTreeList.TreeList tL = sender as DevExpress.XtraTreeList.TreeList;
            DevExpress.XtraTreeList.TreeListHitInfo hitInfo = tL.CalcHitInfo(e.Point);
            if (hitInfo.HitInfoType == DevExpress.XtraTreeList.HitInfoType.Cell)
            {
                DXMenuItem menuChon = new DXMenuItem("Chọn Vật Liệu", this.fcn_Handle_Popup_QLVT_ChonVatLieu);
                menuChon.Tag = hitInfo.Column;
                e.Menu.Items.Add(menuChon);
            }

        }
        private void fcn_Handle_Popup_QLVT_ChonVatLieu(object sender, EventArgs e)
        {
            var node = tL_QLVC_TongHop.Selection;
            foreach (TreeListNode row in node)
                tL_QLVC_TongHop.SetRowCellValue(row, tL_QLVC_TongHop.Columns["Chon"], true);
            tL_QLVC_TongHop.Refresh();
        }

        private void tL_QLVC_TongHop_BeforeFocusNode(object sender, DevExpress.XtraTreeList.BeforeFocusNodeEventArgs e)
        {
            if (e.Node.Level < 2)
                e.CanFocus = false;
        }
        private void lUE_CongTrinh_EditValueChanged(object sender, EventArgs e)
        {
            DataTable dtCT, dtHM;
            DuAnHelper.fcn_GetDtCongTrinhHangMuc(out dtCT, out dtHM,MyConstant.TBL_THONGTINCONGTRINH,MyConstant.TBL_THONGTINHANGMUC,false);
            List<InforCT_HM> HM = SharedControls.rILUE_TenKhoTH.DataSource as List<InforCT_HM>;
            Fcn_LoadDataTongHop(dtCT, dtHM, HM);
        }

        private void dE_Begin_EditValueChanged(object sender, EventArgs e)
        {
            DataTable dtCT, dtHM;
            DuAnHelper.fcn_GetDtCongTrinhHangMuc(out dtCT, out dtHM,MyConstant.TBL_THONGTINCONGTRINH,MyConstant.TBL_THONGTINHANGMUC,false);
            List<InforCT_HM> HM = SharedControls.rILUE_TenKhoTH.DataSource as List<InforCT_HM>;
            Fcn_LoadDataTongHop(dtCT, dtHM, HM);
        }

        private void dE_End_EditValueChanged(object sender, EventArgs e)
        {
            DataTable dtCT, dtHM;
            DuAnHelper.fcn_GetDtCongTrinhHangMuc(out dtCT, out dtHM,MyConstant.TBL_THONGTINCONGTRINH,MyConstant.TBL_THONGTINHANGMUC,false);
            List<InforCT_HM> HM = SharedControls.rILUE_TenKhoTH.DataSource as List<InforCT_HM>;
            Fcn_LoadDataTongHop(dtCT, dtHM, HM);
        }

        private void sB_All_Click(object sender, EventArgs e)
        {
            tL_QLVC_TongHop.CheckAll();
            //TreeListNode[] lst = tL_QLVC_TongHop.FindNodes(x => x.Level == 2 && x.Visible == true);
            //if (lst.Count() == 0)
            //    return;
            //foreach (TreeListNode item in lst)
            //    item.SetValue("Chon", true);
        }

        private void Form_TCTU_LayDauViec_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (_mcheckclosed)
                return;
            m_TruyenData("", null, "", "", "", default, "", "","", "");
        }
    }
}