using Dapper;
using DevExpress.CodeParser;
using DevExpress.DevAV.Chat.Model;
using DevExpress.Mvvm.Native;
using DevExpress.PivotGrid.SliceQueryDataSource;
using DevExpress.Spreadsheet;
using DevExpress.XtraEditors;
using DevExpress.XtraSplashScreen;
using PhanMemQuanLyThiCong.Common.Constant;
using PhanMemQuanLyThiCong.Common.Enums;
using PhanMemQuanLyThiCong.Common.Helper;
using PhanMemQuanLyThiCong.Model;
using PM360.Common.Helper;
using StackExchange.Profiling.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VChatCore.ViewModels.SyncSqlite;

namespace PhanMemQuanLyThiCong
{
    public partial class XtraForm_CaiDatTimNgayCongTac : DevExpress.XtraEditors.XtraForm
    {

        public delegate void DE_SENDATANGAY(DateTime ngayBD, DateTime ngayKT, int TongNhanCongMoiNgay);
        public DE_SENDATANGAY _sendata;

        string _code;
        DataRow  _rowHM;
        //DateTime _dateBD, _dateKT;
        string colFk = "";

        public XtraForm_CaiDatTimNgayCongTac(string code, DateTime dateBD, DateTime dateKT, string typeRow, string tenCongTac)
        {
            InitializeComponent();

            _code = code;

            //DataTable dt = DataProvider.InstanceTHDA.ExecuteQuery($"SELECT * FROM {MyConstant.TBL_THONGTINHANGMUC} WHERE Code = '{_code}'");
            //_rowHM = dt.Rows[0];

            //_dateBD = dateBD;
            //_dateKT = dateKT;
            nud_SoNgay.Maximum = int.MaxValue;

            switch (typeRow)
            {
                case MyConstant.TYPEROW_CongTrinh:
                    colFk = "hm.CodeCongTrinh";
                    labelControl1.Text = "Công trình: ";
                    break;
                case MyConstant.TYPEROW_HangMuc:
                    colFk = "hm.code";
                    labelControl1.Text = "Hạng mục: ";

                    break;
                case MyConstant.TYPEROW_PhanTuyen:
                    colFk = "dmct.CodePhanTuyen";
                    labelControl1.Text = "Phân đoạn: ";

                    break;
                case MyConstant.TYPEROW_Nhom:
                    colFk = "cttk.CodeNhom";
                    labelControl1.Text = "Nhóm: ";

                    break;
                default:
                    MessageShower.ShowWarning("Không hỗ trợ");
                    Close();
                    return;
            }

            labelControl1.Text += tenCongTac;
            de_NBD.EditValueChanged -= de_NBD_EditValueChanged;
            de_NKT.EditValueChanged -= de_NKT_EditValueChanged;
            de_NBD.EditValue = dateBD;
            de_NKT.EditValue = dateKT;
            nud_SoNgay.Value = (dateKT - dateBD).Days + 1;
            de_NBD.EditValueChanging += de_NBD_EditValueChanging;
            de_NKT.EditValueChanging += de_NKT_EditValueChanging;
            de_NBD.EditValueChanged += de_NBD_EditValueChanged;
            de_NKT.EditValueChanged += de_NKT_EditValueChanged;
        }
        private void XtraForm_CaiDatTimNgayCongTac_Load(object sender, EventArgs e)
        {

        }

        public List<CongTac> TinhTongNgayTamTinh(bool isSortType = true)
        {
            DateTime NBD = ((DateTime)de_NBD.EditValue).Date;
            DateTime NKT = ((DateTime)de_NKT.EditValue).Date;

            int tongNgay = (NKT - NBD).Days + 1;

            if (tongNgay <= 0)
            {
                MessageShower.ShowInformation("Số ngày quá ngắn, Vui lòng tăng số ngày thực hiện!");
                DialogResult = DialogResult.Ignore;
                //WaitFormHelper.CloseWaitForm();
                return null;
            }
            //int nhanCongTongMoiNgay = (int)nud_TongNhanCongMoiNgay.Value;


            //Xắp xếp lại theo trình tự thi công


            List<CongTac> congTacsHasDM = DuAnHelper.GetCurrentCongTacByHMs(new string[] { _code }, colFk);


            bool isXxThiCong = rg_PhuongPhapTinh.GetAccessibleName() == "ThiCong";
            if (isXxThiCong)
            {
                var MDMs = congTacsHasDM.Select(x => x.MaHieuCongTac);
                string dbString = $"SELECT MaDinhMuc, TrinhTuThiCong FROM Tbl_DinhMucAll " +
                    $"WHERE MaDinhMuc IN ({MyFunction.fcn_Array2listQueryCondition(MDMs)})";
                var ctacBrief = DataProvider.InstanceTBT.ExecuteQueryModel<CongTacBriefViewModel>(dbString);

                congTacsHasDM = (from ct in congTacsHasDM
                                join dm in ctacBrief
                                on ct.MaHieuCongTac equals dm.MaDinhMuc into temp
                                from c in temp.DefaultIfEmpty()
                                select new CongTac()
                                {
                                    Code = ct.Code,
                                    CodeNhom = ct.CodeNhom,
                                    KhoiLuongNhom = ct.KhoiLuongNhom,
                                    MaHieuCongTac = ct.MaHieuCongTac,
                                    TongNhanCong = ct.TongNhanCong,
                                    TrinhTuThiCong = c?.TrinhTuThiCong ?? int.MaxValue,

                                }).OrderBy(x => x.TrinhTuThiCong).ToList();
            }

            foreach (var x in  congTacsHasDM)
            {
                //x.NhanCongMayTamTinh = x.NhanCongMayTamTinhGoc = (int)Math.Min((double)(x.NhanCongNgayTamTinh ?? x.MayTamTinh ?? 1), (double)nhanCongTongMoiNgay);
                //double haoPhi = DinhMucHelper.GetHPNhanCong(x.Code);

                //x.TongNhanCong = (double)x["NhanCongReal"];

                if (x.TongNhanCong == 0) x.TongNhanCong = 1;
            };


            var grs = congTacsHasDM.Where(x => x.CodeNhom.HasValue()).GroupBy(x => x.CodeNhom);

            foreach (var grNhom in grs)
            {
                var fstCta = grNhom.First();
                foreach (var item in grNhom)
                {
                    item.SortId = fstCta.SortId;
                    item.TrinhTuThiCong = fstCta.TrinhTuThiCong;
                }

                if (fstCta.KhoiLuongNhom.HasValue)
                {
                    fstCta.Code = fstCta.CodeNhom;
                    fstCta.TongNhanCong = grNhom.Sum(x => x.TongNhanCong);
                    fstCta.MaHieuCongTac = "";
                    foreach (var item in grNhom.Where(x => x != fstCta))
                    {
                        congTacsHasDM.Remove(item);
                    }    
                }
            }


            congTacsHasDM = congTacsHasDM.Where(x => x.TongNhanCong.HasValue).ToList();
            int MaximumTongNhanCong = congTacsHasDM.FindAll(x => x.TongNhanCong.HasValue).Sum(x => x.TongNhanCong.Value);

            var allNgayNghi = TDKHHelper.GetNgayNghiHangMuc(_code, NBD, NKT);

            NBD = TDKHHelper.GetNextDate(allNgayNghi, NBD);
            NKT = TDKHHelper.GetNextDate(allNgayNghi, NKT);
            int countReCalc = 10;
            double NCTB = (double)MaximumTongNhanCong / tongNgay;
            double HeSo = 0.9;
            BatDauTinh:
            //countReCalc--;
            //
            HeSo -= 0.1;
            if (HeSo <= 0)
            {
                MessageShower.ShowInformation("Số ngày quá ngắn, Vui lòng tăng số ngày thực hiện!");
                DialogResult = DialogResult.Ignore;
                WaitFormHelper.CloseWaitForm();
                return null;
            }

            congTacsHasDM.ForEach(x =>
            {
                x.NgayBatDau = null;
                x.NgayKetThuc = null;
                x.ChiTietCongTac.dicNCHangNgay.Clear();
            });
            //congTacsHasDM.ForEach(x => x.TongNgayTamTinh = null);
            //Dictionary<DateTime, int> dicNhanCongConLai = new Dictionary<DateTime, int>();

            //for (DateTime date = NBD; date <= NKT; date = date.AddDays(1))
            //{
            //    dicNhanCongConLai.Add(date, nhanCongTongMoiNgay);
            //}

            bool isIncFrtTime = false;
            int count = 0;
            DateTime NgayDaTinhXong = NBD.AddDays(-1);
            DateTime midDate = NBD.AddDays((double)tongNgay / 2);
            var countAll = congTacsHasDM.Count();
            double TyLe = 0.5; //Ngày đầu tiên/ trung bình ngày
            int NCNuaTrai = (int)Math.Ceiling((double)MaximumTongNhanCong / (2.0));
            int SoNgayNuaTrai = tongNgay / 2;

            int NCNuaPhai = MaximumTongNhanCong - NCNuaTrai;
            int SoNgayNuaPhai = tongNgay - SoNgayNuaTrai;
            //double NCDinh = MaximumTongNhanCong / tongNgay;
            for (int i = 0; i < countAll; i++)
            {
               
                CongTac item = congTacsHasDM[i];
                WaitFormHelper.ShowWaitForm($"====Công tác:{i}/{countAll}: {item.MaHieuCongTac}===");

                DateTime ngayKetThuc = NKT;

                var congTacsAfterItem = congTacsHasDM.Skip(i + 1);
                var congTacsBeforeItem = congTacsHasDM.Take(i);

                var congTacHasMaxDateBD = congTacsAfterItem.Where(x => x.MaxNBD.HasValue).FirstOrDefault();

                if (congTacHasMaxDateBD != null)
                    ngayKetThuc = TDKHHelper.GetNextDate(allNgayNghi, congTacHasMaxDateBD.MaxNBD.Value);

                int offsetNKT = 0;

                List<CongTac> lsCalculated = null;

                DateTime minNBD = (i == 0) ? NBD : congTacsHasDM[i - 1].NBDThiCong.Value;
                DateTime minNKT = minNBD;
                //item.NhanCongMayTamTinh = item.NhanCongMayTamTinh ?? (int)Math.Min((double)(item.NhanCongNgayTamTinh ?? item.MayTamTinh ?? 1), (double)nhanCongTongMoiNgay);

                //Không dùng else if vì có nhiều điều kiện chồng cheo

                if (ce_RangBuoc.Checked)
                {
                    #region Lọc điều kiện ràng buộc

                    if (item.MaHieuCongTac.StartsWith("AB.") && congTacsAfterItem.Any(x => x.MaHieuCongTac.StartsWith("AD.")))
                    {
                        offsetNKT = Math.Max(offsetNKT, 1);
                    }
                    if (item.MaHieuCongTac.StartsWith("AA.") && !item.MaHieuCongTac.StartsWith("AA.23") && congTacsAfterItem.Any(x => x.MaHieuCongTac.StartsWith("AA.23")))
                    {
                        offsetNKT = Math.Max(offsetNKT, 1);
                    }
                    if ((item.MaHieuCongTac.StartsWith("AF.111") && congTacsAfterItem.Any(x => x.MaHieuCongTac.StartsWith("AF.") && !x.MaHieuCongTac.StartsWith("AF.111")))
                        || (item.MaHieuCongTac.StartsWith("AF.1") && congTacsAfterItem.Any(x => x.MaHieuCongTac.StartsWith("AF.2") || x.MaHieuCongTac.StartsWith("AF.3") || x.MaHieuCongTac.StartsWith("AF.4"))))
                    {
                        offsetNKT = Math.Max(offsetNKT, 1);
                    }
                    if (item.MaHieuCongTac.StartsWith("AE")
                        && congTacsAfterItem.Any(x => x.MaHieuCongTac.StartsWith("AK.2") || x.MaHieuCongTac.StartsWith("AK.3") || (x.MaHieuCongTac.StartsWith("AK.5") && !x.MaHieuCongTac.StartsWith("AK.57"))))
                    {
                        offsetNKT = Math.Max(offsetNKT, 3);
                    }
                    if (item.MaHieuCongTac.StartsWith("AE") && congTacsHasDM.Any(x => x.MaHieuCongTac.StartsWith("AK.1")))
                    {
                        offsetNKT = Math.Max(offsetNKT, 1);
                    }

                    if (item.MaHieuCongTac.StartsWith("AF.1") && !item.MaHieuCongTac.StartsWith("AF.134") && !item.MaHieuCongTac.StartsWith("AF.125") && congTacsAfterItem.Any(x => x.MaHieuCongTac.StartsWith("AE.")))
                    {
                        offsetNKT = 1;
                    }

                    if (offsetNKT > 0)
                    {
                        ngayKetThuc = ngayKetThuc.AddDays(-offsetNKT);
                        ngayKetThuc = TDKHHelper.GetPrevDate(allNgayNghi, ngayKetThuc);
                    }

                    if (i > 0)
                    {
                        CongTac itemPrev = congTacsHasDM[i - 1];

                        if (item.MaHieuCongTac.StartsWith("AB.1112")
                            || item.MaHieuCongTac.StartsWith("AB.2319")
                            || item.MaHieuCongTac.StartsWith("AB.4")
                            || item.MaHieuCongTac.StartsWith("AB.56")
                            || item.MaHieuCongTac.StartsWith("AB.57"))
                        {
                            minNKT = itemPrev.NKTThiCong.Value; //Hoàn thành sau công tác phía trên
                        }

                        if (item.MaHieuCongTac.StartsWith("AM."))
                        {
                            var ctLastCalculated = congTacsBeforeItem
                                .Where(x => x.MaHieuCongTac.StartsWith("AA.")
                                || x.MaHieuCongTac.StartsWith("AB.")
                                || x.MaHieuCongTac.StartsWith("AC.")
                                || x.MaHieuCongTac.StartsWith("AD.")).LastOrDefault();

                            if (ctLastCalculated != null)
                                minNKT = ctLastCalculated.NKTThiCong.Value;
                        }

                        if (item.MaHieuCongTac.StartsWith("AD.27"))
                        {
                            var ctLastCalculated = congTacsBeforeItem
                                .Where(x => x.MaHieuCongTac.StartsWith("AD.")).LastOrDefault();

                            if (ctLastCalculated != null)
                                minNKT = ctLastCalculated.NKTThiCong.Value;
                        }

                        if (item.MaHieuCongTac.StartsWith("AD."))
                        {
                            var ctLastCalculated = congTacsBeforeItem.Where(x => x.MaHieuCongTac.StartsWith("AB.")).LastOrDefault();
                            if (ctLastCalculated != null)
                            {
                                var minNBDTemp = ctLastCalculated.NBDThiCong.Value.AddDays(1);
                                if (minNBD < minNBDTemp)
                                    minNBD = minNBDTemp;

                                minNKT = ctLastCalculated.NKTThiCong.Value.AddDays(1);
                            }
                            count++;

                            //if (!TinhNgayTamTinh(dicNhanCongConLai, item.NhanCongMayTamTinh.Value, item.TongNhanCong.Value, ngayKetThuc, minNBD, minNKT, lsCalculated, 1, 1, out minNBD, out minNKT))
                            //{
                            //    Debug.WriteLine($"+++++++++++Tăng nhân công dòng+++++++++++++ item.MaHieuCongTac.StartsWith(\"AD.\")");
                            //    goto TangNhanCong;
                            //}
                        }

                        if (item.MaHieuCongTac.StartsWith("AK.2") || item.MaHieuCongTac.StartsWith("AK.3") || (item.MaHieuCongTac.StartsWith("AK.5") && !item.MaHieuCongTac.StartsWith("AK.57")))
                        {
                            var ctLastCalculated = congTacsBeforeItem.Where(x => x.MaHieuCongTac.StartsWith("AE.") && x.NBDThiCong.HasValue && x.NKTThiCong.HasValue).LastOrDefault();
                            if (ctLastCalculated != null)
                            {
                                var minNBDTemp = ctLastCalculated.NBDThiCong.Value.AddDays(1);
                                if (minNBD < minNBDTemp)
                                    minNBD = minNBDTemp;
                                minNKT = ctLastCalculated.NKTThiCong.Value.AddDays(3);
                                count++;
                            }
                        }

                        if (item.MaHieuCongTac.StartsWith("AF.2") || item.MaHieuCongTac.StartsWith("AF.3") || item.MaHieuCongTac.StartsWith("AF.4"))
                        {
                            var ctLastCalculated = congTacsBeforeItem.Where(x => x.MaHieuCongTac.StartsWith("AF.1") && x.NBDThiCong.HasValue && x.NKTThiCong.HasValue).LastOrDefault();
                            if (ctLastCalculated != null)
                            {
                                var minNBDTemp = ctLastCalculated.NBDThiCong.Value.AddDays(1);
                                if (minNBD < minNBDTemp)
                                    minNBD = minNBDTemp;

                                minNKT = ctLastCalculated.NKTThiCong.Value.AddDays(1);
                                count++;
                            }
                        }

                        if (item.MaHieuCongTac.StartsWith("AF.") && !item.MaHieuCongTac.StartsWith("AF.111"))
                        {
                            var ctLastCalculated = congTacsBeforeItem.Where(x => x.MaHieuCongTac.StartsWith("AF.111") && x.NBDThiCong.HasValue && x.NKTThiCong.HasValue).LastOrDefault();
                            if (ctLastCalculated != null)
                            {
                                var minNBDTemp = ctLastCalculated.NBDThiCong.Value.AddDays(1);
                                if (minNBD < minNBDTemp)
                                    minNBD = minNBDTemp;

                                minNKT = ctLastCalculated.NKTThiCong.Value.AddDays(1);
                                count++;
                            }
                        }
                        if (item.MaHieuCongTac.StartsWith("AA.23"))
                        {
                            var ctLastCalculated = congTacsBeforeItem.Where(x => x.MaHieuCongTac.StartsWith("AA.") && x.NBDThiCong.HasValue && x.NKTThiCong.HasValue).LastOrDefault();
                            if (ctLastCalculated != null)
                            {
                                var minNBDTemp = ctLastCalculated.NBDThiCong.Value.AddDays(1);
                                if (minNBD < minNBDTemp)
                                    minNBD = minNBDTemp;

                                minNKT = ctLastCalculated.NKTThiCong.Value.AddDays(1);
                                count++;
                            }
                        }
                        if (item.MaHieuCongTac.StartsWith("AK.6") || item.MaHieuCongTac.StartsWith("AK.7") || item.MaHieuCongTac.StartsWith("AK.8"))
                        {
                            var ctLastCalculated = congTacsBeforeItem
                                .LastOrDefault(x => x.MaHieuCongTac.StartsWith("AK.2") && x.NBDThiCong.HasValue && x.NKTThiCong.HasValue && x.NBDThiCong.Value > NgayDaTinhXong);

                            if (ctLastCalculated != null)
                            {
                                var minNBDTemp = ctLastCalculated.NKTThiCong.Value;
                                if (minNBD < minNBDTemp)
                                    minNBD = minNBDTemp;

                                minNKT = ctLastCalculated.NKTThiCong.Value;
                                count++;
                            }
                        }

                        if (item.MaHieuCongTac.StartsWith("AK.9"))
                        {
                            var ctLastCalculated = congTacsBeforeItem
                                .LastOrDefault(x => x.MaHieuCongTac.StartsWith("AD.") && x.NBDThiCong.HasValue && x.NKTThiCong.HasValue && x.NBDThiCong.Value > NgayDaTinhXong);


                            if (ctLastCalculated != null)
                            {
                                var minNBDTemp = ctLastCalculated.NKTThiCong.Value;
                                if (minNBD < minNBDTemp)
                                    minNBD = minNBDTemp;

                                minNKT = ctLastCalculated.NKTThiCong.Value;
                                count++;
                            }
                        }

                        if (item.MaHieuCongTac.StartsWith("AK.4"))
                        {
                            var ctLastCalculated = congTacsBeforeItem
                                .LastOrDefault(x => (x.MaHieuCongTac.StartsWith("AE.") || x.MaHieuCongTac.StartsWith("AF.")) && x.NBDThiCong.HasValue && x.NKTThiCong.HasValue && x.NBDThiCong.Value > NgayDaTinhXong);

                            if (ctLastCalculated != null)
                            {
                                var minNBDTemp = ctLastCalculated.NKTThiCong.Value;
                                if (minNBD < minNBDTemp)
                                    minNBD = minNBDTemp;

                                minNKT = ctLastCalculated.NKTThiCong.Value;
                                count++;
                            }
                        }

                        if (item.MaHieuCongTac.StartsWith("AE.") || item.MaHieuCongTac.StartsWith("AF."))
                        {
                            var ctLastCalculated = congTacsBeforeItem
                                .LastOrDefault(x => x.MaHieuCongTac.StartsWith("AK.4") && x.NBDThiCong.HasValue && x.NKTThiCong.HasValue && x.NBDThiCong.Value > NgayDaTinhXong);

                            if (ctLastCalculated != null)
                            {
                                var minNBDTemp = ctLastCalculated.NKTThiCong.Value;
                                if (minNBD < minNBDTemp)
                                    minNBD = minNBDTemp;

                                minNKT = ctLastCalculated.NKTThiCong.Value;
                                count++;
                            }
                        }

                        if (item.MaHieuCongTac.StartsWith("AE."))
                        {
                            var ctLastCalculated = congTacsBeforeItem
                                .LastOrDefault(x => x.MaHieuCongTac.StartsWith("AF.1") && !x.MaHieuCongTac.StartsWith("AF.134") && !x.MaHieuCongTac.StartsWith("AF.125") && x.NBDThiCong.HasValue && x.NKTThiCong.HasValue && x.NBDThiCong.Value > NgayDaTinhXong);

                            if (ctLastCalculated != null)
                            {
                                var minNBDTemp = ctLastCalculated.NKTThiCong.Value.AddDays(1);
                                if (minNBD < minNBDTemp)
                                    minNBD = minNBDTemp;

                                minNKT = ctLastCalculated.NKTThiCong.Value;
                                count++;
                            }
                        }
                    }

                    #endregion Lọc điều kiện ràng buộc
                }
                //item.TongNgayTamTinh = item.TongNgayTamTinh ?? (int?)Math.Max((int)Math.Floor((decimal)item.TongNhanCong.Value / (decimal)item.NhanCongMayTamTinh), 1);

                if (minNBD <= NgayDaTinhXong)
                    minNBD = NgayDaTinhXong.AddDays(1);

                minNBD = TDKHHelper.GetNextDate(allNgayNghi, minNBD);

                if (minNBD > ngayKetThuc)
                {
                    item.MaxNBD = ngayKetThuc.AddDays((ngayKetThuc - minNBD).Days);
                    if (item.MaxNBD < NBD)
                    {
                        MessageShower.ShowInformation("Số ngày quá ngắn, Vui lòng tăng số ngày thực hiện!");
                        DialogResult = DialogResult.Ignore;
                        WaitFormHelper.CloseWaitForm();
                        return null;
                    }

                    //if (countReCalc <=0)

                    goto BatDauTinh;
                }
                DateTime crDate = minNBD;
                int NCCongTacConLai = item.TongNhanCong.Value;
                //int soCongTacCungTTTC = (isXxThiCong) ? ((isSortType) ? congTacsAfterItem.Count(x => x.TrinhTuThiCong == item.TrinhTuThiCong) + 1 : 1)
                //    : ((isSortType) ? congTacsAfterItem.Count(x => x.SortId == item.SortId) + 1 : 1);

                //int NCAllCTacInrange = congTacsBeforeItem.Select(x => x.ChiTietCongTac.dicNCHangNgay.Where(y => y.Key > minNBD && y.Key < crDate)).Sum(x => x.Sum(y => y.Value));
                //int TongNCCanSet = NCAllCTacInrange + item.TongNhanCong.Value;
                var ngayNghis = TDKHHelper.CalcNgayNghiBetweenDates(Common.Enums.TypeKLHN.CongTac, item.Code, minNBD, ngayKetThuc);
                int soNgayNghi = ngayNghis.Count();
                //int SoNgay = (ngayKetThuc - minNBD).Days + 1 - soNgayNghi;
                //int SoDu = TongNCCanSet % SoNgay;
                //int SoNC = TongNCCanSet / SoNgay;
                Dictionary<DateTime, int> dicNgay = new Dictionary<DateTime, int>();

                if (i == countAll - 2)
                {

                }

                while (crDate <= ngayKetThuc)
                {
                    if (ngayNghis.Contains(crDate))
                    {
                        //if (item.ChiTietCongTac.dicNCHangNgay.Any())
                        item.ChiTietCongTac.dicNCHangNgay.Add(crDate, 0);
                        crDate = crDate.AddDays(1);
                        continue;
                    }


                    int NhanCongTruocDo = congTacsHasDM.Select(x => x.ChiTietCongTac.dicNCHangNgay.Where(y => y.Key < crDate)).Sum(x => x.Sum(y => y.Value));

                    int soNgayTruocDo = (crDate - NBD).Days;

                    int NhanCongChuaTinh = MaximumTongNhanCong - NhanCongTruocDo;

                    int soNgayChuaTinh = (NKT - crDate).Days + 1;
                    int NhanCongLonNhatMoiNgay;

                    if (soNgayChuaTinh == 1)
                        NhanCongLonNhatMoiNgay = NhanCongChuaTinh;
                    else
                    NhanCongLonNhatMoiNgay = (int)Math.Floor((double)NhanCongChuaTinh / (double)soNgayChuaTinh* (HeSo + 2*(1-HeSo) * (double)soNgayTruocDo / (double)tongNgay));


                    //int NhanCongTruocDo = congTacsHasDM.Select(x => x.ChiTietCongTac.dicNCHangNgay.Where(y => y.Key < crDate)).Sum(x => x.Sum(y => y.Value));

                    //int soNgayTruocDo = (crDate - NBD).Days;

                    ////double TBTruocDo = 0;
                    ////if (soNgayTruocDo > 0)
                    ////    TBTruocDo= (double)NhanCongTruocDo / (double)soNgayTruocDo;
                    
                    ////int NhanCongChuaTinh = MaximumTongNhanCong - NhanCongTruocDo;

                    ////int soNgayChuaTinh = (NKT - crDate).Days + 1;

                    ////double TBChuaTinh = (double)NhanCongChuaTinh / soNgayChuaTinh;
                    ////double NCTB = TBChuaTinh;
                    ////if (crDate <= midDate)
                    ////{
                    //double NCDinh = (double)4 * NCTB / 3;
                    //var offset = Math.Abs(((midDate - crDate).TotalDays * 2 * NCTB * (1 - TyLe) / ((double)tongNgay / 2)));
                    ////}

                    ////int heso = (midDate - crD)
                    //int NhanCongLonNhatMoiNgay = (int)Math.Ceiling(4*NCTB/3 - offset);

                    var CongTacDaThemNgay = congTacsBeforeItem.Where(x => x.ChiTietCongTac.dicNCHangNgay.ContainsKey(crDate));

                    int NCHomNay = (CongTacDaThemNgay.Any()) ? CongTacDaThemNgay.Sum(x => x.ChiTietCongTac.dicNCHangNgay[crDate]) : 0;

                    int soNgayChuaTinhCongTac = (ngayKetThuc - crDate).Days + 1;
                    //int NCTB = Math.Max(NCCongTacConLai / soNgayChuaTinhCongTac, 1);

                    if (NhanCongLonNhatMoiNgay <= NCHomNay)
                    {
                        crDate = crDate.AddDays(1);
                        continue;
                    }



                    int NCCoTheThem = (int)Math.Ceiling((double)(NhanCongLonNhatMoiNgay - NCHomNay));

                    /*                   if (NCTB > NCCoTheThem && NCHomNay == 0)
                                       {

                                           item.ChiTietCongTac.dicNCHangNgay.Add(crDate, (int)Math.Ceiling(NCTB));
                                           NCCongTacConLai -= (int)Math.Ceiling(NCTB);
                                           NgayDaTinhXong = crDate;
                                           if (soNgayChuaTinhCongTac == 1)
                                               break;
                                       }
                                       else*/
                    if (crDate == midDate.Date)
                    {

                    }
                    if (soNgayChuaTinhCongTac == 1)
                    {
                        item.ChiTietCongTac.dicNCHangNgay.Add(crDate, NCCongTacConLai);
                        if (NCCongTacConLai >= NCCoTheThem)
                            NgayDaTinhXong = crDate;
                        NCCongTacConLai = 0;
                        //NgayDaTinhXong = crDate;
                        break;
                    }
                    else if (NCCongTacConLai <= NCCoTheThem)
                    {
                        item.ChiTietCongTac.dicNCHangNgay.Add(crDate, NCCongTacConLai);

                        if (NCCongTacConLai == NCCoTheThem)
                            NgayDaTinhXong = crDate;

                        NCCongTacConLai -= NCCongTacConLai;
                        break;
                    }
                    else
                    {
                        item.ChiTietCongTac.dicNCHangNgay.Add(crDate, NCCoTheThem);
                        NCCongTacConLai -= NCCoTheThem;
                        //if (soCongTacCungTTTC == 1)
                            NgayDaTinhXong = crDate;
                    }
                    crDate = crDate.AddDays(1);
                }


                if (crDate > NKT)
                {
                    item.MaxNBD = minNBD.AddDays(-1);// (item.MaxNBD.HasValue) ? item.MaxNBD.Value.AddDays(-1) : hangMuc.NKTThiCong.Value.AddDays(-1);
                    goto BatDauTinh;
                }
                item.NBDThiCong = minNBD;
                item.NKTThiCong = crDate;
            }
            WaitFormHelper.CloseWaitForm();
            return congTacsHasDM;
        }

        private void ce_NhapSoNgay_CheckedChanged(object sender, EventArgs e)
        {
            if (ce_NhapSoNgay.Checked)
            {
                de_NKT.Enabled= false;
                nud_SoNgay.Enabled = true;
            }
            else
            {
                de_NKT.Enabled = true;
                nud_SoNgay.Enabled = false;
            }
        }

        private void de_NBD_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        {
            if (e.NewValue is null)
                return;

                DateTime dateNew = (DateTime)e.NewValue;
                DateTime dateKT = (DateTime)de_NKT.EditValue;
            if (!ce_NhapSoNgay.Checked)
            {

                //if (dateNew)
                if (dateNew > dateKT)
                {
                    MessageShower.ShowInformation("Vui lòng nhập Ngày Bắt Đầu không vượt quá Ngày Kết Thúc!");
                    e.Cancel = true;
                    return;
                }
                nud_SoNgay.Value = (dateKT - dateNew).Days + 1;
            }
            else
            {
                de_NKT.EditValueChanging -= de_NKT_EditValueChanging;
                de_NKT.EditValue = dateNew.AddDays((int)nud_SoNgay.Value + 1);
                de_NKT.EditValueChanging += de_NKT_EditValueChanging;

            }
        }

        private void de_NKT_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        {
            if (e.NewValue is null)
                return;
            DateTime dateBD = (DateTime)de_NBD.EditValue;
            DateTime dateNew = (DateTime)e.NewValue;
            if (dateNew < dateBD)
            {
                MessageShower.ShowInformation("Vui lòng nhập Ngày Kết Thúc lớn hơn Ngày Bắt Đầu!");
                e.Cancel = true;
                return;
            }

            nud_SoNgay.Value = (dateNew - dateBD).Days + 1;
        }

        private void nud_SoNgay_ValueChanged(object sender, EventArgs e)
        {
            de_NKT.EditValue = (de_NBD.DateTime).AddDays((int)nud_SoNgay.Value - 1);
        }

        private void bt_ThucHien_Click(object sender, EventArgs e)
        {
            if (nud_SoNgay.Value <= 0)
            {
                MessageShower.ShowWarning("Vui lòng nhập số ngày > 0");
                return;
            }    
            WaitFormHelper.ShowWaitForm("Đang tính toán...");
            var cts = TinhTongNgayTamTinh();
            if (cts is null)
            {
                WaitFormHelper.CloseWaitForm();
                return;
            }

            var dr = MessageShower.ShowYesNoQuestion("Bạn có chuyển trạng thái \"Đang thực hiện\" cho các công tác đã tính ngày không?");
            
            if (dr == DialogResult.Yes)
            {
                var codes = cts.Select(x => x.Code);
                string dbString1 = $"UPDATE {TDKH.TBL_ChiTietCongTacTheoKy} " +
                                    $"SET TrangThai = \"Đang thực hiện\" WHERE TrangThai = \"Chưa thực hiện\" " +
                                    $"AND Code IN ({MyFunction.fcn_Array2listQueryCondition(codes)})";

                DataProvider.InstanceTHDA.ExecuteNonQuery(dbString1);
            }
                
            Worksheet ws = SharedControls.spsheet_TD_KH_LapKeHoach.Document.Worksheets[TDKH.SheetName_KeHoachKinhPhi];
            Dictionary<string, string> dic = MyFunction.fcn_getDicOfColumn(ws.GetUsedRange());
            var colCode = ws.Columns[dic[TDKH.COL_Code]];
            ws.Workbook.BeginUpdate();
////          ws.Workbook.History.IsEnabled = false;
            List<string> updates = new List<string>();

            WaitFormHelper.ShowWaitForm("Đang cập nhật...");

            foreach (var ct in cts)
            {
                string nbdStr = ct.NBDThiCong.Value.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE);
                string nktStr = ct.NKTThiCong.Value.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE);
                if (ct.KhoiLuongNhom.HasValue)
                {
                    string dbString = $"UPDATE {TDKH.TBL_NhomCongTac} " +
                                        $"SET NgayBatDau = '{nbdStr}', NgayKetThuc = '{nktStr}' " +
                                        $"WHERE Code = '{ct.Code}'";
                    DataProvider.InstanceTHDA.ExecuteNonQuery(dbString);

                    
                    DoBocChuanHelper.CapNhatNhanCongNhom(ct.Code, ct.KhoiLuongNhom.Value, ct.ChiTietCongTac.dicNCHangNgay);
                }
                else
                {

                    //var ind = colCode.Search(ct.Code, MyConstant.MySearchOptions).Single().RowIndex;
                    //var row = ws.Rows[ind];

                    //row[dic[TDKH.COL_NgayBatDau]].SetValue(ct.NBDThiCong);
                    //row[dic[TDKH.COL_NgayKetThuc]].SetValue(ct.NKTThiCong);

                    string dbString = $"UPDATE {TDKH.TBL_ChiTietCongTacTheoKy} " +
                        $"SET NgayBatDau = '{nbdStr}', NgayKetThuc = '{nktStr}' " +
                        $"WHERE Code = '{ct.Code}'";

                    updates.Add(dbString);
                    DoBocChuanHelper.CapNhatNhanCong(ct.Code, ct.ChiTietCongTac.dicNCHangNgay);
                }

            }

            if (updates.Any())
            {
                DataProvider.InstanceTHDA.ExecuteNonQuery(string.Join(";\r\n", updates));
            }
            //TDKHHelper.LoadCongTacDoBoc();
            ////          ws.Workbook.History.IsEnabled = false;
            ws.Workbook.EndUpdate();
            //TDKHHelper.TinhLaiToanBoKhoiLuongKeHoach(cts.Select(x => x.Code.ToString()));

            WaitFormHelper.CloseWaitForm();
            DialogResult = DialogResult.OK;
            this.Close();

        }

        private void de_NBD_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                DateTime dateBD = de_NBD.DateTime;
                DateTime dateKT = de_NKT.DateTime;
                nud_SoNgay.ValueChanged -= nud_SoNgay_ValueChanged;
                nud_SoNgay.Value = (dateKT - dateBD).Days + 1;
                nud_SoNgay.ValueChanged += nud_SoNgay_ValueChanged;

            }
            catch
            {

            }
        }

        private void de_NKT_EditValueChanged(object sender, EventArgs e)
        {
            DateTime dateBD = de_NBD.DateTime;
            DateTime dateKT = de_NKT.DateTime;
            nud_SoNgay.ValueChanged -= nud_SoNgay_ValueChanged;
            nud_SoNgay.Value = (dateKT - dateBD).Days + 1;
            nud_SoNgay.ValueChanged += nud_SoNgay_ValueChanged;

        }
    }
}
