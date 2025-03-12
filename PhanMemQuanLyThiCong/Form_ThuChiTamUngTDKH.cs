using DevExpress.XtraEditors;
using DevExpress.XtraTreeList.Nodes;
using PhanMemQuanLyThiCong.Constant;
using PhanMemQuanLyThiCong.Common.Helper;
using PhanMemQuanLyThiCong.Model;
using PhanMemQuanLyThiCong.Model.ThuChiTamUng;
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
using PhanMemQuanLyThiCong.Model.QuanLyVanChuyen;
using PhanMemQuanLyThiCong.Common.ViewModel.KLHN;

namespace PhanMemQuanLyThiCong
{
    public partial class Form_ThuChiTamUngTDKH : DevExpress.XtraEditors.XtraForm
    {
        public static string m_KH_TC;
        public static Dictionary<string, string> Dic_KL;
        public static Dictionary<string, string> Dic_TT;
        public static Dictionary<string, string> Dic_LoaiCP;
        public delegate void DE__TRUYENDATA(List<TCTU_TDKH> ThuChiTDKH,string ColCode,string TenNoiDung,string LoaiCP,string ToChucCaNhan);
        public DE__TRUYENDATA m__TRUYENDATA;
        public static bool _mcheckclosed = false;
        //public static DataTable m_dtCT, m_dtHM;
        public Form_ThuChiTamUngTDKH()
        {
            InitializeComponent();
            Dic_KL = new Dictionary<string, string>()
            {
                {"Thi Công","KhoiLuongThiCong" },
                {"Kế Hoạch","KhoiLuongKeHoach" },
            };  
            Dic_TT = new Dictionary<string, string>()
            {
                {"Thi Công","ThanhTienThiCong" },
                {"Kế Hoạch","ThanhTienKeHoach" },
            };      
            Dic_LoaiCP = new Dictionary<string, string>()
            {
                {"Nhân công","1" },
                {"Máy thi công","2" },
                {"Vật liệu","3" },
                {"Công tác","6" },
            };
        }
        public void Fcn_UpdateData(string KH_TC)
        {
            m_KH_TC = KH_TC;
            _mcheckclosed = false;
            //m_dtCT = dtCT;
            //m_dtHM = dtHM;
        }
        private void Fcn_LayData(string LCP)
        {
            WaitFormHelper.ShowWaitForm("Đang phân tích dữ liệu, Vui lòng chờ!");
            DataTable dtCT, dtHM;
            DuAnHelper.fcn_GetDtCongTrinhHangMuc(out dtCT, out dtHM,MyConstant.TBL_THONGTINCONGTRINH,MyConstant.TBL_THONGTINHANGMUC,false);
            string dbString = "";
            string ngaybatdau = dE_Begin.DateTime.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE);
            string ngayketthuc = dE_End.DateTime.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE);
            List<DonViThucHien> Infor = DuAnHelper.GetDonViThucHiens();
            DonViThucHien Detail = Infor.FindAll(x => x.Code == (string)lUE_ToChucCaNhan.EditValue).FirstOrDefault();
            List<TCTU_TDKH> ThuChiTDKH = new List<TCTU_TDKH>();
            int stt = 1;
            DataRow[] dstCT = dtCT.AsEnumerable().Where(x => x["Code"].ToString() == lUE_CongTrinh.EditValue.ToString()).ToArray();
            if (LCP=="Công tác")
            {
                dbString = $"SELECT COALESCE(cttk.CodeHangMuc, dmct.CodeHangMuc) AS CodeHangMuc," +
                    $"COALESCE(cttk.DonVi, dmct.DonVi) AS DonVi,COALESCE(cttk.TenCongTac, dmct.TenCongTac) AS TenCongTac, COALESCE(cttk.MaHieuCongTac, dmct.MaHieuCongTac) AS MaHieuCongTac," +
                    $"cttk.* FROM {TDKH.TBL_ChiTietCongTacTheoKy} cttk " +
                    $"LEFT JOIN {TDKH.TBL_DanhMucCongTac} dmct " +
                    $"ON dmct.Code=cttk.CodeCongTac " +
                    $"WHERE cttk.CodeGiaiDoan='{SharedControls.cbb_DBKH_ChonDot.SelectedValue}' " +
                    $"AND cttk.{Detail.ColCodeFK}='{Detail.Code}' AND cttk.CodeCha IS NULL";
                DataTable dt = DataProvider.InstanceTHDA.ExecuteQuery(dbString);
                string lsCT = MyFunction.fcn_Array2listQueryCondition(dt.AsEnumerable().Select(x => x["Code"].ToString()).ToArray());
                string[] lsCodeCongTac = dt.AsEnumerable().Select(x => x["Code"].ToString()).ToArray();

                var dt_TheoNgay = MyFunction.Fcn_CalKLKHNew(Common.Enums.TypeKLHN.CongTac, lsCodeCongTac, dE_Begin.DateTime, dE_End.DateTime);
                
                foreach (DataRow CT in dstCT.CopyToDataTable().Rows)
                {
                    string crCodeCT = CT["Code"].ToString();
                    ThuChiTDKH.Add(new TCTU_TDKH()
                    {
                        ParentID = "0",
                        ID = crCodeCT,
                        MaHieu = MyConstant.CONST_TYPE_CONGTRINH,
                        TenCongViec = CT["Ten"].ToString(),
                    });
                    foreach (var HM in dtHM.Select($"[CodeCongTrinh] = '{crCodeCT}'"))
                    {
                        string crCodeHM = HM["Code"].ToString();
                        ThuChiTDKH.Add(new TCTU_TDKH()
                        {
                            ParentID = crCodeCT,
                            ID = crCodeHM,
                            MaHieu = MyConstant.CONST_TYPE_HANGMUC,
                            TenCongViec = HM["Ten"].ToString(),
                        });
                        DataRow[] dt_ct = dt.AsEnumerable().Where(x => x["CodeHangMuc"].ToString() == crCodeHM).ToArray();
                        foreach(var rowct in dt_ct)
                        {
                            double Kl = 0;
                            double TT = 0;
                            List<KLHN> Crow = dt_TheoNgay.Where(x => x.ParentCode == rowct["Code"].ToString()).ToList();
                            if (m_KH_TC == "Thi Công")
                            {
                                Kl = Crow.Any()?(double)Crow.Sum(x => x.KhoiLuongThiCong): 0;
                                TT = (long)Math.Round(Crow.Sum(x => x.ThanhTienThiCong)??0);
                            }
                            else
                            {
                                Kl = Crow.Any() ? (double)Crow.Sum(x => x.KhoiLuongKeHoach) : 0;
                                TT = Crow.Any() ?(long)Crow.Sum(x => x.ThanhTienKeHoach):0;
                            }
                            ThuChiTDKH.Add(new TCTU_TDKH
                            {
                                STT=stt++,
                                ParentID = crCodeHM,
                                ID = rowct["Code"].ToString(),
                                MaHieu = rowct["MaHieuCongTac"].ToString(),
                                TenCongViec = rowct["TenCongTac"].ToString(),
                                DonVi= rowct["DonVi"].ToString(),
                                KhoiLuongKeHoach=Math.Round(Kl, 4),
                                KhoiLuongThiCong= Math.Round(Kl, 4),
                                ThanhTienKeHoach= (long)Math.Round(TT),
                                ThanhTienThiCong= (long)Math.Round(TT),
                                LoaiCT= m_KH_TC,
                                NgayBD= dE_Begin.DateTime,
                                NgayKT=dE_End.DateTime,
                                CodeCT=crCodeCT,
                                IsVatLieu=false
                            });
                        }
                    }
                }
            }
            else
            {
                dbString = $"SELECT * FROM {TDKH.TBL_KHVT_VatTu} WHERE \"CodeGiaiDoan\"='{SharedControls.cbb_DBKH_ChonDot.SelectedValue}' AND {Detail.ColCodeFK}='{Detail.Code}' AND \"LoaiVatTu\"='{cBE_LoaiCP.Text}'";
                DataTable VL = DataProvider.InstanceTHDA.ExecuteQuery(dbString);
                string [] lsVL =VL.AsEnumerable().Select(x => x["Code"].ToString()).ToArray();
                var dt_TheoNgay = MyFunction.Fcn_CalKLKHNew(Common.Enums.TypeKLHN.VatLieu, lsVL, dE_Begin.DateTime, dE_End.DateTime);
                foreach (DataRow CT in dstCT.CopyToDataTable().Rows)
                {
                    string crCodeCT = CT["Code"].ToString();
                    ThuChiTDKH.Add(new TCTU_TDKH()
                    {
                        ParentID = "0",
                        ID = crCodeCT,
                        MaHieu = MyConstant.CONST_TYPE_CONGTRINH,
                        TenCongViec = CT["Ten"].ToString(),
                    });
                    foreach (var HM in dtHM.Select($"[CodeCongTrinh] = '{crCodeCT}'"))
                    {
                        string crCodeHM = HM["Code"].ToString();
                        ThuChiTDKH.Add(new TCTU_TDKH()
                        {
                            ParentID = crCodeCT,
                            ID = crCodeHM,
                            MaHieu = MyConstant.CONST_TYPE_HANGMUC,
                            TenCongViec = HM["Ten"].ToString(),
                        });
                        DataRow[] dt_ct = VL.AsEnumerable().Where(x => x["CodeHangMuc"].ToString() == crCodeHM).ToArray();
                        foreach (var rowct in dt_ct)
                        {
                            double Kl = 0;
                            double TT = 0;
                            var crRow = dt_TheoNgay.Where(x => x.ParentCode == rowct["Code"].ToString()).ToArray();
                            if (m_KH_TC == "Thi Công")
                            {
                                Kl = crRow.Any() ? (double)crRow.Sum(x => x.KhoiLuongThiCong) : 0;
                                TT = (long)Math.Round(crRow.Sum(x => x.ThanhTienThiCong) ?? 0);
                            }
                            else
                            {
                                Kl = crRow.Any() ? (double)crRow.Sum(x => x.KhoiLuongKeHoach) : 0;
                                TT = crRow.Any() ? (long)crRow.Sum(x => x.ThanhTienKeHoach) : 0;
                            }

                            ThuChiTDKH.Add(new TCTU_TDKH
                            {
                                STT = stt++,
                                ParentID = crCodeHM,
                                ID = rowct["Code"].ToString(),
                                MaHieu = rowct["MaVatLieu"].ToString(),
                                TenCongViec = rowct["VatTu"].ToString(),
                                DonVi = rowct["DonVi"].ToString(),
                                KhoiLuongKeHoach = Math.Round(Kl, 4),
                                KhoiLuongThiCong = Math.Round(Kl, 4),
                                ThanhTienKeHoach = (long)Math.Round(TT),
                                ThanhTienThiCong = (long)Math.Round(TT),
                                LoaiCT = m_KH_TC,
                                NgayBD = dE_Begin.DateTime,
                                NgayKT = dE_End.DateTime,
                                CodeCT=crCodeCT,
                                IsVatLieu=true
                            });
                        }
                    }
                }
            }
            tL_KHVT.DataSource = ThuChiTDKH;
            tL_KHVT.RefreshDataSource();
            tL_KHVT.ExpandAll();
            tL_KHVT.Refresh();
            WaitFormHelper.CloseWaitForm();
        }
        private void Form_ThuChiTamUngTDKH_Load(object sender, EventArgs e)
        {
            WaitFormHelper.ShowWaitForm("Đang phân tích dữ liệu, Vui lòng chờ!");
            dE_Begin.EditValueChanged -= dE_Begin_EditValueChanged;
            dE_End.EditValueChanged -= dE_End_EditValueChanged;
            dE_Begin.DateTime = dE_End.DateTime = DateTime.Now;
            dE_Begin.EditValueChanged += dE_Begin_EditValueChanged;
            dE_End.EditValueChanged += dE_End_EditValueChanged;
            lUE_ToChucCaNhan.EditValueChanged -= lUE_ToChucCaNhan_EditValueChanged;
            lUE_CongTrinh.EditValueChanged -= lUE_CongTrinh_EditValueChanged;
            cBE_LoaiCP.SelectedIndexChanged -= cBE_LoaiCP_SelectedIndexChanged;
            List<InforCT_HM> Infor = MyFunction.InforHMCT(SharedControls.slke_ThongTinDuAn.Properties.DataSource, false,true);
            lUE_CongTrinh.Properties.DataSource = Infor;
            lUE_CongTrinh.EditValue = Infor.FirstOrDefault().ID;
            lUE_ToChucCaNhan.Properties.DataSource = DuAnHelper.GetCaNhanToChuc(false,false);
            lUE_ToChucCaNhan.EditValue = DuAnHelper.GetCaNhanToChuc(false, false).FirstOrDefault().Code;
            cBE_LoaiCP.SelectedIndex = 0;
            foreach (string item in Dic_KL.Keys)
            {
                if (item != m_KH_TC)
                    tL_KHVT.Columns[Dic_KL[item]].Visible = false;
            }      
            foreach(string item in Dic_TT.Keys)
            {
                if (item != m_KH_TC)
                    tL_KHVT.Columns[Dic_TT[item]].Visible = false;
            }
            Fcn_LayData(cBE_LoaiCP.Text);
            lUE_CongTrinh.EditValueChanged += lUE_CongTrinh_EditValueChanged;
            lUE_ToChucCaNhan.EditValueChanged += lUE_ToChucCaNhan_EditValueChanged;
            cBE_LoaiCP.SelectedIndexChanged += cBE_LoaiCP_SelectedIndexChanged;
            WaitFormHelper.CloseWaitForm();
            //tL_KHVT.ExpandAll();
        }

        private void sB_Update_Click(object sender, EventArgs e)
        {
            Fcn_LayData(cBE_LoaiCP.Text);
            MessageShower.ShowInformation("Hoàn thành!", "");
            tL_KHVT.Refresh();
            //tL_KHVT.ExpandAll();
        }

        private void tL_KHVT_BeforeFocusNode(object sender, DevExpress.XtraTreeList.BeforeFocusNodeEventArgs e)
        {
            if (e.Node.Level < 2)
                e.CanFocus = false;
        }

        private void tL_KHVT_ShowingEditor(object sender, CancelEventArgs e)
        {
            if (tL_KHVT.FocusedColumn.FieldName != "Chon")
                e.Cancel = true;
        }

        private void tL_KHVT_NodeCellStyle(object sender, DevExpress.XtraTreeList.GetCustomNodeCellStyleEventArgs e)
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

        private void tL_KHVT_CustomDrawNodeCell(object sender, DevExpress.XtraTreeList.CustomDrawNodeCellEventArgs e)
        {
            DevExpress.XtraTreeList.TreeList treeList = sender as DevExpress.XtraTreeList.TreeList;
            if (treeList.FocusedColumn == e.Column && treeList.FocusedNode == e.Node)
                return;
            if (object.Equals(e.CellValue, (double)0) || (object.Equals(e.CellValue, false) && (e.Node.Level < 2)))
            {
                e.Appearance.FillRectangle(e.Cache, e.Bounds);
                e.Handled = true;
            }
        }

        private void sB_Cancel_Click(object sender, EventArgs e)
        {
            TreeListNode[] lst = tL_KHVT.FindNodes(x => x.Level == 2);
            if (lst.Count() == 0)
                return;
            foreach (TreeListNode item in lst)
                item.SetValue("Chon", false);
        }

        private void sB_All_Click(object sender, EventArgs e)
        {
            TreeListNode[] lst = tL_KHVT.FindNodes(x => x.Level == 2);
            if (lst.Count() == 0)
                return;
            foreach (TreeListNode item in lst)
                item.SetValue("Chon", true);
        }

        private void tL_KHVT_BeforeExpand(object sender, DevExpress.XtraTreeList.BeforeExpandEventArgs e)
        {
            //if (e.Node.Level == 0)
            //{
            //    List<TreeListNode> lst = tL_KHVT.Nodes.AsEnumerable().Where(x => x.Level == 0).ToList();
            //    foreach (TreeListNode item in lst)
            //    {
            //        if (item.Expanded)
            //        {
            //            MessageShower.ShowInformation("Chỉ chọn được 1 công trình!", "", MessageBoxButtons.OK);
            //            TreeListNodes Child = item.Nodes;
            //            foreach (TreeListNode cr in Child)
            //            {
            //                foreach (TreeListNode crow in cr.Nodes)
            //                    crow.SetValue("Chon", false);
            //            }
            //            item.Collapse();
            //            //e.CanExpand = false;
            //        }
            //    }
            //}

        }

        private void sB_Ok_Click(object sender, EventArgs e)
        {
            List<TCTU_TDKH> ThuChiTDKH = (tL_KHVT.DataSource as List<TCTU_TDKH>).FindAll(x => x.Chon && x.MaHieu != "HM" && x.MaHieu != "CTR");
            if (ThuChiTDKH.Count() == 0)
            {
                MessageShower.ShowWarning("Vui lòng chọn chi phí muốn thu chi");
                return;
            }
            _mcheckclosed = true;
            string tenghep = "";
            string TenNoiDungUng = XtraInputBox.Show("Nhập tên nội dung ứng: ", "", "Dùng tên mặc định");
            if(TenNoiDungUng==""||TenNoiDungUng== "Dùng tên mặc định")
            {
                foreach (TCTU_TDKH item in ThuChiTDKH)
                    tenghep += $", {item.TenCongViec}";
                TenNoiDungUng = tenghep.Remove(0, 1);
            }
            string ColCode = cBE_LoaiCP.Text == "Công tác" ? "CodeCongTac" : "CodeVatTu";
            m__TRUYENDATA(ThuChiTDKH, ColCode,TenNoiDungUng,Dic_LoaiCP[cBE_LoaiCP.Text],lUE_ToChucCaNhan.EditValue.ToString());
            this.Close();
        }

        private void lUE_ToChucCaNhan_EditValueChanged(object sender, EventArgs e)
        {
            Fcn_LayData(cBE_LoaiCP.Text);
        }

        private void cBE_LoaiCP_SelectedIndexChanged(object sender, EventArgs e)
        {
            Fcn_LayData(cBE_LoaiCP.Text);
        }

        private void lUE_CongTrinh_EditValueChanged(object sender, EventArgs e)
        {
            Fcn_LayData(cBE_LoaiCP.Text);
        }

        private void dE_Begin_EditValueChanged(object sender, EventArgs e)
        {
            Fcn_LayData(cBE_LoaiCP.Text);
        }

        private void dE_End_EditValueChanged(object sender, EventArgs e)
        {
            Fcn_LayData(cBE_LoaiCP.Text);
        }

        private void Form_ThuChiTamUngTDKH_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (_mcheckclosed)
                return;
            m__TRUYENDATA(null, "", "", "","");
        }
    }
}
