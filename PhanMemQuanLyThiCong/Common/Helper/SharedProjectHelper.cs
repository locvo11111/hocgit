using DevExpress.DataProcessing.InMemoryDataProcessor;
using DevExpress.Pdf.Native.BouncyCastle.Asn1.Misc;
using DevExpress.Pdf.Native.BouncyCastle.Asn1.X509;
using DevExpress.XtraEditors;
using DevExpress.XtraRichEdit.Fields;
using DevExpress.XtraSpreadsheet.Model;
using DevExpress.XtraWaitForm;
using Microsoft.AspNetCore.SignalR.Client;
using MoreLinq;
using PhanMemQuanLyThiCong.ChatBox;
using PhanMemQuanLyThiCong.Common.Constant;
using PhanMemQuanLyThiCong.Common.Enums;
using PhanMemQuanLyThiCong.Common.ViewModel;
using PhanMemQuanLyThiCong.Model;
using PhanMemQuanLyThiCong.Model.PermissionControl;
using PM360.Common.Helper;
using StackExchange.Profiling.Internal;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using VChatCore.ViewModels.SyncSqlite;
using MSETTING = PhanMemQuanLyThiCong.Properties.Settings;

namespace PhanMemQuanLyThiCong.Common.Helper
{
    public static class SharedProjectHelper
    {

        //public static string[] tblsKhoiLuongHangNgay =
        //{
        //    Server.Tbl_TDKH_KhoiLuongCongViecTungNgay,
        //    Server.Tbl_TDKH_HaoPhiVatTu_KhoiLuongHangNgay,
        //    Server.Tbl_TDKH_KHVT_KhoiLuongHangNgay,
        //};

        /*public static async Task<bool> UploadAllSQLiteData2Server(string[] tbls)
        {
            WaitFormHelper.ShowWaitForm("Đang tải lên server");

            await SyncRoleFromServer();
            AllPermission allPermission = BaseFrom.allPermission;

            //List<string> lsCodeUploaded = new List<string>();
            string crProject = (string)SharedControls.slke_ThongTinDuAn.EditValue;
            Dictionary<string, List<string>> dicTblCodes = new Dictionary<string, List<string>>();//{Table, Code}

            foreach (string nameTbl in tbls)
            {
                string colCode = (nameTbl == Server.Tbl_GiaoViec_CongViecCha) ? "CodeCongViecCha"
                    : (nameTbl == Server.Tbl_GiaoViec_CongViecCon) ? "CodeCongViecCon"
                    : "Code";
                WaitFormHelper.ShowWaitForm($"Bảng: {nameTbl}");
                List<string> whereCondition = new List<string>();

                string dbString = $"SELECT * FROM {nameTbl}";
                if (nameTbl != Server.Tbl_ThongTinDuAn)
                {
                    Dictionary<string, string> lsFk = Server.dicFks[nameTbl];

                    string[] fks = lsFk.Where(x => dicTblCodes.ContainsKey(x.Value) && dicTblCodes[x.Value].Any())
                        .Select(x => $"COALESCE({x.Key}, '') IN ({MyFunction.fcn_Array2listQueryCondition(dicTblCodes[x.Value].ToArray())})")
                        .ToArray();

                    if (fks.Any())
                        whereCondition.AddRange(fks);

                    if (nameTbl == Server.Tbl_TDKH_KhoiLuongCongViecTungNgay || nameTbl == Server.Tbl_TDKH_HaoPhiVatTu_KhoiLuongHangNgay)
                    {
                        whereCondition.Add($"(KhoiLuongThiCong IS NOT NULL OR IsEdited = true)");
                    }
                    else if (nameTbl == Server.Tbl_TDKH_KHVT_KhoiLuongHangNgay)
                        whereCondition.Add($"(KhoiLuongThiCong IS NOT NULL)");

                }
                else
                {
                    whereCondition.Add($"Code = '{crProject}'");
                }

                if (!allPermission.HaveInitProjectPermission && !allPermission.AllProjectThatUserIsAdmin.Contains(crProject))
                {
                    if (nameTbl == Server.Tbl_ThongTinDuAn)
                    {
                        if (!allPermission.AllProject.Contains(crProject))
                        {
                            MessageShower.ShowError("Bạn không có quyền với dự án hiện tại!");
                            goto ReturnFalse;
                        }
                    }
                    else if (nameTbl == Server.Tbl_ThongTinNhaThau || nameTbl == Server.Tbl_ThongTinNhaThauPhu || nameTbl == Server.Tbl_ThongTinToDoiThiCong)
                    {
                        if (!allPermission.AllContractor.Any())
                            continue;

                        var ContractorCondition = allPermission.AllContractor.Select(x => $"'{x}'");
                        
                        if (ContractorCondition.Any())
                            whereCondition.Add($"{colCode} IN ({string.Join(", ", ContractorCondition)})");
                    }
                    else if (nameTbl ==  Server.Tbl_GiaoViec_CongViecCha || nameTbl == Server.Tbl_TDKH_ChiTietCongTacTheoGiaiDoan)
                    {
                        if (!allPermission.AllTask.Any())
                            continue;

                        var ContractorInAdmin = allPermission.AllContractor.Select(x => $"'{x}'");
                        var ContractorAdminCondition = string.Join(", ", ContractorInAdmin);

                        var TaskCondition = allPermission.AllTask.Select(x => $"'{x}'");
                        string con = string.Empty;
                        if (TaskCondition.Any())
                            con = $"{colCode} IN ({string.Join(", ", TaskCondition)}) ";
                        if (ContractorInAdmin.Any())
                        {
                            string con1 = $"((COALESCE(CodeNhaThau, CodeNhaThauPhu, CodeToDoi)) IN ({ContractorAdminCondition}))";
                            if (!string.IsNullOrEmpty(con))
                            {
                                con = con1;
                            }
                            else
                                con = $"({con} OR {con1})";
                        }

                        if (!string.IsNullOrEmpty(con))
                            whereCondition.Add(con);

                        
                    }
                }


                if (whereCondition.Any())
                {
                    dbString += $" WHERE {string.Join(" AND ", whereCondition)}";
                }

                DataTable dt = DataProvider.InstanceTHDA.ExecuteQuery(dbString);
                if (dt.Rows.Count > 0)
                {
                    var res = await CusHttpClient.InstanceCustomer.MPostAsJsonAsync<bool>($"{nameTbl}/{RouteAPI.SUFFIX_AddOrUpdateMulti}", dt);
                    if (!res.MESSAGE_TYPECODE)
                    {
                        goto ReturnFalse;
                    }
                    else
                    {
                        if (!dicTblCodes.ContainsKey(nameTbl))
                            dicTblCodes[nameTbl] = new List<string>() { "" };


                        dicTblCodes[nameTbl].AddRange(dt.AsEnumerable().Select(x => x[colCode].ToString()));
                    }
                }
            }

            WaitFormHelper.CloseWaitForm();
            return true;
            
            ReturnFalse:
            WaitFormHelper.CloseWaitForm();
            return false;
        }*/

        public static async Task<bool> UploadAllSQLiteData2ServerIn1Post(bool checkModified, bool isSecondTime = false, bool OnlyCheckCodeChanged = false, string codeDuAn = null, string tbl = null)
        {

            if (!(await SyncRoleFromServer()))
                return false;
            AllPermission allPermission = BaseFrom.allPermission;

            //List<string> lsCodeUploaded = new List<string>();
             codeDuAn = codeDuAn ?? (string)SharedControls.slke_ThongTinDuAn.EditValue;
            if (codeDuAn is null)
            {
                MessageShower.ShowWarning("Vui lòng chọn dự án để tải lên");
                return false;
            }    
            WaitFormHelper.ShowWaitForm("Đang tải lên server");

            string timeSync = DateTime.Now.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE_WithTime);

            var dta = DataProvider.InstanceTHDA.ExecuteQueryModel<Tbl_ThongTinDuAnViewModel>($"SELECT LastSync, LastSyncSerialNo FROM " +
                $"{Server.Tbl_ThongTinDuAn} da WHERE Code = '{codeDuAn}'").SingleOrDefault();

            
            string lastChangeDA =(dta?.LastSync != null) ?  dta.LastSync.Value.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE_WithTime) : null;
            string lastSyncSeriNo = dta?.LastSyncSerialNo;

            //var lsDAs = SharedControls.slke_ThongTinDuAn.Properties.DataSource as List<Tbl_ThongTinDuAnViewModel>;

            //if (!allPermission.HaveInitProjectPermission)
            //    lsDAs = lsDAs.Where(x => allPermission.AllProject.Contains(x.Code)).ToList();

            //foreach (var da in lsDAs)
            //{
            DataTable dtTable = DataProvider.InstanceTHDA
                    .ExecuteQuery($"SELECT name FROM sqlite_schema WHERE type = 'table' ORDER BY name");

            string[] namesTblIndbCurrent = dtTable.AsEnumerable().Where(x => x[0].ToString() != "sqlite_sequence")
                            .Select(x => x[0].ToString()).ToArray();

            Dictionary<string, List<string>> dicTblCodes = new Dictionary<string, List<string>>();//{Table, Code}

            var codesDA = new List<string>() { codeDuAn };
           
            int ind = 1;
          
            var lstbl = (tbl is null) ? Server.LS_CONST_TYPE_SERVER_TBL : new string[] { Server.LS_CONST_TYPE_SERVER_TBL.Single(x => x == tbl) };

            foreach (string nameTbl in lstbl)
            {


                //if (nameTbl == Server.Tbl_DeletedRecored)
                //{
                //    continue;
                //}
                string nameDBSet = $"{nameTbl}s";
                if (!namesTblIndbCurrent.Contains(nameTbl))
                {
                    AlertShower.ShowInfo($"Không tìm thấy bảng {nameTbl} trong dữ liệu offline!");
                    //allData.SetValueByPropName(nameDBSet, null) ;

                    continue;
                }

                string colCode = (nameTbl == Server.Tbl_GiaoViec_CongViecCha) ? "CodeCongViecCha"
                    : (nameTbl == Server.Tbl_GiaoViec_CongViecCon) ? "CodeCongViecCon"
                    : "Code";
                WaitFormHelper.ShowWaitForm($"Đang tổng hợp bảng: {nameTbl}");
                List<string> whereCondition = new List<string>();
                //{
                //    "ModifiedFromServer = '1'"
                //};

                string dbString = $"SELECT * FROM {nameTbl}";
                if (nameTbl != Server.Tbl_ThongTinDuAn)
                {
                    Dictionary<string, string> lsFk = Server.dicFks[nameTbl];

                    string[] fks = lsFk.Where(x => dicTblCodes.ContainsKey(x.Value) && dicTblCodes[x.Value].Any())
                        .Select(x => $"COALESCE({x.Key}, '') IN ({MyFunction.fcn_Array2listQueryCondition(dicTblCodes[x.Value].ToArray())})")
                        .ToArray();

                    if (fks.Any())
                        whereCondition.Add($"({string.Join(" AND ", fks)})");

                    //if (nameTbl == Server.Tbl_TDKH_KhoiLuongCongViecTungNgay || nameTbl == Server.Tbl_TDKH_HaoPhiVatTu_KhoiLuongHangNgay)
                    //{
                    //    whereCondition.Add($"(KhoiLuongThiCong IS NOT NULL OR IsEdited = true)");
                    //}
                    //else if (nameTbl == Server.Tbl_TDKH_KHVT_KhoiLuongHangNgay)
                    //    whereCondition.Add($"(KhoiLuongThiCong IS NOT NULL)");

                }
                else
                {
           
                    whereCondition.Add($"Code = '{codeDuAn}'");
                }

                if (!allPermission.HaveInitProjectPermission && !allPermission.AllProjectThatUserIsAdmin.Contains(codeDuAn))
                {
                    if (Server.tbls_Contractor.Contains(nameTbl))
                    {
                        if (!allPermission.AllContractor.Any())
                            continue;

                        var ContractorCondition = allPermission.AllContractor.Select(x => $"'{x}'");

                        if (ContractorCondition.Any())
                            whereCondition.Add($"{colCode} IN ({string.Join(", ", ContractorCondition)})");
                    }
                    else if (nameTbl == Server.Tbl_GiaoViec_CongViecCha || nameTbl == Server.Tbl_TDKH_ChiTietCongTacTheoGiaiDoan)
                    {
                        if (!allPermission.AllTask.Any())
                            continue;

                        var ContractorInAdmin = allPermission.AllContractor.Select(x => $"'{x}'");
                        var ContractorAdminCondition = string.Join(", ", ContractorInAdmin);

                        var TaskCondition = allPermission.AllTask.Select(x => $"'{x}'");
                        string con = string.Empty;
                        if (TaskCondition.Any())
                            con = $"{colCode} IN ({string.Join(", ", TaskCondition)}) ";
                        if (ContractorInAdmin.Any())
                        {
                            string con1 = $"(COALESCE(CodeNhaThau, CodeNhaThauPhu, CodeToDoi) IN ({ContractorAdminCondition}))";
                            if (!string.IsNullOrEmpty(con))
                            {
                                con = con1;
                            }
                            else
                                con = $"({con} OR {con1})";

                        }

                        if (!string.IsNullOrEmpty(con))
                            whereCondition.Add(con);
                    }
                }


                if (whereCondition.Any())
                {
                    dbString += $" WHERE {string.Join(" AND ", whereCondition)}";
                }

                DataTable dt = DataProvider.InstanceTHDA.ExecuteQuery(dbString);

                if (!dicTblCodes.ContainsKey(nameTbl))
                    dicTblCodes[nameTbl] = new List<string>() { "" };

                dicTblCodes[nameTbl].AddRange(dt.AsEnumerable().Select(x => x[colCode].ToString()));
                
                if (nameTbl == Server.Tbl_TDKH_KhoiLuongCongViecTungNgay)
                {

                }

                if (nameTbl == Server.Tbl_ThongTinDuAn)
                {
                    if (dt.Rows.Count == 0)
                    {
                        WaitFormHelper.CloseWaitForm();
                        return true;
                    }
                    var dr = dt.Rows[0];
                    if ((dr["LastSync"] == DBNull.Value && dta?.BaseTime != null) || (dr["LastSync"] != DBNull.Value && (DateTime.Parse(dr["LastSync"].ToString()) < dta.BaseTime)))
                    {
                        MessageShower.ShowWarning("Dự án dưới máy có thời gian đồng bộ trước thòi gian đồng bộ server. Vui lòng tạo File mới trước!");
                        goto ReturnFalse;
                    }
                }
                if (tbl != Server.Tbl_ThongTinDuAn && BaseFrom.BanQuyenKeyInfo.SerialNo == lastSyncSeriNo && checkModified && lastChangeDA.HasValue())
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (dr["LastChange"] != DBNull.Value && string.Compare((string)dr["LastChange"], lastChangeDA) <= 0)
                            dr.Delete();


                        //vm = vm.AsEnumerable().Where(x => x["LastChange"] == DBNull.Value || (DateTime)x["LastChange"] >  ProjectId.LastSyncInSQLite).re;

                    }
                    dt.AcceptChanges();
                }



                var percent = Math.Round((double)ind++ / (double)Server.LS_CONST_TYPE_SERVER_TBL.Length*100, 2);

                var MaxQueries = 2000;


                if (dt.Rows.Count == 0)
                    continue;
                var num = (dt.Rows.Count - 1) / MaxQueries + 1;
                for (int i = 0; i < num; i++)
                {
                    
                    var rows = dt.AsEnumerable().Skip(i * MaxQueries).Take(MaxQueries).CopyToDataTable();
                    
                    var allData = new UpDownTableSqlite()
                    {
                        CheckedChangeOnly = OnlyCheckCodeChanged,
                        CodesDAs = codesDA,
                        Data = rows
                    };
                    WaitFormHelper.ShowWaitForm($"{percent}%: Đang gửi dữ liệu lên server: {i}/{num}: {nameTbl}");

                    //dbString = $"SELECT * FROM {Server.Tbl_DeletedRecored} WHERE CodeDuAn = '{codeDuAn}'";
                    //allData.DeletedRecored = DataProvider.InstanceTHDA.ExecuteQueryModel<Tbl_DeletedRecoredViewModel>(dbString);

                    allData.tbl = nameTbl;
                    //allData.Data = dt;
                    var res = await CusHttpClient.InstanceCustomer.MPostAsJsonAsync<UploadAllDataResponseViewModel>($"{RouteAPI.RawSQL_CONTROLLER}/PostAllDataToServer", allData);
                    if (!res.MESSAGE_TYPECODE /*&& res.STATUS_CODE < 0*/) // False all; status > 0 mean Lỗi lấy dữ liệu đồng bộ về.
                    {
                        AlertShower.ShowInfo(res.MESSAGE_CONTENT, "Lỗi tải dự án");
                        goto ReturnFalse;
                    }
                    //if (!isSecondTime)
                    //{
                        var codesChanged = res.Dto.ChangedCodes;

                        //var lsQuery = new List<string>();
                        foreach (var codeChanged in codesChanged)
                        {
                            string colCodeChanged = (codeChanged.TableName == Server.Tbl_GiaoViec_CongViecCha) ? "CodeCongViecCha"
            : (codeChanged.TableName == Server.Tbl_GiaoViec_CongViecCon) ? "CodeCongViecCon"
            : "Code";

                            if (!DuAnHelper.DeleteDataRows(codeChanged.TableName, codeChanged.DicOldNewCode.Values))
                            {
                                WaitFormHelper.CloseWaitForm();
                                return false;
                            }

                            if (!DuAnHelper.UpdateDataRows(codeChanged.TableName, codeChanged.DicOldNewCode))
                            {
                                WaitFormHelper.CloseWaitForm();
                                return false;
                            }
                            //lsQuery.AddRange(codeChanged.DicOldNewCode.Select(x => $"DELETE FROM {codeChanged.TableName} WHERE {colCodeChanged} = '{x.Value}';\r\n" +
                            //$"UPDATE {codeChanged.TableName} SET {colCodeChanged} = '{x.Value}' WHERE {colCodeChanged} = '{x.Key}'"));
                            //var codesNeedDeleted = codeChanged.IdsNeedDeleted;
                        }

                        //if (OnlyCheckCodeChanged)
                        //{
                        //    //WaitFormHelper.CloseWaitForm();
                        //    continue;
                        //}
                        //else
                        //    return await UploadAllSQLiteData2ServerIn1Post(checkModified, true, tbl: nameTbl);


                    //}
                    //else
                    //{
                        WaitFormHelper.ShowWaitForm($"{percent}%: Đang tải files lên");

                        var lsFile = res.Dto.Files;

                        MultipartFormDataContent multiForm = new MultipartFormDataContent();

                        if (lsFile != null)
                        {
                            foreach (FileViewModel file in lsFile)
                            {
                                string fullFilePath = $@"{BaseFrom.m_tempPath}\{BaseFrom.m_crTempDATH}\{file.FilePath}";
                                if (File.Exists(fullFilePath))
                                {
                                    file.Content = FileHelper.GetBytes(fullFilePath);
                                }
                            }
                            var result = await CusHttpClient.InstanceCustomer.MPostAsJsonAsync<bool>(RouteAPI.FILEMANAGE_PostFilesToServer, lsFile.Where(x => x.Content != null));
                            if (!result.MESSAGE_TYPECODE)
                            {
                                //string err = $"Exception: {ex.Message}\r\nInnerException: {ex.InnerException?.Message}";
                                AlertShower.ShowInfo(result.MESSAGE_CONTENT, "Lỗi tải file: ");
                                goto ReturnFalse;
                            }
                        //}
                        WaitFormHelper.ShowWaitForm($"{percent}%: Đang cập nhật IDs");


                        if (!res.MESSAGE_TYPECODE)
                        {
                            MessageShower.ShowError("Lỗi tải dữ liệu đồng bộ về PC");
                            goto ReturnFalse;
                        }

                    }
                }
            }
            //DataTable dtDA = allData.Tbl_ThongTinDuAns;
            string dbstr = $"UPDATE {Server.Tbl_ThongTinDuAn} SET LastSync = '{timeSync}', LastSyncSerialNo = '{BaseFrom.BanQuyenKeyInfo.SerialNo}' WHERE Code = '{codeDuAn}'";
            DataProvider.InstanceTHDA.ExecuteNonQuery( dbstr );
            WaitFormHelper.CloseWaitForm();
            DataProvider.InstanceTHDA.ExecuteNonQuery($"DELETE FROM {MyConstant.TBL_DeletedRecored}");

            return true;

            ReturnFalse:

            WaitFormHelper.CloseWaitForm();
            return false;
        }

        /// <summary>
        /// Đồng bộ quyền người dùng
        /// </summary>
        public static async Task<bool> SyncRoleFromServer()
        {
            WaitFormHelper.ShowWaitForm("Đang đồng bộ quyền người dùng");
            var allrole = await CusHttpClient.InstanceCustomer
                            .MGetAsync<AllPermission>($"{RouteAPI.TongDuAn_GetAllPermissionByUserId}/{BaseFrom.BanQuyenKeyInfo.UserId}");

            if (!allrole.MESSAGE_TYPECODE)
            {
                WaitFormHelper.CloseWaitForm();
                MessageShower.ShowError("Không thể đồng bộ quyền người dùng");
                BaseFrom.allPermission = new AllPermission();
                MSETTING.Default.Save();
                return false;
            }
            else
            {
                BaseFrom.allPermission = allrole.Dto;
            }

            MSETTING.Default.AllPermission = CryptoHelper.Base64EncodeObject(BaseFrom.allPermission);
            MSETTING.Default.Save();
            WaitFormHelper.CloseWaitForm();
            return true;
        }

        public static async Task<bool> SyncProjectsFromServer(ThongTinDuAnExtensionViewModel[] Das, TypeDownloadProjectEnum type)
        {

/*            DataRow dr1 = DataProvider.InstanceTHDA.ExecuteQuery($"SELECT * FROM {MyConstant.TBL_THONGTINTHDA}")
                .AsEnumerable().SingleOrDefault();
            if (dr1 is null)
            {
                MessageShower.ShowError("Lỗi db tổng hợp dự án");
                return false;
            }
*/
            //if (dr1["IsModified"].ToString() != false.ToString())
            //{

            //        MessageShower.ShowError("Dữ liệu offline có thay đổi. Vui lòng đẩy dữ liệu lên server trước khi cập nhật về!");
            //        return false;
            //}    

            WaitFormHelper.ShowWaitForm($"Đang cập nhật các dự án từ Server", "Đang tải dự án");
            string api = $"{RouteAPI.RawSQL_CONTROLLER}/getalldatabyprojectid";

                var count = 0;
                var countFail = 0;
            List<string> Noti = new List<string>();
            bool isCheckModified = type == TypeDownloadProjectEnum.NEW;
            ThongTinDuAnExtensionViewModel[] daUpload;
            if (isCheckModified)
            {
                daUpload = Das.Select(x => new ThongTinDuAnExtensionViewModel()
                {
                    Code = x.Code,
                    TenDuAn = x.TenDuAn,
                    LastSyncInSQLite = x.LastSyncInSQLite,
                    LastSyncSerialNo = x.LastSyncSerialNo,
                }).ToArray();
            }
            else
            {
                daUpload = Das.Select(x => new ThongTinDuAnExtensionViewModel()
                {
                    Code = x.Code,
                    TenDuAn = x.TenDuAn
                }).ToArray();
            }

            try
            {
                foreach (var da in daUpload)
                {
                    var des = $"Đang tải {++count}/{Das.Length}: {da.TenDuAn}\r\n";

                    if (!(await SharedProjectHelper.UploadAllSQLiteData2ServerIn1Post(true, false, true, da.Code)))
                    {
                        MessageShower.ShowWarning($"Không thể đồng bộ id dự án {da.TenDuAn} với server");
                        continue;
                    }

                    WaitFormHelper.ShowWaitForm(des);
                    var result = await CusHttpClient.InstanceCustomer.MPostAsJsonAsync<SQLiteAllDataViewModel1>(api, da);

                    if (!result.MESSAGE_TYPECODE)
                    {
                        countFail++;
                        AlertShower.ShowInfo($"Không thể cập nhật dự án \"{da.TenDuAn}\" từ server: {result.MESSAGE_CONTENT}");
                        WaitFormHelper.CloseWaitForm();
                        Noti.Add($"{da.TenDuAn}: {result.MESSAGE_CONTENT}");
                        continue;
                    }
                    else
                    {
                        var allSqlite = result.Dto;

                        await PushAllToSqlite(allSqlite,des);

                    }
                }
            }
            catch (Exception ex)
            {
                WaitFormHelper.CloseWaitForm();
                MessageShower.ShowError($"Lỗi tải dự án: {ex.Message}__{ex.InnerException?.Message}");
                return false;
            }

            WaitFormHelper.CloseWaitForm();
            if (countFail == 0)
            {
                MessageShower.ShowInformation("Tải về hoàn tất", "");
            }    
            else if (countFail == count)
            {
                MessageShower.ShowInformation("Tải về không thành công", "");

            }
            else
            {
                string mess = $@"Chi tiết tải về: {count - countFail} thành công, {countFail} thất bại";
                var drs = MessageShower.ShowYesNoCancelQuestionWithCustomText("Tải về không thành công", "", yesString: "Xem chi tiết lỗi", Nostring: "Không xem chi tiết");
                if (drs == DialogResult.Yes)
                {
                    XtraFormThongBaoMutilError FrmThongBao = new XtraFormThongBaoMutilError();
                    FrmThongBao.Description = string.Join("\r\n\t", Noti.ToArray());
                    FrmThongBao.ShowDialog();
                }
            }

            //DataProvider.InstanceTHDA.ExecuteNonQuery($"Update {MyConstant.TBL_THONGTINTHDA} SET LastDownload = '{DateTime.Now.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE_WithTime)}'");

            return true;
        }

        public static async Task PushAllToSqlite(SQLiteAllDataViewModel1 allSql, string prefixDescription = "")
        {
            List<FileViewModel> files = new List<FileViewModel>();

            var dtDA = allSql.Tbl_ThongTinDuAns;
            dtDA.AsEnumerable().ForEach(x =>
            {
                x.LastSync = DateTime.UtcNow;
                x.LastSyncSerialNo = BaseFrom.BanQuyenKeyInfo.SerialNo;
            });

            var dtDeleted = allSql.Tbl_DeletedRecoreds;
            var countTbl = Server.LS_CONST_TYPE_SERVER_TBL.Length;
            int count = 0;

            var tblCheckKey = Server.dicTblIndex.Keys;


            //if (ForceInsert)
            //{
                WaitFormHelper.ShowWaitForm($"{prefixDescription}Đang đồng bộ");
                List<string> deletes = new List<string>();
            SQLiteAllDataViewModel alldatatable = new SQLiteAllDataViewModel();
            //foreach (string tbl in Server.LS_CONST_TYPE_SERVER_TBL.Reverse())
            //{
            //    WaitFormHelper.ShowWaitForm($"{prefixDescription}Đang đồng bộ: {++count}/{countTbl}: {tbl}");
            //    string tblName = $"{tbl}s";

            //    var dt = DatatableHelper.BuildDataTable((IList)allSql.GetValueByPropName(tblName));
            //    alldatatable.SetValueByPropName(tblName, dt);
            //    if (tbl == Server.Tbl_DeletedRecored) continue;
            //    string colCode = (tbl == Server.Tbl_GiaoViec_CongViecCha) ? "CodeCongViecCha"
            //    : (tbl == Server.Tbl_GiaoViec_CongViecCon) ? "CodeCongViecCon"
            //    : "Code";

            //    //var data = (IList)allSql.GetValueByPropName($"{tbl}s");


            //    //var dt = DatatableHelper.BuildDataTable(data);
            //    var codes = dt.AsEnumerable().Select(x => (string)x[colCode]).ToList();
            //    var dltString = $"DELETE FROM {tbl} WHERE {colCode} IN ({MyFunction.fcn_Array2listQueryCondition(codes)})";
            //    try
            //    {
            //        DataProvider.InstanceTHDA.ExecuteNonQuery(dltString);
            //    }
            //    catch (Exception ex)
            //    {

            //    }
            
            //}
            //DataProvider.InstanceTHDA.ExecuteNonQueryFromList(deletes);
            //}

            count = 0;
            foreach (string tbl in Server.LS_CONST_TYPE_SERVER_TBL)
            {
                WaitFormHelper.ShowWaitForm($"{prefixDescription}Đang cập nhật: {++count}/{countTbl}: {tbl}");
                if (tbl == Server.Tbl_DeletedRecored) continue;
                string colCode = (tbl == Server.Tbl_GiaoViec_CongViecCha) ? "CodeCongViecCha"
                : (tbl == Server.Tbl_GiaoViec_CongViecCon) ? "CodeCongViecCon"
                : "Code";

                var data = (IList)allSql.GetValueByPropName($"{tbl}s");

                var dt = DatatableHelper.BuildDataTable(data);
                List<string> ls = new List<string>();
                foreach (DataColumn cl in dt.Columns)
                {
                if (cl.DataType == typeof(DateTime))
                    {
                    ls.Add(cl.ColumnName);

                    }
                }
                foreach (string name in ls)
                {
                    string newName = name + "_TBTNEW";
                    var newCl = dt.Columns.Add(newName, typeof(string));
        
                }

                if (ls.Any())
                {

                    foreach (DataRow dr in dt.Rows)
                    {
                        foreach (string item in ls)
                        {
                            string newName = item + "_TBTNEW";
                            if (dr[item] != DBNull.Value)
                            {
                                if (MyConstant.ColWithTime.Contains(item))
                                dr[newName] = ((DateTime)dr[item]).ToString(MyConstant.CONST_DATE_FORMAT_SQLITE_WithTime);
                                else dr[newName] = ((DateTime)dr[item]).ToString(MyConstant.CONST_DATE_FORMAT_SQLITE);
                            }
                        }
                    }

                    foreach (var item in ls)
                    {
                        string newName = item + "_TBTNEW";

                        dt.Columns.Remove(item);
                                dt.Columns[newName].ColumnName = item;

                    }
                }

                //Type type = data.GetType().GenericTypeArguments[0];

                //var dt = DatatableHelper.BuildDataTable(data);
                foreach (DataRow item in dt.Rows)
                {
                    item["ModifiedFromServer"] = false;
                }
                if (dt is null)
                    continue;

                var deletedCodes = dtDeleted.AsEnumerable()
                    .Where(x => x.TableName == tbl).Select(x => x.Code);
                
                if (deletedCodes.Any())
                    DuAnHelper.DeleteDataRows(tbl, deletedCodes);

                var arrDel = dt.AsEnumerable().Where(x => deletedCodes.Contains(x[colCode].ToString())).ToArray();


                

                if (tbl == Server.Tbl_TDKH_ChiTietCongTacTheoGiaiDoan && dt.Rows.Count > 0)
                    dt = dt.AsEnumerable().OrderBy(x => x["CodeCha"].ToString()).CopyToDataTable();
                
                if (tbl == Server.Tbl_TDKH_NhomCongTac && dt.Rows.Count > 0)
                    dt = dt.AsEnumerable().OrderBy(x => x["CodeNhomGiaoThau"].ToString()).CopyToDataTable();


                //if (typeDownload == TypeDownloadProjectEnum.NEWFILE)
                //{
                

                try
                {
                    dt.AsEnumerable().Where(x => x.RowState == DataRowState.Unchanged).ForEach(x => x.SetAdded());
                    DataProvider.InstanceTHDA.UpdateDataTableFromSqliteSource(dt, tbl);

                }
                catch (Exception ex)
                {
                    DataProvider.InstanceTHDA.UpdateDataTableFromOtherSource(dt, tbl);

                }
                //}
                //else
                //    DataProvider.InstanceTHDA.UpdateDataTableFromOtherSource(dt, tbl);

                if (tbl.EndsWith("_FileDinhKem"))
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        string crCode = dr["Code"].ToString();
                        string fileFromroot = Path.Combine(string.Format(CusFilePath.SQLiteFile, tbl, dr["CodeParent"].ToString()), crCode);
                        string crFile = Path.Combine(BaseFrom.m_FullTempathDA, fileFromroot);
                        if (!File.Exists(crFile))
                        {
                            files.Add(new FileViewModel()
                            {
                                FilePath = fileFromroot,
                            });
                        }
                        else //if (codes.Contains(crCode))
                        {
                            string crSum = FileHelper.CalculateFileMD5(crFile);

                            if (crSum != dr["Checksum"].ToString())
                            {
                                files.Add(new FileViewModel()
                                {
                                    FilePath = fileFromroot,
                                });
                            }
                        }
                    }

                }
            }

            foreach (string tbl in Server.LS_CONST_TYPE_SERVER_TBL)
            {
                var deletedCodes = dtDeleted.AsEnumerable()
                    .Where(x => x.TableName == tbl).Select(x => x.Code);

                if (deletedCodes.Any())
                    DuAnHelper.DeleteDataRows(tbl, deletedCodes);
            }

            List<FileViewModel> newFiles = new List<FileViewModel>();
            var countSuccess = 0;
            foreach (var fileItem in files)
            {
                var resultFile = await CusHttpClient.InstanceCustomer
                                .MPostAsJsonAsync<FileViewModel>(RouteAPI.FILEMANAGE_GetFromRootFolder, fileItem.FilePath);

                //WaitFormHelper.CloseWaitForm();
                if (!resultFile.MESSAGE_TYPECODE)
                {
                    //MessageShower.ShowError($"Tải dữ liệu thành công! TẢI FILE LỖI!");
                    AlertShower.ShowInfo($"Lỗi tải files nhân viên: {resultFile.MESSAGE_CONTENT}");
                    continue;
                }
                countSuccess++;
                var file = resultFile.Dto;

                if (file.Content is null)
                { continue; }

                string fullFilePath = Path.Combine(BaseFrom.m_FullTempathDA, file.FilePath);
                string dir = Path.GetDirectoryName(fullFilePath);

                if (!Directory.Exists(dir))
                    Directory.CreateDirectory(dir);

                if (File.Exists(fullFilePath))
                {
                    File.Delete(fullFilePath);
                }

                using (var writer = new BinaryWriter(File.OpenWrite(fullFilePath)))
                {
                    writer.Write(file.Content);
                }
            }

            WaitFormHelper.CloseWaitForm();
            if (countSuccess < files.Count())
            {
                MessageShower.ShowError($"Tải Dữ liệu thành công! Tải File {countSuccess}/{files.Count()}");
            }

        }

        /*public static async Task<bool> AddUserToHubGiaoViec()
        {
            //DataProvider.InstanceTHDA.ExecuteNonQuery($"Update {MyConstant.TBL_THONGTINTHDA} SET IsModified = false, LastUpload = '{DateTime.Now.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE_WithTime)}'");

            var request = new GiaoViecRequest()
            {
                UserId = ConnextService.UserId,
                CodeDuAn = SharedControls.slke_ThongTinDuAn.EditValue as string
            };
            var lstGroups = await ChatHelper.GetAllGroupChatByUser(request);
            if (lstGroups.MESSAGE_TYPECODE)
            {
                ConnextService.ManageGroups = lstGroups.Dto;
                //var lstDatas = await ChatHelper.GetAllGiaoViecByUser(request);
                //if (lstDatas.MESSAGE_TYPECODE)
                //{
                //    ConnextService.ManageGiaoViecs = lstDatas.Dto;
                //    var lstRoles = await ChatHelper.GetAllRoleByListCodeGiaoViec(lstDatas.Dto.GroupBy(x => x.CodeCongViecCha).Select(x => x.Key).ToList());

                //    if (lstRoles.MESSAGE_TYPECODE)
                //        BaseFrom.RoleDetails = lstRoles.Dto;
                //}
                //if (ConnextService._Connection != null && ConnextService._Connection.State == HubConnectionState.Connected)
                //    await ConnextService._Connection.InvokeAsync("AddUserToGroups", BaseFrom.BanQuyenKeyInfo, lstGroups.Dto, false);

                await ConnextService.AddUserToGroups(lstGroups.Dto);
                //WaitFormHelper.CloseWaitForm();
                return true;
            }
            //WaitFormHelper.CloseWaitForm();

            return false;
        }*/
    }
}
