//using DevExpress.ClipboardSource.SpreadsheetML;
using AutoMapper.Internal;
using Dapper;
using DevExpress.CodeParser;
using DevExpress.DataAccess.Excel;
using DevExpress.Internal.WinApi;
using DevExpress.Pdf.Native;
using DevExpress.Spreadsheet;
using DevExpress.Utils.DirectXPaint;
using DevExpress.Utils.Filtering;
using DevExpress.XtraRichEdit.Export.Doc;
using DevExpress.XtraRichEdit.Fields;
using DevExpress.XtraRichEdit.Import.OpenDocument;
using DevExpress.XtraScheduler.Drawing;
using MoreLinq;
//using DevExpress.XtraSpreadsheet.Model;
using PhanMemQuanLyThiCong.Common.Constant;
using PhanMemQuanLyThiCong.Common.Enums;
using VChatCore.ViewModels.SyncSqlite;
using PhanMemQuanLyThiCong.Constant;
using PhanMemQuanLyThiCong.Constant.Enum;
using PhanMemQuanLyThiCong.Controls;
using PhanMemQuanLyThiCong.Model;
using PM360.Common.Helper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using MSETTING = PhanMemQuanLyThiCong.Properties.Settings;
using PhanMemQuanLyThiCong.Common.ViewModel.KLHN;
using PhanMemQuanLyThiCong.Model.TDKH;
using DevExpress.DevAV.Chat.Model;
using DevExpress.XtraPrinting;
using StackExchange.Profiling.Internal;
//using DevExpress.Spreadsheet;

namespace PhanMemQuanLyThiCong.Common.Helper
{
    public static class DinhMucHelper
    {
        private static string keyCompare = "";
        public static Dictionary<string, string> dicReplace = new Dictionary<string, string>()
        {
            {" đường kính ống ", " đk "},
            {" đường kính ", " đk "},
            {" sản xuất ", " sx " },
            {" yêu cầu ", " y/c " },
            {" xi măng ", " xm " },
            {" tiết diện ", " td " },
            {" và ", " & " },
            {" liên kết ", " lk " },
            {" cấu kiện bê tông ", " CKBT " },
            {" khối lượng ", " kl " },
            {" pa nen ", " panen " },
            {".", "," },
        };
        public static string ReplaceFromDic(this string source)
        {
            //source = Regex.Replace(source, @"(\s+|^)((duong kinh ong)|(duong kinh))(\s+|$)", " fi ");
            source = Regex.Replace(source, @"(\s+|^)san xuat(\s+|$)", " sx ");
            source = Regex.Replace(source, @"(\s+|^)yeu cau|y\/c(\s+|$)", " yc ");
            source = Regex.Replace(source, @"(\s+|^)xi mang(\s+|$)", " xm ");
            source = Regex.Replace(source, @"(\s+|^)tiet dien(\s+|$)", " td ");
            source = Regex.Replace(source, @"(\s+|^)va(\s+|$)", " & ");
            source = Regex.Replace(source, @"(\s+|^)lien ket(\s+|$)", " lk ");
            source = Regex.Replace(source, @"(\s+|^)cau kien be tong\s+", " CKBT CKBT ");
            source = Regex.Replace(source, @"(\s+|^)khoi luong(\s+|$)", " kl ");
            source = Regex.Replace(source, @"(\s+|^)pa nen(\s+|$)", " panen ");
            source = Regex.Replace(source, @"\.", ",");

            source = Regex.Replace(source, @"≤\s*|<=\s*|nho hon hoac bang\s+", "<=");
            source = Regex.Replace(source, @"nho hon\s+", "<");
            source = Regex.Replace(source, @"≥\s*|>=\s*|lon hon hoac bang\s+", ">=");
            source = Regex.Replace(source, @"lon hon\s+", ">");
            //source = Regex.Replace(source, @"", " ");

            //Bo dau =
            source = Regex.Replace(source, $"([^<>])(=)", delegate (Match m) {
                return m.Groups[1].Value + " ";
            });
            source = Regex.Replace(source, @"\s*((?!\d)s*,(?!\d)|-|\(|\)|;)", " ");
            source = Regex.Replace(source, @"\s+", " ");

            foreach (var item in MyConstant.dicCompare)
            {
                keyCompare = item.Key;
                //Replace a/b and a.b
                source = Regex.Replace(source, $@"({item.Value})\s*(<=|<|>=|>)*(\d+(,\d+)*)((\s*\/|\-\s*)(\d+(,\d+)*))*([a-z]*)", new MatchEvaluator(Replacefi));

                //source = Regex.Replace(source, $@"({item.Value})(<=|<|>=|>|=)*\s*(\d+,*\d+)([a-z]*)", new MatchEvaluator(ReplacefiNotSideBySideValue));
                source = Regex.Replace(source, $@"({item.Value})\s+(<=|<|>=|>)*([a-z{MyConstant.stringRegexTVCD}]*)\s+(\d+,*\d*)([a-z]*)", new MatchEvaluator(ReplacefiNotSideBySideValue));
            }

            source = Regex.Replace(source, @"(\d+)([a-z]+)", delegate (Match m) {
                return m.Groups[1].Value;
            });

            source = Regex.Replace(source, @"(mac|mac|m)(\s+\d+)", new MatchEvaluator(ReplaceMac));
            source = Regex.Replace(source, @"(\s+|^)(con|cut|co|chech|y|loi|t)(\s+|$)", " con ");
            source = Regex.Replace(source, @"(\s+|^)(can nen|can san|lang nen)(\s+|$)", " cannen ");
            source = Regex.Replace(source, @"(\s+|^)(phuong tien van chuyen)(\s+|$)", " phuongtienvanchuyen ");
            source = Regex.Replace(source, @"(\s+|^)(dap chi|dap phao)(\s+|$)", " dapphao dapphao dapphao dapphao dapphao dapphao dapphao dapphao ");
            source = Regex.Replace(source, @"(\s+|^)(dao dat|dao khuon dat|dao mong)(\s+|$)", " daomong daomong daomong daomong daomong daomong daomong daomong ");
            source = Regex.Replace(source, @"(\s+|^)(dao da)(\s+|$)", " daoda daoda daoda daoda daoda daoda daoda daoda ");
            source = Regex.Replace(source, @"(\s+|^)(dap nen|dap nen)(\s+|$)", " dapnen dapnen dapnen dapnen dapnen dapnen dapnen dapnen ");
            source = Regex.Replace(source, @"(\s+|^)(dap cat)(\s+|$)", " dapcat dapcat dapcat dapcat dapcat dapcat dapcat dapcat ");
            source = Regex.Replace(source, @"(\s+|^)(dao nen)(\s+|$)", " daonen daonen daonen daonen daonen daonen daonen daonen ");
            source = Regex.Replace(source, @"(\s+|^)(dao)(\s+|$)", " dao dao dao dao dao dao dao dao ");
            source = Regex.Replace(source, @"(\s+|^)(van chuyen)(\s+|$)", " vanchuyen vanchuyen vanchuyen vanchuyen vanchuyen vanchuyen vanchuyen vanchuyen ");
            source = Regex.Replace(source, @"(\s+|^)(lap dat|dap dat|lap dat|dap dat|dap nen)(\s+|$)", " dapdat dapdat dapdat dapdat dapdat dapdat dapdat dapdat ");
            source = Regex.Replace(source, @"(\s+|^)(bo via|bo via)(\s+|$)", " bovia bovia bovia ");
            source = Regex.Replace(source, @"(\s+|^)(bo le)(\s+|$)", " bole bole bole ");
            source = Regex.Replace(source, @"(\s+|^)(trong cay canh)(\s+|$)", " trongcaycanh trongcaycanh trongcaycanh trongcaycanh trongcaycanh trongcaycanh trongcaycanh trongcaycanh ");
            source = Regex.Replace(source, @"(\s+|^)(cay canh)(\s+|$)", " caycanh caycanh caycanh caycanh caycanh caycanh caycanh caycanh ");
            source = Regex.Replace(source, @"(\s+|^)(trong hoa)(\s+|$)", " tronghoa tronghoa tronghoa tronghoa tronghoa tronghoa tronghoa tronghoa ");
            source = Regex.Replace(source, @"(\s+|^)(trong cay)(\s+|$)", " trongcay trongcay trongcay trongcay trongcay trongcay trongcay trongcay ");
            source = Regex.Replace(source, @"(\s+|^)(dap)(\s+|$)", " dapdat ");
            source = Regex.Replace(source, @"(\s+|^)(be tong|betong|betong|b.tong|b.tong)(\s+|$)", " betong betong betong betong betong betong betong betong ");
            source = Regex.Replace(source, @"(\s+|^)(dat set|dat set|dat set|dat set|dat xet|dat xet)(\s+|$)", " datset datset datset datset datset datset datset datset");
            source = Regex.Replace(source, @"(\s+|^)(lap dung cua)(\s+|$)", " lapdungcua lapdungcua lapdungcua ");
            source = Regex.Replace(source, @"(\s+|^)(lap dung)(\s+|$)", " lapdung lapdung lapdung ");
            source = Regex.Replace(source, @"(\s+|^)(khung xuong|xa go)(\s+|$)", " xago ");
            source = Regex.Replace(source, @"(\s+|^)(thach cao)(\s+|$)", " thachcao thachcao thachcao thachcao thachcao thachcao thachcao thachcao");
            source = Regex.Replace(source, @"(\s+|^)(ban le)(\s+|$)", " banle ");
            source = Regex.Replace(source, @"(\s+|^)(bu long|bulong)(\s+|$)", " bulong ");
            source = Regex.Replace(source, @"(\s*|^)(cp|cap phoi)(\s*|$)", " capphoi ");
            source = Regex.Replace(source, @"(\s*|^)(co ga)(\s*|$)", " ong cong, ong buy ");


            return source;
        }

        public static string Replacefi(Match m)
        {
            if (!double.TryParse(m.Groups[3].Value, out double db))
            {
                return m.Value.ToString();
            }

            if (!m.Groups[7].Success)
            {
                double fst_val = double.Parse(m.Groups[3].Value);
                if (fst_val <= 1)
                    fst_val = fst_val * 100;
                return keyCompare + m.Groups[2].Value + fst_val + "";
            }
            else
            {
                double fst_val = double.Parse(m.Groups[3].Value);
                double snd_val = double.Parse(m.Groups[7].Value);
                double val = (fst_val > snd_val) ? fst_val : snd_val;

                if (keyCompare == "k" && val <= 1)
                    val = val * 100;
                return keyCompare + m.Groups[2].Value + val.ToString() + " ";
            }
        }

        public static string ReplacefiNotSideBySideValue(Match m)
        {
            double snd_val = double.Parse(m.Groups[4].Value);
            if (keyCompare == "k" && snd_val < 1)
            {
                snd_val = snd_val * 100;
            }

            return m.Groups[2].Value + keyCompare + m.Groups[3].Value + snd_val + " ";
        }

        public static string ReplacefiSideBySideValue(Match m)
        {
            double snd_val = double.Parse(m.Groups[4].Value);
            if (m.Groups[1].Value.StartsWith("k") && snd_val < 1)
            {
                snd_val = snd_val * 100;
            }

            return m.Groups[2].Value + keyCompare + m.Groups[3].Value + snd_val + " ";
        }

        public static string ReplaceMac(Match m)
        {

            return "m" + m.Groups[2].Value.Trim();
        }

        public static List<CTDinhMuc> DinhMucMacDinh()
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

        public static bool isDinhMucNguoiDung(this string MaDM)
        {
            if (MaDM.StartsWith("TT") || MaDM.Contains(".VD"))
                return true;
            return false;
        }

        public static List<HaoPhiVatTuExtensionViewModel> GetModelHaoPhiVatTuHienTai(TypeKLHN type, IEnumerable<string> codeCongTac, string LoaiVT = null, bool GetAll = false)
        {
            DataTable dt = fcn_GetTblHaoPhiVatTuHienTai(type, codeCongTac, LoaiVT, GetAll);
            return dt.fcn_DataTable2List<HaoPhiVatTuExtensionViewModel>();
        }
        public static DataTable fcn_GetTblHaoPhiVatTuHienTai(TypeKLHN type, IEnumerable<string> codeCongTac, string LoaiVT = null, bool GetAll = false)
        {
            string colFk;
            string dbString = "";
            switch (type)
            {
                case TypeKLHN.GiaoViecCha:
                    dbString = $"SELECT {MyConstant.view_HaoPhiVatTu}.*, NULL AS CodeCongViecCon " +
                                    $"FROM {MyConstant.view_HaoPhiVatTu} " +
                                    $"WHERE CodeCongViecCha IN ({MyFunction.fcn_Array2listQueryCondition(codeCongTac)})";
                    break;

                case TypeKLHN.GiaoViecCon:
                    dbString = $"SELECT {MyConstant.view_HaoPhiVatTuGiaoViecCon}.* " +
                                    $"FROM {MyConstant.view_HaoPhiVatTuGiaoViecCon} " +
                                    $"WHERE CodeCongViecCon IN ({MyFunction.fcn_Array2listQueryCondition(codeCongTac)})";
                    break;
                case TypeKLHN.CongTac:
                    dbString = $"SELECT *,  NULL AS CodeCongViecCon " +
                        $"FROM {MyConstant.view_HaoPhiVatTu} WHERE CodeCongTac IN ({MyFunction.fcn_Array2listQueryCondition(codeCongTac)})";
                    break;
                default:
                    return null;
            }

            if (LoaiVT != null)
                dbString += $" AND LoaiVatTu = '{LoaiVT}' ";

            DataTable dtVatTu;

            if (!GetAll)
                dbString += " AND (PhanTichKeHoach = 1 AND IsCha = 0)";
            DataTable dt = DataProvider.InstanceTHDA.ExecuteQuery(dbString);

            dt.Columns.Add("TenMacDinh", typeof(string));

            foreach (DataRow dr in dt.Rows)
            {
                dbString = $"SELECT \"VatTu\" FROM Tbl_VatTu WHERE \"MaVatLieu\" = @MaVatLieu";
                DataTable dt1 = DataProvider.InstanceTBT.ExecuteQuery(dbString, parameter: new object[] { dr["MaVatLieu"] });

                if (dt1.Rows.Count > 0)
                    dr["TenMacDinh"] = (dt.Rows.Count > 0) ? dt1.Rows[0]["VatTu"].ToString() : "Không tên";
            }


            if (type == TypeKLHN.GiaoViecCha)
            {
                string[] lsSeparateGV =
                {
                    "NgayBatDau",
                    "NgayKetThuc",
                    "KhoiLuongKeHoach",
                    "KhoiLuongDinhMucChuan",
                    "KhoiLuongHopDong"
                };

                foreach (string str in lsSeparateGV)
                {
                    dt.Columns.Remove(str);
                    dt.Columns[$"{str}GiaoViec"].ColumnName = str;
                }
            }

            //dt.Locale = MyConstant.culture;
            //dt.Columns.Add(new DataColumn("KhoiLuongDinhMucChuan", typeof(double)));
            //dt.Columns["KhoiLuongDinhMucChuan"].Expression = $"{klKeHoach.ToString(CultureInfo.GetCultureInfo("en-gb"))}*[HeSo]*[DinhMuc]";

            //dt.Columns.Add(new DataColumn("KhoiLuongNguoiDung", typeof(double)));
            //dt.Columns["KhoiLuongNguoiDung"].Expression = $"{klKeHoach.ToString(CultureInfo.GetCultureInfo("en-gb"))}*[HeSoNguoiDung]*[DinhMucNguoiDung]";

            return dt;
        }

        public static DataTable fcn_GetTblHaoPhiVatTuMacDinh(string MaDinhMuc)
        {
            List<int> lsDauMuc = new List<int>();

            string DonGia = (BaseFrom.ProvincesHaveDonGia.Contains(MSETTING.Default.Province))
                                ? $"hp.{MSETTING.Default.Province} AS DonGia "
                                : "0 AS DonGia";

            string dbString = $"SELECT {DonGia}, hp.* FROM {MyConstant.view_HaoPhiVatTu} hp " +
                $"WHERE hp.MaDinhMuc = @MaDinhMuc AND (MaTxHienTruong IS NULL OR MaTxHienTruong LIKE '%HT')";

            DataTable dtSource = DataProvider.InstanceTBT.ExecuteQuery(dbString, parameter: new object[] { MaDinhMuc });
            return dtSource;
        }

        public static double GetHPNhanCong(string codeCongTac)
        {
            string dbString = $"SELECT DinhMucNguoiDung, HeSoNguoiDung FROM {TDKH.Tbl_HaoPhiVatTu} WHERE \"CodeCongTac\" = '{codeCongTac}' AND LoaiVatTu = 'Nhân công' AND PhanTichKeHoach = 1";
            DataTable dt = DataProvider.InstanceTHDA.ExecuteQuery(dbString);

            if (dt.Rows.Count == 0)
                return 0;

            return (double)dt.Rows[0]["DinhMucNguoiDung"] * (double)dt.Rows[0]["HeSoNguoiDung"];
        }

        /// <summary>
        /// 
        /// Không cập nhật cho đo bóc chuẩn
        /// </summary>
        /// <param name="crRowCha"></param>
        /// <param name="crRow"></param>
        /// <param name="dicDate"></param>
        /// <param name="isCapNhatChiTiet"></param>
        private static int ind = 1;



        //public static void fcn_TDKH_CapNhatCongThucFull(Row firstRow, int? type = null, int[] indsNeedSum = null, bool isCalculate = true, Dictionary<string, string> dic = null, bool isForceSetFor = false)
        //{
        //    Worksheet ws = firstRow.Worksheet;
        //    var usedRange = ws.GetUsedRange();
        //    ws.Workbook.BeginUpdate();

        //    for (int i = firstRow.Index; i < usedRange.BottomRowIndex; i++)
        //    {
        //        fcn_TDKH_CapNhatRowChaConTienDoKeHoach(ws.Rows[i], type, indsNeedSum, isCalculate, dic, isForceSetFor);
        //    }
        //    ws.Workbook.EndUpdate();

        //}
        public static void fcn_TDKH_CapNhatRowChaConTienDoKeHoach(Row crRowCha, int? type = null, int[] indsNeedSum = null, bool isCalculate = true, Dictionary<string, string> dic = null, bool isForceSetFor = false)
        {
            Worksheet ws = crRowCha.Worksheet;
            ////          ws.Workbook.History.IsEnabled = false;
            ws.Workbook.BeginUpdate();
            if (!type.HasValue)
            {
                type = Array.IndexOf(TDKH.sheetsName, ws.Name);
            }

            Debug.WriteLine($"********{ind++}: {crRowCha.Index}*******");



            if (dic is null)
                dic = MyFunction.fcn_getDicOfColumn(ws.GetUsedRange());

            WaitFormHelper.ShowWaitForm($"Đang cập nhật công thức: {ws.Name}: {crRowCha.Index}");
            if (isCalculate)
                ws.Calculate();
            var indCons = ws.Columns[dic[TDKH.COL_RowCha]].Search((crRowCha.Index + 1).ToString(), MyConstant.MySearchOptions).Select(x => x.RowIndex + 1).ToArray();
            string typeRowCha = crRowCha[dic[TDKH.COL_TypeRow]].Value.ToString();

            bool isSumKhoiLuong = (typeRowCha == MyConstant.TYPEROW_CVCha || typeRowCha == MyConstant.TYPEROW_NhomDienGiai);
            bool isSumDonGiaThanhTien = true;
            if (indCons.Any())
            {
                int minIndCon = crRowCha.Index + 1;
                int maxIndCon = ws.GetUsedRange().BottomRowIndex;

                if (typeRowCha == MyConstant.TYPEROW_Nhom && crRowCha[dic[TDKH.COL_DBC_KhoiLuongToanBo]].Value.ToString() != "")
                {
                    isSumDonGiaThanhTien = false;
                    goto TinhKhoiLuong;
                }

                if (dic.ContainsKey(TDKH.COL_NgayBatDau))
                {
                    string lsDateBD = $"{dic[TDKH.COL_NgayBatDau]}{minIndCon}:{dic[TDKH.COL_NgayBatDau]}{maxIndCon}";
                    string lsDateKT = $"{dic[TDKH.COL_NgayKetThuc]}{minIndCon}:{dic[TDKH.COL_NgayKetThuc]}{maxIndCon}";

                    crRowCha[dic[TDKH.COL_NgayBatDau]].Formula = $"IF(MIN({lsDateBD})>0; MIN({lsDateBD}); \"\")";
                    crRowCha[dic[TDKH.COL_NgayKetThuc]].Formula = $"IF(MAX({lsDateKT})>0; MAX({lsDateKT}); \"\")";
                }


                if (dic.ContainsKey(TDKH.COL_NgayBatDauThiCong) && typeRowCha != MyConstant.TYPEROW_CVCha)
                {
                    string lsDateBDTC = $"{dic[TDKH.COL_NgayBatDauThiCong]}{minIndCon}:{dic[TDKH.COL_NgayBatDauThiCong]}{maxIndCon}";
                    string lsDateKTTC = $"{dic[TDKH.COL_NgayKetThucThiCong]}{minIndCon}:{dic[TDKH.COL_NgayKetThucThiCong]}{maxIndCon}";


                    crRowCha[dic[TDKH.COL_NgayBatDauThiCong]].Formula = $"IF(MIN({lsDateBDTC})>0; MIN({lsDateBDTC}); \"\")";
                    crRowCha[dic[TDKH.COL_NgayKetThucThiCong]].Formula = $"IF(MAX({lsDateKTTC})>0; MAX({lsDateKTTC}); \"\")";
                }

                //if (dic.ContainsKey(TDKH.COL_TuNgay) && typeRowCha != MyConstant.TYPEROW_CVCha)
                //{
                //    string lsDateBDTC = $"{dic[TDKH.COL_TuNgay]}{minIndCon}:{dic[TDKH.COL_TuNgay]}{maxIndCon}";
                //    string lsDateKTTC = $"{dic[TDKH.COL_DenNgay]}{minIndCon}:{dic[TDKH.COL_DenNgay]}{maxIndCon}";


                //    crRowCha[dic[TDKH.COL_TuNgay]].Formula = $"IF(MIN({lsDateBDTC})>0; MIN({lsDateBDTC}); \"\")";
                //    crRowCha[dic[TDKH.COL_DenNgay]].Formula = $"IF(MAX({lsDateKTTC})>0; MAX({lsDateKTTC}); \"\")";
                //}

                TinhKhoiLuong:

                if (dic.ContainsKey(TDKH.COL_KhoiLuongDaThiCongTheoCongTac) && typeRowCha == MyConstant.TYPEROW_CVCha)
                {
                    string str = TDKH.COL_KhoiLuongDaThiCongTheoCongTac;
                    string headerColRowCha = dic[TDKH.COL_RowCha];
                    string crHeader = dic[str];
                    crRowCha[dic[TDKH.COL_KhoiLuongDaThiCongTheoCongTac]].Formula =
                        $"SUMIF({headerColRowCha}{minIndCon}:{headerColRowCha}{maxIndCon};ROW();{crHeader}{minIndCon}:{crHeader}{maxIndCon})";

                }
                //if (isSumKhoiLuong)
                //{

                //    if (typeRowCha == MyConstant.TYPEROW_Nhom)
                //{
                //    dic.Remove(TDKH.COL_GiaTri);
                //    dic.Remove(TDKH.COL_GiaTriThiCong);
                //    dic.Remove(TDKH.COL_KinhPhiDuKien);
                //    dic.Remove(TDKH.COL_KinhPhiTheoTienDoThiCong);
                //}        
                string[] NhomNotSum =
                {
                    TDKH.COL_DonGia,
                    TDKH.COL_DonGiaThiCong,
                    TDKH.COL_GiaTri,
                    TDKH.COL_GiaTriNhanThau,
                    TDKH.COL_GiaTriThiCong,
                    TDKH.COL_KinhPhiDuKien,
                    TDKH.COL_GiaTriNhanThau,
                    TDKH.COL_KinhPhiTheoTienDoThiCong,

                };

                foreach (string str in TDKH.arrColSum)
                {
                    if (str.Contains("KhoiLuong") && !isSumKhoiLuong)
                        continue;

                    if (typeRowCha == MyConstant.TYPEROW_Nhom)
                    {
                        if (!isSumDonGiaThanhTien && NhomNotSum.Contains(str))
                            continue;
                    }

                    if (typeRowCha == MyConstant.TYPEROW_CVCha)
                        continue;

                    if (dic.ContainsKey(str))
                    {
                        //string lsCell = string.Join(";", indCons.Select(x => $"{dic[str]}{x}"));
                        string headerColRowCha = dic[TDKH.COL_RowCha];
                        string crHeader = dic[str];
                        crRowCha[dic[str]].Formula = $"SUMIF({headerColRowCha}{minIndCon}:{headerColRowCha}{maxIndCon};ROW();{crHeader}{minIndCon}:{crHeader}{maxIndCon})";
                    }
                }
                //}

                if (indsNeedSum != null)
                {
                    foreach (int ind in indsNeedSum)
                    {
                        string str = ws.Range.GetColumnNameByIndex(ind);
                        //string lsCell = string.Join(";", indCons.Select(x => $"{str}{x}"));
                        //crRowCha[str].Formula = $"SUM({lsCell})";

                        string headerColRowCha = dic[TDKH.COL_RowCha];
                        string crHeader = ws.Range.GetColumnNameByIndex(ind);
                        crRowCha[crHeader].Formula = $"SUMIF({headerColRowCha}{minIndCon}:{headerColRowCha}{maxIndCon};ROW();{crHeader}{minIndCon}:{crHeader}{maxIndCon})";

                    }
                }

                foreach (int ind in indCons)
                {
                    fcn_TDKH_CapNhatRowChaConTienDoKeHoach(ws.Rows[ind - 1], type, indsNeedSum, false, dic);
                }
            }
            ws.Workbook.EndUpdate();
            try
            {
                ////              ws.Workbook.History.IsEnabled = true;
            }
            catch (Exception) { }
            WaitFormHelper.CloseWaitForm();
        }



        public static void BoPhanTichKeHoachVatTu(string codeHaoPhi, string codeCongTac)
        {
            if (SharedControls.ctrl_DonViThucHienDuAnTDKH.SelectedDVTH is null)
                return;

            IWorkbook wb = SharedControls.spsheet_TD_KH_LapKeHoach.Document;
            ////          wb.History.IsEnabled = false;
            wb.BeginUpdate();
            string codeHM = MyFunction.GetCodeHangMucFromCodeCongTac(codeCongTac);
            for (int i = (int)DoBocVatTu.VatLieu; i <= (int)DoBocVatTu.MayThiCong; i++)
            {
                Worksheet ws = wb.Worksheets[TDKH.sheetsName[i]];
                //var dic = dicKPVL_All;
                Dictionary<string, string> dic = MyFunction.fcn_getDicOfColumn(ws.GetUsedRange());
                int[] indsCanXoa = ws.Columns[dic[TDKH.COL_KHVT_Search]].Search(codeHaoPhi, MyConstant.MySearchOptions).Select(x => x.RowIndex).ToArray();
                foreach (int ind in indsCanXoa)
                {
                    Row crRow = ws.Rows[ind];
                    int indCha = (int)crRow[dic[TDKH.COL_RowCha]].Value.NumericValue - 1;
                    ws.Rows.Remove(ind);

                    bool isHaveManyCon = ws.Columns[dic[TDKH.COL_RowCha]].Search(indCha.ToString(), MyConstant.MySearchOptions).Any();
                    //int indRowCha = ws.Rows[];
                    if (!isHaveManyCon)
                    {
                        ws.Rows.Remove(indCha);
                    }
                }
            }
            //MyFunction.Fcn_CalKLKHNew(TypeKLHN.HaoPhiVatTu, new string[] { codeHaoPhi });
            TDKHHelper.CapNhatAllVatTuHaoPhi(new string[] { codeHaoPhi });

            wb.EndUpdate();
            try
            {
                ////          wb.History.IsEnabled = true;

            }
            catch (Exception) { }
        }

        public static string getDescriptionGanttChartCongtac(DateTime? NKT, string codeCongTac, double KLToanBo, IEnumerable<string> LoaiVatTu = null)
        {
            string Desc = "";
            if (LoaiVatTu.Contains("Ngày"))
                goto Label;
            DataTable dtHaoPhi = fcn_GetTblHaoPhiVatTuHienTai(TypeKLHN.CongTac, new string[] { codeCongTac });
            if (dtHaoPhi.Rows.Count == 0)
                return "";
            DataTable _dtHaoPhi = LoaiVatTu.Count() == 3 ? dtHaoPhi : dtHaoPhi.AsEnumerable().Where(x => LoaiVatTu.Contains(x["LoaiVatTu"].ToString())).Any() ?
                dtHaoPhi.AsEnumerable().Where(x => LoaiVatTu.Contains(x["LoaiVatTu"].ToString())).CopyToDataTable() : null;
            if (_dtHaoPhi is null)
                return "";
            double KLKH = KLToanBo;
            string VatLieu = "VL:";
            string MTC = "MTC:";
            double KTNC = 0;
            foreach (DataRow rowHP in _dtHaoPhi.Rows)
            {
                string TenVL = rowHP["VatTu"].ToString();
                string LoaiVL = rowHP["LoaiVatTu"].ToString();
                double KhoiLuong = double.Parse(rowHP["DinhMucNguoiDung"].ToString()) * double.Parse(rowHP["HeSoNguoiDung"].ToString()) * KLKH;

                if (TenVL == "" || TenVL.ToUpper().Contains("KHÁC"))
                    continue;
                switch (LoaiVL)
                {
                    case "Vật liệu":
                        VatLieu += $"; {TenVL} {Math.Round(KhoiLuong, 4)}";
                        break;
                    case "Nhân công":
                        KTNC += KhoiLuong;
                        break;
                    default:
                        MTC += $"; {TenVL} {Math.Ceiling(KhoiLuong)}";
                        break;
                }
            }
            VatLieu = VatLieu == "VL:" ? "VL: " : VatLieu;
            MTC = MTC == "MTC:" ? "MTC: " : MTC;
            Desc = KTNC == 0 ? $"[{VatLieu.Remove(3, 1)}][{MTC.Remove(4, 1)}]" : $"[{VatLieu.Remove(3, 1)}][NC: {Math.Ceiling(KTNC)}][{MTC.Remove(4, 1)}]";
            Desc = Desc.Replace("[MTC:]", "");
            Desc = Desc.Replace("[VL:]", "");
            Label:
            Desc = LoaiVatTu.Contains("Vật liệu") || !NKT.HasValue ? Desc : $"{NKT.Value.ToString(MyConstant.CONST_DATE_FORMAT_SPSHEET)} {Desc}";
            Desc = Desc.EndsWith("__") ? Desc.Replace("__", "") : Desc;
            return Desc;
        }
        public static void fcn_CTac(string loaiVatTu, out DataTable dtCongTacTheoKy, out DataTable dtDependency, out List<KLHN> dtTheoNgay, bool All, bool ThiCong,string CodeHM=null, bool KH=false, DateTime? NBD = null, DateTime? NKT = null)
        {
            dtCongTacTheoKy = null; dtDependency = null;
            dtTheoNgay = new List<KLHN>();
            var DA = SharedControls.slke_ThongTinDuAn.GetSelectedDataRow() as Tbl_ThongTinDuAnViewModel;
            if (DA is null)
                return;

            string condition = TongHopHelper.GetConditionNhaThauToDoiDoBocByGiaiDoan();
            if (All)
                condition = $"cttk.CodeGiaiDoan = '{SharedControls.cbb_DBKH_ChonDot.SelectedValue}'";
            string dbString;
            if (string.IsNullOrEmpty(loaiVatTu))
            {
                #region dbStringBangCongTac
                dbString = $"SELECT COALESCE(cttk.CodeNhom,cttkCha.CodeNhom) AS CodeNhom," +
                    $"mtc.Ten as TenMuiThiCong," +
                    $"COALESCE(nctcha.Ten,nct.Ten)as TenNhom," +
                    $"COALESCE(nctcha.KhoiLuongKeHoach,nct.KhoiLuongKeHoach)as KhoiLuongKeHoachNhom," +
                    $"COALESCE(nctcha.STT,nct.STT)as STTNhom," +
                    $"COALESCE(nctcha.DonVi,nct.DonVi)as DonViNhom,COALESCE(nctcha.NgayBatDau,nct.NgayBatDau)as NgayBatDauNhom,COALESCE(nctcha.NgayKetThuc,nct.NgayKetThuc)as" +
                    $" NgayKetThucNhom," +
                    $"COALESCE(cttk.CodePhanTuyen, hm.CodePhanTuyen) AS CodePhanTuyen," +
                    $"COALESCE(TuyenCT.TenPhanTuyen, hm.TenPhanTuyen) AS TenTuyen," +
                    $"COALESCE(TuyenCT.STT, hm.STT) AS STTTuyen," +
                    $" COALESCE(hm.Code, hmct.Code) AS CodeHangMuc," +
                    $"COALESCE(hm.Ten, hmct.Ten) AS TenHangMuc," +
                    $"COALESCE(hm.STT, hmct.STT) AS STTHangMuc," +
                    $"COALESCE(ctrinh.Code, ctrinhct.Code) AS CodeCongTrinh," +
                    $"COALESCE(ctrinh.Ten, ctrinhct.Ten) AS TenCongTrinh," +
                    $"COALESCE(ctrinh.STT, ctrinhct.STT) AS STTCongTrinh," +
                    $"COALESCE(cttk.DonVi, dmct.DonVi) AS DonVi,COALESCE(cttk.TenCongTac, dmct.TenCongTac) AS TenCongTac," +
                    $"COALESCE(cttk.MaHieuCongTac, dmct.MaHieuCongTac) AS MaHieuCongTac, \r\n" +
                    $"hm.CodeCongTrinh, \r\n" +
                    $"COALESCE(ctrinh.CodeDuAn, ctrinhct.CodeDuAn) AS CodeDuAn,nt.Ten as TenNhaThau,nt.Code as CodeNhaThau, \r\n" +
                    //$"nt.Ten AS TenNhaThau, \r\n" +
                    //$"tdtc.Ten AS TenToDoi, \r\n" +
                    //$"ntp.Ten AS TenNhaThauPhu," +
                    $"COALESCE(ctrinh.SortId, ctrinhct.SortId) AS SortIdCtrinh,COALESCE(hm.SortId, hmct.SortId) AS SortIdHM,cttk.* \r\n" +
                    $"FROM {TDKH.TBL_DanhMucCongTac} dmct\r\n" +
                    $"LEFT JOIN {TDKH.TBL_ChiTietCongTacTheoKy} cttk\r\n" +
                    $"ON cttk.CodeCongTac = dmct.Code \r\n" +
                    $"LEFT JOIN {TDKH.Tbl_TDKH_MuiThiCong} mtc\r\n" +
                    $"ON cttk.CodeMuiThiCong = mtc.Code \r\n" +
                    $"LEFT JOIN {TDKH.TBL_ChiTietCongTacTheoKy} cttkCha\r\n" +
                    $"ON cttk.CodeCha = cttkCha.Code \r\n" +
                    //$"LEFT JOIN {TDKH.TBL_DanhMucCongTac} dmct\r\n" +
                    //$"ON cttk.CodeCongTac = dmct.Code \r\n" +
                    $"LEFT JOIN {TDKH.TBL_NhomCongTac} nct\r\n" +
                    $"ON cttk.CodeNhom = nct.Code \r\n" +
                    $"LEFT JOIN {TDKH.TBL_NhomCongTac} nctcha\r\n" +
                    $"ON cttkcha.CodeNhom = nctcha.Code \r\n" +
                    $" JOIN {MyConstant.view_HangMucWithPhanTuyen} hm\r\n" +
                   $"ON (hm.Code = dmct.CodeHangMuc AND ((dmct.CodePhanTuyen IS NOT NULL AND hm.CodePhanTuyen = dmct.CodePhanTuyen) " +
                   $"OR (dmct.CodePhanTuyen IS NULL AND hm.CodePhanTuyen IS NULL)))\r\n" +
                    //$"OR (dmct.CodePhanTuyen IS NOT NULL AND dmct.CodePhanTuyen = hmpt.CodePhanTuyen)) \r\n" +
                    ((CodeHM.HasValue()) ? $"AND hm.Code = '{CodeHM}'\r\n" : "") +
                    $" LEFT JOIN {MyConstant.view_HangMucWithPhanTuyen} TuyenCT  " +
                    $"ON (hm.Code = cttk.CodeHangMuc AND ((cttk.CodePhanTuyen IS NOT NULL AND TuyenCT.CodePhanTuyen = cttk.CodePhanTuyen) " +
                   $"OR (cttk.CodePhanTuyen IS NULL AND TuyenCT.CodePhanTuyen IS NULL)))\r\n" +
                    //$"OR (dmct.CodePhanTuyen IS NOT NULL AND dmct.CodePhanTuyen = hmpt.CodePhanTuyen)) \r\n" +
                    ((CodeHM.HasValue()) ? $"AND TuyenCT.Code = '{CodeHM}'\r\n" : "") +
                    $"INNER JOIN {MyConstant.TBL_THONGTINCONGTRINH} ctrinh\r\n" +
                    $"ON hm.CodeCongTrinh = ctrinh.Code \r\n" +
                    $"INNER JOIN {MyConstant.TBL_THONGTINDUAN} da\r\n" +
                    $"ON ctrinh.CodeDuAn = da.Code \r\n" +
                    $"LEFT JOIN {MyConstant.view_DonViThucHien} nt\r\n" +
                    $"ON COALESCE(cttk.CodeNhaThau,cttk.CodeToDoi,cttk.CodeNhaThauPhu) = nt.Code \r\n" +   
                    //$"LEFT JOIN {MyConstant.TBL_THONGTINNHATHAU} nt\r\n" +
                    //$"ON cttk.CodeNhaThau = nt.Code \r\n" +
                    //$"LEFT JOIN {MyConstant.TBL_THONGTINTODOITHICONG} tdtc\r\n" +
                    //$"ON cttk.CodeToDoi = tdtc.Code \r\n" +
                    //$"LEFT JOIN {MyConstant.TBL_THONGTINNHATHAUPHU} ntp \r\n" +
                    //$"ON cttk.CodeNhaThauPhu = ntp.Code \r\n" +
                    $"LEFT JOIN Tbl_ThongTinHangMuc hmct  ON cttk.CodeHangMuc = hmct.Code " +
                    $"LEFT JOIN Tbl_ThongTinCongTrinh ctrinhct  ON hmct.CodeCongTrinh = ctrinhct.Code " +
                    $"LEFT JOIN Tbl_ThongTinDuAn dact  ON ctrinhct.CodeDuAn = dact.Code " +
                    $"WHERE (da.Code = '{SharedControls.slke_ThongTinDuAn.EditValue}' OR dact.Code = '{SharedControls.slke_ThongTinDuAn.EditValue}') \r\n" +
                    $"AND {condition} " +
                   ((CodeHM.HasValue()) ? $"AND dmct.CodeHangMuc = '{CodeHM}'\r\n" : "") +
                   ((KH&&NBD!=null) ? $"AND cttk.NgayBatDau>= '{NBD.Value.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE)}'" +
                   $" AND cttk.NgayKetThuc<= '{NKT.Value.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE)}'\r\n" : "") +
                      $"GROUP BY hm.Code, hm.CodePhanTuyen, cttk.Code \r\n" +
                    $"ORDER BY SortIdCtrinh ASC,SortIdHM ASC, cttk.SortId ,cttk.CodeNhaThau ASC\r\n";
                #endregion
                dtCongTacTheoKy = DataProvider.InstanceTHDA.ExecuteQuery(dbString);


                string[] lsCodeCongTac = dtCongTacTheoKy.AsEnumerable().Select(x => x["Code"].ToString()).ToArray();
                dbString = $"SELECT * FROM {TDKH.Tbl_Dependency}";
                //dbString = $"SELECT * FROM {TDKH.Tbl_Dependency} WHERE \"Predecessorcode\"  IN ({MyFunction.fcn_Array2listQueryCondition(lsCodeCongTac)}) OR" +
                //    $" \"Successorcode\"  IN ({MyFunction.fcn_Array2listQueryCondition(lsCodeCongTac)}) ";
                dtDependency = DataProvider.InstanceTHDA.ExecuteQuery(dbString);
                if (ThiCong)
                    dtTheoNgay = MyFunction.Fcn_CalKLKHNewWithoutKeHoach(Common.Enums.TypeKLHN.CongTac, lsCodeCongTac, dateBD: NBD, dateKT: NKT);
            }
            else
            {
                #region dbStringBangCongTac
                dbString = $"SELECT cttk.Code,cttk.CodeMuiThiCong,\r\n" +
                    $"cttk.CodeHangMuc,\r\n" +
                    $"cttk.LoaiVatTu,\r\n" +
                    $"cttk.DonVi,\r\n" +
                    $"cttk.DonGia,\r\n" +
                    $"cttk.NgayBatDau,\r\n" +
                    $"cttk.NgayKetThuc,\r\n" +
                    $"cttk.CodeNhaThau,\r\n" +
                    $"cttk.CodeNhaThauPhu,\r\n" +
                    $"cttk.CodeToDoi,\r\n" +
                    $"cttk.KhoiLuongKeHoach as KhoiLuongToanBo,\r\n" +
                    $"cttk.VatTu as TenCongTac,\r\n" +
                    $"cttk.MaVatLieu as MaHieuCongTac,\r\n" +
                    $"hm.CodeCongTrinh,\r\n" +
                    $"hm.Ten as TenHangMuc,\r\n" +
                    $"ctrinh.Ten as TenCongTrinh,\r\n" +
                    $"ctrinh.CodeDuAn,\r\n" +
                    $"nt.Ten AS TenNhaThau,\r\n" +
                    $"tdtc.Ten AS TenToDoi,\r\n" +
                    $"ntp.Ten AS TenNhaThauPhu,hm.TenPhanTuyen as TenTuyen,hm.CodePhanTuyen as CodePhanTuyen," +
                    $"mtc.Ten as TenMuiThiCong,null AS CodeNhom,null AS CodeCha\r\n" +
                    $"FROM {TDKH.TBL_KHVT_VatTu} cttk\r\n" +
                    $"LEFT JOIN {TDKH.Tbl_TDKH_MuiThiCong} mtc\r\n" +
                    $"ON cttk.CodeMuiThiCong = mtc.Code \r\n" +
                    $"LEFT JOIN {MyConstant.view_HangMucWithPhanTuyen} hm\r\n" +
                     $"ON (hm.Code = cttk.CodeHangMuc AND ((cttk.CodePhanTuyen IS NOT NULL AND hm.CodePhanTuyen = cttk.CodePhanTuyen) " +
                   $"OR (cttk.CodePhanTuyen IS NULL AND hm.CodePhanTuyen IS NULL)))\r\n" +
                    ((CodeHM.HasValue()) ? $"AND hm.Code = '{CodeHM}'\r\n" : "") +
                    //$"ON (cttk.CodeHangMuc = hm.Code \r\n" +
                    //$"AND cttk.CodePhanTuyen = hm.CodePhanTuyen) OR cttk.CodeHangMuc = hm.Code \r\n" +
                    $"LEFT JOIN {MyConstant.TBL_THONGTINCONGTRINH} ctrinh\r\n" +
                    $"ON hm.CodeCongTrinh = ctrinh.Code\r\n" +
                    $"LEFT JOIN {MyConstant.TBL_THONGTINDUAN} da\r\n" +
                    $"ON ctrinh.CodeDuAn = da.Code\r\n" +
                    $"LEFT JOIN {MyConstant.TBL_THONGTINNHATHAU} nt\r\n" +
                    $"ON cttk.CodeNhaThau = nt.Code\r\n" +
                    $"LEFT JOIN {MyConstant.TBL_THONGTINTODOITHICONG} tdtc\r\n" +
                    $"ON cttk.CodeToDoi = tdtc.Code\r\n" +
                    $"LEFT JOIN {MyConstant.TBL_THONGTINNHATHAUPHU} ntp\r\n" +
                    $"ON cttk.CodeNhaThauPhu = ntp.Code\r\n" +
                    $"WHERE \"LoaiVatTu\" = '{loaiVatTu}' \r\n" +
                    ((CodeHM.HasValue()) ? $"AND cttk.CodeHangMuc = '{CodeHM}'\r\n" : "") +
                    $"AND da.Code = '{SharedControls.slke_ThongTinDuAn.EditValue}'  AND {condition}";
                #endregion
                dtCongTacTheoKy = DataProvider.InstanceTHDA.ExecuteQuery(dbString);



                string[] lsCodeCongTac = dtCongTacTheoKy.AsEnumerable().Select(x => x["Code"].ToString()).ToArray();
                if (ThiCong)
                    dtTheoNgay = MyFunction.Fcn_CalKLKHNewWithoutKeHoach(Common.Enums.TypeKLHN.VatLieu, lsCodeCongTac);
            }

            dtCongTacTheoKy.AddIndPhanTuyenNhom();
        }





        public static void fcn_CTacTheoKyNew(string loaiVatTu, out DataTable dtCongTacTheoKy, out List<KLHN> dtTheoNgay, bool All, bool HN = true)
        {
            dtCongTacTheoKy = null;
            dtTheoNgay = new List<KLHN>();
            var DA = SharedControls.slke_ThongTinDuAn.GetSelectedDataRow() as Tbl_ThongTinDuAnViewModel;
            if (DA is null)
                return;

            string condition = TongHopHelper.GetConditionNhaThauToDoiDoBocByGiaiDoan();
            if (All)
                condition = $"cttk.CodeGiaiDoan = '{SharedControls.cbb_DBKH_ChonDot.SelectedValue}'";
            string dbString;

            if (string.IsNullOrEmpty(loaiVatTu))
            {
                #region dbStringBangCongTac
                dbString = $"SELECT cttk.*, dmct.CodeHangMuc,dmct.DonVi, dmct.TenCongTac, dmct.MaHieuCongTac, \r\n" +
                    $"hm.CodeCongTrinh, \r\n" +
                    $"ctrinh.CodeDuAn, \r\n" +
                    $"nt.Ten AS TenNhaThau, \r\n" +
                    $"tdtc.Ten AS TenToDoi, \r\n" +
                    $"ntp.Ten AS TenNhaThauPhu \r\n" +
                    $"FROM {TDKH.TBL_ChiTietCongTacTheoKy} cttk\r\n" +
                    $"LEFT JOIN {TDKH.Tbl_TDKH_MuiThiCong} mtc\r\n" +
                    $"ON cttk.CodeMuiThiCong = mtc.Code \r\n" +
                    $"INNER JOIN {TDKH.TBL_DanhMucCongTac} dmct\r\n" +
                    $"ON cttk.CodeCongTac = dmct.Code \r\n" +
                    $"INNER JOIN {MyConstant.TBL_THONGTINHANGMUC} hm\r\n" +
                    $"ON dmct.CodeHangMuc = hm.Code \r\n" +
                    $"INNER JOIN {MyConstant.TBL_THONGTINCONGTRINH} ctrinh\r\n" +
                    $"ON hm.CodeCongTrinh = ctrinh.Code \r\n" +
                    $"INNER JOIN {MyConstant.TBL_THONGTINDUAN} da\r\n" +
                    $"ON ctrinh.CodeDuAn = da.Code \r\n" +
                    $"LEFT JOIN {MyConstant.TBL_THONGTINNHATHAU} nt\r\n" +
                    $"ON cttk.CodeNhaThau = nt.Code \r\n" +
                    $"LEFT JOIN {MyConstant.TBL_THONGTINTODOITHICONG} tdtc\r\n" +
                    $"ON cttk.CodeToDoi = tdtc.Code \r\n" +
                    $"LEFT JOIN {MyConstant.TBL_THONGTINNHATHAUPHU} ntp\r\n" +
                    $"ON cttk.CodeNhaThauPhu = ntp.Code \r\n" +
                    $"WHERE cttk.CodeGiaiDoan = '{SharedControls.cbb_DBKH_ChonDot.SelectedValue}' \r\n" +
                    $"AND da.Code = '{SharedControls.slke_ThongTinDuAn.EditValue}' \r\n" +
                    $"AND {condition}";
                #endregion
                dtCongTacTheoKy = DataProvider.InstanceTHDA.ExecuteQuery(dbString);

                string[] lsCodeCongTac = dtCongTacTheoKy.AsEnumerable().Select(x => x["Code"].ToString()).ToArray();
                //if (All)
                //{
                //    lsCodeCongTac = dtCongTacTheoKy.AsEnumerable().Where(x => x["CodeNhaThau"] != DBNull.Value).Select(x => x["Code"].ToString()).ToArray();
                //    dtTheoNgay = MyFunction.Fcn_CalKLKHNew(Common.Enums.TypeKLHN.CongTac, lsCodeCongTac);
                //    lsCodeCongTac = dtCongTacTheoKy.AsEnumerable().Where(x=>x["CodeNhaThau"]==DBNull.Value).Select(x => x["Code"].ToString()).ToArray();
                //    //DataTable dtNhaThau= MyFunction.Fcn_CalKLKHNew(Common.Enums.TypeKLHN.CongTac, lsCodeCongTac);
                //    //if (dtNhaThau!=null)
                //    //{
                //    //    dtNhaThau.AsEnumerable().ForEach(x => x["KhoiLuongKeHoach"] = 0);

                //    //    if (dtNhaThau != null)
                //    //        dtTheoNgay.Merge(dtNhaThau);
                //    //}
                //}
                //else
                if (HN)
                    dtTheoNgay = MyFunction.Fcn_CalKLKHNewWithoutKeHoach(Common.Enums.TypeKLHN.CongTac, lsCodeCongTac);


            }
            else
            {
                #region dbStringBangCongTac
                dbString = $"SELECT cttk.Code,\r\n" +
                    $"cttk.CodeHangMuc,\r\n" +
                    $"cttk.LoaiVatTu,\r\n" +
                    $"cttk.DonVi,\r\n" +
                    $"cttk.DonGia,\r\n" +
                    $"cttk.NgayBatDau,\r\n" +
                    $"cttk.NgayKetThuc,\r\n" +
                    $"cttk.NgayBatDauThiCong,\r\n" +
                    $"cttk.NgayKetThucThiCong,\r\n" +
                    $"cttk.CodeNhaThau,\r\n" +
                    $"cttk.CodeNhaThauPhu,\r\n" +
                    $"cttk.CodeToDoi,\r\n" +
                    $"cttk.KhoiLuongKeHoach as KhoiLuongToanBo,\r\n" +
                    $"cttk.VatTu as TenCongTac,\r\n" +
                    $"cttk.VatTu as TenCongTac,\r\n" +
                    $"cttk.MaVatLieu as MaHieuCongTac,\r\n" +
                    $"hm.CodeCongTrinh,\r\n" +
                    $"ctrinh.CodeDuAn,\r\n" +
                    $"nt.Ten AS TenNhaThau,\r\n" +
                    $"tdtc.Ten AS TenToDoi,\r\n" +
                    $"ntp.Ten AS TenNhaThauPhu\r\n" +
                    $"FROM {TDKH.TBL_KHVT_VatTu} cttk\r\n" +
                    $"INNER JOIN {MyConstant.TBL_THONGTINHANGMUC} hm\r\n" +
                    $"ON cttk.CodeHangMuc = hm.Code\r\n" +
                    $"INNER JOIN {MyConstant.TBL_THONGTINCONGTRINH} ctrinh\r\n" +
                    $"ON hm.CodeCongTrinh = ctrinh.Code\r\n" +
                    $"INNER JOIN {MyConstant.TBL_THONGTINDUAN} da\r\n" +
                    $"ON ctrinh.CodeDuAn = da.Code\r\n" +
                    $"LEFT JOIN {MyConstant.TBL_THONGTINNHATHAU} nt\r\n" +
                    $"ON cttk.CodeNhaThau = nt.Code\r\n" +
                    $"LEFT JOIN {MyConstant.TBL_THONGTINTODOITHICONG} tdtc\r\n" +
                    $"ON cttk.CodeToDoi = tdtc.Code\r\n" +
                    $"LEFT JOIN {MyConstant.TBL_THONGTINNHATHAUPHU} ntp\r\n" +
                    $"ON cttk.CodeNhaThauPhu = ntp.Code\r\n" +
                    $"WHERE \"LoaiVatTu\" = '{loaiVatTu}' AND  \"CodeGiaiDoan\" = '{SharedControls.cbb_DBKH_ChonDot.SelectedValue}'\r\n" +
                    $"AND da.Code = '{SharedControls.slke_ThongTinDuAn.EditValue}'  AND {condition}";
                #endregion
                dtCongTacTheoKy = DataProvider.InstanceTHDA.ExecuteQuery(dbString);
                string[] lsCodeCongTac = dtCongTacTheoKy.AsEnumerable().Select(x => x["Code"].ToString()).ToArray();
                if (HN)
                    dtTheoNgay = MyFunction.Fcn_CalKLKHNewWithoutKeHoach(Common.Enums.TypeKLHN.VatLieu, lsCodeCongTac);
            }
        }

        public static void CapNhatThongTinCongTac(string codeCongTac)
        {
            string dbString = $"SELECT * FROM {TDKH.TBL_ChiTietCongTacTheoKy} WHERE \"Code\" = '{codeCongTac}'";
            DataRow dr = DataProvider.InstanceTHDA.ExecuteQuery(dbString).Rows[0];

            IWorkbook wb = SharedControls.spsheet_TD_KH_LapKeHoach.Document;
            ////          wb.History.IsEnabled = false;
            wb.BeginUpdate();
            for (int i = 1; i < 3; i++)
            {
                Worksheet ws = wb.Worksheets[TDKH.sheetsName[i]];
                Dictionary<string, string> dic = MyFunction.fcn_getDicOfColumn(ws.GetUsedRange());

                Column colCode = ws.Columns[dic[TDKH.COL_Code]];
                Row crRow = ws.Rows[colCode.Search(codeCongTac).Single().RowIndex];

                foreach (var item in dic)
                {
                    if (dr.Table.Columns.Contains(item.Key))
                    {
                        crRow[item.Value].SetValueFromText(dr[item.Key].ToString());
                    }
                }

                //MyFunction.fcn_TDKH_CapNhatKeHoachKinhPhiChiTietTungNgay(crRow, isSaveToDb: false);
            }

            wb.EndUpdate();

            try
            {

                ////          wb.History.IsEnabled = true;
            }
            catch (Exception) { }
        }

        /*public static Dictionary<string, int> CheckDicDateVatLieu(DefinedName rangeNgay, string crDateString, CellRange rangeData, bool isCapNhatChaCon = true)
        {
            var dicDateOld = MyFunction.fcn_getDicDate(rangeNgay.Range);
            Worksheet ws = rangeNgay.Range.Worksheet;
            ws.Workbook.BeginUpdate();
            DateTime crDate = DateTime.Parse(crDateString);

            int soCol1Ngay = 5;
            int indRowDate = rangeNgay.Range.TopRowIndex;
            
            if (!dicDateOld.ContainsKey(crDateString))
            {
                var item = dicDateOld.FirstOrDefault(x => DateTime.Parse(x.Key) > crDate);
                int ind2Insert = (item.Equals(default(KeyValuePair<string, int>))) ? rangeNgay.Range.RightColumnIndex : item.Value;
                ws.Columns.Insert(ind2Insert, soCol1Ngay, ColumnFormatMode.FormatAsNext);

                string crHeader = ws.Range.GetColumnNameByIndex(ind2Insert);
                string lastHeader = ws.Columns[ind2Insert + soCol1Ngay - 1].Heading;
                CellRange rangeMerge = ws.Range[$"{crHeader}{indRowDate + 1}:{lastHeader}{indRowDate + 1}"];
                rangeMerge.Merge();
                rangeMerge.SetValueFromText(crDateString);

                string crHeaderKLKH = ws.Columns[ind2Insert].Heading;
                string crHeaderTTKH = ws.Columns[ind2Insert + 1].Heading;
                string crHeaderKLTC = ws.Columns[ind2Insert + 2].Heading;
                string crHeaderDGTC = ws.Columns[ind2Insert + 3].Heading;
                string crHeaderTTTC = ws.Columns[ind2Insert + 4].Heading;
                

                ws.Rows[indRowDate + 1][crHeaderKLKH].SetValue("Khối lượng kế hoạch");
                ws.Rows[indRowDate + 1][crHeaderTTKH].SetValue("Thành tiền kế hoạch");
                
                ws.Rows[indRowDate + 1][crHeaderKLTC].SetValue("Khối lượng thi công");
                ws.Rows[indRowDate + 1][crHeaderDGTC].SetValue("Đơn giá thi công");
                ws.Rows[indRowDate + 1][crHeaderTTTC].SetValue("Thành tiền thi công");

                //ws.Range.FromLTRB(ind2Insert + 2);
                dicDateOld = MyFunction.fcn_getDicDate(rangeNgay.Range);

                if (isCapNhatChaCon)
                 MyFunction.CapNhatAllRowRangeNgay(new Dictionary<string, int>() { { crDateString, dicDateOld[crDateString] } }, rangeData);

            }
            ws.Workbook.EndUpdate();

            return dicDateOld;
        }*/

        public static void capNhatTrangThaiCacBang(string codeCongTac, SourceDataEnum source, string newVal)
        {
            try
            {


                string colContainCode = "";
                switch (source)
                {
                    case SourceDataEnum.KHGV:
                        colContainCode = "CodeCongViecCha";
                        break;
                    case SourceDataEnum.TDKH:
                        colContainCode = GiaoViec.COL_CodeCTTheoGiaiDoan;
                        break;
                    default:
                        return;
                }

                string dbString = $"SELECT CodeCongViecCha, CodeCongTacTheoGiaiDoan FROM {GiaoViec.TBL_CONGVIECCHA} WHERE {colContainCode} = '{codeCongTac}'";
                DataTable dt = DataProvider.InstanceTHDA.ExecuteQuery(dbString);

                string codeGV, codeTD;
                if (dt.Rows.Count > 0)
                {

                    codeGV = dt.Rows[0]["CodeCongViecCha"].ToString();
                    codeTD = dt.Rows[0]["CodeCongTacTheoGiaiDoan"].ToString();

                    dbString = $"UPDATE {TDKH.TBL_ChiTietCongTacTheoKy} SET TrangThai =  '{newVal}'" +
                                        $"WHERE \"Code\" = '{codeTD}'";

                    DataProvider.InstanceTHDA.ExecuteNonQuery(dbString);

                    dbString = $"UPDATE {GiaoViec.TBL_CONGVIECCHA} SET TrangThai =  '{newVal}'" +
                            $"WHERE \"CodeCongTacTheoGiaiDoan\" = '{codeTD}'";

                    DataProvider.InstanceTHDA.ExecuteNonQuery(dbString);
                }
                else
                {
                    codeGV = null;
                    codeTD = codeCongTac;
                }

                string[] code =
                {
                codeGV,
                codeTD,
                codeTD,
            };

                Worksheet[] wss =
                {
                SharedControls.spsheet_GV_KH_ChiTietCacHMCongViec.ActiveWorksheet,
                SharedControls.spsheet_TD_KH_LapKeHoach.Document.Worksheets[TDKH.SheetName_KeHoachKinhPhi],
            };

                Dictionary<string, string>[] dics =
                {
                MyFunction.fcn_getDicOfColumn(wss[0].Range[MyConstant.Range_KeHoach]),
                MyFunction.fcn_getDicOfColumn(wss[1].GetUsedRange())
,
                };

                string[] colsCode =
                {
                GiaoViec.COL_CodeCT,
                TDKH.COL_Code
            };

                string[] colsTrangThai =
    {
                GiaoViec.COL_TrangThai,
                TDKH.COL_TrangThai
            };

                for (int i = 0; i < 2; i++)
                {
                    Worksheet wsRef = wss[i];
                    //if (wsRef == wsOld)
                    //{
                    //    continue;
                    //}
                    if (string.IsNullOrEmpty(code[i]))
                        continue;


                    var dicRef = dics[i];

                    Column colCode = wsRef.Columns[dicRef[colsCode[i]]];
                    int? ind = colCode.Search(code[i], MyConstant.MySearchOptions).SingleOrDefault()?.RowIndex;
                    if (ind.HasValue)
                        wsRef.Rows[ind.Value][dicRef[colsTrangThai[i]]].SetValue(newVal);
                }
            }
            catch (Exception ex)
            {
                AlertShower.ShowInfo("Không có thông tin tương ứng tại giao việc");
            }
        }
    }
}
