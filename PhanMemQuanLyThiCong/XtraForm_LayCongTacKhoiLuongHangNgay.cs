using DevExpress.XtraEditors;
using PhanMemQuanLyThiCong.Common.Constant;
using PhanMemQuanLyThiCong.Common.Helper;
using PhanMemQuanLyThiCong.Model.TDKH;
using StackExchange.Profiling.Internal;
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
using System.Xml;
using System.Xml.Serialization;

namespace PhanMemQuanLyThiCong
{
    public partial class XtraForm_LayCongTacKhoiLuongHangNgay : DevExpress.XtraEditors.XtraForm
    {
        public XtraForm_LayCongTacKhoiLuongHangNgay()
        {
            InitializeComponent();
        }
        public void Fcn_LoadData(string ConditionCode=null,DateTime?NBD=null,DateTime?NKT=null)
        {
            WaitFormHelper.ShowWaitForm("Đang tải dữ liệu!! Vui lòng chờ!!!");
            var crDVTH = SharedControls.ctrl_DonViThucHienDuAnTDKH.SelectedDVTH;
            TDKHHelper.GetCodeCongTrinhHangMuc(out string codeHM, out string codeCT);

            //DataTable CongTacs = TDKHHelper.GetCongTacsDataTable(SharedControls.slke_ThongTinDuAn.EditValue?.ToString(),
            //    codeCT, codeHM, SharedControls.cbb_DBKH_ChonDot.SelectedValue.ToString(), $"cttk.{crDVTH.ColCodeFK} = '{crDVTH.Code}'", GetAllHM: false);

            string dbString = $"SELECT dmct.DonVi, cttk.KhoiLuongToanBo/COALESCE(KhoiLuongToanBo, 1) AS KhoiLuongToanBo, dmct.CodePhanTuyen, pt.Ten AS TenPhanTuyen,\r\n" +
                   $"cttk.KhoiLuongHopDongChiTiet AS KhoiLuongHopDong,dmct.TenCongTac, " +
                           $"hm.Ten AS TenHangMuc, " +
                           $"hm.Code AS CodeHangMuc, " +
                           $"ctr.Ten AS TenCongTrinh, " +
                           $"ctr.Code AS CodeCongTrinh, " +
                           $"dmct.KhoiLuongHopDongToanDuAn, " +
                           //$"dmct.DonVi, " +
                           $"dmct.MaHieuCongTac,cttk.* " +
                           $"FROM {TDKH.TBL_ChiTietCongTacTheoKy} cttk\r\n" +
                           $"JOIN {TDKH.TBL_DanhMucCongTac} dmct\r\n" +
                           $"ON cttk.CodeCongTac = dmct.Code " +
                           $"JOIN {MyConstant.TBL_THONGTINHANGMUC} hm\r\n" +
                           $"ON dmct.CodeHangMuc = hm.Code " +
                           $"JOIN {MyConstant.TBL_THONGTINCONGTRINH} ctr\r\n" +
                           $"ON hm.CodeCongTrinh = ctr.Code " +
                           $"LEFT JOIN {TDKH.Tbl_PhanTuyen} pt\r\n" +
                           $"ON dmct.CodePhanTuyen = pt.Code\r\n" +
                           $"WHERE \"CodeGiaiDoan\" = '{SharedControls.cbb_DBKH_ChonDot.SelectedValue}' " +
                           ((codeCT.HasValue()) ? $"AND ctr.Code = '{codeCT}'\r\n" : "") +
                           ((codeHM.HasValue()) ? $"AND hm.Code = '{codeHM}'\r\n" : "") +
                            ((ConditionCode.HasValue()) ? $"AND cttk.Code   NOT IN ({ConditionCode}) \r\n" : "") +
                           $"AND CodeDuAn = '{SharedControls.slke_ThongTinDuAn.EditValue}' " +
                           $"AND (cttk.NgayBatDau>'{NKT.Value.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE)}' OR cttk.NgayKetThuc<'{NKT.Value.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE)}') " +
                           $"AND cttk.{crDVTH.ColCodeFK} = '{crDVTH.Code}' AND cttk.CodeCha IS NULL " +
                           $"ORDER BY cttk.SortId ASC";
            DataTable CongTacs = DataProvider.InstanceTHDA.ExecuteQuery(dbString);
            CongTacs.AddIndPhanTuyenNhom();
            DataTable CongTacDaLay = null;
            if (ConditionCode.HasValue())
            {
                dbString = $"SELECT dmct.DonVi, cttk.KhoiLuongToanBo/COALESCE(KhoiLuongToanBo, 1) AS KhoiLuongToanBo, dmct.CodePhanTuyen, pt.Ten AS TenPhanTuyen,\r\n" +
                 $"cttk.KhoiLuongHopDongChiTiet AS KhoiLuongHopDong,dmct.TenCongTac," +
                         $"hm.Ten AS TenHangMuc, " +
                         $"hm.Code AS CodeHangMuc, " +
                         $"ctr.Ten AS TenCongTrinh, " +
                         $"ctr.Code AS CodeCongTrinh, " +
                         $"dmct.KhoiLuongHopDongToanDuAn, " +
                         //$"dmct.DonVi, " +
                         $"dmct.MaHieuCongTac,cttk.* " +
                         $"FROM {TDKH.TBL_ChiTietCongTacTheoKy} cttk\r\n" +
                         $"JOIN {TDKH.TBL_DanhMucCongTac} dmct\r\n" +
                         $"ON cttk.CodeCongTac = dmct.Code " +
                         $"JOIN {MyConstant.TBL_THONGTINHANGMUC} hm\r\n" +
                         $"ON dmct.CodeHangMuc = hm.Code " +
                         $"JOIN {MyConstant.TBL_THONGTINCONGTRINH} ctr\r\n" +
                         $"ON hm.CodeCongTrinh = ctr.Code " +
                         $"LEFT JOIN {TDKH.Tbl_PhanTuyen} pt\r\n" +
                         $"ON dmct.CodePhanTuyen = pt.Code\r\n" +
                         $"WHERE \"CodeGiaiDoan\" = '{SharedControls.cbb_DBKH_ChonDot.SelectedValue}' " +
                         ((codeCT.HasValue()) ? $"AND ctr.Code = '{codeCT}'\r\n" : "") +
                         ((codeHM.HasValue()) ? $"AND hm.Code = '{codeHM}'\r\n" : "") +
                          ((ConditionCode.HasValue()) ? $"AND cttk.Code  IN ({ConditionCode}) \r\n" : "") +
                         $"AND CodeDuAn = '{SharedControls.slke_ThongTinDuAn.EditValue}' " +
                         $"AND cttk.{crDVTH.ColCodeFK} = '{crDVTH.Code}' AND cttk.CodeCha IS NULL " +
                         $"ORDER BY cttk.SortId ASC";
                CongTacDaLay = DataProvider.InstanceTHDA.ExecuteQuery(dbString);
                CongTacDaLay.AddIndPhanTuyenNhom();
            }          

            var grsCTrinh = CongTacs.AsEnumerable().GroupBy(x => x["CodeCongTrinh"]);
            List<LayCongTac> ChiaCTDaLay = new List<LayCongTac>();
            List<LayCongTac> ChiaCTChuaLay = new List<LayCongTac>();
            foreach (var Ctrinh in grsCTrinh)
            {
                LayCongTac CtrinhItem = new LayCongTac();
                CtrinhItem.Code = Ctrinh.Key.ToString();
                CtrinhItem.MaHieuCongTac = MyConstant.CONST_TYPE_CONGTRINH;
                CtrinhItem.TenCongTac = $"{Ctrinh.FirstOrDefault()["TenCongTrinh"]}".ToUpper();
                ChiaCTDaLay.Add(CtrinhItem);
                ChiaCTChuaLay.Add(CtrinhItem);
                var grsHM = Ctrinh.GroupBy(x => x["CodeHangMuc"]);
                foreach (var HM in grsHM)
                {
                    LayCongTac HMItem = new LayCongTac();
                    HMItem.Code = HM.Key.ToString();
                    HMItem.ParentId = Ctrinh.Key.ToString();
                    HMItem.MaHieuCongTac = MyConstant.CONST_TYPE_HANGMUC;
                    HMItem.TenCongTac = $"{HM.FirstOrDefault()["TenHangMuc"]}".ToUpper();
                    ChiaCTDaLay.Add(HMItem);
                    ChiaCTChuaLay.Add(HMItem);
                    if (CongTacDaLay!=null)
                    {
                        DataRow [] RowHM = CongTacDaLay.AsEnumerable().Where(x => x["CodeHangMuc"].ToString() == HM.Key.ToString()).ToArray();
                        var grPhanTuyenNew=RowHM.AsEnumerable().GroupBy(x=> (int)x["IndPT"]).OrderBy(x => x.Key);
                        foreach (var grPhanTuyen in grPhanTuyenNew)
                        {
                            var fstTuyen = grPhanTuyen.First();
                            string crCodeTuyen = (fstTuyen["CodePhanTuyen"] == DBNull.Value) ? null : $"{fstTuyen["CodePhanTuyen"]}";
                            string codePT = fstTuyen["CodePhanTuyen"].ToString();
                            bool isPhanTuyen = codePT.HasValue();
                            if (isPhanTuyen)
                            {
                                LayCongTac TuyenItem = new LayCongTac();
                                TuyenItem.Code = codePT;
                                TuyenItem.ParentId = HM.Key.ToString();
                                TuyenItem.MaHieuCongTac = MyConstant.CONST_TYPE_PhanTuyen;
                                TuyenItem.TenCongTac = $"{fstTuyen["TenPhanTuyen"]}".ToUpper();
                                ChiaCTDaLay.Add(TuyenItem);
                                //ChiaCTChuaLay.Add(TuyenItem);
                            }
                            var grsNhom = grPhanTuyen.GroupBy(x => (int)x["IndNhom"]).OrderBy(x => x.Key);
                            foreach (var grNhom in grsNhom)
                            {
                                foreach (var ctac in grNhom)
                                {
                                    LayCongTac CtacItem = new LayCongTac();
                                    CtacItem.Code = ctac["Code"].ToString();
                                    CtacItem.ParentId = isPhanTuyen ? codePT : HM.Key.ToString();
                                    CtacItem.TenCongTac = ctac["TenCongTac"].ToString();
                                    CtacItem.MaHieuCongTac = ctac["MaHieuCongTac"].ToString();
                                    CtacItem.DonVi = ctac["DonVi"].ToString();
                                    CtacItem.LoaiCT = 1;
                                    ChiaCTDaLay.Add(CtacItem);
                                    //ChiaCTChuaLay.Add(CtacItem);
                                }
                            }
                        }

                    }
                    var grsPhanTuyen = HM.GroupBy(x => (int)x["IndPT"]).OrderBy(x => x.Key);
                    foreach (var grPhanTuyen in grsPhanTuyen)
                    {
                        var fstTuyen = grPhanTuyen.First();
                        string crCodeTuyen = (fstTuyen["CodePhanTuyen"] == DBNull.Value) ? null : $"{fstTuyen["CodePhanTuyen"]}";
                        string codePT = fstTuyen["CodePhanTuyen"].ToString();
                        bool isPhanTuyen = codePT.HasValue();
                        if (isPhanTuyen)
                        {
                            LayCongTac TuyenItem = new LayCongTac();
                            TuyenItem.Code = codePT;
                            TuyenItem.ParentId = HM.Key.ToString();
                            TuyenItem.MaHieuCongTac = MyConstant.CONST_TYPE_PhanTuyen;
                            TuyenItem.TenCongTac = $"{fstTuyen["TenPhanTuyen"]}".ToUpper();
                            //ChiaCTDaLay.Add(TuyenItem);
                            ChiaCTChuaLay.Add(TuyenItem);
                        }
                        var grsNhom = grPhanTuyen.GroupBy(x => (int)x["IndNhom"]).OrderBy(x => x.Key);
                        foreach (var grNhom in grsNhom)
                        {
                            foreach (var ctac in grNhom)
                            {
                                LayCongTac CtacItem = new LayCongTac();
                                CtacItem.Code = ctac["Code"].ToString();
                                CtacItem.ParentId = isPhanTuyen ? codePT : HM.Key.ToString();
                                CtacItem.TenCongTac = ctac["TenCongTac"].ToString();
                                CtacItem.MaHieuCongTac = ctac["MaHieuCongTac"].ToString();
                                CtacItem.DonVi = ctac["DonVi"].ToString();
                                CtacItem.LoaiCT = 1;
                                //ChiaCTDaLay.Add(CtacItem);
                                ChiaCTChuaLay.Add(CtacItem);
                            }
                        }
                    }
                }
            }



            tl_CongTacChuaLay.DataSource = ChiaCTChuaLay;
            tl_CongTacChuaLay.RefreshDataSource();
            tl_CongTacChuaLay.ExpandAll();

            tl_CongTacDaLay.DataSource = ChiaCTDaLay;
            tl_CongTacDaLay.RefreshDataSource();
            tl_CongTacDaLay.ExpandAll();
            WaitFormHelper.CloseWaitForm();
        }

        private void tl_CongTacChuaLay_NodeCellStyle(object sender, DevExpress.XtraTreeList.GetCustomNodeCellStyleEventArgs e)
        {
            if (e.Node.Level == 0)
            {
                e.Appearance.ForeColor = MyConstant.color_Row_CongTrinh;
                e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold);
            }
            else if (e.Node.Level == 1)
            {
                e.Appearance.ForeColor = MyConstant.color_Row_HangMuc;
                e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold);
            }
            else if (e.Node.Level == 2)
            {
                e.Appearance.ForeColor = MyConstant.color_Row_PhanTuyen;
                e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold);
            }
        }

        private void bt_OK_Click(object sender, EventArgs e)
        {
            List<LayCongTac> lst = tl_CongTacChuaLay.DataSource as List<LayCongTac>;
            List<LayCongTac> lstChuaLay = lst.Where(x => x.Chon == true && x.LoaiCT == 1).ToList();
            if (!lstChuaLay.Any())
            {
                MessageShower.ShowInformation("Chưa có công tác nào được chọn!!!");
                return;
            }

            var crDVTH = SharedControls.ctrl_DonViThucHienDuAnTDKH.SelectedDVTH;
            string IdNguoiDung = BaseFrom.BanQuyenKeyInfo.SerialNo;
            KHHN_CongTacChonNgoaiKeHoach ListNguoiDung = new KHHN_CongTacChonNgoaiKeHoach();
            ListNguoiDung.IdNguoiDung = IdNguoiDung;
            ListNguoiDung.CodeDVTH = crDVTH.Code;
            ListNguoiDung.CodeDuAn = SharedControls.slke_ThongTinDuAn.EditValue.ToString();
            ListNguoiDung.CodeCongTacTheoGiaiDoan = lstChuaLay.Select(x => x.Code).ToList();


            XmlSerializer serializer = new XmlSerializer(typeof(List<KHHN_CongTacChonNgoaiKeHoach>), new XmlRootAttribute("CongTacChonNgoaiKeHoach"));
            XmlSerializer deserializer = new XmlSerializer(typeof(List<KHHN_CongTacChonNgoaiKeHoach>), new XmlRootAttribute("CongTacChonNgoaiKeHoach"));
            //List<KHHN_CongTacChonNgoaiKeHoach> LstTong = new List<KHHN_CongTacChonNgoaiKeHoach>();
            var xml = Path.Combine(BaseFrom.m_FullTempathDA, "DuLieuTam.xml");
            List<KHHN_CongTacChonNgoaiKeHoach> LstTong = new List<KHHN_CongTacChonNgoaiKeHoach>();
            var Reader = new StreamReader(xml);
            if (Reader.BaseStream.Length > 0)
            {
                LstTong = deserializer.Deserialize(Reader) as List<KHHN_CongTacChonNgoaiKeHoach>;
                KHHN_CongTacChonNgoaiKeHoach Idnd = LstTong.Where(x => x.IdNguoiDung == BaseFrom.BanQuyenKeyInfo.SerialNo
                 && x.CodeDVTH == crDVTH.Code && x.CodeDuAn == SharedControls.slke_ThongTinDuAn.EditValue.ToString()).Any() ?
                     LstTong.Where(x => x.IdNguoiDung == BaseFrom.BanQuyenKeyInfo.SerialNo).FirstOrDefault() : null;
                if (Idnd != null)
                {
                    Idnd.CodeCongTacTheoGiaiDoan.AddRange(ListNguoiDung.CodeCongTacTheoGiaiDoan);
                    Idnd.CodeCongTacTheoGiaiDoan.Select(x => x).Distinct();
                }
                else
                    LstTong.Add(ListNguoiDung);
            }
            else
                LstTong.Add(ListNguoiDung);
            Reader.Close();
            Reader.Dispose();
            //var writer = new System.IO.StreamWriter(Path.Combine(BaseFrom.m_templatePath, "DuAnMau", "DuLieuTam.xml"));
            //FileStream fs = new FileStream(Path.Combine(BaseFrom.m_FullTempathDA, "DuLieuTam.xml"), FileMode.Open, FileAccess.ReadWrite);
            var writer = new StreamWriter(xml);
            serializer.Serialize(writer, LstTong);
            writer.Close();
            writer.Dispose();
            //fs.Dispose();
            //var xml = Path.Combine(BaseFrom.m_FullTempathDA, "DuLieuTam.xml");
            //giải mã
            //var writer = new StreamWriter(xml);
            //Reader = new StreamReader(xml);
            //Test = deserializer.Deserialize(Reader) as List<KHHN_CongTacChonNgoaiKeHoach>;
            //Reader.Close();
            this.Close();
        }

        private void XtraForm_LayCongTacKhoiLuongHangNgay_Load(object sender, EventArgs e)
        {
            //Fcn_LoadData();
        }

        private void bt_cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_XoaCongTacLay_Click(object sender, EventArgs e)
        {
            List<LayCongTac> lst = tl_CongTacDaLay.DataSource as List<LayCongTac>;
            List<LayCongTac> lstDaLay = lst.Where(x => x.Chon == true && x.LoaiCT == 1).ToList();
            if (!lstDaLay.Any())
            {
                MessageShower.ShowInformation("Chưa có công tác nào được chọn!!!");
                return;
            }
            XmlSerializer deserializer = new XmlSerializer(typeof(List<KHHN_CongTacChonNgoaiKeHoach>), new XmlRootAttribute("CongTacChonNgoaiKeHoach"));
            var xml = Path.Combine(BaseFrom.m_FullTempathDA, "DuLieuTam.xml");
            List<KHHN_CongTacChonNgoaiKeHoach> LstTong = new List<KHHN_CongTacChonNgoaiKeHoach>();
            var Reader = new StreamReader(xml);
            var crDVTH = SharedControls.ctrl_DonViThucHienDuAnTDKH.SelectedDVTH;
            if (Reader.BaseStream.Length > 0)
            {
                LstTong = deserializer.Deserialize(Reader) as List<KHHN_CongTacChonNgoaiKeHoach>;
                KHHN_CongTacChonNgoaiKeHoach Idnd = LstTong.Where(x => x.IdNguoiDung == BaseFrom.BanQuyenKeyInfo.SerialNo
                 && x.CodeDVTH == crDVTH.Code && x.CodeDuAn == SharedControls.slke_ThongTinDuAn.EditValue.ToString()).Any() ?
                     LstTong.Where(x => x.IdNguoiDung == BaseFrom.BanQuyenKeyInfo.SerialNo).FirstOrDefault() : null;
                if (Idnd != null)
                {
                    string[] LstCode = lstDaLay.Select(x => x.Code).ToArray();
                    Idnd.CodeCongTacTheoGiaiDoan.RemoveAll(x => LstCode.Contains(x));
                    
                }

            }
            Reader.Close();
            Reader.Dispose();
            XmlSerializer serializer = new XmlSerializer(typeof(List<KHHN_CongTacChonNgoaiKeHoach>), new XmlRootAttribute("CongTacChonNgoaiKeHoach"));
            var writer = new StreamWriter(xml);
            serializer.Serialize(writer, LstTong);
            writer.Close();
            writer.Dispose();
            this.Close();
        }
    }
}