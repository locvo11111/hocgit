using DevExpress.Spreadsheet;
//using DevExpress.Utils.Extensions;
using DevExpress.Utils.Filtering;
using DevExpress.XtraEditors;
using DevExpress.XtraPdfViewer;
using DevExpress.XtraReports.UI;
using DevExpress.XtraRichEdit;
using DevExpress.XtraSpreadsheet;
using PhanMemQuanLyThiCong.Constant.Enum;
using PhanMemQuanLyThiCong.Common.Helper;
using PhanMemQuanLyThiCong.Model;
using PhanMemQuanLyThiCong.Model.TDKH;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;
using PhanMemQuanLyThiCong.Model.QuanLyVanChuyen;
using PhanMemQuanLyThiCong.Common.Enums;
using MoreLinq;
using PhanMemQuanLyThiCong.Constant;
using PhanMemQuanLyThiCong.Common.Constant;
using System.Threading;
using System.Threading.Tasks;
using VChatCore.ViewModels.SyncSqlite;
using System.ComponentModel;
using System.Reflection.Emit;
using Newtonsoft.Json;
using DevExpress.Office.Utils;
using DevExpress.XtraSpreadsheet.DocumentFormats.Xlsb;
using StackExchange.Profiling.Internal;
using System.Runtime.InteropServices;
using PhanMemQuanLyThiCong.Common.ViewModel.KLHN;
using DevExpress.XtraRichEdit.Menu;
using DevExpress.DataAccess.Native.Sql.ConnectionStrategies;
using DevExpress.XtraPrinting.Native;
using DevExpress.XtraSpreadsheet.Menu;
using System.Windows.Forms.DataVisualization.Charting;
using PM360.Common.Helper;
using System.Globalization;
using PhanMemQuanLyThiCong.Model.MayThiCong;
using Dapper;
using PhanMemQuanLyThiCong.Common.MLogging;
using System.Net;
using DevExpress.DataProcessing.InMemoryDataProcessor;
using static DevExpress.Mvvm.Native.Either;
using System.Drawing.Imaging;
//using DevExpress.Utils.Extensions;
//using DevExpress.XtraSpreadsheet.Model;
//using DevExpress.Charts.Native;

namespace PhanMemQuanLyThiCong
{
    public static class MyExtenstions
    {
        public static Control FindControl(this Control root, string text)
        {
            if (root == null) throw new ArgumentNullException("root");
            foreach (Control child in root.Controls)
            {
                if (child.Text == text) return child;
                Control found = FindControl(child, text);
                if (found != null) return found;
            }
            return null;
        }
    }
    public static class MyFunction
    {
        public static void DirectoryCopy(string sourceDirName, string destDirName, bool copySubDirs)
        {
            // Get the subdirectories for the specified directory.
            DirectoryInfo dir = new DirectoryInfo(sourceDirName);

            if (!dir.Exists)
            {
                throw new DirectoryNotFoundException(
                    "Source directory does not exist or could not be found: "
                    + sourceDirName);
            }

            DirectoryInfo[] dirs = dir.GetDirectories();

            // If the destination directory doesn't exist, create it.       
            Directory.CreateDirectory(destDirName);

            // Get the files in the directory and copy them to the new location.
            FileInfo[] files = dir.GetFiles();
            foreach (FileInfo file in files)
            {
                string tempPath = Path.Combine(destDirName, file.Name);
                file.CopyTo(tempPath, false);
            }

            // If copying subdirectories, copy them and their contents to new location.
            if (copySubDirs)
            {
                foreach (DirectoryInfo subdir in dirs)
                {
                    string tempPath = Path.Combine(destDirName, subdir.Name);
                    DirectoryCopy(subdir.FullName, tempPath, copySubDirs);
                }
            }
        }

        public static void DirectoryDelete(string target_dir)
        {
            if (!Directory.Exists(target_dir))
                return;

            string[] files = Directory.GetFiles(target_dir);
            string[] dirs = Directory.GetDirectories(target_dir);

            foreach (string file in files)
            {
                File.SetAttributes(file, FileAttributes.Normal);
                File.Delete(file);
            }

            foreach (string dir in dirs)
            {
                DirectoryDelete(dir);
            }

            Directory.Delete(target_dir, false);
        }

        public static int GetIntFromString(string str)
        {
            string numStr = "";
            foreach (char c in str)
            {
                if (char.IsDigit(c))
                    numStr += c;
            }

            if (numStr == "")
                return -1;
            return int.Parse(numStr);
        }

        public static int GetRowCountFromStringRange(string str)
        {
            string[] lsStr = str.Split(':');
            if (lsStr.Length < 2)
                return -1;
            int TopRow = GetIntFromString(lsStr[0]);
            int BotRow = GetIntFromString(lsStr[1]);

            if (TopRow < 0 || BotRow < 0)
                return -1;
            return (BotRow - TopRow + 1);
        }

        /// <summary>
        /// Loại bỏ dấu
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string fcn_RemoveAccents(string text)
        {
            string[] arr1 = new string[] { "á", "à", "ả", "ã", "ạ", "â", "ấ", "ầ", "ẩ", "ẫ", "ậ", "ă", "ắ", "ằ", "ẳ", "ẵ", "ặ",
                                            "đ",
                                            "é","è","ẻ","ẽ","ẹ","ê","ế","ề","ể","ễ","ệ",
                                            "í","ì","ỉ","ĩ","ị",
                                            "ó","ò","ỏ","õ","ọ","ô","ố","ồ","ổ","ỗ","ộ","ơ","ớ","ờ","ở","ỡ","ợ",
                                            "ú","ù","ủ","ũ","ụ","ư","ứ","ừ","ử","ữ","ự",
                                            "ý","ỳ","ỷ","ỹ","ỵ",};
            string[] arr2 = new string[] { "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a",
                                            "d",
                                            "e","e","e","e","e","e","e","e","e","e","e",
                                            "i","i","i","i","i",
                                            "o","o","o","o","o","o","o","o","o","o","o","o","o","o","o","o","o",
                                            "u","u","u","u","u","u","u","u","u","u","u",
                                            "y","y","y","y","y",};
            for (int i = 0; i < arr1.Length; i++)
            {
                text = text.Replace(arr1[i], arr2[i]);
                text = text.Replace(arr1[i].ToUpper(), arr2[i].ToUpper());
            }
            return text;
        }
        public static DataTable Fcn_CalKLHN(DataTable dtHPVT, DataTable dtCongTacTheoKy, bool CongTacGD)
        {
            string lstcode = MyFunction.fcn_Array2listQueryCondition(dtHPVT.AsEnumerable().Select(x => x["Code"].ToString()).ToArray());
            string dbString = $"SELECT * FROM {TDKH.Tbl_HaoPhiVatTu_HangNgay} WHERE \"CodeHaoPhiVatTu\" IN ({lstcode})";
            DataTable DtKLHN = DataProvider.InstanceTHDA.ExecuteQuery(dbString);
            foreach (DataRow item in dtHPVT.Rows)
            {
                DataRow drs_CT = dtCongTacTheoKy.AsEnumerable().Where(x => x["Code"].ToString() == item["CodeCongTac"].ToString()).ToArray().FirstOrDefault();
                if (CongTacGD)
                    drs_CT = dtCongTacTheoKy.AsEnumerable().Where(x => x["CodeCongTacGiaiDoan"].ToString() == item["CodeCongTac"].ToString()).ToArray().FirstOrDefault();
                double KL = double.Parse(item[TDKH.COL_KHVT_DinhMucNguoiDung].ToString()) * double.Parse(item[TDKH.COL_KHVT_HeSoNguoiDung].ToString()) * double.Parse(drs_CT[TDKH.COL_KhoiLuongHopDongChiTiet].ToString());
                DateTime NBD = DateTime.Parse(item["NgayBatDau"].ToString());
                DateTime NKT = DateTime.Parse(item["NgayKetThuc"].ToString());
                double SoNgay = (NKT - NBD).TotalDays + 1;
                string codeVT = item["Code"].ToString();
                DataRow[] drsThuCongKL = DtKLHN.AsEnumerable().Where(x => x["CodeHaoPhiVatTu"].ToString() == codeVT).ToArray();
                if (SoNgay == drsThuCongKL.Count())
                    continue;
                double KLKH = drsThuCongKL.Where(x=>x["KhoiLuongKeHoach"]!=DBNull.Value).Sum(x => double.Parse(x["KhoiLuongKeHoach"].ToString()));
                string[] lstNgay = drsThuCongKL.Select(x => x["Ngay"].ToString()).ToArray();
                double KLPB = (KL - KLKH) / (SoNgay - drsThuCongKL.Count());
                for (int i = 0; i < SoNgay; i++)
                {
                    string Ngay = NBD.AddDays(i).ToString(MyConstant.CONST_DATE_FORMAT_SQLITE);
                    if (lstNgay.Contains(Ngay))
                        continue;
                    DataRow New = DtKLHN.NewRow();
                    New["CodeHaoPhiVatTu"] = codeVT;
                    New["Ngay"] = Ngay;
                    New["KhoiLuongKeHoach"] = KLPB;
                    DtKLHN.Rows.Add(New);
                }

            }
            DtKLHN.AcceptChanges();
            return DtKLHN;
        }
        //public static DataTable Fcn_CalKLKH(DataTable Dt_CongTacGiaiDoan,string ColCode, bool isUpdateToDb = false)
        //{
        //    string lstcode = fcn_Array2listQueryCondition(Dt_CongTacGiaiDoan.AsEnumerable().Select(x => x[ColCode].ToString()).ToArray());


        //    string dbString = $"SELECT * FROM {TDKH.TBL_KhoiLuongCongViecHangNgay} WHERE \"CodeCongTacTheoGiaiDoan\" IN ({lstcode})";
        //    DataTable DtKLHN = DataProvider.InstanceTHDA.ExecuteQuery(dbString);

        //    foreach (DataRow item in Dt_CongTacGiaiDoan.Rows)
        //    {
        //        double KLKH = double.Parse(item["KhoiLuongToanBo"].ToString());
        //        double DonGia = double.Parse(item["DonGia"].ToString());
        //        DateTime NBD = DateTime.Parse(item["NgayBatDau"].ToString());
        //        DateTime NKT = DateTime.Parse(item["NgayKetThuc"].ToString());
        //        int SoNgay = (NKT-NBD).Days + 1;
        //        string codeCt = item[ColCode].ToString();
        //        DataRow[] drsThuCongKL = DtKLHN.AsEnumerable().Where(x => x["KhoiLuongKeHoach"] != DBNull.Value && x["CodeCongTacTheoGiaiDoan"].ToString() ==  codeCt).ToArray();
        //        DataRow[] drsnull = DtKLHN.AsEnumerable().Where(x => x["KhoiLuongKeHoach"] == DBNull.Value && x["CodeCongTacTheoGiaiDoan"].ToString() ==  codeCt).ToArray();
        //        if (SoNgay == drsThuCongKL.Count())
        //            continue;
        //        double KLTT = 0;
        //        double TTTT = 0;
        //        double KLPB = 0;
        //        drsThuCongKL.ForEach(x => KLTT += double.Parse(x["KhoiLuongKeHoach"].ToString()));
        //        KLPB = (KLKH - KLTT) / (SoNgay - drsThuCongKL.Count());
        //        TTTT = KLPB * DonGia;
        //        string[] lstNgay = drsThuCongKL.Select(x => x["Ngay"].ToString()).ToArray();
        //        string[] lstNgaycheck = drsnull.Select(x => x["Ngay"].ToString()).ToArray();
        //        for(int i = 0; i < SoNgay; i++)
        //        {
        //            string Ngay = NBD.AddDays(i).ToString(MyConstant.CONST_DATE_FORMAT_SQLITE);
        //            if (lstNgay.Contains(Ngay))                    
        //                continue;
        //            if (lstNgaycheck.Contains(Ngay))
        //            {
        //                DataRow rowcheck = drsnull.Where(x => x["Ngay"].ToString() == Ngay).ToArray().FirstOrDefault();
        //                //int index = DtKLHN.Rows.IndexOf(rowcheck);
        //                rowcheck["KhoiLuongKeHoach"] = KLPB;
        //                rowcheck["ThanhTienKeHoach"] = TTTT;
        //                continue;
        //            }
        //            DataRow New = DtKLHN.NewRow();
        //            New[ColCode] = Guid.NewGuid().ToString();
        //            New["CodeCongTacTheoGiaiDoan"] = codeCt;
        //            New["Ngay"] = Ngay;
        //            New["KhoiLuongKeHoach"] = KLPB;
        //            New["ThanhTienKeHoach"] = TTTT;
        //            DtKLHN.Rows.Add(New);
        //        }
        //    }

        //    if (isUpdateToDb)
        //        DataProvider.InstanceTHDA.UpdateDataTableFromSqliteSource(DtKLHN, MyConstant.Tbl_Temp_TDKH_KhoiLuongCongViecHangNgay);

        //    DtKLHN.AcceptChanges();
        //    return DtKLHN;
        //} 


        

        public static List<KLTTHangNgay> Fcn_CalKLKHModel(TypeKLHN type, IEnumerable<string> lsCodeMain = null,
            DateTime? dateBD = null, DateTime? dateKT = null)
        {
            var kls = Fcn_CalKLKHNew(type, lsCodeMain, dateBD, dateKT);


            return kls.Select(x => new KLTTHangNgay()
            {
                Code = x.Code,
                ParentCode = x.ParentCode,
                Ngay = x.Ngay,
                CodeHangNgay = x.Code,
                LyTrinhCaoDo = x.LyTrinhCaoDo,
                NhaCungCap = x.NhaCungCap,
                FileCount = x.FileCount,
                CodeCha = x.ParentCode,
                CodeCongTacTheoGiaiDoan = x.CodeCongTacTheoGiaiDoan,
                KhoiLuongKeHoach = x.KhoiLuongKeHoach,
                KhoiLuongThiCong = x.KhoiLuongThiCong,
                ThanhTienThiCongCustom = x.ThanhTienThiCong,
                IsThuCong = x.IsThuCong,
            }).ToList();
        }


        public static List<KLHN> Fcn_CalKLKHNewWithoutLuyKe(TypeKLHN type, IEnumerable<string> lsCodeMain = null,
    DateTime? dateBD = null, DateTime? dateKT = null)
        {

            string workloadSp = type.GetEnumDescription();
            if (workloadSp is null)
            {
                return new List<KLHN>();

            }

            workloadSp += "WithoutLuyKe";
            List<string> dts = new List<string>();

            if (lsCodeMain is null || !lsCodeMain.Any())
            {
                return new List<KLHN>();
            }

            if (dateBD is null || dateKT is null)
            {
                string workloadTableName = type.GetEnumDisplayName();
                if (workloadTableName is null)
                {
                    return new List<KLHN>();

                }
                string dbString = $"SELECT MIN(NgayBatDau) AS NgayBatDau, MAX(NgayKetThuc) AS NgayKetThuc\r\n" +
                    $"FROM {workloadTableName} ct\r\n" +
                    $"WHERE Code IN ({MyFunction.fcn_Array2listQueryCondition(lsCodeMain)})";
                var item = DataProvider.InstanceTHDA.ExecuteQueryModel<Tbl_TDKH_ChiTietCongTacTheoGiaiDoanViewModel>(dbString).SingleOrDefault();
                if (item is null)
                {
                    return new List<KLHN>();
                }
                dateBD = dateBD ?? item.NgayBatDau;
                dateKT = dateKT ?? item.NgayKetThuc;
                if (dateBD is null || dateKT is null)
                {
                    return new List<KLHN>();
                }

                switch (type)
                {
                    case TypeKLHN.CongTac:
                        dbString = $"SELECT Ngay FROM {Server.Tbl_TDKH_KhoiLuongCongViecTungNgay} kl\r\n" +
                            $"JOIN {Server.Tbl_TDKH_ChiTietCongTacTheoGiaiDoan} ct\r\n" +
                            $"ON kl.CodeCongTacTheoGiaiDoan = ct.Code AND (kl.Ngay < ct.NgayBatDau OR kl.Ngay > NgayKetThuc)\r\n" +
                             $"WHERE ct.Code IN ({MyFunction.fcn_Array2listQueryCondition(lsCodeMain)})";

                        break;
                    case TypeKLHN.Nhom:
                        dbString = $"SELECT Ngay FROM {Server.Tbl_TDKH_KhoiLuongCongViecTungNgay} kl\r\n" +
                            $"JOIN {Server.Tbl_TDKH_NhomCongTac} ct\r\n" +
                            $"ON kl.CodeNhom = ct.Code AND (kl.Ngay < ct.NgayBatDau OR kl.Ngay > NgayKetThuc)" +
                             $"WHERE ct.Code IN ({MyFunction.fcn_Array2listQueryCondition(lsCodeMain)})";

                        break;
                    case TypeKLHN.VatLieu:
                        dbString = $"SELECT Ngay FROM {Server.Tbl_TDKH_KHVT_KhoiLuongHangNgay} kl\r\n" +
                            $"JOIN {Server.Tbl_TDKH_KHVT_VatTu} ct\r\n" +
                            $"ON kl.CodeVatTu = ct.Code AND (kl.Ngay < ct.NgayBatDau OR kl.Ngay > NgayKetThuc)" +
                             $"WHERE ct.Code IN ({MyFunction.fcn_Array2listQueryCondition(lsCodeMain)})";

                        break;
                    default:
                        return new List<KLHN>();


                }
                var outRangeDate = DataProvider.InstanceTHDA.ExecuteQueryModel<Tbl_TDKH_KHVT_KhoiLuongHangNgayViewModel>(dbString).Select(x => x.Ngay);
                dts.AddRange(outRangeDate.Select(date => $"('{date.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE)}')"));
            }

            for (DateTime date = dateBD.Value; date <= dateKT.Value; date = date.AddDays(1))
            {
                dts.Add($"('{date.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE)}')");
            }

            var listOfCondition = new List<string>();
            if (lsCodeMain != null && lsCodeMain.Any())
            {
                listOfCondition.Add($"ct.Code IN ({fcn_Array2listQueryCondition(lsCodeMain)})");
            }
            List<KeyValuePair<string, string>> conds = new List<KeyValuePair<string, string>>()
            {
                new KeyValuePair<string, string>("--Dates",string.Join(",", dts.Distinct()))
            };

            if (listOfCondition.Any())
            {
                conds.Add(new KeyValuePair<string, string>("--Condition", $"WHERE {string.Join(" AND ", listOfCondition)}"));
            }

            //var condDates = new List<string>();
            //if (dateBD.HasValue)
            //{
            //    condDates.Add($"hn.Ngay >= {dateBD.Value.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE)}");
            //}

            //if (dateKT.HasValue)
            //{
            //    condDates.Add($"hn.Ngay <= {dateBD.Value.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE)}");
            //}

            //if (condDates.Any())
            //{
            //    conds.Add(new KeyValuePair<string, string>("--DateCondition", $"AND {string.Join(" AND ", condDates)}"));

            //}

            var ret = DataProvider.InstanceTHDA.ExecuteQueryModel<KLHN>(workloadSp, isSQLFile: true, Conditions: conds);
            return ret;
        }
        
        
        public static List<KLHN> Fcn_CalKLKHNewWithoutKeHoach(TypeKLHN type, IEnumerable<string> lsCodeMain = null,
    DateTime? dateBD = null, DateTime? dateKT = null)
        {

            string workloadSp = type.GetEnumDescription();
            if (workloadSp is null)
            {
                return new List<KLHN>();

            }

            workloadSp += "WithoutKeHoach";
            List<string> dts = new List<string>();

            if (lsCodeMain is null || !lsCodeMain.Any())
            {
                return new List<KLHN>();
            }

            var listOfCondition = new List<string>();
            if (lsCodeMain != null && lsCodeMain.Any())
            {
                listOfCondition.Add($"ct.Code IN ({fcn_Array2listQueryCondition(lsCodeMain)})");
            }
            List<KeyValuePair<string, string>> conds = new List<KeyValuePair<string, string>>()
            {
                new KeyValuePair<string, string>("--Dates",string.Join(",", dts.Distinct()))
            };

            if (listOfCondition.Any())
            {
                conds.Add(new KeyValuePair<string, string>("--Condition", $"WHERE {string.Join(" AND ", listOfCondition)}"));
            }
            var condDates = new List<string>();
            if (dateBD.HasValue)
            {
                condDates.Add($"hn.Ngay >= {dateBD.Value.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE)}");
            }
            
            if (dateKT.HasValue)
            {
                condDates.Add($"hn.Ngay <= {dateBD.Value.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE)}");
            }

            if (condDates.Any())
            {
                conds.Add(new KeyValuePair<string, string>("--DateCondition", $" AND {string.Join(" AND ", condDates.Distinct())}"));

            }

            var ret = DataProvider.InstanceTHDA.ExecuteQueryModel<KLHN>(workloadSp, isSQLFile: true, Conditions: conds);
            return ret;
        }

        public static List<KLHN> Fcn_CalKLKHNew(TypeKLHN type, IEnumerable<string> lsCodeMain,
            DateTime? dateBD, DateTime? dateKT)
        {

            string workloadSp = type.GetEnumDescription();
            if (workloadSp is null)
            {
                return new List<KLHN>();

            }
            List<string> dts = new List<string>();

            if (lsCodeMain is null || !lsCodeMain.Any())
            {
                return new List<KLHN>();
            }

            if (dateBD is null || dateKT is null)
            {
                string workloadTableName = type.GetEnumDisplayName();
                if (workloadTableName is null)
                {
                    return new List<KLHN>();

                }
                string dbString = $"SELECT MIN(NgayBatDau) AS NgayBatDau, MAX(NgayKetThuc) AS NgayKetThuc\r\n" +
                    $"FROM {workloadTableName} ct\r\n" +
                    $"WHERE Code IN ({MyFunction.fcn_Array2listQueryCondition(lsCodeMain)})";
                var item = DataProvider.InstanceTHDA.ExecuteQueryModel<Tbl_TDKH_ChiTietCongTacTheoGiaiDoanViewModel>(dbString).SingleOrDefault();
                if (item is null)
                {
                    return new List<KLHN>();
                }
                dateBD = dateBD ?? item.NgayBatDau;
                dateKT = dateKT ?? item.NgayKetThuc;
                if (dateBD is null || dateKT is null)
                {
                    return new List<KLHN>();
                }

                switch (type)
                {
                    case TypeKLHN.CongTac:
                        dbString = $"SELECT Ngay FROM {Server.Tbl_TDKH_KhoiLuongCongViecTungNgay} kl\r\n" +
                            $"JOIN {Server.Tbl_TDKH_ChiTietCongTacTheoGiaiDoan} ct\r\n" +
                            $"ON kl.CodeCongTacTheoGiaiDoan = ct.Code AND (kl.Ngay < ct.NgayBatDau OR kl.Ngay > NgayKetThuc)\r\n" +
                             $"WHERE ct.Code IN ({MyFunction.fcn_Array2listQueryCondition(lsCodeMain)})";

                        break;
                    case TypeKLHN.Nhom:
                        dbString = $"SELECT Ngay FROM {Server.Tbl_TDKH_KhoiLuongCongViecTungNgay} kl\r\n" +
                            $"JOIN {Server.Tbl_TDKH_NhomCongTac} ct\r\n" +
                            $"ON kl.CodeNhom = ct.Code AND (kl.Ngay < ct.NgayBatDau OR kl.Ngay > NgayKetThuc)" +
                             $"WHERE ct.Code IN ({MyFunction.fcn_Array2listQueryCondition(lsCodeMain)})";
                        
                        break;
                    case TypeKLHN.VatLieu:
                        dbString = $"SELECT Ngay FROM {Server.Tbl_TDKH_KHVT_KhoiLuongHangNgay} kl\r\n" +
                            $"JOIN {Server.Tbl_TDKH_KHVT_VatTu} ct\r\n" +
                            $"ON kl.CodeVatTu = ct.Code AND (kl.Ngay < ct.NgayBatDau OR kl.Ngay > NgayKetThuc)" +
                             $"WHERE ct.Code IN ({MyFunction.fcn_Array2listQueryCondition(lsCodeMain)})";

                        break;
                    default:
                        return new List<KLHN>();


                }
                        var outRangeDate = DataProvider.InstanceTHDA.ExecuteQueryModel<Tbl_TDKH_KHVT_KhoiLuongHangNgayViewModel>(dbString).Select(x => x.Ngay);
                        dts.AddRange(outRangeDate.Select(date => $"('{date.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE)}')"));
            }

            for (DateTime date = dateBD.Value; date <= dateKT.Value; date = date.AddDays(1))
            {
                dts.Add($"('{date.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE)}')");
            }


            var listOfCondition = new List<string>();
            if (lsCodeMain != null && lsCodeMain.Any())
            {
                listOfCondition.Add($"ct.Code IN ({fcn_Array2listQueryCondition(lsCodeMain)})");
            }
            List<KeyValuePair<string, string>> conds = new List<KeyValuePair<string, string>>()
            {
                new KeyValuePair<string, string>("--Dates",string.Join(",", dts.Distinct()))
            };

            if (listOfCondition.Any())
            {
                conds.Add(new KeyValuePair<string, string>("--Condition", $"WHERE {string.Join(" AND ", listOfCondition)}"));
            }

            var ret = DataProvider.InstanceTHDA.ExecuteQueryModel<KLHN>(workloadSp, isSQLFile: true, Conditions: conds);
            return ret;
        }
        
        public static List<KLHN> Fcn_CalKLKHNew(TypeKLHN type, IEnumerable<string> lsCodeMain,
            List<DateTime> listOfDate)
        {

            string workloadSp = type.GetEnumDescription();
            if (workloadSp is null)
            {
                return new List<KLHN>();

            }


            if (lsCodeMain is null || !lsCodeMain.Any() || listOfDate is null || !listOfDate.Any())
            {
                return new List<KLHN>();
            }



            var listOfCondition = new List<string>();
            if (lsCodeMain != null && lsCodeMain.Any())
            {
                listOfCondition.Add($"ct.Code IN ({fcn_Array2listQueryCondition(lsCodeMain)})");
            }
            List<KeyValuePair<string, string>> conds = new List<KeyValuePair<string, string>>()
            {
                new KeyValuePair<string, string>("--Dates",string.Join(",", listOfDate.Select(x => $"('{x.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE)}')").Distinct()))
            };

            if (listOfCondition.Any())
            {
                conds.Add(new KeyValuePair<string, string>("--Condition", $"WHERE {string.Join(" AND ", listOfCondition)}"));
            }

            var ret = DataProvider.InstanceTHDA.ExecuteQueryModel<KLHN>(workloadSp, isSQLFile: true, Conditions: conds);
            return ret;
        }


        public static List<KLHNBriefViewModel> CalcKLHNBrief(TypeKLHN type, IEnumerable<string> lsCodeMain = null,
            DateTime? dateBD = null, DateTime? dateKT = null)
        {

            if (!lsCodeMain.Any())
            {
                return new List<KLHNBriefViewModel>() { };
            }
            try
            {
                string workloadSp = type.GetEnumDescription();
                if (workloadSp is null)
                {
                    return new List<KLHNBriefViewModel>();

                }
                List<string> dts = new List<string>();

                if (lsCodeMain is null || !lsCodeMain.Any())
                {
                    return new List<KLHNBriefViewModel>();
                }

                if (dateBD is null || dateKT is null)
                {
                    string workloadTableName = type.GetEnumDisplayName();
                    if (workloadTableName is null)
                    {
                        return new List<KLHNBriefViewModel>();

                    }
                    string dbString = $"SELECT MIN(NgayBatDau) AS NgayBatDau, MAX(NgayKetThuc) AS NgayKetThuc\r\n" +
                        $"FROM {workloadTableName} ct\r\n" +
                        $"WHERE Code IN ({MyFunction.fcn_Array2listQueryCondition(lsCodeMain)})";
                    var item = DataProvider.InstanceTHDA.ExecuteQueryModel<Tbl_TDKH_ChiTietCongTacTheoGiaiDoanViewModel>(dbString).SingleOrDefault();
                    if (item is null)
                    {
                        return new List<KLHNBriefViewModel>();
                    }
                    dateBD = dateBD ?? item.NgayBatDau;
                    dateKT = dateKT ?? item.NgayKetThuc;
                    if (dateBD is null || dateKT is null)
                    {
                        return new List<KLHNBriefViewModel>();
                    }
                }

                

                var listOfCondition = new List<string>();
                if (lsCodeMain != null && lsCodeMain.Any())
                {
                    listOfCondition.Add($"ct.Code IN ({fcn_Array2listQueryCondition(lsCodeMain)})");
                }

                List<KeyValuePair<string, string>> conds = new List<KeyValuePair<string, string>>()
            {
                
            };

                if (dateBD.Value.Date != dateKT.Value.Date)
                    conds.Add(new KeyValuePair<string, string>("--Dates", $"('{dateBD.Value.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE)}'),('{dateKT.Value.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE)}')"));
                else
                    conds.Add(new KeyValuePair<string, string>("--Dates", $"('{dateBD.Value.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE)}')"));

                if (listOfCondition.Any())
                {
                    conds.Add(new KeyValuePair<string, string>("--Condition", $"WHERE {string.Join(" AND ", listOfCondition)}"));
                }

                var ret = DataProvider.InstanceTHDA.ExecuteQueryModel<KLHN>(workloadSp, isSQLFile: true, Conditions: conds);

                var grs = ret.GroupBy(x => x.Code);
                var klFstDate = ret.Where(x => x.Ngay == dateBD.Value);
                var klLastDate = ret.Where(x => x.Ngay == dateKT.Value);

                var KLS = (from fst in klFstDate
                          join lst in klLastDate
                          on fst.ParentCode equals lst.ParentCode
                          select new KLHNBriefViewModel()
                          {
                              ParentCode = fst.ParentCode,

                              KLKHFirstDate = fst.KhoiLuongKeHoach??0,
                              LuyKeKLKHFirstDate = fst.LuyKeKhoiLuongKeHoach??0,
                              KLTCFirstDate = fst.KhoiLuongThiCong??0,
                              LuyKeKLTCFirstDate = fst.LuyKeKhoiLuongThiCong??0,

                              KLKHLastDate = lst.KhoiLuongKeHoach??0,
                              LuyKeKLKHLastDate = lst.LuyKeKhoiLuongKeHoach??0,
                              KLTCLastDate = lst.KhoiLuongThiCong??0,
                              LuyKeKLTCLastDate = lst.LuyKeKhoiLuongThiCong??0,
                              
                              TTKHFirstDate = fst.ThanhTienKeHoach??0,
                              LuyKeTTKHFirstDate = fst.LuyKeThanhTienKeHoach??0,
                              TTTCFirstDate = fst.ThanhTienThiCong??0,
                              LuyKeTTTCFirstDate = fst.LuyKeThanhTienThiCong??0,

                              TTKHLastDate = lst.ThanhTienKeHoach??0,
                              LuyKeTTKHLastDate = lst.LuyKeThanhTienKeHoach??0,
                              TTTCLastDate = lst.ThanhTienThiCong??0,
                              LuyKeTTTCLastDate = lst.LuyKeThanhTienThiCong??0,

                          }).ToList();
                foreach (var item in KLS)
                {

                    item.KLKHInRange = item.LuyKeKLKHLastDate - item.LuyKeKLKHFirstDate + (item.KLKHFirstDate??0);
                    item.KLTCInRange = item.LuyKeKLTCLastDate - item.LuyKeKLTCFirstDate + (item.KLTCFirstDate??0);
                    item.TTKHInRange = item.LuyKeTTKHLastDate - item.LuyKeTTKHFirstDate + (item.TTKHFirstDate ?? 0);
                    item.TTTCInRange = item.LuyKeTTTCLastDate - item.LuyKeTTTCFirstDate + (item.TTTCFirstDate ?? 0);

                }
                return KLS;

            }
            catch (Exception ex)
            {
                string err = $"{ex.Message}; Inner: {ex.InnerException?.Message}";
                Logging.Error(err);
                AlertShower.ShowInfo("Lỗi tính KLHN!");
            }
            return new List<KLHNBriefViewModel>();
        }
        
        //public static List<MultiKLHNBriefViewModel> CalcKLHNMultiBrief(TypeKLHN type, IEnumerable<string> lsCodeMain,
        //    List<KeyValuePair<DateTime?, DateTime?>> dates)
        //{
        //    var workloadTableName = type.GetEnumDescription();
        //    if (string.IsNullOrEmpty(workloadTableName))
        //    {
        //        AlertShower.ShowInfo("Phần tính khối lượng đang phát triển");
        //        return new List<MultiKLHNBriefViewModel>() { };
        //    }
        //    try
        //    {
        //        var ret = new List<MultiKLHNBriefViewModel>();
        //        foreach (var item in dates)
        //        {
        //            ret.Add(new MultiKLHNBriefViewModel() { Briefs = CalcKLHNBrief(type, lsCodeMain, item.Key, item.Value) });
        //        }
        //        return ret;

        //    }
        //    catch (Exception ex)
        //    {
        //        string err = $"{ex.Message}; Inner: {ex.InnerException?.Message}";
        //        Logging.Error(err);
        //        AlertShower.ShowInfo("Lỗi tính KLHN!");
        //    }
        //    return new List<MultiKLHNBriefViewModel>();
        //}
        
        
        
       


        public static Dictionary<DateTime, double> TinhLaiKLKeHoach(DateTime NBD, DateTime NKT, List<DateTime> lsNgayNghi, double tongNC, Dictionary<DateTime, double> dicNCHangNgay, bool isTuPhanBo)
        {
            int TongSoNgay = (NKT.Date - NBD.Date).Days + 1;
            int soNgayThiCong = TongSoNgay - lsNgayNghi.Count;
            
            var pairsInNgayTCdonotHaveNC = dicNCHangNgay.Where(x => x.Value == 0 & !lsNgayNghi.Contains(x.Key)).ToArray();

            foreach (var item in pairsInNgayTCdonotHaveNC)
            {
                double? NCBefore = dicNCHangNgay.Where(x => x.Value > 0 && x.Key < item.Key).Select(x => x.Value).LastOrDefault();
                double? NCAfter = dicNCHangNgay.Where(x => x.Value > 0 && x.Key > item.Key).Select(x => x.Value).FirstOrDefault();

                if (!NCBefore.HasValue && !NCAfter.HasValue)
                    dicNCHangNgay[item.Key] = tongNC / soNgayThiCong;
                else if (!NCBefore.HasValue)
                    dicNCHangNgay[item.Key] = NCAfter.Value;
                else if (!NCAfter.HasValue)
                    dicNCHangNgay[item.Key] = NCBefore.Value;
                else
                    dicNCHangNgay[item.Key] = (NCBefore.Value + NCAfter.Value) / 2;

            }

            var pairsInNgayNghiHaveNC = dicNCHangNgay.Where(x => lsNgayNghi.Contains(x.Key) && x.Value > 0).ToArray();

            foreach (var item in pairsInNgayNghiHaveNC)
            {
                try
                {
                    dicNCHangNgay[item.Key] = 0;
                }
                catch (Exception ex)
                {
                    continue;
                }
            }

            double tongNhanCongDaThem = dicNCHangNgay.Where(x => !lsNgayNghi.Contains(x.Key)).Sum(x => x.Value);

            while (tongNhanCongDaThem > 0 && Math.Abs(tongNC - tongNhanCongDaThem) > 0.1)
            {
                tongNhanCongDaThem = dicNCHangNgay.Where(x => !lsNgayNghi.Contains(x.Key)).Sum(x => x.Value);
                double NCConLai = tongNC - tongNhanCongDaThem;

                var pairsThiCong = dicNCHangNgay.Where(x => !lsNgayNghi.Contains(x.Key)).ToArray();
                int soNgay = pairsThiCong.Count();

                foreach (var item in pairsThiCong)
                {
                    var crNC = item.Value;
                    double NCBu = Math.Round((double)NCConLai / (double)soNgay, 2);
                    dicNCHangNgay[item.Key] = crNC + NCBu;
                    NCConLai -= NCBu;
                    soNgay--;
                }
            }
            return dicNCHangNgay;
        }

        public static long Fcn_UpdateGiaTriHopDong(string CodeHD)
        {
            string db_String = $"SELECT * FROM {MyConstant.TBL_HopDong_PhuLuc} " +
                    $"INNER JOIN {MyConstant.TBL_ThongtinphulucHD} " +
                    $"ON {MyConstant.TBL_HopDong_PhuLuc}.CodePl={MyConstant.TBL_ThongtinphulucHD}.Code " +
                    $"INNER JOIN {MyConstant.TBL_tonghopdanhsachhopdong} " +
                    $"ON {MyConstant.TBL_ThongtinphulucHD}.CodeHd={MyConstant.TBL_tonghopdanhsachhopdong}.Code " +
                    $" WHERE {MyConstant.TBL_tonghopdanhsachhopdong}.CodeHopDong='{CodeHD}'";
            DataTable HD = DataProvider.InstanceTHDA.ExecuteQuery(db_String);
            if (HD.Rows.Count == 0)
                return 0;
            long ThanhTien=(long)Math.Round(HD.AsEnumerable().Sum(x =>double.Parse(x["KhoiLuong"].ToString()) * double.Parse(x["DonGia"].ToString())));
            //double ThanhTienaff=HD.AsEnumerable().Sum(x =>double.Parse(x["KhoiLuong"].ToString()) * double.Parse(x["DonGia"].ToString()));
            db_String = $"UPDATE {MyConstant.Tbl_TAOMOIHOPDONG} SET \"GiaTriHopDong\"='{ThanhTien}' WHERE \"Code\"='{CodeHD}'";
            DataProvider.InstanceTHDA.ExecuteNonQuery(db_String);
            return ThanhTien;
        }
        public static void fcn_CapNhatNgayChoToanBoCongViecLienQuan(string CodeCVCon, DateTime BDHT, DateTime KTHT)
        {

            string dbString = $"SELECT * FROM {GiaoViec.TBL_QUYTRINHTHUCHIEN} WHERE \"CodeCongViecConHienTai\"='{CodeCVCon}'";
            DataTable dt = DataProvider.InstanceTHDA.ExecuteQuery(dbString);

            foreach (DataRow r in dt.Rows)
            {
                dbString = $"SELECT \"Bắt đầu\", \"Kết thúc\" FROM {GiaoViec.TBL_CONGVIECCON} WHERE \"CodeCongViecCon\"='{r["CodeCongViecConTiepTheo"]}'";
                DataTable dtCVTT = DataProvider.InstanceTHDA.ExecuteQuery(dbString);

                if (dtCVTT.Rows.Count == 0)
                    return;
                DataRow crRow = dtCVTT.Rows[0];
                int soNgayTH = DateTime.Parse(crRow["Kết thúc"].ToString()).Subtract(DateTime.Parse(crRow["Bắt đầu"].ToString())).Days;
                int soNgaySoVoiCVtruoc = int.Parse(r["SoNgay"].ToString());
                string typeOfCV = r["LoaiTuongQuan"].ToString();

                DateTime BDnew, KTnew;
                switch (typeOfCV)
                {
                    case "Bắt đầu":
                        BDnew = BDHT.AddDays(soNgaySoVoiCVtruoc);
                        break;
                    case "Trước so với kết thúc":
                        BDnew = KTHT.AddDays(-soNgaySoVoiCVtruoc);
                        break;
                    default:
                        BDnew = KTHT.AddDays(soNgaySoVoiCVtruoc);
                        break;
                }

                KTnew = BDnew.AddDays(soNgayTH);

                dbString = $"UPDATE {GiaoViec.TBL_CONGVIECCON} SET " +
                    $"\"Bắt đầu\" = '{BDnew.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE)}', " +
                    $"\"Kết thúc\" = '{KTnew.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE)}' " +
                    $"WHERE \"CodeCongViecCon\"='{r["CodeCongViecConTiepTheo"]}'";
                DataProvider.InstanceTHDA.ExecuteNonQuery(dbString);

            }
        }

        public static DataTable fcn_Reoder1Row(DataTable dt, DataRow r, int ind)
        {
            int ind1 = dt.Rows.IndexOf(r);

            if (ind1 == -1)
            {
                return null;
            }

            DataRow tempRow = dt.NewRow();
            tempRow.ItemArray = r.ItemArray;
            dt.Rows.InsertAt(tempRow, ind);
            dt.Rows.Remove(r);
            dt.AcceptChanges();
            return dt;

        }

        /// <summary>
        /// Thay đổi lên xuống dữ liệu trong bảng
        /// </summary>
        /// <param name="dbPath">File cơ sở dữ liệu</param>
        /// <param name="tenTbl">Tên bảng chứa thông tin cần thay đổi</param>
        /// <param name="tenCot">Tên cột khóa chính</param>
        /// <param name="priCode">Giá trị khóa chính đã chọn</param>
        /// <param name="tenCotCha">Tên cột tham chiếu trong bảng cha</param>
        /// <param name="codeCotCha">Code tham chiều tới dữ liệu hiện tại</param>
        /// <param name="isUp">True: Di chuyển lên, False: Di chuyển xuống</param>
        /// <returns></returns>
        public static int fcn_ReoderData(string tenTbl, string tenCot, string priCode, string tenCotCha, string codeCotCha, bool isUp)
        {
            if (!string.IsNullOrEmpty(tenCotCha))
            {
                string dbstring = $"SELECT * FROM {tenTbl} WHERE \"{tenCotCha}\"='{codeCotCha}' ORDER BY \"SortId\" DESC";
                DataTable dt = DataProvider.InstanceTHDA.ExecuteQuery(dbstring);
                //DataTable dt1 = dt.Clone();
                List<DataRow> lsRowAll = dt.Select($"[{tenCotCha}]='{codeCotCha}'").ToList();
                DataRow crRow = dt.Select($"[{tenCot}]='{priCode}'").First();
                //DataRow rowinLs = lsRowAll.Where(r => r == crRow).First();
                if (crRow == null)
                {
                    MessageShower.ShowInformation("Không thể di chuyển công tác");
                    return -1;
                }

                int crInd = lsRowAll.IndexOf(crRow);
                int ind2Insert = 0;
                if (isUp)
                {
                    if (crInd < 1)
                    {
                        MessageShower.ShowInformation("Không thể di chuyển công tác trên cùng");
                        return -1;
                    }

                    ind2Insert = dt.Rows.IndexOf(lsRowAll[crInd - 1]);
                }    
                else
                {
                    if (crInd == lsRowAll.Count - 1)
                    {
                        MessageShower.ShowInformation("Không thể di chuyển công tác dưới cùng");
                        return -1;
                    }

                    ind2Insert = dt.Rows.IndexOf(lsRowAll[crInd + 1])+1;
                }    

                MyFunction.fcn_Reoder1Row(dt, crRow, ind2Insert);
                int cntRow = dt.Rows.Count;
                //DataTable dt1 = dt.Clone();
                for (int i = 0; i < cntRow; i++)
                {
                    dt.Rows[i]["SortId"] = cntRow - 1 - i;
                }
                DataProvider.InstanceTHDA.UpdateDataTableFromSqliteSource(dt, tenTbl);
            }
            return 0;
        }


        public static int xemTruocFileCoBan(FileViewModel file, Control parent = null, string extension = null)
        {
            if (parent != null)
            {
                foreach (Control ctrl in parent.Controls)
                {
                    ctrl.Dispose();
                }
            }

            if (extension is null)
                extension = Path.GetExtension(file.FileNameInDb);


            if (CommonConstants.GetFileDocExt().Contains(extension))
            {
                xemTruocWord(file, parent);
            }
            else if (CommonConstants.GetFileExcelExt().Contains(extension) || CommonConstants.GetFileCsvExt().Contains(extension))
            {
                xemTruocEXECL(file, parent);

            }
            else if (CommonConstants.GetFilePdfExt().Contains(extension))
            {
                xemTruocPDF(file, parent);
            }
            else if (CommonConstants.GetFileImgExt().Contains(extension))
            {
                xemTruocHINHANH(file, parent);

            }
            else
            {
                MessageShower.ShowInformation("Không hỗ trợ xem trước file này! Bạn có thể tải file về để xem trên máy tính!");
                return 1;
            }

            return 0;
        }

        public static void xemTruocWord(FileViewModel file, Control parent = null)
        {
            RichEditControl word;
            XtraForm_wordPreview form = null;
            if (parent is null)
            {
                form = new XtraForm_wordPreview();
                word = form.GetRec();
            }
            else
            {
                word = new RichEditControl();
                parent.Controls.Add(word);
            }
            word.Unit = DevExpress.Office.DocumentUnit.Centimeter;

            word.RemoveShortcutKey(Keys.S, Keys.Control);
            word.ModifiedChanged += (s, e) =>
            {
                if (!word.Modified)
                    word.Parent.Text = file.FileNameInDb;
                else
                    word.Parent.Text = "*" + file.FileNameInDb;
            };

            if (file.Table.HasValue())
            {
                word.PopupMenuShowing += (s, e) =>
                {
                    RichEditMenuItem myItem = new RichEditMenuItem("Lưu đè mẫu hiện tại",
                        new EventHandler((s1, e1) =>
                        {
                            //using (FileStream stream = new FileStream("D:\\Doc.xlsx", FileMode.Create, FileAccess.ReadWrite))
                            //{
                            if (!word.Modified)
                            {
                                MessageShower.ShowInformation("File chưa có sự thay đổi");
                                return;
                            }
                            else
                            {


                                word.Document.SaveDocument(file.FilePath, DevExpress.XtraRichEdit.DocumentFormat.OpenXml);

                                var crCRC = FileHelper.CalculateFileMD5(file.FilePath);
                                string dbString = $"UPDATE {file.Table} SET Checksum = '{crCRC}' WHERE Code = '{file.Code}'";
                                DataProvider.InstanceTHDA.ExecuteNonQuery(dbString);
                                file.Checksum = crCRC;
                                MessageShower.ShowInformation("Đã cập nhật");
                                
                                return;
                            }
                            //}
                        }));
                
                    e.Menu.Items.Add(myItem);
                };
            }
           
            word.Dock = DockStyle.Fill;

            try
            {
                FileHelper.fcn_wordStreamDocument(word, file.FilePath);
            }
            catch
            {
                MessageShower.ShowInformation("Lỗi tải file");
            }

            //if (parent == null)
            //{

               
            //    using (var fm_xt = new Form())
            //    {
            //        fm_xt.WindowState = FormWindowState.Maximized;
            //        fm_xt.Controls.Add(word);
            //        fm_xt.Text = file.FileNameInDb;
            //        fm_xt.ShowDialog();
            //    }
            //}
            //else
            //{
            //    parent.Controls.Add(word);
            //    parent.Text = file.FileNameInDb;
            //}
            word.Parent.Text = file.FileNameInDb;
            if (form != null)
                form.ShowDialog();
            
        }
        public static void xemTruocEXECL(FileViewModel file, Control parent = null)
        {
            SpreadsheetControl spsheet;// = new SpreadsheetControl();
            XtraForm_ExcelPreview form = null;
            if (parent is null)
            {
                form = new XtraForm_ExcelPreview();
                spsheet = form.GetSpsheet();
            }
            else
            {
                spsheet = new SpreadsheetControl();
                parent.Controls.Add(spsheet);
            }    

            spsheet.RemoveShortcutKey(Keys.S, Keys.Control);
            spsheet.ModifiedChanged += (s, e) =>
            {
                if (!spsheet.Modified)
                    spsheet.Parent.Text = file.FileNameInDb;
                else
                    spsheet.Parent.Text = "*" + file.FileNameInDb;
            };
            spsheet.Unit = DevExpress.Office.DocumentUnit.Centimeter;
            if (file.Table.HasValue())
            {
                spsheet.PopupMenuShowing += (s, e) =>
                {
                    SpreadsheetMenuItem myItem = new SpreadsheetMenuItem("Lưu đè mẫu hiện tại",
                        new EventHandler((s1, e1) =>
                        {
                            //using (FileStream stream = new FileStream("D:\\Doc.xlsx", FileMode.Create, FileAccess.ReadWrite))
                            //{
                            if (!spsheet.Modified)
                            {
                                MessageShower.ShowInformation("File chưa có sự thay đổi");
                                return;
                            }
                            else
                            {
                                spsheet.Document.SaveDocument(file.FilePath, DevExpress.Spreadsheet.DocumentFormat.OpenXml);
 

                                var crCRC = FileHelper.CalculateFileMD5(file.FilePath);
                                string dbString = $"UPDATE {file.Table} SET Checksum = '{crCRC}' WHERE Code = '{file.Code}'";
                                DataProvider.InstanceTHDA.ExecuteNonQuery(dbString);
                                file.Checksum = crCRC;
                                MessageShower.ShowInformation("Đã cập nhật");
                                return;
                            }
                            //}
                        }));

                    e.Menu.Items.Add(myItem);
                };
            }

            spsheet.Dock = DockStyle.Fill;

            try
            {
                FileHelper.fcn_spSheetStreamDocument(spsheet, file.FilePath);
            }
            catch
            {
                MessageShower.ShowInformation("Lỗi tải file");
            }

            spsheet.Parent.Text = file.FileNameInDb;
            if (form != null)
                form.ShowDialog();


        }
        //}
        //else
        //{
        //    var fs = new FileStream(filename, FileMode.Open, FileAccess.Read);
        //    ctrl.LoadDocument(fs);
        //    fs.Close();
        //    fs.Dispose();
        //    ctrl.Dock = DockStyle.Fill;
        //    ctrl.BringToFront();
        //}


        public static void xemTruocPDF(FileViewModel file, Control parent = null)
        {
            PdfViewer pdf = new PdfViewer();

            pdf.Dock = DockStyle.Fill;

            if (parent == null)
            {

                try
                {
                    pdf.LoadDocument(file.FilePath);
                }
                catch
                {
                    MessageShower.ShowInformation("Lỗi tải file");
                }
                using (Form fm_xt = new Form())
                {
                    fm_xt.WindowState = FormWindowState.Maximized;
                    fm_xt.Text = file.FileNameInDb;
                    fm_xt.Controls.Add(pdf);
                    fm_xt.ShowDialog();

                }
            }
            else
            {
                try
                {
                    FileHelper.fcn_PdfStreamDoc(pdf, file.FilePath);
                }
                catch
                {
                    MessageShower.ShowInformation("Lỗi tải file");
                }
                parent.Controls.Add(pdf);
                parent.Text = file.FileNameInDb;
            }

        }
        public static void xemTruocHINHANH(FileViewModel file, Control parent = null)
        {

            PictureBox hinh = new PictureBox();
            hinh.SizeMode = PictureBoxSizeMode.StretchImage;
            hinh.Dock = DockStyle.Fill;
            try
            {

                FileHelper.fcn_ImageStreamDoc(hinh, file.FilePath);
            }
#pragma warning disable CS0168 // The variable 'ex' is declared but never used
            catch (Exception ex)
#pragma warning restore CS0168 // The variable 'ex' is declared but never used
            {
                MessageShower.ShowInformation("Lỗi tải file");
            }

            if (parent == null)
            {
                using (Form fm_xt = new Form())
                {
                    fm_xt.WindowState = FormWindowState.Maximized;
                    fm_xt.Text = file.FileNameInDb;
                    fm_xt.Controls.Add(hinh);
                    fm_xt.ShowDialog();
                }
            }
            else
            {
                parent.Controls.Add(hinh);
                parent.Text = file.FileNameInDb;
            }


        }


        public static Dictionary<string, string> fcn_getDicOfColumn(CellRange range, int row2getName = 0)
        {
            Worksheet ws = range.Worksheet;
            IWorkbook wb = ws.Workbook;
            Dictionary<string, string> dic = new Dictionary<string, string>();
            //Cập nhật Dic
            //CellRange range = wb.Range[rangeName];
            for (int i = range.LeftColumnIndex; i <= range.RightColumnIndex; i++)
            {
                if (!string.IsNullOrEmpty(ws.Rows[row2getName][i].Value.ToString()))
                    dic.Add(ws.Rows[row2getName][i].Value.ToString(), ws.Columns[i].Heading);
            }
            return dic;
        }     
        public static Dictionary<string, string> Fcn_getDicOfColumnPhuLuc(CellRange range, int row2getName = 0)
        {
            Worksheet ws = range.Worksheet;
            IWorkbook wb = ws.Workbook;
            Dictionary<string, string> dic = new Dictionary<string, string>();
            //Cập nhật Dic
            //CellRange range = wb.Range[rangeName];
            for (int i = range.LeftColumnIndex; i <= range.RightColumnIndex; i++)
            {
                if (!string.IsNullOrEmpty(ws.Rows[row2getName][i].Value.ToString()))
                    dic.Add(ws.Rows[row2getName][i].Value.ToString(), ws.Rows[row2getName+1][i].Value.ToString());
            }
            return dic;
        }
        //public static void fcn_Importdatabasetoexecl(DevExpress.XtraSpreadsheet.SpreadsheetControl name, string database,string code)
        //{
        //    string queryStr = $"SELECT *  FROM {database} WHERE \"CodeDuAn\"='{code}' ORDER BY \"SortId\"";
        //    DataTable dt = DataProvider.InstanceTHDA.ExecuteQuery(queryStr);
        //    if (dt.Rows.Count == 0)
        //        return;
        //    IWorkbook workbook = name.Document;
        //    Worksheet worksheet = workbook.Worksheets[0];
        //    CellRange rangeXD = worksheet.Range[MyConstant.TBL_QUYETTHONGTIN];
        //    name.BeginUpdate();
        //    //worksheet.Clear(worksheet[rangeXD.]);
        //    if (dt.Rows.Count >= rangeXD.RowCount-1)
        //        worksheet.Rows.Insert(rangeXD.BottomRowIndex, dt.Rows.Count - rangeXD.RowCount + 2, RowFormatMode.FormatAsPrevious);
        //    Dictionary<string, string> namedic = MyFunction.fcn_getDicOfColumn(worksheet.Range[MyConstant.TBL_QUYETDICWS]);
        //    for (int i = 0; i < dt.Rows.Count; i++)
        //    {
        //        DataRow rowCon = dt.Rows[i];
        //        foreach (var item in namedic)
        //        {
        //            if (dt.Columns.Contains(item.Key))
        //            {
        //                worksheet.Rows[rangeXD.TopRowIndex + i][item.Value].SetValueFromText(rowCon[item.Key].ToString(), true);
        //            }
        //            if (item.Key == "FileDinhKem")
        //            {
        //                string filedinhkem = item.Value + (i + 3).ToString();
        //                worksheet.Hyperlinks.Add(worksheet.Cells[filedinhkem], $"{filedinhkem}", false, "XEM FILE");
        //            }
        //        }
        //    }
        //    name.EndUpdate();
        //}
        public static void fcn_calsum(DevExpress.XtraSpreadsheet.SpreadsheetControl name,int location)
        {
            Worksheet ws = name.Document.Worksheets[0];
            Dictionary<string, string> Name = MyFunction.fcn_getDicOfColumn(ws.Range[MyConstant.TBL_QUYETDICWS]);
            CellRange rangeXD = ws.Range[MyConstant.TBL_QUYETTHONGTIN];
            int beginindex = 0;
            int endindex = 0;
            int vitri = 0;
            for (int i = location; i >= rangeXD.TopRowIndex; i--)
            {
                Row crRow = ws.Rows[i];
                string kyhieu = crRow[Name[ThuChiTamUng.COL_THUCHI_THUCHIEN]].Value.ToString();
                if (kyhieu == "THÊM GIẢI CHI")
                    endindex = i;
                if (kyhieu == "THÊM CHI PHÍ")
                    beginindex = i;
                if (kyhieu == "" && ws.Rows[i][Name[ThuChiTamUng.COL_THUCHI_CAPNHAP]].Value.ToString() == "CẬP NHẬP")
                {
                    vitri = i;
                    break;
                }
            }
            ws.Rows[vitri][Name[ThuChiTamUng.COL_THUCHI_SOTIENGIAICHI]].Formula = $"SUM({Name[ThuChiTamUng.COL_THUCHI_SOTIENGIAICHI]}{beginindex + 2}:{Name[ThuChiTamUng.COL_THUCHI_SOTIENGIAICHI]}{endindex + 1})";
        }
        //public static string Parse2SQLiteDate(string date)
        public static void fcn_update(int vitri, DevExpress.XtraSpreadsheet.SpreadsheetControl name,string m_codeduan)
        {
            Worksheet ws = name.Document.Worksheets[0];
            Worksheet ws1 = name.Document.Worksheets[1];
            Dictionary<string, string> Name = MyFunction.fcn_getDicOfColumn(ws.Range[MyConstant.TBL_QUYETDICWS]);
            CellRange rangeXD = ws.Range[MyConstant.TBL_QUYETTHONGTIN];
            string codekhoanchi = ws.Rows[vitri][Name[ThuChiTamUng.COL_THUCHI_CODE]].Value.ToString();
            string codetamung = ws.Rows[vitri][Name[ThuChiTamUng.COL_THUCHI_CODE]].Value.ToString();
            string dbString = $"UPDATE  {MyConstant.Tbl_ThuchiTamUng_Khoanchi} SET \"Duyet\"='{true}' WHERE \"Code\"='{codekhoanchi}'";
            DataProvider.InstanceTHDA.ExecuteNonQuery(dbString);
            string queryStr = $"SELECT *  FROM {MyConstant.Tbl_ThuchiTamUng_Khoanchi_cp} WHERE \"CodeDuAn\"='{m_codeduan}' AND \"CodeKhoanChi\"='{codekhoanchi}'";
            DataTable DT_cp = DataProvider.InstanceTHDA.ExecuteQuery(queryStr);
            string queryStr1 = $"SELECT *  FROM {MyConstant.Tbl_ThuchiTamUng_Khoanchi_gc} WHERE \"CodeDuAn\"='{m_codeduan}' AND \"CodeKhoanChi\"='{codekhoanchi}'";
            DataTable DT_gc = DataProvider.InstanceTHDA.ExecuteQuery(queryStr1);
            int sortID_cp = 0;
            int sortID_gc = 0;
            DT_cp.Columns.Add("Đã thêm", typeof(bool));
            DT_cp.AsEnumerable().ForEach(x => x["Đã thêm"] = false);
            DT_gc.Columns.Add("Đã thêm", typeof(bool));
            DT_gc.AsEnumerable().ForEach(x => x["Đã thêm"] = false);
            DataRow rowdt;
            int j = 0;
            int k = 0;
            for (int i = vitri + 1; i <= rangeXD.BottomRowIndex; i++)
            {
                Row crRow = ws.Rows[i];
                string code = ws.Rows[i][Name[ThuChiTamUng.COL_THUCHI_CODE]].Value.ToString();
                if (ws.Rows[i][Name[ThuChiTamUng.COL_THUCHI_CAPNHAP]].Value.ToString() == "CẬP NHẬP")
                    break;
                if (code == "")
                {
                    if (ws.Rows[i][Name[ThuChiTamUng.COL_THUCHI_CHIPHIGIAICHI]].Value.ToString() == "Chi phí")
                    {
                        crRow[Name[ThuChiTamUng.COL_THUCHI_CODE]].Value = Guid.NewGuid().ToString();
                        crRow[Name[MyConstant.COL_HD_CodeDuAn]].Value = m_codeduan;
                        crRow[Name[ThuChiTamUng.COL_THUCHI_CODEKHOANCHI]].Value = codekhoanchi;
                        rowdt = DT_cp.NewRow();
                        rowdt["SortId"] = sortID_cp;
                        rowdt["Đã thêm"] = true;
                        foreach (var str in Name)
                        {
                            if (DT_cp.Columns.Contains(str.Key))
                            {
                                if (str.Key == "SoTien")
                                    rowdt[str.Key] = Double.Parse(crRow[str.Value].Value.ToString());
                                else
                                    rowdt[str.Key] = crRow[str.Value].Value.ToString();
                            }
                        }
                        DT_cp.Rows.Add(rowdt);
                        sortID_cp++;
                    }
                    if (ws.Rows[i][Name[ThuChiTamUng.COL_THUCHI_CHIPHIGIAICHI]].Value.ToString() == "Giải chi")
                    {
                        crRow[Name[ThuChiTamUng.COL_THUCHI_CODE]].Value = Guid.NewGuid().ToString();
                        crRow[Name[MyConstant.COL_HD_CodeDuAn]].Value = m_codeduan;
                        crRow[Name[ThuChiTamUng.COL_THUCHI_CODEKHOANCHI]].Value = codekhoanchi;
                        rowdt = DT_gc.NewRow();
                        rowdt["SortId"] = sortID_gc;
                        rowdt["Đã thêm"] = true;
                        foreach (var str in Name)
                        {
                            if ((DT_gc.Columns.Contains(str.Key)))
                            {
                                if (str.Key == "GiaiChiKinhPhi")
                                    rowdt[str.Key] = Double.Parse(crRow[str.Value].Value.ToString());
                                else
                                {
                                    if (str.Key == "SoTien")
                                        break;
                                    else
                                        rowdt[str.Key] = crRow[str.Value].Value.ToString();
                                }
                            }
                        }
                        DT_gc.Rows.Add(rowdt);
                        sortID_gc++;
                    }
                }
                if (code != "" && code != "Code")
                {
                    if (ws.Rows[i][Name[ThuChiTamUng.COL_THUCHI_CHIPHIGIAICHI]].Value.ToString() == "Chi phí")
                    {
                        DT_cp.Rows[j]["SortId"] = sortID_cp;
                        DT_cp.Rows[j]["Đã thêm"] = true;
                        foreach (var str in Name)
                        {
                            if (DT_cp.Columns.Contains(str.Key))
                            {
                                if (str.Key == "SoTien")
                                    DT_cp.Rows[j][str.Key] = Double.Parse(crRow[str.Value].Value.ToString());
                                else
                                    DT_cp.Rows[j][str.Key] = crRow[str.Value].Value.ToString();
                            }
                        }
                        sortID_cp++;
                        j++;
                    }          
                    if (ws.Rows[i][Name[ThuChiTamUng.COL_THUCHI_CHIPHIGIAICHI]].Value.ToString() == "Giải chi")
                    {
                        DT_gc.Rows[k]["SortId"] = sortID_gc;
                        DT_gc.Rows[k]["Đã thêm"] = true;
                        foreach (var str in Name)
                        {
                            if ((DT_gc.Columns.Contains(str.Key)))
                            {
                                if (str.Key == "GiaiChiKinhPhi")
                                    DT_gc.Rows[k][str.Key] = Double.Parse(crRow[str.Value].Value.ToString());
                                else
                                {
                                    if (str.Key == "SoTien")
                                        break;
                                    else
                                        DT_gc.Rows[k][str.Key] = crRow[str.Value].Value.ToString();
                                }
                            }
                        }
                        sortID_gc++;
                        k++;
                    }
                }

            }
            DT_cp.Select("[Đã thêm] = false").ForEach(x => x.Delete());
            DT_gc.Select("[Đã thêm] = false").ForEach(x => x.Delete());
            DataProvider.InstanceTHDA.UpdateDataTableFromSqliteSource(DT_cp, MyConstant.Tbl_ThuchiTamUng_Khoanchi_cp);
            DataProvider.InstanceTHDA.UpdateDataTableFromSqliteSource(DT_gc, MyConstant.Tbl_ThuchiTamUng_Khoanchi_gc);
        }

        /// <summary>
        /// Tạo string dạng 'a', 'b', 'c' để query điều kiện IN trong cơ sở dữ liệu
        /// </summary>
        /// <param name="array"></param>
        /// <returns></returns>
        public static string fcn_Array2listQueryCondition(IEnumerable<string> array)
        {
            if (array is null)
                return "";
            var newArr = array.Where(x => x != null).Select(x => $"'{x.Replace("'", "''")}'");
            return string.Join(", ", newArr);
        }

        public static void fcn_GetTblCongTrinhVaHangMucOfDuAn(string codeDA, out DataTable dtCT, out DataTable dtHM)
        {
            string dbString = $"SELECT \"Code\", \"Ten\" FROM {MyConstant.TBL_THONGTINCONGTRINH} WHERE \"CodeDuAn\" = '{codeDA}'";
            dtCT = DataProvider.InstanceTHDA.ExecuteQuery(dbString);

            string lsCodeCT = MyFunction.fcn_Array2listQueryCondition(dtCT.AsEnumerable().Select(x => x["Code"].ToString()).ToArray());

            dbString = $"SELECT \"Code\", \"Ten\", \"CodeCongTrinh\" FROM {MyConstant.TBL_THONGTINHANGMUC} WHERE \"CodeCongTrinh\" IN ({lsCodeCT})";
            dtHM = DataProvider.InstanceTHDA.ExecuteQuery(dbString);
        }

        public static void SetValueToCellWorksheet(Cell cell, object obj)
        {
            cell.SetValueFromText((obj == null) ? "" : obj.ToString());
        }

        public static string fcn_GetSelectedDecriptionOfGroupRadio(RadioGroup rg)
        {
            return rg.Properties.Items[rg.SelectedIndex].Description;
        }

        public static void fcn_ReverseCell(SpreadsheetCellEventArgs e)
        {
            if (e.OldFormula != "")
            {
                e.Cell.Formula = e.OldFormula;
            }
            else e.Cell.Value = e.OldValue;
        }

        public static Dictionary<string, string> fcn_getdonvithuchien(DevExpress.XtraSpreadsheet.SpreadsheetControl name, bool methold, int rowindex,string CodeHD=null)
        {
            IWorkbook workbook = name.Document;
            Worksheet worksheet = workbook.Worksheets["Chi tiết phụ lục hợp đồng"];
            Dictionary<string, string> DIC_HOPDONG = MyFunction.fcn_getDicOfColumn(workbook.Range[MyConstant.TBL_QUYETDICWS]);
            //queryStr = $"SELECT * FROM {MyConstant.TBL_Tonghopdanhsachhopdong} WHERE \"CodeDuAn\"='{codeduan}' AND \"TenHopDong\"='{items[0]}' AND \"SoHopDong\"='{items[1]}'";
            //DataTable dt = DataProvider.InstanceTHDA.ExecuteQuery(queryStr);
            string queryStr=CodeHD is null? $"SELECT * FROM {MyConstant.TBL_TaoHopDongMoi} WHERE \"Code\"='{SharedControls.cbo_MeNuTenHopDong.SelectedValue}'":
                $"SELECT * FROM {MyConstant.TBL_TaoHopDongMoi} WHERE \"Code\"='{CodeHD}'";
            DataTable dt_HD = DataProvider.InstanceTHDA.ExecuteQuery(queryStr);
            if (dt_HD.Rows.Count == 0)
                return null;
            string codevitrithuchien = dt_HD.Rows[0]["CodeDonViThucHien"].ToString();
            Dictionary<string, string> codenhathau = new Dictionary<string, string>();
            foreach (DataRow row in dt_HD.Rows)
            {
                foreach (var item in MyConstant.DIC_TaomoiHD)
                {
                    if (row[item.Value].ToString() == codevitrithuchien)
                    {
                        if (methold)
                        {
                            if (item.Key == MyConstant.TBL_THONGTINDUAN)
                                queryStr = $"SELECT \"TenDuAn\" FROM {item.Key} WHERE \"Code\"='{codevitrithuchien}'";
                            else
                                queryStr = $"SELECT \"Ten\" FROM {item.Key} WHERE \"Code\"='{codevitrithuchien}'";
                            DataTable dt_dvth = DataProvider.InstanceTHDA.ExecuteQuery(queryStr);
                            worksheet.Rows[rowindex][DIC_HOPDONG[MyConstant.COL_HD_DVTH]].SetValue(dt_dvth.Rows[0][0].ToString());
                            string DonViThucHien = $"Đơn vị thực hiện: {dt_dvth.Rows[0][0].ToString().ToUpper()}";
                            worksheet.Rows[rowindex-3][DIC_HOPDONG[MyConstant.COL_HD_Tencongviec]].SetValue(DonViThucHien);
                        }
                        if (item.Key == MyConstant.TBL_THONGTINDUAN)
                            codenhathau.Add(MyConstant.TBL_THONGTINNHATHAU, codevitrithuchien);
                        else
                            codenhathau.Add(item.Key, codevitrithuchien);
                        break;
                    }
                }
            }
            return codenhathau;
        }

        public static Dictionary<string, int> fcn_getDicDate(CellRange cells)
        {
            Worksheet ws = cells.Worksheet;
            Dictionary<string, int> dic = new Dictionary<string, int>();
            Row RowDate = ws.Rows[cells.TopRowIndex];
            for (int  i = cells.LeftColumnIndex + 1; i < cells.RightColumnIndex; i++)
            {
                if (RowDate[i].Value.ToString() != "")
                    dic.Add(DateTime.Parse(RowDate[i].Value.ToString()).ToString(MyConstant.CONST_DATE_FORMAT_SQLITE), i);
            }

            return dic;
        }

        
        
        /*public static bool GetDateMinMaxRow(Row crRowKH, out DateTime dateBD, out DateTime dateKT)
        {

            dateBD = DateTime.Parse(crRowKH[dic_All[TDKH.COL_NgayBatDau]].Value.ToString());
            dateKT = DateTime.Parse(crRowKH[dic_All[TDKH.COL_NgayKetThuc]].Value.ToString());


            if ((ValidateHelper.IsDateTime(crRowKH[dic_All[TDKH.COL_NgayBatDauThiCong]].Value) && ValidateHelper.IsDateTime(crRowKH[dic_All[TDKH.COL_NgayKetThucThiCong]].Value)))
            {

                DateTime dateBDTemp = DateTime.Parse(crRowKH[dic_All[TDKH.COL_NgayBatDauThiCong]].Value.ToString());
                DateTime dateKTTemp = DateTime.Parse(crRowKH[dic_All[TDKH.COL_NgayKetThucThiCong]].Value.ToString());
                if (dateBDTemp < dateBD)
                    dateBD = dateBDTemp;

                if (dateKTTemp > dateKT)
                    dateKT = dateKTTemp;
            }
            return true;
        }*/

	    /*public static Dictionary<string, int> fcn_CapNhatColRangeNgay(CellRange rangeDate, DateTime dateBD, int firstColInd = -1, int lastColInd= -1)
        {
            Worksheet ws = rangeDate.Worksheet;
            int indRow = rangeDate.TopRowIndex;
            Row rowDate = ws.Rows[indRow];
            //Dictionary<string, int> dicDate = new Dictionary<string, int>();
            int type = Array.IndexOf(TDKH.sheetsName, ws.Name);
            int soCot1Ngay = TDKH.numColsPerDays[type];

            CellRange rangeData = ws.Range[TDKH.rangesNameData[type]];

            if (firstColInd < 0)
                firstColInd = rangeDate.LeftColumnIndex + 1;
            
            if (lastColInd < 0)
                lastColInd = rangeDate.RightColumnIndex - 1;
            
            for (int i = firstColInd; i< lastColInd; i++)
            {
                string crHeader = ws.Columns[i].Heading;
                string crHeaderTien = ws.Columns[i + 1].Heading;
                string lastHeader = ws.Columns[i + soCot1Ngay - 1].Heading;
                ws.Range[$"{crHeader}{rowDate.Index + 1}:{lastHeader}{rowDate.Index + 1}"].Merge();
                ws.Range.FromLTRB(i, rangeData.TopRowIndex, i, rangeData.BottomRowIndex).NumberFormat = MyConstant.FORMAT_KhoiLuong;
                ws.Range.FromLTRB(i + 1, rangeData.TopRowIndex, i + 1, rangeData.BottomRowIndex).NumberFormat = MyConstant.FORMAT_TIEN;
                rowDate[i].SetValue(dateBD);

                if (type == 1)
                {
                    ws.Rows[indRow + 1][i].SetValue("Khối lượng");
                    ws.Rows[indRow + 1][++i].SetValue("Thành tiền");
                }
                else if (type == 2)
                {
                    ws.Rows[indRow + 1][i].SetValue("Khối lượng");
                    ws.Rows[indRow + 1][++i].SetValue("Đơn giá");
                    ws.Rows[indRow + 1][++i].SetValue("Thành tiền");
                }
                //else if (type > 2)
                //{
                //    string crHeaderTTG = ws.Columns[i + 1].Heading;
                //    string crHeaderTTTC = ws.Columns[i + 4].Heading;
                //    ws.Rows[indRow + 1][++i].SetValue("Khối lượng kế hoạch");
                //    ws.Rows[indRow + 1][++i].SetValue("Thành tiền kế hoạch");
                //    ws.Rows[indRow + 1][++i].SetValue("Khối lượng thi công");
                //    ws.Rows[indRow + 1][++i].SetValue("Đơn giá thi công");
                //    ws.Rows[indRow + 1][++i].SetValue("Thành tiền thi công");
                //    //ws.Columns[i].Visible = ws.Columns[i - 1].Visible = isHienKeHoach;
                //    ws.Columns[crHeaderTTG].NumberFormat = ws.Columns[crHeaderTTTC].NumberFormat = MyConstant.FORMAT_TIEN;
                //}
                dateBD = dateBD.AddDays(1);
            }

            var dicDate = fcn_getDicDate(rangeDate);
            CapNhatAllRowRangeNgay(dicDate.Where(x => x.Value < lastColInd && x.Value >= firstColInd).ToDictionary(x => x.Key, x => x.Value),
                ws.Range[TDKH.rangesNameData[type]], TDKH.dicsAll[type]);


            return dicDate;
        }*/

        /*public static void CapNhatAllRowRangeNgay(Dictionary<string, int> dicDate, CellRange rangeData, Dictionary<string, string> dicData = null)
        {
            Worksheet ws = rangeData.Worksheet;
            ws.Workbook.BeginUpdate();
            int typeSheet = Array.IndexOf(TDKH.sheetsName, ws.Name);
            if (dicData is null)
            {
                dicData = TDKH.dicsAll[typeSheet];
            }
            Column colRowCha = ws.Columns[dicData[TDKH.COL_RowCha]];
            Column colTypeRow = ws.Columns[dicData[TDKH.COL_TypeRow]];
            int soCot1Ngay = TDKH.numColsPerDays[typeSheet];

            ws.Calculate();

            Row rowHeader = ws.Rows[rangeData.TopRowIndex - 1];
            for (int i = rangeData.TopRowIndex; i < rangeData.BottomRowIndex; i++)
            {
                Row crRowCha = ws.Rows[i];
                string typeRowCha = crRowCha[dicData[TDKH.COL_TypeRow]].Value.ToString();
                int[] indsCon = colRowCha.Search((crRowCha.Index + 1).ToString(), MyConstant.MySearchOptions).Select(x => x.RowIndex + 1).ToArray();
                if (!indsCon.Any())
                    continue;

                foreach (var item in dicDate)
                {
                    for (int j = 0; j < soCot1Ngay; j++)
                    {

                        if (rowHeader[item.Value + j].Value.ToString().StartsWith("Khối lượng") && typeRowCha != MyConstant.TYPEROW_CVCha)
                            continue;
                        
                        if (typeRowCha == MyConstant.TYPEROW_CVCha && typeSheet >=3 && j > 1 && crRowCha[item.Value + j].Font.Color.ToArgb() != MyColor.SumThiCong.ToArgb())
                        {
                            continue;
                        }

                        if (typeRowCha == MyConstant.TYPEROW_CVCha && j == 3)
                            continue;

                        string header = ws.Range.GetColumnNameByIndex(item.Value + j);
                        string lsCell = string.Join(";", indsCon.Select(x => $"{header}{x}"));
                        crRowCha[header].Formula = $"SUM({lsCell})";

                    }
                }
            }
            ws.Workbook.EndUpdate();

        }*/

        //public static void fcn_CovertdataHopdong(string codeduan)
        //{
        //    string queryStr = $"SELECT * FROM '{MyConstant.TBL_TaoHopDongMoi}' WHERE \"CodeDuAn\"='{codeduan}'";
        //    DataTable dt = DataProvider.InstanceTHDA.ExecuteQuery(queryStr);
        //    queryStr = $"SELECT * FROM '{MyConstant.TBL_Tonghopdanhsachhopdong}' WHERE \"CodeDuAn\"='{codeduan}'";
        //    DataTable dt_hd = DataProvider.InstanceTHDA.ExecuteQuery(queryStr);
        //    string dbString = "";
        //    foreach (DataRow rows in dt.Rows)
        //    {
        //        bool contains = dt_hd.AsEnumerable().Any(row => rows["Code"].ToString() == row.Field<String>("CodeHopDong"));
        //        if (!contains)
        //        {
        //            dbString = $"INSERT INTO '{MyConstant.TBL_Tonghopdanhsachhopdong}' (\"CodeHopDong\",\"Code\",\"CodeDuAn\") VALUES ('{rows["Code"]}','{Guid.NewGuid().ToString()}','{codeduan}')";
        //            DataProvider.InstanceTHDA.ExecuteQuery(dbString);
        //        }
        //    }

        //}
        public static IEnumerable<Cell> SearchRangeCell(Worksheet worksheet, dynamic data)
        {
            SearchOptions options = new SearchOptions();
            options.SearchBy = SearchBy.Columns;
            options.SearchIn = SearchIn.Values;
            options.MatchEntireCellContents = true;
            return worksheet.Search(data, options);
        }
        public static void fcn_TDKH_CopyDinhMuc(string codeCu, string codeMoi, string tbl)
        {
            string dbString = $"SELECT * FROM {tbl} WHERE \"CodeCongTac\" = '{codeCu}'";
            DataTable dt = DataProvider.InstanceTHDA.ExecuteQuery(dbString).DefaultView.ToTable();
            foreach (DataRow dr in dt.Rows)
            {
                dr["Code"] = Guid.NewGuid().ToString();
                dr["CodeCongTac"] = codeMoi;
            }

            DataProvider.InstanceTHDA.UpdateDataTableFromSqliteSource(dt, tbl);
        }

        //public static bool fcn_getDateStrBDKTFromRowWs(Row crRow, Dictionary<string, string> dic, out DateTime dateBD, out DateTime dateKT,out string ngayBD, out string ngayKT)
        //{

        //    //dateBD = DateTime.MaxValue;
        //    //dateKT = DateTime.MinValue;
        //    ////ngayBD = ngayKT = "";
        //    //for (int i = 0; i < 2; i++)
        //    //{
        //    //    if (!dic.ContainsKey(TDKH.typeColsNgayBatDau[i]))
        //    //    {
        //    //        continue;
        //    //    }

        //    //    DateTime dateBDTemp = DateTime.Parse(crRow[dic[TDKH.typeColsNgayBatDau[i]]].Value.ToString());
        //    //    DateTime dateKTTemp = DateTime.Parse(crRow[dic[TDKH.typeColsNgayKetThuc[i]]].Value.ToString());
                
                
        //    //    dateBD = (dateBD < dateBDTemp) ? dateBD : dateBDTemp;
        //    //    dateKT = (dateKT < dateKTTemp) ? dateKTTemp : dateKT;
        //    //}
        //    //ngayBD = dateBD.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE);
        //    //ngayKT = dateKT.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE);

        //    return true;
        //}


        public static void fcn_getDateStrBDKTFromDr(Row crRow, Dictionary<string, string> dic, out DateTime dateBD, out DateTime dateKT, out string ngayBD, out string ngayKT)
        {
            dateBD = DateTime.MaxValue;
            dateKT = DateTime.MinValue;
            //ngayBD = ngayKT = "";
            for (int i = 0; i < 2; i++)
            {
                if (!dic.ContainsKey(TDKH.typeColsNgayBatDau[i]))
                {
                    continue;
                }

                DateTime dateBDTemp = DateTime.Parse(crRow[dic[TDKH.typeColsNgayBatDau[i]]].Value.ToString());
                DateTime dateKTTemp = DateTime.Parse(crRow[dic[TDKH.typeColsNgayKetThuc[i]]].Value.ToString());
                dateBD = (dateBD < dateBDTemp) ? dateBD : dateBDTemp;
                dateKT = (dateKT < dateKTTemp) ? dateKTTemp : dateKT;
            }
            ngayBD = dateBD.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE);
            ngayKT = dateKT.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dateBDTC"></param>
        /// <param name="dateKTTC"></param>
        /// <param name="lsCodeVL"></param>
        /// <param name="ten"></param>
        /// <param name="loaiKhoiLuong">KhoiLuong or KhoiLuongThiCong</param>
        public static List<Chart_Gantt> fcn_TDKH_getListSourceSeries(DateTime dateBDTC, DateTime dateKTTC, string lsCodeVL, string ten, string loaiKhoiLuong)
        {
            List<Chart_Gantt> chartItemsThiCong = new List<Chart_Gantt>();
            DateTime crDateBDTC = dateBDTC;
            for (DateTime date = dateBDTC; date <= dateKTTC; date = date.AddDays(1))
            {
                string dateStr = date.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE);
                string dbString = $"SELECT * FROM {TDKH.TBL_KHVT_KhoiLuongHangNgay} WHERE \"Ngay\" = '{dateStr}' AND \"CodeVatTu\" IN ({lsCodeVL}) AND \"{loaiKhoiLuong}\" != '0'";
                DataTable dtKL = DataProvider.InstanceTHDA.ExecuteQuery(dbString);

                if (dtKL.AsEnumerable().Where(x => x["Ngay"].ToString() == dateStr).Any())
                {
                    if (date == dateKTTC)
                    {
                        Chart_Gantt item = new Chart_Gantt();
                        item.VatTu = ten;
                        if (loaiKhoiLuong == TDKH.COL_KhoiLuong)
                        {
                            item.NgayBatDau = crDateBDTC;
                            item.NgayKetThuc = dateKTTC.AddDays(1).AddSeconds(-1);
                        }
                        else
                        {
                            item.NgayBatDauThiCong = crDateBDTC;
                            item.NgayKetThucThiCong = dateKTTC.AddDays(1).AddSeconds(-1);
                        }
                        chartItemsThiCong.Add(item);
                    }
                    else
                    {
                        continue;
                    }
                }
                else
                {
                    if (crDateBDTC != date)
                    {
                        Chart_Gantt item = new Chart_Gantt();
                        item.VatTu = ten;
                        if (loaiKhoiLuong == TDKH.COL_KhoiLuong)
                        {
                            item.NgayBatDau = crDateBDTC;
                            item.NgayKetThuc = date.AddSeconds(-1);
                        }
                        else
                        {
                            item.NgayBatDauThiCong = crDateBDTC;
                            item.NgayKetThucThiCong = date.AddSeconds(-1);
                        }
                        chartItemsThiCong.Add(item);
                    }
                    crDateBDTC = date.AddDays(1);
                }
            }
            return chartItemsThiCong;
        }



        public static void fcn_TDKh_CapNhatNgayThiCong(string code, string codeHangNgay, DateTime date)
        {
            string dateString = date.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE);
            Worksheet ws = SharedControls.spsheet_TD_KH_LapKeHoach.Document.Worksheets[TDKH.SheetName_KeHoachKinhPhi];

            Dictionary<string, string> dic = MyFunction.fcn_getDicOfColumn(ws.GetUsedRange());
            Column colCode = ws.Columns[dic[TDKH.COL_Code]];
            var crRow = colCode.Search(code, MyConstant.MySearchOptions).Select(x => ws.Rows[x.RowIndex]).SingleOrDefault();
            //if (cell is null)
            //    return;

            string dbString = $"SELECT * FROM {TDKH.TBL_ChiTietCongTacTheoKy} WHERE Code = '{code}'";
            DataTable dt = DataProvider.InstanceTHDA.ExecuteQuery(dbString);
            DataRow drCTac = dt.Rows[0];

            if (DateTime.TryParse(drCTac["NgayBatDauThiCong"].ToString(), out DateTime dateBD) && dateBD > date)
            {
                drCTac["NgayBatDauThiCong"] = date.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE);
                if (crRow != null)
                    crRow[dic[TDKH.COL_NgayBatDauThiCong]].SetValue(date);
            }

            if (DateTime.TryParse(drCTac["NgayKetThucThiCong"].ToString(), out DateTime dateKT) && dateKT < date)
            {
                drCTac["NgayKetThucThiCong"] = date.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE);
                if (crRow != null)
                    crRow[dic[TDKH.COL_NgayKetThucThiCong]].SetValue(date);
            }

            DataProvider.InstanceTHDA.UpdateDataTableFromSqliteSource(dt, TDKH.TBL_ChiTietCongTacTheoKy);

            //Select codeHangNgay From TDKH_KhoiLuongCongViecTungNgay
            //dbString = $"SELECT * FROM {TDKH.TBL_KhoiLuongCongViecHangNgay} WHERE Code = '{codeHangNgay}'";
            //DataTable dtHangNgay = DataProvider.InstanceTHDA.ExecuteQuery(dbString);
            //DataRow drHangNgay = dtHangNgay.AsEnumerable().SingleOrDefault();

            //if (drHangNgay is null)
            //{
            //    drHangNgay = dtHangNgay.NewRow();
            //    dtHangNgay.Rows.Add(drHangNgay);
            //    drHangNgay["Code"] = Guid.NewGuid().ToString();
            //    drHangNgay["CodeCongTacTheoGiaiDoan"] = code;
            //    drHangNgay["Ngay"] = dateString;
            //}

            //drHangNgay["KhoiLuongThiCong"] = KL;
            //drHangNgay["GhiChu"] = ghiChu;
            //var dicDonGia = fcn_GetDicNgay_DonGia(code, TypeKLHN.CongTac);

            //double donGia = dicDonGia.ContainsKey(dateString) ? dicDonGia[dateString] : double.Parse(drCTac["DonGiaThiCong"].ToString());

            //drHangNgay["ThanhTienThiCong"] = KL*donGia;

            //DataProvider.InstanceTHDA.UpdateDataTableFromSqliteSource(dtHangNgay, TDKH.TBL_KhoiLuongCongViecHangNgay);


            //if (crRow != null)
            //    fcn_TDKH_CapNhatKeHoachKinhPhiChiTietTungNgay(crRow, isSaveToDb: false, dateUpDate: date);

        }

        /// <summary>
        /// Cập nhật range Ngày
        /// </summary>
        /// <param name="crRowWs"></param>
        /// <param name="dicDate"></param>
        /// <param name="cell">ô vừa sửa giá trị</param>
        //public static bool fcn_TDKH_CapNhatKeHoachKinhPhiChiTietTungNgay(Row crRowWs, Dictionary<string, int> dicDate = null,
        //    Cell cell = null, DataRow drHaoPhi = null, DataRow drCTac = null, Dictionary<string, double> dicDateKL = null, bool isSaveToDb = true, DateTime? dateUpDate = null)
        //{
        //    //Stopwatch sw = Stopwatch.StartNew();

        //    Worksheet ws = crRowWs.Worksheet;
        //    IWorkbook wb = ws.Workbook;
        //    wb.BeginUpdate();
        //    wb.Calculate();
        //    string rangeDataName;
        //    string rangeNgayName;//, col_NgayBatDau, col_NgayKetThuc;// = (ws.Name == TDKH.SheetName_KeHoachKinhPhi) ? TDKH.RANGE_KeHoachKPNgay : TDKH.RANGE_KLHangNgay_Ngay;
        //    Dictionary<string, string> dicData;
        //    string col_NgayBatDau = TDKH.COL_NgayBatDau;
        //    string col_NgayKetThuc = TDKH.COL_NgayKetThuc;



        //    switch (ws.Name)
        //    {
        //        case TDKH.SheetName_KeHoachKinhPhi:
        //            rangeDataName = TDKH.RANGE_KeHoach;
        //            rangeNgayName = TDKH.RANGE_KeHoachKP_Ngay;
        //            dicData = dic_All;

        //            //col_NgayBatDau = TDKH.COL_NgayBatDau;
        //            //col_NgayKetThuc = TDKH.COL_NgayKetThuc;
        //            break;
        //        case TDKH.SheetName_KLHangNgay:
        //            rangeDataName = TDKH.RANGE_KLHangNgay;
        //            rangeNgayName = TDKH.RANGE_KLHangNgay_Ngay;
        //            dicData = TDKH.dic_KLHangNgay_All;
        //            col_NgayBatDau = TDKH.COL_NgayBatDauThiCong;
        //            col_NgayKetThuc = TDKH.COL_NgayKetThucThiCong;
        //            break;
        //        //case TDKH.SheetName_VatLieu:
        //        //case TDKH.SheetName_NhanCong:
        //        //case TDKH.SheetName_MayThiCong:

        //        //    string textRutGon = MyFunction.fcn_RemoveAccents(ws.Name).Replace(" ", "");
        //        //    rangeDataName = $"KeHoachVatTu_{textRutGon}_TuDong";
        //        //    rangeNgayName = textRutGon + "_Ngay";
        //        //    dicData = dicKPVL;
        //        //    break;
        //        default:
        //            wb.EndUpdate();
        //            return false;
        //    }

        //    DefinedName rangeNgay = wb.DefinedNames.GetDefinedName(rangeNgayName);

        //    //Lấy danh sách khối lượng từng ngày
        //    string code = crRowWs[dicData[TDKH.COL_Code]].Value.ToString();
        //    int rowInd = crRowWs.Index;
        //    //Cập nhật % thực hiện
        //    //DateTime dateBD, dateKT;
        //    //int soNgay;
        //    CellRange rangeData = wb.Range[rangeDataName];

        //    Row rowTong = ws.Rows[rangeData.TopRowIndex];
        //    //MyFunction.fcn_getDateStrBDKTFromRowWs(rowTong, dicData, out DateTime dateBD, out DateTime dateKT, out string dateBDStr, out string dateKTStr);
        //    if (!ValidateHelper.IsDateTime(rowTong[dicData[col_NgayBatDau]].Value)
        //        || !ValidateHelper.IsDateTime(rowTong[dicData[col_NgayKetThuc]].Value))
        //    {
        //        wb.EndUpdate();
        //        return false;
        //    }

        //    if (!ValidateHelper.IsDateTime(crRowWs[dicData[col_NgayBatDau]].Value)
        //        || !ValidateHelper.IsDateTime(crRowWs[dicData[col_NgayKetThuc]].Value))
        //    {
        //        wb.EndUpdate();
        //        return false;
        //    }



        //    DateTime dateBD;// = DateTime.Parse(crRowWs[dicData[col_NgayBatDau]].Value.ToString());
        //    DateTime dateKT;// = DateTime.Parse(crRowWs[dicData[col_NgayKetThuc]].Value.ToString());
        //    if (!dateUpDate.HasValue)
        //    {
        //        dateBD = DateTime.Parse(crRowWs[dicData[col_NgayBatDau]].Value.ToString());
        //        dateKT = DateTime.Parse(crRowWs[dicData[col_NgayKetThuc]].Value.ToString());
        //    }
        //    else
        //    {
        //        dateBD = dateKT = dateUpDate.Value;
        //        //crRowWs[dicData[col_NgayBatDau]].
        //    }

        //    string dateBDStr = dateBD.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE);
        //    string dateKTStr = dateKT.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE);

        //    if (dicDate == null)
        //    {
        //        if (!DinhMucHelper.fcn_TDKH_CapNhatRangeNgayCongTac(rangeNgay, out dicDate, rowTong: rowTong))
        //        {
        //            wb.EndUpdate();
        //            return false;
        //        }
        //    }
        //    if (ws.Name == TDKH.SheetName_KeHoachKinhPhi)
        //    {
        //        string dbString = $"SELECT \"NgayBatDau\", \"NgayKetThuc\", \"NgayBatDauThiCong\", \"NgayKetThucThiCong\" FROM {TDKH.TBL_ChiTietCongTacTheoKy} WHERE \"Code\" = '{code}'";
        //        DataTable dtCTTK = DataProvider.InstanceTHDA.ExecuteQuery(dbString);
        //        DataRow rowCTTK = dtCTTK.Rows[0];

        //        dbString = $"DELETE FROM {TDKH.TBL_KhoiLuongCongViecHangNgay} WHERE \"CodeCongTacTheoGiaiDoan\" = '{code}' AND " +
        //            $"(\"Ngay\" < '{dateBDStr}' OR \"Ngay\" > '{dateKTStr}') AND " +
        //            $"(\"Ngay\" < '{rowCTTK["NgayBatDauThiCong"]}' OR \"Ngay\" > '{rowCTTK["NgayKetThucThiCong"]}')";
        //        DataProvider.InstanceTHDA.ExecuteNonQuery(dbString);

        //        dbString = $"SELECT * FROM {TDKH.TBL_KhoiLuongCongViecHangNgay} WHERE \"CodeCongTacTheoGiaiDoan\" = '{code}' AND " +
        //            $"(\"Ngay\" >= '{dateBDStr}' AND \"Ngay\" <= '{dateKTStr}')";
        //        DataTable dtCongTacTheoNgay = DataProvider.InstanceTHDA.ExecuteQuery(dbString);
        //        //string headerCell = ws.Columns[cell.ColumnIndex].Heading;

        //        if (cell != null)
        //        {
        //            bool isCotKhoiLuong = (cell.ColumnIndex - rangeNgay.Range.LeftColumnIndex) % 2 != 0; //True nếu cột khối lượng. False nếu thành tiền
        //            string date = DateTime.Parse(ws.Rows[wb.Range[TDKH.RANGE_KeHoachKP_Ngay].TopRowIndex][cell.ColumnIndex - ((isCotKhoiLuong) ? 0 : 1)].DisplayText)
        //                .ToString(MyConstant.CONST_DATE_FORMAT_SQLITE);

        //            //dbString = $"SELECT * FROM {TDKH.TBL_KhoiLuongCongViecHangNgay} WHERE \"CodeCongTacTheoGiaiDoan\" = '{code}' AND Ngay = '{date}'";
        //            //DataTable dtCongTacTheoNgay = DataProvider.InstanceTHDA.ExecuteQuery(dbString);

        //            DataRow rowq = dtCongTacTheoNgay.AsEnumerable().FirstOrDefault(x => x["Ngay"].ToString() == date);
        //            DataRow drCT = rowq ?? dtCongTacTheoNgay.NewRow();


        //            if (isCotKhoiLuong)
        //            {
        //                drCT["KhoiLuongKeHoach"] = cell.Value.ToString();
        //                drCT["ThanhTienKeHoach"] = crRowWs[cell.ColumnIndex + 1].Value.NumericValue;
        //            }

        //            if (rowq == null)
        //            {
        //                drCT["Code"] = Guid.NewGuid().ToString();
        //                drCT["CodeCongTacTheoGiaiDoan"] = code;
        //                drCT["Ngay"] = date;
        //                dtCongTacTheoNgay.Rows.Add(drCT);
        //            }
        //        }

        //        int soNgayTong = (dateKT - dateBD).Days + 1;
        //        if (soNgayTong <= 0)
        //        {
        //            wb.EndUpdate();
        //            return false;
        //        }

        //        DataRow[] drsThuCongKL = dtCongTacTheoNgay.AsEnumerable().Where(x => x["KhoiLuongKeHoach"] != DBNull.Value).ToArray();

        //        //double[] lsKL = drsThuCongKL.Select(y => double.Parse(y["KhoiLuong"].ToString())).ToArray();

        //        string sumOfDateThuCong = "";
        //        drsThuCongKL.ForEach(x => sumOfDateThuCong += $"+ {ws.Columns[dicDate[x["Ngay"].ToString()]].Heading}{rowInd + 1}");
        //        if (sumOfDateThuCong != "")
        //            sumOfDateThuCong = $"- ({sumOfDateThuCong})";
        //        //Khối lượng chưa nhập --> sẽ tự động chia đều
        //        double KhoiLuongChuaNhap = double.Parse(crRowWs[dic[TDKH.COL_DBC_KhoiLuongToanBo]].Value.ToString())
        //            - drsThuCongKL.AsEnumerable().Sum(x => (double.TryParse(x["KhoiLuongKeHoach"].ToString(), out double d)) ? d : 0);// (double)x["KhoiLuong"].ToString());

        //        int numDateAuto = soNgayTong - drsThuCongKL.Length;

        //        double KLDaChiaDeu = Math.Round((double)(KhoiLuongChuaNhap / numDateAuto), 4);

        //        double DonGia = double.Parse(crRowWs[dic[TDKH.COL_DonGia]].Value.ToString());

        //        bool isAddedLastAutoDateTT = false;
        //        //string sumOfAllTT = "";
        //        Cell cellLastTT = null;

        //        if (dicDate.ContainsKey(dateBDStr))
        //        {
        //            int LastBeforeHeading = dicDate[dateBDStr] - 1;
        //            CellRange rangeBefore = ws.Range.FromLTRB(rangeNgay.Range.LeftColumnIndex, crRowWs.Index, LastBeforeHeading, crRowWs.Index);
        //            rangeBefore.FillColor = Color.White;
        //            rangeBefore.ForEach(x => x.SetValue(""));
        //        }

        //        if (dicDate.ContainsKey(dateKTStr))
        //        {
        //            int FrtAfterHeading = dicDate[dateKTStr] + 1;
        //            CellRange rangeAfter = ws.Range.FromLTRB(FrtAfterHeading, crRowWs.Index, rangeNgay.Range.RightColumnIndex, crRowWs.Index);
        //            rangeAfter.FillColor = Color.White;
        //            rangeAfter.ForEach(x => x.SetValue(""));
        //        }


        //        for (DateTime date = dateKT; date >= dateBD; date = date.AddDays(-1))
        //        {
        //            //DataRow[] rowNgay = dtCongTacTheoNgay.Select($"[Ngay] = '{date}'");
        //            string dateStr = date.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE);
        //            string header = ws.Columns[dicDate[dateStr]].Heading;
        //            string headerTien = ws.Columns[dicDate[dateStr] + 1].Heading;

        //            crRowWs[dicDate[dateStr]].FillColor = Color.LightYellow;

        //            crRowWs[header].ClearComments();
        //            crRowWs[headerTien].ClearComments();

        //            DataRow drCT = dtCongTacTheoNgay.AsEnumerable().Where(x => x["Ngay"].ToString() == dateStr && x["KhoiLuongKeHoach"] != DBNull.Value).FirstOrDefault();

        //            if (drCT != null)
        //            {
        //                crRowWs[header].SetValueFromText(drCT["KhoiLuongKeHoach"].ToString());
        //                crRowWs[header].Font.Color = TDKH.color_DaNhapKhoiLuongNgayThuCong;
        //            }
        //            else
        //            {
        //                crRowWs[header].Formula = $"({dicData[TDKH.COL_DBC_KhoiLuongToanBo]}{rowInd + 1}{sumOfDateThuCong})/{numDateAuto}";// SetValue(KLDaChiaDeu);
        //                crRowWs[header].Font.Color = MyConstant.color_Nomal;
        //            }

        //            crRowWs[headerTien].Formula = $"ROUND({header}{crRowWs.Index + 1}*{dicData[TDKH.COL_DonGia]}{crRowWs.Index + 1}; 0)";
        //            crRowWs[headerTien].Font.Color = MyConstant.color_Nomal;

        //        }

        //        if (isSaveToDb)
        //        {
        //            DataProvider.InstanceTHDA.UpdateDataTableFromSqliteSource(dtCongTacTheoNgay, TDKH.TBL_KhoiLuongCongViecHangNgay);
        //        }

        //    }
        //    else if (ws.Name == TDKH.SheetName_KLHangNgay) //KL Hàng ngày
        //    {
        //        string dbString = $"SELECT \"NgayBatDau\", \"NgayKetThuc\", \"NgayBatDauThiCong\", \"NgayKetThucThiCong\" FROM {TDKH.TBL_ChiTietCongTacTheoKy} WHERE \"Code\" = '{code}'";
        //        DataTable dtCTTK = DataProvider.InstanceTHDA.ExecuteQuery(dbString);
        //        DataRow rowCTTK = dtCTTK.Rows[0];

        //        dbString = $"DELETE FROM {TDKH.TBL_KhoiLuongCongViecHangNgay} WHERE \"CodeCongTacTheoGiaiDoan\" = '{code}' AND " +
        //            $"(\"Ngay\" < '{dateBDStr}' OR \"Ngay\" > '{dateKTStr}') AND " +
        //            $"(\"Ngay\" < '{rowCTTK["NgayBatDau"]}' OR \"Ngay\" > '{rowCTTK["NgayKetThuc"]}')";
        //        DataProvider.InstanceTHDA.ExecuteNonQuery(dbString);

        //        dbString = $"SELECT * FROM {TDKH.TBL_KhoiLuongCongViecHangNgay} WHERE \"CodeCongTacTheoGiaiDoan\" = '{code}' AND " +
        //            $"(\"Ngay\" >= '{dateBDStr}' AND \"Ngay\" <= '{dateKTStr}' AND \"KhoiLuongThiCong\" IS NOT NULL)";

        //        DataTable dtCongTacTheoNgay = DataProvider.InstanceTHDA.ExecuteQuery(dbString);

        //        Dictionary<string, long> dicDateDonGia = fcn_GetDicNgay_DonGia(code, TypeKLHN.CongTac);
        //        long DonGiaMacDinh = long.Parse(crRowWs[TDKH.dic_KLHangNgay_All[TDKH.COL_DonGiaThiCong]].Value.ToString());
        //        foreach (DataRow dr in dtCongTacTheoNgay.Rows)
        //        {

        //            crRowWs[dicDate[dr["Ngay"].ToString()]].SetValueFromText(dr["KhoiLuongThiCong"].ToString());
        //            string dateStr = DateTime.Parse(dr["Ngay"].ToString()).ToString(MyConstant.CONST_DATE_FORMAT_SQLITE);
        //            double giatri = (double)Math.Round((double)(double.Parse(dr["KhoiLuongThiCong"].ToString()) * ((dicDateDonGia.ContainsKey(dateStr)) ? dicDateDonGia[dateStr] : DonGiaMacDinh)), 0);
        //            dr["ThanhTienThiCong"] = giatri;
        //        }

        //        crRowWs[dicData[TDKH.COL_DBC_LuyKeDaThucHien]].Formula = "";
        //        crRowWs[dicData[TDKH.COL_GiaTriThiCong]].Formula = "";
        //        for (DateTime date = dateBD; date <= dateKT; date = date.AddDays(1))
        //        {
        //            string dateStr = date.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE);
        //            string header = ws.Columns[dicDate[dateStr]].Heading;
        //            string headerDonGia = ws.Columns[dicDate[dateStr] + 1].Heading;
        //            string headerTien = ws.Columns[dicDate[dateStr] + 2].Heading;

        //            if (!dicDateDonGia.ContainsKey(dateStr))
        //                crRowWs[headerDonGia].Formula = $"{dicData[TDKH.COL_DonGiaThiCong]}{crRowWs.Index + 1}";
        //            else
        //                crRowWs[headerDonGia].SetValue(dicDateDonGia[dateStr]);

        //            crRowWs[headerTien].Formula = $"ROUND({header}{crRowWs.Index + 1}*{headerDonGia}{crRowWs.Index + 1};0)";


        //            crRowWs[dicDate[dateStr]].FillColor = MyConstant.colorNhapKLHangNgay;
        //            crRowWs[dicData[TDKH.COL_DBC_LuyKeDaThucHien]].Formula += $" + {header}{crRowWs.Index + 1}";
        //            crRowWs[dicData[TDKH.COL_GiaTriThiCong]].Formula += $" + {headerTien}{crRowWs.Index + 1}";
        //        }
        //        if (isSaveToDb)
        //            DataProvider.InstanceTHDA.UpdateDataTableFromSqliteSource(dtCongTacTheoNgay, TDKH.TBL_KhoiLuongCongViecHangNgay);
        //    }
        //    else //Vật liệu, NC, MTC
        //    {
                
        //    }
        //    wb.EndUpdate();
        //    return true;
        //}

/*        public static Dictionary<string, long> fcn_GetDicNgay_DonGia(string code, TypeKLHN type)
        {

            Dictionary<string, long> dicDateDonGia = new Dictionary<string, long>();
            string tbl, colFK;

            switch (type)
            {
                case TypeKLHN.GiaoViecCha:
                    tbl = TDKH.Tbl_DonGiaThiCongHangNgay;
                    colFK = "CodeCongViecCha";
                    break; 
                case TypeKLHN.GiaoViecCon:
                    tbl = TDKH.Tbl_DonGiaThiCongHangNgay;
                    colFK = "CodeCongViecCon";
                    break;
                case TypeKLHN.CongTac:
                    tbl = TDKH.Tbl_DonGiaThiCongHangNgay;
                    colFK = "CodeCongTacTheoGiaiDoan";
                    break;
                case TypeKLHN.VatLieu:
                    tbl = TDKH.TBL_KHVT_DonGia;
                    colFK = "CodeVatTu";
                    break;
                case TypeKLHN.HaoPhiVatTu:
                case TypeKLHN.HaoPhiGiaoViecCha:
                case TypeKLHN.HaoPhiGiaoViecCon:
                    tbl = TDKH.Tbl_HaoPhiVatTu_DonGia;
                    colFK = "CodeHaoPhiVatTu";
                    break;
                default:
                    return dicDateDonGia;
            }

            string dbString = $"SELECT * FROM {tbl} WHERE {colFK} = '{code}'";

            DataTable dtDonGiaTheoNgay = DataProvider.InstanceTHDA.ExecuteQuery(dbString);
            foreach (DataRow dr in dtDonGiaTheoNgay.Rows)
            {
                DateTime TuNgay = DateTime.Parse(dr["TuNgay"].ToString());
                DateTime DenNgay = DateTime.Parse(dr["DenNgay"].ToString());
                for (DateTime date = TuNgay; date <= DenNgay; date = date.AddDays(1))
                {
                    string dateStr = date.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE);
                    if (dicDateDonGia.Keys.Contains(dateStr))
                    {
                        MessageShower.ShowInformation($"Lỗi cài nhiều đơn giá trong 1 ngày: {dateStr}");
                        continue;
                    }
                    dicDateDonGia.Add(dateStr, long.Parse(dr["DonGia"].ToString()));
                }
            }
            return dicDateDonGia;
        }*/

        public static void CapNhatDonGiaThiCong(TypeKLHN type, string codeFk, string date, long DonGia)
        {
            string tbl, colFK;

            switch (type)
            {
                case TypeKLHN.GiaoViecCha:
                    tbl = TDKH.Tbl_DonGiaThiCongHangNgay;
                    colFK = "CodeCongViecCha";
                    break;
                case TypeKLHN.GiaoViecCon:
                    tbl = TDKH.Tbl_DonGiaThiCongHangNgay;
                    colFK = "CodeCongViecCon";
                    break;
                case TypeKLHN.CongTac:
                    tbl = TDKH.Tbl_DonGiaThiCongHangNgay;
                    colFK = "CodeCongTacTheoGiaiDoan";
                    break;
                case TypeKLHN.VatLieu:
                    tbl = TDKH.TBL_KHVT_DonGia;
                    colFK = "CodeVatTu";
                    break;
                case TypeKLHN.HaoPhiVatTu:
                    tbl = TDKH.Tbl_HaoPhiVatTu_DonGia;
                    colFK = "CodeHaoPhiVatTu";
                    break;
                default:
                    return;
            }

            string dbString = $"SELECT * FROM {tbl} WHERE {colFK} = '{codeFk}' AND TuNgay <= '{date}' AND DenNgay >= '{date}' ";
            DataTable dt = DataProvider.InstanceTHDA.ExecuteQuery(dbString);

            if (dt.Rows.Count > 0)
            {
                DataRow dr = dt.Rows[0];
                string tuNgay = (string)dr["TuNgay"];
                string denNgay = (string)dr["DenNgay"];

                if (tuNgay == denNgay)
                {
                    dr["DonGia"] = DonGia;
                    goto UPDATE;
                }
                else if (tuNgay == date)
                {
                    dr["TuNgay"] = DateTime.Parse(tuNgay).AddDays(1).ToString(MyConstant.CONST_DATE_FORMAT_SQLITE);
                }
                else if (denNgay == date)
                {
                    dr["DenNgay"] = DateTime.Parse(denNgay).AddDays(-1).ToString(MyConstant.CONST_DATE_FORMAT_SQLITE);
                }
                else
                {
                    dr["DenNgay"] = DateTime.Parse(date).AddDays(-1).ToString(MyConstant.CONST_DATE_FORMAT_SQLITE);

                    var drDouplicate = dt.NewRow();
                    dt.Rows.Add(drDouplicate);
                    drDouplicate.ItemArray = dr.ItemArray;
                    drDouplicate["Code"] = Guid.NewGuid().ToString();
                    drDouplicate["TuNgay"] = DateTime.Parse(date).AddDays(1).ToString(MyConstant.CONST_DATE_FORMAT_SQLITE);
                }
            }   
            
            var drNew = dt.NewRow();
            dt.Rows.Add(drNew);
            drNew["Code"] = Guid.NewGuid();
            drNew[colFK] = codeFk;
            drNew["TuNgay"] = drNew["DenNgay"] = date;
            drNew["DonGia"] = DonGia;

            UPDATE:
            DataProvider.InstanceTHDA.UpdateDataTableFromSqliteSource(dt, tbl);
        }
        public static string GetCodeHangMucFromCodeCongTac(string codeCongTac)
        {
            string dbString = $"SELECT {TDKH.TBL_DanhMucCongTac}.CodeHangMuc FROM {TDKH.TBL_ChiTietCongTacTheoKy} " +
                $"INNER JOIN {TDKH.TBL_DanhMucCongTac} " +
                $"ON {TDKH.TBL_ChiTietCongTacTheoKy}.CodeCongTac = {TDKH.TBL_DanhMucCongTac}.Code " +
                $"WHERE {TDKH.TBL_ChiTietCongTacTheoKy}.Code = '{codeCongTac}'";

            DataTable dt = DataProvider.InstanceTHDA.ExecuteQuery(dbString);
            return dt.Rows[0]["CodeHangMuc"].ToString();
        }
        
        public static string GetCodeHangMucFromCodeHaoPhi(string codeVatTu)
        {
            string dbString = $"SELECT CodeHangMuc FROM {TDKH.TBL_KHVT_VatTu} " +
                $"WHERE Code = '{codeVatTu}'";

            DataTable dt = DataProvider.InstanceTHDA.ExecuteQuery(dbString);
            return (dt.Rows.Count > 0) ? dt.Rows[0][0].ToString() : "";
        }
        public static List<InforCT_HM> InforHMCT(object Duan,bool HMCT,bool DuAnHT=false)
        {
            List<InforCT_HM> Infor = new List<InforCT_HM>();
            var ls = Duan as List<Tbl_ThongTinDuAnViewModel>;
            List<Tbl_ThongTinDuAnViewModel> NewDuAn = new List<Tbl_ThongTinDuAnViewModel>();
            if (ls is null)
                return Infor;
            NewDuAn = ls;
            if (DuAnHT)
                NewDuAn = ls.Where(x => x.Code == SharedControls.slke_ThongTinDuAn.EditValue.ToString()).ToList();
            foreach (Tbl_ThongTinDuAnViewModel item in NewDuAn)
            {
                string dbString = $"SELECT \"Code\", \"Ten\" FROM {MyConstant.TBL_THONGTINCONGTRINH} WHERE \"CodeDuAn\" = '{item.Code}'";
                DataTable dt = DataProvider.InstanceTHDA.ExecuteQuery(dbString);
                foreach (DataRow row in dt.Rows)
                {
                    if (HMCT)
                    {
                        dbString = $"SELECT \"Code\", \"Ten\" FROM {MyConstant.TBL_THONGTINHANGMUC} WHERE \"CodeCongTrinh\" = '{row["Code"].ToString()}'";
                        DataTable dt_HM = DataProvider.InstanceTHDA.ExecuteQuery(dbString);
                        foreach (DataRow rowHM in dt_HM.Rows)
                        {
                            Infor.Add(new InforCT_HM
                            {
                                CongTrinh = row["Ten"].ToString(),
                                HangMuc = rowHM["Ten"].ToString(),
                                ID = rowHM["Code"].ToString(),
                                DuAn = item.TenDuAn
                            });
                        }
                    }
                    else
                    {
                        Infor.Add(new InforCT_HM
                        {
                            CongTrinh = row["Ten"].ToString(),
                            //HangMuc = rowHM["Ten"].ToString(),
                            ID = row["Code"].ToString(),
                            DuAn = item.TenDuAn
                        });
                    }
                }
            }
            return Infor;
        }
        /// <summary>
        /// Thêm hao phí định mức mặc định
        /// </summary>
        /// <param name="codeCongTac"></param>
        public static DataTable fcn_TDKH_ThemDinhMucMacDinhChoCongTac(TypeKLHN type, string code, bool reCalcVatTu)
        {
            string colFk, dbString;

            switch (type)
            {
                case TypeKLHN.GiaoViecCha:
                    colFk = "CodeCongViecCha";
                    dbString = $"SELECT MaDinhMuc AS MaHieuCongTac, NgayBatDau, NgayKetThuc " +
                        $"FROM {GiaoViec.TBL_CONGVIECCHA} WHERE CodeCongViecCha = '{code}'";
                    break;
                case TypeKLHN.CongTac:
                    colFk = "CodeCongTac";
                    dbString = $"SELECT COALESCE({TDKH.TBL_DanhMucCongTac}.MaHieuCongTac,{TDKH.TBL_ChiTietCongTacTheoKy}.MaHieuCongTac) AS MaHieuCongTac, " +
                        $"NgayBatDau, NgayKetThuc " +
                        $"FROM {TDKH.TBL_ChiTietCongTacTheoKy} " +
                        $"LEFT JOIN {TDKH.TBL_DanhMucCongTac} " +
                        $"ON {TDKH.TBL_ChiTietCongTacTheoKy}.CodeCongTac = {TDKH.TBL_DanhMucCongTac}.Code " +
                        $"WHERE {TDKH.TBL_ChiTietCongTacTheoKy}.Code = '{code}'";
                    break;
                default:
                    return DataProvider.InstanceTHDA.ExecuteQuery($"SELECT * FROM {TDKH.Tbl_HaoPhiVatTu} LIMIT 0");


            }

            DataTable dtCT = DataProvider.InstanceTHDA.ExecuteQuery(dbString);
            if (dtCT.Rows.Count == 0) 
            {
                MessageShower.ShowError("Không có hao phí!");
                DataProvider.InstanceTHDA.ExecuteQuery($"SELECT * FROM {TDKH.Tbl_HaoPhiVatTu} LIMIT 0");
            }

            string MHCT = dtCT.Rows[0]["MaHieuCongTac"].ToString();
            string NgayBD = dtCT.Rows[0]["NgayBatDau"].ToString();
            string NgayKT = dtCT.Rows[0]["NgayKetThuc"].ToString();
            

            DataTable dt = DinhMucHelper.fcn_GetTblHaoPhiVatTuMacDinh(MHCT);
            DataTable dtSource = DataProvider.InstanceTHDA.ExecuteQuery($"SELECT * FROM {TDKH.Tbl_HaoPhiVatTu} WHERE \"CodeCongTac\" = '{code}'");

            foreach (DataRow row in dtSource.Rows)
            {
                row.Delete();
            }
            //dtSource.AcceptChanges();
            string[] lsProps = { "MaVatLieu", "VatTu", "DinhMuc", "DonVi", "HeSo", "LoaiVatTu", "MaTXHienTruong", "DonGia" };
            foreach (DataRow dr in dt.Rows)
            {
                DataRow newRow = dtSource.NewRow();
                newRow["Code"] = Guid.NewGuid().ToString();
                newRow[colFk] = code;
                foreach (string str in lsProps)
                {
                    newRow[str] = dr[str];
                }
                newRow["HeSoNguoiDung"] = dr["HeSo"];
                newRow["DinhMucNguoiDung"] = dr["DinhMuc"];
                newRow["DonGiaThiCong"] = dr["DonGia"];
                newRow["NgayBatDau"] = NgayBD;
                newRow["NgayKetThuc"] = NgayKT;

                if (dr["VatTu"].ToString() == TDKH.VatTu_VatLieuKhac)
                {
                    newRow["PhanTichKeHoach"] = false;
                }

                dtSource.Rows.Add(newRow);
            }

            DataProvider.InstanceTHDA.UpdateDataTableFromSqliteSource(dtSource, TDKH.Tbl_HaoPhiVatTu/*, true, $"WHERE \"CodeCongTac\" = '{crCodeCongTac}'"*/);
            
            if (type == TypeKLHN.CongTac && reCalcVatTu)
                TDKHHelper.TinhLaiToanBoKhoiLuongKeHoach(new string[] { code });

            return dtSource;
        }

        public static List<T> TryGetObjFromJson<T>(string jsonString) where T: class
        {
            try
            {
                return JsonConvert.DeserializeObject<List<T>>(jsonString) ?? new List<T>();
            }
            catch (Exception ex)
            {
                return new List<T>();
            }
        }
        public static string ConvertSuperscript(string value)
        {
            string stringFormKd = value.Normalize(NormalizationForm.FormKD);
            StringBuilder stringBuilder = new StringBuilder();

            //foreach (char character in stringFormKd)
            //{
            //    UnicodeCategory unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(character);
            //    if (unicodeCategory != UnicodeCategory.NonSpacingMark)
            //    {
            //        stringBuilder.Append(character);
            //    }
            //}
            value.Normalize(System.Text.NormalizationForm.FormKD).Where(c => CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark).ForEach(x => stringBuilder.Append(x));
            return stringBuilder.ToString().Normalize(NormalizationForm.FormKC);
        }
        public static double Fcn_CalCulateAll(string lsCodeNhienLieu)
        {
            //WaitFormHelper.ShowWaitForm("Đang Tính toán lại nhiên liệu hằng ngày!!!!!!!!!Vui lòng chờ!!!!!!!!!");
            //            string dbString = $"SELECT TH.* " +
            //$"FROM {MyConstant.TBL_MTC_NHIENLIEU} TH ";
            //            List<MTC_TongHopNhienLieuHangNgay> Tong = DataProvider.InstanceTHDA.ExecuteQueryModel<MTC_TongHopNhienLieuHangNgay>(dbString);
            //string lstCode = string.Join(",", lsCodeNhienLieu);
            string Dbstring = $"SELECT ROW_NUMBER() OVER(ORDER BY HN.Code) AS STT,HN.* " +
$"FROM {MyConstant.TBL_MTC_NHIENLIEUHANGNGAY} HN WHERE HN.CodeNhienLieu='{lsCodeNhienLieu}' ORDER BY HN.Ngay ASC";
            List<MTC_NhienLieuHangNgay> HN = DataProvider.InstanceTHDA.ExecuteQueryModel<MTC_NhienLieuHangNgay>(Dbstring);
            double KLHN = (double)HN.Sum(x => x.KhoiLuong);
            //            foreach (var item in Tong)
            //            {
            //                List<MTC_NhienLieuHangNgay> NL = HN.Where(x => x.Code == item.Code).ToList();
            //                if (!NL.Any())
            //                    continue;
            //                item.KhoiLuongDaNhap = NL.Sum(x => x.KhoiLuong);
            //            }
            //            int res = DataProvider.InstanceTHDA.AddOrUpdate<MTC_TongHopNhienLieuHangNgay>("UPDATE_MTC_TongHopNhienLieu",
            //Tong, false, true, true);
            //            WaitFormHelper.CloseWaitForm();
            //Fcn_UpdateDaTaTongHopNhienLieuPhu();
            //Fcn_LoadThongBao();
            return KLHN;
        }
        public static void SaveJpeg(string path, Image img, int quality)
        {
            if (quality < 0 || quality > 100)
                throw new ArgumentOutOfRangeException("quality must be between 0 and 100.");

            // Encoder parameter for image quality 
            EncoderParameter qualityParam = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, quality);
            // JPEG image codec 
            ImageCodecInfo jpegCodec = GetEncoderInfo("image/jpeg");
            EncoderParameters encoderParams = new EncoderParameters(1);
            encoderParams.Param[0] = qualityParam;
            img.Save(path, jpegCodec, encoderParams);
        }
        private static ImageCodecInfo GetEncoderInfo(string mimeType)
        {
            // Get image codecs for all image formats 
            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageEncoders();

            // Find the correct image codec 
            for (int i = 0; i < codecs.Length; i++)
                if (codecs[i].MimeType == mimeType)
                    return codecs[i];

            return null;
        }
        public static double Fcn_UpdateDaTaTongHopNhienLieuPhu(string lsCodeNhienLieu)
        {
            //WaitFormHelper.ShowWaitForm("Đang Tính toán lại nhiên liệu đã nhập!!!!!!!!!Vui lòng chờ!!!!!!!!!");
            string dbString = $"SELECT TH.* " +
 $"FROM {MyConstant.TBL_MTC_NHIENLIEU} TH WHERE TH.Code='{lsCodeNhienLieu}'";
            List<MTC_TongHopNhienLieuHangNgay> Tong = DataProvider.InstanceTHDA.ExecuteQueryModel<MTC_TongHopNhienLieuHangNgay>(dbString);

            dbString = $"SELECT nt.* FROM {MyConstant.TBL_MTC_CHITIETNHATTRINH} nt ";

            List<MTC_ChiTietHangNgay> ChiTietMay = DataProvider.InstanceTHDA.ExecuteQueryModel<MTC_ChiTietHangNgay>(dbString);
            List<MTC_NhienLieuInMay> NLP = new List<MTC_NhienLieuInMay>();
            foreach (var item in ChiTietMay)
            {
                if (!string.IsNullOrEmpty(item.NhienLieuPhu) && item.NhienLieuPhu != "0")
                {
                    var ChiTietNLPhu = JsonConvert.DeserializeObject<List<MTC_NhienLieuInMay>>(item.NhienLieuPhu);
                    if (ChiTietNLPhu != null)
                        NLP.AddRange(ChiTietNLPhu);
                }
            }
            dbString = $"SELECT NL.CodeNhienLieu,may.Code as CodeMayToanDuAn,mayDA.Code as CodeMay,may.Ten as TenMayThiCong,may.CaMayKm, " +
$"nt.* FROM {MyConstant.TBL_MTC_CHITIETNHATTRINH} nt " +
$" LEFT JOIN {MyConstant.TBL_MTC_DUANINMAY} mayDA ON mayDA.Code=nt.CodeMayDuAn " +
$"LEFT JOIN {MyConstant.TBL_MTC_DANHSACHMAY} may ON may.Code=mayDA.CodeMay "
+ $"LEFT JOIN {MyConstant.TBL_MTC_NHIENLIEUINMAY} NL ON may.Code=NL.CodeMay " +
$"WHERE NL.LoaiNhienLieu=1 AND NL.CodeNhienLieu='{lsCodeNhienLieu}'";
            List<MTC_ChiTietHangNgay> ChiTietMayChinh = DataProvider.InstanceTHDA.ExecuteQueryModel<MTC_ChiTietHangNgay>(dbString);
            double KLDD = 0;
            foreach (var item in Tong)
            {
                List<MTC_NhienLieuInMay> NLPTH = NLP.Where(x => x.CodeNhienLieu == item.Code).ToList();
                KLDD += (double)NLPTH.Sum(x => x.MucTieuThu);
                //List<MTC_ChiTietHangNgay> NL = ChiTietMayChinh.Where(x => x.CodeNhienLieu == item.Code).ToList();
                if (!ChiTietMayChinh.Any())
                    continue;
                KLDD += (double)ChiTietMayChinh.Sum(x => x.NhienLieuChinh);
            }
            //            int res = DataProvider.InstanceTHDA.AddOrUpdate<MTC_TongHopNhienLieuHangNgay>("UPDATE_MTC_TongHopNhienLieu",
            //Tong, false, true, true);
            //            WaitFormHelper.CloseWaitForm();
            return KLDD;
        }
    }
}
