using DevExpress.Export;
using DevExpress.Utils;
using DevExpress.Utils.Extensions;
using DevExpress.XtraEditors;
using DevExpress.XtraPrinting;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Columns;
using DevExpress.XtraTreeList.Nodes;
using PhanMemQuanLyThiCong.Common.Constant;
using PhanMemQuanLyThiCong.Common.Enums;
using PhanMemQuanLyThiCong.Common.Helper;
using PhanMemQuanLyThiCong.Common.ViewModel.KLHN;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PhanMemQuanLyThiCong.Controls.KiemSoat
{
    public partial class KiemSoatTienDo : DevExpress.XtraEditors.XtraUserControl
    {
        static bool _CheckCongTac = false;
        static bool _CheckFocus = false;
        Dictionary<string, string> Dic_Col = new Dictionary<string, string>();
        public DateTime _Max { get; set; }
        public DateTime _Min { get; set; }

        #region Custom Properties

        private bool _IsBrief = false;
        [DisplayName("IsBrief")]
        public bool IsBrief
        {
            get
            {
                return _IsBrief;
            }

            set
            {
                _IsBrief = value;

                col_NgayBatDau.Visible = Col_NgayKetThuc.Visible = SumNgay.Visible = ColTongKhoiLuong.Visible = !value;
                if (value)
                {
                    splitContainerControl1.PanelVisibility = SplitPanelVisibility.Panel2;
                    lci_chiTiet.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                    lci_DenNgayHienTai.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                    lci_ChonDonVi.Visibility = lci_btXemChiTiet.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                }
                else
                {
                    splitContainerControl1.PanelVisibility = SplitPanelVisibility.Both;
                    lci_chiTiet.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                    lci_DenNgayHienTai.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                    lci_ChonDonVi.Visibility = lci_btXemChiTiet.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                }

            }
        }
        #endregion

        private void tl_KiemSoat_NodeCellStyle(object sender, DevExpress.XtraTreeList.GetCustomNodeCellStyleEventArgs e)
        {
            _CheckFocus = true;
            if (ce_ChiTietTuyen.Checked)
            {
                if (e.Node.Level < 4)
                {
                    e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold);
                }
                else
                {
                    string Guid = (string)e.Node.GetValue("ID");
                    if (!Guid.Contains("_ChiTiet"))
                        e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold);
                }
                string NoiDung = (string)e.Node.GetValue("TenCongTac");
                string FiledName = e.Column.FieldName;
                bool CheckDate = DateTime.TryParse(FiledName, out DateTime Check);
                double standard = 0.1 / 100;
                if (/*FiledName != "TenCongTac"&& FiledName != "TongKhoiLuong"&& FiledName != "NgayBatDau"&& FiledName != "NgayKetThuc"*/CheckDate)
                {
                    if (DateTime.Parse(FiledName) > DateTime.Now)
                        return;
                }
                if (e.Node.Level >= 4)
                {
                    if (NoiDung == "Còn lại (Kế hoạch-Thi công)" && /*FiledName != "TenCongTac"*/CheckDate)
                    {
                        if (string.IsNullOrEmpty(e.Node.GetValue(FiledName).ToString()))
                            return;
                        TreeListNode Parent = e.Node.ParentNode;
                        TreeListNodes ChildNode = Parent.Nodes;
                        TreeListNode KH = ChildNode.AsEnumerable().Where(x => x.GetValue("TenCongTac").ToString() == "Lũy kế kế hoạch").FirstOrDefault();
                        double KLKH = string.IsNullOrEmpty(KH.GetValue(FiledName).ToString()) ? 0 : (double)KH.GetValue(FiledName);
                        double KL = (double)e.Node.GetValue(FiledName);
                        var tl = KLKH == 0 ? 0 : KL / KLKH;
                        if (tl > standard)
                            e.Appearance.ForeColor = Color.Red;
                        else
                            e.Appearance.ForeColor = Color.Green;
                    }
                    else if (NoiDung == "Thi công chi tiết" && /*FiledName != "TenCongTac" && FiledName != "TongKhoiLuong"*/CheckDate)
                    {
                        TreeListNode Parent = e.Node.ParentNode;
                        TreeListNodes ChildNode = Parent.Nodes;
                        TreeListNode KH = ChildNode.AsEnumerable().Where(x => x.GetValue("TenCongTac").ToString() == "Kế hoạch chi tiết").FirstOrDefault();
                        double KLKH = 0, KLTC = 0;
                        if (KH.GetValue(FiledName).ToString() == "")
                        {
                            if (!string.IsNullOrEmpty(e.Node.GetValue(FiledName).ToString()))
                            {
                                KLTC = (double)e.Node.GetValue(FiledName);
                                if (KLTC > 0)
                                {
                                    e.Appearance.ForeColor = Color.Yellow;
                                    e.Appearance.BackColor = Color.Green;
                                }
                            }
                            return;
                        }
                        else
                        {
                            KLKH = (double)KH.GetValue(FiledName);
                            if (KLKH == 0)
                            {
                                if (!string.IsNullOrEmpty(e.Node.GetValue(FiledName).ToString()))
                                {
                                    KLTC = (double)e.Node.GetValue(FiledName);
                                    if (KLTC > 0)
                                    {
                                        e.Appearance.ForeColor = Color.Yellow;
                                        e.Appearance.BackColor = Color.Green;
                                    }
                                }
                                return;
                            }
                        }
                        //if (KLKH == 0)
                        //{
                        //    if (e.Node.GetValue("NgayKetThuc").ToString() == "")
                        //        return;
                        //    DateTime NKT = DateTime.Parse(e.Node.GetValue("NgayKetThuc").ToString());
                        //    if (DateTime.Parse(FiledName) > NKT)
                        //    {
                        //        if (e.Node.GetValue(FiledName).ToString() == "")
                        //        {
                        //            e.Appearance.BackColor = Color.Red;
                        //            e.Appearance.ForeColor = Color.Yellow;
                        //            return;
                        //        }
                        //        else
                        //        {
                        //            if ((double)e.Node.GetValue(FiledName) > 0)
                        //            {
                        //                e.Appearance.BackColor = Color.DarkRed;
                        //                e.Appearance.ForeColor = Color.Yellow;
                        //                return;
                        //            }
                        //        }
                        //    }
                        //    return;
                        //}
                        if (e.Node.GetValue(FiledName).ToString() == "")
                        {
                            e.Appearance.BackColor = Color.Red;
                            e.Appearance.ForeColor = Color.Yellow;
                            return;
                        }
                        KLTC = (double)e.Node.GetValue(FiledName);
                        var tl = (KLKH - KLTC) / KLKH;
                        if (tl > standard)
                        {
                            e.Appearance.ForeColor = Color.Yellow;
                            e.Appearance.BackColor = Color.Red;
                        }
                        else
                        {
                            e.Appearance.ForeColor = Color.Yellow;
                            e.Appearance.BackColor = Color.Green;
                        }
                        //    e.Appearance.ForeColor = Color.Green;
                        //if (KLTC >= KLKH)
                        //{
                        //    e.Appearance.ForeColor = Color.Yellow;
                        //    e.Appearance.BackColor = Color.Green;
                        //}
                        //else
                        //{
                        //    e.Appearance.ForeColor = Color.Yellow;
                        //    e.Appearance.BackColor = Color.Red;
                        //}
                    }
                    else if (NoiDung == "Lũy kế thi công" && /*FiledName != "TenCongTac" && FiledName != "TongKhoiLuong"*/CheckDate)
                    {
                        TreeListNode Parent = e.Node.ParentNode;
                        TreeListNodes ChildNode = Parent.Nodes;
                        TreeListNode KH = ChildNode.AsEnumerable().Where(x => x.GetValue("TenCongTac").ToString() == "Lũy kế kế hoạch").FirstOrDefault();
                        double KLKH = 0, KLTC = 0;
                        if (KH.GetValue(FiledName).ToString() == "")
                        {
                            if (!string.IsNullOrEmpty(e.Node.GetValue(FiledName).ToString()))
                            {
                                KLTC = (double)e.Node.GetValue(FiledName);
                                if (KLTC > 0)
                                {
                                    e.Appearance.ForeColor = Color.Yellow;
                                    e.Appearance.BackColor = Color.Green;
                                }
                            }
                            return;
                        }
                        else
                        {
                            KLKH = (double)KH.GetValue(FiledName);
                            if (KLKH == 0)
                            {
                                if (!string.IsNullOrEmpty(e.Node.GetValue(FiledName).ToString()))
                                {
                                    KLTC = (double)e.Node.GetValue(FiledName);
                                    if (KLTC > 0)
                                    {
                                        e.Appearance.ForeColor = Color.Yellow;
                                        e.Appearance.BackColor = Color.Green;
                                    }
                                }
                                return;
                            }
                        }
                        KLKH = (double)KH.GetValue(FiledName);
                        if (e.Node.GetValue(FiledName).ToString() == "")
                        {
                            e.Appearance.BackColor = Color.Red;
                            e.Appearance.ForeColor = Color.Yellow;
                            return;
                        }
                        KLTC = (double)e.Node.GetValue(FiledName);
                        var tl = KLKH == 0 ? 0 : (KLKH - KLTC) / KLKH;
                        if (tl > standard)
                        {
                            e.Appearance.ForeColor = Color.Yellow;
                            e.Appearance.BackColor = Color.Red;
                        }
                        else
                        {
                            e.Appearance.ForeColor = Color.Yellow;
                            e.Appearance.BackColor = Color.Green;
                        }
                    }
                    else if (NoiDung == "" && FiledName == "TongKhoiLuong")
                    {
                        e.Appearance.BackColor = Color.White;
                    }
                }
                else if (e.Node.Level == 3)
                {
                    string ID = e.Node.GetValue("ID").ToString();
                    if (/*FiledName == "TenCongTac" || FiledName == "TongKhoiLuong"|| FiledName == "NgayKetThuc"|| FiledName == "NgayBatDau"|| FiledName == "SumNgay"*/!CheckDate)
                    {
                        if (ID.Contains("_Nhom"))
                            e.Appearance.ForeColor = MyConstant.color_Row_NhomCongTac;
                        else if (ID.Contains("_CodeTuyen"))
                            e.Appearance.ForeColor = MyConstant.color_Row_PhanTuyen;
                        return;
                    }
                    if (ID.Contains("_CodeTuyen"))
                    {
                        bool m_Checkred = false, /*m_CheckTrongWhile = false,*/m_CheckGreen = false;
                        TreeListNodes ChildNode = e.Node.Nodes;
                        if (ChildNode.Count() == 0)
                            return;
                        foreach (TreeListNode item in ChildNode)
                        {
                            TreeListNodes ChildChiTiet = item.Nodes;
                            TreeListNode ConLai = ChildChiTiet.AsEnumerable().Where(x => x.GetValue("TenCongTac").ToString() == "Còn lại (Kế hoạch-Thi công)").FirstOrDefault();
                            if (ConLai is null)
                            {
                                continue;
                            }
                            if (ConLai.GetValue(FiledName).ToString() == "")
                            {
                                continue;
                            }
                            double KLKH = (double)ConLai.GetValue(FiledName);
                            TreeListNode LKKH = ChildChiTiet.AsEnumerable().Where(x => x.GetValue("TenCongTac").ToString() == "Lũy kế kế hoạch").FirstOrDefault();
                            double LKKL = (double)LKKH.GetValue(FiledName);
                            var tl = LKKL == 0 ? 0 : KLKH / LKKL;
                            if (tl > standard)
                            {
                                m_Checkred = true;
                                e.Appearance.BackColor = Color.Red;
                                return;
                            }
                            else
                                m_CheckGreen = true;
                        }
                        if (m_CheckGreen)
                            e.Appearance.BackColor = Color.Green;
                    }
                    else
                    {
                        TreeListNodes ChildNode = e.Node.Nodes;
                        TreeListNode ConLai = ChildNode.AsEnumerable().Where(x => x.GetValue("TenCongTac").ToString() == "Còn lại (Kế hoạch-Thi công)").FirstOrDefault();
                        if (ConLai is null)
                            return;
                        //double.TryParse(ConLai.GetValue(FiledName).ToString(), out double KLCL);
                        //if (KLCL == 0)
                        //    return;
                        if (ConLai.GetValue(FiledName).ToString() == "")
                            return;
                        double KLKH = (double)ConLai.GetValue(FiledName);
                        TreeListNode LKKH = ChildNode.AsEnumerable().Where(x => x.GetValue("TenCongTac").ToString() == "Lũy kế kế hoạch").FirstOrDefault();
                        double LKKL = (double)LKKH.GetValue(FiledName);
                        var tl = LKKL == 0 ? 0 : KLKH / LKKL;
                        if (tl > standard)
                            e.Appearance.ForeColor = Color.Red;
                        else
                            e.Appearance.ForeColor = Color.Green;
                    }
                }
                else if (e.Node.Level == 2)
                {
                    bool m_Checkred = false, /*m_CheckTrongWhile = false,*/m_CheckGreen = false;
                    if (/*FiledName == "TenCongTac" || FiledName == "TongKhoiLuong"*/!CheckDate)
                        return;
                    TreeListNodes ChildNodeCongTac = e.Node.Nodes;
                    if (ChildNodeCongTac.Count() == 0)
                        return;
                    string ID =string.Empty;
                    foreach (TreeListNode item in ChildNodeCongTac)
                    {
                        ID = item.GetValue("ID").ToString();
                        TreeListNodes ChildChiTiet = item.Nodes;
                        if (ID.Contains("_CodeTuyen"))
                        {
                            foreach (TreeListNode itemct in ChildChiTiet)
                            {
                                TreeListNodes ChildChiTietNew = itemct.Nodes;
                                TreeListNode ConLai = ChildChiTietNew.AsEnumerable().Where(x => x.GetValue("TenCongTac").ToString() == "Còn lại (Kế hoạch-Thi công)").FirstOrDefault();
                                if (ConLai is null)
                                {
                                    continue;
                                }
                                if (ConLai.GetValue(FiledName).ToString() == "")
                                {
                                    continue;
                                }
                                double KLKH = (double)ConLai.GetValue(FiledName);
                                TreeListNode LKKH = ChildChiTietNew.AsEnumerable().Where(x => x.GetValue("TenCongTac").ToString() == "Lũy kế kế hoạch").FirstOrDefault();
                                double LKKL = (double)LKKH.GetValue(FiledName);
                                var tl = LKKL == 0 ? 0 : KLKH / LKKL;
                                if (tl > standard)
                                {
                                    m_Checkred = true;
                                    e.Appearance.BackColor = Color.Red;
                                    return;
                                }
                                else
                                    m_CheckGreen = true;
                            }
                        }
                        else
                        {
                            TreeListNode ConLai = ChildChiTiet.AsEnumerable().Where(x => x.GetValue("TenCongTac").ToString() == "Còn lại (Kế hoạch-Thi công)").FirstOrDefault();
                            if (ConLai is null)
                            {
                                continue;
                            }
                            if (ConLai.GetValue(FiledName).ToString() == "")
                            {
                                continue;
                            }
                            double KLKH = (double)ConLai.GetValue(FiledName);
                            TreeListNode LKKH = ChildChiTiet.AsEnumerable().Where(x => x.GetValue("TenCongTac").ToString() == "Lũy kế kế hoạch").FirstOrDefault();
                            double LKKL = (double)LKKH.GetValue(FiledName);
                            var tl = LKKL == 0 ? 0 : KLKH / LKKL;
                            if (tl > standard)
                            {
                                m_Checkred = true;
                                e.Appearance.BackColor = Color.Red;
                                return;
                            }
                            else
                                m_CheckGreen = true;
                        }
                    }
                    if (m_CheckGreen)
                        e.Appearance.BackColor = Color.Green;
                }
                else if (e.Node.Level == 1)
                {
                    bool m_Checkred = false, /*m_CheckTrongWhile = false,*/m_CheckGreen = false;
                    if (/*FiledName == "TenCongTac" || FiledName == "TongKhoiLuong"*/!CheckDate)
                        return;
                    TreeListNodes ChildNodeHM = e.Node.Nodes;
                    string ID = string.Empty;
                    foreach (TreeListNode item in ChildNodeHM)
                    {
                        TreeListNodes ChildCongTac = item.Nodes;
                        foreach (TreeListNode rowCT in ChildCongTac)
                        {
                            ID = rowCT.GetValue("ID").ToString();
                            TreeListNodes ChildChiTiet = rowCT.Nodes;
                            if (ID.Contains("_CodeTuyen"))
                            {
                                foreach (TreeListNode itemct in ChildChiTiet)
                                {
                                    TreeListNodes ChildChiTietNew = itemct.Nodes;
                                    TreeListNode ConLai = ChildChiTietNew.AsEnumerable().Where(x => x.GetValue("TenCongTac").ToString() == "Còn lại (Kế hoạch-Thi công)").FirstOrDefault();
                                    if (ConLai is null)
                                    {
                                        continue;
                                    }
                                    if (ConLai.GetValue(FiledName).ToString() == "")
                                    {
                                        continue;
                                    }
                                    double KLKH = (double)ConLai.GetValue(FiledName);
                                    TreeListNode LKKH = ChildChiTietNew.AsEnumerable().Where(x => x.GetValue("TenCongTac").ToString() == "Lũy kế kế hoạch").FirstOrDefault();
                                    double LKKL = (double)LKKH.GetValue(FiledName);
                                    var tl = LKKL == 0 ? 0 : KLKH / LKKL;
                                    if (tl > standard)
                                    {
                                        m_Checkred = true;
                                        e.Appearance.BackColor = Color.Red;
                                        return;
                                    }
                                    else
                                        m_CheckGreen = true;
                                }

                            }
                            else
                            {
                                TreeListNode ConLai = ChildChiTiet.AsEnumerable().Where(x => x.GetValue("TenCongTac").ToString() == "Còn lại (Kế hoạch-Thi công)").FirstOrDefault();
                                if (ConLai == null)
                                {
                                    continue;
                                }
                                if (ConLai.GetValue(FiledName).ToString() == "")
                                {
                                    continue;
                                }
                                double KLKH = (double)ConLai.GetValue(FiledName);
                                TreeListNode LKKH = ChildChiTiet.AsEnumerable().Where(x => x.GetValue("TenCongTac").ToString() == "Lũy kế kế hoạch").FirstOrDefault();
                                double LKKL = (double)LKKH.GetValue(FiledName);
                                var tl = LKKL == 0 ? 0 : KLKH / LKKL;
                                if (tl > standard)
                                {
                                    m_Checkred = true;
                                    e.Appearance.BackColor = Color.Red;
                                    return;
                                }
                                else
                                    m_CheckGreen = true;
                            }
                       
                        }
                    }
                    if (m_CheckGreen)
                        e.Appearance.BackColor = Color.Green;

                }
                else if (e.Node.Level == 0)
                {
                    bool m_Checkred = false, /*m_CheckTrongWhile = false,*/m_CheckGreen = false;
                    if (/*FiledName == "TenCongTac" || FiledName == "TongKhoiLuong"*/!CheckDate)
                        return;
                    string ID = string.Empty;
                    TreeListNodes ChildNodeCT = e.Node.Nodes;
                    foreach (TreeListNode rowCTR in ChildNodeCT)
                    {
                        TreeListNodes ChildNodeHM = rowCTR.Nodes;
                        foreach (TreeListNode item in ChildNodeHM)
                        {
                            TreeListNodes ChildCongTac = item.Nodes;
                            foreach (TreeListNode rowCT in ChildCongTac)
                            {
                                ID = rowCT.GetValue("ID").ToString();
                                TreeListNodes ChildChiTiet = rowCT.Nodes;
                                if (ID.Contains("_CodeTuyen"))
                                {
                                    foreach (TreeListNode itemct in ChildChiTiet)
                                    {
                                        TreeListNodes ChildChiTietNew = itemct.Nodes;
                                        TreeListNode ConLai = ChildChiTietNew.AsEnumerable().Where(x => x.GetValue("TenCongTac").ToString() == "Còn lại (Kế hoạch-Thi công)").FirstOrDefault();
                                        if (ConLai is null)
                                        {
                                            continue;
                                        }
                                        if (ConLai.GetValue(FiledName).ToString() == "")
                                        {
                                            continue;
                                        }
                                        double KLKH = (double)ConLai.GetValue(FiledName);
                                        TreeListNode LKKH = ChildChiTietNew.AsEnumerable().Where(x => x.GetValue("TenCongTac").ToString() == "Lũy kế kế hoạch").FirstOrDefault();
                                        double LKKL = (double)LKKH.GetValue(FiledName);
                                        var tl = LKKL == 0 ? 0 : KLKH / LKKL;
                                        if (tl > standard)
                                        {
                                            m_Checkred = true;
                                            e.Appearance.BackColor = Color.Red;
                                            return;
                                        }
                                        else
                                            m_CheckGreen = true;
                                    }
                                }
                                else
                                {
                                    TreeListNode ConLai = ChildChiTiet.AsEnumerable().Where(x => x.GetValue("TenCongTac").ToString() == "Còn lại (Kế hoạch-Thi công)").FirstOrDefault();
                                    if (ConLai == null)
                                    {
                                        continue;
                                    }
                                    if (ConLai.GetValue(FiledName).ToString() == "")
                                    {
                                        continue;
                                    }
                                    double KLKH = (double)ConLai.GetValue(FiledName);
                                    TreeListNode LKKH = ChildChiTiet.AsEnumerable().Where(x => x.GetValue("TenCongTac").ToString() == "Lũy kế kế hoạch").FirstOrDefault();
                                    double LKKL = (double)LKKH.GetValue(FiledName);
                                    var tl = LKKL == 0 ? 0 : KLKH / LKKL;
                                    if (tl > standard)
                                    {
                                        m_Checkred = true;
                                        e.Appearance.BackColor = Color.Red;
                                        return;
                                    }
                                    else
                                        m_CheckGreen = true;
                                }                                    
                            }
                        }
                    }
                    if (m_CheckGreen)
                        e.Appearance.BackColor = Color.Green;

                }
            }
            else
            {
                if (e.Node.Level < 4)
                {
                    e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold);
                }
                string NoiDung = (string)e.Node.GetValue("TenCongTac");
                string FiledName = e.Column.FieldName;
                bool CheckDate = DateTime.TryParse(FiledName, out DateTime Check);
                double standard = 0.1 / 100;
                if (/*FiledName != "TenCongTac"&& FiledName != "TongKhoiLuong"&& FiledName != "NgayBatDau"&& FiledName != "NgayKetThuc"*/CheckDate)
                {
                    if (DateTime.Parse(FiledName) > DateTime.Now)
                        return;
                }
                if (e.Node.Level == 4)
                {
                    if (NoiDung == "Còn lại (Kế hoạch-Thi công)" && /*FiledName != "TenCongTac"*/CheckDate)
                    {
                        if (string.IsNullOrEmpty(e.Node.GetValue(FiledName).ToString()))
                            return;
                        TreeListNode Parent = e.Node.ParentNode;
                        TreeListNodes ChildNode = Parent.Nodes;
                        TreeListNode KH = ChildNode.AsEnumerable().Where(x => x.GetValue("TenCongTac").ToString() == "Lũy kế kế hoạch").FirstOrDefault();
                        double KLKH = string.IsNullOrEmpty(KH.GetValue(FiledName).ToString()) ? 0 : (double)KH.GetValue(FiledName);
                        double KL = (double)e.Node.GetValue(FiledName);
                        var tl = KLKH == 0 ? 0 : KL / KLKH;
                        if (tl > standard)
                            e.Appearance.ForeColor = Color.Red;
                        else
                            e.Appearance.ForeColor = Color.Green;
                    }
                    else if (NoiDung == "Thi công chi tiết" && /*FiledName != "TenCongTac" && FiledName != "TongKhoiLuong"*/CheckDate)
                    {
                        TreeListNode Parent = e.Node.ParentNode;
                        TreeListNodes ChildNode = Parent.Nodes;
                        TreeListNode KH = ChildNode.AsEnumerable().Where(x => x.GetValue("TenCongTac").ToString() == "Kế hoạch chi tiết").FirstOrDefault();
                        double KLKH = 0, KLTC = 0;
                        if (KH.GetValue(FiledName).ToString() == "")
                        {
                            if (!string.IsNullOrEmpty(e.Node.GetValue(FiledName).ToString()))
                            {
                                KLTC = (double)e.Node.GetValue(FiledName);
                                if (KLTC > 0)
                                {
                                    e.Appearance.ForeColor = Color.Yellow;
                                    e.Appearance.BackColor = Color.Green;
                                }
                            }
                            return;
                        }
                        else
                        {
                            KLKH = (double)KH.GetValue(FiledName);
                            if (KLKH == 0)
                            {
                                if (!string.IsNullOrEmpty(e.Node.GetValue(FiledName).ToString()))
                                {
                                    KLTC = (double)e.Node.GetValue(FiledName);
                                    if (KLTC > 0)
                                    {
                                        e.Appearance.ForeColor = Color.Yellow;
                                        e.Appearance.BackColor = Color.Green;
                                    }
                                }
                                return;
                            }
                        }
                        //if (KLKH == 0)
                        //{
                        //    if (e.Node.GetValue("NgayKetThuc").ToString() == "")
                        //        return;
                        //    DateTime NKT = DateTime.Parse(e.Node.GetValue("NgayKetThuc").ToString());
                        //    if (DateTime.Parse(FiledName) > NKT)
                        //    {
                        //        if (e.Node.GetValue(FiledName).ToString() == "")
                        //        {
                        //            e.Appearance.BackColor = Color.Red;
                        //            e.Appearance.ForeColor = Color.Yellow;
                        //            return;
                        //        }
                        //        else
                        //        {
                        //            if ((double)e.Node.GetValue(FiledName) > 0)
                        //            {
                        //                e.Appearance.BackColor = Color.DarkRed;
                        //                e.Appearance.ForeColor = Color.Yellow;
                        //                return;
                        //            }
                        //        }
                        //    }
                        //    return;
                        //}
                        if (e.Node.GetValue(FiledName).ToString() == "")
                        {
                            e.Appearance.BackColor = Color.Red;
                            e.Appearance.ForeColor = Color.Yellow;
                            return;
                        }
                        KLTC = (double)e.Node.GetValue(FiledName);
                        var tl = (KLKH - KLTC) / KLKH;
                        if (tl > standard)
                        {
                            e.Appearance.ForeColor = Color.Yellow;
                            e.Appearance.BackColor = Color.Red;
                        }
                        else
                        {
                            e.Appearance.ForeColor = Color.Yellow;
                            e.Appearance.BackColor = Color.Green;
                        }
                        //    e.Appearance.ForeColor = Color.Green;
                        //if (KLTC >= KLKH)
                        //{
                        //    e.Appearance.ForeColor = Color.Yellow;
                        //    e.Appearance.BackColor = Color.Green;
                        //}
                        //else
                        //{
                        //    e.Appearance.ForeColor = Color.Yellow;
                        //    e.Appearance.BackColor = Color.Red;
                        //}
                    }
                    else if (NoiDung == "Lũy kế thi công" && /*FiledName != "TenCongTac" && FiledName != "TongKhoiLuong"*/CheckDate)
                    {
                        TreeListNode Parent = e.Node.ParentNode;
                        TreeListNodes ChildNode = Parent.Nodes;
                        TreeListNode KH = ChildNode.AsEnumerable().Where(x => x.GetValue("TenCongTac").ToString() == "Lũy kế kế hoạch").FirstOrDefault();
                        double KLKH = 0, KLTC = 0;
                        if (KH.GetValue(FiledName).ToString() == "")
                        {
                            if (!string.IsNullOrEmpty(e.Node.GetValue(FiledName).ToString()))
                            {
                                KLTC = (double)e.Node.GetValue(FiledName);
                                if (KLTC > 0)
                                {
                                    e.Appearance.ForeColor = Color.Yellow;
                                    e.Appearance.BackColor = Color.Green;
                                }
                            }
                            return;
                        }
                        else
                        {
                            KLKH = (double)KH.GetValue(FiledName);
                            if (KLKH == 0)
                            {
                                if (!string.IsNullOrEmpty(e.Node.GetValue(FiledName).ToString()))
                                {
                                    KLTC = (double)e.Node.GetValue(FiledName);
                                    if (KLTC > 0)
                                    {
                                        e.Appearance.ForeColor = Color.Yellow;
                                        e.Appearance.BackColor = Color.Green;
                                    }
                                }
                                return;
                            }
                        }
                        KLKH = (double)KH.GetValue(FiledName);
                        if (e.Node.GetValue(FiledName).ToString() == "")
                        {
                            e.Appearance.BackColor = Color.Red;
                            e.Appearance.ForeColor = Color.Yellow;
                            return;
                        }
                        KLTC = (double)e.Node.GetValue(FiledName);
                        var tl = KLKH == 0 ? 0 : (KLKH - KLTC) / KLKH;
                        if (tl > standard)
                        {
                            e.Appearance.ForeColor = Color.Yellow;
                            e.Appearance.BackColor = Color.Red;
                        }
                        else
                        {
                            e.Appearance.ForeColor = Color.Yellow;
                            e.Appearance.BackColor = Color.Green;
                        }
                    }
                    else if (NoiDung == "" && FiledName == "TongKhoiLuong")
                    {
                        e.Appearance.BackColor = Color.White;
                    }
                }
                else if (e.Node.Level == 3)
                {

                    if (/*FiledName == "TenCongTac" || FiledName == "TongKhoiLuong"|| FiledName == "NgayKetThuc"|| FiledName == "NgayBatDau"|| FiledName == "SumNgay"*/!CheckDate)
                    {
                        string ID = e.Node.GetValue("ID").ToString();
                        if (ID.Contains("_Nhom"))
                            e.Appearance.ForeColor = MyConstant.color_Row_NhomCongTac;
                        else if (ID.Contains("_CodeTuyen"))
                            e.Appearance.ForeColor = MyConstant.color_Row_PhanTuyen;
                        return;
                    }
                    TreeListNodes ChildNode = e.Node.Nodes;
                    TreeListNode ConLai = ChildNode.AsEnumerable().Where(x => x.GetValue("TenCongTac").ToString() == "Còn lại (Kế hoạch-Thi công)").FirstOrDefault();
                    if (ConLai == null)
                        return;
                    //double.TryParse(ConLai.GetValue(FiledName).ToString(), out double KLCL);
                    //if (KLCL == 0)
                    //    return;
                    if (ConLai.GetValue(FiledName).ToString() == "")
                        return;
                    double KLKH = (double)ConLai.GetValue(FiledName);
                    TreeListNode LKKH = ChildNode.AsEnumerable().Where(x => x.GetValue("TenCongTac").ToString() == "Lũy kế kế hoạch").FirstOrDefault();
                    double LKKL = (double)LKKH.GetValue(FiledName);
                    var tl = LKKL == 0 ? 0 : KLKH / LKKL;
                    if (tl > standard)
                        e.Appearance.ForeColor = Color.Red;
                    else
                        e.Appearance.ForeColor = Color.Green;
                    //if (KLKH > 0)
                    //    e.Appearance.BackColor = Color.Red;
                    //else
                    //    e.Appearance.BackColor = Color.Green;
                }
                else if (e.Node.Level == 2)
                {
                    bool m_Checkred = false, /*m_CheckTrongWhile = false,*/m_CheckGreen = false;
                    if (/*FiledName == "TenCongTac" || FiledName == "TongKhoiLuong"*/!CheckDate)
                        return;
                    TreeListNodes ChildNodeCongTac = e.Node.Nodes;
                    if (ChildNodeCongTac.Count() == 0)
                        return;
                    foreach (TreeListNode item in ChildNodeCongTac)
                    {
                        TreeListNodes ChildChiTiet = item.Nodes;
                        TreeListNode ConLai = ChildChiTiet.AsEnumerable().Where(x => x.GetValue("TenCongTac").ToString() == "Còn lại (Kế hoạch-Thi công)").FirstOrDefault();
                        if (ConLai == null)
                        {
                            //e.Appearance.BackColor = Color.White;
                            //m_CheckTrongWhile = true;
                            continue;
                        }
                        if (ConLai.GetValue(FiledName).ToString() == "")
                        {
                            //e.Appearance.BackColor = Color.White;
                            //m_CheckTrongWhile = true;
                            continue;
                        }
                        double KLKH = (double)ConLai.GetValue(FiledName);
                        TreeListNode LKKH = ChildChiTiet.AsEnumerable().Where(x => x.GetValue("TenCongTac").ToString() == "Lũy kế kế hoạch").FirstOrDefault();
                        double LKKL = (double)LKKH.GetValue(FiledName);
                        var tl = LKKL == 0 ? 0 : KLKH / LKKL;
                        //if (tl > standard)
                        //    e.Appearance.ForeColor = Color.Red;
                        //else
                        //    e.Appearance.ForeColor = Color.Green;
                        if (tl > standard)
                        {
                            m_Checkred = true;
                            e.Appearance.BackColor = Color.Red;
                            return;
                        }
                        else
                            m_CheckGreen = true;
                    }
                    if (m_CheckGreen)
                        e.Appearance.BackColor = Color.Green;
                    //if (m_Check)
                    //    e.Appearance.BackColor = Color.Red;
                    //else if (!m_Check && !m_CheckTrong)
                    //    e.Appearance.BackColor = Color.Green;
                }
                else if (e.Node.Level == 1)
                {
                    bool isCongTac = false;
                    bool m_Checkred = false, /*m_CheckTrongWhile = false,*/m_CheckGreen = false;
                    if (/*FiledName == "TenCongTac" || FiledName == "TongKhoiLuong"*/!CheckDate)
                        return;
                    TreeListNodes ChildNodeHM = e.Node.Nodes;
                    foreach (TreeListNode item in ChildNodeHM)
                    {
                        TreeListNodes ChildCongTac = item.Nodes;
                        foreach (TreeListNode rowCT in ChildCongTac)
                        {
                            isCongTac = true;
                            TreeListNodes ChildChiTiet = rowCT.Nodes;
                            TreeListNode ConLai = ChildChiTiet.AsEnumerable().Where(x => x.GetValue("TenCongTac").ToString() == "Còn lại (Kế hoạch-Thi công)").FirstOrDefault();
                            if (ConLai == null)
                            {
                                //e.Appearance.BackColor = Color.White;
                                //m_CheckTrong = true;
                                continue;
                            }
                            //double.TryParse(ConLai.GetValue(FiledName).ToString(), out double KLCL);
                            //if (KLCL <= 0)
                            //{
                            //    e.Appearance.BackColor = Color.White;
                            //    m_CheckTrong = true;
                            //    continue;
                            //}
                            if (ConLai.GetValue(FiledName).ToString() == "")
                            {
                                //e.Appearance.BackColor = Color.White;
                                //m_CheckTrong = true;
                                continue;
                            }
                            double KLKH = (double)ConLai.GetValue(FiledName);
                            TreeListNode LKKH = ChildChiTiet.AsEnumerable().Where(x => x.GetValue("TenCongTac").ToString() == "Lũy kế kế hoạch").FirstOrDefault();
                            double LKKL = (double)LKKH.GetValue(FiledName);
                            var tl = LKKL == 0 ? 0 : KLKH / LKKL;
                            //if (tl > standard)
                            //    e.Appearance.ForeColor = Color.Red;
                            //else
                            //    e.Appearance.ForeColor = Color.Green;
                            if (tl > standard)
                            {
                                m_Checkred = true;
                                e.Appearance.BackColor = Color.Red;
                                return;
                            }
                            else
                                m_CheckGreen = true;
                        }
                    }
                    if (m_CheckGreen)
                        e.Appearance.BackColor = Color.Green;

                }
                else if (e.Node.Level == 0)
                {
                    bool isCongTac = false;
                    bool m_Checkred = false, /*m_CheckTrongWhile = false,*/m_CheckGreen = false;
                    if (/*FiledName == "TenCongTac" || FiledName == "TongKhoiLuong"*/!CheckDate)
                        return;
                    TreeListNodes ChildNodeCT = e.Node.Nodes;
                    foreach (TreeListNode rowCTR in ChildNodeCT)
                    {
                        TreeListNodes ChildNodeHM = rowCTR.Nodes;
                        foreach (TreeListNode item in ChildNodeHM)
                        {
                            TreeListNodes ChildCongTac = item.Nodes;
                            foreach (TreeListNode rowCT in ChildCongTac)
                            {
                                isCongTac = true;
                                TreeListNodes ChildChiTiet = rowCT.Nodes;
                                TreeListNode ConLai = ChildChiTiet.AsEnumerable().Where(x => x.GetValue("TenCongTac").ToString() == "Còn lại (Kế hoạch-Thi công)").FirstOrDefault();
                                if (ConLai == null)
                                {
                                    //e.Appearance.BackColor = Color.White;
                                    //m_checkTrong = true;
                                    continue;
                                }
                                //double.TryParse(ConLai.GetValue(FiledName).ToString(), out double KLCL);
                                //if (KLCL <= 0)
                                //{
                                //    e.Appearance.BackColor = Color.White;
                                //    m_checkTrong = true;
                                //    continue;
                                //}
                                if (ConLai.GetValue(FiledName).ToString() == "")
                                {
                                    //e.Appearance.BackColor = Color.White;
                                    //m_checkTrong = true;
                                    continue;
                                }
                                double KLKH = (double)ConLai.GetValue(FiledName);
                                TreeListNode LKKH = ChildChiTiet.AsEnumerable().Where(x => x.GetValue("TenCongTac").ToString() == "Lũy kế kế hoạch").FirstOrDefault();
                                double LKKL = (double)LKKH.GetValue(FiledName);
                                var tl = LKKL == 0 ? 0 : KLKH / LKKL;
                                //if (tl > standard)
                                //    e.Appearance.ForeColor = Color.Red;
                                //else
                                //    e.Appearance.ForeColor = Color.Green;
                                if (tl > standard)
                                {
                                    m_Checkred = true;
                                    e.Appearance.BackColor = Color.Red;
                                    return;
                                }
                                else
                                    m_CheckGreen = true;
                            }
                        }
                    }
                    if (m_CheckGreen)
                        e.Appearance.BackColor = Color.Green;

                }
            }
        }

        public KiemSoatTienDo()
        {
            InitializeComponent();
        }

        public void Fcn_UpdateKiemSoat(bool AllDuAn = false, string Condition = null, string Condition1 = null)
        {
            WaitFormHelper.ShowWaitForm("Đang phân tích dữ liệu");
            //DataTable dtCT, dtHM;
            //DuAnHelper.fcn_GetDtCongTrinhHangMuc(out dtCT, out dtHM, MyConstant.TBL_THONGTINCONGTRINH, MyConstant.TBL_THONGTINHANGMUC, true);
            //dbString = $"SELECT COALESCE(cttk.DonVi, dmct.DonVi) AS DonVi,COALESCE(cttk.TenCongTac, dmct.TenCongTac) AS TenCongTac," +
            //    $"COALESCE(dmct.CodeHangMuc, hmct.Code) AS CodeHangMuc," +
            //    $"COALESCE(cttk.TenCongTac, dmct.MaHieuCongTac) AS MaHieuCongTac," +
            //    $"cttk.* " +
            //      $"FROM {TDKH.TBL_ChiTietCongTacTheoKy} cttk " +
            //      $"LEFT JOIN {TDKH.TBL_DanhMucCongTac} dmct " +
            //      $"ON cttk.CodeCongTac = dmct.Code " +
            //      $"LEFT JOIN Tbl_ThongTinHangMuc hmct ON cttk.CodeHangMuc = hmct.Code "+
            //$"WHERE cttk.CodeGiaiDoan = '{SharedControls.cbb_DBKH_ChonDot.SelectedValue}' ";
            string dbString = !AllDuAn ? $"SELECT COALESCE(hm.Code, hmct.Code) AS CodeHangMuc,COALESCE(hm.Ten, hmct.Ten) AS TenHangMuc," +
                    $"COALESCE(cttk.CodeNhom,cttkCha.CodeNhom) AS CodeNhom,nct.Ten as TenNhom,nct.KhoiLuongKeHoach as KLKHNhom,nct.DonVi as DonViNhom," +
                    $"nct.NgayBatDau as NgayBatDauNhom,nct.NgayKetThuc as NgayKetThucNhom," +
                    $"COALESCE(ctrinh.Code, ctrinhct.Code) AS CodeCongTrinh,COALESCE(ctrinh.Ten, ctrinhct.Ten) AS TenCongTrinh," +
                    $"COALESCE(cttk.DonVi, dmct.DonVi) AS DonVi,COALESCE(cttk.TenCongTac, dmct.TenCongTac) AS TenCongTac," +
                    $"COALESCE(cttk.MaHieuCongTac, dmct.MaHieuCongTac) AS MaHieuCongTac, \r\n" +
                    $"hm.CodeCongTrinh, \r\n" +
                    $"COALESCE(ctrinh.CodeDuAn, ctrinhct.CodeDuAn) AS CodeDuAn, \r\n" +
                    $"COALESCE(da.TenDuAn, dact.TenDuAn) AS TenDuAn, \r\n" +
                    $"COALESCE(nt.Ten, ntp.Ten,tdtc.Ten) AS TenNhaThau, \r\n" +
                    $"COALESCE(nt.Code, ntp.Code,tdtc.Code) AS CodeNhaThauChinh, \r\n" +
                    $"COALESCE(ctrinh.SortId, ctrinhct.SortId) AS SortIdCtrinh,COALESCE(hm.SortId, hmct.SortId) AS SortIdHM,cttk.* \r\n" +
                    $"FROM {TDKH.TBL_ChiTietCongTacTheoKy} cttk\r\n" +
                    $"LEFT JOIN {TDKH.TBL_ChiTietCongTacTheoKy} cttkCha\r\n" +
                    $"ON cttk.CodeCha = cttkCha.Code AND cttkCha.TrangThai IN ({MyFunction.fcn_Array2listQueryCondition(MyConstant.TrangThai)}) \r\n" +
                    $"LEFT JOIN {TDKH.TBL_NhomCongTac} nct\r\n" +
                    $"ON nct.Code = cttk.CodeNhom \r\n" +
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
                    $"LEFT JOIN {MyConstant.TBL_THONGTINNHATHAUPHU} ntp \r\n" +
                    $"ON cttk.CodeNhaThauPhu = ntp.Code \r\n" +
                    $"LEFT JOIN Tbl_ThongTinHangMuc hmct  ON cttk.CodeHangMuc = hmct.Code " +
                    $"LEFT JOIN Tbl_ThongTinCongTrinh ctrinhct  ON hmct.CodeCongTrinh = ctrinhct.Code " +
                    $"LEFT JOIN Tbl_ThongTinDuAn dact  ON ctrinhct.CodeDuAn = dact.Code" +
                    $" AND (dact.Code = '{SharedControls.slke_ThongTinDuAn.EditValue}' " +
                    $"AND (dact.IsShareToOtherKey='1' OR dact.CreatedBySerialno='{BaseFrom.BanQuyenKeyInfo.SerialNo}')) " +
                    $"WHERE  da.Code = '{SharedControls.slke_ThongTinDuAn.EditValue}' " +
                    $"AND (da.IsShareToOtherKey='1' OR da.CreatedBySerialno='{BaseFrom.BanQuyenKeyInfo.SerialNo}') AND " +
                    $" (cttk.TrangThai IN ({MyFunction.fcn_Array2listQueryCondition(MyConstant.TrangThai)}) OR nct.TrangThai IN ({MyFunction.fcn_Array2listQueryCondition(MyConstant.TrangThai)}))  " 
           /*         $"ORDER BY SortIdCtrinh ASC,SortIdHM ASC, cttk.SortId ASC\r\n" */:
                    (Condition is null ?
                    $"SELECT COALESCE(hm.Code, hmct.Code) AS CodeHangMuc,COALESCE(hm.Ten, hmct.Ten) AS TenHangMuc," +
                    $"COALESCE(cttk.CodeNhom,cttkCha.CodeNhom) AS CodeNhom,nct.Ten as TenNhom,nct.KhoiLuongKeHoach as KLKHNhom,nct.DonVi as DonViNhom," +
                   $"nct.NgayBatDau as NgayBatDauNhom,nct.NgayKetThuc as NgayKetThucNhom," +
                    $"COALESCE(ctrinh.Code, ctrinhct.Code) AS CodeCongTrinh,COALESCE(ctrinh.Ten, ctrinhct.Ten) AS TenCongTrinh," +
                    $"COALESCE(cttk.DonVi, dmct.DonVi) AS DonVi,COALESCE(cttk.TenCongTac, dmct.TenCongTac) AS TenCongTac," +
                    $"COALESCE(cttk.MaHieuCongTac, dmct.MaHieuCongTac) AS MaHieuCongTac, \r\n" +
                    $"hm.CodeCongTrinh, \r\n" +
                    $"COALESCE(ctrinh.CodeDuAn, ctrinhct.CodeDuAn) AS CodeDuAn, \r\n" +
                    $"COALESCE(da.TenDuAn, dact.TenDuAn) AS TenDuAn, \r\n" +
                    $"COALESCE(nt.Ten, ntp.Ten,tdtc.Ten) AS TenNhaThau, \r\n" +
                    $"COALESCE(nt.Code, ntp.Code,tdtc.Code) AS CodeNhaThauChinh, \r\n" +
                    $"COALESCE(ctrinh.SortId, ctrinhct.SortId) AS SortIdCtrinh,COALESCE(hm.SortId, hmct.SortId) AS SortIdHM,cttk.* \r\n" +
                    $"FROM {TDKH.TBL_ChiTietCongTacTheoKy} cttk\r\n" +
                    $"LEFT JOIN {TDKH.TBL_ChiTietCongTacTheoKy} cttkCha\r\n" +
                    $"ON cttk.CodeCha = cttkCha.Code AND cttkCha.TrangThai IN ({MyFunction.fcn_Array2listQueryCondition(MyConstant.TrangThai)}) \r\n" +
                    $"LEFT JOIN {TDKH.TBL_NhomCongTac} nct\r\n" +
                    $"ON nct.Code = cttk.CodeNhom \r\n" +
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
                    $"LEFT JOIN {MyConstant.TBL_THONGTINNHATHAUPHU} ntp \r\n" +
                    $"ON cttk.CodeNhaThauPhu = ntp.Code \r\n" +
                    $"LEFT JOIN Tbl_ThongTinHangMuc hmct  ON cttk.CodeHangMuc = hmct.Code " +
                    $"LEFT JOIN Tbl_ThongTinCongTrinh ctrinhct  ON hmct.CodeCongTrinh = ctrinhct.Code " +
                    $"LEFT JOIN Tbl_ThongTinDuAn dact  ON ctrinhct.CodeDuAn = dact.Code " +
                    $"AND (dact.IsShareToOtherKey='1' OR dact.CreatedBySerialno='{BaseFrom.BanQuyenKeyInfo.SerialNo}') " +
                    $"WHERE ( cttk.TrangThai IN ({MyFunction.fcn_Array2listQueryCondition(MyConstant.TrangThai)}) OR nct.TrangThai IN ({MyFunction.fcn_Array2listQueryCondition(MyConstant.TrangThai)}))" +
                    $" AND " +
                    $" (da.IsShareToOtherKey='1' OR da.CreatedBySerialno='{BaseFrom.BanQuyenKeyInfo.SerialNo}') \r\n"
                  /*  $"ORDER BY SortIdCtrinh ASC,SortIdHM ASC, cttk.SortId ASC\r\n" */:

                    $"SELECT COALESCE(hm.Code, hmct.Code) AS CodeHangMuc,COALESCE(hm.Ten, hmct.Ten) AS TenHangMuc," +
                    $"COALESCE(cttk.CodeNhom,cttkCha.CodeNhom) AS CodeNhom,nct.Ten as TenNhom,nct.KhoiLuongKeHoach as KLKHNhom,nct.DonVi as DonViNhom," +
                    $"nct.NgayBatDau as NgayBatDauNhom,nct.NgayKetThuc as NgayKetThucNhom," +
                    $"COALESCE(ctrinh.Code, ctrinhct.Code) AS CodeCongTrinh,COALESCE(ctrinh.Ten, ctrinhct.Ten) AS TenCongTrinh," +
                    $"COALESCE(cttk.DonVi, dmct.DonVi) AS DonVi,COALESCE(cttk.TenCongTac, dmct.TenCongTac) AS TenCongTac," +
                    $"COALESCE(cttk.MaHieuCongTac, dmct.MaHieuCongTac) AS MaHieuCongTac, \r\n" +
                    $"hm.CodeCongTrinh, \r\n" +
                    $"COALESCE(ctrinh.CodeDuAn, ctrinhct.CodeDuAn) AS CodeDuAn, \r\n" +
                    $"COALESCE(da.TenDuAn, dact.TenDuAn) AS TenDuAn, \r\n" +
                    $"COALESCE(nt.Ten, ntp.Ten,tdtc.Ten) AS TenNhaThau, \r\n" +
                    $"COALESCE(nt.Code, ntp.Code,tdtc.Code) AS CodeNhaThauChinh, \r\n" +
                    $"COALESCE(ctrinh.SortId, ctrinhct.SortId) AS SortIdCtrinh,COALESCE(hm.SortId, hmct.SortId) AS SortIdHM,cttk.* \r\n" +
                    $"FROM {TDKH.TBL_ChiTietCongTacTheoKy} cttk\r\n" +
                    $"LEFT JOIN {TDKH.TBL_ChiTietCongTacTheoKy} cttkCha \r\n" +
                    $"ON cttk.CodeCha = cttkCha.Code AND cttkCha.TrangThai IN ({MyFunction.fcn_Array2listQueryCondition(MyConstant.TrangThai)})\r\n" +
                    $"LEFT JOIN {TDKH.TBL_NhomCongTac} nct\r\n" +
                    $"ON nct.Code = cttk.CodeNhom \r\n" +
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
                    $"LEFT JOIN {MyConstant.TBL_THONGTINNHATHAUPHU} ntp \r\n" +
                    $"ON cttk.CodeNhaThauPhu = ntp.Code \r\n" +
                    $"LEFT JOIN Tbl_ThongTinHangMuc hmct  ON cttk.CodeHangMuc = hmct.Code " +
                    $"LEFT JOIN Tbl_ThongTinCongTrinh ctrinhct  ON hmct.CodeCongTrinh = ctrinhct.Code " +
                    $"LEFT JOIN Tbl_ThongTinDuAn dact  ON ctrinhct.CodeDuAn = dact.Code {Condition1} " +
                    $"AND (dact.IsShareToOtherKey='1' OR dact.CreatedBySerialno='{BaseFrom.BanQuyenKeyInfo.SerialNo}') " +
                    $"WHERE {Condition}" +
                    $" (da.IsShareToOtherKey='1' OR da.CreatedBySerialno='{BaseFrom.BanQuyenKeyInfo.SerialNo}') AND" +
                    $" (cttk.TrangThai IN ({MyFunction.fcn_Array2listQueryCondition(MyConstant.TrangThai)}) OR nct.TrangThai IN ({MyFunction.fcn_Array2listQueryCondition(MyConstant.TrangThai)}))  ")
                 /*   $"ORDER BY SortIdCtrinh ASC,SortIdHM ASC, cttk.SortId ASC\r\n")*/;

            //if (Ce_Full.Checked)
            //{
            //    dbString += $"cttk.NgayBatDau>={de_NBD.DateTime.Date.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE)} AND " +
            //        $"cttk<={de_NKT.DateTime.Date.ToString(MyConstant.CONST_DATE_FORMAT_SQLITE)} ";
            //}
            dbString += $"ORDER BY SortIdCtrinh ASC,SortIdHM ASC, cttk.SortId ASC\r\n";

            DataTable dtCongTacTheoKy = DataProvider.InstanceTHDA.ExecuteQuery(dbString);
            Fcn_UpdateThongTin(dtCongTacTheoKy);
            WaitFormHelper.CloseWaitForm();
            if (dtCongTacTheoKy.Rows.Count == 0)
            {
                MessageShower.ShowError("Dự án chưa có công tác ở trạng thái ĐANG THỰC HIỆN, Vui lòng kiểm tra lại!!!!!!!");
            }
            if (!_CheckFocus)
            {
                if (tl_ThongTin.FocusedNode is null)
                    return;
                string CodeDuAn = tl_ThongTin.FocusedNode.GetValue("ID").ToString();
                Fcn_UpdateData(CodeDuAn, false);
            }
        }

        private void tl_KiemSoat_CustomDrawNodeCell(object sender, DevExpress.XtraTreeList.CustomDrawNodeCellEventArgs e)
        {
            DevExpress.XtraTreeList.TreeList treeList = sender as DevExpress.XtraTreeList.TreeList;
            if (treeList.FocusedColumn == e.Column && treeList.FocusedNode == e.Node)
                return;
            if (object.Equals(e.CellValue, (double)0))
            {
                e.Appearance.FillRectangle(e.Cache, e.Bounds);
                e.Handled = true;
            }
        }

        private void tl_ThongTin_BeforeExpand(object sender, DevExpress.XtraTreeList.BeforeExpandEventArgs e)
        {
            if (e.Node.Level > 1)
                e.CanExpand = false;
        }

        private void tl_ThongTin_NodeCellStyle(object sender, DevExpress.XtraTreeList.GetCustomNodeCellStyleEventArgs e)
        {
            if (e.Node.Level <= 1)
            {
                e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold);
            }
            string NoiDung = (string)e.Node.GetValue("TenCongTac");
            string FiledName = e.Column.FieldName;
            double standard = 0.1 / 100;
            if (e.Node.Level == 2)
            {
                bool/* m_Check = false,*/ isCongTac = false /*m_checkTrong = false*/;
                bool m_Checkred = false, /*m_CheckTrongWhile = false,*/m_CheckGreen = false;
                if (FiledName == "TenCongTac" || FiledName == "STT" || FiledName == "TenColum")
                    return;
                string TenCongTac = e.Node.GetValue("TenCongTac").ToString();
                if (TenCongTac == "Lũy kế thi công")
                {
                    if (DateTime.Parse(FiledName) > DateTime.Now)
                        return;
                    TreeListNodes ChildNodeCT = e.Node.Nodes;
                    foreach (TreeListNode rowCTR in ChildNodeCT)
                    {
                        TreeListNodes ChildNodeHM = rowCTR.Nodes;
                        foreach (TreeListNode item in ChildNodeHM)
                        {
                            TreeListNodes ChildCongTac = item.Nodes;
                            foreach (TreeListNode rowCT in ChildCongTac)
                            {
                                isCongTac = true;
                                TreeListNodes ChildChiTiet = rowCT.Nodes;
                                TreeListNode ConLai = ChildChiTiet.AsEnumerable().Where(x => x.GetValue("TenCongTac").ToString() == "Còn lại (Kế hoạch-Thi công)").FirstOrDefault();
                                if (ConLai.GetValue(FiledName).ToString() == "")
                                {
                                    //e.Appearance.BackColor = Color.White;
                                    //m_checkTrong = true;
                                    continue;
                                }

                                double KLKH = (double)ConLai.GetValue(FiledName);
                                TreeListNode LKKH = ChildChiTiet.AsEnumerable().Where(x => x.GetValue("TenCongTac").ToString() == "Lũy kế kế hoạch").FirstOrDefault();
                                double LKKL = (double)LKKH.GetValue(FiledName);
                                var tl = LKKL == 0 ? 0 : KLKH / LKKL;
                                if (tl > standard)
                                {
                                    //m_Check = true;
                                    e.Appearance.BackColor = Color.Red;
                                    return;
                                }
                                else
                                    m_CheckGreen = true;
                            }
                        }
                    }
                    if (m_CheckGreen)
                        e.Appearance.BackColor = Color.Green;

                }
                else if (TenCongTac == "Lũy kế kế hoạch")
                    e.Appearance.BackColor = Color.Green;
            }
            else if (e.Node.Level == 0)
            {
                e.Appearance.ForeColor = Color.Red;
                if (FiledName == "TenCongTac" || FiledName == "STT" || FiledName == "TenColum")
                    return;
                e.Appearance.BackColor = Color.Green;
                //TreeListNodes ChildNodeNhaThau = e.Node.Nodes;
                //foreach (TreeListNode rowNhaThau in ChildNodeNhaThau)
                //{
                //    TreeListNodes ChildNodeKHTC = rowNhaThau.Nodes;
                //    foreach (TreeListNode rowKHTC in ChildNodeKHTC)
                //    {
                //        string TenCongTac = rowKHTC.GetValue("TenCongTac").ToString();
                //        if (TenCongTac == "Lũy kế thi công")
                //        {
                //            TreeListNodes ChildNodeCT = rowKHTC.Nodes;
                //            foreach (TreeListNode rowCTR in ChildNodeCT)
                //            {
                //                TreeListNodes ChildNodeHM = rowCTR.Nodes;
                //                foreach (TreeListNode item in ChildNodeHM)
                //                {
                //                    TreeListNodes ChildCongTac = item.Nodes;
                //                    foreach (TreeListNode rowCT in ChildCongTac)
                //                    {
                //                        isCongTac = true;
                //                        TreeListNodes ChildChiTiet = rowCT.Nodes;
                //                        TreeListNode ConLai = ChildChiTiet.AsEnumerable().Where(x => x.GetValue("TenCongTac").ToString() == "Còn lại (Kế hoạch-Thi công)").FirstOrDefault();
                //                        if (ConLai.GetValue(FiledName).ToString() == "")
                //                            continue;
                //                        double KLKH = (double)ConLai.GetValue(FiledName);
                //                        if (KLKH > 0)
                //                        {
                //                            m_Check = true;
                //                            e.Appearance.BackColor = Color.Red;
                //                            return;
                //                        }
                //                    }
                //                }
                //            }
                //        }
                //    }
                //}
                //if (!m_Check && isCongTac)
                //    e.Appearance.BackColor = Color.Green;
            }
        }

        private void tl_ThongTin_BeforeCollapse(object sender, DevExpress.XtraTreeList.BeforeCollapseEventArgs e)
        {
            if (e.Node.Level == 0)
                e.CanCollapse = false;
        }

        private void tl_ThongTin_FocusedNodeChanged(object sender, DevExpress.XtraTreeList.FocusedNodeChangedEventArgs e)
        {
            if (e.Node.Level > 1)
                return;
            if (e.Node.Level == 0)
            {
                string CodeDuAn = e.Node.GetValue("ID").ToString();
                Fcn_UpdateData(CodeDuAn, false);
            }
            else
            {
                TreeListNode Parent = e.Node.ParentNode;
                string CodeDuAn = Parent.GetValue("ID").ToString();
                string colum = e.Node.GetValue("TenColum").ToString();
                string CodeNhaThau = e.Node.GetValue("ID").ToString();
                Fcn_UpdateData(CodeDuAn, true, colum, CodeNhaThau);
            }
        }

        private void ce_ChiTiet_CheckedChanged(object sender, EventArgs e)
        {
            if (ce_ChiTiet.Checked)
                splitContainerControl1.PanelVisibility = SplitPanelVisibility.Both;
            else
                splitContainerControl1.PanelVisibility = SplitPanelVisibility.Panel1;
        }

        private void tl_ThongTin_CustomDrawRow(object sender, DevExpress.XtraTreeList.CustomDrawRowEventArgs e)
        {
            if (e.RowInfo.Level > 1)
                return;
            e.DefaultDraw();
            if (e.RowInfo.IsFocused)
                e.Cache.DrawRectangle(Pens.Red, e.Bounds);

        }

        private void ce_CongTacError_CheckedChanged(object sender, EventArgs e)
        {
            //TreeListNode Focus = tl_ThongTin.FocusedNode;
            //if (ce_CongTacError.Checked)
            //{
            //    _CheckCongTac = true;
            //    goto Label;
            //}
            //else
            //{
            //    _CheckCongTac = false;
            //    goto Label;
            //}
            //Label:
            ////TreeListNode Focus = tl_ThongTin.FocusedNode;
            //if (Focus.Level == 0)
            //{
            //    string CodeDuAn = Focus.GetValue("ID").ToString();
            //    Fcn_UpdateData(CodeDuAn, false);
            //}
            //else
            //{
            //    TreeListNode Parent = Focus.ParentNode;
            //    string CodeDuAn = Parent.GetValue("ID").ToString();
            //    string colum = Focus.GetValue("TenColum").ToString();
            //    string CodeNhaThau = Focus.GetValue("ID").ToString();
            //    string dbString = $"SELECT * FROM {Dic_Col[colum]} WHERE \"Code\"='{CodeNhaThau}'";
            //    DataTable dtThauChinh = DataProvider.InstanceTHDA.ExecuteQuery(dbString);
            //    Fcn_UpdateData(CodeDuAn, true, dtThauChinh, colum);
            //}
        }

        private void tl_KiemSoat_CustomRowFilter(object sender, DevExpress.XtraTreeList.CustomRowFilterEventArgs e)
        {
            if (!_CheckCongTac)
                return;
            TreeList treeList = sender as TreeList;
            if (e.Node.Level == 4)
            {
                bool CheckError = false;
                if (e.Node.GetValue("TenCongTac").ToString() == "Còn lại (Kế hoạch-Thi công)")
                {
                    for (DateTime i = _Min; i <= _Max; i = i.AddDays(1))
                    {
                        string colum = i.ToString(MyConstant.CONST_DATE_FORMAT_SPSHEET);
                        if (tl_KiemSoat.Columns.ColumnByFieldName(colum) == null || e.Node.GetValue(colum).ToString() == "")
                            continue;
                        double KLCL = (double)e.Node.GetValue(colum);
                        if (KLCL >= 0)
                            CheckError = true;
                    }
                }
                if (!CheckError)
                {
                    TreeListNode Parent = e.Node.ParentNode;
                    Parent.Visible = false;
                    TreeListNodes Child = Parent.Nodes;
                    //foreach (TreeListNode item in Child)
                    //{
                    //    item.Visible = false;
                    //}

                }
            }
        }

        private void Fcn_AddChiTiet(DataRow row, string NoiDung, string CodeCongTac)
        {
            row["ID"] = $"{Guid.NewGuid()}_ChiTiet";
            row["ParentID"] = CodeCongTac;
            row["TenCongTac"] = NoiDung;
        }

        private void ce_HienNgayHienTai_CheckedChanged(object sender, EventArgs e)
        {
            tl_ThongTin.BeginUpdate();
            if (ce_HienNgayHienTai.Checked)
            {
                foreach (TreeListColumn item in tl_ThongTin.Columns.OrderBy(x => x.Caption))
                {
                    string name = item.Caption;
                    if (name == "Tên đơn vị" || name == "STT" || name == "Tên Colum")
                        continue;
                    if (DateTime.Parse(name).Date != DateTime.Now.Date)
                        item.Visible = false;
                    else
                        item.Visible = true;
                }
            }
            else
            {
                if (tl_ThongTin.Columns.ColumnByFieldName(DateTime.Now.Date.ToString(MyConstant.CONST_DATE_FORMAT_SPSHEET)) != null)
                    tl_ThongTin.Columns[DateTime.Now.Date.ToString(MyConstant.CONST_DATE_FORMAT_SPSHEET)].Visible = false;
                int count = 3;
                foreach (TreeListColumn item in tl_ThongTin.Columns.Where(x => x.Caption != "Tên đơn vị" && x.Caption != "STT" && x.Caption != "Tên Colum").OrderBy(x => DateTime.Parse(x.Caption)))
                {
                    string name = item.Caption;
                    item.Visible = true;
                    item.VisibleIndex = count++;
                }
            }
            tl_ThongTin.EndUpdate();
        }

        private void slke_ChonDonVi_EditValueChanged(object sender, EventArgs e)
        {
            var dr = slke_ChonDonVi.GetSelectedDataRow() as DataRowView;
            if (dr is null)
                return;


            var newNode = tl_ThongTin.GetNodeList().Where(x => x.GetValue("ID").ToString() == dr.Row["ID"].ToString()).Single();

            tl_ThongTin.FocusedNode = newNode;

        }

        private void searchLookUpEdit1View_DataSourceChanged(object sender, EventArgs e)
        {
            searchLookUpEdit1View.ExpandAllGroups();
        }

        private void bt_ChiTiet_Click(object sender, EventArgs e)
        {
            SharedControls.xtraTab_KiemSoat.SelectedTabPage = SharedControls.xtraTab_KiemSoatTienDo;
        }

        private void KiemSoatTienDo_Load(object sender, EventArgs e)
        {
            de_NKT.DateTime = DateTime.Now;
            de_NBD.DateTime = DateTime.Now.AddDays(-1);
            SharedControls.Error = (int)sp_SaiSo.Value;
        }
        private void sb_XuatFile_Click(object sender, EventArgs e)
        {
            XtraFolderBrowserDialog Xtra = new XtraFolderBrowserDialog();
            string PathSave = "";
            if (Xtra.ShowDialog() == DialogResult.OK)
            {
                PathSave = Xtra.SelectedPath;
            }
            else
                return;
            WaitFormHelper.ShowWaitForm("Đang xuất dữ liệu?Vui lòng chờ !!!!");
            XlsxExportOptionsEx advOptions = new XlsxExportOptionsEx();
            advOptions.SheetName = "Tiến độ hằng ngày";
            advOptions.ExportType = DevExpress.Export.ExportType.WYSIWYG;
            //advOptions.AllowFixedColumns= DevExpress.Utils.DefaultBoolean.True; 
            //advOptions.AllowSortingAndFiltering= DevExpress.Utils.DefaultBoolean.False;
            //advOptions.AllowConditionalFormatting = DevExpress.Utils.DefaultBoolean.True;
            string time = DateTime.Now.ToString("dd-MM-yyyy_hh-mm-ss");
            tl_KiemSoat.ExportToXlsx(Path.Combine(PathSave, $"Tiến độ hằng ngày_{SharedControls.slke_ThongTinDuAn.Text}_{time}.xlsx"), advOptions);
            WaitFormHelper.CloseWaitForm();
            DialogResult dialogResult = XtraMessageBox.Show($"Xuất file [Tiến độ hằng ngày_{SharedControls.slke_ThongTinDuAn.Text}_{time}.xlsx] thành công. Bạn có muốn mở file không???", "QUẢN LÝ THI CÔNG - THÔNG BÁO", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialogResult == DialogResult.Yes)
            {
                Process.Start(Path.Combine(PathSave, $"Tiến độ hằng ngày_{SharedControls.slke_ThongTinDuAn.Text}_{time}.xlsx"));
            }
        }

        private void Ce_Full_CheckedChanged(object sender, EventArgs e)
        {
            Fcn_UpdateKiemSoat();
            if (!_CheckFocus)
            {
                if (tl_ThongTin.FocusedNode is null)
                    return;
                string CodeDuAn = tl_ThongTin.FocusedNode.GetValue("ID").ToString();
                Fcn_UpdateData(CodeDuAn, false);
            }

        }
        private void Args_Showing_TenKho(object sender, XtraMessageShowingArgs e)
        {
            e.Form.Appearance.FontStyleDelta = FontStyle.Bold;
            e.Form.Appearance.FontSizeDelta = 2;
            foreach (var control in e.Form.Controls)
            {
                SimpleButton button = control as SimpleButton;
                if (button != null)
                {
                    button.ImageOptions.SvgImageSize = new Size(16, 16);
                    // button.Height = 25;
                    switch (button.DialogResult.ToString())
                    {
                        case ("OK"):
                            //button.ImageOptions.SvgImage = svgImageCollection1[5];
                            button.Text = "Cập nhập ẩn hiện";
                            button.Width = 200;
                            button.Height = 50;
                            button.Click += (ss, ee) =>
                            {
                                Fcn_AnHienTienDo();
                            };
                            break;
                        case ("No"):
                            //button.ImageOptions.SvgImage = svgImageCollection1[0];
                            button.Text = "Cập nhập tiến độ";
                            button.Width = 200;
                            button.Height = 50;
                            button.Click += (ss, ee) =>
                            {
                                Ce_Full.Checked = false;
                                Fcn_UpdateKiemSoat();
                                if (!_CheckFocus)
                                {
                                    if (tl_ThongTin.FocusedNode is null)
                                        return;
                                    string CodeDuAn = tl_ThongTin.FocusedNode.GetValue("ID").ToString();
                                    Fcn_UpdateData(CodeDuAn, false);
                                }
                            };
                            break;
                        default:
                            //button.ImageOptions.SvgImage = svgImageCollection1[3];
                            button.Text = "Thoát";
                            button.Width = 200;
                            button.Height = 50;
                            button.Click += (ss, ee) => { e.Form.Close(); };
                            break;
                    }
                }
            }
        }
        private void sb_Update_Click(object sender, EventArgs e)
        {
            XtraMessageBoxArgs args = new XtraMessageBoxArgs();
            args.Caption = "Lựa chọn cập nhập";
            args.Buttons = new DialogResult[] { DialogResult.OK, DialogResult.No, DialogResult.Cancel };
            args.Showing += Args_Showing_TenKho;
            DevExpress.XtraEditors.XtraMessageBox.Show(args);
        }

        private void Fcn_UpdateDataChiTietTuyen(string CodeDuAn, bool ChiTiet, string ColColum = null, string CodeNhaThau = null)
        {
            WaitFormHelper.ShowWaitForm("Đang phân tích dữ liệu");
            string dbString = ChiTiet ? $"SELECT COALESCE(cttk.CodePhanTuyen, hm.CodePhanTuyen) AS CodePhanTuyen," +
                $"COALESCE(TuyenCT.Ten, hm.TenPhanTuyen) AS TenTuyen," +
                $" COALESCE(hm.Code, hmct.Code) AS CodeHangMuc,COALESCE(hm.Ten, hmct.Ten) AS TenHangMuc," +
         $"COALESCE(ctrinh.Code, ctrinhct.Code) AS CodeCongTrinh,COALESCE(ctrinh.Ten, ctrinhct.Ten) AS TenCongTrinh," +
         $"COALESCE(cttk.CodeNhom,cttkCha.CodeNhom) AS CodeNhom,nct.Ten as TenNhom,nct.KhoiLuongKeHoach as KLKHNhom,nct.DonVi as DonViNhom," +
         $"nct.NgayBatDau as NgayBatDauNhom,nct.NgayKetThuc as NgayKetThucNhom," +
         $"COALESCE(cttk.DonVi, dmct.DonVi) AS DonVi,COALESCE(cttk.TenCongTac, dmct.TenCongTac) AS TenCongTac," +
         $"COALESCE(cttk.MaHieuCongTac, dmct.MaHieuCongTac) AS MaHieuCongTac, \r\n" +
         $"hm.CodeCongTrinh, \r\n" +
         $"COALESCE(ctrinh.CodeDuAn, ctrinhct.CodeDuAn) AS CodeDuAn, \r\n" +
         $"COALESCE(da.TenDuAn, dact.TenDuAn) AS TenDuAn, \r\n" +
         $"COALESCE(nt.Ten, ntp.Ten,tdtc.Ten) AS TenNhaThau, \r\n" +
         $"COALESCE(nt.Code, ntp.Code,tdtc.Code) AS CodeNhaThauChinh, \r\n" +
         $"COALESCE(ctrinh.SortId, ctrinhct.SortId) AS SortIdCtrinh,COALESCE(hm.SortId, hmct.SortId) AS SortIdHM,cttk.* \r\n" +
         $"FROM {TDKH.TBL_ChiTietCongTacTheoKy} cttk\r\n" +
         $"LEFT JOIN {TDKH.TBL_ChiTietCongTacTheoKy} cttkCha\r\n" +
         $"ON cttk.CodeCha = cttkCha.Code \r\n" +
         $"LEFT JOIN {TDKH.TBL_NhomCongTac} nct\r\n" +
         $"ON nct.Code = cttk.CodeNhom \r\n" +
         $"INNER JOIN {TDKH.TBL_DanhMucCongTac} dmct\r\n" +
         $"ON cttk.CodeCongTac = dmct.Code \r\n" +
                           $" JOIN {MyConstant.view_HangMucWithPhanTuyen} hm\r\n" +
                             $"ON (hm.Code = dmct.CodeHangMuc AND ((dmct.CodePhanTuyen IS NOT NULL AND hm.CodePhanTuyen = dmct.CodePhanTuyen) " +
                             $"OR (dmct.CodePhanTuyen IS NULL AND hm.CodePhanTuyen IS NULL)))\r\n" +
                              $" LEFT JOIN {MyConstant.view_HangMucWithPhanTuyen} TuyenCT  " +
                              $"ON (hm.Code = cttk.CodeHangMuc AND ((cttk.CodePhanTuyen IS NOT NULL AND TuyenCT.CodePhanTuyen = cttk.CodePhanTuyen) " +
                             $"OR (cttk.CodePhanTuyen IS NULL AND TuyenCT.CodePhanTuyen IS NULL)))\r\n" +
         $"INNER JOIN {MyConstant.TBL_THONGTINCONGTRINH} ctrinh\r\n" +
         $"ON hm.CodeCongTrinh = ctrinh.Code \r\n" +
         $"INNER JOIN {MyConstant.TBL_THONGTINDUAN} da\r\n" +
         $"ON ctrinh.CodeDuAn = da.Code \r\n" +
         $"LEFT JOIN {MyConstant.TBL_THONGTINNHATHAU} nt\r\n" +
         $"ON cttk.CodeNhaThau = nt.Code \r\n" +
         $"LEFT JOIN {MyConstant.TBL_THONGTINTODOITHICONG} tdtc\r\n" +
         $"ON cttk.CodeToDoi = tdtc.Code \r\n" +
         $"LEFT JOIN {MyConstant.TBL_THONGTINNHATHAUPHU} ntp \r\n" +
         $"ON cttk.CodeNhaThauPhu = ntp.Code \r\n" +
         $"LEFT JOIN Tbl_ThongTinHangMuc hmct  ON cttk.CodeHangMuc = hmct.Code " +
         $"LEFT JOIN Tbl_ThongTinCongTrinh ctrinhct  ON hmct.CodeCongTrinh = ctrinhct.Code " +
         $"LEFT JOIN Tbl_ThongTinDuAn dact  ON ctrinhct.CodeDuAn = dact.Code " +
         $"WHERE  cttk.{ColColum}='{CodeNhaThau}' AND" +
         $" (cttk.TrangThai IN ({MyFunction.fcn_Array2listQueryCondition(MyConstant.TrangThai)}) OR nct.TrangThai IN ({MyFunction.fcn_Array2listQueryCondition(MyConstant.TrangThai)}))  " +
         $"ORDER BY SortIdCtrinh ASC,SortIdHM ASC, cttk.SortId ASC\r\n" :

        $"SELECT COALESCE(cttk.CodePhanTuyen, hm.CodePhanTuyen) AS CodePhanTuyen," +
                $"COALESCE(TuyenCT.Ten, hm.TenPhanTuyen) AS TenTuyen," +
                $" COALESCE(hm.Code, hmct.Code) AS CodeHangMuc,COALESCE(hm.Ten, hmct.Ten) AS TenHangMuc," +
         $"COALESCE(cttk.CodeNhom,cttkCha.CodeNhom) AS CodeNhom,nct.Ten as TenNhom,nct.KhoiLuongKeHoach as KLKHNhom,nct.DonVi as DonViNhom," +
         $"nct.NgayBatDau as NgayBatDauNhom,nct.NgayKetThuc as NgayKetThucNhom," +
         $"COALESCE(ctrinh.Code, ctrinhct.Code) AS CodeCongTrinh,COALESCE(ctrinh.Ten, ctrinhct.Ten) AS TenCongTrinh," +
         $"COALESCE(cttk.DonVi, dmct.DonVi) AS DonVi,COALESCE(cttk.TenCongTac, dmct.TenCongTac) AS TenCongTac," +
         $"COALESCE(cttk.MaHieuCongTac, dmct.MaHieuCongTac) AS MaHieuCongTac, \r\n" +
         $"hm.CodeCongTrinh, \r\n" +
         $"COALESCE(ctrinh.CodeDuAn, ctrinhct.CodeDuAn) AS CodeDuAn, \r\n" +
         $"COALESCE(da.TenDuAn, dact.TenDuAn) AS TenDuAn, \r\n" +
         $"COALESCE(nt.Ten, ntp.Ten,tdtc.Ten) AS TenNhaThau, \r\n" +
         $"COALESCE(nt.Code, ntp.Code,tdtc.Code) AS CodeNhaThauChinh, \r\n" +
         $"COALESCE(ctrinh.SortId, ctrinhct.SortId) AS SortIdCtrinh,COALESCE(hm.SortId, hmct.SortId) AS SortIdHM,cttk.* \r\n" +
         $"FROM {TDKH.TBL_ChiTietCongTacTheoKy} cttk\r\n" +
         $"LEFT JOIN {TDKH.TBL_ChiTietCongTacTheoKy} cttkCha\r\n" +
         $"ON cttk.CodeCha = cttkCha.Code \r\n" +
         $"LEFT JOIN {TDKH.TBL_NhomCongTac} nct\r\n" +
         $"ON nct.Code = cttk.CodeNhom \r\n" +
         $"INNER JOIN {TDKH.TBL_DanhMucCongTac} dmct\r\n" +
         $"ON cttk.CodeCongTac = dmct.Code \r\n" +
                              $" JOIN {MyConstant.view_HangMucWithPhanTuyen} hm\r\n" +
                             $"ON (hm.Code = dmct.CodeHangMuc AND ((dmct.CodePhanTuyen IS NOT NULL AND hm.CodePhanTuyen = dmct.CodePhanTuyen) " +
                             $"OR (dmct.CodePhanTuyen IS NULL AND hm.CodePhanTuyen IS NULL)))\r\n" +
                              $" LEFT JOIN {MyConstant.view_HangMucWithPhanTuyen} TuyenCT  " +
                              $"ON (hm.Code = cttk.CodeHangMuc AND ((cttk.CodePhanTuyen IS NOT NULL AND TuyenCT.CodePhanTuyen = cttk.CodePhanTuyen) " +
                             $"OR (cttk.CodePhanTuyen IS NULL AND TuyenCT.CodePhanTuyen IS NULL)))\r\n" +
         $"INNER JOIN {MyConstant.TBL_THONGTINCONGTRINH} ctrinh\r\n" +
         $"ON hm.CodeCongTrinh = ctrinh.Code \r\n" +
         $"INNER JOIN {MyConstant.TBL_THONGTINDUAN} da\r\n" +
         $"ON ctrinh.CodeDuAn = da.Code \r\n" +
         $"LEFT JOIN {MyConstant.TBL_THONGTINNHATHAU} nt\r\n" +
         $"ON cttk.CodeNhaThau = nt.Code \r\n" +
         $"LEFT JOIN {MyConstant.TBL_THONGTINTODOITHICONG} tdtc\r\n" +
         $"ON cttk.CodeToDoi = tdtc.Code \r\n" +
         $"LEFT JOIN {MyConstant.TBL_THONGTINNHATHAUPHU} ntp \r\n" +
         $"ON cttk.CodeNhaThauPhu = ntp.Code \r\n" +
         $"LEFT JOIN Tbl_ThongTinHangMuc hmct  ON cttk.CodeHangMuc = hmct.Code " +
         $"LEFT JOIN Tbl_ThongTinCongTrinh ctrinhct  ON hmct.CodeCongTrinh = ctrinhct.Code " +
         $"LEFT JOIN Tbl_ThongTinDuAn dact  ON ctrinhct.CodeDuAn = dact.Code AND dact.Code = '{CodeDuAn}' " +
         $"WHERE  da.Code = '{CodeDuAn}' AND " +
         $" (cttk.TrangThai IN ({MyFunction.fcn_Array2listQueryCondition(MyConstant.TrangThai)}) OR nct.TrangThai IN ({MyFunction.fcn_Array2listQueryCondition(MyConstant.TrangThai)}))  " +
         $"ORDER BY SortIdCtrinh ASC,SortIdHM ASC, cttk.SortId ASC\r\n";

            DataTable dtcongtactheochuky = DataProvider.InstanceTHDA.ExecuteQuery(dbString);
            dtcongtactheochuky.AddIndPhanTuyenNhom();
            var codeNhoms = dtcongtactheochuky.AsEnumerable().Where(x => x["CodeNhom"] != DBNull.Value)
       .Select(x => x["CodeNhom"].ToString()).Distinct().ToList();
            string[] lstCode = dtcongtactheochuky.AsEnumerable().Select(x => x["CodeCha"].ToString()).ToArray();
            string[] lstCodeCT = dtcongtactheochuky.AsEnumerable().Where(x => !lstCode.Contains(x["Code"].ToString())).Select(x => x["Code"].ToString()).ToArray();
            DateTime Min_KH = dtcongtactheochuky.AsEnumerable().Where(x => x["NgayBatDau"] != DBNull.Value).Min(x => DateTime.Parse(x["NgayBatDau"].ToString()));
            DateTime Max_KH = dtcongtactheochuky.AsEnumerable().Where(x => x["NgayKetThuc"] != DBNull.Value).Max(x => DateTime.Parse(x["NgayKetThuc"].ToString()));
            DateTime Min_KHNhom = dtcongtactheochuky.AsEnumerable().Where(x => x["NgayBatDauNhom"] != DBNull.Value).Any() ?
                 dtcongtactheochuky.AsEnumerable().Where(x => x["NgayBatDauNhom"] != DBNull.Value).Min(x => DateTime.Parse(x["NgayBatDauNhom"].ToString())) : Min_KH;
            DateTime Max_KHNhom = dtcongtactheochuky.AsEnumerable().Where(x => x["NgayKetThucNhom"] != DBNull.Value).Any() ?
                dtcongtactheochuky.AsEnumerable().Where(x => x["NgayKetThucNhom"] != DBNull.Value).Max(x => DateTime.Parse(x["NgayKetThucNhom"].ToString())) : Max_KH;
            DateTime Max = Max_KH > Max_KHNhom ? Max_KH : Max_KHNhom;
            DateTime Min = Min_KH < Min_KHNhom ? Min_KH : Min_KHNhom;

            dbString = (!ChiTiet || ColColum == "CodeNhaThau") ? $"SELECT {TDKH.TBL_ChiTietCongTacTheoKy}.*,MIN(Ngay) AS MinNgayThiCong, MAX(Ngay) as MaxNgayThiCong  " +
      $"FROM {TDKH.TBL_ChiTietCongTacTheoKy} " +
      $"LEFT JOIN {TDKH.TBL_KhoiLuongCongViecHangNgay} " +
      $"ON {TDKH.TBL_ChiTietCongTacTheoKy}.Code = {TDKH.TBL_KhoiLuongCongViecHangNgay}.CodeCongTacTheoGiaiDoan " +
      $"WHERE {TDKH.TBL_ChiTietCongTacTheoKy}.CodeGiaiDoan = '{SharedControls.cbb_DBKH_ChonDot.SelectedValue}' " :

      $"SELECT {TDKH.TBL_ChiTietCongTacTheoKy}.*,MIN(Ngay) AS MinNgayThiCong, MAX(Ngay) as MaxNgayThiCong  " +
      $"FROM {TDKH.TBL_ChiTietCongTacTheoKy} " +
      $"LEFT JOIN {TDKH.TBL_KhoiLuongCongViecHangNgay} " +
      $"ON {TDKH.TBL_ChiTietCongTacTheoKy}.Code = {TDKH.TBL_KhoiLuongCongViecHangNgay}.CodeCongTacTheoGiaiDoan " +
      $"WHERE {TDKH.TBL_ChiTietCongTacTheoKy}.CodeGiaiDoan = '{SharedControls.cbb_DBKH_ChonDot.SelectedValue}' AND {TDKH.TBL_ChiTietCongTacTheoKy}.{ColColum}='{CodeNhaThau}' ";
            DataTable dttc = DataProvider.InstanceTHDA.ExecuteQuery(dbString);
            if (dttc.Rows.Count != 0)
            {
                DateTime Max_TC = dttc.Rows[0]["MaxNgayThiCong"] != DBNull.Value ? DateTime.Parse(dttc.Rows[0]["MaxNgayThiCong"].ToString()) : default;
                Max = Max_KH >= Max_TC ? Max_KH : Max_TC;
                DateTime Min_TC = dttc.Rows[0]["MinNgayThiCong"] != DBNull.Value ? DateTime.Parse(dttc.Rows[0]["MinNgayThiCong"].ToString()) : default;
                Min = Min_KH >= Min_TC && Min_TC != default ? Min_TC : Min_KH;
            }
            dbString = $"SELECT {TDKH.TBL_KhoiLuongCongViecHangNgay}.*,MIN(Ngay) AS MinNgayThiCong, MAX(Ngay) as MaxNgayThiCong  " +
     $"FROM {TDKH.TBL_KhoiLuongCongViecHangNgay} " +
     $"WHERE {TDKH.TBL_KhoiLuongCongViecHangNgay}.CodeNhom IN ({MyFunction.fcn_Array2listQueryCondition(codeNhoms.ToArray())})";
            dttc = DataProvider.InstanceTHDA.ExecuteQuery(dbString);
            if (dttc.Rows.Count != 0)
            {
                DateTime Max_TC = dttc.Rows[0]["MaxNgayThiCong"] != DBNull.Value ? DateTime.Parse(dttc.Rows[0]["MaxNgayThiCong"].ToString()) : default;
                Max = Max_KH >= Max_TC ? Max_KH : Max_TC;
                DateTime Min_TC = dttc.Rows[0]["MinNgayThiCong"] != DBNull.Value ? DateTime.Parse(dttc.Rows[0]["MinNgayThiCong"].ToString()) : default;
                Min = Min_KH >= Min_TC && Min_TC != default ? Min_TC : Min_KH;
            }
            _Max = Ce_Full.Checked ? Max : de_NKT.DateTime;
            _Min = Ce_Full.Checked ? Min : de_NBD.DateTime;
            DataTable dt = new DataTable();
            dt.Columns.Add("TenCongTac", typeof(string));
            dt.Columns.Add("ID", typeof(string));
            dt.Columns.Add("ParentID", typeof(string));
            dt.Columns.Add("TongKhoiLuong", typeof(double));
            dt.Columns.Add("TenColum", typeof(string));
            dt.Columns.Add("NgayBatDau", typeof(string));
            dt.Columns.Add("NgayKetThuc", typeof(string));
            dt.Columns.Add("SumNgay", typeof(int));
            tl_KiemSoat.BeginUpdate();
            List<TreeListColumn> lst = new List<TreeListColumn>();
            foreach (var item in tl_KiemSoat.Columns)
            {
                if (DateTime.TryParse(item.FieldName, out DateTime Rs))
                    lst.Add(item);
            }
            lst.ForEach(x => tl_KiemSoat.Columns.Remove(x));
            //List<KLHN> lstDate = new List<KLHN>();
            //List<DateTime> lstDateCr = new List<DateTime>();

            for (DateTime i = _Min.Date; i <= _Max.Date; i = i.AddDays(1))
            {
                //lstDate.Add(new KLHN
                //{
                //    Ngay = i
                //});
                dt.Columns.Add(i.ToString(MyConstant.CONST_DATE_FORMAT_SPSHEET), typeof(double));
                TreeListColumn col = tl_KiemSoat.Columns.Add();
                col.Caption = col.FieldName = i.ToString(MyConstant.CONST_DATE_FORMAT_SPSHEET);
                col.Visible = true;
                col.Format.FormatType = FormatType.Numeric;
                col.Format.FormatString = "n4";
            }
            tl_KiemSoat.EndUpdate();

            
            List<KLHN> rowKLHNAll = MyFunction.Fcn_CalKLKHNew(TypeKLHN.CongTac, lstCodeCT, dateBD: de_NBD.DateTime.Date, dateKT: de_NKT.DateTime.Date);
            //List<KLHNBriefViewModel> LKKHDayAll = MyFunction.CalcKLHNBrief(TypeKLHN.CongTac, lstCodeCT);
            List<KLHN> rowKLHNAllNhom = MyFunction.Fcn_CalKLKHNew(TypeKLHN.Nhom, codeNhoms, dateBD: de_NBD.DateTime.Date, dateKT: de_NKT.DateTime.Date);
            //List<KLHNBriefViewModel> LKKHDayAllNhom = MyFunction.CalcKLHNBrief(TypeKLHN.Nhom, codeNhoms, null, de_NBD.DateTime.Date.AddDays(-1));
            var grNhaThau = dtcongtactheochuky.AsEnumerable().GroupBy(x => x["CodeNhaThauChinh"]);
            foreach (var NT in grNhaThau)
            {
                DataRow RowThauChinh = dt.NewRow();
                RowThauChinh["ID"] = NT.Key.ToString();
                RowThauChinh["ParentID"] = CodeDuAn;
                RowThauChinh["TenColum"] = ColColum;
                RowThauChinh["TenCongTac"] = NT.FirstOrDefault()["TenNhaThau"].ToString();
                dt.Rows.Add(RowThauChinh);

                var grCongTrinh = NT.GroupBy(x => x["CodeCongTrinh"]);
                foreach (var Ctrinh in grCongTrinh)
                {
                    string CodeCT = Guid.NewGuid().ToString();
                    DataRow RowCT = dt.NewRow();
                    RowCT["ID"] = CodeCT;
                    RowCT["ParentID"] = NT.Key.ToString();
                    RowCT["TenCongTac"] = Ctrinh.FirstOrDefault()["TenCongTrinh"];
                    dt.Rows.Add(RowCT);

                    var grHangMuc = Ctrinh.GroupBy(x => x["CodeHangMuc"]);
                    foreach (var HM in grHangMuc)
                    {
                        string CodeHM = Guid.NewGuid().ToString();
                        DataRow RowHM = dt.NewRow();
                        RowHM["ID"] = CodeHM;
                        RowHM["ParentID"] = CodeCT;
                        RowHM["TenCongTac"] = HM.FirstOrDefault()["TenHangMuc"].ToString();
                        dt.Rows.Add(RowHM);
                        var grPhanTuyen = HM.GroupBy(x => (int)x["IndPT"])
                      .OrderBy(x => x.Key);
                        foreach (var Tuyen in grPhanTuyen)
                        {
                            var fstTuyen = Tuyen.First();
                            string crCodeTuyen = (fstTuyen["CodePhanTuyen"] == DBNull.Value) ? null : $"{fstTuyen["CodePhanTuyen"]}_{Guid.NewGuid()}_CodeTuyen";
                            if (fstTuyen["CodePhanTuyen"] != DBNull.Value)
                            {
                                var grTuyenNhomNew = Tuyen.GroupBy(x => (int)x["IndNhom"])
                    .OrderBy(x => x.Key);
                                DataRow RowCongTac = dt.NewRow();
                                RowCongTac["ID"] = crCodeTuyen;
                                RowCongTac["ParentID"] = CodeHM;
                                RowCongTac["TenCongTac"] = fstTuyen["TenTuyen"];
                                dt.Rows.Add(RowCongTac);
                            }
                            var grTuyenNhom = Tuyen.GroupBy(x => (int)x["IndNhom"])
                                                .OrderBy(x => x.Key);
                            foreach (var Nhom in grTuyenNhom)
                            {
                                var fstNhom = Nhom.First();
                                if (fstNhom["CodeNhom"] == DBNull.Value)
                                {
                                    goto LabelCongTac;
                                }
                                else
                                {
                                    if (fstNhom["KLKHNhom"] != DBNull.Value)
                                    {
                                        string CodeCTac = fstNhom["CodeNhom"].ToString();
                                        string CodeCongTac = $"{Guid.NewGuid()}_Nhom";
                                        List<KLHN> rowKLHN = rowKLHNAllNhom.Where(x => x.ParentCode == CodeCTac).ToList();
                                        if (!rowKLHN.Any() && !Ce_NgoaiKH.Checked)
                                            continue;
                                        var LKKHDay = rowKLHN.Where(x => x.Ngay == de_NBD.DateTime.Date).FirstOrDefault();
                                        double LKKH = (LKKHDay?.LuyKeKhoiLuongKeHoach - LKKHDay?.KhoiLuongKeHoach) ?? 0;
                                        double LKTC = (LKKHDay?.LuyKeKhoiLuongThiCong - LKKHDay?.KhoiLuongThiCong) ?? 0;
                                        double KLKH = (double)rowKLHN.Sum(x => x.KhoiLuongKeHoach);
                                       // WaitFormHelper.ShowWaitForm($"{fstNhom["TenNhom"]}", "Đang tổng hợp dữ liệu");
                                        DataRow RowCongTac = dt.NewRow();
                                        RowCongTac["ID"] = CodeCongTac;
                                        RowCongTac["ParentID"] = crCodeTuyen ?? CodeHM;
                                        RowCongTac["TenCongTac"] = fstNhom["TenNhom"];
                                        RowCongTac["TongKhoiLuong"] = fstNhom["KLKHNhom"];
                                        dt.Rows.Add(RowCongTac);

                                        DataRow RowKH = dt.NewRow();
                                        DataRow RowTC = dt.NewRow();
                                        DataRow RowKHLK = dt.NewRow();
                                        DataRow RowTCLK = dt.NewRow();
                                        DataRow RowConLai = dt.NewRow();
                                        DataRow RowDongTrong = dt.NewRow();

                                        Fcn_AddChiTiet(RowKH, "Kế hoạch chi tiết", CodeCongTac);
                                        Fcn_AddChiTiet(RowTC, "Thi công chi tiết", CodeCongTac);
                                        Fcn_AddChiTiet(RowKHLK, "Lũy kế kế hoạch", CodeCongTac);
                                        Fcn_AddChiTiet(RowTCLK, "Lũy kế thi công", CodeCongTac);
                                        Fcn_AddChiTiet(RowConLai, "Còn lại (Kế hoạch-Thi công)", CodeCongTac);
                                        Fcn_AddChiTiet(RowDongTrong, "", CodeCongTac);
                                        DateTime NBD = DateTime.Parse(fstNhom["NgayBatDauNhom"].ToString());
                                        DateTime NKT = DateTime.Parse(fstNhom["NgayKetThucNhom"].ToString());
                                        double NKH = (NKT - NBD).TotalDays + 1;
                                        RowKH["TongKhoiLuong"] = Math.Round(KLKH / NKH, 2);
                                        RowKH["SumNgay"] = NKH;
                                        RowKH["NgayBatDau"] = NBD.ToString(MyConstant.CONST_DATE_FORMAT_SPSHEET);
                                        RowKH["NgayKetThuc"] = NKT.ToString(MyConstant.CONST_DATE_FORMAT_SPSHEET);

                                        List<KLHN> rowKLHNTC = rowKLHN.Where(x => x.KhoiLuongThiCong.HasValue && x.KhoiLuongThiCong > 0).ToList();
                                        if (!rowKLHNTC.Any())
                                            goto LabelNhom;
                                        double KLTC = (double)rowKLHNTC.Sum(x => x.KhoiLuongThiCong);
                                        DateTime MIN = (DateTime)rowKLHNTC.Min(x => x.Ngay);
                                        DateTime MAX = (DateTime)rowKLHNTC.Max(x => x.Ngay);
                                        double NTC = (DateTime.Now.Date - MIN.Date).TotalDays + 1;
                                        double NTCSum = (MAX.Date - MIN.Date).TotalDays + 1;
                                        RowTC["SumNgay"] = NTCSum;
                                        RowTC["NgayBatDau"] = MIN.ToString(MyConstant.CONST_DATE_FORMAT_SPSHEET);
                                        RowTC["NgayKetThuc"] = MAX.ToString(MyConstant.CONST_DATE_FORMAT_SPSHEET);
                                        if (NTC > 0)
                                            RowTC["TongKhoiLuong"] = Math.Round(KLTC / NTC, 2);
                                        LabelNhom:
                                        double SumConLai = 0, KLCL;
                                        foreach (KLHN item in rowKLHN.OrderBy(x => x.Ngay))
                                        {
                                            string colum = item.Ngay.ToString(MyConstant.CONST_DATE_FORMAT_SPSHEET);
                                            //if (!dt.Columns.Contains(colum))
                                            //    goto Label1;
                                            double KLKHDay = item.KhoiLuongKeHoach ?? 0;
                                            LKKH += KLKHDay;


                                            RowKH[colum] = KLKHDay;
                                            RowKHLK[colum] = Math.Round(LKKH, 4);

                                            if (item.Ngay.Date <= de_NKT.DateTime.Date)
                                            {
                                                double KLTCDay = item.KhoiLuongThiCong ?? 0;

                                                if (KLTCDay > 0)
                                                {
                                                    RowTC[colum] = KLTCDay;
                                                    LKTC += KLTCDay;
                                                }
                                                KLCL = LKKH - LKTC;
                                                SumConLai = KLCL;
                                                RowTCLK[colum] = Math.Round(LKTC, 4);
                                                RowConLai[colum] = Math.Round(KLCL, 4);
                                            }
                                        }
                                        RowTCLK["TongKhoiLuong"] = Math.Round(LKTC, 4);
                                        RowKHLK["TongKhoiLuong"] = Math.Round(LKKH, 4);
                                        RowConLai["TongKhoiLuong"] = Math.Round(SumConLai, 4);
                                        Label1:
                                        dt.Rows.Add(RowKH);
                                        dt.Rows.Add(RowTC);
                                        dt.Rows.Add(RowKHLK);
                                        dt.Rows.Add(RowTCLK);
                                        dt.Rows.Add(RowConLai);
                                        dt.Rows.Add(RowDongTrong);
                                        continue;
                                    }
                                    else
                                        goto LabelCongTac;
                                }
                                LabelCongTac:
                                var grCongTac = Nhom.GroupBy(x => x["Code"]);
                                foreach (var CTac in grCongTac)
                                {
                                    if (lstCode.Contains(CTac.Key.ToString()))
                                        continue;
                                    string CodeCTac = CTac.Key.ToString();
                                    string CodeCongTac = Guid.NewGuid().ToString();
                                    List<KLHN> rowKLHN = rowKLHNAll.Where(x => x.ParentCode == CodeCTac).ToList();
                                    if (!rowKLHN.Any() && !Ce_NgoaiKH.Checked)
                                        continue;
                                    var LKKHDay = rowKLHN.Where(x => x.Ngay == de_NBD.DateTime.Date).FirstOrDefault();
                                    double LKKH = (LKKHDay?.LuyKeKhoiLuongKeHoach - LKKHDay?.KhoiLuongKeHoach) ?? 0;
                                    double LKTC = (LKKHDay?.LuyKeKhoiLuongThiCong - LKKHDay?.KhoiLuongThiCong) ?? 0;
                                    double KLKH = (double)rowKLHN.Sum(x => x.KhoiLuongKeHoach);
                                    //WaitFormHelper.ShowWaitForm($"{CTac.FirstOrDefault()["TenCongTac"]}", "Đang tổng hợp dữ liệu");
                                    DataRow RowCongTac = dt.NewRow();
                                    RowCongTac["ID"] = CodeCongTac;
                                    RowCongTac["ParentID"] = crCodeTuyen ?? CodeHM;
                                    RowCongTac["TenCongTac"] = CTac.FirstOrDefault()["TenCongTac"].ToString();
                                    RowCongTac["TongKhoiLuong"] = CTac.FirstOrDefault()["KhoiLuongHopDongChiTiet"];
                                    dt.Rows.Add(RowCongTac);


                                    DataRow RowKH = dt.NewRow();
                                    DataRow RowTC = dt.NewRow();
                                    DataRow RowKHLK = dt.NewRow();
                                    DataRow RowTCLK = dt.NewRow();
                                    DataRow RowConLai = dt.NewRow();
                                    DataRow RowDongTrong = dt.NewRow();

                                    Fcn_AddChiTiet(RowKH, "Kế hoạch chi tiết", CodeCongTac);
                                    Fcn_AddChiTiet(RowTC, "Thi công chi tiết", CodeCongTac);
                                    Fcn_AddChiTiet(RowKHLK, "Lũy kế kế hoạch", CodeCongTac);
                                    Fcn_AddChiTiet(RowTCLK, "Lũy kế thi công", CodeCongTac);
                                    Fcn_AddChiTiet(RowConLai, "Còn lại (Kế hoạch-Thi công)", CodeCongTac);
                                    Fcn_AddChiTiet(RowDongTrong, "", CodeCongTac);
                                    DateTime NBD = DateTime.Parse(CTac.FirstOrDefault()["NgayBatDau"].ToString());
                                    DateTime NKT = DateTime.Parse(CTac.FirstOrDefault()["NgayKetThuc"].ToString());
                                    double NKH = (NKT - NBD).TotalDays + 1;


                                    RowKH["TongKhoiLuong"] = Math.Round(KLKH / NKH, 2);
                                    RowKH["SumNgay"] = NKH;
                                    RowKH["NgayBatDau"] = NBD.ToString(MyConstant.CONST_DATE_FORMAT_SPSHEET);
                                    RowKH["NgayKetThuc"] = NKT.ToString(MyConstant.CONST_DATE_FORMAT_SPSHEET);

                                    List<KLHN> rowKLHNTC = rowKLHN.Where(x => x.KhoiLuongThiCong.HasValue && x.KhoiLuongThiCong > 0).ToList();
                                    if (!rowKLHNTC.Any())
                                        goto Label;
                                    double KLTC = (double)rowKLHNTC.Sum(x => x.KhoiLuongThiCong);
                                    DateTime MIN = (DateTime)rowKLHNTC.Min(x => x.Ngay);
                                    DateTime MAX = (DateTime)rowKLHNTC.Max(x => x.Ngay);
                                    double NTC = (DateTime.Now.Date - MIN.Date).TotalDays + 1;
                                    double NTCSum = (MAX.Date - MIN.Date).TotalDays + 1;
                                    RowTC["SumNgay"] = NTCSum;
                                    RowTC["NgayBatDau"] = MIN.ToString(MyConstant.CONST_DATE_FORMAT_SPSHEET);
                                    RowTC["NgayKetThuc"] = MAX.ToString(MyConstant.CONST_DATE_FORMAT_SPSHEET);
                                    if (NTC > 0)
                                        RowTC["TongKhoiLuong"] = Math.Round(KLTC / NTC, 2);
                                    Label:
                                    double SumConLai = 0, KLCL = 0;
                                    //lstDateCr = rowKLHN.Select(x => x.Ngay.Date).Distinct().ToList();
                                    //foreach (var itemdate in lstDate)
                                    //{
                                    //    if (lstDateCr.Contains(itemdate.Ngay))
                                    //        continue;
                                    //    rowKLHN.Add(itemdate);
                                    //}
                                    foreach (KLHN item in rowKLHN.OrderBy(x => x.Ngay))
                                    {
                                        string colum = item.Ngay.ToString(MyConstant.CONST_DATE_FORMAT_SPSHEET);
                                        //if (!dt.Columns.Contains(colum))
                                        //    goto Label1;
                                        double KLKHDay = item.KhoiLuongKeHoach ?? 0;
                                        LKKH += KLKHDay;
                                        RowKH[colum] = KLKHDay;
                                        RowKHLK[colum] = Math.Round(LKKH, 4);

                                        if (item.Ngay.Date <= de_NKT.DateTime.Date)
                                        {
                                            double KLTCDay = item.KhoiLuongThiCong ?? 0;
                                            if (KLTCDay > 0)
                                            {
                                                LKTC += KLTCDay;
                                                RowTC[colum] = KLTCDay;
                                            }
                                            KLCL = LKKH - LKTC;
                                            SumConLai = KLCL;
                                            RowTCLK[colum] = Math.Round(LKTC, 4);
                                            RowConLai[colum] = Math.Round(KLCL, 4);
                                        }
                                    }
                                    RowTCLK["TongKhoiLuong"] = Math.Round(LKTC, 4);
                                    RowKHLK["TongKhoiLuong"] = Math.Round(LKKH, 4);
                                    RowConLai["TongKhoiLuong"] = Math.Round(SumConLai, 4);
                                    Label1:
                                    dt.Rows.Add(RowKH);
                                    dt.Rows.Add(RowTC);
                                    dt.Rows.Add(RowKHLK);
                                    dt.Rows.Add(RowTCLK);
                                    dt.Rows.Add(RowConLai);
                                    dt.Rows.Add(RowDongTrong);
                                }
                            }

                        }
                    }


                }

            }
            tl_KiemSoat.RefreshDataSource();
            tl_KiemSoat.Refresh();
            tl_KiemSoat.DataSource = null;

            tl_KiemSoat.DataSource = dt;
            if (IsBrief)
            {
                col_tenCongViec.FilterInfo = new TreeListColumnFilterInfo($"[TenCongTac] = 'Còn lại (Kế hoạch-Thi công)' OR [TenCongTac] = 'Lũy kế kế hoạch' OR [TenCongTac] = 'Lũy kế thi công' OR [TenCongTac] = ''");
            }


            tl_KiemSoat.RefreshDataSource();
            tl_KiemSoat.Refresh();
            tl_KiemSoat.ExpandAll();
            //tl_KiemSoat.EndUpdate();
            tl_KiemSoat.FocusedColumn = tl_KiemSoat.Columns[DateTime.Now.ToString(MyConstant.CONST_DATE_FORMAT_SPSHEET)];
            var colNow = tl_KiemSoat.Columns[DateTime.Now.ToString(MyConstant.CONST_DATE_FORMAT_SPSHEET)];
            if (colNow != null)
                colNow.AppearanceHeader.BackColor = Color.Yellow;
            WaitFormHelper.CloseWaitForm();
        }
        private void Fcn_UpdateData(string CodeDuAn, bool ChiTiet, string ColColum = null, string CodeNhaThau = null)
        {
            if (ce_ChiTietTuyen.Checked)
            {
                Fcn_UpdateDataChiTietTuyen(CodeDuAn, ChiTiet, ColColum, CodeNhaThau);
                return;
            }
            WaitFormHelper.ShowWaitForm("Đang phân tích dữ liệu chi tiết hằng ngày!!!! Vui lòng chờ!!!!");
            string dbString = ChiTiet ? $"SELECT COALESCE(cttk.CodePhanTuyen, hm.CodePhanTuyen) AS CodePhanTuyen," +
                $"COALESCE(TuyenCT.Ten, hm.TenPhanTuyen) AS TenTuyen," +
                $" COALESCE(hm.Code, hmct.Code) AS CodeHangMuc,COALESCE(hm.Ten, hmct.Ten) AS TenHangMuc," +
         $"COALESCE(ctrinh.Code, ctrinhct.Code) AS CodeCongTrinh,COALESCE(ctrinh.Ten, ctrinhct.Ten) AS TenCongTrinh," +
         $"COALESCE(cttk.CodeNhom,cttkCha.CodeNhom) AS CodeNhom,nct.Ten as TenNhom,nct.KhoiLuongKeHoach as KLKHNhom,nct.DonVi as DonViNhom," +
         $"nct.NgayBatDau as NgayBatDauNhom,nct.NgayKetThuc as NgayKetThucNhom," +
         $"COALESCE(cttk.DonVi, dmct.DonVi) AS DonVi,COALESCE(cttk.TenCongTac, dmct.TenCongTac) AS TenCongTac," +
         $"COALESCE(cttk.MaHieuCongTac, dmct.MaHieuCongTac) AS MaHieuCongTac, \r\n" +
         $"hm.CodeCongTrinh, \r\n" +
         $"COALESCE(ctrinh.CodeDuAn, ctrinhct.CodeDuAn) AS CodeDuAn, \r\n" +
         $"COALESCE(da.TenDuAn, dact.TenDuAn) AS TenDuAn, \r\n" +
         $"COALESCE(nt.Ten, ntp.Ten,tdtc.Ten) AS TenNhaThau, \r\n" +
         $"COALESCE(nt.Code, ntp.Code,tdtc.Code) AS CodeNhaThauChinh, \r\n" +
         $"COALESCE(ctrinh.SortId, ctrinhct.SortId) AS SortIdCtrinh,COALESCE(hm.SortId, hmct.SortId) AS SortIdHM,cttk.* \r\n" +
         $"FROM {TDKH.TBL_ChiTietCongTacTheoKy} cttk\r\n" +
         $"LEFT JOIN {TDKH.TBL_ChiTietCongTacTheoKy} cttkCha\r\n" +
         $"ON cttk.CodeCha = cttkCha.Code \r\n" +
         $"LEFT JOIN {TDKH.TBL_NhomCongTac} nct\r\n" +
         $"ON nct.Code = cttk.CodeNhom \r\n" +
         $"INNER JOIN {TDKH.TBL_DanhMucCongTac} dmct\r\n" +
         $"ON cttk.CodeCongTac = dmct.Code \r\n" +
                           $" JOIN {MyConstant.view_HangMucWithPhanTuyen} hm\r\n" +
                             $"ON (hm.Code = dmct.CodeHangMuc AND ((dmct.CodePhanTuyen IS NOT NULL AND hm.CodePhanTuyen = dmct.CodePhanTuyen) " +
                             $"OR (dmct.CodePhanTuyen IS NULL AND hm.CodePhanTuyen IS NULL)))\r\n" +
                              $" LEFT JOIN {MyConstant.view_HangMucWithPhanTuyen} TuyenCT  " +
                              $"ON (hm.Code = cttk.CodeHangMuc AND ((cttk.CodePhanTuyen IS NOT NULL AND TuyenCT.CodePhanTuyen = cttk.CodePhanTuyen) " +
                             $"OR (cttk.CodePhanTuyen IS NULL AND TuyenCT.CodePhanTuyen IS NULL)))\r\n" +
         $"INNER JOIN {MyConstant.TBL_THONGTINCONGTRINH} ctrinh\r\n" +
         $"ON hm.CodeCongTrinh = ctrinh.Code \r\n" +
         $"INNER JOIN {MyConstant.TBL_THONGTINDUAN} da\r\n" +
         $"ON ctrinh.CodeDuAn = da.Code \r\n" +
         $"LEFT JOIN {MyConstant.TBL_THONGTINNHATHAU} nt\r\n" +
         $"ON cttk.CodeNhaThau = nt.Code \r\n" +
         $"LEFT JOIN {MyConstant.TBL_THONGTINTODOITHICONG} tdtc\r\n" +
         $"ON cttk.CodeToDoi = tdtc.Code \r\n" +
         $"LEFT JOIN {MyConstant.TBL_THONGTINNHATHAUPHU} ntp \r\n" +
         $"ON cttk.CodeNhaThauPhu = ntp.Code \r\n" +
         $"LEFT JOIN Tbl_ThongTinHangMuc hmct  ON cttk.CodeHangMuc = hmct.Code " +
         $"LEFT JOIN Tbl_ThongTinCongTrinh ctrinhct  ON hmct.CodeCongTrinh = ctrinhct.Code " +
         $"LEFT JOIN Tbl_ThongTinDuAn dact  ON ctrinhct.CodeDuAn = dact.Code " +
         $"WHERE  cttk.{ColColum}='{CodeNhaThau}' AND" +
         $" (cttk.TrangThai IN ({MyFunction.fcn_Array2listQueryCondition(MyConstant.TrangThai)}) OR nct.TrangThai IN ({MyFunction.fcn_Array2listQueryCondition(MyConstant.TrangThai)}))  " +
         $"ORDER BY SortIdCtrinh ASC,SortIdHM ASC, cttk.SortId ASC\r\n" :

        $"SELECT COALESCE(cttk.CodePhanTuyen, hm.CodePhanTuyen) AS CodePhanTuyen," +
                $"COALESCE(TuyenCT.Ten, hm.TenPhanTuyen) AS TenTuyen," +
                $" COALESCE(hm.Code, hmct.Code) AS CodeHangMuc,COALESCE(hm.Ten, hmct.Ten) AS TenHangMuc," +
         $"COALESCE(cttk.CodeNhom,cttkCha.CodeNhom) AS CodeNhom,nct.Ten as TenNhom,nct.KhoiLuongKeHoach as KLKHNhom,nct.DonVi as DonViNhom," +
         $"nct.NgayBatDau as NgayBatDauNhom,nct.NgayKetThuc as NgayKetThucNhom," +
         $"COALESCE(ctrinh.Code, ctrinhct.Code) AS CodeCongTrinh,COALESCE(ctrinh.Ten, ctrinhct.Ten) AS TenCongTrinh," +
         $"COALESCE(cttk.DonVi, dmct.DonVi) AS DonVi,COALESCE(cttk.TenCongTac, dmct.TenCongTac) AS TenCongTac," +
         $"COALESCE(cttk.MaHieuCongTac, dmct.MaHieuCongTac) AS MaHieuCongTac, \r\n" +
         $"hm.CodeCongTrinh, \r\n" +
         $"COALESCE(ctrinh.CodeDuAn, ctrinhct.CodeDuAn) AS CodeDuAn, \r\n" +
         $"COALESCE(da.TenDuAn, dact.TenDuAn) AS TenDuAn, \r\n" +
         $"COALESCE(nt.Ten, ntp.Ten,tdtc.Ten) AS TenNhaThau, \r\n" +
         $"COALESCE(nt.Code, ntp.Code,tdtc.Code) AS CodeNhaThauChinh, \r\n" +
         $"COALESCE(ctrinh.SortId, ctrinhct.SortId) AS SortIdCtrinh,COALESCE(hm.SortId, hmct.SortId) AS SortIdHM,cttk.* \r\n" +
         $"FROM {TDKH.TBL_ChiTietCongTacTheoKy} cttk\r\n" +
         $"LEFT JOIN {TDKH.TBL_ChiTietCongTacTheoKy} cttkCha\r\n" +
         $"ON cttk.CodeCha = cttkCha.Code \r\n" +
         $"LEFT JOIN {TDKH.TBL_NhomCongTac} nct\r\n" +
         $"ON nct.Code = cttk.CodeNhom \r\n" +
         $"INNER JOIN {TDKH.TBL_DanhMucCongTac} dmct\r\n" +
         $"ON cttk.CodeCongTac = dmct.Code \r\n" +
                              $" JOIN {MyConstant.view_HangMucWithPhanTuyen} hm\r\n" +
                             $"ON (hm.Code = dmct.CodeHangMuc AND ((dmct.CodePhanTuyen IS NOT NULL AND hm.CodePhanTuyen = dmct.CodePhanTuyen) " +
                             $"OR (dmct.CodePhanTuyen IS NULL AND hm.CodePhanTuyen IS NULL)))\r\n" +
                              $" LEFT JOIN {MyConstant.view_HangMucWithPhanTuyen} TuyenCT  " +
                              $"ON (hm.Code = cttk.CodeHangMuc AND ((cttk.CodePhanTuyen IS NOT NULL AND TuyenCT.CodePhanTuyen = cttk.CodePhanTuyen) " +
                             $"OR (cttk.CodePhanTuyen IS NULL AND TuyenCT.CodePhanTuyen IS NULL)))\r\n" +
         $"INNER JOIN {MyConstant.TBL_THONGTINCONGTRINH} ctrinh\r\n" +
         $"ON hm.CodeCongTrinh = ctrinh.Code \r\n" +
         $"INNER JOIN {MyConstant.TBL_THONGTINDUAN} da\r\n" +
         $"ON ctrinh.CodeDuAn = da.Code \r\n" +
         $"LEFT JOIN {MyConstant.TBL_THONGTINNHATHAU} nt\r\n" +
         $"ON cttk.CodeNhaThau = nt.Code \r\n" +
         $"LEFT JOIN {MyConstant.TBL_THONGTINTODOITHICONG} tdtc\r\n" +
         $"ON cttk.CodeToDoi = tdtc.Code \r\n" +
         $"LEFT JOIN {MyConstant.TBL_THONGTINNHATHAUPHU} ntp \r\n" +
         $"ON cttk.CodeNhaThauPhu = ntp.Code \r\n" +
         $"LEFT JOIN Tbl_ThongTinHangMuc hmct  ON cttk.CodeHangMuc = hmct.Code " +
         $"LEFT JOIN Tbl_ThongTinCongTrinh ctrinhct  ON hmct.CodeCongTrinh = ctrinhct.Code " +
         $"LEFT JOIN Tbl_ThongTinDuAn dact  ON ctrinhct.CodeDuAn = dact.Code AND dact.Code = '{CodeDuAn}' " +
         $"WHERE  da.Code = '{CodeDuAn}' AND " +
         $" (cttk.TrangThai IN ({MyFunction.fcn_Array2listQueryCondition(MyConstant.TrangThai)}) OR nct.TrangThai IN ({MyFunction.fcn_Array2listQueryCondition(MyConstant.TrangThai)}))  " +
         $"ORDER BY SortIdCtrinh ASC,SortIdHM ASC, cttk.SortId ASC\r\n";

            DataTable dtcongtactheochuky = DataProvider.InstanceTHDA.ExecuteQuery(dbString);
            dtcongtactheochuky.AddIndPhanTuyenNhom();

            List<string> codeNhoms = null;
   
            if (Ce_Full.Checked)
            {
                codeNhoms = dtcongtactheochuky.AsEnumerable().Where(x => x["CodeNhom"] != DBNull.Value)
.Select(x => x["CodeNhom"].ToString()).Distinct().ToList();
                DateTime Min_KH = dtcongtactheochuky.AsEnumerable().Where(x => x["NgayBatDau"] != DBNull.Value).Min(x => DateTime.Parse(x["NgayBatDau"].ToString()));
                DateTime Max_KH = dtcongtactheochuky.AsEnumerable().Where(x => x["NgayKetThuc"] != DBNull.Value).Max(x => DateTime.Parse(x["NgayKetThuc"].ToString()));
                DateTime Min_KHNhom = dtcongtactheochuky.AsEnumerable().Where(x => x["NgayBatDauNhom"] != DBNull.Value).Any() ?
                     dtcongtactheochuky.AsEnumerable().Where(x => x["NgayBatDauNhom"] != DBNull.Value).Min(x => DateTime.Parse(x["NgayBatDauNhom"].ToString())) : Min_KH;
                DateTime Max_KHNhom = dtcongtactheochuky.AsEnumerable().Where(x => x["NgayKetThucNhom"] != DBNull.Value).Any() ?
                    dtcongtactheochuky.AsEnumerable().Where(x => x["NgayKetThucNhom"] != DBNull.Value).Max(x => DateTime.Parse(x["NgayKetThucNhom"].ToString())) : Max_KH;
                DateTime Max = Max_KH > Max_KHNhom ? Max_KH : Max_KHNhom;
                DateTime Min = Min_KH < Min_KHNhom ? Min_KH : Min_KHNhom;

                dbString = (!ChiTiet || ColColum == "CodeNhaThau") ? $"SELECT {TDKH.TBL_ChiTietCongTacTheoKy}.*,MIN(Ngay) AS MinNgayThiCong, MAX(Ngay) as MaxNgayThiCong  " +
          $"FROM {TDKH.TBL_ChiTietCongTacTheoKy} " +
          $"LEFT JOIN {TDKH.TBL_KhoiLuongCongViecHangNgay} " +
          $"ON {TDKH.TBL_ChiTietCongTacTheoKy}.Code = {TDKH.TBL_KhoiLuongCongViecHangNgay}.CodeCongTacTheoGiaiDoan " +
          $"WHERE {TDKH.TBL_ChiTietCongTacTheoKy}.CodeGiaiDoan = '{SharedControls.cbb_DBKH_ChonDot.SelectedValue}' " :

          $"SELECT {TDKH.TBL_ChiTietCongTacTheoKy}.*,MIN(Ngay) AS MinNgayThiCong, MAX(Ngay) as MaxNgayThiCong  " +
          $"FROM {TDKH.TBL_ChiTietCongTacTheoKy} " +
          $"LEFT JOIN {TDKH.TBL_KhoiLuongCongViecHangNgay} " +
          $"ON {TDKH.TBL_ChiTietCongTacTheoKy}.Code = {TDKH.TBL_KhoiLuongCongViecHangNgay}.CodeCongTacTheoGiaiDoan " +
          $"WHERE {TDKH.TBL_ChiTietCongTacTheoKy}.CodeGiaiDoan = '{SharedControls.cbb_DBKH_ChonDot.SelectedValue}' AND {TDKH.TBL_ChiTietCongTacTheoKy}.{ColColum}='{CodeNhaThau}' ";
                DataTable dttc = DataProvider.InstanceTHDA.ExecuteQuery(dbString);
                if (dttc.Rows.Count != 0)
                {
                    DateTime Max_TC = dttc.Rows[0]["MaxNgayThiCong"] != DBNull.Value ? DateTime.Parse(dttc.Rows[0]["MaxNgayThiCong"].ToString()) : default;
                    Max = Max_KH >= Max_TC ? Max_KH : Max_TC;
                    DateTime Min_TC = dttc.Rows[0]["MinNgayThiCong"] != DBNull.Value ? DateTime.Parse(dttc.Rows[0]["MinNgayThiCong"].ToString()) : default;
                    Min = Min_KH >= Min_TC && Min_TC != default ? Min_TC : Min_KH;
                }
                dbString = $"SELECT {TDKH.TBL_KhoiLuongCongViecHangNgay}.*,MIN(Ngay) AS MinNgayThiCong, MAX(Ngay) as MaxNgayThiCong  " +
         $"FROM {TDKH.TBL_KhoiLuongCongViecHangNgay} " +
         $"WHERE {TDKH.TBL_KhoiLuongCongViecHangNgay}.CodeNhom IN ({MyFunction.fcn_Array2listQueryCondition(codeNhoms.ToArray())})";
                dttc = DataProvider.InstanceTHDA.ExecuteQuery(dbString);
                if (dttc.Rows.Count != 0)
                {
                    DateTime Max_TC = dttc.Rows[0]["MaxNgayThiCong"] != DBNull.Value ? DateTime.Parse(dttc.Rows[0]["MaxNgayThiCong"].ToString()) : default;
                    Max = Max_KH >= Max_TC ? Max_KH : Max_TC;
                    DateTime Min_TC = dttc.Rows[0]["MinNgayThiCong"] != DBNull.Value ? DateTime.Parse(dttc.Rows[0]["MinNgayThiCong"].ToString()) : default;
                    Min = Min_KH >= Min_TC && Min_TC != default ? Min_TC : Min_KH;
                }
                //_Max = Ce_Full.Checked ? Max : de_NKT.DateTime;
                //_Min = Ce_Full.Checked ? Min : de_NBD.DateTime;
            }
            //if(!Ce_Full.Checked&& !Ce_NgoaiKH.Checked)
            //{
            //    DataRow[] Row = dtcongtactheochuky.AsEnumerable().
            //        Where(x => DateTime.Parse(x["NgayBatDau"].ToString()).Date >= de_NBD.DateTime.Date&& 
            //        DateTime.Parse(x["NgayKetThuc"].ToString()).Date <= de_NKT.DateTime.Date).ToArray();
            //    if (Row.Any())
            //        dtcongtactheochuky = Row.CopyToDataTable();
            //    else
            //    {
            //        tl_KiemSoat.DataSource = null;
            //        return;
            //    }    
            //}
            codeNhoms = dtcongtactheochuky.AsEnumerable().Where(x => x["CodeNhom"] != DBNull.Value)
       .Select(x => x["CodeNhom"].ToString()).Distinct().ToList();

            string[] lstCode = dtcongtactheochuky.AsEnumerable().Select(x => x["CodeCha"].ToString()).Distinct().ToArray();
            string[] lstCodeCT = dtcongtactheochuky.AsEnumerable().Where(x => !lstCode.Contains(x["Code"].ToString())).Select(x => x["Code"].ToString()).Distinct().ToArray();       
            DataTable dt = new DataTable();
            dt.Columns.Add("TenCongTac", typeof(string));
            dt.Columns.Add("ID", typeof(string));
            dt.Columns.Add("ParentID", typeof(string));
            dt.Columns.Add("TongKhoiLuong", typeof(double));
            dt.Columns.Add("TenColum", typeof(string));
            dt.Columns.Add("NgayBatDau", typeof(string));
            dt.Columns.Add("NgayKetThuc", typeof(string));
            dt.Columns.Add("SumNgay", typeof(int));
            tl_KiemSoat.BeginUpdate();
            List<TreeListColumn> lst = new List<TreeListColumn>();
            foreach (var item in tl_KiemSoat.Columns)
            {
                if (DateTime.TryParse(item.FieldName, out DateTime Rs))
                    lst.Add(item);
            }
            lst.ForEach(x => tl_KiemSoat.Columns.Remove(x));
            //List<KLHN> lstDate = new List<KLHN>();
            //List<DateTime> lstDateCr = new List<DateTime>();

            for (DateTime i = de_NBD.DateTime.Date; i <= de_NKT.DateTime.Date; i = i.AddDays(1))
            {
                //lstDate.Add(new KLHN
                //{
                //    Ngay = i
                //});
                dt.Columns.Add(i.ToString(MyConstant.CONST_DATE_FORMAT_SPSHEET), typeof(double));
                TreeListColumn col = tl_KiemSoat.Columns.Add();
                col.Caption = col.FieldName = i.ToString(MyConstant.CONST_DATE_FORMAT_SPSHEET);
                col.Visible = true;
                col.Format.FormatType = FormatType.Numeric;
                col.Format.FormatString = $"n{SharedControls.Error}";
            }
            tl_KiemSoat.EndUpdate();
            List<KLHN> rowKLHNAll = MyFunction.Fcn_CalKLKHNew(TypeKLHN.CongTac, lstCodeCT, dateBD: de_NBD.DateTime.Date, dateKT: de_NKT.DateTime.Date);

            List<KLHN> rowKLHNAllNhom = MyFunction.Fcn_CalKLKHNew(TypeKLHN.Nhom, codeNhoms, dateBD: de_NBD.DateTime.Date, dateKT: de_NKT.DateTime.Date);

            var grNhaThau = dtcongtactheochuky.AsEnumerable().GroupBy(x => x["CodeNhaThauChinh"]);
            foreach (var NT in grNhaThau)
            {
                DataRow RowThauChinh = dt.NewRow();
                RowThauChinh["ID"] = NT.Key.ToString();
                RowThauChinh["ParentID"] = CodeDuAn;
                RowThauChinh["TenColum"] = ColColum;
                RowThauChinh["TenCongTac"] = NT.FirstOrDefault()["TenNhaThau"].ToString();
                dt.Rows.Add(RowThauChinh);

                var grCongTrinh = NT.GroupBy(x => x["CodeCongTrinh"]);
                foreach (var Ctrinh in grCongTrinh)
                {
                    string CodeCT = Guid.NewGuid().ToString();
                    DataRow RowCT = dt.NewRow();
                    RowCT["ID"] = CodeCT;
                    RowCT["ParentID"] = NT.Key.ToString();
                    RowCT["TenCongTac"] = Ctrinh.FirstOrDefault()["TenCongTrinh"];
                    dt.Rows.Add(RowCT);

                    var grHangMuc = Ctrinh.GroupBy(x => x["CodeHangMuc"]);
                    foreach (var HM in grHangMuc)
                    {
                        string CodeHM = Guid.NewGuid().ToString();
                        DataRow RowHM = dt.NewRow();
                        RowHM["ID"] = CodeHM;
                        RowHM["ParentID"] = CodeCT;
                        RowHM["TenCongTac"] = HM.FirstOrDefault()["TenHangMuc"].ToString();
                        dt.Rows.Add(RowHM);
                        var grPhanTuyen = HM.GroupBy(x => (int)x["IndPT"])
                      .OrderBy(x => x.Key);
                        foreach (var Tuyen in grPhanTuyen)
                        {                            
                            var fstTuyen = Tuyen.First();
                            string crCodeTuyen = (fstTuyen["CodePhanTuyen"] == DBNull.Value) ? null : $"{fstTuyen["CodePhanTuyen"]}_{Guid.NewGuid()}_CodeTuyen";
                            if (fstTuyen["CodePhanTuyen"] != DBNull.Value)
                            {
                                var grTuyenNhomNew = Tuyen.GroupBy(x => (int)x["IndNhom"])
                    .OrderBy(x => x.Key);
                               // WaitFormHelper.ShowWaitForm($"{fstTuyen["TenTuyen"]}", "Đang tổng hợp dữ liệu");
                                DataRow RowCongTac = dt.NewRow();
                                RowCongTac["ID"] = crCodeTuyen;
                                RowCongTac["ParentID"] = CodeHM;
                                RowCongTac["TenCongTac"] = fstTuyen["TenTuyen"];
                                DataRow RowKH = dt.NewRow();
                                DataRow RowTC = dt.NewRow();
                                DataRow RowKHLK = dt.NewRow();
                                DataRow RowTCLK = dt.NewRow();
                                DataRow RowConLai = dt.NewRow();
                                DataRow RowDongTrong = dt.NewRow();

                                Fcn_AddChiTiet(RowKH, "Kế hoạch chi tiết", crCodeTuyen);
                                Fcn_AddChiTiet(RowTC, "Thi công chi tiết", crCodeTuyen);
                                Fcn_AddChiTiet(RowKHLK, "Lũy kế kế hoạch", crCodeTuyen);
                                Fcn_AddChiTiet(RowTCLK, "Lũy kế thi công", crCodeTuyen);
                                Fcn_AddChiTiet(RowConLai, "Còn lại (Kế hoạch-Thi công)", crCodeTuyen);
                                Fcn_AddChiTiet(RowDongTrong, "", crCodeTuyen);
                                List<KLHN> lstTuyen = new List<KLHN>();
                                List<DateTime> NBDKH = new List<DateTime>();
                                List<DateTime> NKTKH = new List<DateTime>();
                                List<DateTime> NBDTC = new List<DateTime>();
                                List<DateTime> NKTTC = new List<DateTime>();
                                double KLKH = 0, KLTC = 0, LKKH = 0, LKTC = 0, SumConLai = 0, KLCL = 0, KLTong = 0;
                                foreach (var Nhom in grTuyenNhomNew)
                                {
                                    if (ce_ChiVeCongTac.Checked)
                                        goto LabelCongTac;
                                    var fstNhom = Nhom.First();
                                    if (fstNhom["CodeNhom"] == DBNull.Value)
                                    {
                                        goto LabelCongTac;
                                    }
                                    else
                                    {
                                        if (fstNhom["KLKHNhom"] != DBNull.Value)
                                        {
                                            string CodeCTac = fstNhom["CodeNhom"].ToString();
                                            List<KLHN> rowKLHN = rowKLHNAllNhom.Where(x => x.ParentCode == CodeCTac).ToList();
                                            if (!rowKLHN.Any() && !Ce_NgoaiKH.Checked)
                                                continue;
                                            KLTong += double.Parse(fstNhom["KLKHNhom"].ToString());
                                            KLHN LKKHDay = rowKLHN.Where(x => x.Ngay == de_NBD.DateTime.Date).SingleOrDefault();
                                            lstTuyen.Add(new KLHN
                                            {
                                                Ngay = de_NBD.DateTime.Date,
                                                KhoiLuongKeHoach = LKKHDay?.LuyKeKhoiLuongKeHoach,
                                                KhoiLuongThiCong = LKKHDay?.LuyKeKhoiLuongThiCong
                                            });
                                            KLKH = (double)rowKLHN.Sum(x => x.KhoiLuongKeHoach);
                                            DateTime NBD = DateTime.Parse(fstNhom["NgayBatDauNhom"].ToString());
                                            DateTime NKT = DateTime.Parse(fstNhom["NgayKetThucNhom"].ToString());
                                            NBDKH.Add(NBD);
                                            NKTKH.Add(NKT);

                                            List<KLHN> rowKLHNTC = rowKLHN.Where(x => x.KhoiLuongThiCong.HasValue && x.KhoiLuongThiCong > 0).ToList();
                                            if (!rowKLHNTC.Any())
                                                goto LabelNhom;
                                            KLTC += (double)rowKLHNTC.Sum(x => x.KhoiLuongThiCong);
                                            DateTime MIN = (DateTime)rowKLHNTC.Min(x => x.Ngay);
                                            DateTime MAX = (DateTime)rowKLHNTC.Max(x => x.Ngay);
                                            NBDTC.Add(MIN);
                                            NKTTC.Add(MAX);
                                            LabelNhom:
                                            lstTuyen.AddRange(rowKLHN);
                                            lstTuyen.Remove(LKKHDay);
                                            continue;
                                        }
                                        else
                                            goto LabelCongTac;
                                    }
                                    LabelCongTac:
                                    var grCongTac = Nhom.GroupBy(x => x["Code"]);
                                    foreach (var CTac in grCongTac)
                                    {
                                        if (lstCode.Contains(CTac.Key.ToString()))
                                            continue;
                                        string CodeCTac = CTac.Key.ToString();
                                        List<KLHN> rowKLHN = rowKLHNAll.Where(x => x.ParentCode == CodeCTac).ToList();
                                        if (!rowKLHN.Any() && !Ce_NgoaiKH.Checked)
                                            continue;
                                        KLTong += double.Parse(CTac.FirstOrDefault()["KhoiLuongHopDongChiTiet"].ToString());
                                        KLHN LKKHDay = rowKLHN.Where(x => x.Ngay == de_NBD.DateTime.Date).FirstOrDefault();
                                        lstTuyen.Add(new KLHN
                                        {
                                            Ngay = de_NBD.DateTime.Date,
                                            KhoiLuongKeHoach = LKKHDay.LuyKeKhoiLuongKeHoach,
                                            KhoiLuongThiCong = LKKHDay.LuyKeKhoiLuongThiCong
                                        });

                                        KLKH += (double)rowKLHN.Sum(x => x.KhoiLuongKeHoach);
                                        DateTime NBD = DateTime.Parse(CTac.FirstOrDefault()["NgayBatDau"].ToString());
                                        DateTime NKT = DateTime.Parse(CTac.FirstOrDefault()["NgayKetThuc"].ToString());
                                        NBDKH.Add(NBD);
                                        NKTKH.Add(NKT);

                                        List<KLHN> rowKLHNTC = rowKLHN.Where(x => x.KhoiLuongThiCong.HasValue && x.KhoiLuongThiCong > 0).ToList();
                                        if (!rowKLHNTC.Any())
                                            goto Label;
                                        KLTC += (double)rowKLHNTC.Sum(x => x.KhoiLuongThiCong);
                                        DateTime MIN = (DateTime)rowKLHNTC.Min(x => x.Ngay);
                                        DateTime MAX = (DateTime)rowKLHNTC.Max(x => x.Ngay);
                                        NBDTC.Add(MIN);
                                        NKTTC.Add(MAX);
                                        Label:
                                        lstTuyen.AddRange(rowKLHN);
                                        lstTuyen.Remove(LKKHDay);
                                    }
                                }
                                if (!NBDKH.Any() && !NKTKH.Any())
                                    continue;
                                DateTime NBDTuyen = NBDKH.Min();
                                DateTime NKTTuyen = NKTKH.Max();
                                double NKH = (NKTTuyen - NBDTuyen).TotalDays + 1;
                                RowKH["TongKhoiLuong"] = Math.Round(KLKH / NKH, 2);
                                RowKH["SumNgay"] = NKH;
                                RowKH["NgayBatDau"] = NBDTuyen.ToString(MyConstant.CONST_DATE_FORMAT_SPSHEET);
                                RowKH["NgayKetThuc"] = NKTTuyen.ToString(MyConstant.CONST_DATE_FORMAT_SPSHEET);
                                if (NBDTC.Any() && NKTTC.Any())
                                {
                                    DateTime NBDTCTuyen = NBDTC.Min();
                                    DateTime NKTTCTuyen = NKTTC.Max();
                                    double NTC = (DateTime.Now.Date - NBDTCTuyen.Date).TotalDays + 1;
                                    double NTCSum = (NKTTCTuyen.Date - NBDTCTuyen.Date).TotalDays + 1;
                                    RowTC["SumNgay"] = NTCSum;
                                    RowTC["NgayBatDau"] = NBDTCTuyen.ToString(MyConstant.CONST_DATE_FORMAT_SPSHEET);
                                    RowTC["NgayKetThuc"] = NKTTCTuyen.ToString(MyConstant.CONST_DATE_FORMAT_SPSHEET);
                                    if (NTC > 0)
                                        RowTC["TongKhoiLuong"] = Math.Round(KLTC / NTC, 2);
                                }
                                //lstDateCr = lstTuyen.Select(x => x.Ngay.Date).Distinct().ToList();
                                //foreach (var itemdate in lstDate)
                                //{
                                //    if (lstDateCr.Contains(itemdate.Ngay))
                                //        continue;
                                //    lstTuyen.Add(itemdate);
                                //}
                                var grNgay = lstTuyen.GroupBy(x => x.Ngay.Date);
                                foreach (var item in grNgay.OrderBy(x => x.Key))
                                {
                                    string colum = item.Key.ToString(MyConstant.CONST_DATE_FORMAT_SPSHEET);
                                    //if (!dt.Columns.Contains(colum))
                                    //    goto Label1;
                                    double KLKHDay = item.Sum(x => x.KhoiLuongKeHoach) ?? 0;
                                    LKKH += KLKHDay;
                                    RowKH[colum] = KLKHDay;
                                    RowKHLK[colum] = Math.Round(LKKH, 4);

                                    if (item.Key.Date <= de_NKT.DateTime.Date)
                                    {
                                        double KLTCDay = item.Sum(x => x.KhoiLuongThiCong) ?? 0;
                                        if (KLTCDay > 0)
                                        {
                                            RowTC[colum] = KLTCDay;
                                            LKTC += KLTCDay;
                                        }
                                        KLCL = LKKH - LKTC;
                                        SumConLai = KLCL;
                                        RowTCLK[colum] = Math.Round(LKTC, 4);
                                        RowConLai[colum] = Math.Round(KLCL, 4);
                                    }
                                }
                                RowTCLK["TongKhoiLuong"] = Math.Round(LKTC, 4);
                                RowKHLK["TongKhoiLuong"] = Math.Round(LKKH, 4);
                                RowConLai["TongKhoiLuong"] = Math.Round(SumConLai, 4);
                                RowCongTac["TongKhoiLuong"] = KLTong;
                                Label1:
                                dt.Rows.Add(RowCongTac);
                                dt.Rows.Add(RowKH);
                                dt.Rows.Add(RowTC);
                                dt.Rows.Add(RowKHLK);
                                dt.Rows.Add(RowTCLK);
                                dt.Rows.Add(RowConLai);
                                dt.Rows.Add(RowDongTrong);
                                continue;
                            }
                            var grTuyenNhom = Tuyen.GroupBy(x => (int)x["IndNhom"])
                                                .OrderBy(x => x.Key);
                            foreach (var Nhom in grTuyenNhom)
                            {
                                if (ce_ChiVeCongTac.Checked)
                                    goto LabelCongTac;
                                var fstNhom = Nhom.First();
                                if (fstNhom["CodeNhom"] == DBNull.Value)
                                {
                                    goto LabelCongTac;
                                }
                                else
                                {
                                    if (fstNhom["KLKHNhom"] != DBNull.Value)
                                    {
                                        string CodeCTac = fstNhom["CodeNhom"].ToString();
                                        string CodeCongTac = $"{Guid.NewGuid()}_Nhom";
                                        List<KLHN> rowKLHN = rowKLHNAllNhom.Where(x => x.ParentCode == CodeCTac).ToList();
                                        if (!rowKLHN.Any() && !Ce_NgoaiKH.Checked)
                                            continue;
                                        var LKKHDay = rowKLHN.Where(x => x.Ngay == de_NBD.DateTime.Date).FirstOrDefault();
                                        double LKKH = (LKKHDay?.LuyKeKhoiLuongKeHoach - LKKHDay?.KhoiLuongKeHoach) ?? 0;
                                        double LKTC = (LKKHDay?.LuyKeKhoiLuongThiCong - LKKHDay?.KhoiLuongThiCong) ?? 0;
                                        double KLKH = (double)rowKLHN.Sum(x => x.KhoiLuongKeHoach);
                                        //WaitFormHelper.ShowWaitForm($"{ fstNhom["TenNhom"]}", "Đang tổng hợp dữ liệu");
                                        DataRow RowCongTac = dt.NewRow();
                                        RowCongTac["ID"] = CodeCongTac;
                                        RowCongTac["ParentID"] = CodeHM;
                                        RowCongTac["TenCongTac"] = fstNhom["TenNhom"];
                                        RowCongTac["TongKhoiLuong"] = fstNhom["KLKHNhom"];
                                        dt.Rows.Add(RowCongTac);

                                        DataRow RowKH = dt.NewRow();
                                        DataRow RowTC = dt.NewRow();
                                        DataRow RowKHLK = dt.NewRow();
                                        DataRow RowTCLK = dt.NewRow();
                                        DataRow RowConLai = dt.NewRow();
                                        DataRow RowDongTrong = dt.NewRow();

                                        Fcn_AddChiTiet(RowKH, "Kế hoạch chi tiết", CodeCongTac);
                                        Fcn_AddChiTiet(RowTC, "Thi công chi tiết", CodeCongTac);
                                        Fcn_AddChiTiet(RowKHLK, "Lũy kế kế hoạch", CodeCongTac);
                                        Fcn_AddChiTiet(RowTCLK, "Lũy kế thi công", CodeCongTac);
                                        Fcn_AddChiTiet(RowConLai, "Còn lại (Kế hoạch-Thi công)", CodeCongTac);
                                        Fcn_AddChiTiet(RowDongTrong, "", CodeCongTac);
                                        DateTime NBD = DateTime.Parse(fstNhom["NgayBatDauNhom"].ToString());
                                        DateTime NKT = DateTime.Parse(fstNhom["NgayKetThucNhom"].ToString());
                                        double NKH = (NKT - NBD).TotalDays + 1;
                                        RowKH["TongKhoiLuong"] = Math.Round(KLKH / NKH, 2);
                                        RowKH["SumNgay"] = NKH;
                                        RowKH["NgayBatDau"] = NBD.ToString(MyConstant.CONST_DATE_FORMAT_SPSHEET);
                                        RowKH["NgayKetThuc"] = NKT.ToString(MyConstant.CONST_DATE_FORMAT_SPSHEET);

                                        List<KLHN> rowKLHNTC = rowKLHN.Where(x => x.KhoiLuongThiCong.HasValue && x.KhoiLuongThiCong > 0).ToList();
                                        if (!rowKLHNTC.Any())
                                            goto LabelNhom;
                                        double KLTC = (double)rowKLHNTC.Sum(x => x.KhoiLuongThiCong);
                                        DateTime MIN = (DateTime)rowKLHNTC.Min(x => x.Ngay);
                                        DateTime MAX = (DateTime)rowKLHNTC.Max(x => x.Ngay);
                                        double NTC = (DateTime.Now.Date - MIN.Date).TotalDays + 1;
                                        double NTCSum = (MAX.Date - MIN.Date).TotalDays + 1;
                                        RowTC["SumNgay"] = NTCSum;
                                        RowTC["NgayBatDau"] = MIN.ToString(MyConstant.CONST_DATE_FORMAT_SPSHEET);
                                        RowTC["NgayKetThuc"] = MAX.ToString(MyConstant.CONST_DATE_FORMAT_SPSHEET);
                                        if (NTC > 0)
                                            RowTC["TongKhoiLuong"] = Math.Round(KLTC / NTC, 2);
                                        LabelNhom:
                                        double SumConLai = 0, KLCL;
                                        foreach (KLHN item in rowKLHN.OrderBy(x => x.Ngay))
                                        {
                                            string colum = item.Ngay.ToString(MyConstant.CONST_DATE_FORMAT_SPSHEET);
                                            //if (!dt.Columns.Contains(colum))
                                            //    goto Label1;
                                            double KLKHDay = /*item.Ngay.Date == de_NBD.DateTime.Date ?(double)item.LuyKeKhoiLuongKeHoach:*/
                                                item.KhoiLuongKeHoach ?? 0;
                                            LKKH += KLKHDay;


                                            RowKH[colum] = KLKHDay;
                                            RowKHLK[colum] = Math.Round(LKKH, 4);

                                            if (item.Ngay.Date <= de_NKT.DateTime.Date)
                                            {
                                                
                                                double KLTCDay = /*item.Ngay.Date == de_NBD.DateTime.Date ? */
                                                //    item.LuyKeKhoiLuongThiCong??0:
                                                    item.KhoiLuongThiCong ?? 0;

                                                if (KLTCDay > 0)
                                                {
                                                    RowTC[colum] = KLTCDay;
                                                    LKTC += KLTCDay;
                                                }
                                                KLCL = LKKH - LKTC;
                                                SumConLai = KLCL;
                                                RowTCLK[colum] = Math.Round(LKTC, 4);
                                                RowConLai[colum] = Math.Round(KLCL, 4);
                                            }
                                        }
                                        RowTCLK["TongKhoiLuong"] = Math.Round(LKTC, 4);
                                        RowKHLK["TongKhoiLuong"] = Math.Round(LKKH, 4);
                                        RowConLai["TongKhoiLuong"] = Math.Round(SumConLai, 4);
                                        Label1:
                                        dt.Rows.Add(RowKH);
                                        dt.Rows.Add(RowTC);
                                        dt.Rows.Add(RowKHLK);
                                        dt.Rows.Add(RowTCLK);
                                        dt.Rows.Add(RowConLai);
                                        dt.Rows.Add(RowDongTrong);
                                        continue;
                                    }
                                    else
                                        goto LabelCongTac;
                                }
                                LabelCongTac:
                                var grCongTac = Nhom.GroupBy(x => x["Code"]);
                                foreach (var CTac in grCongTac)
                                {
                                    if (lstCode.Contains(CTac.Key.ToString()))
                                        continue;
                                    string CodeCTac = CTac.Key.ToString();
                                    string CodeCongTac = Guid.NewGuid().ToString();
                                    List<KLHN> rowKLHN = rowKLHNAll.Where(x => x.ParentCode == CodeCTac).ToList();
                                    if (!rowKLHN.Any() && !Ce_NgoaiKH.Checked)
                                        continue;
                                    //KLHNBriefViewModel LKKHDay = LKKHDayAll.Where(x => x.Code == CodeCTac).FirstOrDefault();
                                    var LKKHDay = rowKLHN.Where(x => x.Ngay == de_NBD.DateTime.Date).FirstOrDefault();
                                    double LKKH = (LKKHDay?.LuyKeKhoiLuongKeHoach - LKKHDay?.KhoiLuongKeHoach) ?? 0;
                                    double LKTC = (LKKHDay?.LuyKeKhoiLuongThiCong - LKKHDay?.KhoiLuongThiCong) ?? 0;
                                    double KLKH = (double)rowKLHN.Sum(x => x.KhoiLuongKeHoach);
                                    //WaitFormHelper.ShowWaitForm($"{CTac.FirstOrDefault()["TenCongTac"]}", "Đang tổng hợp dữ liệu");
                                    DataRow RowCongTac = dt.NewRow();
                                    RowCongTac["ID"] = CodeCongTac;
                                    RowCongTac["ParentID"] = CodeHM;
                                    RowCongTac["TenCongTac"] = CTac.FirstOrDefault()["TenCongTac"].ToString();
                                    RowCongTac["TongKhoiLuong"] = CTac.FirstOrDefault()["KhoiLuongHopDongChiTiet"];
                                    dt.Rows.Add(RowCongTac);


                                    DataRow RowKH = dt.NewRow();
                                    DataRow RowTC = dt.NewRow();
                                    DataRow RowKHLK = dt.NewRow();
                                    DataRow RowTCLK = dt.NewRow();
                                    DataRow RowConLai = dt.NewRow();
                                    DataRow RowDongTrong = dt.NewRow();

                                    Fcn_AddChiTiet(RowKH, "Kế hoạch chi tiết", CodeCongTac);
                                    Fcn_AddChiTiet(RowTC, "Thi công chi tiết", CodeCongTac);
                                    Fcn_AddChiTiet(RowKHLK, "Lũy kế kế hoạch", CodeCongTac);
                                    Fcn_AddChiTiet(RowTCLK, "Lũy kế thi công", CodeCongTac);
                                    Fcn_AddChiTiet(RowConLai, "Còn lại (Kế hoạch-Thi công)", CodeCongTac);
                                    Fcn_AddChiTiet(RowDongTrong, "", CodeCongTac);
                                    DateTime NBD = DateTime.Parse(CTac.FirstOrDefault()["NgayBatDau"].ToString());
                                    DateTime NKT = DateTime.Parse(CTac.FirstOrDefault()["NgayKetThuc"].ToString());
                                    double NKH = (NKT - NBD).TotalDays + 1;


                                    RowKH["TongKhoiLuong"] = Math.Round(KLKH / NKH, 2);
                                    RowKH["SumNgay"] = NKH;
                                    RowKH["NgayBatDau"] = NBD.ToString(MyConstant.CONST_DATE_FORMAT_SPSHEET);
                                    RowKH["NgayKetThuc"] = NKT.ToString(MyConstant.CONST_DATE_FORMAT_SPSHEET);

                                    List<KLHN> rowKLHNTC = rowKLHN.Where(x => x.KhoiLuongThiCong.HasValue && x.KhoiLuongThiCong > 0).ToList();
                                    if (!rowKLHNTC.Any())
                                        goto Label;
                                    double KLTC = (double)rowKLHNTC.Sum(x => x.KhoiLuongThiCong);
                                    DateTime MIN = (DateTime)rowKLHNTC.Min(x => x.Ngay);
                                    DateTime MAX = (DateTime)rowKLHNTC.Max(x => x.Ngay);
                                    double NTC = (DateTime.Now.Date - MIN.Date).TotalDays + 1;
                                    double NTCSum = (MAX.Date - MIN.Date).TotalDays + 1;
                                    RowTC["SumNgay"] = NTCSum;
                                    RowTC["NgayBatDau"] = MIN.ToString(MyConstant.CONST_DATE_FORMAT_SPSHEET);
                                    RowTC["NgayKetThuc"] = MAX.ToString(MyConstant.CONST_DATE_FORMAT_SPSHEET);
                                    if (NTC > 0)
                                        RowTC["TongKhoiLuong"] = Math.Round(KLTC / NTC, 2);
                                    Label:
                                    double SumConLai = 0, KLCL = 0;
                                    //lstDateCr = rowKLHN.Select(x => x.Ngay.Date).Distinct().ToList();
                                    //foreach (var itemdate in lstDate)
                                    //{
                                    //    if (lstDateCr.Contains(itemdate.Ngay))
                                    //        continue;
                                    //    rowKLHN.Add(itemdate);
                                    //}
                                    foreach (KLHN item in rowKLHN.OrderBy(x => x.Ngay))
                                    {
                                        string colum = item.Ngay.ToString(MyConstant.CONST_DATE_FORMAT_SPSHEET);
                                        //if (!dt.Columns.Contains(colum))
                                        //    goto Label1;

                                        double KLKHDay =item.KhoiLuongKeHoach ?? 0;
                                        LKKH += KLKHDay;
                                        RowKH[colum] = KLKHDay;
                                        RowKHLK[colum] = Math.Round(LKKH, 4);

                                        if (item.Ngay.Date <= de_NKT.DateTime.Date)
                                        {
                                            double KLTCDay =item.KhoiLuongThiCong ?? 0;
                                            if (KLTCDay > 0)
                                            {
                                                LKTC += KLTCDay;
                                                RowTC[colum] = KLTCDay;
                                            }
                                            KLCL = LKKH - LKTC;
                                            SumConLai = KLCL;
                                            RowTCLK[colum] = Math.Round(LKTC, 4);
                                            RowConLai[colum] = Math.Round(KLCL, 4);
                                        }
                                    }
                                    RowTCLK["TongKhoiLuong"] = Math.Round(LKTC, 4);
                                    RowKHLK["TongKhoiLuong"] = Math.Round(LKKH, 4);
                                    RowConLai["TongKhoiLuong"] = Math.Round(SumConLai, 4);
                                    Label1:
                                    dt.Rows.Add(RowKH);
                                    dt.Rows.Add(RowTC);
                                    dt.Rows.Add(RowKHLK);
                                    dt.Rows.Add(RowTCLK);
                                    dt.Rows.Add(RowConLai);
                                    dt.Rows.Add(RowDongTrong);
                                }
                            }

                        }
                    }


                }

            }
            tl_KiemSoat.RefreshDataSource();
            tl_KiemSoat.Refresh();
            tl_KiemSoat.DataSource = null;

            tl_KiemSoat.DataSource = dt;
            if (IsBrief)
            {
                col_tenCongViec.FilterInfo = new TreeListColumnFilterInfo($"[TenCongTac] = 'Còn lại (Kế hoạch-Thi công)' OR [TenCongTac] = 'Lũy kế kế hoạch' OR [TenCongTac] = 'Lũy kế thi công' OR [TenCongTac] = ''");
            }


            tl_KiemSoat.RefreshDataSource();
            tl_KiemSoat.Refresh();
            tl_KiemSoat.ExpandAll();
            //tl_KiemSoat.EndUpdate();
            tl_KiemSoat.FocusedColumn = tl_KiemSoat.Columns[DateTime.Now.ToString(MyConstant.CONST_DATE_FORMAT_SPSHEET)];
            var colNow = tl_KiemSoat.Columns[DateTime.Now.ToString(MyConstant.CONST_DATE_FORMAT_SPSHEET)];
            if (colNow != null)
                colNow.AppearanceHeader.BackColor = Color.Yellow;
            WaitFormHelper.CloseWaitForm();
        }
        public void Fcn_UpdateThongTin(DataTable dtcongtactheochuky)
        {
            if (dtcongtactheochuky.Rows.Count == 0)
            {
                tl_ThongTin.DataSource = null;
                tl_KiemSoat.DataSource = null;
                return;
            }
            WaitFormHelper.ShowWaitForm("Đang phân tích dữ liệu");
            Dic_Col = new Dictionary<string, string>()
            {
                {"CodeNhaThau",MyConstant.TBL_THONGTINNHATHAU},
                {"CodeNhaThauPhu",MyConstant.TBL_THONGTINNHATHAUPHU},
                {"CodeToDoi",MyConstant.TBL_THONGTINTODOITHICONG},
            };
            List<string> codeNhoms = null;
            if (Ce_Full.Checked)
            {
                codeNhoms=dtcongtactheochuky.AsEnumerable().Where(x => x["CodeNhom"] != DBNull.Value)
.Select(x => x["CodeNhom"].ToString()).Distinct().ToList();
                DateTime Min_KH = dtcongtactheochuky.AsEnumerable().Where(x => x["NgayBatDau"] != DBNull.Value).Min(x => DateTime.Parse(x["NgayBatDau"].ToString()));
                DateTime Max_KH = dtcongtactheochuky.AsEnumerable().Where(x => x["NgayKetThuc"] != DBNull.Value).Max(x => DateTime.Parse(x["NgayKetThuc"].ToString()));
                DateTime Min_KHNhom = dtcongtactheochuky.AsEnumerable().Where(x => x["NgayBatDauNhom"] != DBNull.Value).Any() ?
                    dtcongtactheochuky.AsEnumerable().Where(x => x["NgayBatDauNhom"] != DBNull.Value).Min(x => DateTime.Parse(x["NgayBatDauNhom"].ToString())) : Min_KH;
                DateTime Max_KHNhom = dtcongtactheochuky.AsEnumerable().Where(x => x["NgayKetThucNhom"] != DBNull.Value).Any() ?
                    dtcongtactheochuky.AsEnumerable().Where(x => x["NgayKetThucNhom"] != DBNull.Value).Max(x => DateTime.Parse(x["NgayKetThucNhom"].ToString())) : Max_KH;
                DateTime Max = Max_KH > Max_KHNhom ? Max_KH : Max_KHNhom;
                DateTime Min = Min_KH < Min_KHNhom ? Min_KH : Min_KHNhom;
                string dbString = $"SELECT {TDKH.TBL_ChiTietCongTacTheoKy}.*,MIN(Ngay) AS MinNgayThiCong, MAX(Ngay) as MaxNgayThiCong  " +
      $"FROM {TDKH.TBL_ChiTietCongTacTheoKy} " +
      $"LEFT JOIN {TDKH.TBL_KhoiLuongCongViecHangNgay} " +
      $"ON {TDKH.TBL_ChiTietCongTacTheoKy}.Code = {TDKH.TBL_KhoiLuongCongViecHangNgay}.CodeCongTacTheoGiaiDoan " +
            $"WHERE {TDKH.TBL_ChiTietCongTacTheoKy}.CodeGiaiDoan = '{SharedControls.cbb_DBKH_ChonDot.SelectedValue}' ";
                DataTable dttc = DataProvider.InstanceTHDA.ExecuteQuery(dbString);            
                if (dttc.Rows.Count != 0)
                {
                    DateTime Max_TC = dttc.Rows[0]["MaxNgayThiCong"] != DBNull.Value ? DateTime.Parse(dttc.Rows[0]["MaxNgayThiCong"].ToString()) : default;
                    Max = Max_KH >= Max_TC ? Max_KH : Max_TC;
                    DateTime Min_TC = dttc.Rows[0]["MinNgayThiCong"] != DBNull.Value ? DateTime.Parse(dttc.Rows[0]["MinNgayThiCong"].ToString()) : default;
                    Min = Min_KH >= Min_TC && Min_TC != default ? Min_TC : Min_KH;
                }
                dbString = $"SELECT {TDKH.TBL_KhoiLuongCongViecHangNgay}.*,MIN(Ngay) AS MinNgayThiCong, MAX(Ngay) as MaxNgayThiCong  " +
        $"FROM {TDKH.TBL_KhoiLuongCongViecHangNgay} " +
        $"WHERE {TDKH.TBL_KhoiLuongCongViecHangNgay}.CodeNhom IN ({MyFunction.fcn_Array2listQueryCondition(codeNhoms.ToArray())})";
                dttc = DataProvider.InstanceTHDA.ExecuteQuery(dbString);
                if (dttc.Rows.Count != 0)
                {
                    DateTime Max_TC = dttc.Rows[0]["MaxNgayThiCong"] != DBNull.Value ? DateTime.Parse(dttc.Rows[0]["MaxNgayThiCong"].ToString()) : default;
                    Max = Max_KH >= Max_TC ? Max_KH : Max_TC;
                    DateTime Min_TC = dttc.Rows[0]["MinNgayThiCong"] != DBNull.Value ? DateTime.Parse(dttc.Rows[0]["MinNgayThiCong"].ToString()) : default;
                    Min = Min_KH >= Min_TC && Min_TC != default ? Min_TC : Min_KH;
                }
                //Max = Ce_Full.Checked ? Max_KH : de_NKT.DateTime;
                //Min = Ce_Full.Checked ? Min_KH : de_NBD.DateTime;
                de_NKT.DateTime = Max;
                de_NBD.DateTime = Min;
            }
            DataTable dt = new DataTable();

            dt.Columns.Add("TenCongTac", typeof(string));
            dt.Columns.Add("ID", typeof(string));
            dt.Columns.Add("ParentID", typeof(string));
            dt.Columns.Add("TenColum", typeof(string));
            dt.Columns.Add("TongKhoiLuong", typeof(double));
            dt.Columns.Add("NgayBatDau", typeof(string));
            dt.Columns.Add("NgayKetThuc", typeof(string));
            dt.Columns.Add("SumNgay", typeof(int));

            dt.Columns.Add("TenDuAn", typeof(string));
            dt.Columns.Add("TenCongTacInBrief", typeof(string));
            dt.Columns.Add("TenDVTH", typeof(string));
            dt.Columns.Add("IsGetToSlke", typeof(bool));
            tl_ThongTin.BeginUpdate();
            List<TreeListColumn> lst = new List<TreeListColumn>();
            foreach (var item in tl_ThongTin.Columns)
            {
                if (DateTime.TryParse(item.FieldName, out DateTime Rs))
                    lst.Add(item);
            }
            lst.ForEach(x => tl_ThongTin.Columns.Remove(x));
            //List<KLHN> lstDate = new List<KLHN>();
            //List<DateTime> lstDateCr = new List<DateTime>();
            for (DateTime i = de_NBD.DateTime.Date; i <= de_NKT.DateTime.Date; i = i.AddDays(1))
            {
                //lstDate.Add(new KLHN
                //{
                //    Ngay = i
                //});
                dt.Columns.Add(i.ToString(MyConstant.CONST_DATE_FORMAT_SPSHEET), typeof(double));
                TreeListColumn col = tl_ThongTin.Columns.Add();
                col.Caption = col.FieldName = i.ToString(MyConstant.CONST_DATE_FORMAT_SPSHEET);
                col.Visible = true;

                if (i.Date != DateTime.Now.Date)
                    col.Visible = false;
            }
            tl_ThongTin.EndUpdate();
            //if (!Ce_Full.Checked && !Ce_NgoaiKH.Checked)
            //{
            //    DataRow[] Row = dtcongtactheochuky.AsEnumerable().
            //        Where(x=>DateTime.Parse(x["NgayKetThuc"].ToString()).Date < de_NBD.DateTime.Date).ToArray();
            //    if (Row.Any())
            //        dtcongtactheochuky = Row.CopyToDataTable();
            //    else
            //    {
            //        tl_KiemSoat.DataSource = null;
            //        return;
            //    }
            //}
            codeNhoms = dtcongtactheochuky.AsEnumerable().Where(x => x["CodeNhom"] != DBNull.Value)
.Select(x => x["CodeNhom"].ToString()).Distinct().ToList();
            string[] lstCode = dtcongtactheochuky.AsEnumerable().Select(x => x["CodeCha"].ToString()).Distinct().ToArray();
            string[] lstCodeCT = dtcongtactheochuky.AsEnumerable().Where(x => !lstCode.Contains(x["Code"].ToString())).Select(x => x["Code"].ToString()).Distinct().ToArray();
            List<KLHN> rowKLHNAll = MyFunction.Fcn_CalKLKHNew(TypeKLHN.CongTac, lstCodeCT, dateBD: de_NBD.DateTime.Date, dateKT: de_NKT.DateTime.Date);
            List<KLHN> rowKLHNAllNhom = MyFunction.Fcn_CalKLKHNew(TypeKLHN.Nhom, codeNhoms, dateBD: de_NBD.DateTime.Date, dateKT: de_NKT.DateTime.Date);
            
            var grDuAn = dtcongtactheochuky.AsEnumerable().GroupBy(x => x["CodeDuAn"]);
            _CheckFocus = false;           
            foreach (var DA in grDuAn)
            {
                DataRow RowDuAn = dt.NewRow();
                RowDuAn["ID"] = DA.Key;
                RowDuAn["ParentID"] = "0";
                RowDuAn["TenCongTac"] = DA.FirstOrDefault()["TenDuAn"].ToString();
                dt.Rows.Add(RowDuAn);
                var grNhaThau = DA.GroupBy(x => x["CodeNhaThauChinh"]);
                foreach (var NT in grNhaThau)
                {
                    string ColColum = NT.FirstOrDefault()["CodeNhaThau"] != DBNull.Value ? "CodeNhaThau" :
                        (NT.FirstOrDefault()["CodeToDoi"] != DBNull.Value ? "CodeToDoi" : "CodeNhaThauPhu");
                    DataRow RowThauChinh = dt.NewRow();
                    RowThauChinh["ID"] = NT.Key;
                    RowThauChinh["ParentID"] = DA.Key;
                    RowThauChinh["TenColum"] = ColColum;
                    RowThauChinh["TenCongTac"] = RowThauChinh["TenDVTH"] = NT.FirstOrDefault()["TenNhaThau"];
                    RowThauChinh["TenDuAn"] = DA.FirstOrDefault()["TenDuAn"];
                    RowThauChinh["TenCongTacInBrief"] = $"{NT.FirstOrDefault()["TenNhaThau"]} ({DA.FirstOrDefault()["TenDuAn"]})";
                    RowThauChinh["IsGetToSlke"] = true;

                    dt.Rows.Add(RowThauChinh);
                    string CodeLKTC = Guid.NewGuid().ToString();
                    DataRow RowKHTong = dt.NewRow();
                    DataRow RowTCTong = dt.NewRow();
                    DataRow RowTrong = dt.NewRow();
                    Fcn_AddChiTiet(RowKHTong, "Lũy kế kế hoạch", NT.Key.ToString());
                    Fcn_AddChiTiet(RowTrong, "", NT.Key.ToString());
                    RowTCTong["ID"] = CodeLKTC;
                    RowTCTong["ParentID"] = NT.Key;
                    RowTCTong["TenCongTac"] = "Lũy kế thi công";
                    dt.Rows.Add(RowKHTong);
                    dt.Rows.Add(RowTCTong);
                    dt.Rows.Add(RowTrong);
                    var grCongTrinh = NT.GroupBy(x => x["CodeCongTrinh"]);
                    foreach (var Ctrinh in grCongTrinh)
                    {
                        string CodeCT = Guid.NewGuid().ToString();
                        DataRow RowCT = dt.NewRow();
                        RowCT["ID"] = CodeCT;
                        RowCT["ParentID"] = CodeLKTC;
                        RowCT["TenCongTac"] = Ctrinh.FirstOrDefault()["TenCongTrinh"];
                        dt.Rows.Add(RowCT);

                        var grHangMuc = Ctrinh.GroupBy(x => x["CodeHangMuc"]);
                        foreach (var HM in grHangMuc)
                        {
                            string CodeHM = Guid.NewGuid().ToString();
                            DataRow RowHM = dt.NewRow();
                            RowHM["ID"] = CodeHM;
                            RowHM["ParentID"] = CodeCT;
                            RowHM["TenCongTac"] = HM.FirstOrDefault()["TenHangMuc"].ToString();
                            dt.Rows.Add(RowHM);
                            var grNhom = HM.GroupBy(x => x["CodeNhom"]);
                            foreach (var Nhom in grNhom)
                            {
                                if (Nhom.Key is null)
                                {
                                    goto LabelCongTac;
                                }
                                else
                                {
                                    var fstNhom = Nhom.FirstOrDefault();
                                    if (fstNhom["KLKHNhom"] != DBNull.Value)
                                    {
                                        string CodeCTac = Nhom.Key.ToString();
                                        string CodeCongTac = $"{Guid.NewGuid()}_Nhom";
                                        List<KLHN> rowKLHN = rowKLHNAllNhom.Where(x => x.ParentCode == CodeCTac).ToList();
                                        if (!rowKLHN.Any())
                                            continue;
                                        var LKKHDay = rowKLHN.Where(x => x.Ngay == de_NBD.DateTime.Date).FirstOrDefault();
                                        double LKKH = (LKKHDay?.LuyKeKhoiLuongKeHoach - LKKHDay?.KhoiLuongKeHoach) ?? 0;
                                        double LKTC = (LKKHDay?.LuyKeKhoiLuongThiCong - LKKHDay?.KhoiLuongThiCong) ?? 0;
                                        //rowKLHN.Add(new KLHN
                                        //{
                                        //    Ngay = LKKHDay.Ngay.Value,
                                        //    KhoiLuongKeHoach = LKKHDay.KLKHFromBeginOfPrj,
                                        //    KhoiLuongThiCong = LKKHDay.KLTCFromBeginOfPrj
                                        //});
                                        List<KLHN> rowKLHNTC = rowKLHN.Where(x => x.KhoiLuongThiCong.HasValue && x.KhoiLuongThiCong > 0).ToList();
                                        double KLKH = (double)rowKLHN.Sum(x => x.KhoiLuongKeHoach);
                                        //WaitFormHelper.ShowWaitForm($"{fstNhom["TenNhom"]}", "Đang tổng hợp dữ liệu");
                                        DataRow RowCongTac = dt.NewRow();
                                        RowCongTac["ID"] = CodeCongTac;
                                        RowCongTac["ParentID"] = CodeHM;
                                        RowCongTac["TenCongTac"] = fstNhom["TenNhom"].ToString();
                                        dt.Rows.Add(RowCongTac);


                                        DataRow RowKH = dt.NewRow();
                                        DataRow RowTC = dt.NewRow();
                                        DataRow RowKHLK = dt.NewRow();
                                        DataRow RowTCLK = dt.NewRow();
                                        DataRow RowConLai = dt.NewRow();
                                        DataRow RowDongTrong = dt.NewRow();

                                        Fcn_AddChiTiet(RowKH, "Kế hoạch chi tiết", CodeCongTac);
                                        Fcn_AddChiTiet(RowTC, "Thi công chi tiết", CodeCongTac);
                                        Fcn_AddChiTiet(RowKHLK, "Lũy kế kế hoạch", CodeCongTac);
                                        Fcn_AddChiTiet(RowTCLK, "Lũy kế thi công", CodeCongTac);
                                        Fcn_AddChiTiet(RowConLai, "Còn lại (Kế hoạch-Thi công)", CodeCongTac);
                                        Fcn_AddChiTiet(RowDongTrong, "", CodeCongTac);
                                        DateTime NBD = DateTime.Parse(fstNhom["NgayBatDauNhom"].ToString());
                                        DateTime NKT = DateTime.Parse(fstNhom["NgayKetThucNhom"].ToString());
                                        double NKH = (NKT - NBD).TotalDays + 1;


                                        RowKH["TongKhoiLuong"] = Math.Round(KLKH / NKH, 2);
                                        RowKH["SumNgay"] = NKH;
                                        RowKH["NgayBatDau"] = NBD.ToString(MyConstant.CONST_DATE_FORMAT_SPSHEET);
                                        RowKH["NgayKetThuc"] = NKT.ToString(MyConstant.CONST_DATE_FORMAT_SPSHEET);

                                        if (rowKLHNTC.Count() == 0)
                                            goto LabelNhom;
                                        double KLTC = (double)rowKLHNTC.Sum(x => x.KhoiLuongThiCong);
                                        DateTime MIN = (DateTime)rowKLHNTC.Min(x => x.Ngay);
                                        DateTime MAX = (DateTime)rowKLHNTC.Max(x => x.Ngay);
                                        double NTC = (DateTime.Now.Date - MIN.Date).TotalDays + 1;
                                        double NTCSum = (MAX.Date - MIN.Date).TotalDays + 1;
                                        RowTC["SumNgay"] = NTCSum;
                                        RowTC["NgayBatDau"] = MIN.ToString(MyConstant.CONST_DATE_FORMAT_SPSHEET);
                                        RowTC["NgayKetThuc"] = MAX.ToString(MyConstant.CONST_DATE_FORMAT_SPSHEET);
                                        if (NTC > 0)
                                            RowTC["TongKhoiLuong"] = Math.Round(KLTC / NTC, 2);
                                        LabelNhom:
                                        double SumConLai = 0, KLCL = 0;
                                        //lstDateCr = rowKLHN.Select(x => x.Ngay).ToList();
                                        //foreach (var itemdate in lstDate)
                                        //{
                                        //    if (lstDateCr.Contains(itemdate.Ngay))
                                        //        continue;
                                        //    rowKLHN.Add(itemdate);
                                        //}
                                        foreach (KLHN item in rowKLHN.OrderBy(x => x.Ngay))
                                        {
                                            //if (!item.KhoiLuongThiCong.HasValue && !item.KhoiLuongKeHoach.HasValue || item.KhoiLuongKeHoach == 0 && item.KhoiLuongThiCong == 0)
                                            //    continue;
                                            string colum = item.Ngay.ToString(MyConstant.CONST_DATE_FORMAT_SPSHEET);
                                            //if (!dt.Columns.Contains(colum))
                                            //    goto Label2;
                                            double KLKHDay = item.KhoiLuongKeHoach ?? 0;
                                            LKKH += KLKHDay;
                                            RowKH[colum] = KLKHDay;
                                            RowKHLK[colum] = Math.Round(LKKH, 4);

                                            if (item.Ngay.Date <= de_NKT.DateTime.Date)
                                            {
                                                double KLTCDay = item.KhoiLuongThiCong ?? 0;

                                                if (KLTCDay > 0)
                                                {
                                                    RowTC[colum] = KLTCDay;
                                                    LKTC += KLTCDay;
                                                }
                                                //SumLKKH += item.KhoiLuongKeHoach.HasValue ? (double)item.KhoiLuongKeHoach : 0;
                                                //LKTC += item.KhoiLuongThiCong.HasValue ? (double)item.KhoiLuongThiCong : 0;
                                                //SumLKTC += LKTC;
                                                KLCL = LKKH - LKTC;
                                                SumConLai = KLCL;
                                                RowTCLK[colum] = Math.Round(LKTC, 4);
                                                RowConLai[colum] = Math.Round(KLCL, 4);
                                            }
                                        }
                                        //RowTCLK["TongKhoiLuong"] = Math.Round(SumLKTC, 4);
                                        //RowKHLK["TongKhoiLuong"] = Math.Round(SumLKKH, 4);
                                        //RowConLai["TongKhoiLuong"] = Math.Round(SumConLai, 4);
                                        Label2:
                                        dt.Rows.Add(RowKH);
                                        dt.Rows.Add(RowTC);
                                        dt.Rows.Add(RowKHLK);
                                        dt.Rows.Add(RowTCLK);
                                        dt.Rows.Add(RowConLai);
                                        dt.Rows.Add(RowDongTrong);
                                    }
                                    else
                                        goto LabelCongTac;
                                }
                                LabelCongTac:
                                var grCongTac = Nhom.GroupBy(x => x["Code"]);
                                foreach (var CTac in grCongTac)
                                {
                                    if (lstCode.Contains(CTac.Key.ToString()))
                                        continue;
                                    string CodeCTac = CTac.Key.ToString();
                                    string CodeCongTac = Guid.NewGuid().ToString();
                                    List<KLHN> rowKLHN = rowKLHNAll.Where(x => x.ParentCode == CodeCTac).ToList();
                                    if (!rowKLHN.Any())
                                        continue;
                                    var LKKHDay = rowKLHN.Where(x => x.Ngay == de_NBD.DateTime.Date).FirstOrDefault();
                                    double LKKH = (LKKHDay?.LuyKeKhoiLuongKeHoach - LKKHDay?.KhoiLuongKeHoach) ?? 0;
                                    double LKTC = (LKKHDay?.LuyKeKhoiLuongThiCong - LKKHDay?.KhoiLuongThiCong) ?? 0;
                                    //rowKLHN.Add(new KLHN
                                    //{
                                    //    Ngay = LKKHDay.Ngay.Value,
                                    //    KhoiLuongKeHoach = LKKHDay.KLKHFromBeginOfPrj,
                                    //    KhoiLuongThiCong = LKKHDay.KLTCFromBeginOfPrj
                                    //});
                                    List<KLHN> rowKLHNTC = rowKLHN.Where(x => x.KhoiLuongThiCong.HasValue && x.KhoiLuongThiCong > 0).ToList();
                                    double KLKH = (double)rowKLHN.Sum(x => x.KhoiLuongKeHoach);
                                    //WaitFormHelper.ShowWaitForm($"{CTac.FirstOrDefault()["TenCongTac"]}", "Đang tổng hợp dữ liệu");
                                    DataRow RowCongTac = dt.NewRow();
                                    RowCongTac["ID"] = CodeCongTac;
                                    RowCongTac["ParentID"] = CodeHM;
                                    RowCongTac["TenCongTac"] = CTac.FirstOrDefault()["TenCongTac"].ToString();
                                    dt.Rows.Add(RowCongTac);


                                    DataRow RowKH = dt.NewRow();
                                    DataRow RowTC = dt.NewRow();
                                    DataRow RowKHLK = dt.NewRow();
                                    DataRow RowTCLK = dt.NewRow();
                                    DataRow RowConLai = dt.NewRow();
                                    DataRow RowDongTrong = dt.NewRow();

                                    Fcn_AddChiTiet(RowKH, "Kế hoạch chi tiết", CodeCongTac);
                                    Fcn_AddChiTiet(RowTC, "Thi công chi tiết", CodeCongTac);
                                    Fcn_AddChiTiet(RowKHLK, "Lũy kế kế hoạch", CodeCongTac);
                                    Fcn_AddChiTiet(RowTCLK, "Lũy kế thi công", CodeCongTac);
                                    Fcn_AddChiTiet(RowConLai, "Còn lại (Kế hoạch-Thi công)", CodeCongTac);
                                    Fcn_AddChiTiet(RowDongTrong, "", CodeCongTac);
                                    DateTime NBD = DateTime.Parse(CTac.FirstOrDefault()["NgayBatDau"].ToString());
                                    DateTime NKT = DateTime.Parse(CTac.FirstOrDefault()["NgayKetThuc"].ToString());
                                    double NKH = (NKT - NBD).TotalDays + 1;


                                    RowKH["TongKhoiLuong"] = Math.Round(KLKH / NKH, 2);
                                    RowKH["SumNgay"] = NKH;
                                    RowKH["NgayBatDau"] = NBD.ToString(MyConstant.CONST_DATE_FORMAT_SPSHEET);
                                    RowKH["NgayKetThuc"] = NKT.ToString(MyConstant.CONST_DATE_FORMAT_SPSHEET);

                                    if (rowKLHNTC.Count() == 0)
                                        goto Label1;
                                    double KLTC = (double)rowKLHNTC.Sum(x => x.KhoiLuongThiCong);
                                    DateTime MIN = (DateTime)rowKLHNTC.Min(x => x.Ngay);
                                    DateTime MAX = (DateTime)rowKLHNTC.Max(x => x.Ngay);
                                    double NTC = (DateTime.Now.Date - MIN.Date).TotalDays + 1;
                                    double NTCSum = (MAX.Date - MIN.Date).TotalDays + 1;
                                    RowTC["SumNgay"] = NTCSum;
                                    RowTC["NgayBatDau"] = MIN.ToString(MyConstant.CONST_DATE_FORMAT_SPSHEET);
                                    RowTC["NgayKetThuc"] = MAX.ToString(MyConstant.CONST_DATE_FORMAT_SPSHEET);
                                    if (NTC > 0)
                                        RowTC["TongKhoiLuong"] = Math.Round(KLTC / NTC, 2);
                                    Label1:
                                    //lstDateCr = rowKLHN.Select(x => x.Ngay).ToList();
                                    //foreach (var itemdate in lstDate)
                                    //{
                                    //    if (lstDateCr.Contains(itemdate.Ngay))
                                    //        continue;
                                    //    rowKLHN.Add(itemdate);
                                    //}
                                    double SumConLai = 0, KLCL = 0;
                                    foreach (KLHN item in rowKLHN.OrderBy(x => x.Ngay))
                                    {
                                        string colum = item.Ngay.ToString(MyConstant.CONST_DATE_FORMAT_SPSHEET);
                                        //if (!dt.Columns.Contains(colum))
                                        //    goto Label2;
                                        double KLKHDay = item.KhoiLuongKeHoach ?? 0;
                                        LKKH += KLKHDay;
                                        RowKH[colum] = KLKHDay;
                                        RowKHLK[colum] = Math.Round(LKKH, 4);

                                        if (item.Ngay.Date <= de_NKT.DateTime.Date)
                                        {
                                            double KLTCDay = item.KhoiLuongThiCong ?? 0;

                                            if (KLTCDay > 0)
                                            {
                                                RowTC[colum] = KLTCDay;
                                                LKTC += KLTCDay;
                                            }
                                            KLCL = LKKH - LKTC;
                                            SumConLai = KLCL;
                                            RowTCLK[colum] = Math.Round(LKTC, 4);
                                            RowConLai[colum] = Math.Round(KLCL, 4);
                                        }
                                    }
                                    //RowTCLK["TongKhoiLuong"] = Math.Round(SumLKTC, 4);
                                    //RowKHLK["TongKhoiLuong"] = Math.Round(SumLKKH, 4);
                                    //RowConLai["TongKhoiLuong"] = Math.Round(SumConLai, 4);
                                    Label2:
                                    dt.Rows.Add(RowKH);
                                    dt.Rows.Add(RowTC);
                                    dt.Rows.Add(RowKHLK);
                                    dt.Rows.Add(RowTCLK);
                                    dt.Rows.Add(RowConLai);
                                    dt.Rows.Add(RowDongTrong);
                                }

                            }

                        }


                    }

                }
            }
            tl_ThongTin.FocusedNodeChanged -= tl_ThongTin_FocusedNodeChanged;
            tl_ThongTin.DataSource = dt;
            if (dt.Rows.Count == 0)
                goto Label;
            DataTable dtSlke = dt.AsEnumerable()
                .Where(x => x["IsGetToSlke"].ToString() == true.ToString()).CopyToDataTable();

            slke_ChonDonVi.Properties.DataSource = dtSlke;
            tl_ThongTin.Refresh();
            tl_ThongTin.ExpandAll();

            if (dtSlke.Rows.Count > 0)
            {
                slke_ChonDonVi.EditValue = dtSlke.Rows[0]["ID"];
            }

            //tl_ThongTin.EndUpdate();

            var colNow = tl_ThongTin.Columns[DateTime.Now.ToString(MyConstant.CONST_DATE_FORMAT_SPSHEET)];

            if (colNow != null)
            {
                tl_ThongTin.FocusedColumn = colNow;

                colNow.AppearanceHeader.BackColor = Color.Yellow;
            }
            tl_ThongTin.BeginUpdate();
            foreach (TreeListColumn item in tl_ThongTin.Columns.OrderBy(x => x.Caption))
            {
                string name = item.Caption;
                if (name == "Tên đơn vị" || name == "STT" || name == "Tên Colum")
                    continue;
                if (DateTime.Parse(name).Date != DateTime.Now.Date)
                    item.Visible = false;
                else
                    item.Visible = true;
            }
            tl_ThongTin.Refresh();
            tl_ThongTin.FocusedNodeChanged += tl_ThongTin_FocusedNodeChanged;
            tl_ThongTin.FocusedNode = tl_ThongTin.Nodes[0];
            Label:
            tl_ThongTin.EndUpdate();
            WaitFormHelper.CloseWaitForm();
        }

        private void sb_AllDuAn_Click(object sender, EventArgs e)
        {
            DialogResult rs = MessageShower.ShowYesNoQuestion("Xem tất cả dự án sẽ mất thời gian phân tích, Bạn có chắc muốn xem không??????");
            if (rs == DialogResult.No)
                return;
            Fcn_UpdateKiemSoat(true);
            if (!_CheckFocus)
            {
                if (tl_ThongTin.FocusedNode is null)
                    return;
                string CodeDuAn = tl_ThongTin.FocusedNode.GetValue("ID").ToString();
                Fcn_UpdateData(CodeDuAn, false);
            }
        }

        private void Ce_NgoaiKH_CheckedChanged(object sender, EventArgs e)
        {
            Fcn_UpdateKiemSoat();
            if (!_CheckFocus)
            {
                if (tl_ThongTin.FocusedNode is null)
                    return;
                string CodeDuAn = tl_ThongTin.FocusedNode.GetValue("ID").ToString();
                Fcn_UpdateData(CodeDuAn, false);
            }
        }

        private void ce_ChiTietTuyen_CheckedChanged(object sender, EventArgs e)
        {
            Fcn_UpdateKiemSoat();
            if (!_CheckFocus)
            {
                if (tl_ThongTin.FocusedNode is null)
                    return;
                string CodeDuAn = tl_ThongTin.FocusedNode.GetValue("ID").ToString();
                Fcn_UpdateData(CodeDuAn, false);
            }
        }
        private void Fcn_AnHienTienDo()
        {
            WaitFormHelper.ShowWaitForm("Đang ẩn hiện theo cài đặt");
            tl_KiemSoat.BeginUpdate();
            foreach (DevExpress.XtraEditors.Controls.CheckedListBoxItem item in Ccbe_AnHien.Properties.Items)
            {
                if ((int)item.Value == 1)
                {
                    TreeListNode[] Da = tl_KiemSoat.FindNodes(x => x.GetValue("TenCongTac").ToString() =="Kế hoạch chi tiết");
                    if (item.CheckState == CheckState.Unchecked)
                        Da.ForEach(x => x.Visible=true);
                    else
                        Da.ForEach(x => x.Visible = false);
                }
                else if ((int)item.Value == 2)
                {
                    TreeListNode[] Da = tl_KiemSoat.FindNodes(x => x.GetValue("TenCongTac").ToString() == "Thi công chi tiết");
                    if (item.CheckState == CheckState.Unchecked)
                        Da.ForEach(x => x.Visible = true);
                    else
                        Da.ForEach(x => x.Visible = false);

                }
                else if ((int)item.Value == 3)
                {
                    TreeListNode[] Da = tl_KiemSoat.FindNodes(x => x.GetValue("TenCongTac").ToString() == "Lũy kế kế hoạch");
                    if (item.CheckState == CheckState.Unchecked)
                        Da.ForEach(x => x.Visible = true);
                    else
                        Da.ForEach(x => x.Visible = false);

                }
                else if ((int)item.Value == 4)
                {
                    TreeListNode[] Da = tl_KiemSoat.FindNodes(x => x.GetValue("TenCongTac").ToString() == "Lũy kế thi công");
                    if (item.CheckState == CheckState.Unchecked)
                        Da.ForEach(x => x.Visible = true);
                    else
                        Da.ForEach(x => x.Visible = false);
                }
                else
                {
                    TreeListNode[] Da = tl_KiemSoat.FindNodes(x => x.GetValue("TenCongTac").ToString() == "Còn lại (Kế hoạch-Thi công)");
                    if (item.CheckState == CheckState.Unchecked)
                        Da.ForEach(x => x.Visible = true);
                    else
                        Da.ForEach(x => x.Visible = false);
                }
            }
            tl_KiemSoat.EndUpdate();
            WaitFormHelper.CloseWaitForm();
        }

        private void Ccbe_AnHien_EditValueChanged(object sender, EventArgs e)
        {
            Fcn_AnHienTienDo();
        }

        private void sp_SaiSo_EditValueChanged(object sender, EventArgs e)
        {
            SharedControls.Error = (int)sp_SaiSo.Value;
        }

        private void ce_ChiVeCongTac_CheckedChanged(object sender, EventArgs e)
        {
            Fcn_UpdateKiemSoat();
            if (!_CheckFocus)
            {
                if (tl_ThongTin.FocusedNode is null)
                    return;
                string CodeDuAn = tl_ThongTin.FocusedNode.GetValue("ID").ToString();
                Fcn_UpdateData(CodeDuAn, false);
            }
        }
    }
}
