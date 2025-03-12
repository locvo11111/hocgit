using Dapper;
using DevExpress.CodeParser.VB;
using DevExpress.DataAccess.DataFederation;
using DevExpress.LookAndFeel;
using DevExpress.Spreadsheet;
using DevExpress.Utils.Extensions;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraPrinting;
using DevExpress.XtraScheduler.Drawing;
using DevExpress.XtraSpreadsheet.Model.History;
using Newtonsoft.Json;
using PhanMemQuanLyThiCong.Common.Constant;
using PhanMemQuanLyThiCong.Common.Enums;
using PhanMemQuanLyThiCong.Common.ViewModel;
using PhanMemQuanLyThiCong.Constant;
using PhanMemQuanLyThiCong.Controls;
using PhanMemQuanLyThiCong.Model;
using PhanMemQuanLyThiCong.Model.TDKH;
using PhanMemQuanLyThiCong.Model.ThuChiTamUng;
using PM360.Common.Helper;
using StackExchange.Profiling.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Contracts;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VChatCore.ViewModels.SyncSqlite;
using MSETTING = PhanMemQuanLyThiCong.Properties.Settings;

namespace PhanMemQuanLyThiCong.Common.Helper
{
    public static class DuAnHelper
    {
        static string[] tbls_Contractor = { Server.Tbl_ThongTinNhaThau, Server.Tbl_ThongTinNhaThauPhu, Server.Tbl_ThongTinToDoiThiCong };
        public static List<DonViThucHien> GetDonViThucHiens(Ctrl_DonViThucHienDuAn ctrlDVTH = null, bool NhaCungCap = false)
        {
            string codeDA = SharedControls.slke_ThongTinDuAn.EditValue as string;
            //ctrlDVTH.EditValue = null;
            string[] tbls =
            {
                MyConstant.TBL_THONGTINNHATHAU,
                MyConstant.TBL_THONGTINNHATHAUPHU,
                MyConstant.TBL_THONGTINTODOITHICONG,
            };
            if (NhaCungCap)
            {
                var lst = tbls.ToList();
                lst.Add(MyConstant.TBL_THONGTINNHACUNGCAP);
                tbls = lst.ToArray();
            }
            List<DonViThucHien> DVTHS = new List<DonViThucHien>();

            foreach (string tbl in tbls)
            {
                string dbString = $"SELECT * FROM \"{tbl}\" WHERE \"CodeDuAn\" = '{codeDA}'";
                DataTable dt = DataProvider.InstanceTHDA.ExecuteQuery(dbString);

                foreach (DataRow dr in dt.Rows)
                {
                    var newDVTH = new DonViThucHien()
                    {
                        Code = dr["Code"].ToString(),
                        CodeDuAn = codeDA,
                        Ten = dr["Ten"].ToString(),
                        Table = tbl,
                    };

                    if (dt.Columns.Contains("CodeTongThau"))
                    {
                        newDVTH.CodeTongThau = dr["CodeTongThau"].ToString();
                    }

                    DVTHS.Add(newDVTH);
                }
            }



            if (ctrlDVTH != null)
            {
                if (!BaseFrom.allPermission.HaveInitProjectPermission && !BaseFrom.allPermission.AllProjectThatUserIsAdmin.Contains(codeDA))
                {
                    var allDVTH = BaseFrom.allPermission.AllContractor;
                    DVTHS = DVTHS.Where(x => allDVTH.Contains(x.Code)).ToList();
                }
                ctrlDVTH.DataSource = DVTHS;

                if (ctrlDVTH == SharedControls.ctrl_DonViThucHienDuAnTDKH)
                {
                    SharedControls.ctrl_DonViThucHienGiaoViec.DataSource = DVTHS.Where(x => x.LoaiDVTH != null).ToList();
                }

            }

            return DVTHS;
        }
        public static List<InForToChuc_CaNhan> GetCaNhanToChuc(bool NCC = true, bool NV = true, bool NTP = false)
        {
            string codeDA = SharedControls.slke_ThongTinDuAn.EditValue as string;

            string[] tbls =
            {
                MyConstant.TBL_THONGTINNHATHAU,
                MyConstant.TBL_THONGTINNHATHAUPHU,
                MyConstant.TBL_THONGTINTODOITHICONG,
                MyConstant.TBL_THONGTINNHACUNGCAP,
            };
            if (NTP)
            {
                int index = Array.IndexOf(tbls, MyConstant.TBL_THONGTINNHATHAU);
                tbls = tbls.Where((e, i) => i != index).ToArray();
            }
            List<InForToChuc_CaNhan> DVTHS = new List<InForToChuc_CaNhan>();
            string dbString = "";
            foreach (string tbl in tbls)
            {
                if (!NCC && tbl == MyConstant.TBL_THONGTINNHACUNGCAP)
                    continue;
                dbString = $"SELECT \"Code\", \"Ten\" FROM \"{tbl}\" WHERE \"CodeDuAn\" = '{codeDA}'";
                DataTable dt = DataProvider.InstanceTHDA.ExecuteQuery(dbString);

                foreach (DataRow dr in dt.Rows)
                {
                    DVTHS.Add(new InForToChuc_CaNhan()
                    {
                        Code = dr["Code"].ToString(),
                        Ten = dr["Ten"].ToString(),
                        Table = tbl,
                        CodeDuAn = codeDA,
                    });
                }
            }
            if (!NV)
                return DVTHS;
            dbString = $"SELECT \"Code\", \"TenNhanVien\" FROM {DanhSachNhanVienConstant.TBL_CHAMCONG_BANGNHANVIEN} ";
            DataTable dt_NV = DataProvider.InstanceTHDA.ExecuteQuery(dbString);

            foreach (DataRow dr in dt_NV.Rows)
            {
                DVTHS.Add(new InForToChuc_CaNhan()
                {
                    Code = dr["Code"].ToString(),
                    Ten = dr["TenNhanVien"].ToString(),

                });
            }
            return DVTHS;
        }
        public static List<T> ConvertToList<T>(DataTable dt)
        {
            var columnNames = dt.Columns.Cast<DataColumn>()
                    .Select(c => c.ColumnName)
                    .ToList();
            var properties = typeof(T).GetProperties();
            return dt.AsEnumerable().Select(row =>
            {
                var objT = Activator.CreateInstance<T>();
                foreach (var pro in properties)
                {
                    if (columnNames.Contains(pro.Name))
                    {
                        PropertyInfo pI = objT.GetType().GetProperty(pro.Name);
                        pro.SetValue(objT, row[pro.Name] == DBNull.Value ? null : Convert.ChangeType(row[pro.Name], pI.PropertyType));
                    }
                }
                return objT;
            }).ToList();
        }
        public class ListtoDataTableConverter
        {
            public DataTable ToDataTable<T>(BindingList<T> items)
            {
                DataTable dataTable = new DataTable(typeof(T).Name);
                //Get all the properties
                PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
                foreach (PropertyInfo prop in Props)
                {
                    //Setting column names as Property names
                    dataTable.Columns.Add(prop.Name);
                }
                foreach (T item in items)
                {
                    var values = new object[Props.Length];
                    for (int i = 0; i < Props.Length; i++)
                    {
                        //inserting property values to datatable rows
                        values[i] = Props[i].GetValue(item, null);
                    }
                    dataTable.Rows.Add(values);
                }
                //put a breakpoint here and check datatable
                return dataTable;
            }
            public DataTable ToDataTableList<T>(List<T> items)
            {
                DataTable dataTable = new DataTable(typeof(T).Name);
                //Get all the properties
                PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
                foreach (PropertyInfo prop in Props)
                {
                    //Setting column names as Property names
                    dataTable.Columns.Add(prop.Name);
                }
                foreach (T item in items)
                {
                    var values = new object[Props.Length];
                    for (int i = 0; i < Props.Length; i++)
                    {
                        //inserting property values to datatable rows
                        values[i] = Props[i].GetValue(item, null);
                    }
                    dataTable.Rows.Add(values);
                }
                //put a breakpoint here and check datatable
                return dataTable;
            }
        }
        public static void fcn_GetDtCongTrinhHangMuc(out DataTable dtCongTrinh, out DataTable dtHangMuc, string TBL_CT, string TBL_HM, bool All)
        {
            string dbString = $"SELECT * FROM {TBL_CT} WHERE \"CodeDuAn\" = '{SharedControls.slke_ThongTinDuAn.EditValue}' ORDER BY \"CreatedOn\" ASC";
            if (All)
                dbString = $"SELECT * FROM {TBL_CT} ORDER BY \"CreatedOn\" ASC ";
            dtCongTrinh = DataProvider.InstanceTHDA.ExecuteQuery(dbString);


            string lsCodeCT = MyFunction.fcn_Array2listQueryCondition(dtCongTrinh.AsEnumerable().Select(x => x["Code"].ToString()).ToArray());

            dbString = $"SELECT * FROM {TBL_HM} WHERE \"CodeCongTrinh\" IN ({lsCodeCT}) ORDER BY \"CreatedOn\" ASC";
            dtHangMuc = DataProvider.InstanceTHDA.ExecuteQuery(dbString);

        }

        public static List<Provinces> GetAllProvinces()
        {
            string dbString = $"SELECT * FROM {MyConstant.TBL_Provinces}";
            List<Provinces> ps = DataProvider.InstanceTBT
                .ExecuteQueryModel<Provinces>(dbString);

            return ps;
        }

        public static List<Department> GetAllDepartments()
        {
            string dbString = $"SELECT * FROM {MyConstant.TBL_Department}";
            List<Department> ps = DataProvider.InstanceTBT
                .ExecuteQueryModel<Department>(dbString);
            return ps;
        }

        public static List<CategoryDinhMuc> GetAllCategoryDinhMuc()
        {
            string dbString = $"SELECT * FROM {MyConstant.TBL_DinhMuc}";
            List<CategoryDinhMuc> ps = DataProvider.InstanceTBT
                .ExecuteQueryModel<CategoryDinhMuc>(dbString);
            return ps;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fstTbl"></param>
        /// <param name="ids"></param>
        /// <param name="AddToDeletedTable">
        /// Khi add vào bảng deleted thì sẽ bị xóa kèm trên server khi cập nhật dự án lên
        /// </param>
        /// <returns></returns>
        public static bool DeleteDataRows(string fstTbl, IEnumerable<string> ids = null, bool AddToDeletedTable = true)
        {
            try
            {
                var allTbls = Server.LS_CONST_TYPE_SERVER_TBL;
                int indTbl = Array.IndexOf(allTbls, fstTbl);
                if (indTbl < 0)
                {
                    MessageShower.ShowError("Bảng dữ liệu không tồn tại");
                    return false;
                }

                Dictionary<string, List<string>> dicTblCodes = new Dictionary<string, List<string>>();//{Table, Code}
                                                                                                      //SQLiteAllDataViewModel AllData = new SQLiteAllDataViewModel();
                Dictionary<string, DataTable> dicTblFile_dtDeleted = new Dictionary<string, DataTable>();
                string dbString = "";
                for (int i = indTbl; i < allTbls.Length; i++)
                {
                    string tbl = allTbls[i];
                    string colCode = (tbl == Server.Tbl_GiaoViec_CongViecCha) ? "CodeCongViecCha"
                                        : (tbl == Server.Tbl_GiaoViec_CongViecCon) ? "CodeCongViecCon"
                                        : "Code";



                    dbString = $"SELECT * FROM {tbl}";
                    List<string> whereCondition = new List<string>();


                    if (tbl != fstTbl)
                    {
                        Dictionary<string, string> lsFk = Server.dicFks[tbl];


                        string[] fks = lsFk.Where(x => dicTblCodes.ContainsKey(x.Value) && dicTblCodes[x.Value].Any())
                            .Select(x => $"{x.Key} IN ({MyFunction.fcn_Array2listQueryCondition(dicTblCodes[x.Value])})")
                            .ToArray();

                        if (fks.Any())
                            whereCondition.Add($"({string.Join(" OR ", fks)})");
                        else if (!lsFk.Any(x => dicTblCodes.ContainsKey(x.Value)) || lsFk.Any(x => dicTblCodes.ContainsKey(x.Value) && !dicTblCodes[x.Value].Any()))
                            continue;

                    }
                    else
                    {
                        if (ids != null)
                            whereCondition.Add($"{colCode} IN ({MyFunction.fcn_Array2listQueryCondition(ids)})");//IN ({SQLRawHelper.fcn_Array2listQueryCondition(allPermission.AllProject.ToArray())})";
                    }



                    if (whereCondition.Any())
                    {
                        dbString += $" WHERE {string.Join(" AND ", whereCondition)}";
                    }
                    else if (tbl != fstTbl)
                        continue;


                    DataTable dt = DataProvider.InstanceTHDA.ExecuteQuery(dbString);

                    if (Server.tblsFileDinhKem.ContainsKey(tbl))
                    {
                        dicTblFile_dtDeleted.Add(tbl, dt);
                    }

                    if (!dicTblCodes.ContainsKey(tbl))
                        dicTblCodes[tbl] = new List<string>() { };

                    var codes = dt.AsEnumerable().Select(x => x[colCode].ToString());
                    dicTblCodes[tbl].AddRange(codes);

                    //Setnull các code nhóm;
                    if (Server.dicSetNull.ContainsKey(tbl))
                    {
                        //var tbl = item.Key;
                        var kp = Server.dicSetNull[tbl];
                        if (dicTblCodes.ContainsKey(tbl))
                        {
                            foreach (var item in kp)
                            {
                                string con = MyFunction.fcn_Array2listQueryCondition(dicTblCodes[tbl].ToArray());

                                dbString = $"UPDATE {item.Key} SET {item.Value} = NULL WHERE {item.Value} IN ({con})";
                                DataProvider.InstanceTHDA.ExecuteNonQuery(dbString);
                            }
                        }
                    }
                }



                for (int i = allTbls.Length - 1; i >= indTbl; i--)
                {
                    string tbl = allTbls[i];
                    Type ModelSQL = Type.GetType($"VChatCore.Model.SyncSqlite.{tbl}");

                    string nameDBSet = $"{tbl}s";
                    string colCode = (tbl == Server.Tbl_GiaoViec_CongViecCha) ? "CodeCongViecCha"
                                        : (tbl == Server.Tbl_GiaoViec_CongViecCon) ? "CodeCongViecCon"
                                        : "Code";
                    if (!dicTblCodes.ContainsKey(tbl) || !dicTblCodes[tbl].Any())
                        continue;

                    string con = MyFunction.fcn_Array2listQueryCondition(dicTblCodes[tbl].ToArray());
                    dbString = $"DELETE FROM {tbl} WHERE {colCode} IN ({con})";

                    int num = DataProvider.InstanceTHDA.ExecuteNonQuery(dbString, AddToDeletedDataTable: AddToDeletedTable);
                }
                foreach (var item in Server.tblsFileDinhKem)
                {
                    string tblFile = item.Key;
                    string tblParentFile = item.Value;
                    if (!dicTblCodes.ContainsKey(tblParentFile))
                        continue;

                    foreach (string code in dicTblCodes[tblParentFile])
                    {
                        if (string.IsNullOrEmpty(code))
                            continue;
                        string dir = Path.Combine(BaseFrom.m_FullTempathDA,
                            string.Format(CusFilePath.SQLiteFile, tblFile, code));

                        if (Directory.Exists(dir))
                            Directory.Delete(dir, true);
                    }

                    if (!dicTblCodes.ContainsKey(tblFile))
                        continue;

                    foreach (string code in dicTblCodes[tblFile])
                    {
                        if (!dicTblFile_dtDeleted.ContainsKey(tblFile) || string.IsNullOrEmpty(code))
                            continue;

                        DataTable dtFile = dicTblFile_dtDeleted[tblFile];
                        DataRow dr = dtFile.AsEnumerable().Single(x => x["Code"].ToString() == code);
                        string dir = Path.Combine(BaseFrom.m_FullTempathDA, string.Format(CusFilePath.SQLiteFile, tblFile, dr["CodeParent"].ToString()));

                        if (!Directory.Exists(dir))
                            continue;

                        string fullFilePath = Path.Combine(dir, code);

                        if (File.Exists(fullFilePath))
                        {
                            File.Delete(fullFilePath);
                        }

                    }
                }

            }
            catch (Exception ex)
            {
                MessageShower.ShowError($"Lỗi xóa data: {ex.Message}__{ex.InnerException?.Message}");
                return false;
            }
            //MessageShower.ShowInformation("Xóa dữ liệu thành công");
            return true;

        }

        public static bool UpdateDataRows(string fstTbl, Dictionary<string, string> ids = null, bool AddToDeletedTable = true)
        {
            //DataProvider.InstanceTHDA.ExecuteNonQuery("PRAGMA foreign_keys = 0");
            try
            {
                var allTbls = Server.LS_CONST_TYPE_SERVER_TBL;
                int indTbl = Array.IndexOf(allTbls, fstTbl);
                if (indTbl < 0)
                {
                    MessageShower.ShowError("Bảng dữ liệu không tồn tại");
                    return false;
                }

                Dictionary<string, Dictionary<string, string>> dicTblCodes = new Dictionary<string, Dictionary<string, string>>();//{Table, Code}
                                                                                                                                  //SQLiteAllDataViewModel AllData = new SQLiteAllDataViewModel();
                Dictionary<string, DataTable> dicTblFile_dtDeleted = new Dictionary<string, DataTable>();
                string dbString = "";

                var tbls = Server.dicFks.Where(x => x.Value.Values.Contains(fstTbl)).Select(x => x.Key).ToList();

                List<string> queries = new List<string>();
                for (int i = indTbl; i < allTbls.Length; i++)
                {
                    string tbl = allTbls[i];

                    string colCode = (tbl == Server.Tbl_GiaoViec_CongViecCha) ? "CodeCongViecCha"
                                        : (tbl == Server.Tbl_GiaoViec_CongViecCon) ? "CodeCongViecCon"
                                        : "Code";

                    dbString = $"SELECT * FROM {tbl}";
                    List<string> whereCondition = new List<string>();

                    if (tbl != fstTbl)
                    {
                        if (!tbls.Contains(tbl))
                            continue;

                        var colFks = Server.dicFks[tbl].Where(x => x.Value == fstTbl).Select(x => x.Key);

                        foreach (string colFk in colFks)
                        {

                            var qr = $"SELECT * FROM {tbl} WHERE {colFk} IN ({MyFunction.fcn_Array2listQueryCondition(ids.Keys)})";
                            DataTable dtQr = DataProvider.InstanceTHDA.ExecuteQuery(qr);

                            var grs = dtQr.AsEnumerable().GroupBy(x => x[colFk].ToString());

                            foreach (var gr in grs)
                            {

                                queries.Add($"UPDATE {tbl} SET {colFk} = '{ids[gr.Key]}' WHERE {colCode} IN ({MyFunction.fcn_Array2listQueryCondition(gr.Select(x => x[colCode].ToString()))})");
                            }

                            qr = $"UPDATE {tbl} SET {colFk}  = NULL WHERE  {colFk} IN ({MyFunction.fcn_Array2listQueryCondition(ids.Keys)})";
                            DataProvider.InstanceTHDA.ExecuteNonQuery(qr);
                        }



                    }
                    else
                    {
                        //List<string> queries = new List<string>();
                        foreach (var item in ids)
                        {
                            queries.Add($"UPDATE {tbl} SET {colCode} = '{item.Value}' WHERE {colCode} = '{item.Key}'");
                        }
                        //DataProvider.InstanceTHDA.ExecuteNonQueryFromList(queries);

                        if (ids != null)
                            whereCondition.Add($"{colCode} IN ({MyFunction.fcn_Array2listQueryCondition(ids.Keys)})");//IN ({SQLRawHelper.fcn_Array2listQueryCondition(allPermission.AllProject.ToArray())})";
                    }



                    if (whereCondition.Any())
                    {
                        dbString += $" WHERE {string.Join(" AND ", whereCondition)}";
                    }
                    else if (tbl != fstTbl)
                        continue;


                    DataTable dt = DataProvider.InstanceTHDA.ExecuteQuery(dbString);

                    if (Server.tblsFileDinhKem.ContainsKey(tbl))
                    {
                        dicTblFile_dtDeleted.Add(tbl, dt);
                    }

                    if (!dicTblCodes.ContainsKey(tbl))
                        dicTblCodes[tbl] = new Dictionary<string, string>() { };

                    var codes = dt.AsEnumerable().Where(x => ids.ContainsKey(x[colCode].ToString())).ToDictionary(x => x[colCode].ToString(), x => ids[x[colCode].ToString()]);
                    dicTblCodes[tbl] = codes;

                    ////Setnull các code nhóm;
                    //if (Server.dicSetNull.ContainsKey(tbl))
                    //{
                    //    //var tbl = item.Key;
                    //    var kp = Server.dicSetNull[tbl];
                    //    if (dicTblCodes.ContainsKey(tbl))
                    //    {
                    //        foreach (var item in kp)
                    //        {
                    //            string con = MyFunction.fcn_Array2listQueryCondition(dicTblCodes[tbl].ToArray());

                    //            dbString = $"UPDATE {item.Key} SET {item.Value} = NULL WHERE {item.Value} IN ({con})";
                    //            DataProvider.InstanceTHDA.ExecuteNonQuery(dbString);
                    //        }
                    //    }
                    //}
                }
                DataProvider.InstanceTHDA.ExecuteNonQueryFromList(queries);



                /*                for (int i = allTbls.Length - 1; i >= indTbl; i--)
                                {
                                    string tbl = allTbls[i];
                                    Type ModelSQL = Type.GetType($"VChatCore.Model.SyncSqlite.{tbl}");

                                    string nameDBSet = $"{tbl}s";
                                    string colCode = (tbl == Server.Tbl_GiaoViec_CongViecCha) ? "CodeCongViecCha"
                                                        : (tbl == Server.Tbl_GiaoViec_CongViecCon) ? "CodeCongViecCon"
                                                        : "Code";
                                    if (!dicTblCodes.ContainsKey(tbl) || !dicTblCodes[tbl].Any())
                                        continue;

                                    string con = MyFunction.fcn_Array2listQueryCondition(dicTblCodes[tbl].Keys.ToArray());
                                    dbString = $"UPDATE {tbl} SET WHERE {colCode} IN ({con})";

                                    int num = DataProvider.InstanceTHDA.ExecuteNonQuery(dbString, AddToDeletedDataTable: AddToDeletedTable);
                                }*/
                foreach (var item in Server.tblsFileDinhKem)
                {
                    string tblFile = item.Key;
                    string tblParentFile = item.Value;
                    if (!dicTblCodes.ContainsKey(tblParentFile))
                        continue;

                    foreach (var code in dicTblCodes[tblParentFile])
                    {
                        if (string.IsNullOrEmpty(code.Key))
                            continue;
                        string dir = Path.Combine(BaseFrom.m_FullTempathDA,
                            string.Format(CusFilePath.SQLiteFile, tblFile, code.Key));

                        string newdir = Path.Combine(BaseFrom.m_FullTempathDA,
                            string.Format(CusFilePath.SQLiteFile, tblFile, code.Value));

                        if (Directory.Exists(dir))
                            Directory.Move(dir, newdir);
                    }

                    if (!dicTblCodes.ContainsKey(tblFile))
                        continue;

                    foreach (var code in dicTblCodes[tblFile])
                    {
                        if (!dicTblFile_dtDeleted.ContainsKey(tblFile) || string.IsNullOrEmpty(code.Key))
                            continue;

                        DataTable dtFile = dicTblFile_dtDeleted[tblFile];
                        DataRow dr = dtFile.AsEnumerable().Single(x => x["Code"].ToString() == code.Key);
                        string dir = Path.Combine(BaseFrom.m_FullTempathDA, string.Format(CusFilePath.SQLiteFile, tblFile, dr["CodeParent"].ToString()));

                        if (!Directory.Exists(dir))
                            continue;

                        string fullFilePath = Path.Combine(dir, code.Key);
                        string newfullFilePath = Path.Combine(dir, code.Value);

                        if (File.Exists(fullFilePath))
                        {
                            File.Move(fullFilePath, newfullFilePath);
                        }

                    }
                }

            }
            catch (Exception ex)
            {
                MessageShower.ShowError($"Lỗi Cập nhật dữ liệu: {ex.Message}__{ex.InnerException?.Message}");
                //DataProvider.InstanceTHDA.ExecuteNonQuery("PRAGMA foreign_keys = 1");

                return false;
            }
            //DataProvider.InstanceTHDA.ExecuteNonQuery("PRAGMA foreign_keys = 1");

            //MessageShower.ShowInformation("Xóa dữ liệu thành công");
            return true;

        }

        public static List<Tbl_ThongTinNhaCungCapViewModel> GetAllNhaCungCapOfCurrentPrj()
        {
            string dbString = $"SELECT * FROM {MyConstant.TBL_THONGTINNHACUNGCAP} WHERE CodeDuAn = '{SharedControls.slke_ThongTinDuAn.EditValue}'";
            return DataProvider.InstanceTHDA.ExecuteQueryModel<Tbl_ThongTinNhaCungCapViewModel>(dbString);
        }

        public static List<KLTTHangNgay> GetNCCDetail(KLTTHangNgay data, Dictionary<string, string> dicNCC = null)
        {
            var lsCon = new List<KLTTHangNgay>();
            var NccsVM = MyFunction.TryGetObjFromJson<NhaCungCapHangNgayViewModel>(data.NhaCungCap);
            var AllCrCodeNCC = NccsVM.Select(x => x.Code);
            data.CodesNCCString = string.Join(", ", NccsVM);

            if (!NccsVM.Any())
                return lsCon;

            if (dicNCC is null)
            {
                var nccs = DuAnHelper.GetAllNhaCungCapOfCurrentPrj();
                dicNCC = nccs.ToDictionary(x => x.Code, x => x.Ten);
            }
            foreach (var ncc in NccsVM)
            {
                lsCon.Add(new KLTTHangNgay()
                {
                    ParentCode = data.Code,
                    TenCongTac = (dicNCC.ContainsKey(ncc.Code)) ? dicNCC[ncc.Code] : "",
                    Code = Guid.NewGuid().ToString(),
                    CodeNhaCungCap = ncc.Code,
                    KhoiLuongThiCong = ncc.KhoiLuong,
                    DonGiaThiCong = ncc.DonGia
                });
            }

            lsCon.Add(new KLTTHangNgay()
            {
                ParentCode = data.Code,
                TenCongTac = "Còn lại",
                Code = data.Code + "_",
                KhoiLuongThiCong = data.KhoiLuongThiCong - lsCon.Sum(x => x.KhoiLuongThiCong) ?? 0,
                DonGiaThiCong = data.DonGiaThiCong
            });
            return lsCon;
        }

        public static void updateNCC(RepositoryItemCheckedComboBoxEdit rpCbe_NCC, KLTTHangNgay row)
        {
            string newVal = rpCbe_NCC.GetCheckedItems() as string;
            string[] newNccs = newVal.Split(',').Where(x => x.HasValue()).Select(x => x.Trim()).ToArray();

            var crKLHN = row;

            var AllCrNCC = MyFunction.TryGetObjFromJson<NhaCungCapHangNgayViewModel>(crKLHN.NhaCungCap);
            var AllCrCodeNCC = AllCrNCC.Select(x => x.Code);

            foreach (var ncc in AllCrNCC.ToArray())
            {
                if (!newNccs.Contains(ncc.Code))
                {
                    AllCrNCC.Remove(ncc);
                    continue;
                }
            }

            foreach (string codeNewNCC in newNccs)
            {
                if (!AllCrCodeNCC.Contains(codeNewNCC))
                    AllCrNCC.Add(new NhaCungCapHangNgayViewModel()
                    {
                        Code = codeNewNCC
                    });
            }

            string JsonNCC = JsonConvert.SerializeObject(AllCrNCC);

            crKLHN.NhaCungCap = JsonNCC;
            //tl_CongTacVatLieu.RefreshDataSource();
        }

        public static List<CongTac> GetCurrentCongTacByHMs(IEnumerable<string> hms, string colfk)
        {
            var DVTH = SharedControls.ctrl_DonViThucHienDuAnTDKH.SelectedDVTH;
            string dbString = $"SELECT cttk.*, dmct.MaHieuCongTac, nct.KhoiLuongKeHoach AS KhoiLuongNhom, " +
                    $"CAST(ROUND(TOTAL(hp.HeSoNguoiDung*hp.DinhMucNguoiDung)*cttk.KhoiLuongToanBo, 0) AS INT) AS TongNhanCong " +
                    $"FROM {TDKH.TBL_ChiTietCongTacTheoKy} cttk\r\n" +
                    $"JOIN {TDKH.TBL_DanhMucCongTac} dmct\r\n" +
                    $"ON cttk.CodeCongTac = dmct.Code\r\n" +
                    $"JOIN {MyConstant.TBL_THONGTINHANGMUC} hm\r\n" +
                    $"ON dmct.CodeHangMuc = hm.Code\r\n" +

                    $"LEFT JOIN {TDKH.Tbl_HaoPhiVatTu} hp\r\n" +
                    $"ON hp.CodeCongTac = cttk.Code AND hp.LoaiVatTu = 'Nhân công'\r\n" +
                    $"LEFT JOIN {TDKH.TBL_NhomCongTac} nct\r\n" +
                    $"ON cttk.CodeNhom = nct.Code\r\n" +

                    $"WHERE CodeGiaiDoan = '{SharedControls.cbb_DBKH_ChonDot.SelectedValue}' " +
                    $"AND {DVTH.ColCodeFK} = '{DVTH.Code}' " +
                    $"AND {colfk} IN ({MyFunction.fcn_Array2listQueryCondition(hms)})" +
            $"GROUP BY cttk.Code\r\n" +
            $"ORDER BY cttk.SortId";
            return DataProvider.InstanceTHDA.ExecuteQueryModel<CongTac>(dbString);
        }

        public static DataTable GetCurrentCongTacByHMsDataTable(IEnumerable<string> hms)
        {
            var DVTH = SharedControls.ctrl_DonViThucHienDuAnTDKH.SelectedDVTH;
            string dbString = $"SELECT cttk.*, dmct.MaHieuCongTac, " +
                    $"ROUND(TOTAL(hp.HeSoNguoiDung*hp.DinhMucNguoiDung)*cttk.KhoiLuongToanBo, 0) AS TongNhanCong " +
                    $"FROM {TDKH.TBL_ChiTietCongTacTheoKy} cttk\r\n" +
                    $"JOIN {TDKH.TBL_DanhMucCongTac} dmct\r\n" +
                    $"ON cttk.CodeCongTac = dmct.Code\r\n" +

                    $"LEFT JOIN {TDKH.Tbl_HaoPhiVatTu} hp\r\n" +
                    $"ON hp.CodeCongTac = cttk.Code AND hp.LoaiVatTu = 'Nhân công'\r\n" +

                    $"WHERE CodeGiaiDoan = '{SharedControls.cbb_DBKH_ChonDot.SelectedValue}' " +
                    $"AND {DVTH.ColCodeFK} = '{DVTH.Code}' " +
                    $"AND COALESCE(cttk.CodeHangMuc, dmct.CodeHangMuc) IN ({MyFunction.fcn_Array2listQueryCondition(hms)})" +
                    $"GROUP BY cttk.Code\r\n" +
                    $"ORDER BY cttk.SortId";
            return DataProvider.InstanceTHDA.ExecuteQuery(dbString);
        }

        public static void UpdateNBDKTThiCongCongTac(string code)
        {
            var wb = SharedControls.spsheet_TD_KH_LapKeHoach.Document;
            var ws = wb.Worksheets[TDKH.SheetName_KeHoachKinhPhi];
            Dictionary<string, string> dic = MyFunction.fcn_getDicOfColumn(ws.GetUsedRange());

            var search = ws.Columns[dic[TDKH.COL_Code]].Search(code, MyConstant.MySearchOptions);
            if (!search.Any())
                return;

            SharedControls.np_KhoiLuongHangNgay.State = DevExpress.XtraBars.Navigation.NavigationPaneState.Collapsed;

            var crRow = ws.Rows[search.Single().RowIndex];

            string dbString = $"SELECT MIN(Ngay) AS NgayBatDauThiCong, Max(Ngay) AS NgayKetThucThiCong " +
                $"FROM {TDKH.TBL_ChiTietCongTacTheoKy} cttk " +
                $"LEFT JOIN {TDKH.TBL_KhoiLuongCongViecHangNgay} hn " +
                $"ON hn.CodeCongTacTheoGiaiDoan = cttk.Code " +
                $"WHERE cttk.Code = '{code}' " +
                $"GROUP BY cttk.Code";

            DataTable dt = DataProvider.InstanceTHDA.ExecuteQuery(dbString);
            DataRow dr = dt.Rows[0];

            crRow[dic[TDKH.COL_NgayBatDauThiCong]].SetValue(dr[0]);
            crRow[dic[TDKH.COL_NgayKetThucThiCong]].SetValue(dr[1]);

        }
        public static void UpdateStateTDKHNhomByKhoiLuongThiCong(IEnumerable<string> lsCtac = null)
        {
            DataTable dtnhom;
            string dbstring = string.Empty;
            if (lsCtac?.Any() == true)
            {
                dbstring = $"SELECT Nhom.*,\r\n" +
                     $"TOTAL(KhoiLuongThiCong) AS KhoiLuongDaThiCongNhom,\r\n" +
                     $"MIN(Ngay) AS MinNgayThiCong, MAX(Ngay) AS MaxNgayThiCong\r\n" +
                    $"FROM {TDKH.TBL_NhomCongTac} Nhom\r\n" +
                    $" JOIN {TDKH.TBL_ChiTietCongTacTheoKy} cttk \r\n" +
                    $"ON cttk.CodeNhom = Nhom.Code AND cttk.CodeNhaThau IS NULL AND cttk.CodeGiaiDoan='{SharedControls.cbb_DBKH_ChonDot.SelectedValue}'\r\n" +
                    $"LEFT JOIN {TDKH.TBL_KhoiLuongCongViecHangNgay} klhn\r\n" +
                    $"ON Nhom.Code = klhn.CodeNhom\r\n" +
                    $"AND klhn.KhoiLuongThiCong IS NOT NULL\r\n" +
                    $"WHERE Nhom.Code IN ({MyFunction.fcn_Array2listQueryCondition(lsCtac)}) GROUP BY Nhom.Code";
                dtnhom = DataProvider.InstanceTHDA.ExecuteQuery(dbstring);
                //if (dtnhom.Rows.Count == 0)
                //{
                //    dbstring = $"SELECT Nhom.*,\r\n" +
                //    $"TOTAL(KhoiLuongThiCong) AS KhoiLuongDaThiCongNhom,\r\n" +
                //    $"MIN(Ngay) AS MinNgayThiCong, MAX(Ngay) AS MaxNgayThiCong\r\n" +
                //    $"FROM {TDKH.TBL_NhomCongTac} Nhom\r\n" +
                //    $"LEFT JOIN {TDKH.TBL_NhomCongTac} Nhom1\r\n" +
                //    $"ON Nhom1.CodeNhomGiaoThau = Nhom.Code\r\n" +
                //    $" JOIN {TDKH.TBL_ChiTietCongTacTheoKy} cttk\r\n" +
                //    $"ON cttk.CodeNhom = Nhom1.Code AND cttk.CodeNhaThau IS NULL \r\n" +
                //    $"LEFT JOIN {TDKH.TBL_KhoiLuongCongViecHangNgay} klhn\r\n" +
                //    $"ON Nhom1.Code = klhn.CodeNhom\r\n" +
                //    $"AND klhn.KhoiLuongThiCong IS NOT NULL\r\n" +
                //    $"WHERE Nhom.Code IN ({MyFunction.fcn_Array2listQueryCondition(lsCtac)}) GROUP BY Nhom.Code";
                //    dtnhom = DataProvider.InstanceTHDA.ExecuteQuery(dbstring);
                //}
            }
            else
            {
                dbstring = $"SELECT Nhom.*,\r\n" +
                $"TOTAL(KhoiLuongThiCong) AS KhoiLuongDaThiCongNhom,\r\n" +
                $"MIN(Ngay) AS MinNgayThiCong, MAX(Ngay) AS MaxNgayThiCong\r\n" +
                $"FROM {TDKH.TBL_NhomCongTac} Nhom\r\n" +
               $" JOIN {TDKH.TBL_ChiTietCongTacTheoKy} cttk \r\n" +
               $"ON cttk.CodeNhom = Nhom.Code AND cttk.CodeNhaThau IS NULL AND cttk.CodeGiaiDoan='{SharedControls.cbb_DBKH_ChonDot.SelectedValue}' \r\n" +
               $"LEFT JOIN {TDKH.TBL_KhoiLuongCongViecHangNgay} klhn\r\n" +
               $"ON Nhom.Code = klhn.CodeNhom\r\n" +
              $"AND klhn.KhoiLuongThiCong IS NOT NULL\r\n" +
               $" GROUP BY Nhom.Code";
                dtnhom = DataProvider.InstanceTHDA.ExecuteQuery(dbstring);
            }
            foreach (DataRow dr in dtnhom.Rows)
            {
                if (dr["KhoiLuongDaThiCongNhom"] == DBNull.Value)
                    continue;
                double KLDaThiCong = double.Parse(dr["KhoiLuongDaThiCongNhom"].ToString());
                double KLKH = double.Parse(dr["KhoiLuongKeHoach"].ToString());
                if (dr["TrangThai"] == DBNull.Value)
                    dr["TrangThai"] = "Chưa thực hiện";
                string crState = (string)dr["TrangThai"];
                double tyle = 0;
                if (KLKH > 0)
                {
                    tyle = Math.Round(KLDaThiCong / KLKH, 2);
                }
                else if (KLDaThiCong == 0)
                    tyle = 1;

                if (KLDaThiCong > 0)
                {
                    if (tyle == 1)
                        dr["TrangThai"] = TrangThai.HoanThanh.GetEnumDisplayName();
                    else if (crState == TrangThai.ChuaThucHien.GetEnumDisplayName())
                        dr["TrangThai"] = TrangThai.DangThucHien.GetEnumDisplayName();
                }
                else if (KLDaThiCong <= 0 && crState == TrangThai.DangThucHien.GetEnumDisplayName())
                    dr["TrangThai"] = TrangThai.ChuaThucHien.GetEnumDisplayName();
                else continue;
            }
            DataProvider.InstanceTHDA.UpdateDataTableFromSqliteSource(dtnhom, TDKH.TBL_NhomCongTac);
            string[] codesGiaoThau = dtnhom.AsEnumerable().Select(x => x["CodeNhomGiaoThau"].ToString()).ToArray();

            if (!codesGiaoThau.Any())
                return;
            dbstring = $"SELECT Nhom.*,\r\n" +
                   $"TOTAL(KhoiLuongThiCong) AS KhoiLuongDaThiCongNhom,\r\n" +
                   $"MIN(Ngay) AS MinNgayThiCong, MAX(Ngay) AS MaxNgayThiCong\r\n" +
                   $"FROM {TDKH.TBL_NhomCongTac} Nhom\r\n" +
                   $"LEFT JOIN {TDKH.TBL_NhomCongTac} Nhom1\r\n" +
                   $"ON Nhom1.CodeNhomGiaoThau = Nhom.Code\r\n" +
                   $" JOIN {TDKH.TBL_ChiTietCongTacTheoKy} cttk\r\n" +
                   $"ON cttk.CodeNhom = Nhom1.Code AND cttk.CodeNhaThau IS NULL AND cttk.CodeGiaiDoan='{SharedControls.cbb_DBKH_ChonDot.SelectedValue}' \r\n" +
                   $"LEFT JOIN {TDKH.TBL_KhoiLuongCongViecHangNgay} klhn\r\n" +
                   $"ON Nhom1.Code = klhn.CodeNhom\r\n" +
                   $"AND klhn.KhoiLuongThiCong IS NOT NULL\r\n" +
                   $"WHERE Nhom.Code IN ({MyFunction.fcn_Array2listQueryCondition(codesGiaoThau)}) GROUP BY Nhom.Code";
            dtnhom = DataProvider.InstanceTHDA.ExecuteQuery(dbstring);
            foreach (DataRow dr in dtnhom.Rows)
            {
                if (dr["KhoiLuongDaThiCongNhom"] == DBNull.Value)
                    continue;
                double KLDaThiCong = double.Parse(dr["KhoiLuongDaThiCongNhom"].ToString());
                if (dr["TrangThai"] == DBNull.Value)
                    dr["TrangThai"] = "Chưa thực hiện";
                string crState = (string)dr["TrangThai"];

                if (KLDaThiCong > 0 && crState == TrangThai.ChuaThucHien.GetEnumDisplayName())
                {
                    dr["TrangThai"] = TrangThai.DangThucHien.GetEnumDisplayName();
                }
                else if (KLDaThiCong <= 0 && crState == TrangThai.DangThucHien.GetEnumDisplayName())
                    dr["TrangThai"] = TrangThai.ChuaThucHien.GetEnumDisplayName();
                else continue;
            }
            DataProvider.InstanceTHDA.UpdateDataTableFromSqliteSource(dtnhom, TDKH.TBL_NhomCongTac);
        }
        public static void UpdateStateTDKHByKhoiLuongThiCong(IEnumerable<string> lsCtac = null)
        {
            DataTable dtCttk;
            var crDVTH = SharedControls.ctrl_DonViThucHienDuAnTDKH.SelectedDVTH;

            if (lsCtac?.Any() == true)
            {
                var dbString1 = $"SELECT cttk2.Code\r\n" +
                    $"FROM {Server.Tbl_TDKH_ChiTietCongTacTheoGiaiDoan} cttk\r\n" +
                    $"JOIN {Server.Tbl_TDKH_ChiTietCongTacTheoGiaiDoan} cttk2\r\n" +
                    $"ON cttk.CodeCongTac = cttk2.CodeCongTac AND cttk.CodeNhaThau IS NULL AND cttk2.CodeNhaThau IS NOT NULL\r\n" +
                    $"WHERE cttk.Code IN ({MyFunction.fcn_Array2listQueryCondition(lsCtac)})";

                var codeGiaoThaus = DataProvider.InstanceTHDA.ExecuteQuery(dbString1).AsEnumerable().Select(dr => dr[0].ToString()).ToArray();

                var codes = lsCtac.ToList();
                codes.AddRange(codeGiaoThaus);
                string lsCons = $"cttk.Code IN ({MyFunction.fcn_Array2listQueryCondition(codes)})";
                dtCttk = TDKHHelper.GetCongTacTheoKys(new string[] { lsCons });
            }
            else
            {
                dtCttk = TDKHHelper.GetCongTacTheoKys(new string[] { });
            }



            foreach (DataRow dr in dtCttk.Rows)
            {
                double KLDaThiCong = double.Parse(dr["KhoiLuongDaThiCong"].ToString());
                double KLKH = double.Parse(dr["KhoiLuongToanBo"].ToString());
                string crState = (string)dr["TrangThai"];
                double tyle = 0;

                if (KLKH > 0)
                {
                    tyle = Math.Round(KLDaThiCong / KLKH, 2);
                }
                else if (KLDaThiCong == 0)
                    tyle = 1;

                if (KLDaThiCong > 0)
                {
                    if (tyle == 1)
                        dr["TrangThai"] = TrangThai.HoanThanh.GetEnumDisplayName();
                    else if (crState == TrangThai.ChuaThucHien.GetEnumDisplayName())
                        dr["TrangThai"] = TrangThai.DangThucHien.GetEnumDisplayName();
                }
                else if (KLDaThiCong <= 0 && crState == TrangThai.DangThucHien.GetEnumDisplayName())
                    dr["TrangThai"] = TrangThai.ChuaThucHien.GetEnumDisplayName();
                else continue;

                DinhMucHelper.capNhatTrangThaiCacBang(dr["Code"].ToString(), SourceDataEnum.TDKH, (string)dr["TrangThai"]);
            }

            DataProvider.InstanceTHDA.UpdateDataTableFromSqliteSource(dtCttk, TDKH.TBL_ChiTietCongTacTheoKy);
            string[] codesDMCT = dtCttk.AsEnumerable().Select(x => x["CodeDanhMucCongTac"].ToString()).ToArray();

            if (!codesDMCT.Any())
                return;
            string dbString = $"SELECT cttk0.*, " +
                $"TOTAL(KhoiLuongThiCong) AS {TDKH.COL_KhoiLuongDaThiCong} " +
                $"FROM {TDKH.TBL_ChiTietCongTacTheoKy} cttk0 " +
                $"JOIN {TDKH.TBL_ChiTietCongTacTheoKy} cttk " +
                $"ON cttk0.CodeCongTac = cttk.CodeCongTac AND cttk0.CodeNhaThau IS NOT NULL AND cttk.CodeNhaThau IS NULL " +
                $"LEFT JOIN {TDKH.TBL_KhoiLuongCongViecHangNgay} hn " +
                $"ON cttk.Code = hn.CodeCongTacTheoGiaiDoan " +
                $"AND hn.KhoiLuongThiCong IS NOT NULL " +
                $"WHERE cttk0.CodeCongTac IN ({MyFunction.fcn_Array2listQueryCondition(codesDMCT)}) " +
                $"GROUP BY cttk0.CodeCongTac";

            DataTable dt = DataProvider.InstanceTHDA.ExecuteQuery(dbString);
            foreach (DataRow dr in dt.Rows)
            {
                double KLDaThiCong = double.Parse(dr["KhoiLuongDaThiCong"].ToString());
                string crState = (string)dr["TrangThai"];

                if (KLDaThiCong > 0 && crState == TrangThai.ChuaThucHien.GetEnumDisplayName())
                {
                    dr["TrangThai"] = TrangThai.DangThucHien.GetEnumDisplayName();
                }
                else if (KLDaThiCong <= 0 && crState == TrangThai.DangThucHien.GetEnumDisplayName())
                    dr["TrangThai"] = TrangThai.ChuaThucHien.GetEnumDisplayName();
                else continue;

                DinhMucHelper.capNhatTrangThaiCacBang(dr["Code"].ToString(), SourceDataEnum.TDKH, (string)dr["TrangThai"]);
            }
            DataProvider.InstanceTHDA.UpdateDataTableFromSqliteSource(dt, TDKH.TBL_ChiTietCongTacTheoKy);


        }
        public static void UpdateStateGiaoViecByKhoiLuongThiCong(IEnumerable<string> lsCtac = null)
        {
            string condition = (lsCtac?.Any() == true) ?
                $"WHERE cha.CodeCongViecCha IN ({MyFunction.fcn_Array2listQueryCondition(lsCtac)})"
                : "";

            string dbString = $"SELECT cha.*, " +
                $"TOTAL(hn.KhoiLuongThiCong) AS {TDKH.COL_KhoiLuongDaThiCong} " +
                $"FROM {GiaoViec.TBL_CONGVIECCHA} cha " +
                $"LEFT JOIN {TDKH.TBL_ChiTietCongTacTheoKy} cttk " +
                $"ON cha.CodeCongTacTheoGiaiDoan = cttk.Code " +
                $"LEFT JOIN {TDKH.TBL_KhoiLuongCongViecHangNgay} hn " +
                $"ON hn.CodeCongViecCha = cha.CodeCongViecCha OR hn.CodeCongTacTheoGiaiDoan = cttk.Code " +
                $"{condition}" +
                $"GROUP BY cha.CodeCongViecCha";



            DataTable dtCttk = DataProvider.InstanceTHDA.ExecuteQuery(dbString);
            List<string> lsUpdateDb = new List<string>();

            foreach (DataRow dr in dtCttk.Rows)
            {
                double KLDaThiCong = double.Parse(dr["KhoiLuongDaThiCong"].ToString());
                string crState = (string)dr["TrangThai"];

                if (KLDaThiCong > 0 && crState == TrangThai.ChuaThucHien.GetEnumDisplayName())
                    dr["TrangThai"] = TrangThai.DangThucHien.GetEnumDisplayName();
                else if (KLDaThiCong <= 0 && crState == TrangThai.DangThucHien.GetEnumDisplayName())
                    dr["TrangThai"] = TrangThai.ChuaThucHien.GetEnumDisplayName();
                else continue;

                if (dr["CodeCongTacTheoGiaiDoan"] != DBNull.Value)
                {
                    lsUpdateDb.Add($"UPDATE {TDKH.TBL_ChiTietCongTacTheoKy} " +
                                    $"SET TrangThai = '{dr["TrangThai"]}' " +
                                    $"WHERE Code = '{dr["CodeCongTacTheoGiaiDoan"]}'");

                    DinhMucHelper.capNhatTrangThaiCacBang(dr["CodeCongTacTheoGiaiDoan"].ToString(), SourceDataEnum.TDKH, (string)dr["TrangThai"]);
                    dr.AcceptChanges();
                    continue;
                }
                DinhMucHelper.capNhatTrangThaiCacBang(dr["CodeCongTacTheoGiaiDoan"].ToString(), SourceDataEnum.KHGV, (string)dr["TrangThai"]);

                DataProvider.InstanceTHDA.UpdateDataTableFromSqliteSource(dtCttk, GiaoViec.TBL_CONGVIECCHA);
                if (lsUpdateDb.Any())
                {
                    DataProvider.InstanceTHDA.ExecuteQuery(string.Join(";\r\n", lsUpdateDb));
                }
            }
        }
        public static void Fcn_DeleteCongTrinh()
        {
            DialogResult rs = MessageShower.ShowYesNoQuestion("Bạn có muốn xóa Công trình, Hạng mục không có công tác không!!!!!!");
            if (rs == DialogResult.No)
                return;
            WaitFormHelper.ShowWaitForm("Đang xóa công trình hạng mục không có công tác, Vui lòng chờ!");
            DuAnHelper.fcn_GetDtCongTrinhHangMuc(out DataTable dtCongTrinh, out DataTable dtHangMuc, MyConstant.TBL_THONGTINCONGTRINH, MyConstant.TBL_THONGTINHANGMUC, false);
            if (dtCongTrinh.Rows.Count > 1)
            {
                foreach (DataRow row in dtCongTrinh.Rows)
                {
                    string CodeHM = MyFunction.fcn_Array2listQueryCondition(dtHangMuc.AsEnumerable().Where(x => x["CodeCongTrinh"].ToString() == row["Code"].ToString()).Select(x => x["Code"].ToString()).ToArray());
                    string queryStr = $"SELECT {TDKH.TBL_DanhMucCongTac}.Code FROM {TDKH.TBL_DanhMucCongTac} WHERE \"CodeHangMuc\" IN ({CodeHM})" +
                        $" UNION ALL SELECT {TDKH.TBL_ChiTietCongTacTheoKy}.Code FROM {TDKH.TBL_ChiTietCongTacTheoKy} WHERE \"CodeHangMuc\" IN ({CodeHM})";
                    DataTable dt = DataProvider.InstanceTHDA.ExecuteQuery(queryStr);
                    if (dt.Rows.Count == 0)
                    {
                        string dbString = $"SELECT * FROM {MyConstant.TBL_THONGTINCONGTRINH} WHERE \"CodeDuAn\" = '{SharedControls.slke_ThongTinDuAn.EditValue}' ORDER BY \"CreatedOn\" ASC";
                        DataTable dtCongTrinhNew = DataProvider.InstanceTHDA.ExecuteQuery(dbString);
                        if (dtCongTrinhNew.Rows.Count == 1)
                            continue;
                        DuAnHelper.DeleteDataRows(MyConstant.TBL_THONGTINCONGTRINH, new string[] { row["Code"].ToString() });
                    }
                }
            }
            string lsCodeCT = MyFunction.fcn_Array2listQueryCondition(dtCongTrinh.AsEnumerable().Select(x => x["Code"].ToString()).ToArray());
            if (dtHangMuc.Rows.Count > 1)
            {
                foreach (DataRow row in dtHangMuc.Rows)
                {
                    string queryStr = $"SELECT {TDKH.TBL_DanhMucCongTac}.Code FROM {TDKH.TBL_DanhMucCongTac} WHERE \"CodeHangMuc\"='{row["Code"]}'" +
                        $" UNION ALL SELECT {TDKH.TBL_ChiTietCongTacTheoKy}.Code FROM {TDKH.TBL_ChiTietCongTacTheoKy} WHERE \"CodeHangMuc\"='{row["Code"]}'";
                    DataTable dt = DataProvider.InstanceTHDA.ExecuteQuery(queryStr);

                    if (dt.Rows.Count == 0)
                    {
                        queryStr = $"SELECT * FROM {MyConstant.TBL_THONGTINHANGMUC} WHERE \"CodeCongTrinh\" IN ({lsCodeCT}) ORDER BY \"CreatedOn\" ASC";
                        DataTable dtHangMucNew = DataProvider.InstanceTHDA.ExecuteQuery(queryStr);
                        if (dtHangMucNew.Rows.Count == 1)
                        {
                            WaitFormHelper.CloseWaitForm();
                            return;
                        }
                        DuAnHelper.DeleteDataRows(MyConstant.TBL_THONGTINHANGMUC, new string[] { row["Code"].ToString() });
                    }
                }
            }
            WaitFormHelper.CloseWaitForm();
        }

    }
}
