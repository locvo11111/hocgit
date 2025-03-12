using DevExpress.XtraEditors;
using PhanMemQuanLyThiCong.Common.Constant;
using PhanMemQuanLyThiCong.Common.Helper;
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
    public partial class XtraForm_TaoLienKetCongTacThauChinhPhu : DevExpress.XtraEditors.XtraForm
    {
        public delegate void DE_TRUYENDATALink(List<LayCongTac> Link, string CodeCT);
        public DE_TRUYENDATALink m_DataLink;
        private List<LayCongTac> _LstSource;
        private string _CodeCTNhaThau;
        public XtraForm_TaoLienKetCongTacThauChinhPhu()
        {
            InitializeComponent();
        }
        public void Fcn_LoadData(string TypeRow,string CodeCTNhaThau)
        {
            _CodeCTNhaThau = CodeCTNhaThau;
            _LstSource = new List<LayCongTac>();
            string Condition= TypeRow==MyConstant.TYPEROW_CVCha?"cttk.CodeCha IS NULL":"cttk.CodeCha IS NOT NULL";
            string dbString = $"SELECT cttk.*, \r\n" +
                $"cttk.KhoiLuongHopDongChiTiet AS KhoiLuongHopDong," +
                        $"hm.Ten AS TenHangMuc, " +
                        $"hm.Code AS CodeHangMuc, " +
                        $"ctr.Ten AS TenCongTrinh, " +
                        $"ctr.Code AS CodeCongTrinh, " +
                        $"COALESCE(tdtc.Ten,ntp.Ten) AS TenNhaThauPhu,COALESCE(cttk.CodeNhaThauPhu,cttk.CodeToDoi) as CodeNhaThauPhu \r\n" +
                        $"FROM {TDKH.TBL_ChiTietCongTacTheoKy} cttk\r\n" +
                        $"JOIN {MyConstant.TBL_THONGTINHANGMUC} hm\r\n" +
                        $"ON cttk.CodeHangMuc = hm.Code " +
                        $"JOIN {MyConstant.TBL_THONGTINCONGTRINH} ctr\r\n" +
                        $"ON hm.CodeCongTrinh = ctr.Code " +
                        $"LEFT JOIN {MyConstant.TBL_THONGTINTODOITHICONG} tdtc\r\n" +
                        $"ON cttk.CodeToDoi = tdtc.Code \r\n" +
                        $"LEFT JOIN {MyConstant.TBL_THONGTINNHATHAUPHU} ntp\r\n" +
                        $"ON cttk.CodeNhaThauPhu = ntp.Code \r\n" +
                        $"WHERE (\"CodeGiaiDoan\" = '{SharedControls.cbb_DBKH_ChonDot.SelectedValue}' " +
                        $"AND cttk.CodeNhaThau IS NULL AND {Condition} AND cttk.CodeCongTac IS NULL AND cttk.CodeCongTacGiaoThau IS NULL) OR cttk.CodeCongTacGiaoThau='{CodeCTNhaThau}' \r\n" +
                        $"ORDER BY cttk.SortId";

            var dtCongTacTheoKy = DataProvider.InstanceTHDA.ExecuteQuery(dbString);
            var grNhaThau = dtCongTacTheoKy.AsEnumerable().GroupBy(x => x["CodeNhaThauPhu"].ToString());
            foreach(var Nt in grNhaThau)
            {
                _LstSource.Add(new LayCongTac
                {
                    Id=Nt.Key,
                    ParentId="0",
                    TenCongTac=Nt.FirstOrDefault()["TenNhaThauPhu"].ToString()

                });
                var grCongTac= Nt.GroupBy(x => x["Code"].ToString());
                foreach(var Ct in grCongTac)
                {
                    _LstSource.Add(new LayCongTac
                    {
                        Id = Ct.Key,
                        Chon = Ct.FirstOrDefault()["CodeCongTacGiaoThau"] == DBNull.Value ? false : true,
                        ParentId = Nt.Key,
                        CodeCongTacGiaoThau= Ct.FirstOrDefault()["CodeCongTacGiaoThau"].ToString(),
                        TenCongTac = Ct.FirstOrDefault()["TenCongTac"].ToString(),
                        DonVi = Ct.FirstOrDefault()["DonVi"].ToString(),
                        KhoiLuongHopDong = Ct.FirstOrDefault()["KhoiLuongHopDong"] != DBNull.Value ? double.Parse(Ct.FirstOrDefault()["KhoiLuongHopDong"].ToString()) : 0
                    }) ;
                }
            }
            tl_Link.DataSource = _LstSource;
            tl_Link.ExpandAll();
            tl_Link.RefreshDataSource();
        }
        private void sb_Huy_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void sb_Ok_Click(object sender, EventArgs e)
        {
            List<LayCongTac> LstSource = _LstSource.Where(x => x.Chon == true||!string.IsNullOrEmpty(x.CodeCongTacGiaoThau)).ToList();
            m_DataLink(LstSource, _CodeCTNhaThau);
            this.Close();
        }

        private void tl_Link_NodeCellStyle(object sender, DevExpress.XtraTreeList.GetCustomNodeCellStyleEventArgs e)
        {
            if (e.Node.Level == 0)
            {
                e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold);
                e.Appearance.ForeColor = Color.Red;
            }
        }
    }
}