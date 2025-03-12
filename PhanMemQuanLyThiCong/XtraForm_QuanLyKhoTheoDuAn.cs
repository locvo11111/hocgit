using DevExpress.XtraEditors;
using PhanMemQuanLyThiCong.Common.Constant;
using PhanMemQuanLyThiCong.Common.Helper;
using PhanMemQuanLyThiCong.Model.QuanLyVanChuyen;
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
    public partial class XtraForm_QuanLyKhoTheoDuAn : DevExpress.XtraEditors.XtraForm
    {
        public XtraForm_QuanLyKhoTheoDuAn()
        {
            InitializeComponent();
        }
        private List<string> lstCodeKho { get; set; }
        private DataTable dtKho { get; set; }
        public void Fcn_LoadData()
        {
            string dbString = $"SELECT QLVT.*,COALESCE(QLVT.Ten, DA.TenDuAn,CT.Ten,HM.Ten,KC.Ten) AS TenKho," +
                $"COALESCE(QLVT.CodeDuAn, QLVT.CodeCongTrinh,QLVT.CodeHangMuc,QLVT.CodeKhoChung) AS CodeKho" +
                $" FROM {QLVT.Tbl_QLVT_TenKhoTheoDuAn} QLVT" +
                $" LEFT JOIN {MyConstant.TBL_THONGTINDUAN} DA ON DA.Code=QLVT.CodeDuAn " +
                $" LEFT JOIN {MyConstant.TBL_THONGTINCONGTRINH} CT ON CT.Code=QLVT.CodeCongTrinh " +
                $" LEFT JOIN {MyConstant.TBL_THONGTINHANGMUC} HM ON HM.Code=QLVT.CodeHangMuc " +
                $" LEFT JOIN {QLVT.Tbl_QLVT_TenKhoChung} KC ON KC.Code=QLVT.CodeKhoChung ";
            dtKho = DataProvider.InstanceTHDA.ExecuteQuery(dbString);
            lstCodeKho = dtKho.AsEnumerable().Select(x => x["CodeKho"].ToString()).ToList();
            dbString = $"SELECT DA.Code,DA.TenDuAn,CT.Ten as TenCT,CT.Code as CodeCongTrinh, HM.Code as CodeHangMuc,HM.Ten as TenHM,NULL as TenKhoChung " +
                $" FROM {MyConstant.TBL_THONGTINDUAN} DA" +
                $" LEFT JOIN {QLVT.Tbl_QLVT_TenKhoTheoDuAn} QLVTDA ON DA.Code=QLVTDA.CodeDuAn " +
                $" LEFT JOIN {MyConstant.TBL_THONGTINCONGTRINH} CT ON DA.Code=CT.CodeDuAn " +
                $" LEFT JOIN {QLVT.Tbl_QLVT_TenKhoTheoDuAn} QLVTCT ON CT.Code=QLVTCT.CodeCongTrinh " +
                $" LEFT JOIN {MyConstant.TBL_THONGTINHANGMUC} HM ON HM.CodeCongTrinh=CT.Code " +
                $" LEFT JOIN {QLVT.Tbl_QLVT_TenKhoTheoDuAn} QLVTHM ON HM.Code=QLVTHM.CodeHangMuc " +
                $"UNION ALL SELECT KC.Code,NULL as TenDuAn,NULL as TenCT,NULL as CodeCongTrinh,NULL as CodeHangMuc,NULL as TenHM," +
                $" KC.Ten as TenKhoChung FROM {QLVT.Tbl_QLVT_TenKhoChung} KC " +
                $" LEFT JOIN {QLVT.Tbl_QLVT_TenKhoTheoDuAn} QLVTKC ON KC.Code=QLVTKC.CodeKhoChung ";
            DataTable dtDuAn = DataProvider.InstanceTHDA.ExecuteQuery(dbString);

            List<QuanLyDanhSachKhoDuAn> lst = new List<QuanLyDanhSachKhoDuAn>();
            string CodeKC = $"{Guid.NewGuid()}_KhoChung";
            string CodeDA = $"{Guid.NewGuid()}_DA";
            lst.Add(new QuanLyDanhSachKhoDuAn
            {
                ID = CodeDA,
                ParentID = "0",
                TenKho = "TÊN KHO THEO DỰ ÁN"
            });
            lst.Add(new QuanLyDanhSachKhoDuAn
            {
                ID = CodeKC,
                ParentID = "0",
                TenKho = "TÊN KHO THEO DANH SÁCH KHO CHUNG"
            });
            var grDuAn = dtDuAn.AsEnumerable().GroupBy(x => x["Code"]);

            foreach (var itemDuAn in grDuAn)
            {
                if (itemDuAn.FirstOrDefault()["TenDuAn"] == DBNull.Value)
                {
                    lst.Add(new QuanLyDanhSachKhoDuAn
                    {
                        ID = itemDuAn.Key.ToString(),
                        CodeKhoChung = itemDuAn.Key.ToString(),
                        ParentID = CodeKC,
                        TenKho = itemDuAn.FirstOrDefault()["TenKhoChung"].ToString()
                    });
                }
                else
                {
                    lst.Add(new QuanLyDanhSachKhoDuAn
                    {
                        ID = itemDuAn.Key.ToString(),
                        CodeDuAn = itemDuAn.Key.ToString(),
                        ParentID = CodeDA,
                        TenKho = itemDuAn.FirstOrDefault()["TenDuAn"].ToString()
                    });
                }
                var grCongTrinh = itemDuAn.Where(x => x["CodeCongTrinh"] != DBNull.Value).GroupBy(x => x["CodeCongTrinh"]);
                foreach (var itemctrinh in grCongTrinh)
                {
                    lst.Add(new QuanLyDanhSachKhoDuAn
                    {
                        ID = itemctrinh.Key.ToString(),
                        CodeCongTrinh = itemctrinh.Key.ToString(),
                        ParentID = itemDuAn.Key.ToString(),
                        TenKho = itemDuAn.FirstOrDefault()["TenCT"].ToString()
                    });
                    var grHM = itemctrinh.GroupBy(x => x["CodeHangMuc"]);
                    foreach (var itemHM in grHM)
                    {
                        lst.Add(new QuanLyDanhSachKhoDuAn
                        {
                            ID = itemHM.Key.ToString(),
                            CodeHangMuc = itemHM.Key.ToString(),
                            ParentID = itemctrinh.Key.ToString(),
                            TenKho = itemDuAn.FirstOrDefault()["TenHM"].ToString()
                        });
                    }

                }
            }
            foreach (var item in lst)
            {
                if (lstCodeKho.Contains(item.ID))
                    item.Chon = true;
            }
            tl_TenKho.DataSource = lst;
            tl_TenKho.ExpandAll();
        }

        private void tl_TenKho_NodeCellStyle(object sender, DevExpress.XtraTreeList.GetCustomNodeCellStyleEventArgs e)
        {
            if (e.Node.Level == 0)
            {
                e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold);
                string Id = e.Node.GetValue("ID").ToString();
                if (Id.Contains("_DA"))
                    e.Appearance.ForeColor = Color.Green;
                else
                    e.Appearance.ForeColor = Color.Red;

            }
        }

        private void sb_Save_Click(object sender, EventArgs e)
        {
            List<QuanLyDanhSachKhoDuAn> lst = tl_TenKho.DataSource as List<QuanLyDanhSachKhoDuAn>;
            List<string> lstCode = lst.Where(x => x.Chon).Select(x => x.ID).ToList();
            if (lstCodeKho.Any())
            {
                //foreach(var item in lstCodeKho)
                //{
                //   if(lstCode.Contains(item))
                //        DuAnHelper.DeleteDataRows(QLVT.Tbl_QLVT_TenKhoTheoDuAn, NonIntersect.ToArray());
                //}   
                //foreach(var item in lstCodeKho)
                //{
                //   if(lstCode.Contains(item))
                //        DuAnHelper.DeleteDataRows(QLVT.Tbl_QLVT_TenKhoTheoDuAn, NonIntersect.ToArray());
                //}
                if (lstCode.Any())
                {
                    List<string> lstCodeIntersect = lstCodeKho.Intersect(lstCode).ToList();
                    //if (lstCodeIntersect.Count() == lstCode.Count())
                    //    return;
                    List<string> NonIntersect = lstCodeKho.Except(lstCodeIntersect).ToList();
                    if (NonIntersect.Any())
                    {
                        DataRow[] Rowdelete = dtKho.AsEnumerable().Where(x => NonIntersect.Contains(x["CodeKho"].ToString())).ToArray();
                        foreach (var itemrow in Rowdelete)
                        {
                            DuAnHelper.DeleteDataRows(QLVT.Tbl_QLVT_TenKhoTheoDuAn, new string[] { itemrow["Code"].ToString() });
                            dtKho.Rows.Remove(itemrow);
                        }
                    }

                    List<string> NonIntersectTenKho = lstCode.Except(lstCodeIntersect).ToList();
                    List<QuanLyDanhSachKhoDuAn> lstTenKhoSelect = lst.Where(x => NonIntersectTenKho.Contains(x.ID)).ToList();
                    foreach (var item in lstTenKhoSelect)
                    {
                        DataRow row = dtKho.NewRow();
                        row["Code"] = Guid.NewGuid();
                        row[item.ColCode] = item.ID;
                        dtKho.Rows.Add(row);
                    }
                    DataProvider.InstanceTHDA.UpdateDataTableFromSqliteSource(dtKho, QLVT.Tbl_QLVT_TenKhoTheoDuAn);
                }
                else
                {
                    DialogResult rs = MessageShower.ShowYesNoQuestion("Bạn bỏ chọn tất cả kho trong dự án, Bạn có chắc chắn lựa chọn này và tiếp tục không????");
                    if (rs == DialogResult.Yes)
                    {
                        DuAnHelper.DeleteDataRows(QLVT.Tbl_QLVT_TenKhoTheoDuAn, lstCodeKho.ToArray());
                    }
                }
            }
            else
            {
                foreach (var item in lst)
                {
                    if (!item.Chon)
                        continue;
                    DataRow row = dtKho.NewRow();
                    row["Code"] = Guid.NewGuid();
                    row[item.ColCode] = item.ID;
                    dtKho.Rows.Add(row);
                }
                DataProvider.InstanceTHDA.UpdateDataTableFromSqliteSource(dtKho, QLVT.Tbl_QLVT_TenKhoTheoDuAn);
            }
        }

        private void tl_TenKho_CustomDrawNodeCheckBox(object sender, DevExpress.XtraTreeList.CustomDrawNodeCheckBoxEventArgs e)
        {
            if (e.Node.Level == 0)
            {
                e.ObjectArgs.State = DevExpress.Utils.Drawing.ObjectState.Disabled;
                e.Handled = false;
                //e.Node.ChildrenCheckBoxStyle= DevExpress.XtraTreeList.NodeCheckBoxStyle.Check;
            }
        }

        private void tl_TenKho_BeforeCheckNode(object sender, DevExpress.XtraTreeList.CheckNodeEventArgs e)
        {
            if (e.Node.Level == 0)
            {
                //e.Node.ChildrenCheckBoxStyle = DevExpress.XtraTreeList.NodeCheckBoxStyle.Check;
                e.CanCheck = false;
            }

        }
    }
}