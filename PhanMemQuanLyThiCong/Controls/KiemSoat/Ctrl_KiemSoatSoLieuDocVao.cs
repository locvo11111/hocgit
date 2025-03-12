using DevExpress.Spreadsheet;
using DevExpress.XtraEditors;
using PhanMemQuanLyThiCong.Common.Constant;
using PhanMemQuanLyThiCong.Common.Helper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PhanMemQuanLyThiCong.Controls.KiemSoat
{
    public partial class Ctrl_KiemSoatSoLieuDocVao : DevExpress.XtraEditors.XtraUserControl
    {
        public DataTable _dtDuLieuGoc { get; set; }
        public Ctrl_KiemSoatSoLieuDocVao()
        {
            InitializeComponent();
        }
        public void Fcn_LoadData()
        {
            WaitFormHelper.ShowWaitForm($"Đang phân tích dữ liệu, Vui lòng chờ!!!!!!!");
            string dbString = $"SELECT " +
                    $"hm.Code AS CodeHangMuc,hm.Ten AS TenHangMuc," +
                    $"ctrinh.Code AS CodeCongTrinh,ctrinh.Ten AS TenCongTrinh," +
                    $"COALESCE(cttk.DonVi, dmct.DonVi) AS DonVi,COALESCE(cttk.TenCongTac, dmct.TenCongTac) AS TenCongTac," +
                    $"COALESCE(cttk.MaHieuCongTac, dmct.MaHieuCongTac) AS MaHieuCongTac,hp.Code as CodeHP,hp.DonGia as DonGiaVatLieu," +
                    $"hp.MaVatLieu,hp.VatTu,hp.DonVi as DonViHP,hp.DinhMuc,hp.LoaiVatTu,hp.HeSo as HeSoVatTu,cttk.KhoiLuongToanBo,cttk.DonGia, \r\n" +
                    $"ROUND(cttk.KhoiLuongToanBo*hp.HeSoNguoiDung*hp.DinhMucNguoiDung, 2) AS KhoiLuongKeHoach," +
                    $"hm.CodeCongTrinh, \r\n" +
                    $"ctrinh.CodeDuAn AS CodeDuAn, \r\n" +
                    $"ctrinh.SortId AS SortIdCtrinh,hm.SortId AS SortIdHM,cttk.Code \r\n" +
                    $"FROM {TDKH.TBL_ChiTietCongTacTheoKy} cttk\r\n" +
                    $"LEFT JOIN {TDKH.TBL_DanhMucCongTac} dmct\r\n" +
                    $"ON cttk.CodeCongTac = dmct.Code \r\n" +
                    $"LEFT JOIN {TDKH.Tbl_HaoPhiVatTu} hp\r\n" +
                    $"ON hp.CodeCongTac = cttk.Code \r\n" +
                    $"LEFT JOIN {MyConstant.TBL_THONGTINHANGMUC} hm\r\n" +
                    $"ON dmct.CodeHangMuc = hm.Code \r\n" +
                    $"LEFT JOIN {MyConstant.TBL_THONGTINCONGTRINH} ctrinh\r\n" +
                    $"ON hm.CodeCongTrinh = ctrinh.Code \r\n" +
                    $"WHERE ctrinh.CodeDuAn = '{SharedControls.slke_ThongTinDuAn.EditValue}'\r\n" +
                    $"ORDER BY SortIdCtrinh ASC,SortIdHM ASC, cttk.SortId ASC\r\n";
            DataTable dtCongTacTheoKy = DataProvider.InstanceTHDA.ExecuteQuery(dbString);
            if (dtCongTacTheoKy.Rows.Count == 0)
            {
                WaitFormHelper.CloseWaitForm();
                return;
            }
            Worksheet ws = spread_ThamDinh.Document.Worksheets[0];
            ws.Rows.Remove(2, ws.GetUsedRange().BottomRowIndex);
            List<DefinedName> lstWS=ws.DefinedNames.Where(x => x.Name != "Data_0").ToList();
            lstWS.ForEach(x => ws.DefinedNames.Remove(x));
            var grCtrinh = dtCongTacTheoKy.AsEnumerable().GroupBy(x => x["CodeCongTrinh"].ToString());
            CellRange RangeMau = spread_ThamDinh.Document.Worksheets["Mau"].Range["Data_Mau"];
            Row CongTac = spread_ThamDinh.Document.Worksheets["Mau"].Rows[6];
            Row VatLieu = spread_ThamDinh.Document.Worksheets["Mau"].Rows[7];
            Row TenVatLieu = spread_ThamDinh.Document.Worksheets["Mau"].Rows[8];
            //dbString = $"SELECT hp.MaDinhMuc,hp.MaVatLieu,hp.DinhMuc,hp.LoaiVatTu,hp.HeSo,VT.VatTu,VT.DonVi as DonViVatTu," +
            //    $"CT.TenDinhMuc,CT.DonVi as DonViCongTac FROM {MyConstant.TBL_HaoPhiVatTu} hp " +
            //    $"LEFT JOIN  {MyConstant.TBL_TBT_DINHMUCALL} CT ON CT.MaDinhMuc=hp.MaDinhMuc " +
            //    $"LEFT JOIN  {MyConstant.TBL_TBT_VATTU} VT ON VT.MaVatLieu=hp.MaVatLieu";
            //DataTable dt = DataProvider.InstanceTBT.ExecuteQuery(dbString);
            DataTable dt = _dtDuLieuGoc;
            int CountRange = 0;
            int STT = 1;
            Dictionary<string, string> Name = MyFunction.fcn_getDicOfColumn(RangeMau);
            List<string> lstMaVLGoc = new List<string>();
            List<string> lstMaVLDistinct = new List<string>();
            /////Phần hao phí vật tư
            /////
            //Worksheet wshP = spread_ThamDinh.Document.Worksheets["Hao phí vật tư"];
            //wshP.Rows.Remove(3, wshP.GetUsedRange().BottomRowIndex);
            //List<DefinedName> lstWShp = wshP.DefinedNames.Where(x => x.Name != "HaoPhi_0").ToList();
            //lstWShp.ForEach(x => wshP.DefinedNames.Remove(x));
            //CellRange RangeMauCongTrinh = spread_ThamDinh.Document.Worksheets["Mau1"].Range["CongTrinh_Mau"];
            //Dictionary<string, string> NameHp = MyFunction.fcn_getDicOfColumn(RangeMauCongTrinh);
            //Row CongTachp = spread_ThamDinh.Document.Worksheets["Mau1"].Rows[6];
            //Row VatLieuhp = spread_ThamDinh.Document.Worksheets["Mau1"].Rows[7];
            //Row TenVatLieuhp = spread_ThamDinh.Document.Worksheets["Mau1"].Rows[8];
            //Row CopyHM = spread_ThamDinh.Document.Worksheets["Mau1"].Rows[9];
            //int CountRangeHp = 0 ;
            //wshP.Rows[2]["A"].SetValueFromText($"Dự án: {SharedControls.slke_ThongTinDuAn.Text}");
            //string Fomular = string.Empty;
            /////End


            spread_ThamDinh.BeginUpdate();
            foreach (var CTrinh in grCtrinh)
            {
                var grHangMuc = CTrinh.GroupBy(x => x["CodeHangMuc"].ToString());
                ////Phần hao phí
                //CellRange RangeHaoPhi = wshP.Range[$"HaoPhi_{CountRangeHp++}"];
                //wshP.Rows[RangeHaoPhi.BottomRowIndex + 3].CopyFrom(RangeMauCongTrinh, PasteSpecial.All);
                //CellRange NewRangeHP = wshP.Range[$"A{RangeHaoPhi.BottomRowIndex + 4}:L{RangeHaoPhi.BottomRowIndex + 4 + RangeMauCongTrinh.RowCount}"];
                //NewRangeHP.Name = $"HaoPhi{CountRangeHp}";
                //wshP.Rows[NewRangeHP.TopRowIndex][0].SetValueFromText($"CÔNG TRÌNH: {CTrinh.FirstOrDefault()["TenCongTrinh"]}");
                //int IndexhP = NewRangeHP.TopRowIndex + 8;
                //wshP.Rows.Hide(IndexhP - 4, IndexhP);
                ////end

                foreach (var Hm in grHangMuc)
                {
                    STT = 1;
                    CellRange RangeData = ws.Range[$"Data_{CountRange++}"];
                    ws.Rows[RangeData.BottomRowIndex + 2].CopyFrom(RangeMau, PasteSpecial.All);
                    CellRange NewRange = ws.Range[$"A{RangeData.BottomRowIndex + 3}:M{RangeData.BottomRowIndex + 3 + RangeMau.RowCount}"];
                    NewRange.Name = $"Data_{CountRange}";
                    ws.Rows[NewRange.TopRowIndex][0].SetValueFromText($"CÔNG TRÌNH: {CTrinh.FirstOrDefault()["TenCongTrinh"]}");
                    ws.Rows[NewRange.TopRowIndex+1][0].SetValueFromText($"HẠNG MỤC: {Hm.FirstOrDefault()["TenHangMuc"]}");
                    var grCongTac = Hm.GroupBy(x => x["Code"]);
                    int Index = NewRange.TopRowIndex +8;
                    ws.Rows.Hide(Index-3,Index);
                    ////Phần hao phí
                    //Row CrowHP = wshP.Rows[IndexhP++];
                    //CrowHP.CopyFrom(CopyHM, PasteSpecial.All);
                    //CrowHP.Visible = true;
                    //CrowHP[0].SetValueFromText($"HẠNG MỤC: {Hm.FirstOrDefault()["TenHangMuc"]}");                    
                    //wshP.Rows.Insert(IndexhP+2, 1, RowFormatMode.FormatAsNext);
                    ////End
                    foreach (var Ctac in grCongTac)
                    {
                        var CtacFirst = Ctac.FirstOrDefault();
                        WaitFormHelper.ShowWaitForm($"{STT}_{CtacFirst["MaHieuCongTac"]}-{CtacFirst["TenCongTac"]}");
                        DataRow [] DtCongTac = dt.AsEnumerable().Where(x => x["MaDinhMuc"].ToString() == CtacFirst["MaHieuCongTac"].ToString()).ToArray();
                        Row Crow = ws.Rows[Index++];
                        ws.Rows.Insert(Index, 1, RowFormatMode.FormatAsNext);
                        Crow.CopyFrom(CongTac, PasteSpecial.All);
                        Crow.Visible = true;
                        Crow[Name["STT"]].SetValue(STT++);
                        Crow[Name["MaHieuCongTac"]].SetValueFromText(CtacFirst["MaHieuCongTac"].ToString());
                        Crow[Name["TenCongTacDocVao"]].SetValueFromText(CtacFirst["TenCongTac"].ToString());
                        Crow[Name["DonViDocVao"]].SetValueFromText(CtacFirst["DonVi"].ToString());
                        Crow[Name["KLDocVao"]].SetValue(CtacFirst["KhoiLuongToanBo"]);
                        Crow[Name["DonGiaDocVao"]].SetValue(CtacFirst["DonGia"]);
                        if (DtCongTac.Any())
                        {
                            Crow[Name["TenCongTacGoc"]].SetValueFromText(DtCongTac.FirstOrDefault()["TenDinhMuc"].ToString());
                            Crow[Name["DonViGoc"]].SetValueFromText(DtCongTac.FirstOrDefault()["DonViCongTac"].ToString());

                        }
                        else
                        {
                            Crow[Name["MaHieuCongTac"]].FillColor = Crow[Name["GhiChu"]].FillColor = Color.Gold;
                            Crow[Name["GhiChu"]].SetValueFromText("Mã hiệu công tác này không tra được trong định mức tham khảo");
                        }
                        ////Phần hao phí
                        //CrowHP = wshP.Rows[IndexhP++];
                        //wshP.Rows.Insert(IndexhP+2, 1, RowFormatMode.FormatAsNext);
                        //CrowHP.CopyFrom(CongTachp, PasteSpecial.All);
                        //CrowHP.Visible = true;
                        //int IndexCongTac = IndexhP;
                        //Fomular = string.Empty;
                        //CrowHP[NameHp["STT"]].SetValue(STT-1);
                        //CrowHP[NameHp["MaHieuCongTac"]].SetValueFromText(CtacFirst["MaHieuCongTac"].ToString());
                        //CrowHP[NameHp["TenCongTac"]].SetValueFromText(CtacFirst["TenCongTac"].ToString());
                        //CrowHP[NameHp["DonVi"]].SetValueFromText(CtacFirst["DonVi"].ToString());
                        //CrowHP[NameHp["KhoiLuongThiCong"]].SetValue(CtacFirst["KhoiLuongToanBo"]);
                        //CrowHP[NameHp["DonGia"]].SetValue(CtacFirst["DonGia"]);
                        ////End

                        var LoaiVatTu = Ctac.GroupBy(x => x["LoaiVatTu"]).OrderByDescending(x=>x.Key);
                        foreach(var LoaiVT in LoaiVatTu)
                        {
                            Crow = ws.Rows[Index++];
                            ws.Rows.Insert(Index, 1, RowFormatMode.FormatAsNext);
                            Crow.CopyFrom(VatLieu, PasteSpecial.All);
                            Crow.Visible = true;
                            Crow[Name["TenCongTacDocVao"]].SetValueFromText(LoaiVT.Key.ToString());
                            var grHaoPhi = LoaiVT.GroupBy(x => x["CodeHP"]);
                            ////Phần hao phí
                            //CrowHP = wshP.Rows[IndexhP++];
                            //CrowHP.CopyFrom(VatLieuhp, PasteSpecial.All);
                            //CrowHP.Visible = true;
                            //int IndexLoaiVL = IndexhP;
                            //wshP.Rows.Insert(IndexhP+2, grHaoPhi.Count()+ 1, RowFormatMode.FormatAsNext);
                            //CrowHP[NameHp["TenCongTac"]].SetValueFromText(LoaiVT.Key.ToString());
                            //CrowHP[NameHp["ThanhTien"]].Formula = $"SUM({NameHp["ThanhTien"]}{IndexhP+1}:{NameHp["ThanhTien"]}{IndexhP+ grHaoPhi.Count()})";
                            //Fomular += $"+{NameHp["ThanhTien"]}{IndexhP}";
                            ////End
                            lstMaVLGoc.Clear();
                            lstMaVLDistinct.Clear();
                            foreach (var HaoPhi in grHaoPhi)
                            {
                                if (string.IsNullOrEmpty(HaoPhi.FirstOrDefault()["VatTu"].ToString()))
                                    continue;
                                Crow = ws.Rows[Index++];
                                ws.Rows.Insert(Index, 1, RowFormatMode.FormatAsNext);
                                Crow.CopyFrom(TenVatLieu, PasteSpecial.All);
                                Crow.Visible = true;
                                var HpFirst = HaoPhi.FirstOrDefault();
                                string DonViVt =MyFunction.ConvertSuperscript(HpFirst["DonViHP"].ToString());
                                Crow[Name["TenCongTacDocVao"]].SetValueFromText(HpFirst["VatTu"].ToString());
                                Crow[Name["DonViDocVao"]].SetValueFromText(HpFirst["DonViHP"].ToString());
                                Crow[Name["MucHaoPhiDocVao"]].SetValue(HpFirst["DinhMuc"]);
                                Crow[Name["KLDocVao"]].SetValue(HpFirst["KhoiLuongKeHoach"]);
                                Crow[Name["DonGiaDocVao"]].SetValue(HpFirst["DonGiaVatLieu"]);
                                Crow[Name["MaHieuCongTac"]].SetValueFromText(HpFirst["MaVatLieu"].ToString());                             
                                DataRow [] DtVatLieu = DtCongTac.Where(x =>double.Parse(x["DinhMuc"].ToString()) ==double.Parse(HpFirst["DinhMuc"].ToString())
                                && MyFunction.ConvertSuperscript(x["DonViVatTu"].ToString()).ToUpper()== DonViVt.ToUpper()
                                && x["LoaiVatTu"].ToString()== LoaiVT.Key.ToString()).ToArray();   
                                foreach(var itemVl in DtVatLieu)
                                {
                                    if (lstMaVLDistinct.Contains(itemVl["MaVatLieu"].ToString()))
                                        continue;
                                    lstMaVLGoc.Add(itemVl["MaVatLieu"].ToString());
                                    lstMaVLDistinct.Add(itemVl["MaVatLieu"].ToString());
                                    Crow[Name["TenCongTacGoc"]].SetValueFromText(itemVl["VatTu"].ToString());
                                    Crow[Name["DonViGoc"]].SetValueFromText(itemVl["DonViVatTu"].ToString());
                                    Crow[Name["MucHaoPhiGoc"]].SetValue(itemVl["DinhMuc"]);
                                    break;
                                }
                                //if (DtVatLieu.Any())
                                //{
                                //    var VlMauFirst = DtVatLieu.FirstOrDefault();
                                //    lstMaVLGoc.Add(VlMauFirst["MaVatLieu"].ToString());
                                //    Crow[Name["TenCongTacGoc"]].SetValueFromText(VlMauFirst["VatTu"].ToString());
                                //    Crow[Name["DonViGoc"]].SetValueFromText(VlMauFirst["DonViVatTu"].ToString());
                                //    Crow[Name["MucHaoPhiGoc"]].SetValue(VlMauFirst["DinhMuc"]);     
                                //}
                                string TenCongTacDocVao = Crow[Name["TenCongTacDocVao"]].Value.ToString().ToUpper();
                                string TenCongTacGoc = Crow[Name["TenCongTacGoc"]].Value.ToString().ToUpper();       
                                string DonViGoc = Crow[Name["DonViGoc"]].Value.ToString().ToUpper();
                                double MucHaoPhiGoc = Crow[Name["MucHaoPhiGoc"]].Value.NumericValue;
                                double MucHaoPhiDocVao = Crow[Name["MucHaoPhiDocVao"]].Value.NumericValue;
                                if (string.IsNullOrEmpty(TenCongTacGoc))
                                {
                                    if (HpFirst["MaVatLieu"].ToString() == "TT")
                                    {
                                        Crow[Name["TenCongTacDocVao"]].FillColor = Crow[Name["GhiChu"]].FillColor = Color.GreenYellow;
                                        Crow[Name["GhiChu"]].SetValueFromText($"{LoaiVT.Key} được tạo theo công tác TT từ người lập");
                                    }
                                    else
                                    {
                                        Crow[Name["TenCongTacDocVao"]].FillColor = Crow[Name["GhiChu"]].FillColor = Color.Gold;
                                        Crow[Name["GhiChu"]].SetValueFromText($"{LoaiVT.Key} không tra được trong định mức tham khảo");
                                    }
                                }
                                //else if (TenCongTacDocVao != TenCongTacGoc)
                                //{
                                //    if(DonViGoc != DonViDocVao&& MucHaoPhiDocVao != MucHaoPhiGoc)
                                //    {
                                //        Crow[Name["TenCongTacDocVao"]].FillColor = Crow[Name["TenCongTacGoc"]].FillColor =
                                //            Crow[Name["MucHaoPhiDocVao"]].FillColor = Crow[Name["MucHaoPhiGoc"]].FillColor=
                                //           Crow[Name["DonViDocVao"]].FillColor = Crow[Name["DonViGoc"]].FillColor
                                //           = Crow[Name["GhiChu"]].FillColor = Color.Gold;
                                //        Crow[Name["GhiChu"]].SetValueFromText($"Tên, hao phí và đơn vị không đúng với định mức");
                                //    }
                                //    else if (DonViDocVao != DonViGoc)
                                //    {
                                //        Crow[Name["TenCongTacDocVao"]].FillColor =  Crow[Name["TenCongTacGoc"]].FillColor=  
                                //            Crow[Name["DonViDocVao"]].FillColor=  Crow[Name["DonViGoc"]].FillColor
                                //            = Crow[Name["GhiChu"]].FillColor = Color.Gold;
                                //        Crow[Name["GhiChu"]].SetValueFromText($"Tên và đơn vị không đúng với định mức");
                                //    }
                                //    else
                                //    {
                                //        Crow[Name["TenCongTacDocVao"]].FillColor =  Crow[Name["TenCongTacGoc"]].FillColor = Crow[Name["GhiChu"]].FillColor = Color.Gold;
                                //        Crow[Name["GhiChu"]].SetValueFromText($"{LoaiVT.Key} đọc vào không đúng trong định mức");
                                //    }

                                //}
                                else if (MyFunction.ConvertSuperscript(DonViGoc) != DonViVt.ToUpper())
                                {
                                    if (MucHaoPhiDocVao != MucHaoPhiGoc)
                                    {
                                        Crow[Name["DonViDocVao"]].Font.Strikethrough =  Crow[Name["MucHaoPhiDocVao"]].Font.Strikethrough = true;
                                        Crow[Name["MucHaoPhiDocVao"]].FillColor = Crow[Name["MucHaoPhiGoc"]].FillColor =
                                                 Crow[Name["DonViDocVao"]].FillColor = Crow[Name["DonViGoc"]].FillColor
                                                 = Crow[Name["GhiChu"]].FillColor = Color.Gold;
                                        Crow[Name["GhiChu"]].SetValueFromText("Tên đơn vị và hao phí không đúng so với định mức tham khảo");
                                    }
                                    else
                                    {
                                        Crow[Name["DonViDocVao"]].Font.Strikethrough = true;
                                        Crow[Name["DonViDocVao"]].FillColor = Crow[Name["DonViGoc"]].FillColor = Crow[Name["GhiChu"]].FillColor = Color.Gold;
                                        Crow[Name["GhiChu"]].SetValueFromText("Tên đơn vị không đúng so với định mức");
                                    }
                                }
                                else if(MucHaoPhiGoc!=MucHaoPhiDocVao)
                                {
                                    Crow[Name["MucHaoPhiDocVao"]].FillColor = Crow[Name["MucHaoPhiGoc"]].FillColor 
                                                = Crow[Name["GhiChu"]].FillColor = Color.Gold;
                                    Crow[Name["GhiChu"]].SetValueFromText("Hao phí không đúng so với định mức tham khảo");
                                }
                                ////Phần hao phí
                                //CrowHP = wshP.Rows[IndexhP++];
                                //CrowHP.CopyFrom(TenVatLieuhp, PasteSpecial.All);
                                //CrowHP.Visible = true;
                                ////wshP.Rows.Insert(IndexhP, 1, RowFormatMode.FormatAsNext);
                                //CrowHP[NameHp["TenCongTac"]].SetValueFromText(HpFirst["VatTu"].ToString());
                                //CrowHP[NameHp["DonVi"]].SetValueFromText(HpFirst["DonViHP"].ToString());
                                //CrowHP[NameHp["DinhMuc"]].SetValue(HpFirst["DinhMuc"]);
                                //CrowHP[NameHp["KhoiLuongThiCong"]].SetValue(HpFirst["KhoiLuongKeHoach"]);
                                //CrowHP[NameHp["DonGia"]].SetValue(HpFirst["DonGiaVatLieu"]);
                                //CrowHP[NameHp["HeSoVatTu"]].SetValue(HpFirst["HeSoVatTu"]);
                                //CrowHP[NameHp["MaHieuCongTac"]].SetValueFromText(HpFirst["MaVatLieu"].ToString());
                                //CrowHP[NameHp["TongHaoPhi"]].Formula = $"{NameHp["DinhMuc"]}{IndexhP}*{NameHp["HeSoVatTu"]}{IndexhP}*{NameHp["KhoiLuongThiCong"]}{IndexCongTac}";
                                ////End


                            }
                            DataRow[] lstNot = DtCongTac.Where(x => !lstMaVLGoc.Contains(x["MaVatLieu"].ToString()) && x["LoaiVatTu"].ToString() == LoaiVT.Key.ToString())
                                .ToArray();
                            lstMaVLGoc.Clear();
                            foreach (var Not in lstNot)
                            {
                                if (lstMaVLGoc.Contains(Not["MaVatLieu"].ToString()))
                                    continue;
                                lstMaVLGoc.Add(Not["MaVatLieu"].ToString());
                                Crow = ws.Rows[Index++];
                                ws.Rows.Insert(Index, 1, RowFormatMode.FormatAsNext);
                                Crow.CopyFrom(TenVatLieu, PasteSpecial.All);
                                Crow.Visible = true;
                                Crow[Name["TenCongTacDocVao"]].FillColor
                                      = Crow[Name["GhiChu"]].FillColor = Color.Gold;
                                Crow[Name["GhiChu"]].SetValueFromText($"Không tìm thấy {LoaiVT.Key} trong định mức tham khảo");
                                Crow[Name["TenCongTacGoc"]].SetValueFromText(Not["VatTu"].ToString());
                                Crow[Name["DonViGoc"]].SetValueFromText(Not["DonViVatTu"].ToString());
                                Crow[Name["MucHaoPhiGoc"]].SetValue(Not["DinhMuc"]);
                                Crow[Name["MaHieuCongTac"]].SetValue(Not["MaVatLieu"]);
                            }
                        }
                        //Phần hao phí
                        //wshP.Rows[IndexCongTac-1][NameHp["ThanhTien"]].Formula = $"={Fomular}";
                        //End
                    }

                }

            }
            spread_ThamDinh.EndUpdate();
            WaitFormHelper.CloseWaitForm();
        }

        private void sb_CapNhap_Click(object sender, EventArgs e)
        {
            string name = spread_ThamDinh.ActiveWorksheet.Name;
            if (name == "Thẩm tra định mức")
            {
                Fcn_LoadData();
            }
            else if (name == "Hao phí vật tư")
            {
                LoadDataHaoPhi();
            }
        }
        private void LoadDataHaoPhi()
        {
            WaitFormHelper.ShowWaitForm($"Đang phân tích dữ liệu, Vui lòng chờ!!!!!!!");
            string dbString = $"SELECT " +
                    $"hm.Code AS CodeHangMuc,hm.Ten AS TenHangMuc," +
                    $"ctrinh.Code AS CodeCongTrinh,ctrinh.Ten AS TenCongTrinh," +
                    $"COALESCE(cttk.DonVi, dmct.DonVi) AS DonVi,COALESCE(cttk.TenCongTac, dmct.TenCongTac) AS TenCongTac," +
                    $"COALESCE(cttk.MaHieuCongTac, dmct.MaHieuCongTac) AS MaHieuCongTac,hp.Code as CodeHP,hp.DonGia as DonGiaVatLieu," +
                    $"hp.MaVatLieu,hp.VatTu,hp.DonVi as DonViHP,hp.DinhMuc,hp.LoaiVatTu,hp.HeSo as HeSoVatTu,cttk.KhoiLuongToanBo,cttk.DonGia, \r\n" +
                    $"ROUND(cttk.KhoiLuongToanBo*hp.HeSoNguoiDung*hp.DinhMucNguoiDung, 2) AS KhoiLuongKeHoach," +
                    $"hm.CodeCongTrinh, \r\n" +
                    $"ctrinh.CodeDuAn AS CodeDuAn, \r\n" +
                    $"ctrinh.SortId AS SortIdCtrinh,hm.SortId AS SortIdHM,cttk.Code \r\n" +
                    $"FROM {TDKH.TBL_ChiTietCongTacTheoKy} cttk\r\n" +
                    $"LEFT JOIN {TDKH.TBL_DanhMucCongTac} dmct\r\n" +
                    $"ON cttk.CodeCongTac = dmct.Code \r\n" +
                    $"LEFT JOIN {TDKH.Tbl_HaoPhiVatTu} hp\r\n" +
                    $"ON hp.CodeCongTac = cttk.Code \r\n" +
                    $"LEFT JOIN {MyConstant.TBL_THONGTINHANGMUC} hm\r\n" +
                    $"ON dmct.CodeHangMuc = hm.Code \r\n" +
                    $"LEFT JOIN {MyConstant.TBL_THONGTINCONGTRINH} ctrinh\r\n" +
                    $"ON hm.CodeCongTrinh = ctrinh.Code \r\n" +
                    $"WHERE ctrinh.CodeDuAn = '{SharedControls.slke_ThongTinDuAn.EditValue}'\r\n" +
                    $"ORDER BY SortIdCtrinh ASC,SortIdHM ASC, cttk.SortId ASC\r\n";
            DataTable dtCongTacTheoKy = DataProvider.InstanceTHDA.ExecuteQuery(dbString);
            if (dtCongTacTheoKy.Rows.Count == 0)
            {
                WaitFormHelper.CloseWaitForm();
                return;
            }        
            ///Phần hao phí vật tư
            ///
            Worksheet wshP = spread_ThamDinh.Document.Worksheets["Hao phí vật tư"];
            wshP.Rows.Remove(3, wshP.GetUsedRange().BottomRowIndex);
            List<DefinedName> lstWShp = wshP.DefinedNames.Where(x => x.Name != "HaoPhi_0").ToList();
            lstWShp.ForEach(x => wshP.DefinedNames.Remove(x));
            CellRange RangeMauCongTrinh = spread_ThamDinh.Document.Worksheets["Mau1"].Range["CongTrinh_Mau"];
            Dictionary<string, string> NameHp = MyFunction.fcn_getDicOfColumn(RangeMauCongTrinh);
            Row CongTachp = spread_ThamDinh.Document.Worksheets["Mau1"].Rows[6];
            Row VatLieuhp = spread_ThamDinh.Document.Worksheets["Mau1"].Rows[7];
            Row TenVatLieuhp = spread_ThamDinh.Document.Worksheets["Mau1"].Rows[8];
            Row CopyHM = spread_ThamDinh.Document.Worksheets["Mau1"].Rows[9];
            int CountRangeHp = 0;
            wshP.Rows[2]["A"].SetValueFromText($"Dự án: {SharedControls.slke_ThongTinDuAn.Text}");
            string Fomular = string.Empty;
            ///End
            var grCtrinh = dtCongTacTheoKy.AsEnumerable().GroupBy(x => x["CodeCongTrinh"].ToString());
            int CountRange = 0;
            int STT = 1;
            spread_ThamDinh.BeginUpdate();
            foreach (var CTrinh in grCtrinh)
            {
                var grHangMuc = CTrinh.GroupBy(x => x["CodeHangMuc"].ToString());
                //Phần hao phí
                CellRange RangeHaoPhi = wshP.Range[$"HaoPhi_{CountRangeHp++}"];
                wshP.Rows[RangeHaoPhi.BottomRowIndex + 3].CopyFrom(RangeMauCongTrinh, PasteSpecial.All);
                CellRange NewRangeHP = wshP.Range[$"A{RangeHaoPhi.BottomRowIndex + 4}:L{RangeHaoPhi.BottomRowIndex + 4 + RangeMauCongTrinh.RowCount}"];
                NewRangeHP.Name = $"HaoPhi_{CountRangeHp}";
                wshP.Rows[NewRangeHP.TopRowIndex][0].SetValueFromText($"CÔNG TRÌNH: {CTrinh.FirstOrDefault()["TenCongTrinh"]}");
                int IndexhP = NewRangeHP.TopRowIndex + 8;
                wshP.Rows.Hide(IndexhP - 4, IndexhP);
                //end

                foreach (var Hm in grHangMuc)
                {
                    STT = 1;
                    var grCongTac = Hm.GroupBy(x => x["Code"]);
                    //Phần hao phí
                    Row CrowHP = wshP.Rows[IndexhP++];
                    CrowHP.CopyFrom(CopyHM, PasteSpecial.All);
                    CrowHP.Visible = true;
                    CrowHP[0].SetValueFromText($"HẠNG MỤC: {Hm.FirstOrDefault()["TenHangMuc"]}");
                    wshP.Rows.Insert(IndexhP + 2, 1, RowFormatMode.FormatAsNext);
                    //End
                    foreach (var Ctac in grCongTac)
                    {
                        var CtacFirst = Ctac.FirstOrDefault();
                        WaitFormHelper.ShowWaitForm($"{STT}_{CtacFirst["MaHieuCongTac"]}-{CtacFirst["TenCongTac"]}");                      
                        //Phần hao phí
                        CrowHP = wshP.Rows[IndexhP++];
                        wshP.Rows.Insert(IndexhP + 2, 1, RowFormatMode.FormatAsNext);
                        CrowHP.CopyFrom(CongTachp, PasteSpecial.All);
                        CrowHP.Visible = true;
                        int IndexCongTac = IndexhP;
                        Fomular = string.Empty;
                        CrowHP[NameHp["STT"]].SetValue(STT++);
                        CrowHP[NameHp["MaHieuCongTac"]].SetValueFromText(CtacFirst["MaHieuCongTac"].ToString());
                        CrowHP[NameHp["TenCongTac"]].SetValueFromText(CtacFirst["TenCongTac"].ToString());
                        CrowHP[NameHp["DonVi"]].SetValueFromText(CtacFirst["DonVi"].ToString());
                        CrowHP[NameHp["KhoiLuongThiCong"]].SetValue(CtacFirst["KhoiLuongToanBo"]);
                        CrowHP[NameHp["DonGia"]].SetValue(CtacFirst["DonGia"]);
                        //End

                        var LoaiVatTu = Ctac.GroupBy(x => x["LoaiVatTu"]).OrderByDescending(x => x.Key);
                        foreach (var LoaiVT in LoaiVatTu)
                        {
                            var grHaoPhi = LoaiVT.GroupBy(x => x["CodeHP"]);
                            //Phần hao phí
                            CrowHP = wshP.Rows[IndexhP++];
                            CrowHP.CopyFrom(VatLieuhp, PasteSpecial.All);
                            CrowHP.Visible = true;
                            int IndexLoaiVL = IndexhP;
                            wshP.Rows.Insert(IndexhP + 2, grHaoPhi.Count() + 1, RowFormatMode.FormatAsNext);
                            CrowHP[NameHp["TenCongTac"]].SetValueFromText(LoaiVT.Key.ToString());
                            CrowHP[NameHp["ThanhTien"]].Formula = $"SUM({NameHp["ThanhTien"]}{IndexhP + 1}:{NameHp["ThanhTien"]}{IndexhP + grHaoPhi.Count()})";
                            Fomular += $"+{NameHp["ThanhTien"]}{IndexhP}";
                            //End
                            foreach (var HaoPhi in grHaoPhi)
                            {
                                var HpFirst = HaoPhi.FirstOrDefault();
                                //string DonViVt = MyFunction.ConvertSuperscript(HpFirst["DonViHP"].ToString());
                                //Phần hao phí
                                CrowHP = wshP.Rows[IndexhP++];
                                CrowHP.CopyFrom(TenVatLieuhp, PasteSpecial.All);
                                CrowHP.Visible = true;
                                //wshP.Rows.Insert(IndexhP, 1, RowFormatMode.FormatAsNext);
                                CrowHP[NameHp["TenCongTac"]].SetValueFromText(HpFirst["VatTu"].ToString());
                                CrowHP[NameHp["DonVi"]].SetValueFromText(HpFirst["DonViHP"].ToString());
                                CrowHP[NameHp["DinhMuc"]].SetValue(HpFirst["DinhMuc"]);
                                CrowHP[NameHp["KhoiLuongThiCong"]].SetValue(HpFirst["KhoiLuongKeHoach"]);
                                CrowHP[NameHp["DonGia"]].SetValue(HpFirst["DonGiaVatLieu"]);
                                CrowHP[NameHp["HeSoVatTu"]].SetValue(HpFirst["HeSoVatTu"]);
                                CrowHP[NameHp["MaHieuCongTac"]].SetValueFromText(HpFirst["MaVatLieu"].ToString());
                                CrowHP[NameHp["TongHaoPhi"]].Formula = $"{NameHp["DinhMuc"]}{IndexhP}*{NameHp["HeSoVatTu"]}{IndexhP}*{NameHp["KhoiLuongThiCong"]}{IndexCongTac}";
                                //End


                            }
                        }
                        wshP.Rows[IndexCongTac - 1][NameHp["ThanhTien"]].Formula = $"={Fomular}";
                    }

                }

            }
            spread_ThamDinh.EndUpdate();
            Fcn_FomatVatLieuKhac(wshP);
            WaitFormHelper.CloseWaitForm();

        }
        private void Fcn_FomatVatLieuKhac(Worksheet wshP)
        {
            spread_ThamDinh.BeginUpdate();
            string[] ArrVatLieu = new string[]
            {
                "Vật liệu khác",
                "Vât liệu khác",
                "Máy khác",
                "May khác",
            };    
            string[] ArrVatLieuCha = new string[]
            {
                "Vật liệu",
                "Nhân công",
                "Máy thi công"
            };
            int RowCha = 0;
            for (int i=wshP.GetUsedRange().TopRowIndex;i<= wshP.GetUsedRange().BottomRowIndex; i++)
            {
                Row Crow = wshP.Rows[i];
                //string MaHieu = Crow["B"].Value.TextValue;
                //if (string.IsNullOrEmpty(MaHieu))
                //    continue;
                string TenVL = Crow["D"].Value.ToString();
                
                if (ArrVatLieuCha.Contains(TenVL))
                {
                    RowCha = i;
                    continue;
                }   
                if (ArrVatLieu.Contains(TenVL))
                {
                    int LastLine = 0;
                    for(int j=i+1;j<= wshP.GetUsedRange().BottomRowIndex; j++)
                    {
                        Row CrowCon = wshP.Rows[j];
                        TenVL = CrowCon["D"].Value.ToString();
                        string STT = CrowCon["A"].Value.ToString();
                        //MaHieu = CrowCon["B"].Value.TextValue;
                        if(!string.IsNullOrEmpty(STT)|| ArrVatLieuCha.Contains(TenVL))
                        {
                            LastLine = j;
                            break;
                        }
                    }
                    string Fomula = "=(";
                    for(int k= RowCha+2; k <= LastLine; k++)
                    {
                        if (k == i+1)
                            continue;
                        Fomula += $"+L{k}";
                    }
                    Fomula += $")*G{i+1}*H{i+1}%";
                    if(!Fomula.Contains("()"))
                        Crow["L"].Formula = Fomula;
                }
            }
            spread_ThamDinh.EndUpdate();
        }
        public void Fcn_LoadDataInput()
        {
            if (SharedControls.slke_ThongTinDuAn is null)
                return;
            FileHelper.fcn_spSheetStreamDocument(spread_ThamDinh, $@"{BaseFrom.m_templatePath}\FileExcel\20.1Thẩm tra định mức.xlsx");
            spread_ThamDinh_ActiveSheetChanged(null, null);
        }
        private void Ctrl_KiemSoatSoLieuDocVao_Load(object sender, EventArgs e)
        {
            //Fcn_LoadData();
        }

        private void sb_Export_Click(object sender, EventArgs e)
        {
            SaveFileDialog f = new SaveFileDialog();
            f.Filter = "Execl files (*.xlsx)|*.xlsx";
            f.FileName = $"Hao phí vật tư - {DateTime.Now.ToString("dd-MM-yyyy_hh-mm-ss")}.xlsx";
            f.FilterIndex = 0;
            f.RestoreDirectory = true;
            f.CreatePrompt = true;
            f.Title = "Xuất hao phí vật tư";
            if (f.ShowDialog() == DialogResult.OK)
            {
                string name = spread_ThamDinh.ActiveWorksheet.Name;
                if (name == "Thẩm tra định mức")
                {
                    LoadDataHaoPhi();
                }
                else if (name == "Hao phí vật tư")
                {
                    Fcn_LoadData();
                }
                //string time = DateTime.Now.ToString("dd-MM-yyyy_hh-mm-ss");
                Workbook wb = spread_ThamDinh.Document.Clone();
                wb.SaveDocument(f.FileName, DocumentFormat.Xlsx);
                DialogResult dialogResult = XtraMessageBox.Show($"{f.FileName} thành công. Bạn có muốn mở file không???", "QUẢN LÝ THI CÔNG - THÔNG BÁO", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dialogResult == DialogResult.Yes)
                {
                    Process.Start(f.FileName);
                }
            }
        }

        private void spread_ThamDinh_ActiveSheetChanged(object sender, ActiveSheetChangedEventArgs e)
        {
            string name = spread_ThamDinh.ActiveWorksheet.Name;
            if(name== "Thẩm tra định mức")
            {
                Fcn_LoadData();
            }
            else if(name== "Hao phí vật tư")
            {
                LoadDataHaoPhi();
            }
        }
    }
}
