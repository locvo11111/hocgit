using Dapper;
using DevExpress.Spreadsheet;
using DevExpress.Utils.Menu;
using DevExpress.XtraEditors;
using DevExpress.XtraSpreadsheet.Commands;
using PhanMemQuanLyThiCong.Common.Constant;
using PhanMemQuanLyThiCong.Common.Enums;
using PhanMemQuanLyThiCong.Common.Helper;
using PM360.Common.Helper;
using StackExchange.Profiling.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VChatCore.ViewModels.SyncSqlite;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Menu;
using DevExpress.Mvvm.Native;
using DevExpress.CodeParser;
using PhanMemQuanLyThiCong.Model.TDKH;
using DevExpress.XtraTreeList.Nodes;
using DevExpress.Internal.WinApi;
using PhanMemQuanLyThiCong.Model;
using DevExpress.XtraEditors.Repository;

namespace PhanMemQuanLyThiCong.Controls
{
    public partial class ctrl_HaoPhiVatTu : DevExpress.XtraEditors.XtraUserControl
    {
        string _colFk;
        TypeKLHN _type;
        string _codeCongTac = "";
        string _tenCongTac = "LỖI";
        string _codeCTTK = null;

        public ctrl_HaoPhiVatTu()
        {
            InitializeComponent();
        }


        string[] colsWithForeColor =
        {
            nameof(HaoPhiVatTuExtensionViewModel.DonGiaDinhMucGoc),
            nameof(HaoPhiVatTuExtensionViewModel.DonGiaDinhMucGiaoThau),
            nameof(HaoPhiVatTuExtensionViewModel.DonGiaDocVao),
        };

        [DisplayName("ReadOnly")]
        public bool ReadOnly
        {
            get { return tl_haophi.OptionsBehavior.ReadOnly; }
            set { tl_haophi.OptionsBehavior.ReadOnly = value; }
        }

        private string[] colsParent =
        {
            nameof(HaoPhiVatTuExtensionViewModel.VatTu),
            nameof(HaoPhiVatTuExtensionViewModel.DonGiaDinhMucGoc),
            nameof(HaoPhiVatTuExtensionViewModel.DonGiaDinhMucGiaoThau),
            nameof(HaoPhiVatTuExtensionViewModel.DonGiaDocVao),
            "Xoa"

        };

        public void pushData(TypeKLHN type, string codeCongTac, string tenCongTac = "LỖI")
        {
            _type = type;
            _codeCongTac = codeCongTac;
            _tenCongTac = tenCongTac;
            loadData();
        }
        
        public void loadData()
        {
            lb_TenCongTac.Text = $"Hao phí vật tư: {_tenCongTac}";
            tl_haophi.Enabled = true;
            switch (_type)
            {
                case TypeKLHN.GiaoViecCha:
                    _colFk = "CodeCongViecCha";
                    string dbString = $"SELECT CodeCongTacTheoGiaiDoan FROM {GiaoViec.TBL_CONGVIECCHA} " +
                        $"WHERE CodeCongViecCha = '{_codeCongTac}'";
                    DataRow dr = DataProvider.InstanceTHDA.ExecuteQuery(dbString).Rows[0];
                    if (dr[0] != DBNull.Value)
                    {
                        _codeCTTK = (string)dr[0];
                    }
                        break;
                case TypeKLHN.CongTac:
                    _colFk = "CodeCongTac";
                    _codeCTTK = _codeCongTac;

                    dbString = $"SELECT COALESCE(dmct.HasHopDongAB,cttk.HasHopDongAB) AS HasHopDongAB," +
                        $"COALESCE(dmct.PhanTichVatTu,cttk.PhanTichVatTu) AS PhanTichVatTu \r\n" +
                        $"FROM {TDKH.TBL_ChiTietCongTacTheoKy} cttk\r\n" +
                        $"JOIN {TDKH.TBL_DanhMucCongTac} dmct\r\n" +
                        $"ON cttk.CodeCongTac = dmct.Code\r\n" +
                        $"WHERE cttk.Code = '{_codeCTTK}'";
                    DataRow drct = DataProvider.InstanceTHDA.ExecuteQuery(dbString).AsEnumerable().FirstOrDefault();
                    DataTable drctt = DataProvider.InstanceTHDA.ExecuteQuery(dbString);

                    dbString = $"SELECT cttk.*\r\n" +
                        $"FROM {TDKH.TBL_ChiTietCongTacTheoKy} cttk\r\n" +
                        $"WHERE cttk.CodeCha = '{_codeCTTK}'";
                    DataTable drCtChia = DataProvider.InstanceTHDA.ExecuteQuery(dbString);
                    if (drct is null|| drCtChia.Rows.Count!=0)
                    {
                        tl_haophi.Enabled = false;
                        break;
                    }

                    //if (drct.Field<bool>(TDKH.COL_HasHopDongAB) == false && drct.Field<bool>(TDKH.COL_PhanTichVatTu) == false)
                    //{
                    //    tl_haophi.Enabled = false;

                    //}

                    break;
                default:
                    tl_haophi.DataSource = null;
                    return;
            }

            var dtHp = DinhMucHelper.GetModelHaoPhiVatTuHienTai(_type, new string[] { _codeCongTac }, GetAll: true);

            var lsCodeHps = dtHp.Select(x => x.Code);
            foreach (var dr in dtHp)
            {
                if (dr.CodeHaoPhiCha != null && lsCodeHps.Contains(dr.CodeHaoPhiCha))
                {
                    dr.ParentCode = dr.CodeHaoPhiCha;

                }
                else
                    dr.ParentCode = dr.LoaiVatTu;
            }


            string dbString1 = $"SELECT DonGiaVatLieuDocVao, DonGiaNhanCongDocVao, DonGiaMayDocVao FROM {TDKH.TBL_ChiTietCongTacTheoKy} WHERE Code = '{_codeCTTK}'";
            var dtDonGia = DataProvider.InstanceTHDA.ExecuteQuery(dbString1);
            DataRow drDonGia = dtDonGia.AsEnumerable().SingleOrDefault();

            if (drDonGia is null)
            {
                drDonGia = dtDonGia.NewRow();
                drDonGia["DonGiaVatLieuDocVao"] = 0;
                drDonGia["DonGiaNhanCongDocVao"] = 0;
                drDonGia["DonGiaMayDocVao"] = 0;
            }

            var drVL = dtHp.Where(x => x.LoaiVatTu == "Vật liệu").ToArray();
            var drNC = dtHp.Where(x => x.LoaiVatTu == "Nhân công").ToArray();
            var drMay = dtHp.Where(x => x.LoaiVatTu == "Máy thi công").ToArray();


            dtHp.Add(new HaoPhiVatTuExtensionViewModel()
            {
                Code = "Vật liệu",
                VatTu = "Vật liệu",
                DonGiaDocVao = long.Parse(drDonGia["DonGiaVatLieuDocVao"].ToString()),
                DonGiaDinhMucGocCustom = drVL.Sum(x => x.DonGiaDinhMucGoc),
                DonGiaDinhMucGiaoThauCustom = drVL.Sum(x => x.DonGiaDinhMucGiaoThau),
            });

            dtHp.Add(new HaoPhiVatTuExtensionViewModel()
            {
                Code = "Nhân công",
                VatTu = "Nhân công",
                DonGiaDocVao = long.Parse(drDonGia["DonGiaNhanCongDocVao"].ToString()),
                DonGiaDinhMucGocCustom = drNC.Sum(x => x.DonGiaDinhMucGoc),
                DonGiaDinhMucGiaoThauCustom = drNC.Sum(x => x.DonGiaDinhMucGiaoThau),

            });

            dtHp.Add(new HaoPhiVatTuExtensionViewModel()
            {
                Code = "Máy thi công",
                VatTu = "Máy thi công",
                DonGiaDocVao = long.Parse(drDonGia["DonGiaMayDocVao"].ToString()),
                DonGiaDinhMucGocCustom = drMay.Sum(x => x.DonGiaDinhMucGoc),
                DonGiaDinhMucGiaoThauCustom = drMay.Sum(x => x.DonGiaDinhMucGiaoThau),

            });

            tl_haophi.DataSource = new BindingList<HaoPhiVatTuExtensionViewModel>(dtHp);
        }

        public void Clear()
        {
            lb_TenCongTac.Text = "Chưa chọn công tác";
            tl_haophi.DataSource = null;
        }

        private void gv_HaoPhiDinhMuc_CellValueChanged(object sender, CellValueChangedEventArgs e)
        {
            var gv = tl_haophi;
            //if (gv.FocusedColumn.ColumnEdit != null)
            var crNode = e.Node;

            string[] colsNeedSum =
            {
                nameof(HaoPhiVatTuExtensionViewModel.DonGiaDinhMucGoc),
                nameof(HaoPhiVatTuExtensionViewModel.DonGiaDinhMucGiaoThau),
            };

            var hpvt = gv.GetFocusedRow() as HaoPhiVatTuExtensionViewModel;
            string code = hpvt.Code;// dgv_TDKH_KH_HaoPhi.Rows[e.RowIndex].Cells["Code"].Value.ToString();
            string colName = gv.FocusedColumn.FieldName;
            
            string dbString = $"UPDATE {TDKH.Tbl_HaoPhiVatTu} SET \"{colName}\" = @Value " +
                $"WHERE Code = @Code";

            if (DataProvider.InstanceTHDA.ExecuteNonQuery(dbString, parameter: new object[] {e.Value, code}) == 0)
            {
                //MessageShower.ShowInformation("Lỗi cập nhật vật tư");
            }

            tl_haophi.CellValueChanged -= gv_HaoPhiDinhMuc_CellValueChanged;
            string DMND = nameof(HaoPhiVatTuExtensionViewModel.DinhMucNguoiDung);
            List<string> lsCode = new List<string>() { hpvt.Code };

            if (crNode.Level == 2 && gv.FocusedColumn.FieldName == DMND)
            {
                var allSameNode = crNode.ParentNode.Nodes;
                var last = allSameNode.Last(x => x != crNode);
                double sum = (double)crNode.ParentNode.GetValue(DMND);
                double newVal = sum - (allSameNode.Where(x => x != last).Sum(x => (double)x.GetValue(DMND)));

                last.SetValue(DMND, newVal);
                string codeLast = (string)last.GetValue("Code");
                lsCode.Add(codeLast);
                dbString = $"UPDATE {TDKH.Tbl_HaoPhiVatTu} SET {DMND} = @Value " +
                                        $"WHERE Code = @codeLast";

                DataProvider.InstanceTHDA.ExecuteNonQuery(dbString, parameter: new object[] { newVal, codeLast });
            }

            if (crNode.Nodes.Any())
            {
                var nodes = crNode.Nodes;
                //string HSND = nameof(HaoPhiVatTuExtensionViewModel.HeSoNguoiDung);
                //string HSND = nameof(HaoPhiVatTuExtensionViewModel.HeSoNguoiDung);
                
                List<string> codes = new List<string>();
                if (colName == nameof(HaoPhiVatTuExtensionViewModel.HeSoNguoiDung) || colName == nameof(HaoPhiVatTuExtensionViewModel.PhanTichKeHoach))
                {
                    foreach (TreeListNode node in nodes)
                    {
                        codes.Add((string)node.GetValue("Code"));
                        node.SetValue(colName, e.Value);
                    }
                    dbString = $"UPDATE {TDKH.Tbl_HaoPhiVatTu} SET {colName} = @Value WHERE Code IN ({MyFunction.fcn_Array2listQueryCondition(codes)})";
                    DataProvider.InstanceTHDA.ExecuteNonQuery(dbString, parameter: new object[] { e.Value });

                }
                else if (colName == DMND)
                {
                    var allSameNode = crNode.Nodes;
                    var last = allSameNode.Last();
                    double sum = (double)crNode.ParentNode.GetValue(DMND);
                    double newVal = sum - (allSameNode.Where(x => x != last).Sum(x => (double)x.GetValue(DMND)));

                    last.SetValue(DMND, newVal);
                    string codeLast = (string)last.GetValue("Code");
                    lsCode.Add(codeLast);
                    dbString = $"UPDATE {TDKH.Tbl_HaoPhiVatTu} SET {DMND} = @Value " +
                                            $"WHERE Code = @Code";

                    DataProvider.InstanceTHDA.ExecuteNonQuery(dbString, parameter: new object[] { newVal, codeLast });

                }
                lsCode.AddRange(codes);
            }
            
            
            string tenVatTu = null, maVatTu = null;

            if (colName == nameof(HaoPhiVatTuExtensionViewModel.VatTu) || colName == nameof(HaoPhiVatTuExtensionViewModel.DonGia) || colName == nameof(HaoPhiVatTuExtensionViewModel.DonVi))
                maVatTu = (string)crNode.GetValue(nameof(HaoPhiVatTuExtensionViewModel.MaVatLieu));
            else if (colName == nameof(HaoPhiVatTuExtensionViewModel.VatTu))
            {
                tenVatTu = (string)crNode.GetValue(nameof(HaoPhiVatTuExtensionViewModel.VatTu));
                
            } 
            
           

            if (hpvt.CodeCongTac != null)
                TDKHHelper.CapNhatAllVatTuHaoPhi(lsCode, MaVatTu: new List<string> { maVatTu }, tenVatTu: new string[] { tenVatTu }, CodesHangMuc: new string[] { hpvt.CodeHangMuc }, new string[] { hpvt.CodePhanTuyen });

            foreach (string col in colsNeedSum)
            {
                var parentNode = e.Node.ParentNode;
                //var parentRecored = tl_haophi.GetDataRecordByNode(parentNode) as HaoPhiVatTuExtensionViewModel;
                long val = 0;
                foreach (var node in parentNode.Nodes.ToArray())
                {
                    val += (long)node.GetValue(col);
                }
                parentNode.SetValue($"{col}Custom", val);
            }
            tl_haophi.CellValueChanged += gv_HaoPhiDinhMuc_CellValueChanged;

        }

        private void repo_ce_PhanTichKeHoach_CheckedChanged(object sender, EventArgs e)
        {
            tl_haophi.CloseEditor();
        }

        private void gv_HaoPhiDinhMuc_PopupMenuShowing(object sender, PopupMenuShowingEventArgs e)
        {
            if (tl_haophi.DataSource is null)
                return;
            var crNode = tl_haophi.FocusedNode;

            var RecalOthes = new DXMenuItem("TÍNH LẠI VẬT LIỆU KHÁC, MÁY KHÁC", 
                (s, g) =>
                {
                    TDKHHelper.CalcOthersMaterials();
                    loadData();
                });

            RecalOthes.Appearance.Font = new Font(RecalOthes.Appearance.Font, FontStyle.Bold);
            RecalOthes.Appearance.ForeColor = Color.Green;
            e.Menu.Items.Add(RecalOthes);

            if (e.MenuType == DevExpress.XtraTreeList.Menu.TreeListMenuType.Node && crNode.Level == 1)
            {
                
                HaoPhiVatTuExtensionViewModel dr = tl_haophi.GetFocusedRow() as HaoPhiVatTuExtensionViewModel;

                var BoVatTuCacCongTacKhac = new DXMenuItem("Bỏ vật tư này ở các công tác khác trong Hạng Mục", fcn_handle_dgv_TDKH_HaoPhi_BoVatTuOcacCongTacKhac);

                BoVatTuCacCongTacKhac.Appearance.Font = new Font(BoVatTuCacCongTacKhac.Appearance.Font, FontStyle.Bold);
                BoVatTuCacCongTacKhac.Appearance.ForeColor = Color.Blue;

                e.Menu.Items.Add(BoVatTuCacCongTacKhac);

                var ChonVatTuCacCongTacKhac = new DXMenuItem("Chọn vật tư này ở công tác khác trong Hạng Mục", fcn_handle_dgv_TDKH_HaoPhi_ChonVatTuOcacCongTacKhac);

                ChonVatTuCacCongTacKhac.Appearance.Font = new Font(ChonVatTuCacCongTacKhac.Appearance.Font, FontStyle.Bold);
                ChonVatTuCacCongTacKhac.Appearance.ForeColor = Color.Blue;

                e.Menu.Items.Add(ChonVatTuCacCongTacKhac);


                var BoVTKhacVatTuCacCongTacKhac = new DXMenuItem("Bỏ TẤT CẢ \"vật liệu, nhân công, máy\" ở TẤT CẢ các công tác khác trong hạng mục", fcn_handle_dgv_TDKH_HaoPhi_BoVatTuKhacOcacCongTacKhac);

                BoVTKhacVatTuCacCongTacKhac.Appearance.Font = new Font(BoVTKhacVatTuCacCongTacKhac.Appearance.Font, FontStyle.Bold);
                BoVTKhacVatTuCacCongTacKhac.Appearance.ForeColor = Color.Green;

                e.Menu.Items.Add(BoVTKhacVatTuCacCongTacKhac);
                
                var ChonVTKhacVatTuCacCongTacKhac = new DXMenuItem("Chọn TẤT CẢ \"vật liệu, nhân công, máy\" ở TẤT CẢ các công tác khác trong hạng mục", fcn_handle_dgv_TDKH_HaoPhi_ChonVatTuKhacOcacCongTacKhac);

                ChonVTKhacVatTuCacCongTacKhac.Appearance.Font = new Font(ChonVTKhacVatTuCacCongTacKhac.Appearance.Font, FontStyle.Bold);
                ChonVTKhacVatTuCacCongTacKhac.Appearance.ForeColor = Color.Green;

                e.Menu.Items.Add(ChonVTKhacVatTuCacCongTacKhac);
                
                var BoLoaiVTCacCongTacKhac = new DXMenuItem($"Bỏ {dr.LoaiVatTu.ToUpper()} khác ở công tác khác trong Hạng Mục", fcn_handle_dgv_TDKH_HaoPhi_BoLoaiVTKhacOcacCongTacKhac);

                BoLoaiVTCacCongTacKhac.Appearance.Font = new Font(BoLoaiVTCacCongTacKhac.Appearance.Font, FontStyle.Bold);
                BoLoaiVTCacCongTacKhac.Appearance.ForeColor = Color.Violet;

                e.Menu.Items.Add(BoLoaiVTCacCongTacKhac);

                var ChonLoaiVTCacCongTacKhac = new DXMenuItem($"Chọn {dr.LoaiVatTu.ToUpper()} khác ở công tác khác trong Hạng Mục", fcn_handle_dgv_TDKH_HaoPhi_ChonLoaiVTTuKhacOcacCongTacKhac);

                ChonLoaiVTCacCongTacKhac.Appearance.Font = new Font(ChonLoaiVTCacCongTacKhac.Appearance.Font, FontStyle.Bold);
                ChonLoaiVTCacCongTacKhac.Appearance.ForeColor = Color.Violet;

                e.Menu.Items.Add(ChonLoaiVTCacCongTacKhac);

                //var QuyDoiVatLieu = new DXMenuItem($"Quy đổi {dr.LoaiVatTu.ToUpper()} khác ở công tác khác trong Hạng Mục", fcn_handle_dgv_TDKH_HaoPhi_ChonLoaiVTTuKhacOcacCongTacKhac);

                //ChonLoaiVTCacCongTacKhac.Appearance.Font = new Font(ChonLoaiVTCacCongTacKhac.Appearance.Font, FontStyle.Bold);
                //ChonLoaiVTCacCongTacKhac.Appearance.ForeColor = Color.Violet;

                //e.Menu.Items.Add(ChonLoaiVTCacCongTacKhac);


                //e.Menu.Items.Add(new DXMenuItem("Bỏ vật tư này ở các công tác khác trong Hạng Mục", fcn_handle_dgv_TDKH_HaoPhi_LayToanBoVatTuKhac));


                e.Menu.Items.Add(new DXMenuItem("Bỏ các vật tư khác", fcn_handle_dgv_TDKH_HaoPhi_BoVatTuKhac));
                e.Menu.Items.Add(new DXMenuItem("Chỉ lấy các vật tư khác", fcn_handle_dgv_TDKH_HaoPhi_LayToanBoVatTuKhac));
            }

            if (e.Menu is null)
                e.Menu = new TreeListMenu(tl_haophi);
            
            e.Menu.Items.Add(new DXMenuItem("Thêm vật tư", fcn_handle_dgv_TDKH_HaoPhi_ThemVatTu));

            //var crNode = tl_haophi.GetFocusedRow();
            if (crNode is null)
                return;

            if (crNode.Level == 1)
            {
                var subMenu = new DXSubMenuItem("CHIA NHỎ VẬT TƯ");
                subMenu.BeginGroup = true;
                e.Menu.Items.Add(subMenu);
                var textEdit = new RepositoryItemTextEdit();
                textEdit.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
                textEdit.Mask.EditMask = "N0";
                textEdit.EditValueChangedDelay = 0;
                textEdit.EditValueChangedFiringMode = DevExpress.XtraEditors.Controls.EditValueChangedFiringMode.Buffered;
                subMenu.Items.Add(new DXEditMenuItem("", textEdit,
                    new EventHandler(fcn_handle_dgv_TDKH_HaoPhi_ThemVatTuCon), null, null, 100, 0));

                //e.Menu.Items.Add(new DXMenuItem("CHIA NHỎ VẬT TƯ", fcn_handle_dgv_TDKH_HaoPhi_ThemVatTuCon));

                if (crNode.HasChildren)
                    e.Menu.Items.Add(new DXMenuItem("CHIA ĐỀU LẠI VẬT TƯ PHỤ", fcn_handle_dgv_TDKH_HaoPhi_ChiaDeu));

            }



            e.Menu.Items.Add(new DXMenuItem("Lấy lại hao phí vật tư mặc định", fcn_handle_dgv_TDKH_HaoPhi_GetHaoPhiMacDinh));


            DXSubMenuItem ChonAllVatTu = new DXSubMenuItem("Chọn tất cả");
            
            ChonAllVatTu.Items.Add(new DXMenuItem("Tất cả vật tư", fcn_handle_dgv_TDKH_HaoPhi_LayAll));
            ChonAllVatTu.Items.Add(new DXMenuItem("Vật liệu", fcn_handle_dgv_TDKH_HaoPhi_LayVL));
            ChonAllVatTu.Items.Add(new DXMenuItem("Nhân công", fcn_handle_dgv_TDKH_HaoPhi_LayNC));
            ChonAllVatTu.Items.Add(new DXMenuItem("Máy", fcn_handle_dgv_TDKH_HaoPhi_LayMTC));
            e.Menu.Items.Add(ChonAllVatTu);


            DXSubMenuItem BoAllVatTu = new DXSubMenuItem("Bỏ chọn tất cả");

            BoAllVatTu.Items.Add(new DXMenuItem("Tất cả vật tư", fcn_handle_dgv_TDKH_HaoPhi_BoAll));
            BoAllVatTu.Items.Add(new DXMenuItem("Vật liệu", fcn_handle_dgv_TDKH_HaoPhi_BoVL));
            BoAllVatTu.Items.Add(new DXMenuItem("Nhân công", fcn_handle_dgv_TDKH_HaoPhi_BoNC));
            BoAllVatTu.Items.Add(new DXMenuItem("Máy", fcn_handle_dgv_TDKH_HaoPhi_BoMTC));
            e.Menu.Items.Add(BoAllVatTu);
            //e.Menu.Items.Add(new MenuItem("Paste"));
        }

        private void SetOnlyCrHaoPhiState(bool newstate, string LoaiVatTu, bool ExceptCr)
        {
            HaoPhiVatTuExtensionViewModel dr1 = tl_haophi.GetFocusedRow() as HaoPhiVatTuExtensionViewModel;
            string crCode = dr1?.Code;

            var dt = tl_haophi.DataSource as BindingList<HaoPhiVatTuExtensionViewModel>;
            var querys = new List<string>();
            HaoPhiVatTuExtensionViewModel[] drs;

            if (LoaiVatTu is null)
                drs = dt.ToArray();
            else
                drs = dt.Where(x => x.LoaiVatTu == LoaiVatTu).ToArray();


            foreach (var dr in drs)
            {
                string code = dr.Code;
                bool state = (ExceptCr && code == crCode) ? !newstate : newstate;
                dr.PhanTichKeHoach = state;
                querys.Add($"UPDATE {TDKH.Tbl_HaoPhiVatTu} SET PhanTichKeHoach = {(state?1:0)} WHERE Code = '{code}'");
            }
            DataProvider.InstanceTHDA.ExecuteNonQuery(string.Join(";\r\n", querys));
            tl_haophi.RefreshDataSource();

            if (_codeCTTK != null)
                TDKHHelper.TinhLaiToanBoKhoiLuongKeHoach(new string[] { _codeCTTK });
        }

        
        private void fcn_handle_dgv_TDKH_HaoPhi_BoVatTuOcacCongTacKhac(object sender, EventArgs e)
        {
            SetPhanTichKeHoachVatTu(false);
        }

        private void fcn_handle_dgv_TDKH_HaoPhi_ChonVatTuOcacCongTacKhac(object sender, EventArgs e)
        {
            SetPhanTichKeHoachVatTu(true);
        }
        
        private void fcn_handle_dgv_TDKH_HaoPhi_BoVatTuKhacOcacCongTacKhac(object sender, EventArgs e)
        {
            SetPhanTichKeHoachVatTuChoVLKhac(false);
        }
        
        private void fcn_handle_dgv_TDKH_HaoPhi_ChonVatTuKhacOcacCongTacKhac(object sender, EventArgs e)
        {
            SetPhanTichKeHoachVatTuChoVLKhac(true);
        }
        
        private void fcn_handle_dgv_TDKH_HaoPhi_BoLoaiVTKhacOcacCongTacKhac(object sender, EventArgs e)
        {
            SetPhanTichKeHoachVatTuChoVLKhac(false, true);
        }
        
        private void fcn_handle_dgv_TDKH_HaoPhi_ChonLoaiVTTuKhacOcacCongTacKhac(object sender, EventArgs e)
        {
            SetPhanTichKeHoachVatTuChoVLKhac(true, true);
        }

        private void SetPhanTichKeHoachVatTuChoVLKhac(bool state, bool loaiVL = false)
        {
            HaoPhiVatTuExtensionViewModel dr = tl_haophi.GetFocusedRow() as HaoPhiVatTuExtensionViewModel;
            int isCheck = (state) ? 1 : 0;
            if (dr.CodeCongTac.HasValue())
            {
                DataTable dtCtacs = TDKHHelper.GetCongTacWithTheSameHMAndContractorWith(dr.CodeCongTac);
                var lsCode = dtCtacs.AsEnumerable().Select(x => (string)x["Code"]);

                string dbString = $"UPDATE {TDKH.Tbl_HaoPhiVatTu} SET PhanTichKeHoach = {isCheck} " +
                    $"WHERE CodeCongTac IN ({MyFunction.fcn_Array2listQueryCondition(lsCode)}) " +
                    $"AND " +
                    $"((VatTu != @VatTu OR MaVatLieu != @MaVatLieu) {{0}} )";

                string con = (loaiVL)
                    ? $"AND LoaiVatTu = @LoaiVatTu"
                    : $"OR LoaiVatTu != @LoaiVatTu";
                dbString = string.Format(dbString, con);

                DataProvider.InstanceTHDA.ExecuteNonQuery(dbString, parameter: new object[] {dr.VatTu, dr.MaVatLieu, dr.LoaiVatTu});
            }
            else
            {
                MessageShower.ShowInformation("Tính năng này chỉ áp dụng cho công tác từ Tiến độ kế hoạch");
                return;
            }

            if (_codeCTTK != null)
                TDKHHelper.TinhLaiToanBoKhoiLuongKeHoach(new string[] { _codeCTTK });


            loadData();
        }
        private void SetPhanTichKeHoachVatTu(bool state)
        {
            HaoPhiVatTuExtensionViewModel dr = tl_haophi.GetFocusedRow() as HaoPhiVatTuExtensionViewModel;
            int isCheck = (state) ? 1 : 0;
            if (dr.CodeCongTac.HasValue())
            {
                DataTable dtCtacs = TDKHHelper.GetCongTacWithTheSameHMAndContractorWith(dr.CodeCongTac);
                var lsCode = dtCtacs.AsEnumerable().Select(x => (string)x["Code"]);

                string dbString = $"UPDATE {TDKH.Tbl_HaoPhiVatTu} SET PhanTichKeHoach = {isCheck} " +
                    $"WHERE CodeCongTac IN ({MyFunction.fcn_Array2listQueryCondition(lsCode)}) " +
                    $"AND VatTu = @VatTu AND LoaiVatTu = @LoaiVatTu " +
                    $"AND MaVatLieu = @MaVatLieu";
                DataProvider.InstanceTHDA.ExecuteNonQuery(dbString, parameter: new object[] {dr.VatTu, dr.LoaiVatTu, dr.MaVatLieu});
            }
            else
            {
                MessageShower.ShowInformation("Tính năng này chỉ áp dụng cho công tác từ Tiến độ kế hoạch");
                return;
            }

            if (_codeCTTK != null)
                TDKHHelper.TinhLaiToanBoKhoiLuongKeHoach(new string[] { _codeCTTK });
            loadData();


        }

        private void fcn_handle_dgv_TDKH_HaoPhi_BoVatTuKhac(object sender, EventArgs e)
        {
            SetOnlyCrHaoPhiState(false, null, true);

        }

        private void fcn_handle_dgv_TDKH_HaoPhi_BoAll(object sender, EventArgs e)
        {
            SetOnlyCrHaoPhiState(false, null, false);
        }

        private void fcn_handle_dgv_TDKH_HaoPhi_BoVL(object sender, EventArgs e)
        {
            SetOnlyCrHaoPhiState(false, "Vật liệu", false);
        }

        private void fcn_handle_dgv_TDKH_HaoPhi_BoNC(object sender, EventArgs e)
        {
            SetOnlyCrHaoPhiState(false, "Nhân công", false);
        }
        private void fcn_handle_dgv_TDKH_HaoPhi_BoMTC(object sender, EventArgs e)
        {
            SetOnlyCrHaoPhiState(false, "Máy thi công", false);
        }

        private void fcn_handle_dgv_TDKH_HaoPhi_LayToanBoVatTuKhac(object sender, EventArgs e)
        {
            SetOnlyCrHaoPhiState(true, null, true);
        }

        private void fcn_handle_dgv_TDKH_HaoPhi_LayAll(object sender, EventArgs e)
        {
            SetOnlyCrHaoPhiState(true, null, false);
        }

        private void fcn_handle_dgv_TDKH_HaoPhi_LayVL(object sender, EventArgs e)
        {
            SetOnlyCrHaoPhiState(true, "Vật liệu", false);
        }

        private void fcn_handle_dgv_TDKH_HaoPhi_LayNC(object sender, EventArgs e)
        {
            SetOnlyCrHaoPhiState(true, "Nhân công", false);
        }
        private void fcn_handle_dgv_TDKH_HaoPhi_LayMTC(object sender, EventArgs e)
        {
            SetOnlyCrHaoPhiState(true, "Máy thi công", false);
        }

        private void fcn_handle_dgv_TDKH_HaoPhi_GetHaoPhiMacDinh(object sender, EventArgs e)
        {
            if (MessageShower.ShowYesNoQuestion("Hao phí hiện tại sẽ được thay thế bằng hao phí mặc định của phần mềm?") != DialogResult.Yes) 
                return;

            MyFunction.fcn_TDKH_ThemDinhMucMacDinhChoCongTac(_type, _codeCongTac, true);

            if (_codeCTTK != null)
                TDKHHelper.TinhLaiToanBoKhoiLuongKeHoach(new string[] { _codeCTTK });

            loadData();
        }

        private void fcn_handle_dgv_TDKH_HaoPhi_ThemVatTu(object sender, EventArgs e)
        {
            Form_ChonCongTacDinhMuc newForm = new Form_ChonCongTacDinhMuc(_colFk, new string[] { _codeCongTac }, _codeCTTK);
            if (newForm.ShowDialog() == DialogResult.OK)
            {
                loadData();
            }
        }
        
        private void fcn_handle_dgv_TDKH_HaoPhi_ThemVatTuCon(object sender, EventArgs e)
        {
            DXEditMenuItem item = sender as DXEditMenuItem;

            if (!int.TryParse(item.EditValue.ToString(), out int SoLan) || SoLan <= 0 || SoLan > 10)
            {
                MessageShower.ShowWarning("Vui lòng nhập số nguyên từ 1 đến 10");
                return;
            }    

            var crRow = tl_haophi.GetFocusedRow() as HaoPhiVatTuExtensionViewModel;
            

            if (crRow is null)
            {
                MessageShower.ShowWarning("Không có hao phí để thêm!");
                return;
            }

            double DinhMucTB = Math.Round(crRow.DinhMucNguoiDung / (double)SoLan, 4);
            if (tl_haophi.FocusedNode.HasChildren)
                DinhMucTB = 0;

            List<Tbl_TDKH_HaoPhiVatTuViewModel> newsHp = new List<Tbl_TDKH_HaoPhiVatTuViewModel>();
            for (int i = 1; i <= SoLan; i++)
            {
                var newDM = crRow.Clone() as HaoPhiVatTuExtensionViewModel;
                newDM.Code = Guid.NewGuid().ToString();
                newDM.DinhMucNguoiDung = DinhMucTB;
                newDM.CodeHaoPhiCha = crRow.Code;

                newsHp.Add(newDM);
            }

            DataProvider.InstanceTHDA.UpdateDataTableFromSqliteSource(newsHp.fcn_ObjToDataTable(), TDKH.Tbl_HaoPhiVatTu);


            //Form_ChonCongTacDinhMuc newForm = new Form_ChonCongTacDinhMuc(_colFk, new string[] { _codeCongTac }, _codeCTTK, crRow);
            //if (newForm.ShowDialog() == DialogResult.OK)
            //{
                loadData();
            //}
        }

        private void fcn_handle_dgv_TDKH_HaoPhi_ChiaDeu(object sender, EventArgs e)
        {
            tl_haophi.CellValueChanged -= gv_HaoPhiDinhMuc_CellValueChanged;

            var crNode = tl_haophi.FocusedNode;
            var crhp = tl_haophi.GetFocusedRow() as HaoPhiVatTuExtensionViewModel;

            var crDMND = crhp.DinhMucNguoiDung;
            var consNode = crNode.Nodes;

                    
            var newDMND = Math.Round(crDMND / (double)consNode.Count(), 4);
            List<string> codesCon = new List<string>();
            foreach (TreeListNode node in consNode)
            {
                node.SetValue("DinhMucNguoiDung", newDMND);
                codesCon.Add((string)node.GetValue("Code"));
            }

            string dbString = $"UPDATE {TDKH.Tbl_HaoPhiVatTu} SET DinhMucNguoiDung = @Value WHERE Code IN ({MyFunction.fcn_Array2listQueryCondition(codesCon)})";
            DataProvider.InstanceTHDA.ExecuteNonQuery(dbString, parameter: new object[] { newDMND });

            tl_haophi.CellValueChanged += gv_HaoPhiDinhMuc_CellValueChanged;

        }

        //private void fcn_handle_dgv_HaoPhi_NhanThongTinVatTu(DataRow[] dataRows)
        //{


        //    loadData();
        //}

        private void gv_HaoPhiDinhMuc_ValidatingEditor(object sender, DevExpress.XtraEditors.Controls.BaseContainerValidateEditorEventArgs e)
        {
            var view = tl_haophi;
            string[] doubColumn =
            {
                "Định mức người dùng",
                "Hệ số người dùng",
            };

            if (doubColumn.Contains(view.FocusedColumn.Caption))
            {
                if (!double.TryParse((string)e.Value, out double db))
                {
                    e.Valid = false;
                    e.ErrorText = "Vui lòng nhập số thực!";
                }
            }

            if (view.FocusedColumn.Caption == "Đơn giá")
            {
                if (!int.TryParse((string)e.Value, out int db))
                {
                    e.Valid = false;
                    e.ErrorText = "Vui lòng nhập số nguyên!";
                }
            }
        }

        private void tl_haophi_GetCustomSummaryValue(object sender, GetCustomSummaryValueEventArgs e)
        {

        }

        private void tl_haophi_NodeCellStyle(object sender, GetCustomNodeCellStyleEventArgs e)
        {
            if (e.Node.Level == 0)
            {
                e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold);

                if (colsWithForeColor.Contains(e.Column.FieldName))
                    e.Appearance.ForeColor = MyConstant.color_Row_HangMuc;
            }
            if (e.Node.Level == 2)
            {
                //e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold);

                //if (!colsWithForeColor.Contains(e.Column.FieldName))
                    e.Appearance.ForeColor = MyConstant.color_Row_NhomDienGiai;
            }

        }

        private void tl_haophi_CustomDrawNodeCell(object sender, CustomDrawNodeCellEventArgs e)
        {
            if (!colsParent.Contains(e.Column.FieldName) && e.Node.Level == 0)
            {
                e.Appearance.FillRectangle(e.Cache, e.Bounds);
                e.Handled = true;
            }
            else if (e.Column.FieldName == "PhanTichKeHoach" && e.Node.Level == 2)
            {
                e.Appearance.FillRectangle(e.Cache, e.Bounds);
                e.Handled = true;
            }
        }

        private void tl_haophi_EditFormShowing(object sender, EditFormShowingEventArgs e)
        {

        }

        private void tl_haophi_ShowingEditor(object sender, CancelEventArgs e)
        {
            if (tl_haophi.FocusedNode.Level == 0)
                e.Cancel = true;

            if (tl_haophi.FocusedNode.Level == 2 && tl_haophi.FocusedColumn.FieldName == nameof(HaoPhiVatTuExtensionViewModel.HeSoNguoiDung))
            {
                MessageShower.ShowWarning("Chỉ được thay đổi hệ số của hao phí chính!");
                e.Cancel = true;
            }
        }

        private void tl_haophi_CellValueChanging(object sender, CellValueChangedEventArgs e)
        {

        }

        private void tl_haophi_DataSourceChanged(object sender, EventArgs e)
        {
            tl_haophi.ExpandAll();
            colDonGiaGoc.Visible = BaseFrom.IsShowDonGiaKeHoach;

            colKhoiLuongGoc.Visible = BaseFrom.IsShowKhoiLuongKeHoach;

            if (!BaseFrom.IsShowKhoiLuongKeHoach || !BaseFrom.IsShowDonGiaKeHoach)
                colDonGiaGocChiTiet.Visible = false;
        }

        private void repos_button_Xoa_Click(object sender, EventArgs e)
        {
            var crNode = tl_haophi.FocusedNode;
            var crhp = tl_haophi.GetFocusedRow() as HaoPhiVatTuExtensionViewModel;
            string mes = $"Bạn có muốn xóa {crhp.VatTu} khỏi công tác này?";

            if (crNode.HasChildren)
            {
                mes += $"\r\nCác hao phí con sẽ bị xóa theo!";
            }    

            var dr = MessageShower.ShowYesNoQuestion(mes);

            if (dr != DialogResult.Yes)
                return;

            string crCodeVT = crhp.Code;
            var crParentNode = tl_haophi.FocusedNode.ParentNode;

            List<string> lsMaVatTu = new List<string>() { crhp.MaVatLieu};
            List<string> lsCodeVT = new List<string>() {crhp.Code};
            tl_haophi.CellValueChanged -= gv_HaoPhiDinhMuc_CellValueChanged;

            if (crNode.HasChildren)
            {
                lsMaVatTu = crNode.Nodes.Select(x => (string)x.GetValue(nameof(HaoPhiVatTuExtensionViewModel.MaVatLieu))).ToList();
                tl_haophi.DeleteNode(tl_haophi.FocusedNode);


            }
            else if (crNode.Level == 2)
            {
                tl_haophi.DeleteNode(tl_haophi.FocusedNode);

                string DMND = nameof(HaoPhiVatTuExtensionViewModel.DinhMucNguoiDung);

                var allSameNode = crParentNode.Nodes;
                var last = allSameNode.LastOrDefault();

                if (last != null)
                {
                    lsCodeVT.Add((string)last.GetValue("Code"));
                    double sum = (double)crParentNode.GetValue(DMND);
                    double newVal = sum - (allSameNode.Where(x => x != last).Sum(x => (double)x.GetValue(DMND)));

                    last.SetValue(DMND, newVal);
                    string codeLast = (string)last.GetValue("Code");

                    string dbString1 = $"UPDATE {TDKH.Tbl_HaoPhiVatTu} SET {DMND} = @Value " +
                                    $"WHERE Code = @Code";

                    DataProvider.InstanceTHDA.ExecuteNonQuery(dbString1, parameter: new object[] { newVal, codeLast });
                }
            }
            string dbString = $"DELETE FROM {TDKH.Tbl_HaoPhiVatTu} WHERE Code = '{crCodeVT}' OR CodeHaoPhiCha = '{crCodeVT}'";
            DataProvider.InstanceTHDA.ExecuteNonQuery(dbString);
            TDKHHelper.TinhLaiToanBoKhoiLuongKeHoach(new string[] { _codeCTTK });
            tl_haophi.CellValueChanged += gv_HaoPhiDinhMuc_CellValueChanged;

        }

        private void tl_haophi_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                TreeList treeList = sender as TreeList;
                TreeListHitInfo info = treeList.CalcHitInfo(e.Location);
                if (info.Node != null)
                {
                    treeList.FocusedNode = info.Node;
                }
            }
        }
    }
}
