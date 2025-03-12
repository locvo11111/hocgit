using DevExpress.CodeParser;
using DevExpress.XtraEditors;
using DevExpress.XtraPrinting;
using DevExpress.XtraSplashScreen;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Nodes;
using MoreLinq;
using PhanMemQuanLyThiCong;
using PhanMemQuanLyThiCong.Constant;
using PhanMemQuanLyThiCong.Common.Helper;
//using PM360.Common.Constaint;
using PM360.Common.Helper;
//using PM360.DAL.Interfaces;
//using PM360.DAO.Models;
//using PM360.DAO.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;
using Unity;
using PhanMemQuanLyThiCong.Common.Constant;
using PhanMemQuanLyThiCong.Model;

namespace PhanMemQuanLyThiCong
{
    public partial class Frm_TraCuuThuCong : DevExpress.XtraEditors.XtraForm
    {
        public delegate void SenData(List<CongTac> lstDatas);
        public SenData senData;
        List<CongTac> _congTacs;

        bool _isMacDinh = true;

        int _soDM = 5;
        List<CTDinhMuc> _ctDinhMuc = null;

        public Frm_TraCuuThuCong(List<CongTac> lstDatas)
        {
            InitializeComponent();
            _congTacs = lstDatas;
        }

        //List<CongTac> lsDinhMucAll = new List<CongTac>();
        private void Frm_TraCuuThuCong_Load(object sender, EventArgs e)
        {
            WaitFormHelper.ShowWaitForm("Đang chuẩn bị dữ liệu định mức chuẩn");
            if (BaseFrom.DinhMucTraCuus is null)
            {
                string dbString = "SELECT " +
                    "MaDinhMuc AS MaHieuCongTac, TenDinhMuc AS TenCongTac, LoaiTT, TenNKThiCongThucTe, DonVi, CTDinhMuc, LoaiDinhMuc, TrinhTuThiCong, TGNTR1, TGNTR3, TGNTR7, TGNTR14, TGNTR21, TGNTR28, BBTruocNT, BBSauNT, NhanCongNgayTamTinh, MayTamTinh, SoNgayTiepTheo, NhomBienBanChinh, NhomBienBanPhu AS NhomMauBienBan, TenBBVatLieu, GhiChu " +
                    "FROM Tbl_DinhMucAll " +
                    "WHERE (CTDinhMuc != 'Người dùng') OR (MaDinhMuc = 'TT')";
                BaseFrom.DinhMucTraCuus = DataProvider.InstanceTBT.ExecuteQueryModel<CongTac>(dbString);
                foreach (var dm in BaseFrom.DinhMucTraCuus)
                {
                    string tenDinhMucGoc = dm.TenCongTac.ToLower();
                    string tenDinhMuc = tenDinhMucGoc.ReplaceFromDic();
                    dm.TenCongTacCompare = tenDinhMuc;
                }
            }
            //congTacsWithDM.Clear();



            Form_CaiDatTraCuuThuCong frmCaiDat = new Form_CaiDatTraCuuThuCong(true);
            frmCaiDat.send = new Form_CaiDatTraCuuThuCong.SendData(Received_Setting);
            WaitFormHelper.CloseWaitForm();
            frmCaiDat.ShowDialog();
            //congTacBindingSource.DataSource = congTacsWithDM;
            //LoadData();
        }

        private void bt_Setting_Click(object sender, EventArgs e)
        {
            Form_CaiDatTraCuuThuCong frmCaiDat = new Form_CaiDatTraCuuThuCong(false, _isMacDinh, _soDM, _ctDinhMuc);
            frmCaiDat.send = new Form_CaiDatTraCuuThuCong.SendData(Received_Setting);
            frmCaiDat.ShowDialog();
        }

        private void Received_Setting(bool isMacDinh, int soDM, List<CTDinhMuc> cTDinhMucs)
        {
            _soDM = soDM;
            _ctDinhMuc = cTDinhMucs;
            _isMacDinh = isMacDinh;
            string[] lsCTDM = _ctDinhMuc.Where(x => x.chon).Select(x => x.ctDinhMuc).ToArray();

            List<CongTac> lsDinhMuc = BaseFrom.DinhMucTraCuus.Where(x => lsCTDM.Contains(x.CTDinhMuc) || x.MaHieuCongTac == "TT").Select(x => (CongTac)x.Clone()).ToList();

            lsDinhMuc.ForEach(dm => dm.UuTien = Array.IndexOf(lsCTDM, dm.CTDinhMuc));

            List<CongTac> congTacsWithDM = new List<CongTac>();

            WaitFormHelper.ShowWaitForm("Đang tra cứu");

            CongTac ttGoc = lsDinhMuc.Where(x => x.MaHieuCongTac == "TT").FirstOrDefault();
            int count = 0;
            foreach (var item in _congTacs)
            {
                ttGoc.Code = Guid.NewGuid().ToString();

                ttGoc.ParentId = item.Code;
                ttGoc.IdCongTacGd = item.IdCongTacGd;

                WaitFormHelper.ShowWaitForm($"{count++}: {item.TenCongTac}");
                string tenCongTacGoc = item.TenCongTac;

                tenCongTacGoc = tenCongTacGoc.Trim().ToLower();
                if (Regex.IsMatch(tenCongTacGoc, @"cung cấp|kẻ joint|cầu chắn rác|lưới chắn rác"))
                {
                    congTacsWithDM.Add(item);
                    var tt1 = (CongTac)ttGoc.Clone();
                    if (tt1 is null)
                        continue;
                    tt1.Chon = true;
                    congTacsWithDM.Add(tt1);
                    continue;
                }

                tenCongTacGoc = tenCongTacGoc.ReplaceFromDic();

                string[] splitTenCT = tenCongTacGoc.Split(' ');
                //CongTac[] DinhMucAllTemp = lsDinhMuc.ToArray();

                //Xếp trùng nhóm
                Match match = Regex.Match(tenCongTacGoc, @"(\s+|^)(pvc|upvc|hdpe|pppr)(\s+|$)");
                CongTac[] rowsDM = lsDinhMuc.ToArray();
                if (match.Length > 0)
                {
                    string val = match.Groups[2].Value;
                    if (val == "upvc")
                        val = "pvc";

                    rowsDM = lsDinhMuc.Where(x => Regex.IsMatch(x.TenCongTacCompare.ToString(), $@"(\s+|^)({val})(\s+|$)")).ToArray();
                    if (rowsDM.Length == 0)
                    {
                        continue;
                    }
                }

                foreach (CongTac dm in rowsDM)
                {
                    string tenDinhMuc = dm.TenCongTacCompare.ToString();

                    string[] splitThamChieu = tenDinhMuc.Split(' ');
                    List<string> key = new List<string>();

                    foreach (string str in splitTenCT)
                    {
                        if (splitThamChieu.Contains(str))
                        {
                            key.Add(str);
                        }
                    }

                    foreach (var itemDic in MyConstant.dicCompare)
                    {
                        Match matchCTGoc = Regex.Match(tenCongTacGoc, $@"({itemDic.Key})(<|<=|>|>=)*(\d+(,\d+){0,1})");
                        Match matchDinhMuc = Regex.Match(tenDinhMuc, $@"({itemDic.Key})(<|<=|>|>=)*(\d+(,\d+){0,1})");

                        if (matchCTGoc.Success && matchDinhMuc.Success)
                        {
                            bool isGocCompare = matchCTGoc.Groups[2].Success;
                            bool isDmCompare = matchDinhMuc.Groups[2].Success;

                            if (matchCTGoc.Value == matchDinhMuc.Value && key.Contains(matchCTGoc.Value))
                                continue;

                            double fstValue = double.Parse(matchCTGoc.Groups[3].Value);
                            double sndValue = double.Parse(matchDinhMuc.Groups[3].Value);
                            if (!isGocCompare && !isDmCompare)
                            {
                                dm.SetValueByPropName($"ChenhLech{itemDic.Key}", Math.Abs(sndValue - fstValue));
                            }

                            else if (isGocCompare && isDmCompare)
                            {
                                string fstCompare = matchCTGoc.Groups[2].Value;
                                string sndCompare = matchDinhMuc.Groups[2].Value;

                                if (fstCompare.Contains(sndCompare) || sndCompare.Contains(fstCompare))
                                {
                                    if ((fstCompare == "<" && sndValue >= fstValue)
                                        || (fstCompare == "<=" && sndValue >= fstValue)
                                        || (fstCompare == ">" && sndValue <= fstValue)
                                        || (fstCompare == ">=" && sndValue <= fstValue)
                                        )
                                    {
                                        key.Add(matchCTGoc.Value);
                                    }
                                }
                                else
                                {
                                    key.Clear();
                                    goto doneCompare;
                                }

                            }
                            else if (isDmCompare)
                            {
                                NCalc.Expression exp = new NCalc.Expression($"{fstValue.ToString(CultureInfo.InvariantCulture)} {matchDinhMuc.Groups[2].Value} {sndValue.ToString(CultureInfo.InvariantCulture)}");
                                if ((bool)exp.Evaluate())
                                {
                                    dm.SetValueByPropName($"ChenhLech{itemDic.Key}", Math.Abs(sndValue - fstValue));

                                    key.Add(matchCTGoc.Value);
                                }
                            }
                        }
                    }

                    doneCompare:
                    dm.keysMatch = $"'{string.Join(";", key)}'";
                    dm.keyCount = key.Count;
                    dm.dinhMucLenght = splitThamChieu.Length;
                }

                rowsDM = rowsDM.OrderByDescending(x => x.keyCount).ThenBy(x => x.UuTien)
                    .ThenBy(x => x.ChenhLechfi)
                    .ThenBy(x => x.ChenhLechcao)
                    .ThenBy(x => x.ChenhLechk)
                    .ThenBy(x => x.ChenhLechday)
                    .ThenBy(x => x.ChenhLechsau)
                    .ThenBy(x => x.ChenhLechrong)
                    .ThenBy(x => x.ChenhLechdamnen)
                    .ThenBy(x => x.dinhMucLenght).ToArray();
                var cts = rowsDM.Where(x => (double)(x.keyCount) / (double)(splitTenCT.Length) > 0.5)
                    .ToArray().Take(_soDM);

                //if (cts.Length > 5)
                //    cts = cts.Where(x => x.keyCount >= cts[4].keyCount && x.dinhMucLenght <= cts[4].dinhMucLenght).ToArray();

                if (!cts.Any())
                {
                    cts = rowsDM.Take(_soDM).ToArray();
                    //if (rowsDM.Length > 5)
                    //    cts = rowsDM.Where(x => x.keyCount >= rowsDM[4].keyCount).ToArray();
                }

                Debug.WriteLine(item.Code);
                congTacsWithDM.Add((CongTac)item.Clone());
                cts = cts.Select(x => (CongTac)x.Clone()).ToArray();
                cts.First().Chon = true;
                cts.ForEach(x =>
                {
                    //if (string.IsNullOrEmpty(x.ID))
                    x.Code = Guid.NewGuid().ToString();
                    Debug.WriteLine($"\t\t{x.Code}");
                    x.ParentId = item.Code;
                    x.IdCongTacGd = item.IdCongTacGd;

                    x.MaGiaiDoan = item.MaGiaiDoan;
                    x.MaHangMuc = item.MaHangMuc;
                    congTacsWithDM.Add(x);
                });
                var tt = (CongTac)ttGoc.Clone();
                congTacsWithDM.Add((CongTac)tt.Clone());
            }
            treeList.DataSource = null;
            treeList.DataSource = congTacsWithDM;
            treeList.ExpandAll();
            treeList.BestFitColumns();
            //LoadData();
            WaitFormHelper.CloseWaitForm();

        }

        public void LoadData()
        {
            treeList.DataSource = congTacBindingSource;
            treeList.ForceInitialize();
            treeList.ExpandAll();
            treeList.BestFitColumns();
        }


        private void btn_OK_Click(object sender, EventArgs e)
        {
            List<CongTac> congTacsWithDM = treeList.DataSource as List<CongTac>;

            senData(congTacsWithDM.FindAll(x => x.Chon && !string.IsNullOrEmpty(x.ParentId)));
            this.Close();
        }

        private void Frm_TraCuuThuCong_FormClosed(object sender, FormClosedEventArgs e)
        {
            //senData(congTacs.FindAll(x => x.Chon && !string.IsNullOrEmpty(x.ParentId)));
            this.Close();
        }

        private void treeList_FocusedNodeChanged(object sender, DevExpress.XtraTreeList.FocusedNodeChangedEventArgs e)
        {

        }

        private void treeList_NodeCellStyle(object sender, GetCustomNodeCellStyleEventArgs e)
        {
            if (e.Node.Level == 0)
            {
                e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold);
            }
        }
    }
}