using DevExpress.DataProcessing.InMemoryDataProcessor;
using PhanMemQuanLyThiCong.Common.Constant;
using PhanMemQuanLyThiCong.Model.PermissionControl;
using PhanMemQuanLyThiCong.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PM360.Common.Helper;
using System.IO;
using DevExpress.XtraRichEdit.Fields;
using System.Windows.Forms;
using MSETTING = PhanMemQuanLyThiCong.Properties.Settings;
using VChatCore.ViewModels.SyncSqlite;
using DevExpress.DataProcessing;
using Dapper;

namespace PhanMemQuanLyThiCong.Common.Helper
{
    public static class THDAHelper
    {
        static string[] tblsMTC =
        {
            Server.Tbl_MTC_ChiTietDinhMuc,
            Server.Tbl_MTC_ChiTietNhatTrinh,
            Server.Tbl_MTC_ChuSoHuuInMay,
            Server.Tbl_MTC_DanhSachChuSoHuu,
            Server.Tbl_MTC_DanhSachMay,
            Server.Tbl_MTC_DuAnInMay,
            Server.Tbl_MTC_FileDinhKem,
            Server.Tbl_MTC_LoaiNhienLieu,
            Server.Tbl_MTC_NguoiVanHanhInMay,
            Server.Tbl_MTC_NhatTrinhCongTac,
            Server.Tbl_MTC_NhienLieu,
            Server.Tbl_MTC_NhienLieuInMay,
            Server.Tbl_MTC_TrangThai,
        };
        public static void MigrationSQLite()
        {
            try
            {

                #region SQLiteToModel
                Tbl_ThongTinTHDA THDA = DataProvider.InstanceTHDA.ExecuteQueryModel<Tbl_ThongTinTHDA>($"SELECT * FROM Tbl_ThongTinTHDA")[0];

                bool isNeedUpdate = true;
                Version.TryParse(THDA.LastMigrationVersion, out Version crVersion);

                WaitFormHelper.ShowWaitForm("Đang xử lý file dữ liệu!");
                DataProvider.InstanceTHDA.ExecuteNonQuery($"DELETE FROM {Server.Tbl_TDKH_HaoPhiVatTu_KhoiLuongHangNgay} WHERE KhoiLuongThiCong IS NULL OR KhoiLuongThiCong = 0", AddToDeletedDataTable: false);
                DataProvider.InstanceTHDA.ExecuteNonQuery($"DELETE FROM Tbl_TDKH_KhoiLuongCongViecTungNgay WHERE KhoiLuongKeHoach IS NULL AND (KhoiLuongThiCong IS NULL OR KhoiLuongThiCong = 0)", AddToDeletedDataTable: false);

                WaitFormHelper.CloseWaitForm();
                if (crVersion != null && crVersion.CompareTo(BaseFrom.FirstVersionSameDb) >= 0)
                    return;

                if (crVersion is null || crVersion.CompareTo(Version.Parse("1.0.1.686")) < 0)
                {
                    string dbString = $"UPDATE {Server.Tbl_QLVT_Nhapvattu} SET TenKhoNhap = NULL;\r\n" +
                        $"UPDATE {Server.Tbl_QLVT_XuatVatTu} SET TenKhoNhap = NULL;\r\n" +
                        $"UPDATE {Server.Tbl_QLVT_ChuyenKhoVatTu} SET TenKhoChuyenDen = NULL, TenKhoChuyenDi = NULL;\r\n";
                    DataProvider.InstanceTHDA.ExecuteNonQuery(dbString);

                }

                if (crVersion is null || crVersion.CompareTo(Version.Parse("1.0.1.325")) <= 0)
                {
                    string dbString = $"DELETE FROM {Server.Tbl_TDKH_KhoiLuongCongViecTungNgay} " +
                        $"WHERE Code IN\r\n" +
                        $"(" +
                        $"SELECT hn2.Code\r\n" +
                        $"FROM {Server.Tbl_TDKH_KhoiLuongCongViecTungNgay} hn1\r\n" +
                        $"JOIN {Server.Tbl_TDKH_KhoiLuongCongViecTungNgay} hn2\r\n" +
                        $"ON hn1.rowid < hn2.rowid AND hn1.CodeCongTacTheoGiaiDoan = hn2.CodeCongTacTheoGiaiDoan AND hn1.Ngay = hn2.Ngay" +
                        $")";
                    DataProvider.InstanceTHDA.ExecuteNonQuery(dbString);
                }

                if (crVersion is null || crVersion.CompareTo(Version.Parse("1.0.1.308")) <= 0)
                {
                    string dbString = $"UPDATE {Server.Tbl_BangNhanVien_FileDinhKem} SET Ngay = NULL";
                    DataProvider.InstanceTHDA.ExecuteNonQuery(dbString);
                }

                    if (crVersion is null || crVersion.CompareTo(Version.Parse("1.0.1.283")) <= 0)
                {
                    string dbString = $"DELETE FROM {Server.Tbl_MTC_ChiTietNhatTrinh} WHERE CodeCongTacTheoGiaiDoan IS NULL AND CodeCongTacThuCong IS NULL";
                    DataProvider.InstanceTHDA.ExecuteNonQuery(dbString);
                }

                if (crVersion is null || crVersion.CompareTo(Version.Parse("1.0.1.259")) <= 0)
                {
                    string dbString = $@"UPDATE {Server.Tbl_TDKH_HaoPhiVatTu} SET CodeVatTu = NULL
WHERE CodeVatTu NOT IN 
(SELECT Code 
FROM {Server.Tbl_TDKH_KHVT_VatTu})";
                    DataProvider.InstanceTHDA.ExecuteNonQuery(dbString);
                }

                if (crVersion is null || crVersion.CompareTo(Version.Parse("1.0.1.196")) <= 0)
                {
                    foreach (string tbl in tblsMTC)
                    {
                        try
                        {
                            DataProvider.InstanceTHDA.ExecuteNonQuery($"DELETE FROM {tbl}");
                        }
                        catch (Exception ex)
                        {

                        }
                    }

                }

                if (crVersion is null || crVersion.CompareTo(Version.Parse("1.0.1.267")) <= 0)
                {



                    foreach (var item in Server.dicTblIndex)
                    {
                        string tbl = item.Key;
                        string colCode = (tbl == Server.Tbl_GiaoViec_CongViecCha) ? "CodeCongViecCha"
                        : (tbl == Server.Tbl_GiaoViec_CongViecCon) ? "CodeCongViecCon"
                        : "Code";

                        var whereStr = item.Value.Select(x => $"((tbl1.{x} IS NULL AND tbl2.{x} IS NULL ) OR tbl1.{x} = tbl2.{x})");

                        string dbString = $"DELETE FROM {tbl}\r\n" +
                            $"WHERE rowid IN\r\n" +
                            $"(" +
                            $"SELECT tbl2.rowid\r\n" +
                            $"FROM {tbl} tbl1\r\n" +
                            $"JOIN {tbl} tbl2\r\n" +
                            $"ON {string.Join(" AND ", whereStr)} AND tbl1.rowid < tbl2.rowid)";
                        DataProvider.InstanceTHDA.ExecuteNonQuery(dbString);
                    }
                }


                if (crVersion is null || crVersion.CompareTo(Version.Parse("1.0.1.222")) <= 0)
                {
                    DataProvider.InstanceTHDA.ExecuteNonQuery($"Update {Server.Tbl_MTC_DanhSachMay} SET NgayMuaMay = '{DateTime.Now.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE_WithTime)}'");
                }


                if (crVersion is null || crVersion.CompareTo(Version.Parse("1.0.1.204")) <= 0)
                {
                    var dt = DataProvider.InstanceTHDA.ExecuteQuery($"SELECT * FROM {Server.Tbl_ChamCong_BangNhanVien} WHERE TenNhanVien IS NULL");
                    DuAnHelper.DeleteDataRows(Server.Tbl_ChamCong_BangNhanVien, dt.AsEnumerable().Select(x => x["Code"].ToString()));
                }

                if (crVersion is null || crVersion.CompareTo(Version.Parse("1.0.1.220")) <= 0)
                {
                    DuAnHelper.DeleteDataRows(Server.Tbl_ChamCong_ChiTietPhuPhiNhanVien);
                }

                WaitFormHelper.ShowWaitForm($"File của bạn được tạo bới phiên bản phần mềm cũ ({crVersion}).\r\nĐang cập nhật lên phiên bản mới!");

                if (Directory.Exists(BaseFrom.m_TempFilePath))
                {
                    Directory.CreateDirectory(BaseFrom.m_TempFilePath);
                }

                string tempFileDb = Path.Combine(BaseFrom.m_TempFilePath, "db_QuanLyThiCong.sqlite3");

                if (File.Exists(tempFileDb))
                {
                    File.Delete(tempFileDb);
                }

                File.Copy(Path.Combine(BaseFrom.m_templatePath, "DuAnMau", "db_QuanLyThiCong.sqlite3"),
                    Path.Combine(BaseFrom.m_TempFilePath, "db_QuanLyThiCong.sqlite3"));

                DataProvider.InstanceTemp.changePath(tempFileDb);

                DataTable dtTable = DataProvider.InstanceTHDA
                                    .ExecuteQuery($"SELECT name FROM sqlite_schema WHERE type = 'table' ORDER BY name");

                string[] namesTblIndbCurrent = dtTable.AsEnumerable().Where(x => x[0].ToString() != "sqlite_sequence")
                               .Select(x => x[0].ToString()).ToArray();

                //var allData = new SQLiteAllDataViewModel();
                List<string> tbls = new List<string>() { "Tbl_ThongTinTHDA" };
                tbls.AddRange(Server.LS_CONST_TYPE_SERVER_TBL);
                int count = 0;
                int num = tbls.Count();
                foreach (string nameTbl in tbls)
                {
                    if (nameTbl == Server.Tbl_QLVT_YeuCauVatTu)
                    {

                    }

                    WaitFormHelper.ShowWaitForm($"File của bạn được tạo bới phiên bản phần mềm cũ {crVersion}.\r\n{nameTbl}\r\nĐang cập nhật lên phiên bản mới!\r\n" +
                        $"{MSETTING.Default.PathHienTai}\r\n" +
                        $"({++count}/{num})");


                    string colCode = (nameTbl == Server.Tbl_GiaoViec_CongViecCha) ? "CodeCongViecCha"
                                    : (nameTbl == Server.Tbl_GiaoViec_CongViecCon) ? "CodeCongViecCon"
                                    : "Code";

                    string nameDBSet = $"{nameTbl}s";
                    if (!namesTblIndbCurrent.Contains(nameTbl))
                    {
                        AlertShower.ShowInfo($"Không tìm thấy bảng {nameTbl} trong dữ liệu offline!");
                        continue;
                    }

                    List<string> whereCondition = new List<string>();

                    string dbString = $"SELECT * FROM {nameTbl}";

                    DataTable dt = DataProvider.InstanceTHDA.ExecuteQuery(dbString);
                    foreach (DataRow dr in dt.Rows)
                    {
                        dr.SetAdded();
                    }
                    try
                    {
                        DataProvider.InstanceTemp.UpdateDataTableFromSqliteSource(dt, nameTbl);
                    }
                    catch (Exception ex)
                    {
                        DataProvider.InstanceTemp.UpdateDataTableFromOtherSource(dt, nameTbl);

                    }

                    //allData.SetValueByPropName(nameDBSet, dt);
                }
                DataProvider.InstanceTemp.ExecuteNonQuery($"UPDATE Tbl_ThongTinTHDA SET {nameof(Tbl_ThongTinTHDA.LastMigrationVersion)} = '{Application.ProductVersion}'");

                string crFileDA = MSETTING.Default.PathHienTai;
                string crDir = Path.GetDirectoryName(crFileDA);
                string crFilenameWithourExtension = Path.GetFileNameWithoutExtension(crFileDA);
                string crExtension = Path.GetExtension(crFileDA);

                string newFile = Path.Combine(crDir, $"{crFilenameWithourExtension}_Backup_{DateTime.Now.ToString(MyConstant.CONST_DATE_FORMAT_FileSUffix)}{crExtension}");


                File.Copy(crFileDA, newFile, false);

                File.Copy(tempFileDb, Path.Combine(BaseFrom.m_FullTempathDA, "db_QuanLyThiCong.sqlite3"), overwrite: true);

                //WaitFormHelper.ShowWaitForm()

                TongHopHelper.fcn_saveDA(false);
                WaitFormHelper.CloseWaitForm();
                #endregion
            } catch (Exception ex)
            {
                WaitFormHelper.CloseWaitForm();

                MessageShower.ShowError("Lỗi cập nhật file cũ");
            }
        }
    }
}
