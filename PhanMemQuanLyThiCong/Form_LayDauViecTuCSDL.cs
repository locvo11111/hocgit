using DevExpress.CodeParser;
using DevExpress.XtraRichEdit.Fields;
using DevExpress.XtraTreeList.Nodes;
using MoreLinq.Extensions;
using PhanMemQuanLyThiCong.Common.Constant;
using PhanMemQuanLyThiCong.Common.Helper;
using PhanMemQuanLyThiCong.Constant;
using PhanMemQuanLyThiCong.Model;
using PhanMemQuanLyThiCong.Model.TDKH;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PhanMemQuanLyThiCong
{
    public partial class Form_LayDauViecTuCSDL : Form
    {
        //DataProvider m_db = new DataProvider("");
        int _type = -1;
        string  _codeKy,_TenForm, _codeDA;
        public Form_LayDauViecTuCSDL(int typeDauViec, string NameNT, string codeHangMuc = null)
        {
            InitializeComponent();
            //DataProvider.InstanceTHDA.changePath(dbPath);
            _type = typeDauViec;
            //_codeCT = codeCongTrinh;
            //_codeHM = codeHM;
            _codeKy = SharedControls.cbb_DBKH_ChonDot.SelectedValue as string;
            _codeDA = SharedControls.slke_ThongTinDuAn.EditValue as string;
            _TenForm = NameNT;

            if (_type == MyConstant.CONST_TYPE_LAYDAUVIEC_OnlyName || _type == MyConstant.CONST_TYPE_LAYDAUVIEC_FromGiaoViecChung)
            {
                gr_CongTacDaLay.Visible = false;
            }
            else
                ce_LayCongTacMaGop.Visible = false;

            if (_type == MyConstant.CONST_TYPE_LAYDAUVIEC_FromGiaoViecChung)
            {
                bt_Xoa.Visible = true;
            }
            else
                bt_Xoa.Visible = false;


        }

        public delegate void DE_TRUYENDATABANGCONGTAC(LayCongTac[] dt, int type, bool copyNhom);
        public DE_TRUYENDATABANGCONGTAC m_truyenData;
        private void Form_LayDauViecTuCSDL_Load(object sender, EventArgs e)
        {
            if (_type == MyConstant.CONST_TYPE_LAYDAUVIEC_KeHoachTDsangGiaoViec)
            {
                DonViThucHien dvth = SharedControls.ctrl_DonViThucHienGiaoViec.SelectedDVTH;
                if (dvth is null)
                {
                    MessageShower.ShowError("Vui lòng chọn đơn vị thực hiện ở bên trái");
                    return;
                }    

                string colFk = SharedControls.ctrl_DonViThucHienGiaoViec.SelectedDVTH?.ColCodeFK;
                string code = SharedControls.ctrl_DonViThucHienGiaoViec.SelectedDVTH?.Code;
                string dbString = $"SELECT * FROM {TDKH.TBL_ChiTietCongTacTheoKy} WHERE \"CodeGiaiDoan\" = '{_codeKy}' " +
                                    $"AND \"CodeNhaThau\" IS NOT NULL ";

                dbString = $"SELECT cttk.*, dmct.CodePhanTuyen, pt.Ten AS TenPhanTuyen,\r\n" +
                    $"cttk.KhoiLuongHopDongChiTiet AS KhoiLuongHopDong,dmct.TenCongTac, " +
                            $"hm.Ten AS TenHangMuc, " +
                            $"hm.Code AS CodeHangMuc, " +
                            $"ctr.Ten AS TenCongTrinh, " +
                            $"ctr.Code AS CodeCongTrinh, " +
                            $"dmct.KhoiLuongHopDongToanDuAn, " +
                            $"dmct.DonVi, " +
                            $"dmct.MaHieuCongTac," +
                            $"cvc.CodeCongViecCha " +
                            //$"cvc.CodeNhaThauPhu," +
                            //$"cvc.CodeToDoi " +
                            $"FROM {TDKH.TBL_ChiTietCongTacTheoKy} cttk\r\n" +
                            $"JOIN {TDKH.TBL_DanhMucCongTac} dmct\r\n" +
                            $"ON cttk.CodeCongTac = dmct.Code " +
                            $"JOIN {MyConstant.TBL_THONGTINHANGMUC} hm\r\n" +
                            $"ON dmct.CodeHangMuc = hm.Code " +
                            $"JOIN {MyConstant.TBL_THONGTINCONGTRINH} ctr\r\n" +
                            $"ON hm.CodeCongTrinh = ctr.Code " +
                            $"LEFT JOIN {GiaoViec.TBL_CONGVIECCHA} cvc\r\n " +
                            $"ON cttk.Code = cvc.CodeCongTacTheoGiaiDoan " +
                            $"LEFT JOIN {TDKH.Tbl_PhanTuyen} pt\r\n" +
                            $"ON dmct.CodePhanTuyen = pt.Code\r\n" +
                            $"WHERE \"CodeGiaiDoan\" = '{_codeKy}' " +
                            $"AND CodeDuAn = '{_codeDA}' " +
                            $"AND cttk.{colFk} = '{code}' AND cttk.CodeCha IS NULL\r\n" +
                            $"ORDER BY cttk.SortId, " +
                            $"cvc.SortId ASC";

                var dt = DataProvider.InstanceTHDA.ExecuteQueryModel<LayCongTac>(dbString);
                string codeKPT = "KPT";
                foreach (var item in dt.Where(x => x.CodePhanTuyen is null))
                {
                    item.CodePhanTuyen = $"{codeKPT}_{item.CodeHangMuc}";
                    item.TenPhanTuyen = "KHÔNG PHÂN ĐOẠN";
                }                //dt.Columns.Add("Chon", typeof(bool));

                //dt.Columns.Add("KhoiLuongDaGiao", typeof(double));
                //dt.Columns.Add("KhoiLuongConLai", typeof(double));
                //dt.Columns["KhoiLuongConLai"].Expression = "[KhoiLuongToanBo] - [KhoiLuongDaGiao]";

                var listCodeCongTac = dt.AsEnumerable().Select(x => $"'{x.CodeCongTac}'").Distinct().ToList();

                var drsDaLay = dt.Where(x => x.CodeCongViecCha != null).ToList();
                var drsChuaLay = dt.Where(x => x.CodeCongViecCha is null).ToList();

                List<LayCongTac>[] sources =
                {
                    drsDaLay,
                    drsChuaLay
                };

                foreach (var drs in sources)
                {
                    var grsCT = drs.GroupBy(x => new { x.CodeCongTrinh, x.TenCongTrinh });
                    
                    foreach (var grCT in grsCT)
                    {
                        drs.Add(new LayCongTac()
                        {
                            Code = grCT.Key.CodeCongTrinh,
                            TenCongTac = grCT.Key.TenCongTrinh
                        });

                        var grsHM = grCT.GroupBy(x => new { x.CodeHangMuc, x.TenHangMuc });

                        foreach (var grHM in grsHM)
                        {
                            drs.Add(new LayCongTac()
                            {
                                ParentId = grCT.Key.CodeCongTrinh,
                                Code = grHM.Key.CodeHangMuc,
                                TenCongTac = grHM.Key.TenHangMuc
                            });

                            var grsPT = grHM.GroupBy(x => new { x.CodePhanTuyen, x.TenPhanTuyen });

                            foreach (var grPT in grsPT)
                            {
                                drs.Add(new LayCongTac()
                                {
                                    ParentId = grHM.Key.CodeHangMuc,
                                    Code = grPT.Key.CodePhanTuyen,
                                    TenCongTac = grPT.Key.TenPhanTuyen
                                });

                                grPT.ForEach(x =>
                                {
                                    x.ParentId = grPT.Key.CodePhanTuyen;
                                    x.KhoiLuongCanLay = x.KhoiLuongConLai;
                                });
                            }    
                        }

                    }

                }

                gc_CongTacChuaLay.DataSource = new BindingList<LayCongTac>(drsChuaLay);

                gc_CongTacDaLay.DataSource = new BindingList<LayCongTac>(drsDaLay);


            }
            else if (_type == MyConstant.CONST_TYPE_LAYDAUVIEC_DoBocChuan || _type == MyConstant.CONST_TYPE_LAYDAUVIEC_OnlyName)//Lấy công tác tổng cho nhà thầu tổ đội
            {
                this.Text=$"{_TenForm}";

                string dbString = $"SELECT dmct.DonVi, cttk.KhoiLuongToanBo/COALESCE(KhoiLuongToanBo, 1) AS KhoiLuongToanBo, cttk.*, dmct.CodePhanTuyen, pt.Ten AS TenPhanTuyen,\r\n" +
                    $"cttk.KhoiLuongHopDongChiTiet AS KhoiLuongHopDong,dmct.TenCongTac, " +
                            $"hm.Ten AS TenHangMuc, " +
                            $"hm.Code AS CodeHangMuc, " +
                            $"ctr.Ten AS TenCongTrinh, " +
                            $"ctr.Code AS CodeCongTrinh, " +
                            $"dmct.KhoiLuongHopDongToanDuAn, " +
                            //$"dmct.DonVi, " +
                            $"dmct.MaHieuCongTac " +
                            $"FROM {TDKH.TBL_ChiTietCongTacTheoKy} cttk\r\n" +
                            $"JOIN {TDKH.TBL_DanhMucCongTac} dmct\r\n" +
                            $"ON cttk.CodeCongTac = dmct.Code " +
                            $"JOIN {MyConstant.TBL_THONGTINHANGMUC} hm\r\n" +
                            $"ON dmct.CodeHangMuc = hm.Code " +
                            $"JOIN {MyConstant.TBL_THONGTINCONGTRINH} ctr\r\n" +
                            $"ON hm.CodeCongTrinh = ctr.Code " +
                            $"LEFT JOIN {TDKH.Tbl_PhanTuyen} pt\r\n" +
                            $"ON dmct.CodePhanTuyen = pt.Code\r\n" +
                            $"WHERE \"CodeGiaiDoan\" = '{_codeKy}' {{0}} " +
                            $"AND CodeDuAn = '{_codeDA}' " +
                            $"AND \"CodeNhaThau\" IS NOT NULL AND cttk.CodeCha IS NULL " +
                            $"ORDER BY cttk.SortId ASC";

                string fstr = "";
                if (_type == MyConstant.CONST_TYPE_LAYDAUVIEC_OnlyName && !ce_LayCongTacMaGop.Checked)
                {
                    fstr = "AND dmct.CodeGop IS NULL";
                }
                dbString = string.Format(dbString, fstr);
                var dt = DataProvider.InstanceTHDA.ExecuteQueryModel<LayCongTac>(dbString);

                //if (_type == MyConstant.CONST_TYPE_LAYDAUVIEC_OnlyName)
                //{

                //}
                string strCodeCTTheoKy = MyFunction.fcn_Array2listQueryCondition(dt.Select(x => x.Code).ToArray());
                dbString = $"SELECT con.* " +
    $"FROM {TDKH.TBL_ChiTietCongTacTheoKy} con\r\n" +
    $"WHERE \"CodeCha\" IN ({strCodeCTTheoKy}) ORDER BY \"Row\" ASC";

                DataTable ctConChia = DataProvider.InstanceTHDA
                    .ExecuteQuery(dbString);

                string codeKPT = "KPT";
                foreach (var item in dt.Where(x => x.CodePhanTuyen is null))
                {
                    item.CodePhanTuyen = $"{codeKPT}_{item.CodeHangMuc}";
                    item.TenPhanTuyen = "KHÔNG PHÂN ĐOẠN";
                }                //dt.Columns.Add("Chon", typeof(bool));

                var listCodeCongTac = dt.AsEnumerable().Select(x => $"'{x.CodeCongTac}'").Distinct().ToList();

                string colFk = SharedControls.ctrl_DonViThucHienDuAnTDKH.SelectedDVTH.ColCodeFK;
                string code = SharedControls.ctrl_DonViThucHienDuAnTDKH.SelectedDVTH.Code;

                dbString = $"SELECT * FROM {TDKH.TBL_ChiTietCongTacTheoKy} WHERE CodeCongTac IN ({string.Join(", ", listCodeCongTac)}) AND CodeCha IS NULL AND \"CodeNhaThau\" IS NULL";/* +
                                    $"AND {colFk} = '{code}'";*/
                DataTable dtDanhMucCongTac = DataProvider.InstanceTHDA.ExecuteQuery(dbString);
                var listCodeCongTacChia = MyFunction.fcn_Array2listQueryCondition(ctConChia.AsEnumerable().Select(x => x["Code"].ToString()));

                dbString = $"SELECT * FROM {TDKH.TBL_ChiTietCongTacTheoKy} WHERE CodeCongTac IN ({string.Join(", ", listCodeCongTac)}) AND CodeCha IS NOT NULL AND \"CodeNhaThau\" IS NULL";/* +
                                    $"AND {colFk} = '{code}'";*/
                DataTable dtDanhMucCongTacChia = DataProvider.InstanceTHDA.ExecuteQuery(dbString);
                List<LayCongTac> ChiaCTDaLay=new List<LayCongTac>();
                List<LayCongTac> ChiaCTChuaLay=new List<LayCongTac>();
                foreach (var dr in dt)
                {
                    string codeCTac = dr.CodeCongTac.ToString();
                    DataRow[] drsCongTacDaLay = dtDanhMucCongTac.AsEnumerable().Where(x => x["CodeCongTac"].ToString() == codeCTac 
                                                                                        && x[colFk].ToString() == code).ToArray();
                    DataRow[] drsCongTacChuaLay = dtDanhMucCongTac.AsEnumerable().Where(x => x["CodeCongTac"].ToString() == codeCTac 
                                                                                        && x[colFk].ToString() != code).ToArray();

                    if (drsCongTacDaLay.Any())
                    {
                        dr.DaLay = true;
                        continue;
                    }    
                        dr.DaLay = false;


                    //DataRow[] drsDMCT = dt.AsEnumerable().Where(x =>x["CodeCongTac"].ToString() == codeCTac 
                    //                                        && x[colFk].ToString() != code).ToArray();

                    if (drsCongTacChuaLay.Any())
                    {
                        //dr.KhoiLuongDaGiao = drsCongTacChuaLay.Sum(x => double.Parse(x["KhoiLuongToanBo"].ToString()));

                        foreach (var ct in drsCongTacChuaLay)
                        {
                            
                            dr.KhoiLuongDaGiao += double.Parse(ct["KhoiLuongToanBo"].ToString()) / ((ct["HeSoQuyDoiDonVi"] != DBNull.Value) ? double.Parse(ct["HeSoQuyDoiDonVi"].ToString()) : 1.0);
                        }
                    }
                    else
                        dr.KhoiLuongDaGiao = 0;
                }

                var drsDaLay = dt.Where(x => x.DaLay).ToList();
                var drsChuaLay = dt.Where(x => !x.DaLay).ToList();

                List<LayCongTac>[] sources =
                {
                    drsDaLay,
                    drsChuaLay
                };
                foreach (var drs in sources)
                {
                    var grsCT = drs.GroupBy(x => new { x.CodeCongTrinh, x.TenCongTrinh });

                    foreach (var grCT in grsCT)
                    {
                        drs.Add(new LayCongTac()
                        {
                            Code = grCT.Key.CodeCongTrinh,
                            TenCongTac = grCT.Key.TenCongTrinh
                        });

                        var grsHM = grCT.GroupBy(x => new { x.CodeHangMuc, x.TenHangMuc });

                        foreach (var grHM in grsHM)
                        {
                            drs.Add(new LayCongTac()
                            {
                                ParentId = grCT.Key.CodeCongTrinh,
                                Code = grHM.Key.CodeHangMuc,
                                TenCongTac = grHM.Key.TenHangMuc
                            });

                            var grsPT = grHM.GroupBy(x => new { x.CodePhanTuyen, x.TenPhanTuyen });

                            foreach (var grPT in grsPT)
                            {
                                drs.Add(new LayCongTac()
                                {
                                    ParentId = grHM.Key.CodeHangMuc,
                                    Code = grPT.Key.CodePhanTuyen,
                                    TenCongTac = grPT.Key.TenPhanTuyen
                                });
                                foreach(var item in grPT)
                                {
                                    DataRow[] drs_ChiaCT = ctConChia.Select($"[CodeCha] = '{item.Code}'");
                                    foreach(var row in drs_ChiaCT)
                                    {
                                        LayCongTac NewCTChia = new LayCongTac();
                                        NewCTChia=DatatableHelper.fcn_DataTable2List<LayCongTac>(row.Table).FirstOrDefault();
                                        NewCTChia.ParentId = NewCTChia.CodeCha= item.Code;
                                        NewCTChia.Code = row["Code"].ToString();
                                        item.CodeCha = "";
                                        NewCTChia.MaHieuCongTac = item.MaHieuCongTac;
                                        NewCTChia.TenCongTac = item.TenCongTac;
                                        NewCTChia.DonVi = item.DonVi;
                                        NewCTChia.CodeHangMuc = item.CodeHangMuc;
                                        NewCTChia.KhoiLuongToanBo =row["KhoiLuongToanBo"]!=DBNull.Value?double.Parse(row["KhoiLuongToanBo"].ToString()):0;
                                        DataRow[] drsCongTacDaLayChia = dtDanhMucCongTacChia.AsEnumerable().Where(x => x["CodeCongTac"].ToString() == item.CodeCongTac
                                                                                      && x[colFk].ToString() == code).ToArray();
                                        DataRow[] drsCongTacChuaLayChia = dtDanhMucCongTacChia.AsEnumerable().Where(x => x["CodeCongTac"].ToString() == item.CodeCongTac
                                                                                                            && x[colFk].ToString() != code).ToArray();
                                        if (drsCongTacChuaLayChia.Any())
                                        {
                                            foreach (var ct in drsCongTacChuaLayChia)
                                            {

                                                NewCTChia.KhoiLuongDaGiao += double.Parse(ct["KhoiLuongToanBo"].ToString()) / ((ct["HeSoQuyDoiDonVi"] != DBNull.Value) ? double.Parse(ct["HeSoQuyDoiDonVi"].ToString()) : 1.0);
                                            }
                                        }
                                        else
                                            NewCTChia.KhoiLuongDaGiao = 0;
                                        if (item.DaLay)
                                            ChiaCTDaLay.Add(NewCTChia);
                                        else
                                            ChiaCTChuaLay.Add(NewCTChia);
                                        NewCTChia.KhoiLuongCanLay = NewCTChia.KhoiLuongConLai;
                                    }
                                    item.ParentId = grPT.Key.CodePhanTuyen;
                                    item.KhoiLuongCanLay = item.KhoiLuongConLai;
                                }
                                //grPT.ForEach(x =>
                                //{
                                //    x.ParentId = grPT.Key.CodePhanTuyen;
                                //    x.KhoiLuongCanLay = x.KhoiLuongConLai;
                                //});
                            }
                        }

                    }

                }
                if (ChiaCTDaLay.Any())
                    drsDaLay.AddRange(ChiaCTDaLay);        
                if (ChiaCTChuaLay.Any())
                    drsChuaLay.AddRange(ChiaCTChuaLay);
                gc_CongTacChuaLay.DataSource = (drsChuaLay.Any())
                    ? new BindingList<LayCongTac>(drsChuaLay)
                    : null;
                
                gc_CongTacDaLay.DataSource = (drsDaLay.Any())
                    ? new BindingList<LayCongTac>(drsDaLay)
                    : null;
                
            }    
            else if (_type == MyConstant.CONST_TYPE_LAYDAUVIEC_FromGiaoViecChung)
            {
                DonViThucHien dvth = SharedControls.ctrl_DonViThucHienGiaoViec.SelectedDVTH;
                if (dvth is null)
                {
                    MessageShower.ShowError("Vui lòng chọn đơn vị thực hiện ở bên trái");
                    return;
                }

                string colFk = SharedControls.ctrl_DonViThucHienGiaoViec.SelectedDVTH?.ColCodeFK;
                string code = SharedControls.ctrl_DonViThucHienGiaoViec.SelectedDVTH?.Code;
                string dbString = $"SELECT * FROM {TDKH.TBL_ChiTietCongTacTheoKy} WHERE \"CodeGiaiDoan\" = '{_codeKy}' " +
                                    $"AND \"CodeNhaThau\" IS NOT NULL ";

                dbString = $"SELECT cvc.CodeCongViecCha AS Code, cvc.TenCongViec AS TenCongTac, cvc.KhoiLuongKeHoach AS KhoiLuongToanBo, cvc.*, cvc.MaDinhMuc AS MaHieuCongTac " +
                            $"FROM {Server.Tbl_GiaoViec_CongViecCha} cvc\r\n" +
                            $"WHERE CodeHangMuc IS NULL AND CodeDauMuc IS NULL";
                            

                var dt = DataProvider.InstanceTHDA.ExecuteQueryModel<LayCongTac>(dbString);
                gc_CongTacChuaLay.DataSource = new BindingList<LayCongTac>(dt);
                //string codeKPT = "KPT";
                //foreach (var item in dt.Where(x => x.CodePhanTuyen is null))
                //{
                //    item.CodePhanTuyen = $"{codeKPT}_{item.CodeHangMuc}";
                //    item.TenPhanTuyen = "KHÔNG PHÂN ĐOẠN";
                //}                //dt.Columns.Add("Chon", typeof(bool));

                //dt.Columns.Add("KhoiLuongDaGiao", typeof(double));
                //dt.Columns.Add("KhoiLuongConLai", typeof(double));
                //dt.Columns["KhoiLuongConLai"].Expression = "[KhoiLuongToanBo] - [KhoiLuongDaGiao]";

                //var listCodeCongTac = dt.AsEnumerable().Select(x => $"'{x.CodeCongTac}'").Distinct().ToList();

                //var drsDaLay = dt.Where(x => x.CodeCongViecCha != null).ToList();
                //var drsChuaLay = dt.Where(x => x.CodeCongViecCha is null).ToList();

                //List<LayCongTac>[] sources =
                //{
                //    drsDaLay,
                //    drsChuaLay
                //};

                //foreach (var drs in sources)
                //{
                //    var grsCT = drs.GroupBy(x => new { x.CodeCongTrinh, x.TenCongTrinh });

                //    foreach (var grCT in grsCT)
                //    {
                //        drs.Add(new LayCongTac()
                //        {
                //            Code = grCT.Key.CodeCongTrinh,
                //            TenCongTac = grCT.Key.TenCongTrinh
                //        });

                //        var grsHM = grCT.GroupBy(x => new { x.CodeHangMuc, x.TenHangMuc });

                //        foreach (var grHM in grsHM)
                //        {
                //            drs.Add(new LayCongTac()
                //            {
                //                ParentId = grCT.Key.CodeCongTrinh,
                //                Code = grHM.Key.CodeHangMuc,
                //                TenCongTac = grHM.Key.TenHangMuc
                //            });

                //            var grsPT = grHM.GroupBy(x => new { x.CodePhanTuyen, x.TenPhanTuyen });

                //            foreach (var grPT in grsPT)
                //            {
                //                drs.Add(new LayCongTac()
                //                {
                //                    ParentId = grHM.Key.CodeHangMuc,
                //                    Code = grPT.Key.CodePhanTuyen,
                //                    TenCongTac = grPT.Key.TenPhanTuyen
                //                });

                //                grPT.ForEach(x =>
                //                {
                //                    x.ParentId = grPT.Key.CodePhanTuyen;
                //                    x.KhoiLuongCanLay = x.KhoiLuongConLai;
                //                });
                //            }
                //        }

                //    }

                //}
            }
        }

        private void bt_OK_Click(object sender, EventArgs e)
        {
            var dtTable = (gc_CongTacChuaLay.DataSource as BindingList<LayCongTac>);

            if (_type == MyConstant.CONST_TYPE_LAYDAUVIEC_FromGiaoViecChung)
            {
                var drss = dtTable.AsEnumerable().Where(x => x.Chon == true).ToArray();
                m_truyenData(drss, _type, ce_CopyNhom.Checked);
                this.Close();
                return;
            }

            var drs = dtTable.AsEnumerable().Where(x => x.Chon == true && x.CodeHangMuc != null).ToArray();
            var drsHetKhoiLuong = drs.Where(x => x.KhoiLuongConLai <= 0.01).ToList();
            if (drsHetKhoiLuong.Any() && _type == MyConstant.CONST_TYPE_LAYDAUVIEC_DoBocChuan)
            {
                var dr = MessageShower.ShowYesNoQuestion($"Có {drsHetKhoiLuong.Count} công tác đã hết khối lượng để phân khai, bạn có muốn tiếp tục thêm các công tác này không?");
                if (dr == DialogResult.No)
                    drs = drs.Where(x => x.KhoiLuongConLai > 0.01).ToArray();
            }

            if (!drs.Any())
            {
                MessageShower.ShowError("Chưa chọn công tác nào!");
                return;
            }    
            m_truyenData(drs, _type, ce_CopyNhom.Checked);
            this.Close();
        }

        private void bt_checkAll_Click(object sender, EventArgs e)
        {
            gc_CongTacChuaLay.CheckAll();
            //foreach (TreeListNode node in gc_CongTacChuaLay.Nodes)
            //{
            //    if (node.Level == 0)
            //        node.CheckAll();
            //}
            //var dtTable = (gc_CongTacChuaLay.DataSource as BindingList<LayCongTac>);

            //foreach (var Row in dtTable)
            //{
            //    Row.Chon = true;
            //}
            //gc_CongTacChuaLay.RefreshDataSource();

        }

        private void gv_CongTacChuaLay_DataSourceChanged(object sender, EventArgs e)
        {
            gc_CongTacChuaLay.ExpandAll();
        }

        private void gv_CongTacDaLay_DataSourceChanged(object sender, EventArgs e)
        {
            gc_CongTacDaLay.ExpandAll();

        }

        private void gc_CongTacChuaLay_CellValueChanged(object sender, DevExpress.XtraTreeList.CellValueChangedEventArgs e)
        {
            //var crNode = e.Node;
            //string fieldName = e.Column.FieldName;
            //if (fieldName == nameof(LayCongTac.Chon))
            //{
            //    if (e.Value is null)
            //        return;

            //    var node = crNode;
            //    while (node.HasChildren)
            //    {
            //        foreach (var child in node.chi)
            //    }
            //}
        }

        private void gc_CongTacChuaLay_NodeCellStyle(object sender, DevExpress.XtraTreeList.GetCustomNodeCellStyleEventArgs e)
        {
            if (e.Node.Level == 0)
            {
                e.Appearance.ForeColor = MyConstant.color_Row_CongTrinh;
                e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold);
            }
            else if (e.Node.Level == 1)
            {
                e.Appearance.ForeColor = MyConstant.color_Row_HangMuc;
                e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold);
            }
            else if (e.Node.Level == 2)
            {
                e.Appearance.ForeColor = MyConstant.color_Row_PhanTuyen;
                e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold);
            }
        }

        private void gc_CongTacDaLay_NodeCellStyle(object sender, DevExpress.XtraTreeList.GetCustomNodeCellStyleEventArgs e)
        {
            if (e.Node.Level == 0)
            {
                e.Appearance.ForeColor = MyConstant.color_Row_CongTrinh;
                e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold);
            }
            else if (e.Node.Level == 1)
            {
                e.Appearance.ForeColor = MyConstant.color_Row_HangMuc;
                e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold);
            }
        }

        private void gc_CongTacChuaLay_DataSourceChanged(object sender, EventArgs e)
        {
            gc_CongTacChuaLay.ExpandAll();
        }

        private void gc_CongTacDaLay_DataSourceChanged(object sender, EventArgs e)
        {
            gc_CongTacDaLay.ExpandAll();
        }

        private void bt_LayCongTacMaGop_CheckedChanged(object sender, EventArgs e)
        {
            Form_LayDauViecTuCSDL_Load(null, null);
        }

        private void bt_ChonCongTacDaBoiDen_Click(object sender, EventArgs e)
        {
            var nodes = gc_CongTacChuaLay.GetSelectedCells().Select(x => x.Node).Distinct();
            foreach (var node in nodes)
            {
                node.Checked = true;
            }

        }

        private void bt_BoChonCongTacBoiDen_Click(object sender, EventArgs e)
        {
            var nodes = gc_CongTacChuaLay.GetSelectedCells().Select(x => x.Node).Distinct();
            foreach (var node in nodes)
            {
                node.Checked = false;
            }
        }

        private void gc_CongTacChuaLay_SelectionChanged(object sender, EventArgs e)
        {


        }

        private void gc_CongTacChuaLay_AfterCheckNode(object sender, DevExpress.XtraTreeList.NodeEventArgs e)
        {
            LayCongTac lst=gc_CongTacChuaLay.GetRow(e.Node.Id) as LayCongTac;
            if (!string.IsNullOrEmpty(lst.CodeCha))
            {
                TreeListNode Parent = e.Node.ParentNode;
                Parent.Checked = true;
                   
            }

        }

        private void bt_Xoa_Click(object sender, EventArgs e)
        {
            var dr = MessageShower.ShowYesNoQuestion("Xác nhận xóa công tác trong dữ liệu chung?");

            if (dr != DialogResult.Yes)
                return;

            var dtTable = (gc_CongTacChuaLay.DataSource as BindingList<LayCongTac>);

            var codes = dtTable.AsEnumerable().Where(x => x.Chon == true).Select(x => x.CodeCongViecCha).ToArray();
            if (!codes.Any())
            {
                MessageShower.ShowWarning("Chưa chọn công tác nào!");
                return;
            }

            string dbString = $"DELETE FROM {Server.Tbl_GiaoViec_CongViecCha} WHERE CodeCongViecCha IN ({MyFunction.fcn_Array2listQueryCondition(codes)})";
            var num = DataProvider.InstanceTHDA.ExecuteNonQuery(dbString);

            MessageShower.ShowInformation($"Đã xóa {num} Công tác");
            Form_LayDauViecTuCSDL_Load(null, null);
        }

        private void bt_uncheckAll_Click(object sender, EventArgs e)
        {
            gc_CongTacChuaLay.UncheckAll();

        }

        private void gc_CongTacDaLay_Click(object sender, EventArgs e)
        {
            Form_LayDauViecTuCSDL_Load(null, null);
        }
    }
}
