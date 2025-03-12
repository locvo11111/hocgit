using Dapper;
using DevExpress.DataProcessing.InMemoryDataProcessor;
using DevExpress.XtraRichEdit.Fields;
using DevExpress.XtraRichEdit.Import.OpenXml;
using DevExpress.XtraSpreadsheet.Model;
using PhanMemQuanLyThiCong.Common.Constant;
using PhanMemQuanLyThiCong.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhanMemQuanLyThiCong.Common.Helper
{
    public static class DoBocChuanHelper
    {
        public static void ReCalcCongTacHaoPhiInNhoms(IEnumerable<string> codesNhom, string[] codesCttk = null, bool isForceDonGia = false, bool isCalKL = true, bool ReSumKL = false)
        {
            foreach (string code in codesNhom)
            {
                ReCalcCongTacHaoPhiInNhom(code, codesCttk, isForceDonGia, isCalKL, ReSumKL);
            }
        }
        public static void ReCalDonGiaThiCongNhom(string codeNhom, string[] codesCttk = null, bool isForceDonGia = false)
        {
            string dbString = $"SELECT NHOM.*," +
                $"TOTAL(KhoiLuongThiCong) AS KhoiLuongDaThiCongNhom\r\n" +
                $" FROM {TDKH.TBL_NhomCongTac} NHOM " +
                $" JOIN {TDKH.TBL_KhoiLuongCongViecHangNgay} klhn ON klhn.CodeNhom=NHOM.Code" +
                $" WHERE NHOM.Code = '{codeNhom}' GROUP BY NHOM.Code";
            var dtNhom = DataProvider.InstanceTHDA.ExecuteQuery(dbString);
            bool IsGiaoThau = false;
            if (dtNhom.Rows.Count == 0)
            {
                dbString = $"SELECT NHOM1.Code," +
                $"TOTAL(KhoiLuongThiCong) AS KhoiLuongDaThiCongNhom\r\n" +
                $" FROM {TDKH.TBL_NhomCongTac} NHOM " +
                $"LEFT JOIN {TDKH.TBL_NhomCongTac} Nhom1\r\n" +
                $"ON Nhom1.CodeNhomGiaoThau = Nhom.Code\r\n" +
                $" JOIN {TDKH.TBL_KhoiLuongCongViecHangNgay} klhn ON klhn.CodeNhom=NHOM1.Code" +
                $" WHERE NHOM.Code = '{codeNhom}' GROUP BY NHOM.Code";
                dtNhom = DataProvider.InstanceTHDA.ExecuteQuery(dbString);
                if (dtNhom.Rows.Count == 0)
                    return;
                IsGiaoThau = true;
            }
            var drNhom = dtNhom.Rows[0];
            if (!double.TryParse(drNhom["KhoiLuongDaThiCongNhom"].ToString(), out double klkhNhom))
            {
                return;
            }
            List<string> lstCodeNhom = new List<string>();
            if (IsGiaoThau)
            {
                dbString = $"SELECT NHOM1.Code" +
                $" FROM {TDKH.TBL_NhomCongTac} NHOM " +
                $"LEFT JOIN {TDKH.TBL_NhomCongTac} Nhom1\r\n" +
                $"ON Nhom1.CodeNhomGiaoThau = Nhom.Code\r\n" +
                $" WHERE NHOM.Code = '{codeNhom}'";
                DataTable dtNhomNhanThau = DataProvider.InstanceTHDA.ExecuteQuery(dbString);
                lstCodeNhom = dtNhomNhanThau.AsEnumerable().Select(x => x["Code"].ToString()).Distinct().ToList();
            }
            else
                lstCodeNhom.Add(codeNhom);

            dbString = $"SELECT klhn.*,cttk.DonGiaThiCong FROM {TDKH.TBL_ChiTietCongTacTheoKy} cttk " +
                $" JOIN {TDKH.TBL_KhoiLuongCongViecHangNgay} klhn ON klhn.CodeCongTacTheoGiaiDoan=cttk.Code " +
                $"WHERE cttk.CodeNhom IN ({MyFunction.fcn_Array2listQueryCondition(lstCodeNhom.ToArray())})";

            if (codesCttk != null)
            {
                dbString += $" AND Code IN ({MyFunction.fcn_Array2listQueryCondition(codesCttk)}) ";
            }
            //dbString += "GROUP BY cttk.CodeNhom";
            DataTable dtCttks = DataProvider.InstanceTHDA.ExecuteQuery(dbString);
            if (dtCttks.Rows.Count == 0)
                return;
            codesCttk = dtCttks.AsEnumerable().Select(x => x["Code"].ToString()).ToArray();
            if (isForceDonGia || drNhom["DonGiaThiCong"] == DBNull.Value)
            {
                var TTNhom = dtCttks.AsEnumerable().Sum(x => double.Parse(x["DonGiaThiCong"].ToString()) * double.Parse(x["KhoiLuongThiCong"].ToString()));
                var donGiaNew = (klkhNhom == 0) ? 0 : TTNhom / klkhNhom;

                dbString = $"UPDATE {TDKH.TBL_NhomCongTac}\r\n" +
                    $"SET DonGiaThiCong = '{Math.Round(donGiaNew)}'\r\n" +
                    $"WHERE Code = '{codeNhom}'";

                DataProvider.InstanceTHDA.ExecuteNonQuery(dbString);
            }

        }
        public static void ReCalcCongTacHaoPhiInNhom(string codeNhom, string[] codesCttk = null, bool isForceDonGia = false, bool isCalKL = true, bool ReSumKL = false)
        {
            string dbString = $"SELECT * FROM {TDKH.TBL_NhomCongTac} WHERE Code = '{codeNhom}'";
            var dtNhom = DataProvider.InstanceTHDA.ExecuteQuery(dbString);
            if (dtNhom.Rows.Count == 0)
            {
                return;
            }
            var drNhom = dtNhom.Rows[0];
            if (!double.TryParse(drNhom["KhoiLuongKeHoach"].ToString(), out double klkhNhom) && !ReSumKL)
            {
                return;
            }

            WaitFormHelper.ShowWaitForm("Đang cập nhật công tác trong nhóm");


            dbString = $"SELECT * FROM {TDKH.TBL_ChiTietCongTacTheoKy} WHERE CodeNhom = '{codeNhom}'\r\n";

            if (codesCttk != null)
            {
                dbString += $" AND Code IN ({MyFunction.fcn_Array2listQueryCondition(codesCttk)}) ";
            }

            DataTable dtCttks = DataProvider.InstanceTHDA.ExecuteQuery(dbString);
            codesCttk = dtCttks.AsEnumerable().Select(x => x["Code"].ToString()).ToArray();

            if (dtCttks.Rows.Count == 0)
            {
                dbString = $"DELETE FROM {Server.Tbl_TDKH_NhomCongTac} WHERE Code IN\r\n" +
                    $"(SELECT nhom.Code FROM {Server.Tbl_TDKH_NhomCongTac} nhom\r\n" +
                    $"LEFT JOIN {TDKH.TBL_ChiTietCongTacTheoKy} cttk\r\n" +
                    $"ON nhom.Code = cttk.CodeNhom\r\n" +
                    $"LEFT JOIN {TDKH.TBL_DanhMucCongTac} dmct\r\n" +
                    $"ON cttk.CodeCongTac = dmct.Code\r\n" +
                    $"WHERE cttk.code IS NULL)\r\n";
                DataProvider.InstanceTHDA.ExecuteNonQuery(dbString);
                return;

            }

            if (ReSumKL)
            {
                drNhom["KhoiLuongKeHoach"] = klkhNhom = dtCttks.AsEnumerable().Sum(x => double.Parse(x["KhoiLuongToanBo"].ToString()));
                DataProvider.InstanceTHDA.ExecuteNonQuery($"UPDATE {Server.Tbl_TDKH_NhomCongTac} SET KhoiLuongKeHoach = '{klkhNhom}' WHERE Code = '{codeNhom}'");
            }

            string condCodesCttk = MyFunction.fcn_Array2listQueryCondition(codesCttk);

            if (drNhom["NgayBatDau"] == DBNull.Value || drNhom["NgayKetThuc"] == DBNull.Value)
            {
                var minNBD = dtCttks.AsEnumerable().Min(x => x["NgayBatDau"].ToString());
                var maxNKT = dtCttks.AsEnumerable().Min(x => x["NgayKetThuc"].ToString());

                dbString = $"UPDATE {TDKH.TBL_NhomCongTac}\r\n" +
                    $"SET NgayBatDau = '{minNBD}', NgayKetThuc = '{maxNKT}'\r\n" +
                    $"WHERE Code = '{codeNhom}'";

                DataProvider.InstanceTHDA.ExecuteNonQuery(dbString);

            }
            else
            {
                foreach (DataRow dr in dtCttks.Rows)
                {
                    if (dr["NgayBatDau"].ToString() != drNhom["NgayBatDau"].ToString())
                    {
                        dr["NgayBatDau"] = drNhom["NgayBatDau"];
                    }

                    if (dr["NgayKetThuc"].ToString() != drNhom["NgayKetThuc"].ToString())
                    {
                        dr["NgayKetThuc"] = drNhom["NgayKetThuc"];
                    }
                }
            }

            bool isGiaoThau = (dtCttks.Rows[0]["CodeNhaThau"] != DBNull.Value);

            if (isForceDonGia || drNhom["DonGia"] == DBNull.Value)
            {
                var DonGia = dtCttks.AsEnumerable().Sum(x => double.Parse(x["DonGia"].ToString()) * double.Parse(x["KhoiLuongToanBo"].ToString()));
                //var KhoiLuong = dtCttks.AsEnumerable().Sum(x => double.Parse(x["KhoiLuongToanBo"].ToString()));

                double.TryParse(drNhom["KhoiLuongKeHoach"].ToString(), out double KLNhom);

                var donGiaNew = (KLNhom == 0) ? 0 : DonGia / KLNhom;

                dbString = $"UPDATE {TDKH.TBL_NhomCongTac}\r\n" +
                    $"SET DonGia = '{Math.Round(donGiaNew)}'\r\n" +
                    $"WHERE Code = '{codeNhom}'";

                DataProvider.InstanceTHDA.ExecuteNonQuery(dbString);

            }

            if (isForceDonGia && isGiaoThau)
            {
                var kltchns = MyFunction.Fcn_CalKLKHNewWithoutKeHoach(Enums.TypeKLHN.CongTac, codesCttk);
                double? rate = null;
                foreach (DataRow dr in dtCttks.Rows)
                {
                    double KLKH = double.Parse(dr["KhoiLuongToanBo"].ToString());
                    double KLTC = kltchns.Where(x => x.ParentCode == dr["Code"].ToString())
                        .Sum(x => x.KhoiLuongThiCong) ?? 0;

                    if (KLKH == 0)
                        continue;

                    var eachRate = KLTC / KLKH;
                    if (rate is null || eachRate < rate)
                        rate = eachRate;

                }
                rate = rate ?? 0;

                double KLTCNhom = rate.Value * klkhNhom;


                double TTTCNhom = kltchns.Sum(x => x.ThanhTienThiCong) ?? 0;


                double DGTCNhom = (KLTCNhom == 0) ? 0 : TTTCNhom / KLTCNhom;

                dbString = $"UPDATE {TDKH.TBL_NhomCongTac}\r\n" +
                        $"SET DonGiaThiCong = '{Math.Round(DGTCNhom)}', KhoiLuongDaThiCong = '{KLTCNhom}'\r\n" +
                        $"WHERE Code = '{codeNhom}'";

                DataProvider.InstanceTHDA.ExecuteNonQuery(dbString);

            }

            //if (isForceDonGia || drNhom["DonGiaThiCong"] == DBNull.Value)
            //{
            //    var DonGia = dtCttks.AsEnumerable().Sum(x => double.Parse(x["DonGiaThiCong"].ToString()));

            //    dbString = $"UPDATE {TDKH.TBL_NhomCongTac}\r\n" +
            //        $"SET DonGiaThiCong = '{DonGia}'\r\n" +
            //        $"WHERE Code = '{codeNhom}'";

            //    DataProvider.InstanceTHDA.ExecuteNonQuery(dbString);
            //}

            //if (isForceDonGia || drNhom["DonGiaHopDongChiTiet"] == DBNull.Value)
            //{
            //    var DonGia = dtCttks.AsEnumerable().Sum(x => double.Parse(x["DonGiaHopDongChiTiet"].ToString()));

            //    dbString = $"UPDATE {TDKH.TBL_NhomCongTac}\r\n" +
            //        $"SET DonGiaHopDongChiTiet = '{DonGia}'\r\n" +
            //        $"WHERE Code = '{codeNhom}'";

            //    DataProvider.InstanceTHDA.ExecuteNonQuery(dbString);
            //}

            DataProvider.InstanceTHDA.UpdateDataTableFromSqliteSource(dtCttks, TDKH.TBL_ChiTietCongTacTheoKy);

            if (isCalKL)
            {

                dbString = $"SELECT cvhn.* " +
                    $"FROM {TDKH.TBL_KhoiLuongCongViecHangNgay} cvhn\r\n" +
                    $"JOIN {TDKH.TBL_NhomCongTac} nct\r\n" +
                    $"ON cvhn.CodeNhom = nct.Code\r\n" +
                    $"WHERE nct.Code = '{codeNhom}'";

                DataTable dthnNhom = DataProvider.InstanceTHDA.ExecuteQuery(dbString);

                //dthnNhom.Columns.Add("Calculated", typeof(bool));
                //dthnNhom.Columns["Calculated"].DefaultValue = false;

                //var validsDate = dthnNhom.AsEnumerable().Select(x => x["Ngay"].ToString()).ToArray();

                dbString = $"SELECT cttk.Code AS CodeCongTacTheoGiaiDoan, cvhn.*, cttk.KhoiLuongToanBo AS KhoiLuongCongTac " +
                    $"FROM {TDKH.TBL_ChiTietCongTacTheoKy} cttk\r\n" +
                    $"LEFT JOIN {TDKH.TBL_KhoiLuongCongViecHangNgay} cvhn\r\n" +
                    $"ON cvhn.CodeCongTacTheoGiaiDoan = cttk.Code\r\n" +
                    $"JOIN {TDKH.TBL_NhomCongTac} nct\r\n" +
                    $"ON cttk.CodeNhom = nct.Code\r\n" +
                    $"WHERE cttk.Code IN ({condCodesCttk})";

                DataTable dthnCttk = DataProvider.InstanceTHDA.ExecuteQuery(dbString);


                //dbString = $"SELECT cvhn.* " +
                //    $"FROM {TDKH.TBL_KhoiLuongHaoPhiTheoNgay} cvhn\r\n" +
                //    $"JOIN {TDKH.Tbl_HaoPhiVatTu} hp\r\n" +
                //    $"ON cvhn.CodeHaoPhiVatTu = hp.Code\r\n" +
                //    //$"JOIN {TDKH.TBL_ChiTietCongTacTheoKy} cttk\r\n" +
                //    //$"ON hp.CodeCongTac = cttk.Code\r\n" +
                //    $"WHERE hp.CodeCongTac IN ({condCodesCttk})";

                //DataTable dthnHp = DataProvider.InstanceTHDA.ExecuteQuery(dbString);

                var grsCttks = dthnCttk.AsEnumerable().GroupBy(x => x["CodeCongTacTheoGiaiDoan"].ToString());

                foreach (var gr in grsCttks)
                {
                    double klkhctac = double.Parse(gr.First()["KhoiLuongCongTac"].ToString());
                    double tyle = klkhctac / klkhNhom;
                    List<string> codesHnNhomCalculated = new List<string>();
                    foreach (DataRow dr in gr.Where(x => x["Code"] != DBNull.Value))
                    {
                        string crDate = dr["Ngay"].ToString();
                        DataRow drhnNhom = dthnNhom.AsEnumerable().SingleOrDefault(x => x["Ngay"].ToString() == crDate);

                        if (drhnNhom is null)
                        {
                            if (dr["KhoiLuongKeHoachGiaoViec"] == DBNull.Value)
                            {

                                dr.Delete();
                                continue;
                            }
                            else
                            {
                                dr["KhoiLuongKeHoach"] = dr["KhoiLuongThiCong"] = DBNull.Value;
                            }
                        }
                        else
                        {
                            codesHnNhomCalculated.Add(drhnNhom["Code"].ToString());


                            if (drhnNhom["KhoiLuongKeHoach"] == DBNull.Value)
                            {
                                dr["KhoiLuongKeHoach"] = DBNull.Value;
                            }
                            else
                            {
                                dr["KhoiLuongKeHoach"] = Math.Round(double.Parse(drhnNhom["KhoiLuongKeHoach"].ToString()) * tyle, 4);
                            }

                            if (drhnNhom["KhoiLuongBoSung"] == DBNull.Value)
                            {
                                dr["KhoiLuongBoSung"] = DBNull.Value;
                            }
                            else
                            {
                                dr["KhoiLuongBoSung"] = Math.Round(double.Parse(drhnNhom["KhoiLuongBoSung"].ToString()) * tyle, 4);
                            }

                            if (drhnNhom["KhoiLuongThiCong"] == DBNull.Value)
                            {
                                dr["KhoiLuongThiCong"] = DBNull.Value;
                            }
                            else
                            {
                                dr["KhoiLuongThiCong"] = Math.Round(double.Parse(drhnNhom["KhoiLuongThiCong"].ToString()) * tyle, 4);
                            }
                        }
                    }

                    foreach (DataRow drhnNhom in dthnNhom.AsEnumerable().Where(x => x.RowState != DataRowState.Deleted && !codesHnNhomCalculated.Contains((string)x["Code"])))
                    {
                        DataRow newRow = dthnCttk.NewRow();
                        dthnCttk.Rows.Add(newRow);
                        newRow["Code"] = Guid.NewGuid().ToString();
                        newRow["CodeCongTacTheoGiaiDoan"] = gr.Key;
                        newRow["Ngay"] = drhnNhom["Ngay"];

                        if (drhnNhom["KhoiLuongKeHoach"] == DBNull.Value)
                        {
                            newRow["KhoiLuongKeHoach"] = DBNull.Value;
                        }
                        else
                        {
                            newRow["KhoiLuongKeHoach"] = Math.Round(double.Parse(drhnNhom["KhoiLuongKeHoach"].ToString()) * tyle, 4);
                        }
                        
                        if (drhnNhom["KhoiLuongBoSung"] == DBNull.Value)
                        {
                            newRow["KhoiLuongBoSung"] = DBNull.Value;
                        }
                        else
                        {
                            newRow["KhoiLuongBoSung"] = Math.Round(double.Parse(drhnNhom["KhoiLuongBoSung"].ToString()) * tyle, 4);
                        }

                        if (drhnNhom["KhoiLuongThiCong"] == DBNull.Value)
                        {
                            newRow["KhoiLuongThiCong"] = DBNull.Value;
                        }
                        else
                        {
                            newRow["KhoiLuongThiCong"] = Math.Round(double.Parse(drhnNhom["KhoiLuongThiCong"].ToString()) * tyle, 4);
                        }
                    }
                }
                DataProvider.InstanceTHDA.UpdateDataTableFromSqliteSource(dthnCttk, TDKH.TBL_KhoiLuongCongViecHangNgay);
                //ReCalcKlhnHaoPhiByCtac(codesCttk);
            }


            WaitFormHelper.CloseWaitForm();
        }

        public static void ReCalcKlhnVatTuByDonViThucHien(DonViThucHien crDVTH, string CodeCT, string CodeHM)
        {
            WaitFormHelper.ShowWaitForm("Đang tính toán lại khối lượng vật tư");

            string dbString = $"SELECT cttk.Code " +
                $"FROM {TDKH.TBL_ChiTietCongTacTheoKy} cttk\r\n" +
                $"LEFT JOIN {TDKH.TBL_DanhMucCongTac} dmct\r\n" +
                $"ON cttk.CodeCongTac = dmct.Code\r\n" +
                $"JOIN {Server.Tbl_ThongTinHangMuc} hm\r\n" +
                $"ON COALESCE(cttk.CodeHangMuc, dmct.CodeHangMuc) = hm.Code\r\n" +
                $"WHERE cttk.{crDVTH.ColCodeFK} = '{crDVTH.Code}'\r\n" +
                ((CodeHM != null)? $"AND hm.Code = '{CodeHM}'\r\n":"" )+
                ((CodeCT != null)? $"AND hm.CodeCongTrinh = '{CodeCT}'\r\n":"" )+
                $"";
            DataTable dt = DataProvider.InstanceTHDA.ExecuteQuery(dbString);

            var codes = dt.AsEnumerable().Select(x => x["Code"].ToString());
            DataTable dthnHaoPhi = DoBocChuanHelper.ReCalcKlhnHaoPhiByCtac(codes);

            dbString = $"SELECT vt.Code AS CodeVatTuReal, hn.*\r\n" +
                $"FROM {TDKH.TBL_KHVT_VatTu} vt\r\n" +
                $"LEFT JOIN {TDKH.TBL_KHVT_KhoiLuongHangNgay} hn\r\n" +
                $"ON hn.CodeVatTu = vt.Code\r\n" +
                $"WHERE vt.{crDVTH.ColCodeFK} = '{crDVTH.Code}'";
            DataTable dthnVtu = DataProvider.InstanceTHDA.ExecuteQuery(dbString);

            var grsHpByVt = dthnHaoPhi.AsEnumerable().Where(x => x["CodeVatTu"] != DBNull.Value && x["Ngay"] != DBNull.Value).GroupBy(x => x["CodeVatTu"].ToString());

            foreach (var grHpByVt in grsHpByVt)
            {
                string codeVT = grHpByVt.Key;

                var drsVt = dthnVtu.AsEnumerable()
                                .Where(x => x["CodeVatTu"].ToString() == codeVT);

                var grsNgay = grHpByVt.GroupBy(x => x["Ngay"].ToString());

                foreach (var grNgay in grsNgay)
                {
                    string crDate = grNgay.Key;
                    var drVatTu = drsVt.AsEnumerable().SingleOrDefault(x => x["Ngay"].ToString() == crDate);

                    if (drVatTu is null)
                    {
                        drVatTu = dthnVtu.NewRow();
                        dthnVtu.Rows.Add(drVatTu);

                        drVatTu["Code"] = Guid.NewGuid().ToString();
                        drVatTu["CodeVatTu"] = codeVT;
                        drVatTu["Ngay"] = crDate;
                    }
                    drVatTu["KhoiLuongKeHoach"] = grNgay.Where(x => x["KhoiLuongKeHoach"] != DBNull.Value).Sum(x => double.Parse(x["KhoiLuongKeHoach"].ToString()));
                    drVatTu["KhoiLuongThiCong"] = grNgay.Where(x => x["KhoiLuongThiCong"] != DBNull.Value).Sum(x => double.Parse(x["KhoiLuongThiCong"].ToString()));
                    drVatTu["KhoiLuongBoSung"] = grNgay.Where(x => x["KhoiLuongBoSung"] != DBNull.Value).Sum(x => double.Parse(x["KhoiLuongBoSung"].ToString()));
                }
            }

            DataProvider.InstanceTHDA.UpdateDataTableFromSqliteSource(dthnVtu, TDKH.TBL_KHVT_KhoiLuongHangNgay);
            WaitFormHelper.CloseWaitForm();
        }

        public static DataTable ReCalcKlhnHaoPhiByCtac(IEnumerable<string> codesCttk)
        {
            WaitFormHelper.ShowWaitForm("Đang tính toán lại khối lượng hao phí");
            string condCodesCttk = MyFunction.fcn_Array2listQueryCondition(codesCttk);
            string dbString = $"SELECT cvhn.*, cttk.KhoiLuongToanBo AS KhoiLuongCongTac " +
                    $"FROM {TDKH.TBL_KhoiLuongCongViecHangNgay} cvhn\r\n" +
                    $"JOIN {TDKH.TBL_ChiTietCongTacTheoKy} cttk\r\n" +
                    $"ON cvhn.CodeCongTacTheoGiaiDoan = cttk.Code\r\n" +
                    //$"JOIN {TDKH.TBL_NhomCongTac} nct\r\n" +
                    //$"ON cttk.CodeNhom = nct.Code\r\n" +
                    $"WHERE cttk.Code IN ({condCodesCttk})";

            DataTable dthnCttk = DataProvider.InstanceTHDA.ExecuteQuery(dbString);

            dthnCttk.Columns.Add("Calculated", typeof(bool));
            dthnCttk.Columns["Calculated"].DefaultValue = false;

            dbString = $"SELECT hp.Code AS CodeHaoPhiVatTuReal, cvhn.*, hp.CodeCongTac, hp.HeSoNguoiDung, hp.DinhMucNguoiDung, hp.CodeVatTu " +
                $"FROM {TDKH.Tbl_HaoPhiVatTu} hp\r\n" +
                $"LEFT JOIN {TDKH.TBL_KhoiLuongHaoPhiTheoNgay} cvhn\r\n" +
                $"ON cvhn.CodeHaoPhiVatTu = hp.Code\r\n" +
                //$"JOIN {TDKH.TBL_ChiTietCongTacTheoKy} cttk\r\n" +
                //$"ON hp.CodeCongTac = cttk.Code\r\n" +
                $"WHERE hp.PhanTichKeHoach = 1 AND hp.CodeCongTac IN ({condCodesCttk})";

            DataTable dthnHp = DataProvider.InstanceTHDA.ExecuteQuery(dbString);

            var grsCtac = dthnHp.AsEnumerable().GroupBy(x => x["CodeCongTac"].ToString());

            foreach (var grCtac in grsCtac)
            {
                string codeCttk = grCtac.Key;
                var drs = dthnCttk.AsEnumerable()
                    .Where(x => x["CodeCongTacTheoGiaiDoan"].ToString() == codeCttk);


                var grsHp = grCtac.GroupBy(x => x["CodeHaoPhiVatTuReal"].ToString());

                foreach (var grHp in grsHp)
                {
                    var fstHp = grHp.First();
                    double hsnd = double.Parse(fstHp["HeSoNguoiDung"].ToString());
                    double dmnd = double.Parse(fstHp["DinhMucNguoiDung"].ToString());

                    List<string> codesHnCalculated = new List<string>();

                    foreach (var hnHp in grHp.Where(x => x["Code"] != DBNull.Value))
                    {
                        var hncttk = drs.SingleOrDefault(x => x["Ngay"].ToString() == hnHp["Ngay"].ToString());

                        if (hncttk is null)
                        {
                            if (hnHp["KhoiLuongKeHoachGiaoViec"] == DBNull.Value)
                            {
                                hnHp.Delete();
                            }
                            else
                                hnHp["KhoiLuongKeHoach"] = hnHp["KhoiLuongThiCong"] = DBNull.Value;
                        }
                        else
                        {
                            codesHnCalculated.Add(hncttk["Code"].ToString());

                            if (hncttk["KhoiLuongKeHoach"] != DBNull.Value)
                            {
                                hnHp["KhoiLuongKeHoach"] = Math.Round(double.Parse(hncttk["KhoiLuongKeHoach"].ToString()) * hsnd * dmnd, 4);
                            }
                            
                            if (hncttk["KhoiLuongBoSung"] != DBNull.Value)
                            {
                                hnHp["KhoiLuongBoSung"] = Math.Round(double.Parse(hncttk["KhoiLuongBoSung"].ToString()) * hsnd * dmnd, 4);
                            }

                            if (hncttk["KhoiLuongThiCong"] != DBNull.Value)
                            {
                                hnHp["KhoiLuongThiCong"] = Math.Round(double.Parse(hncttk["KhoiLuongThiCong"].ToString()) * hsnd * dmnd, 4);
                            }
                        }

                    }
                    foreach (DataRow drhnCTac in drs.AsEnumerable().Where(x => x.RowState != DataRowState.Deleted && !codesHnCalculated.Contains((string)x["Code"])))
                    {
                        DataRow newRow = dthnHp.NewRow();
                        dthnHp.Rows.Add(newRow);
                        newRow["Code"] = Guid.NewGuid().ToString();
                        newRow["CodeHaoPhiVatTu"] = grHp.Key;
                        newRow["Ngay"] = drhnCTac["Ngay"];

                        if (drhnCTac["KhoiLuongKeHoach"] == DBNull.Value)
                        {
                            newRow["KhoiLuongKeHoach"] = DBNull.Value;
                        }
                        else
                        {
                            newRow["KhoiLuongKeHoach"] = Math.Round(double.Parse(drhnCTac["KhoiLuongKeHoach"].ToString()) * hsnd * dmnd, 4);
                        }
                        
                        if (drhnCTac["KhoiLuongBoSung"] == DBNull.Value)
                        {
                            newRow["KhoiLuongBoSung"] = DBNull.Value;
                        }
                        else
                        {
                            newRow["KhoiLuongBoSung"] = Math.Round(double.Parse(drhnCTac["KhoiLuongBoSung"].ToString()) * hsnd * dmnd, 4);
                        }

                        if (drhnCTac["KhoiLuongThiCong"] == DBNull.Value)
                        {
                            newRow["KhoiLuongThiCong"] = DBNull.Value;
                        }
                        else
                        {
                            newRow["KhoiLuongThiCong"] = Math.Round(double.Parse(drhnCTac["KhoiLuongThiCong"].ToString()) * hsnd * dmnd, 4);
                        }
                    }
                }
            }

            //DataProvider.InstanceTHDA.UpdateDataTableFromSqliteSource(dthnHp, TDKH.Tbl_HaoPhiVatTu_HangNgay);
            WaitFormHelper.CloseWaitForm();
            dthnHp.AcceptChanges();
            return dthnHp;

        }

        public static void CapNhatNhanCongNhom(string codeNhom, double KLKHTong, Dictionary<DateTime, int> dicNCdt)
        {
            int sumNC = dicNCdt.Sum(x => x.Value);
            var dicTyLe = dicNCdt.ToDictionary(x => x.Key.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE), x => x.Value / (double)sumNC);

            string dbString = $"SELECT hn.*\r\n" +
                $"FROM {TDKH.TBL_KhoiLuongCongViecHangNgay} hn\r\n" +
                //$"ON hn.CodeNhom = nct.Code\r\n" +
                $"WHERE hn.CodeNhom = '{codeNhom}'";

            DataTable dthn = DataProvider.InstanceTHDA.ExecuteQuery(dbString);

            foreach (DataRow dr in dthn.AsEnumerable().Where(x => !dicTyLe.ContainsKey(x["Ngay"].ToString())))
            {
                if (dr["KhoiLuongKeHoachGiaoViec"] != DBNull.Value || dr["KhoiLuongThiCong"] != DBNull.Value)
                {
                    dr["KhoiLuongKeHoach"] = DBNull.Value;
                }
                else
                    dr.Delete();
            }

            foreach (var item in dicTyLe)
            {
                var drhn = dthn.AsEnumerable()
                    .SingleOrDefault(x => x.RowState != DataRowState.Deleted && x["Ngay"].ToString() == item.Key);

                if (drhn is null)
                {
                    drhn = dthn.NewRow();
                    dthn.Rows.Add(drhn);

                    drhn["Code"] = Guid.NewGuid().ToString();
                    drhn["CodeNhom"] = codeNhom;
                    drhn["Ngay"] = item.Key;
                }

                drhn["KhoiLuongKeHoach"] = Math.Round(KLKHTong * (double)item.Value, 4);
            }
            DataProvider.InstanceTHDA.UpdateDataTableFromSqliteSource(dthn, TDKH.TBL_KhoiLuongCongViecHangNgay);
        }

        public static void CapNhatNhanCong(string CodeCTTK, Dictionary<DateTime, int> dicNCdt)
        {
            var dicNC = dicNCdt.ToDictionary(x => x.Key.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE), x => x.Value);

            string dbString = $"SELECT hp.Code AS CodeHaoPhiVatTu, hn.* " +
                $"FROM {TDKH.Tbl_HaoPhiVatTu} hp\r\n" +
                $"LEFT JOIN {TDKH.Tbl_HaoPhiVatTu_HangNgay} hn\r\n" +
                $"ON hn.CodeHaoPhiVatTu = hp.Code\r\n" +
                $"WHERE CodeCongTac = '{CodeCTTK}' AND hp.LoaiVatTu = 'Nhân công'";

            DataTable dthn = DataProvider.InstanceTHDA.ExecuteQuery(dbString);



            var grsHn = dthn.AsEnumerable().GroupBy(x => x["CodeHaoPhiVatTu"].ToString());

            var numTypeNC = grsHn.Count();

            foreach (var grhn in grsHn)
            {
                foreach (DataRow dr in grhn.Where(x => x["Code"] != DBNull.Value && !dicNC.ContainsKey(x["Ngay"].ToString())))
                {
                    if (dr["KhoiLuongKeHoachGiaoViec"] != DBNull.Value || dr["KhoiLuongThiCong"] != DBNull.Value)
                    {
                        dr["KhoiLuongKeHoach"] = DBNull.Value;
                    }
                    else
                        dr.Delete();
                }

                foreach (var item in dicNC)
                {
                    var drhn = grhn.SingleOrDefault(x => x.RowState != DataRowState.Deleted && x["Code"] != DBNull.Value && x["Ngay"].ToString() == item.Key);

                    if (drhn is null)
                    {
                        drhn = dthn.NewRow();
                        dthn.Rows.Add(drhn);

                        drhn["Code"] = Guid.NewGuid().ToString();
                        drhn["CodeHaoPhiVatTu"] = grhn.Key;
                        drhn["Ngay"] = item.Key;
                    }

                    drhn["KhoiLuongKeHoach"] = Math.Round((double)item.Value / (double)numTypeNC, 4);
                }
            }

            DataProvider.InstanceTHDA.UpdateDataTableFromSqliteSource(dthn, TDKH.Tbl_HaoPhiVatTu_HangNgay);

        }
    }
}