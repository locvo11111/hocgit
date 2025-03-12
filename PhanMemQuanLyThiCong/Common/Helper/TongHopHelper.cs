using DevExpress.DataAccess.Sql;
using DevExpress.XtraRichEdit.Import.OpenXml;
using DevExpress.XtraSpreadsheet.Internal;
//using DevExpress.XtraSpreadsheet.Model;
using DevExpress.XtraTab;
using Microsoft.AspNet.SignalR.Messaging;
using Microsoft.Owin.Security;
using Newtonsoft.Json;
using PhanMemQuanLyThiCong.Common.Constant;
using PhanMemQuanLyThiCong.Constant;
using PhanMemQuanLyThiCong.Controls;
using PhanMemQuanLyThiCong.Model;
using PhanMemQuanLyThiCong.Model.Excel;
using PhanMemQuanLyThiCong.Model.PermissionControl;
using PhanMemQuanLyThiCong.Model.TDKH;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VChatCore.ViewModels.SyncSqlite;
using DevExpress.Spreadsheet;
using DevExpress.XtraSpreadsheet.Export;
using MSETTING = PhanMemQuanLyThiCong.Properties.Settings;
using PhanMemQuanLyThiCong.Common.Enums;
using System.Windows.Forms;
using Microsoft.Win32;
using StackExchange.Profiling.Internal;
//using DevExpress.Compression;
using PhanMemQuanLyThiCong.Common.ViewModel.KLHN;
using DevExpress.XtraReports.UI;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraEditors;
using System.IO.Compression;

namespace PhanMemQuanLyThiCong.Common.Helper
{
    public static class TongHopHelper
    {
        //Create method to get All KhoiLuongHangNgay From TBL_KhoiLuongCongViecHangNgay by cbb_MenuDuAnDangThucHien
        public static List<KLTTHangNgay> GetAllKeHoachTienDoCongTrinhTheoDuAn(bool IsLimitNBD2Now = false)
        {

            //string dbString1 = $"SELECT * FROM {TDKH.TBL_ChiTietCongTacTheoKy}";
            //DataTable dtTheoKy = DataProvider.InstanceTHDA.ExecuteQuery(dbString1);

            string dbString = $"SELECT {TDKH.TBL_ChiTietCongTacTheoKy}.Code, {MyConstant.TBL_THONGTINCONGTRINH}.Code AS CodeCongTrinh, " +
                $"{MyConstant.TBL_THONGTINHANGMUC}.Code AS CodeHangMuc, {MyConstant.TBL_THONGTINCONGTRINH}.Ten AS TenCongTrinh,  " +
                $"{MyConstant.TBL_THONGTINHANGMUC}.Ten AS TenHangMuc, {TDKH.TBL_DanhMucCongTac}.TenCongTac, " +
                //$"{TDKH.TBL_KhoiLuongCongViecHangNgay}.ThanhTienKeHoach AS KhoiLuongKeHoach, {TDKH.TBL_KhoiLuongCongViecHangNgay}.ThanhTienThiCong AS KhoiLuongThiCong, " +
                $"{MyConstant.view_DonViThucHien}.Ten AS TenDonViThucHien, {MyConstant.TBL_THONGTINDUAN}.TenDuAn " +
                $"FROM {MyConstant.TBL_THONGTINCONGTRINH} " +
                $"JOIN {MyConstant.TBL_THONGTINHANGMUC} " +
                $"ON {MyConstant.TBL_THONGTINHANGMUC}.CodeCongTrinh = {MyConstant.TBL_THONGTINCONGTRINH}.Code " +
                $"JOIN {MyConstant.TBL_THONGTINDUAN} " +
                $"ON {MyConstant.TBL_THONGTINCONGTRINH}.CodeDuAn = {MyConstant.TBL_THONGTINDUAN}.Code " +
                $"LEFT JOIN {TDKH.TBL_DanhMucCongTac} " +
                $"ON {TDKH.TBL_DanhMucCongTac}.CodeHangMuc = {MyConstant.TBL_THONGTINHANGMUC}.Code " +
                $"LEFT JOIN {TDKH.TBL_ChiTietCongTacTheoKy} " +
                $"ON {TDKH.TBL_ChiTietCongTacTheoKy}.CodeCongTac = {TDKH.TBL_DanhMucCongTac}.Code " +
                $"LEFT JOIN {TDKH.TBL_KhoiLuongCongViecHangNgay} " +
                //$"ON {TDKH.TBL_KhoiLuongCongViecHangNgay}.CodeCongTacTheoGiaiDoan = {TDKH.TBL_ChiTietCongTacTheoKy}.Code " +
                $"LEFT JOIN {MyConstant.view_DonViThucHien} " +
                $"ON (COALESCE(CodeNhaThau, CodeNhaThauPhu, CodeToDoi) = {MyConstant.view_DonViThucHien}.Code) ";// +
                //$"WHERE {TDKH.TBL_KhoiLuongCongViecHangNgay}.Ngay IS NULL OR {TDKH.TBL_KhoiLuongCongViecHangNgay}.Ngay <= '{DateTime.Now.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE)}'";
                
            //$"WHERE {MyConstant.TBL_THONGTINCONGTRINH}.CodeDuAn = '{SharedControls.slke_ThongTinDuAn.EditValue}'";
            if (IsLimitNBD2Now)
            {
                dbString += $" WHERE {TDKH.TBL_ChiTietCongTacTheoKy}.NgayBatDau <= '{DateTime.Now.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE)}' ";
            }

            List<KLTTHangNgay> lsAll = DataProvider.InstanceTHDA.ExecuteQueryModel<KLTTHangNgay>(dbString);

            List<KLTTHangNgay> lsOut = new List<KLTTHangNgay>();

            var codeCongTacs = lsAll.Select(x => x.Code).Distinct();
            var KLHangNgay = MyFunction.Fcn_CalKLKHNewWithoutKeHoach(Enums.TypeKLHN.CongTac, codeCongTacs, dateKT: DateTime.Now.Date).Select(x => new KLTTHangNgay()
            {
                CodeCha = x.ParentCode,
                CodeCongTacTheoGiaiDoan = x.CodeCongTacTheoGiaiDoan,
                KhoiLuongKeHoach = x.KhoiLuongKeHoach,
                KhoiLuongThiCong = x.KhoiLuongThiCong,
                ThanhTienThiCongCustom = x.ThanhTienThiCong
            }).ToList();


           
            if (KLHangNgay is null)
                return null;

            foreach (KLTTHangNgay dr in lsAll)
            {
                dr.KhoiLuongKeHoach = KLHangNgay.Where(x =>x.CodeCongTacTheoGiaiDoan == dr.Code && x.ThanhTienKeHoach.HasValue).Sum(x => x.ThanhTienKeHoach.Value);
                dr.KhoiLuongThiCong = KLHangNgay.Where(x => x.CodeCongTacTheoGiaiDoan == dr.Code && x.ThanhTienThiCong.HasValue).Sum(x => x.ThanhTienThiCong.Value);
            }

            var grCongTrinhs = lsAll.GroupBy(x => new { x.CodeCongTrinh, x.TenDuAn });
            foreach (var grCongTrinh in grCongTrinhs)
            {
                lsOut.Add(new KLTTHangNgay()
                {
                    Code = grCongTrinh.Key.CodeCongTrinh,
                    TenCongTac = $"Công trình: {grCongTrinh.First().TenCongTrinh}",
                    KhoiLuongKeHoach = grCongTrinh.Where(x => x.KhoiLuongKeHoach.HasValue).Sum(x => x.KhoiLuongKeHoach),
                    KhoiLuongThiCong = grCongTrinh.Where(x => x.KhoiLuongThiCong > 0).Sum(x => x.KhoiLuongThiCong),
                    TenDuAn = grCongTrinh.Key.TenDuAn
                });

                var grHMs = grCongTrinh.GroupBy(x => x.CodeHangMuc);

                foreach (var grHM in grHMs)
                {
                    lsOut.Add(new KLTTHangNgay()
                    {
                        ParentCode =grCongTrinh.Key.CodeCongTrinh,
                        Code = grHM.Key,
                        TenCongTac = $"Hạng mục: {grCongTrinh.First().TenHangMuc}",
                        KhoiLuongKeHoach = grHM.Where(x => x.KhoiLuongKeHoach.HasValue).Sum(x => x.KhoiLuongKeHoach),
                        KhoiLuongThiCong = grHM.Where(x => x.KhoiLuongThiCong > 0).Sum(x => x.KhoiLuongThiCong),
                        TenDuAn = grCongTrinh.Key.TenDuAn

                    });

                    var grCTacs = grHM.Where(x => x.Code != null).GroupBy(x => new { x.Code, x.TenDonViThucHien});

                    foreach (var grCTac in grCTacs)
                    {

                        lsOut.Add(new KLTTHangNgay()
                        {
                            ParentCode = grHM.Key,
                            Code = grCTac.Key.Code,
                            TenDonViThucHien = grCTac.Key.TenDonViThucHien,
                            TenCongTac = grCTac.First().TenCongTac,
                            KhoiLuongKeHoach = grCTac.Where(x => x.KhoiLuongKeHoach.HasValue).Sum(x => x.KhoiLuongKeHoach),
                            KhoiLuongThiCong = grCTac.Where(x => x.KhoiLuongThiCong > 0).Sum(x => x.KhoiLuongThiCong),
                            TenDuAn = grCongTrinh.Key.TenDuAn
                            
                        });
                    }
                }
            }
            return lsOut;
        }
        
        public static DataRow GetKinhPhiPhanBo()
        {
            DonViThucHien DVTH = SharedControls.ctrl_DonViThucHienDuAnTDKH.SelectedDVTH as DonViThucHien;

            if (DVTH is null)
                return null;

            string dkString = GetConditionNhaThauToDoiDoBocByGiaiDoan();

            string dbString = $"SELECT * FROM {TDKH.TBL_KinhPhiPhanBo} cttk WHERE {dkString}";
            DataTable dt = DataProvider.InstanceTHDA.ExecuteQuery(dbString);
            DataRow dr = dt.AsEnumerable().SingleOrDefault();

            if (dr is null)
            {
                dr = dt.NewRow();
                dt.Rows.Add(dr);
                dr["Code"] = Guid.NewGuid().ToString();
                dr["CodeGiaiDoan"] = (string)SharedControls.cbb_DBKH_ChonDot.SelectedValue;
                if (DVTH != null)
                {
                    dr[DVTH.ColCodeFK] = DVTH.Code;
                }
                DataProvider.InstanceTHDA.UpdateDataTableFromSqliteSource(dt, TDKH.TBL_KinhPhiPhanBo);
            }
            return DataProvider.InstanceTHDA.ExecuteQuery(dbString).Rows[0];
        }

        public static string GetConditionNhaThauToDoiDoBoc()
        {
            DonViThucHien dvth = SharedControls.ctrl_DonViThucHienDuAnTDKH.SelectedDVTH as DonViThucHien;
            if (dvth is null)
            {
                return null;
            }

            else return $" cttk.{dvth.ColCodeFK} = '{dvth.Code}'";
        }

        public static string GetConditionNhaThauToDoiDoBocByGiaiDoan()
        {
            string con = GetConditionNhaThauToDoiDoBoc();
            if (con != null)
            {
                return $"cttk.CodeGiaiDoan = '{SharedControls.cbb_DBKH_ChonDot.SelectedValue}' AND {con}";
            }
            else
                return $"cttk.CodeGiaiDoan = '{SharedControls.cbb_DBKH_ChonDot.SelectedValue}'";
        }
        public static List<KLTTHangNgay> GetAllThanhTienHaoPhiCongTac(TypeDVTH typeDVTH)
        {
            string colFk = MyConstant.lsColFkDVTH[(int)typeDVTH];
            string tbl = MyConstant.lsTblDVTH[(int)typeDVTH];

            string dbString = $"SELECT {tbl}.Code AS CodeDVTH, {tbl}.Ten AS TenDonViThucHien, " +
                $"{TDKH.TBL_ChiTietCongTacTheoKy}.Code AS CodeCongTac, {TDKH.TBL_DanhMucCongTac}.TenCongTac, " +
                $"{MyConstant.TBL_THONGTINHANGMUC}.Code AS CodeHangMuc, {MyConstant.TBL_THONGTINHANGMUC}.Ten AS TenHangMuc, " +
                $"{MyConstant.TBL_THONGTINCONGTRINH}.Code AS CodeCongTrinh, {MyConstant.TBL_THONGTINCONGTRINH}.Ten AS TenCongTrinh, " +
                $"{TDKH.TBL_DanhMucCongTac}.Code AS CodeDanhMucCongTac, {TDKH.TBL_ChiTietCongTacTheoKy}.KhoiLuongToanBo AS KhoiLuongKeHoach, {TDKH.TBL_ChiTietCongTacTheoKy}.KinhPhiDuKien, " +
                $"{TDKH.TBL_ChiTietCongTacTheoKy}.KhoiLuongHopDongChiTiet AS KhoiLuongHopDong, " +
                $"{TDKH.TBL_ChiTietCongTacTheoKy}.DonGia AS DonGiaKeHoach " +
                $"FROM {tbl} " +
                $"LEFT JOIN {TDKH.TBL_ChiTietCongTacTheoKy} " +
                $"ON {TDKH.TBL_ChiTietCongTacTheoKy}.{colFk} = {tbl}.Code " +
                $"JOIN {TDKH.TBL_DanhMucCongTac} " +
                $"ON {TDKH.TBL_DanhMucCongTac}.Code = {TDKH.TBL_ChiTietCongTacTheoKy}.CodeCongTac " +
                $"JOIN {MyConstant.TBL_THONGTINHANGMUC} " +
                $"ON {MyConstant.TBL_THONGTINHANGMUC}.Code = {TDKH.TBL_DanhMucCongTac}.CodeHangMuc " +
                $"JOIN {MyConstant.TBL_THONGTINCONGTRINH} " +
                $"ON {MyConstant.TBL_THONGTINCONGTRINH}.Code = {MyConstant.TBL_THONGTINHANGMUC}.CodeCongTrinh " +
                $"WHERE {TDKH.TBL_ChiTietCongTacTheoKy}.{colFk} IS NOT NULL AND {TDKH.TBL_ChiTietCongTacTheoKy}.{colFk}<>'{SharedControls.slke_ThongTinDuAn.EditValue}' AND {TDKH.TBL_ChiTietCongTacTheoKy}.CodeGiaiDoan='{SharedControls.cbb_DBKH_ChonDot.SelectedValue}' ";
            if (typeDVTH == TypeDVTH.TuThucHien)
                dbString += $" AND {tbl}.CodeTongThau IS NOT NULL";
            else if (typeDVTH == TypeDVTH.NhaThauPhu)
                dbString += $" AND {tbl}.CodeTongThau IS NULL";
            DataTable dt = DataProvider.InstanceTHDA.ExecuteQuery(dbString);
            DataTable DTHPVT= new DataTable();
            DataTable DTKLHN = new DataTable();
            DataTable DTKLHNGoc = new DataTable();
            DataTable Dt_ChuKy = new DataTable();
            if (typeDVTH == TypeDVTH.NhaThau)
            {
                dbString = $"SELECT *  FROM {TDKH.TBL_ChiTietCongTacTheoKy} WHERE \"CodeGiaiDoan\"='{SharedControls.cbb_DBKH_ChonDot.SelectedValue}' AND \"CodeNhaThau\" IS NULL";
                Dt_ChuKy = DataProvider.InstanceTHDA.ExecuteQuery(dbString);
                //dbString = $"SELECT *  FROM {TDKH.TBL_KhoiLuongCongViecHangNgay} WHERE \"CodeCongTacTheoGiaiDoan\" IN ({lstcode})";
                //DTKLHN = MyFunction.Fcn_CalKLKHNew(Common.Enums.TypeKLHN.HaoPhiVatTu, lstcode);
                //DTKLHNGoc = MyFunction.Fcn_CalKLKHNew(Common.Enums.TypeKLHN.CongTac, lstcode, colFk);
            }
            //else
            //{
            //    string[] lstcode = dt.AsEnumerable().Select(x => x["CodeCongTac"].ToString()).ToArray();
            //    dbString = $"SELECT *  FROM {TDKH.Tbl_HaoPhiVatTu} WHERE \"CodeCongTac\" IN ({lstcode})";
            //    DTHPVT = DataProvider.InstanceTHDA.ExecuteQuery(dbString);
            //    lstcode = DTHPVT.AsEnumerable().Select(x => x["Code"].ToString()).ToArray();
            //    DTKLHN = MyFunction.Fcn_CalKLKHNew(Common.Enums.TypeKLHN.CongTac, lstcode);
            //    DTKLHNGoc = MyFunction.Fcn_CalKLKHNew(Common.Enums.TypeKLHN.CongTac, lstcode, colFk);
            //}

            List<KLTTHangNgay> lsSource = DatatableHelper.fcn_DataTable2List<KLTTHangNgay>(dt);

            List<KLTTHangNgay> lsOut = new List<KLTTHangNgay>();

            var grsDVTH = lsSource.GroupBy(x => x.CodeDVTH);

            foreach (var grDVTH in grsDVTH)
            {
                lsOut.Add(
                        new KLTTHangNgay()
                        {
                            Code = grDVTH.Key,
                            TenCongTac = grDVTH.First().TenDonViThucHien,
                        }
                    );


                var grsCTrinh = grDVTH.GroupBy(x => x.CodeCongTrinh);
                foreach (var grCTrinh in grsCTrinh)
                {
                    string CodeCT = Guid.NewGuid().ToString();
                    lsOut.Add
                        (
                            new KLTTHangNgay()
                            {
                                ParentCode = grDVTH.Key,
                                Code = CodeCT,
                                TenCongTac = grCTrinh.First().TenCongTrinh,
                            }
                        );
                    var grsHM = grCTrinh.GroupBy(x => x.CodeHangMuc);
                    foreach (var grHM in grsHM)
                    {
                        string CodeHM = Guid.NewGuid().ToString();
                        lsOut.Add
                            (
                                new KLTTHangNgay()
                                {
                                    ParentCode = CodeCT,
                                    Code = CodeHM,
                                    TenCongTac = grHM.First().TenHangMuc,
                                }
                            );
                        var grsCTacs = grHM.GroupBy(x => x.CodeCha);
                        foreach (var grCTac in grsCTacs)
                        {
                            string CodeCongtac = Guid.NewGuid().ToString();
                            lsOut.Add
                            (
                                new KLTTHangNgay()
                                {
                                    ParentCode = CodeHM,
                                    Code = CodeCongtac,
                                    TenCongTac = grCTac.First().TenCongTac,
                                }
                            );


                            long? TienThiCong = 0;/*DTKLHN.AsEnumerable().Where(x => x["ThanhTienThiCong"] != DBNull.Value && x["CodeCongTacTheoGiaiDoan"].ToString() == grCTac.Key).Sum(x => long.Parse(x["ThanhTienThiCong"].ToString()));*/
                            long? TienThiCongGoc = 0;/* DTKLHNGoc.AsEnumerable().Where(x => x["ThanhTienThiCong"] != DBNull.Value && x["CodeCongTacTheoGiaiDoan"].ToString() == grCTac.Key).Sum(x => long.Parse(x["ThanhTienThiCong"].ToString()));*/
                            if (typeDVTH == TypeDVTH.NhaThau)
                            {
                                DataRow[] Crow = Dt_ChuKy.AsEnumerable().Where(x => x["CodeCongTac"].ToString() == grCTac.FirstOrDefault().CodeDanhMucCongTac).ToArray();
                                string lsCode = MyFunction.fcn_Array2listQueryCondition(Crow.Select(x => x["Code"].ToString()).ToArray());
                                dbString = $"SELECT *  FROM {TDKH.Tbl_HaoPhiVatTu} WHERE \"CodeCongTac\" IN ({lsCode})";
                                DTHPVT = DataProvider.InstanceTHDA.ExecuteQuery(dbString);
                                //string[] lstcode = DTHPVT.AsEnumerable().Select(x => x["Code"].ToString()).ToArray();
                                //DTKLHN = MyFunction.Fcn_CalKLKHNew(Common.Enums.TypeKLHN.HaoPhiVatTu, lstcode);
                                //TienThiCong = DTKLHN.AsEnumerable().Where(x => x["ThanhTienThiCong"] != DBNull.Value).Sum(x => long.Parse(x["ThanhTienThiCong"].ToString()));






                            }
                            else
                            {
                                dbString = $"SELECT *  FROM {TDKH.Tbl_HaoPhiVatTu} WHERE \"CodeCongTac\"='{grCTac.Key}'";
                                DTHPVT = DataProvider.InstanceTHDA.ExecuteQuery(dbString);
  
                            }
                            //string[] lstcode = DTHPVT.AsEnumerable().Select(x => x["Code"].ToString()).ToArray();
                            //DTKLHN = MyFunction.Fcn_CalKLKHNew(Common.Enums.TypeKLHN.HaoPhiVatTu, lstcode);
                            //TienThiCong = DTKLHN.AsEnumerable().Where(x => x["ThanhTienThiCong"] != DBNull.Value).Sum(x => long.Parse(x["ThanhTienTHiCong"].ToString()));

                            foreach (DataRow row in DTHPVT.Rows)
                            {
                                dbString = $"SELECT {TDKH.Tbl_HaoPhiVatTu_HangNgay}.*,{TDKH.Tbl_HaoPhiVatTu}.DonGia  FROM {TDKH.Tbl_HaoPhiVatTu_HangNgay} JOIN {TDKH.Tbl_HaoPhiVatTu} ON {TDKH.Tbl_HaoPhiVatTu}.Code={TDKH.Tbl_HaoPhiVatTu_HangNgay}.CodeHaoPhiVatTu" +
                                    $" WHERE \"CodeHaoPhiVatTu\"='{row["Code"]}'";
                                DTKLHNGoc = DataProvider.InstanceTHDA.ExecuteQuery(dbString);
                                //TienThiCong =(long)DTKLHNGoc.AsEnumerable().Where(x => x["KhoiLuongThiCong"] != DBNull.Value && x["KhoiLuongThiCong"] != DBNull.Value).Sum(x =>
                                //double.Parse(x["KhoiLuongThiCong"].ToString())*double.Parse(x["KhoiLuongThiCong"].ToString()));
                                if (DTKLHNGoc.Rows.Count == 0)
                                    continue;
                                long TienThiCongTT = (long)DTKLHNGoc.AsEnumerable().Where(x => x["KhoiLuongThiCong"] != DBNull.Value && x["DonGia"] != DBNull.Value).Sum(x =>
                                 double.Parse(x["DonGia"].ToString()) * double.Parse(x["KhoiLuongThiCong"].ToString()));
                                TienThiCong += TienThiCongTT;
                                dbString = $"SELECT {TDKH.Tbl_HaoPhiVatTu}.DonGia  FROM {TDKH.Tbl_HaoPhiVatTu} INNER JOIN {TDKH.TBL_ChiTietCongTacTheoKy} ON {TDKH.TBL_ChiTietCongTacTheoKy}.Code={TDKH.Tbl_HaoPhiVatTu}.CodeCongTac" +
                                    $" WHERE {TDKH.Tbl_HaoPhiVatTu}.MaVatLieu='{row["MaVatLieu"]}' AND {TDKH.Tbl_HaoPhiVatTu}.VatTu='{row["VatTu"]}' AND {TDKH.TBL_ChiTietCongTacTheoKy}.CodeCongTac='{grCTac.FirstOrDefault().CodeDanhMucCongTac}' AND {TDKH.TBL_ChiTietCongTacTheoKy}.CodeNhaThau IS NOT NULL ";
                                DataTable DTHPVTGoc = DataProvider.InstanceTHDA.ExecuteQuery(dbString);
                                double DonGia = double.Parse(DTHPVTGoc.Rows[0][0].ToString());
                                double KhoiLuong = DTKLHNGoc.AsEnumerable().Where(x => x["KhoiLuongThiCong"] != DBNull.Value).Sum(x => double.Parse(x["KhoiLuongThiCong"].ToString()));
                                TienThiCongGoc += (long)Math.Round(DonGia * KhoiLuong, 2);

                            }
                            lsOut.Add(new KLTTHangNgay()
                            {
                                ParentCode = CodeCongtac,
                                Code = Guid.NewGuid().ToString(),
                                TenCongTac = "1. Thu",
                                GiaTri =TienThiCongGoc
                            });

                            lsOut.Add
                            (
                                new KLTTHangNgay()
                                {
                                    ParentCode = CodeCongtac,
                                    Code = Guid.NewGuid().ToString(),
                                    TenCongTac = "2. Chi",
                                    GiaTri = TienThiCong
                                }
                            );

                            lsOut.Add
                            (
                                new KLTTHangNgay()
                                {
                                    ParentCode = CodeCongtac,
                                    Code = Guid.NewGuid().ToString(),
                                    TenCongTac = "3. Còn lại",
                                    GiaTri =TienThiCongGoc-TienThiCong
                                    
                                }
                            );
                        }
                    }

                }
            }
            return lsOut;
        }      
        public static List<KLTTHangNgay> GetAllThanhTienChiTietCongTac(TypeDVTH typeDVTH)
        {
            string colFk = MyConstant.lsColFkDVTH[(int)typeDVTH];
            string tbl = MyConstant.lsTblDVTH[(int)typeDVTH];

            string dbString = $"SELECT {tbl}.Code AS CodeDVTH, {tbl}.Ten AS TenDonViThucHien, " +
                $"{TDKH.TBL_ChiTietCongTacTheoKy}.Code AS CodeCongTac, {TDKH.TBL_DanhMucCongTac}.TenCongTac, " +
                $"{MyConstant.TBL_THONGTINHANGMUC}.Code AS CodeHangMuc, {MyConstant.TBL_THONGTINHANGMUC}.Ten AS TenHangMuc, " +
                $"{MyConstant.TBL_THONGTINCONGTRINH}.Code AS CodeCongTrinh, {MyConstant.TBL_THONGTINCONGTRINH}.Ten AS TenCongTrinh, " +
                $"{TDKH.TBL_DanhMucCongTac}.Code AS CodeDanhMucCongTac, {TDKH.TBL_ChiTietCongTacTheoKy}.KhoiLuongToanBo AS KhoiLuongKeHoach, {TDKH.TBL_ChiTietCongTacTheoKy}.KinhPhiDuKien, " +
                $"{TDKH.TBL_ChiTietCongTacTheoKy}.KhoiLuongHopDongChiTiet AS KhoiLuongHopDong, " +
                $"{TDKH.TBL_ChiTietCongTacTheoKy}.DonGia AS DonGiaKeHoach " +
                $"FROM {tbl} " +
                $"LEFT JOIN {TDKH.TBL_ChiTietCongTacTheoKy} " +
                $"ON {TDKH.TBL_ChiTietCongTacTheoKy}.{colFk} = {tbl}.Code " +
                $"JOIN {TDKH.TBL_DanhMucCongTac} " +
                $"ON {TDKH.TBL_DanhMucCongTac}.Code = {TDKH.TBL_ChiTietCongTacTheoKy}.CodeCongTac " +
                $"JOIN {MyConstant.TBL_THONGTINHANGMUC} " +
                $"ON {MyConstant.TBL_THONGTINHANGMUC}.Code = {TDKH.TBL_DanhMucCongTac}.CodeHangMuc " +
                $"JOIN {MyConstant.TBL_THONGTINCONGTRINH} " +
                $"ON {MyConstant.TBL_THONGTINCONGTRINH}.Code = {MyConstant.TBL_THONGTINHANGMUC}.CodeCongTrinh " +
                $"WHERE {TDKH.TBL_ChiTietCongTacTheoKy}.{colFk} IS NOT NULL AND {TDKH.TBL_ChiTietCongTacTheoKy}.{colFk}<>'{SharedControls.slke_ThongTinDuAn.EditValue}' AND {TDKH.TBL_ChiTietCongTacTheoKy}.CodeGiaiDoan='{SharedControls.cbb_DBKH_ChonDot.SelectedValue}' ";
            if (typeDVTH == TypeDVTH.TuThucHien)
                dbString += $" AND {tbl}.CodeTongThau IS NOT NULL";
            else if (typeDVTH == TypeDVTH.NhaThauPhu)
                dbString += $" AND {tbl}.CodeTongThau IS NULL";
            DataTable dt = DataProvider.InstanceTHDA.ExecuteQuery(dbString);
            List<KLHN> DTKLHN = new List<KLHN>();  
            string[] lstcode = dt.AsEnumerable().Select(x => x["CodeCongTac"].ToString()).ToArray();
            DTKLHN = MyFunction.Fcn_CalKLKHNewWithoutKeHoach(Common.Enums.TypeKLHN.CongTac, lstcode);

            List<KLTTHangNgay> lsSource = DatatableHelper.fcn_DataTable2List<KLTTHangNgay>(dt);

            List<KLTTHangNgay> lsOut = new List<KLTTHangNgay>();

            var grsDVTH = lsSource.GroupBy(x => x.CodeDVTH);

            foreach (var grDVTH in grsDVTH)
            {
                lsOut.Add(
                        new KLTTHangNgay()
                        {
                            Code = grDVTH.Key,
                            TenCongTac = grDVTH.First().TenDonViThucHien,
                        }
                    );


                var grsCTrinh = grDVTH.GroupBy(x => x.CodeCongTrinh);
                foreach (var grCTrinh in grsCTrinh)
                {
                    string CodeCT = Guid.NewGuid().ToString();
                    lsOut.Add
                        (
                            new KLTTHangNgay()
                            {
                                ParentCode = grDVTH.Key,
                                Code = CodeCT,
                                TenCongTac = grCTrinh.First().TenCongTrinh,
                            }
                        );
                    var grsHM = grCTrinh.GroupBy(x => x.CodeHangMuc);
                    foreach (var grHM in grsHM)
                    {
                        string CodeHM = Guid.NewGuid().ToString();
                        lsOut.Add
                            (
                                new KLTTHangNgay()
                                {
                                    ParentCode = CodeCT,
                                    Code = CodeHM,
                                    TenCongTac = grHM.First().TenHangMuc,
                                }
                            );
                        var grsCTacs = grHM.GroupBy(x => x.CodeCha);
                        foreach (var grCTac in grsCTacs)
                        {
                            string CodeCongtac = Guid.NewGuid().ToString();
                            var hns = DTKLHN.Where(x => x.CodeCongTacTheoGiaiDoan == grCTac.Key);
                            lsOut.Add
                            (
                                new KLTTHangNgay()
                                {
                                    ParentCode = CodeHM,
                                    Code = CodeCongtac,
                                    TenCongTac = grCTac.First().TenCongTac,
                                }
                            );
                            long? TienThiCong = 0, TienThiCongGoc = 0;
                            if (DTKLHN == null)
                                goto Label;
                            TienThiCong = (long)Math.Round(hns.Sum(x => x.ThanhTienThiCong) ?? 0);
                            TienThiCongGoc = (long)Math.Round(hns.Sum(x => x.ThanhTienThiCong) ?? 0);
                            Label:
                            lsOut.Add(new KLTTHangNgay()
                            {
                                ParentCode = CodeCongtac,
                                Code = Guid.NewGuid().ToString(),
                                TenCongTac = "1. Thu",
                                GiaTri =TienThiCongGoc
                            });

                            lsOut.Add
                            (
                                new KLTTHangNgay()
                                {
                                    ParentCode = CodeCongtac,
                                    Code = Guid.NewGuid().ToString(),
                                    TenCongTac = "2. Chi",
                                    GiaTri = TienThiCong
                                }
                            );

                            lsOut.Add
                            (
                                new KLTTHangNgay()
                                {
                                    ParentCode = CodeCongtac,
                                    Code = Guid.NewGuid().ToString(),
                                    TenCongTac = "3. Còn lại",
                                    GiaTri =TienThiCongGoc-TienThiCong
                                    
                                }
                            );
                        }
                    }

                }
            }
            return lsOut;
        }
        public static List<KLTTHangNgay> GetAllThanhTienTheoDonViThucHien(TypeDVTH typeDVTH, TypeBaoCao typeBaoCao)
        {
            string colFk = MyConstant.lsColFkDVTH[(int)typeDVTH];
            string tbl = MyConstant.lsTblDVTH[(int)typeDVTH];

            string dbString = $"SELECT {tbl}.Code AS CodeDVTH, {tbl}.Ten AS TenDonViThucHien, " +
                $"{TDKH.TBL_ChiTietCongTacTheoKy}.Code AS Code, {TDKH.TBL_DanhMucCongTac}.TenCongTac, " +
                $"{MyConstant.TBL_THONGTINHANGMUC}.Code AS CodeHangMuc, {MyConstant.TBL_THONGTINHANGMUC}.Ten AS TenHangMuc, " +
                $"{MyConstant.TBL_THONGTINCONGTRINH}.Code AS CodeCongTrinh, {MyConstant.TBL_THONGTINCONGTRINH}.Ten AS TenCongTrinh, " +
                $"{TDKH.TBL_DanhMucCongTac}.Code AS CodeDanhMucCongTac, {TDKH.TBL_ChiTietCongTacTheoKy}.KhoiLuongToanBo AS KhoiLuongKeHoach, {TDKH.TBL_ChiTietCongTacTheoKy}.KinhPhiDuKien, " +
                $"{TDKH.TBL_ChiTietCongTacTheoKy}.KhoiLuongHopDongChiTiet AS KhoiLuongHopDong, " +
                $"{TDKH.TBL_ChiTietCongTacTheoKy}.DonGia AS DonGiaKeHoach " +
                $"FROM {tbl} " +
                $"LEFT JOIN {TDKH.TBL_ChiTietCongTacTheoKy} " +
                $"ON {TDKH.TBL_ChiTietCongTacTheoKy}.{colFk} = {tbl}.Code " +
                $"JOIN {TDKH.TBL_DanhMucCongTac} " +
                $"ON {TDKH.TBL_DanhMucCongTac}.Code = {TDKH.TBL_ChiTietCongTacTheoKy}.CodeCongTac " +
                $"JOIN {MyConstant.TBL_THONGTINHANGMUC} " +
                $"ON {MyConstant.TBL_THONGTINHANGMUC}.Code = {TDKH.TBL_DanhMucCongTac}.CodeHangMuc " +
                $"JOIN {MyConstant.TBL_THONGTINCONGTRINH} " +
                $"ON {MyConstant.TBL_THONGTINCONGTRINH}.Code = {MyConstant.TBL_THONGTINHANGMUC}.CodeCongTrinh " +
                $"WHERE {TDKH.TBL_ChiTietCongTacTheoKy}.{colFk} IS NOT NULL AND {TDKH.TBL_ChiTietCongTacTheoKy}.{colFk}<>'{SharedControls.slke_ThongTinDuAn.EditValue}' AND {TDKH.TBL_ChiTietCongTacTheoKy}.CodeGiaiDoan='{SharedControls.cbb_DBKH_ChonDot.SelectedValue}' ";
            DataTable dt = DataProvider.InstanceTHDA.ExecuteQuery(dbString);
            //string[] lstcode = dt.AsEnumerable().Select(x => x["Code"].ToString()).ToArray();

            //List<KLHN> Lst = MyFunction.Fcn_CalKLKHNew(TypeKLHN.CongTac, lstcode,ignoreKLKH:true);
            List<KLHN> Lst = new List<KLHN>();
            List<KLHN> LstCon = new List<KLHN>();
            List<KLTTHangNgay> lsSource = DatatableHelper.fcn_DataTable2List<KLTTHangNgay>(dt);

            List<KLTTHangNgay> lsOut = new List<KLTTHangNgay>();

            var grsDVTH = lsSource.GroupBy(x => x.CodeDVTH);

            foreach (var grDVTH in grsDVTH)
            {
                lsOut.Add(
                        new KLTTHangNgay()
                        {
                            Code = grDVTH.Key,
                            TenCongTac = grDVTH.First().TenDonViThucHien,
                        }
                    );


                var grsCTrinh = grDVTH.GroupBy(x => x.CodeCongTrinh);
                foreach (var grCTrinh in grsCTrinh)
                {
                    string CodeCT = Guid.NewGuid().ToString();
                    lsOut.Add
                        (
                            new KLTTHangNgay()
                            {
                                ParentCode = grDVTH.Key,
                                Code = CodeCT,
                                TenCongTac = grCTrinh.First().TenCongTrinh,
                            }
                        );
                    var grsHM = grCTrinh.GroupBy(x => x.CodeHangMuc);
                    foreach (var grHM in grsHM)
                    {
                        string CodeHM = Guid.NewGuid().ToString();
                        lsOut.Add
                            (
                                new KLTTHangNgay()
                                {
                                    ParentCode = CodeCT,
                                    Code = CodeHM,
                                    TenCongTac = grHM.First().TenHangMuc,
                                }
                            );
                        var grsCTacs = grHM.GroupBy(x => x.Code);
                        foreach (var grCTac in grsCTacs)
                        {
                            string CodeCongtac = Guid.NewGuid().ToString();
                            lsOut.Add
                            (
                                new KLTTHangNgay()
                                {
                                    ParentCode = CodeHM,
                                    Code = CodeCongtac,
                                    TenCongTac = grCTac.First().TenCongTac,
                                }
                            );


                            lsOut.Add
                            (
                                new KLTTHangNgay()
                                {
                                    ParentCode = CodeCongtac,
                                    Code = Guid.NewGuid().ToString(),
                                    TenCongTac = "1. Kế hoạch",
                                    GiaTri = (typeBaoCao == TypeBaoCao.DongTien) ? (double)grCTac.First().ThanhTienKeHoach : (double)grCTac.First().KhoiLuongKeHoach
                                }
                            );
                            Lst = MyFunction.Fcn_CalKLKHNewWithoutKeHoach(TypeKLHN.CongTac,new string[] { grCTac.FirstOrDefault().Code });
                            if (Lst.Any())
                            {
                                LstCon = Lst.Where(x => x.ParentCode == grCTac.FirstOrDefault().Code).Any()?
                                    Lst.Where(x => x.ParentCode == grCTac.FirstOrDefault().Code).ToList():null;
                            }
                            long? TienThiCong = (long)Math.Round(LstCon!=null?(colFk=="CodeNhaThau"? LstCon.Sum(x=>x.ThanhTienThiCong): LstCon.Sum(x => x.ThanhTienThiCong)) ?? 0 : 0);
                            double? KLThiCong = LstCon != null ? LstCon.Sum(x => x.KhoiLuongThiCong) : 0;
                            lsOut.Add
                            (
                                new KLTTHangNgay()
                                {
                                    ParentCode = CodeCongtac,
                                    Code = Guid.NewGuid().ToString(),
                                    TenCongTac = "2. Thi công",
                                    GiaTri = (typeBaoCao == TypeBaoCao.DongTien) ? (double)TienThiCong : KLThiCong
                                }
                            );

                            lsOut.Add
                            (
                                new KLTTHangNgay()
                                {
                                    ParentCode = CodeCongtac,
                                    Code = Guid.NewGuid().ToString(),
                                    TenCongTac = "3. Còn lại",
                                    GiaTri = (typeBaoCao == TypeBaoCao.DongTien) ? (TienThiCong - (double)grCTac.First().ThanhTienKeHoach ?? 0) : (KLThiCong - grCTac.First().KhoiLuongKeHoach)
                                }
                            );
                        }
                    }

                }
            }

            return lsOut;
        }
        public static ExcelHTML fcn_ExportHTML(CellRange range, CellRange rangeHeader = null, Worksheet wsToShowData = null)
        {
            Worksheet ws = range.Worksheet;
            HtmlDocumentExporterOptions options = new HtmlDocumentExporterOptions();
            MemoryStream st = new MemoryStream();
            Workbook workBookTemp = new Workbook();
            workBookTemp.Styles[DevExpress.Spreadsheet.BuiltInStyleId.Normal].Font.Name = "Times New Roman";
            range.Font.Name = "Times New Roman";
            if (wsToShowData == null)
            {
                wsToShowData = workBookTemp.Worksheets[0];
            }

            if (rangeHeader != null)
            {
                wsToShowData.Cells["A1"].CopyFrom(rangeHeader);
                wsToShowData.Rows[rangeHeader.RowCount][0].CopyFrom(range);
                options.SheetIndex = wsToShowData.Index;
                options.Range = wsToShowData.Range.FromLTRB(0, 0, Math.Max(range.ColumnCount, rangeHeader.ColumnCount) - 1, range.RowCount + rangeHeader.RowCount).GetReferenceA1();
                options.UseCssForWidthAndHeight = true;

                IWorkbook wb = wsToShowData.Workbook;
                wb.ExportToHtml(st, options);
            }
            else
            {
                wsToShowData.Cells["A1"].CopyFrom(range);
                options.SheetIndex = ws.Index;
                options.Range = range.GetReferenceA1();
                options.UseCssForWidthAndHeight = true;
                wsToShowData.GetUsedRange().Font.Name = "Times New Roman";
                ws.Workbook.ExportToHtml(st, options);
            }
            workBookTemp.Dispose();
            return new ExcelHTML() { stream = st, ecd = options.Encoding };
        }
        public static List<KLTTHangNgay> GetAllThanhTienVatLieuTest(TypeDVTH typeDVTH, TypeBaoCao typeBaoCao)
        {
            List<KLTTHangNgay> lsOut = new List<KLTTHangNgay>();
            string colFk = MyConstant.lsColFkDVTH[(int)typeDVTH];
            string tbl = MyConstant.lsTblDVTH[(int)typeDVTH];
            string dbString = $"SELECT * FROM  {tbl} WHERE \"CodeDuAn\"='{SharedControls.slke_ThongTinDuAn.EditValue}' ";
            DataTable dtnhathau = DataProvider.InstanceTHDA.ExecuteQuery(dbString);

            dbString = $"SELECT {TDKH.TBL_KHVT_KhoiLuongHangNgay}.*,{TDKH.TBL_KHVT_VatTu}.Code AS CodeCongTac,{TDKH.TBL_KHVT_KhoiLuongHangNgay}.Code AS CodeHangNgay,{TDKH.TBL_KHVT_VatTu}.DonGia AS DonGiaKeHoach," +
                $" {TDKH.TBL_KHVT_VatTu}.DonGiaThiCong, {TDKH.TBL_KHVT_VatTu}.VatTu as TenCongTac,{TDKH.TBL_KHVT_VatTu}.MaVatLieu as MaCongTac,{TDKH.TBL_KHVT_VatTu}.DonVi,  " +
                $"{MyConstant.TBL_THONGTINHANGMUC}.Code AS CodeHangMuc, {MyConstant.TBL_THONGTINHANGMUC}.Ten AS TenHangMuc, " +
                $"{MyConstant.TBL_THONGTINCONGTRINH}.Code AS CodeCongTrinh, {MyConstant.TBL_THONGTINCONGTRINH}.Ten AS TenCongTrinh " +
                $"FROM {TDKH.TBL_KHVT_KhoiLuongHangNgay} " +
                $"JOIN {TDKH.TBL_KHVT_VatTu} ON {TDKH.TBL_KHVT_VatTu}.Code={TDKH.TBL_KHVT_KhoiLuongHangNgay}.CodeVatTu " +
                $"JOIN {MyConstant.TBL_THONGTINHANGMUC} ON {MyConstant.TBL_THONGTINHANGMUC}.Code={TDKH.TBL_KHVT_VatTu}.CodeHangMuc " +
                $"JOIN {MyConstant.TBL_THONGTINCONGTRINH} " +
                $"ON {MyConstant.TBL_THONGTINCONGTRINH}.Code = {MyConstant.TBL_THONGTINHANGMUC}.CodeCongTrinh " +
                $"WHERE {MyConstant.TBL_THONGTINCONGTRINH}.CodeDuAn='{SharedControls.slke_ThongTinDuAn.EditValue}'";
            DataTable dt = DataProvider.InstanceTHDA.ExecuteQuery(dbString);
            List<KLTTHangNgay> lsSource = DatatableHelper.fcn_DataTable2List<KLTTHangNgay>(dt);
            var lsCon = new List<KLTTHangNgay>();
            foreach(KLTTHangNgay item in lsSource)
            {
                List<Tbl_ThongTinNhaCungCapViewModel> _nccs = DuAnHelper.GetAllNhaCungCapOfCurrentPrj();
                var NccsVM = MyFunction.TryGetObjFromJson<NhaCungCapHangNgayViewModel>(item.NhaCungCap);
                if (!NccsVM.Any())
                    continue;
                Dictionary<string, string> _DicNccs = _nccs.ToDictionary(x => x.Code, x => x.Ten);
                if (_DicNccs is null)
                {
                    var nccs = DuAnHelper.GetAllNhaCungCapOfCurrentPrj();
                    _DicNccs = nccs.ToDictionary(x => x.Code, x => x.Ten);
                }
                foreach (var ncc in NccsVM)
                {
                    lsCon.Add(new KLTTHangNgay()
                    {
                        ParentCode = item.CodeCha,
                        TenCongTac =item.TenCongTac,
                        DonVi=item.DonVi,
                        MaCongTac=item.MaCongTac,
                        Code = Guid.NewGuid().ToString(),
                        CodeNhaCungCap = ncc.Code,
                        KhoiLuongThiCong = ncc.KhoiLuong,
                        DonGiaThiCong = ncc.DonGia,
                        CodeHangMuc=item.CodeHangMuc
                        
                        
                    });
                }
            }
            dbString = $"SELECT {tbl}.Code AS CodeDVTH, {tbl}.Ten AS TenDVTH, " +
                $"{QLVT.TBL_QLVT_NHAPVT}.Code AS CodeCongTac,{QLVT.TBL_QLVT_YEUCAUVT}.TenVatTu as TenCongTac,{QLVT.TBL_QLVT_YEUCAUVT}.MaVatTu as MaCongTac," +
                $"{QLVT.TBL_QLVT_YEUCAUVT}.DonVi,{QLVT.TBL_QLVT_YEUCAUVT}.HopDongKl as KhoiLuongHDVatLieu,{QLVT.TBL_QLVT_YEUCAUVT}.DonGiaHienTruong as DonGiaHDVatLieu , " +
                $"{MyConstant.TBL_THONGTINHANGMUC}.Code AS CodeHangMuc, {MyConstant.TBL_THONGTINHANGMUC}.Ten AS TenHangMuc, " +
                $"{MyConstant.TBL_THONGTINCONGTRINH}.Code AS CodeCongTrinh, {MyConstant.TBL_THONGTINCONGTRINH}.Ten AS TenCongTrinh, " +
                $"{QLVT.TBL_QLVT_NHAPVTKLHN}.KhoiLuong AS KhoiLuongVatLieu,{QLVT.TBL_QLVT_NHAPVTKLHN}.DonGia AS DonGiaVatLieu,{QLVT.TBL_QLVT_NHAPVTKLHN}.Ngay " +
                $"FROM {tbl} " +
                $"LEFT JOIN {QLVT.TBL_QLVT_YEUCAUVT} " +
                $"ON {QLVT.TBL_QLVT_YEUCAUVT}.TenNhaCungCap = {tbl}.Code " +
                $"LEFT JOIN {QLVT.TBL_QLVT_NHAPVT} " +
                $"ON {QLVT.TBL_QLVT_YEUCAUVT}.Code = {QLVT.TBL_QLVT_NHAPVT}.CodeDeXuat " +
                $"LEFT JOIN {QLVT.TBL_QLVT_NHAPVTKLHN} ON " +
                $"{QLVT.TBL_QLVT_NHAPVTKLHN}.CodeCha = {QLVT.TBL_QLVT_NHAPVT}.Code " +
                $"JOIN {MyConstant.TBL_THONGTINHANGMUC} " +
                $"ON {MyConstant.TBL_THONGTINHANGMUC}.Code = {QLVT.TBL_QLVT_YEUCAUVT}.CodeHangMuc " +
                $"JOIN {MyConstant.TBL_THONGTINCONGTRINH} " +
                $"ON {MyConstant.TBL_THONGTINCONGTRINH}.Code = {MyConstant.TBL_THONGTINHANGMUC}.CodeCongTrinh " +
                $"WHERE {QLVT.TBL_QLVT_YEUCAUVT}.CodeGiaiDoan='{SharedControls.cbb_DBKH_ChonDot.SelectedValue}'";
            dt = DataProvider.InstanceTHDA.ExecuteQuery(dbString);

            List<KLTTHangNgay> lsSourceQLVC = DatatableHelper.fcn_DataTable2List<KLTTHangNgay>(dt);
            dbString = $"SELECT {TDKH.TBL_KHVT_VatTu}.*,{TDKH.TBL_KHVT_VatTu}.Code AS CodeCha,{TDKH.TBL_KHVT_VatTu}.DonGia AS DonGiaKeHoach," +
    $" {TDKH.TBL_KHVT_VatTu}.VatTu as TenCongTac,{TDKH.TBL_KHVT_VatTu}.MaVatLieu as MaCongTac,  " +
    $"{MyConstant.TBL_THONGTINHANGMUC}.Code AS CodeHangMuc, {MyConstant.TBL_THONGTINHANGMUC}.Ten AS TenHangMuc, " +
    $"{MyConstant.TBL_THONGTINCONGTRINH}.Code AS CodeCongTrinh, {MyConstant.TBL_THONGTINCONGTRINH}.Ten AS TenCongTrinh " +
    $"FROM {TDKH.TBL_KHVT_VatTu} " +
    $"JOIN {MyConstant.TBL_THONGTINHANGMUC} ON {MyConstant.TBL_THONGTINHANGMUC}.Code={TDKH.TBL_KHVT_VatTu}.CodeHangMuc " +
    $"JOIN {MyConstant.TBL_THONGTINCONGTRINH} " +
    $"ON {MyConstant.TBL_THONGTINCONGTRINH}.Code = {MyConstant.TBL_THONGTINHANGMUC}.CodeCongTrinh " +
    $"WHERE {MyConstant.TBL_THONGTINCONGTRINH}.CodeDuAn='{SharedControls.slke_ThongTinDuAn.EditValue}' AND {TDKH.TBL_KHVT_VatTu}.CodeNhaThau IS NOT NULL";
            dt = DataProvider.InstanceTHDA.ExecuteQuery(dbString);

            List<KLTTHangNgay> lsSourceKHVT = DatatableHelper.fcn_DataTable2List<KLTTHangNgay>(dt);
            foreach(DataRow row in dtnhathau.Rows)
            {
                string CodeNhaThau = row["Code"].ToString();
                string VL = Guid.NewGuid().ToString();
                string MTC = Guid.NewGuid().ToString();
                string NC = Guid.NewGuid().ToString();
                lsOut.Add(new KLTTHangNgay()
                {
                    Code =CodeNhaThau ,
                    TenCongTac = row["Ten"].ToString(),
                });         
                lsOut.Add(new KLTTHangNgay()
                {
                    Code =VL ,
                    ParentCode=CodeNhaThau,
                    TenCongTac = "VẬT LIỆU",
                });   
                lsOut.Add(new KLTTHangNgay()
                {
                    ParentCode = CodeNhaThau,
                    Code =MTC ,
                    TenCongTac = "NHÂN CÔNG",
                });
                           
                lsOut.Add(new KLTTHangNgay()
                {
                    ParentCode = CodeNhaThau,
                    Code =NC,
                    TenCongTac ="MÁY THI CÔNG",
                });
                List<KLTTHangNgay> lsSourceVL = lsSourceKHVT.Where(x => x.LoaiVatTu == "Vật liệu").ToList();
                List<KLTTHangNgay> lsSourceMTC = lsSourceKHVT.Where(x => x.LoaiVatTu == "Máy thi công").ToList();
                List<KLTTHangNgay> lsSourceNC = lsSourceKHVT.Where(x => x.LoaiVatTu == "Nhân công").ToList();

                List<KLTTHangNgay>  SourceVL=Fcn_GetDataHMCTTENVL(lsSourceVL, lsSourceQLVC, VL, lsCon, CodeNhaThau, typeBaoCao);
                List<KLTTHangNgay>  SourceMTC=Fcn_GetDataHMCTTENVL(lsSourceMTC,null, MTC, lsCon, CodeNhaThau, typeBaoCao);
                List < KLTTHangNgay > SourceNC= Fcn_GetDataHMCTTENVL(lsSourceNC,null, NC, lsCon, CodeNhaThau, typeBaoCao);
                if(SourceVL!=null)
                    lsOut.AddRange(SourceVL);
                if (SourceNC != null)
                    lsOut.AddRange(SourceNC);
                if (SourceMTC != null)
                    lsOut.AddRange(SourceMTC);

            }
            return lsOut;

        } 
        public static List<KLTTHangNgay> GetAllThanhTienChiTietVatLieu(TypeDVTH typeDVTH)
        {
            List<KLTTHangNgay> lsOut = new List<KLTTHangNgay>();
            string colFk = MyConstant.lsColFkDVTH[(int)typeDVTH];
            string tbl = MyConstant.lsTblDVTH[(int)typeDVTH];
            string dbString = $"SELECT * FROM  {tbl} WHERE \"CodeDuAn\"='{SharedControls.slke_ThongTinDuAn.EditValue}' ";
            DataTable dtnhathau = DataProvider.InstanceTHDA.ExecuteQuery(dbString);

            dbString = $"SELECT {TDKH.TBL_KHVT_KhoiLuongHangNgay}.*,{TDKH.TBL_KHVT_VatTu}.Code AS CodeCha,{TDKH.TBL_KHVT_KhoiLuongHangNgay}.Code AS CodeHangNgay,{TDKH.TBL_KHVT_VatTu}.DonGia AS DonGiaKeHoach," +
                $" {TDKH.TBL_KHVT_VatTu}.DonGiaThiCong, {TDKH.TBL_KHVT_VatTu}.VatTu as TenCongTac,{TDKH.TBL_KHVT_VatTu}.MaVatLieu as MaCongTac,{TDKH.TBL_KHVT_VatTu}.DonVi,  " +
                $"{MyConstant.TBL_THONGTINHANGMUC}.Code AS CodeHangMuc, {MyConstant.TBL_THONGTINHANGMUC}.Ten AS TenHangMuc, " +
                $"{MyConstant.TBL_THONGTINCONGTRINH}.Code AS CodeCongTrinh, {MyConstant.TBL_THONGTINCONGTRINH}.Ten AS TenCongTrinh " +
                $"FROM {TDKH.TBL_KHVT_KhoiLuongHangNgay} " +
                $"JOIN {TDKH.TBL_KHVT_VatTu} ON {TDKH.TBL_KHVT_VatTu}.Code={TDKH.TBL_KHVT_KhoiLuongHangNgay}.CodeVatTu " +
                $"JOIN {MyConstant.TBL_THONGTINHANGMUC} ON {MyConstant.TBL_THONGTINHANGMUC}.Code={TDKH.TBL_KHVT_VatTu}.CodeHangMuc " +
                $"JOIN {MyConstant.TBL_THONGTINCONGTRINH} " +
                $"ON {MyConstant.TBL_THONGTINCONGTRINH}.Code = {MyConstant.TBL_THONGTINHANGMUC}.CodeCongTrinh " +
                $"WHERE {MyConstant.TBL_THONGTINCONGTRINH}.CodeDuAn='{SharedControls.slke_ThongTinDuAn.EditValue}'";
            DataTable dt = DataProvider.InstanceTHDA.ExecuteQuery(dbString);
            List<KLTTHangNgay> lsSource = DatatableHelper.fcn_DataTable2List<KLTTHangNgay>(dt);
            var lsCon = new List<KLTTHangNgay>();
            foreach (KLTTHangNgay item in lsSource)
            {
                List<Tbl_ThongTinNhaCungCapViewModel> _nccs = DuAnHelper.GetAllNhaCungCapOfCurrentPrj();
                var NccsVM = MyFunction.TryGetObjFromJson<NhaCungCapHangNgayViewModel>(item.NhaCungCap);
                if (!NccsVM.Any())
                    continue;
                Dictionary<string, string> _DicNccs = _nccs.ToDictionary(x => x.Code, x => x.Ten);
                if (_DicNccs is null)
                {
                    var nccs = DuAnHelper.GetAllNhaCungCapOfCurrentPrj();
                    _DicNccs = nccs.ToDictionary(x => x.Code, x => x.Ten);
                }
                foreach (var ncc in NccsVM)
                {
                    lsCon.Add(new KLTTHangNgay()
                    {
                        ParentCode = item.CodeCha,
                        TenCongTac = item.TenCongTac,
                        DonVi = item.DonVi,
                        MaCongTac = item.MaCongTac,
                        Code = Guid.NewGuid().ToString(),
                        CodeNhaCungCap = ncc.Code,
                        KhoiLuongThiCong = ncc.KhoiLuong,
                        DonGiaThiCong = ncc.DonGia,
                        
                        
                   });
                }
            }
            dbString = $"SELECT {tbl}.Code AS CodeDVTH, {tbl}.Ten AS TenDVTH, " +
                $"{QLVT.TBL_QLVT_NHAPVT}.Code AS CodeCongTac,{QLVT.TBL_QLVT_YEUCAUVT}.TenVatTu as TenCongTac,{QLVT.TBL_QLVT_YEUCAUVT}.MaVatTu as MaCongTac," +
                $"{QLVT.TBL_QLVT_YEUCAUVT}.DonVi,{QLVT.TBL_QLVT_YEUCAUVT}.HopDongKl as KhoiLuongHDVatLieu,{QLVT.TBL_QLVT_YEUCAUVT}.DonGiaHienTruong as DonGiaHDVatLieu , " +
                $"{MyConstant.TBL_THONGTINHANGMUC}.Code AS CodeHangMuc, {MyConstant.TBL_THONGTINHANGMUC}.Ten AS TenHangMuc, " +
                $"{MyConstant.TBL_THONGTINCONGTRINH}.Code AS CodeCongTrinh, {MyConstant.TBL_THONGTINCONGTRINH}.Ten AS TenCongTrinh, " +
                $"{QLVT.TBL_QLVT_NHAPVTKLHN}.KhoiLuong AS KhoiLuongVatLieu,{QLVT.TBL_QLVT_NHAPVTKLHN}.DonGia AS DonGiaVatLieu " +
                $"FROM {tbl} " +
                $"LEFT JOIN {QLVT.TBL_QLVT_YEUCAUVT} " +
                $"ON {QLVT.TBL_QLVT_YEUCAUVT}.TenNhaCungCap = {tbl}.Code " +
                $"LEFT JOIN {QLVT.TBL_QLVT_NHAPVT} " +
                $"ON {QLVT.TBL_QLVT_YEUCAUVT}.Code = {QLVT.TBL_QLVT_NHAPVT}.CodeDeXuat " +
                $"LEFT JOIN {QLVT.TBL_QLVT_NHAPVTKLHN} ON " +
                $"{QLVT.TBL_QLVT_NHAPVTKLHN}.CodeCha = {QLVT.TBL_QLVT_NHAPVT}.Code " +
                $"JOIN {MyConstant.TBL_THONGTINHANGMUC} " +
                $"ON {MyConstant.TBL_THONGTINHANGMUC}.Code = {QLVT.TBL_QLVT_YEUCAUVT}.CodeHangMuc " +
                $"JOIN {MyConstant.TBL_THONGTINCONGTRINH} " +
                $"ON {MyConstant.TBL_THONGTINCONGTRINH}.Code = {MyConstant.TBL_THONGTINHANGMUC}.CodeCongTrinh " +
                $"WHERE {QLVT.TBL_QLVT_YEUCAUVT}.CodeGiaiDoan='{SharedControls.cbb_DBKH_ChonDot.SelectedValue}'";
            dt = DataProvider.InstanceTHDA.ExecuteQuery(dbString);

            List<KLTTHangNgay> lsSourceQLVC = DatatableHelper.fcn_DataTable2List<KLTTHangNgay>(dt);
            dbString = $"SELECT {TDKH.TBL_KHVT_VatTu}.*,{TDKH.TBL_KHVT_VatTu}.Code AS CodeCha,{TDKH.TBL_KHVT_VatTu}.DonGiaThiCong AS DonGiaKeHoach," +
    $" {TDKH.TBL_KHVT_VatTu}.VatTu as TenCongTac,{TDKH.TBL_KHVT_VatTu}.MaVatLieu as MaCongTac,  " +
    $"{MyConstant.TBL_THONGTINHANGMUC}.Code AS CodeHangMuc, {MyConstant.TBL_THONGTINHANGMUC}.Ten AS TenHangMuc, " +
    $"{MyConstant.TBL_THONGTINCONGTRINH}.Code AS CodeCongTrinh, {MyConstant.TBL_THONGTINCONGTRINH}.Ten AS TenCongTrinh " +
    $"FROM {TDKH.TBL_KHVT_VatTu} " +
    $"JOIN {MyConstant.TBL_THONGTINHANGMUC} ON {MyConstant.TBL_THONGTINHANGMUC}.Code={TDKH.TBL_KHVT_VatTu}.CodeHangMuc " +
    $"JOIN {MyConstant.TBL_THONGTINCONGTRINH} " +
    $"ON {MyConstant.TBL_THONGTINCONGTRINH}.Code = {MyConstant.TBL_THONGTINHANGMUC}.CodeCongTrinh " +
    $"WHERE {MyConstant.TBL_THONGTINCONGTRINH}.CodeDuAn='{SharedControls.slke_ThongTinDuAn.EditValue}' AND {TDKH.TBL_KHVT_VatTu}.CodeNhaThau IS NOT NULL";
            dt = DataProvider.InstanceTHDA.ExecuteQuery(dbString);

            List<KLTTHangNgay> lsSourceKHVT = DatatableHelper.fcn_DataTable2List<KLTTHangNgay>(dt);
            foreach (DataRow row in dtnhathau.Rows)
            {
                string CodeNhaThau = row["Code"].ToString();
                string VL = Guid.NewGuid().ToString();
                string MTC = Guid.NewGuid().ToString();
                string NC = Guid.NewGuid().ToString();
                lsOut.Add(new KLTTHangNgay()
                {
                    Code = CodeNhaThau,
                    TenCongTac = row["Ten"].ToString(),
                });
                lsOut.Add(new KLTTHangNgay()
                {
                    Code = VL,
                    ParentCode = CodeNhaThau,
                    TenCongTac = "VẬT LIỆU",
                });
                lsOut.Add(new KLTTHangNgay()
                {
                    ParentCode = CodeNhaThau,
                    Code = MTC,
                    TenCongTac = "NHÂN CÔNG",
                });
                           
                lsOut.Add(new KLTTHangNgay()
                {
                    ParentCode = CodeNhaThau,
                    Code = NC,
                    TenCongTac = "MÁY THI CÔNG",
                });
                List<KLTTHangNgay> lsSourceVL = lsSourceKHVT.Where(x => x.LoaiVatTu == "Vật liệu").ToList();
                List<KLTTHangNgay> lsSourceMTC = lsSourceKHVT.Where(x => x.LoaiVatTu == "Máy thi công").ToList();
                List<KLTTHangNgay> lsSourceNC = lsSourceKHVT.Where(x => x.LoaiVatTu == "Nhân công").ToList();

                List<KLTTHangNgay> SourceVL = Fcn_GetDataHMCTTENVLChiTiet(lsSourceVL, lsSourceQLVC, VL, lsCon, CodeNhaThau);
                List<KLTTHangNgay> SourceMTC = Fcn_GetDataHMCTTENVLChiTiet(lsSourceMTC, null, MTC, lsCon, CodeNhaThau);
                List<KLTTHangNgay> SourceNC = Fcn_GetDataHMCTTENVLChiTiet(lsSourceNC, null, NC, lsCon, CodeNhaThau);
                lsOut.AddRange(SourceVL);
                lsOut.AddRange(SourceNC);
                lsOut.AddRange(SourceMTC);

            }
            return lsOut;

        }
        public static List<KLTTHangNgay> Fcn_GetDataHMCTTENVL(List<KLTTHangNgay> lsSource,List<KLTTHangNgay> lsSourceQLVC,string ParentCode, List<KLTTHangNgay> lsCon,string CodeNhaThau, TypeBaoCao typeBaoCao)
        {
            List<KLTTHangNgay> lsOut = new List<KLTTHangNgay>();
            if (lsSource.Count() == 0)
                return null;
            var grsCTrinh = lsSource.GroupBy(x => x.CodeCongTrinh);
            foreach (var grCTrinh in grsCTrinh)
            {
                string CodeCT = Guid.NewGuid().ToString();
                lsOut.Add
                    (
                        new KLTTHangNgay()
                        {
                            ParentCode = ParentCode,
                            Code = CodeCT,
                            TenCongTac = grCTrinh.First().TenCongTrinh,
                        }
                    );
                var grsHM = grCTrinh.GroupBy(x => x.CodeHangMuc);
                foreach (var grHM in grsHM)
                {
                    string CodeHM = Guid.NewGuid().ToString();
                    lsOut.Add
                        (
                            new KLTTHangNgay()
                            {
                                ParentCode = CodeCT,
                                Code = CodeHM,
                                TenCongTac = grHM.First().TenHangMuc,
                            }
                        );
                    var grsCTacs = grHM.GroupBy(x => x.CodeCha);
                    foreach (var grCTac in grsCTacs)
                    {
                        double? KLTC = 0;
                        long? TTTC = 0;
                        if (lsSource.FirstOrDefault().LoaiVatTu=="Vật liệu")
                        {
                            if (lsSourceQLVC != null)
                            {
                            List<KLTTHangNgay> CrQLVC=lsSourceQLVC.Where(x => x.CodeDVTH == CodeNhaThau && x.CodeHangMuc == grHM.Key && x.MaCongTac == grCTac.First().MaCongTac && x.DonVi == grCTac.First().DonVi && x.TenCongTac == grCTac.First().TenCongTac).ToList();
                            KLTC = CrQLVC.Count() == 0 ? 0 : CrQLVC.Sum(x => x.KhoiLuongVatLieu);
                            TTTC = CrQLVC.Count() == 0 ? 0 : (long)Math.Round(CrQLVC.Sum(x => x.ThanhTienVatLieu) ?? 0);
                            }

                            List<KLTTHangNgay> CrVL = lsCon.Where(x => x.CodeNhaCungCap == CodeNhaThau&& x.CodeHangMuc == grHM.Key && x.MaCongTac == grCTac.First().MaCongTac && x.DonVi == grCTac.First().DonVi && x.TenCongTac == grCTac.First().TenCongTac).ToList();
                            KLTC +=CrVL.Count() == 0 ? 0 : CrVL.Sum(x => x.KhoiLuongThiCong);
                            TTTC +=CrVL.Count() == 0 ? 0 : (long)Math.Round(CrVL.Sum(x => x.ThanhTienThiCong) ??0);
                        }
                        else
                        {
                            List<KLTTHangNgay> CrMTC = lsCon.Where(x => x.CodeNhaCungCap == CodeNhaThau && x.ParentCode == grCTac.Key).ToList();
                            KLTC = CrMTC.Count() == 0 ? 0 : CrMTC.Sum(x => x.KhoiLuongThiCong);
                            TTTC = CrMTC.Count() == 0 ? 0 : (long)Math.Round(CrMTC.Sum(x => x.ThanhTienThiCong)??0);
                        }
                        if (KLTC == 0)
                            continue;
                        string CodeCongtac = Guid.NewGuid().ToString();
                        lsOut.Add
                        (
                            new KLTTHangNgay()
                            {
                                ParentCode = CodeHM,
                                Code = CodeCongtac,
                                TenCongTac = grCTac.First().TenCongTac,
                            }
                        );

                        lsOut.Add
                         (
                             new KLTTHangNgay()
                             {
                                 ParentCode = CodeCongtac,
                                 Code = Guid.NewGuid().ToString(),
                                 TenCongTac = "1. Hợp đồng",
                                 GiaTri = (typeBaoCao == TypeBaoCao.DongTien) ? (double)grCTac.First().ThanhTienHopDong : (double)grCTac.First().KhoiLuongHopDong
                             }
                         );
                        lsOut.Add
                           (
                               new KLTTHangNgay()
                               {
                                   ParentCode = CodeCongtac,
                                   Code = Guid.NewGuid().ToString(),
                                   TenCongTac = "2. Thi công",
                                   GiaTri = (typeBaoCao == TypeBaoCao.DongTien) ? (double)TTTC : KLTC
                               }
                           );

                        lsOut.Add
                        (
                            new KLTTHangNgay()
                            {
                                ParentCode = CodeCongtac,
                                Code = Guid.NewGuid().ToString(),
                                TenCongTac = "3. Còn lại",
                                GiaTri = (typeBaoCao == TypeBaoCao.DongTien) ? ((double)grCTac.First().ThanhTienHopDong - TTTC?? 0) : (grCTac.First().KhoiLuongHopDong - KLTC)
                            }
                        );
                    }
                }
            }
            return lsOut;
        }     
        public static List<KLTTHangNgay> Fcn_GetDataHMCTTENVLChiTiet(List<KLTTHangNgay> lsSource,List<KLTTHangNgay> lsSourceQLVC,string ParentCode, List<KLTTHangNgay> lsCon,string CodeNhaThau)
        {
            List<KLTTHangNgay> lsOut = new List<KLTTHangNgay>();
            if (lsSource.Count() == 0)
                return null;
            var grsCTrinh = lsSource.GroupBy(x => x.CodeCongTrinh);
            foreach (var grCTrinh in grsCTrinh)
            {
                string CodeCT = Guid.NewGuid().ToString();
                lsOut.Add
                    (
                        new KLTTHangNgay()
                        {
                            ParentCode = ParentCode,
                            Code = CodeCT,
                            TenCongTac = grCTrinh.First().TenCongTrinh,
                        }
                    );
                var grsHM = grCTrinh.GroupBy(x => x.CodeHangMuc);
                foreach (var grHM in grsHM)
                {
                    string CodeHM = Guid.NewGuid().ToString();
                    lsOut.Add
                        (
                            new KLTTHangNgay()
                            {
                                ParentCode = CodeCT,
                                Code = CodeHM,
                                TenCongTac = grHM.First().TenHangMuc,
                            }
                        );
                    var grsCTacs = grHM.GroupBy(x => x.CodeCha);
                    foreach (var grCTac in grsCTacs)
                    {
                        double? KLTC = 0;
                        long? TTTC = 0,TTTCGoc=0;
                        if (lsSource.FirstOrDefault().LoaiVatTu=="Vật liệu")
                        {
                            if (lsSourceQLVC != null)
                            {
                            List<KLTTHangNgay> CrQLVC=lsSourceQLVC.Where(x => x.CodeDVTH == CodeNhaThau && x.CodeHangMuc == grHM.Key && x.MaCongTac == grCTac.First().MaCongTac && x.DonVi == grCTac.First().DonVi && x.TenCongTac == grCTac.First().TenCongTac).ToList();
                                KLTC = CrQLVC.Count() == 0 ? 0 : CrQLVC.Sum(x => x.KhoiLuongVatLieu);
                                TTTC = CrQLVC.Count() == 0 ? 0 : (long)Math.Round(CrQLVC.Sum(x => x.ThanhTienVatLieu)??0);
                            }

                            List<KLTTHangNgay> CrVL = lsCon.Where(x => x.CodeNhaCungCap == CodeNhaThau && x.MaCongTac == grCTac.First().MaCongTac && x.DonVi == grCTac.First().DonVi && x.TenCongTac == grCTac.First().TenCongTac).ToList();
                            KLTC += CrVL.Count() == 0 ? 0 : CrVL.Sum(x => x.KhoiLuongThiCong);
                            TTTC +=CrVL.Count() == 0 ? 0 : (long)Math.Round(CrVL.Sum(x => x.ThanhTienThiCong)??0);
                            TTTCGoc = (long)Math.Round((double)KLTC * (double)grCTac.First().DonGiaKeHoach);
                        }
                        else
                        {
                            List<KLTTHangNgay> CrMTC = lsCon.Where(x => x.CodeNhaCungCap == CodeNhaThau && x.ParentCode == grCTac.Key).ToList();
                            KLTC = CrMTC.Count() == 0 ? 0 : CrMTC.Sum(x => x.KhoiLuongThiCong);
                            TTTC = CrMTC.Count() == 0 ? 0 : (long)Math.Round(CrMTC.Sum(x => x.ThanhTienThiCong)??0);
                            TTTCGoc = (long)Math.Round((double)KLTC * (double)grCTac.First().DonGiaKeHoach);

                        }
                        string CodeCongtac = Guid.NewGuid().ToString();
                        lsOut.Add
                        (
                            new KLTTHangNgay()
                            {
                                ParentCode = CodeHM,
                                Code = CodeCongtac,
                                TenCongTac = grCTac.First().TenCongTac,
                            }
                        );

                        lsOut.Add
                         (
                             new KLTTHangNgay()
                             {
                                 ParentCode = CodeCongtac,
                                 Code = Guid.NewGuid().ToString(),
                                 TenCongTac = "1. Thu",
                                 GiaTri = TTTCGoc
                             }
                         );
                        lsOut.Add
                           (
                               new KLTTHangNgay()
                               {
                                   ParentCode = CodeCongtac,
                                   Code = Guid.NewGuid().ToString(),
                                   TenCongTac = "2. Chi",
                                   GiaTri = TTTC
                               }
                           );

                        lsOut.Add
                        (
                            new KLTTHangNgay()
                            {
                                ParentCode = CodeCongtac,
                                Code = Guid.NewGuid().ToString(),
                                TenCongTac = "3. Còn lại",
                                GiaTri = TTTCGoc- TTTC
                            }
                        );
                    }
                }
            }
            return lsOut;
        }
        public static List<KLTTHangNgay> GetAllThanhTienVatLieu(TypeDVTH typeDVTH, TypeBaoCao typeBaoCao)
        {
            string colFk = MyConstant.lsColFkDVTH[(int)typeDVTH];
            string tbl = MyConstant.lsTblDVTH[(int)typeDVTH];

            string dbString = $"SELECT {tbl}.Code AS CodeDVTH, {tbl}.Ten AS TenDVTH, " +
                $"{QLVT.TBL_QLVT_NHAPVT}.Code AS CodeCongTac, {QLVT.TBL_QLVT_YEUCAUVT}.TenVatTu as TenCongTac,{QLVT.TBL_QLVT_YEUCAUVT}.HopDongKl as KhoiLuongHDVatLieu,{QLVT.TBL_QLVT_YEUCAUVT}.DonGiaHienTruong as DonGiaHDVatLieu , " +
                $"{MyConstant.TBL_THONGTINHANGMUC}.Code AS CodeHangMuc, {MyConstant.TBL_THONGTINHANGMUC}.Ten AS TenHangMuc, " +
                $"{MyConstant.TBL_THONGTINCONGTRINH}.Code AS CodeCongTrinh, {MyConstant.TBL_THONGTINCONGTRINH}.Ten AS TenCongTrinh, " +
                $"{QLVT.TBL_QLVT_NHAPVTKLHN}.KhoiLuong AS KhoiLuongVatLieu,{QLVT.TBL_QLVT_NHAPVTKLHN}.DonGia AS DonGiaVatLieu " +
                $"FROM {tbl} " +
                $"LEFT JOIN {QLVT.TBL_QLVT_YEUCAUVT} " +
                $"ON {QLVT.TBL_QLVT_YEUCAUVT}.TenNhaCungCap = {tbl}.Code " +  
                $"LEFT JOIN {QLVT.TBL_QLVT_NHAPVT} " +
                $"ON {QLVT.TBL_QLVT_YEUCAUVT}.Code = {QLVT.TBL_QLVT_NHAPVT}.CodeDeXuat " +
                $"LEFT JOIN {QLVT.TBL_QLVT_NHAPVTKLHN} ON " +
                $"{QLVT.TBL_QLVT_NHAPVTKLHN}.CodeCha = {QLVT.TBL_QLVT_NHAPVT}.Code " +
                $"JOIN {MyConstant.TBL_THONGTINHANGMUC} " +
                $"ON {MyConstant.TBL_THONGTINHANGMUC}.Code = {QLVT.TBL_QLVT_YEUCAUVT}.CodeHangMuc " +
                $"JOIN {MyConstant.TBL_THONGTINCONGTRINH} " +
                $"ON {MyConstant.TBL_THONGTINCONGTRINH}.Code = {MyConstant.TBL_THONGTINHANGMUC}.CodeCongTrinh " +
                $"WHERE {QLVT.TBL_QLVT_YEUCAUVT}.CodeGiaiDoan='{SharedControls.cbb_DBKH_ChonDot.SelectedValue}'";

            DataTable dt = DataProvider.InstanceTHDA.ExecuteQuery(dbString);
 
            List<KLTTHangNgay> lsSource = DatatableHelper.fcn_DataTable2List<KLTTHangNgay>(dt);
           
            List<KLTTHangNgay> lsOut = new List<KLTTHangNgay>();

            var grsDVTH = lsSource.GroupBy(x => x.CodeDVTH);

            foreach (var grDVTH in grsDVTH)
            {
                lsOut.Add(
                        new KLTTHangNgay()
                        {
                            Code = grDVTH.Key,
                            TenCongTac = grDVTH.First().TenDonViThucHien,
                            //GiaTri = grDVTH.Where(x => x.ThanhTienKeHoachSaved.HasValue).Sum(x => x.ThanhTienKeHoachSaved.Value)
                        }
                    );


                var grsCTrinh = grDVTH.GroupBy(x => x.CodeCongTrinh);
                foreach (var grCTrinh in grsCTrinh)
                {
                    string CodeCT = Guid.NewGuid().ToString();
                    lsOut.Add
                        (
                            new KLTTHangNgay()
                            {
                                ParentCode = grDVTH.Key,
                                Code = CodeCT,
                                TenCongTac = grCTrinh.First().TenCongTrinh,
                                //KhoiLuongKeHoach = grCTrinh.Where(x => x.KhoiLuongKeHoach.HasValue).Sum(x => x.KhoiLuongKeHoach),
                                //KhoiLuongThiCong = grCTrinh.Where(x => x.KhoiLuongThiCong > 0).Sum(x => x.KhoiLuongThiCong)
                            }
                        );
                    var grsHM = grCTrinh.GroupBy(x => x.CodeHangMuc);
                    foreach (var grHM in grsHM)
                    {
                        string CodeHM = Guid.NewGuid().ToString();
                        lsOut.Add
                            (
                                new KLTTHangNgay()
                                {
                                    ParentCode = CodeCT,
                                    Code = CodeHM,
                                    TenCongTac = grHM.First().TenHangMuc,
                                    //KhoiLuongKeHoach = grHM.Where(x => x.KhoiLuongKeHoach.HasValue).Sum(x => x.KhoiLuongKeHoach),
                                    //KhoiLuongThiCong = grHM.Where(x => x.KhoiLuongThiCong > 0).Sum(x => x.KhoiLuongThiCong)
                                }
                            );
                        var grsCTacs = grHM.GroupBy(x => x.CodeCha);
                        foreach (var grCTac in grsCTacs)
                        {
                            string CodeCongtac = Guid.NewGuid().ToString();
                            lsOut.Add
                            (
                                new KLTTHangNgay()
                                {
                                    ParentCode = CodeHM,
                                    Code = CodeCongtac,
                                    TenCongTac = grCTac.First().TenCongTac,
                                }
                            );

                            lsOut.Add
                            (
                                new KLTTHangNgay()
                                {
                                    ParentCode = CodeCongtac,
                                    Code = Guid.NewGuid().ToString(),
                                    TenCongTac = "1. Hợp đồng",
                                    GiaTri = (typeBaoCao == TypeBaoCao.DongTien) ? (double)grCTac.First().ThanhTienHDVatLieu : (double)grCTac.First().KhoiLuongHDVatLieu
                                }
                            );

                            //lsOut.Add
                            //(
                            //    new KLTTHangNgay()
                            //    {
                            //        ParentCode = grCTac.Key,
                            //        Code = Guid.NewGuid().ToString(),
                            //        TenCongTac = "2. Kế hoạch",
                            //        GiaTri = (typeBaoCao == TypeBaoCao.DongTien) ? (double)grCTac.First().ThanhTienKeHoach : (double)grCTac.First().KhoiLuongKeHoach
                            //    }
                            //);

                            long? TienThiCong = (long)Math.Round(grCTac.Where(x => x.ThanhTienVatLieu.HasValue).Sum(x => x.ThanhTienVatLieu.Value));
                            double? KLThiCong = grCTac.Where(x => x.KhoiLuongVatLieu.HasValue).Sum(x => x.KhoiLuongVatLieu.Value);
                            lsOut.Add
                            (
                                new KLTTHangNgay()
                                {
                                    ParentCode = CodeCongtac,
                                    Code = Guid.NewGuid().ToString(),
                                    TenCongTac = "2. Thi công",
                                    GiaTri = (typeBaoCao == TypeBaoCao.DongTien) ? (double)TienThiCong : KLThiCong
                                }
                            );

                            lsOut.Add
                            (
                                new KLTTHangNgay()
                                {
                                    ParentCode = CodeCongtac,
                                    Code = Guid.NewGuid().ToString(),
                                    TenCongTac = "3. Còn lại",
                                    GiaTri = (typeBaoCao == TypeBaoCao.DongTien) ? ((double)grCTac.First().ThanhTienHDVatLieu - TienThiCong ?? 0) : (grCTac.First().KhoiLuongHDVatLieu - KLThiCong) 
                                }
                            );
                        }
                    }

                }
            }

            return lsOut;
        }

        private static void PreventChangeTabs(object sender, TabPageChangingEventArgs e)
        {
            e.Cancel = true;
        }

        public static void fcn_updateCbbThongTinDuAnCongTrinh()
        {
            var slke_ThongTinDuAn = SharedControls.slke_ThongTinDuAn;
            //slke_ThongTinDuAn.SelectedIndexChanged -= slke_ThongTinDuAn_SelectedIndexChanged;
            //slke_ThongTinDuAn.Properties.DataSource = null;

            //slke_ThongTinDuAn.EditValue = null;
            
            string db_string = $"SELECT * from {MyConstant.TBL_THONGTINDUAN} " +
                $"WHERE CreatedBySerialno = '{BaseFrom.BanQuyenKeyInfo.SerialNo}' OR IsShareToOtherKey = 1 " +
                $"ORDER BY CreatedOn DESC";
            var DAs = DataProvider.InstanceTHDA.ExecuteQueryModel<Tbl_ThongTinDuAnViewModel>(db_string);

            if (!BaseFrom.allPermission.HaveInitProjectPermission)
            {
                BaseFrom.allPermission.DASOffline = DAs.Where(x => x.CreatedBy == BaseFrom.BanQuyenKeyInfo.Email).Select(x => x.Code).ToList();
                var allPmsProject = BaseFrom.allPermission.AllProject;
                DAs = DAs.Where(x => allPmsProject.Contains(x.Code)).ToList();
            }

            slke_ThongTinDuAn.Properties.DataSource = DAs;//new BindingSource(DicCbbDA, null);

            var crDA = DAs.FirstOrDefault(x => x.Code == MSETTING.Default.DuAnHienTai);
            //string crCodeInSlke = slke_ThongTinDuAn.EditValue as string;
            slke_ThongTinDuAn.EditValue = crDA?.Code;
            //if ()
            return;
        }

        public static void SetControlsByPermission(bool isSetCbbDACT = false)
        {
            SharedControls.slke_ThongTinDuAn.Enabled = true;

            if (isSetCbbDACT)
            {
                try
                {
                    fcn_updateCbbThongTinDuAnCongTrinh();
                }
                catch
                {
                    SharedControls.slke_ThongTinDuAn.Properties.DataSource = null;
                }
            }
            BaseFrom.IsSetNullOnChangedTab = true;
            WaitFormHelper.ShowWaitForm("Đang cập nhật Form chính theo phân quyền trong khóa bạn đã chọn");

            var allPermission = BaseFrom.allPermission;

            SharedControls.tabMain.SelectedTabPage = null;
            SharedControls.tabMain.SelectedPageChanging += PreventChangeTabs;
            bool isnullDA = SharedControls.slke_ThongTinDuAn.EditValue is null;

            foreach (XtraTabPage tab in SharedControls.tabMain.TabPages)
            {
                if (tab.Text.StartsWith("*"))
                {
                    tab.PageVisible = false;
                    continue;
                }
                XtraTabControl subTabControl = tab.Controls.OfType<XtraTabControl>().FirstOrDefault();
                if (subTabControl is null) 
                {
                    if ((allPermission.HaveInitProjectPermission || allPermission.TabsUnHide.Contains(tab.Name)))
                    {
                        if (isnullDA && !SharedControls.TabPageNameNotDependOnPrj.Contains(tab.Name))
                            tab.PageVisible = false;
                        else
                            tab.PageVisible = true;

                    }    
                    else
                    {
                        tab.PageVisible = false;
                    }
                    continue;
                }

                subTabControl.SelectedTabPage = null;
                subTabControl.SelectedPageChanging += PreventChangeTabs;
                bool isParentTabVisible = false;
                foreach (XtraTabPage subTab in subTabControl.TabPages)
                {
                    if ((allPermission.HaveInitProjectPermission || allPermission.TabsUnHide.Contains(subTab.Name)) && !subTab.Text.StartsWith("*"))
                    {


                        if (isnullDA && !SharedControls.TabPageNameNotDependOnPrj.Contains(tab.Name) && !SharedControls.TabPageNameNotDependOnPrj.Contains(subTab.Name))
                        {
                            subTab.PageVisible = false;
                        }
                        else
                        {
                            
                            subTab.PageVisible = true;
                            isParentTabVisible = true;

                        }
                    }
                    else
                    {
                        subTab.PageVisible = false;
                    }
                }
                if (tab.Text == SharedControls.xtraTab_QLVatLieu_VanChuyen.Text)
                    isParentTabVisible = true;
                //subTabControl.SelectedTabPage = subTabControl.TabPages.Where(x => x.PageVisible).FirstOrDefault();
                tab.PageVisible = isParentTabVisible;
                subTabControl.SelectedPageChanging -= PreventChangeTabs;
            }
            if (TDKH.KeyTruongSon.Contains(BaseFrom.BanQuyenKeyInfo.SerialNo))
            {
                foreach (var item in TDKH.TabForceHide)
                    item.PageVisible = false;
                foreach (var item in TDKH.SubTabForceHide)
                    item.PageVisible = false;
            }
            SharedControls.tabMain.SelectedPageChanging -= PreventChangeTabs;
            SharedControls.tabMain.SelectedTabPage = null;

            //SharedControls.tabMain.SelectedTabPage = SharedControls.tabMain.TabPages.Where(x => x.PageVisible).FirstOrDefault();
            BaseFrom.IsSetNullOnChangedTab = false;
            WaitFormHelper.CloseWaitForm();
        }

        public static void ResetAllPermission()
        {
            BaseFrom.allPermission = new AllPermission();
            MSETTING.Default.AllPermission = CryptoHelper.Base64EncodeObject(BaseFrom.allPermission);
            MSETTING.Default.Save();
        }

        public static void CheckQuyenDuAnWithSQLite()
        {
            
        }

        public static void SetTextFormByLicense(bool isResetVisiblePages = false)
        {
            //SharedControls.Form.Text = BaseFrom.m_crTempDATH;
            SharedControls.Form.Text = $"{BaseFrom.m_crTempDATH} - {CommonConstants.NAME_PHANMEM} V{Application.ProductVersion}";

            if (!string.IsNullOrEmpty(BaseFrom.BanQuyenKeyInfo.FullName))
                SharedControls.btn_TT_HienThiThongTinMoi.Text = BaseFrom.BanQuyenKeyInfo.FullName;
            else
                SharedControls.btn_TT_HienThiThongTinMoi.Text = "Đăng nhập";

            if (SharedControls.ce_Mode.Checked)
            {
                if (BaseFrom.IsFullAccess)
                {
                    SharedControls.lb_ThongBaoBanQuyen.Text = FormNoti.ValidKey;

                    BaseFrom.IsValidAccount = true;
                    SharedControls.thôngTinPhiênBảnToolStripMenuItem.Enabled = true;
                    //SharedControls.đăngKýSửDụngPhầnMềmQuảnLýThiCôngToolStripMenuItem.Enabled = false;
                    //if (BaseFrom.BanQuyenOldViewModel.TypeKhoa == "Khóa cứng")

                    if (BaseFrom.BanQuyenKeyInfo.TypeCode == TypeStatus.KHOACUNG)
                    {
                        SharedControls.Form.Text += $" (Phần mềm bản quyền / Khóa cứng)";
                    }
                    else
                    {
                        SharedControls.Form.Text += $" (Hạn dùng: {BaseFrom.BanQuyenKeyInfo.LimitDate} ngày / Phần mềm bản quyền / Khóa mềm)";
                    }
                }
                else
                {
                    SharedControls.lb_ThongBaoBanQuyen.Text = FormNoti.NoLicense;
                    BaseFrom.allPermission = new AllPermission();
                    //MSETTING.Default.AllPermission = BaseFrom.allPermission;
                    MSETTING.Default.Save();

                    if (BaseFrom.IsLimitDate)
                    {
                        SharedControls.thôngTinPhiênBảnToolStripMenuItem.Enabled = true;
                        SharedControls.đăngKýSửDụngPhầnMềmQuảnLýThiCôngToolStripMenuItem.Enabled = false;
                        SharedControls.Form.Text += $" (Hạn dùng: Phiên bản dùng thử / Khóa mềm)";
                    }
                    else
                    {
                        SharedControls.thôngTinPhiênBảnToolStripMenuItem.Enabled = false;
                        SharedControls.đăngKýSửDụngPhầnMềmQuảnLýThiCôngToolStripMenuItem.Enabled = true;
                        SharedControls.Form.Text += $" (Dùng thử)";
                    }
                }


            }
            else
            {
                if (BaseFrom.IsValidAccount)
                {

                    SharedControls.đăngKýSửDụngPhầnMềmQuảnLýThiCôngToolStripMenuItem.Enabled = false;

                    SharedControls.Form.Text += $" ({BaseFrom.BanQuyenKeyInfo.FullName} ({BaseFrom.BanQuyenKeyInfo.Email}))";
                    if (string.IsNullOrEmpty(BaseFrom.BanQuyenKeyInfo.SerialNo))
                    {
                        SharedControls.lb_ThongBaoBanQuyen.Text = FormNoti.InvalidKeyCode;

                        SharedControls.Form.Text += $" (Chưa chọn khóa hoạt động)";
                    }
                    else
                    {
                        BaseFrom.allPermission = CryptoHelper.Base64DecodeToObject<AllPermission>(MSETTING.Default.AllPermission);
                        if (BaseFrom.BanQuyenKeyInfo.TypeCode == TypeStatus.KHOACUNG)
                            SharedControls.Form.Text += $" (Khóa hoạt động: {BaseFrom.BanQuyenKeyInfo.KeyCode})";
                        else if (BaseFrom.BanQuyenKeyInfo.TypeCode == TypeStatus.KHOAMEM)
                            SharedControls.Form.Text += $" (Khóa hoạt động: {BaseFrom.BanQuyenKeyInfo.SerialNo} (Khóa mềm))";

                        SharedControls.lb_ThongBaoBanQuyen.Text = FormNoti.ValidKey;

                        //return;
                    }
                }
                else
                {
                    SharedControls.lb_ThongBaoBanQuyen.Text = FormNoti.InvalidAccount;

                    SharedControls.đăngKýSửDụngPhầnMềmQuảnLýThiCôngToolStripMenuItem.Enabled = true;
                    SharedControls.Form.Text += $" (Tài khoản không hợp lệ)";
                }
            }
            if (BaseFrom.BanQuyenKeyInfo.SubscriptionTypeId > 1)
            {
                if (BaseFrom.BanQuyenKeyInfo.IsSeverExpired)
                {
                    SharedControls.Form.Text += $" - (Server: Hết hạn)";
                }
                else
                {
                    SharedControls.Form.Text += $" - (Server: Còn {(BaseFrom.BanQuyenKeyInfo.EndSeverDate.Date - DateTime.Now.Date).Days + 1} Ngày)";
                }
            }

#if DEBUG
            SharedControls.Form.Text += "\t+++===DEBUG VERSION===+++";
#endif

            if (isResetVisiblePages)
            {
                TongHopHelper.SetControlsByPermission();
            }
        }

        public static void AppUnauthorize(bool isShowMessageBox = true)
        {
            if (isShowMessageBox)
            {
                AlertShower.ShowInfo("Hết phiên đăng nhập. Vui lòng đăng nhập lại");
            }
            BaseFrom.BanQuyenKeyInfo = new BanQuyenKeyInfo();
            ResetAllPermission();
            SetTextFormByLicense(true);
        }
        public static bool IsAutoTime()
        {
            var key = Registry.LocalMachine.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\W32Time\Parameters");
            if (key is null)
                return false;

            string type = key.GetValue("Type").ToString();
            key.Close();
            return (type == "NTP");
        }

        public static bool fcn_saveDA(bool isSaveAs, string ThongBaoSaveAs = null, bool isShowErrorDialog = true)
        {
            try
            {
                if (SharedControls.uc_PATC != null)
                {

                }
                string dbString = $"INSERT OR IGNORE INTO Tbl_ThongTinTHDA " +
                    $"(Code, SavedBy, Version) VALUES " +
                    $"(@Code, @SavedBy, @Version);\r\n" +
                    $"UPDATE Tbl_ThongTinTHDA " +
                    $"SET SavedBy = '{BaseFrom.BanQuyenKeyInfo.Email}', Version = '{Application.ProductVersion}'";

                object[] mParams =
                {
                    1,
                    BaseFrom.BanQuyenKeyInfo.Email,
                    Application.ProductVersion
                };
                DataProvider.InstanceTHDA.ExecuteNonQuery(dbString, parameter: mParams);


                string m_crFileDA = MSETTING.Default.PathHienTai;
                string filename = Path.GetFileNameWithoutExtension(m_crFileDA);

                //if (newPath != null)
                //{
                //    m_crFileDA = newPath;
                //    filename = Path.GetFileNameWithoutExtension(m_crFileDA);
                //    goto SavingBegin;
                //}

                if (!File.Exists(m_crFileDA) || isSaveAs)
                {
                    if (ThongBaoSaveAs.HasValue())
                        MessageShower.ShowInformation(ThongBaoSaveAs);

                    var saveFileDialog = SharedControls.saveFileDialog;

                    saveFileDialog.DefaultExt = "qltc";
                    saveFileDialog.Filter = "Quan Ly Thi Cong (*.qltc)|*.qltc";
                    saveFileDialog.AddExtension = false;

                    if (saveFileDialog.ShowDialog() == DialogResult.Cancel)
                        return false;
                    m_crFileDA = saveFileDialog.FileName;
                    filename = Path.GetFileNameWithoutExtension(m_crFileDA);
                    Directory.Move($@"{BaseFrom.m_tempPath}\{BaseFrom.m_crTempDATH}", $@"{BaseFrom.m_tempPath}\{filename}");
                    BaseFrom.m_crTempDATH = filename;
                    DataProvider.InstanceTHDA.changePath($@"{BaseFrom.m_tempPath}\{filename}\{MyConstant.CONST_DbFromPathDA}");
                }

                if (fcn_SaveTHDAToQLTC(m_crFileDA))
                {
                    MSETTING.Default.PathHienTai = m_crFileDA;
                    MSETTING.Default.Save();
                    AlertShower.ShowInfo("Đã lưu dự án");
                    BaseFrom.THDAChanged = false;
                    TongHopHelper.SetTextFormByLicense();
                    //FileHelper.CalculateFileMD5(BaseFrom.m_crTem)
                    return true;
                }
                else return false;

            }
            catch (Exception ex)
            {
                string err = $"Lỗi lưu dự án: {ex.Message}__{ex.InnerException?.Message}";
                if (isShowErrorDialog)
                    MessageShower.ShowError(err);

                AlertShower.ShowInfo(err);
                return false;
            }
            //return true;
        }

        public static bool fcn_SaveTHDAToQLTC(string newPath)
        {
            try
            {
                string m_crFileDA = newPath;
                string filename = Path.GetFileNameWithoutExtension(m_crFileDA);
                string tempDirInTemp = $@"{BaseFrom.m_tempPath}\~${filename}";
                string tempFileBeforeSave = $@"{Path.GetDirectoryName(m_crFileDA)}/~${Path.GetFileName(m_crFileDA)}";

                if (Directory.Exists(tempDirInTemp))
                    MyFunction.DirectoryDelete(tempDirInTemp);

                Directory.CreateDirectory(tempDirInTemp);

                MyFunction.DirectoryCopy($@"{BaseFrom.m_tempPath}\{filename}", tempDirInTemp, true);

                if (File.Exists(tempFileBeforeSave))
                    File.Delete(tempFileBeforeSave);

                //Directory.CreateDirectory(tempFileBeforeSave);

                ZipFile.CreateFromDirectory(tempDirInTemp, tempFileBeforeSave);

                //DevExpress.Compression.EncryptionType encryptionType = DevExpress.Compression.EncryptionType.PkZip;


                //using (var memoryStream = new FileStream(tempFileBeforeSave, FileMode.Create))
                //using (ZipArchive archive = new ZipArchive(memoryStream, ZipArchiveMode.Create))
                //{

                //    //archive.Password = m_pw;
                //    //archive.EncryptionType = encryptionType;

                //    archive.CreateEntry(tempDirInTemp);
                //    //archive.Save(c);
                   
                //}

                if (File.Exists(m_crFileDA))
                    File.Delete(m_crFileDA);

                File.Move(tempFileBeforeSave, m_crFileDA);
                MyFunction.DirectoryDelete(tempDirInTemp);
                File.Delete(tempFileBeforeSave);
            }
            catch (Exception ex)
            {
                MessageShower.ShowError($"Lỗi lưu dự án: {ex.Message}__{ex.InnerException?.Message}");
                return false;
            }
            return true;
        }

        public static void CheckTimeSync()
        {
            if (!IsAutoTime())
            {
                MessageShower.ShowError("Vui lòng bật chế độ đồng bộ thời gian với server và khởi động lại để sử dụng phần mềm!");
                if (MSETTING.Default.PathHienTai.HasValue() && BaseFrom.THDAChanged)
                    fcn_saveDA(false);
                Environment.Exit(0);
            }
        }
        public static RepositoryItemPictureEdit CreatePictureEdit(SearchLookUpEdit LookUp, string name)
        {
            RepositoryItemPictureEdit ret = new RepositoryItemPictureEdit();
            LookUp.Properties.RepositoryItems.Add(ret);
            ret.PictureInterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            ret.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Squeeze;
            ret.Name = name;
            return ret;
        }

        public static bool CheckSaveDA(bool deleteFolder = true)
        {
            if (BaseFrom.m_crTempDATH.HasValue())
            {
                if (BaseFrom.THDAChanged)
                {
                    DialogResult rs = MessageShower.ShowYesNoCancelQuestion("Lưu dự án", "Dự án đang làm có thay đổi, bạn có muốn lưu không?");

                    if (rs == DialogResult.Cancel)
                        return false;

                    if (rs == DialogResult.Yes)
                    {
                        if (fcn_saveDA(false))
                        {
                            if (deleteFolder)
                            {
                                MyFunction.DirectoryDelete(BaseFrom.m_FullTempathDA);
                                DataProvider.InstanceTHDA.changePath("");
                            }
                            return true;
                        }
                        else return false;
                    }
                    else
                    {
                        if (deleteFolder)
                        {
                            MyFunction.DirectoryDelete(BaseFrom.m_FullTempathDA);
                            DataProvider.InstanceTHDA.changePath("");
                        } 
                        return true;
                    }
                }
                else if (deleteFolder)
                    MyFunction.DirectoryDelete(BaseFrom.m_FullTempathDA);
            }

            
            return true;
        }
    }
}
