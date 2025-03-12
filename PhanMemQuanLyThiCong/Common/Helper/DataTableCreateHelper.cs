using PhanMemQuanLyThiCong.Common.Constant;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhanMemQuanLyThiCong.Common.Helper
{
    public static class DataTableCreateHelper
    {
        public static DataTable DoBocChuanTDKH()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add(TDKH.COL_Chon, typeof(bool));
            dt.Columns.Add(TDKH.COL_STT, typeof(string));
            dt.Columns.Add(TDKH.COL_STTDocVao, typeof(string));
            dt.Columns.Add(TDKH.COL_STTND, typeof(string));
            dt.Columns.Add(TDKH.COL_CustomOrderWholeHM, typeof(int));
            dt.Columns.Add(TDKH.COL_CustomOrder, typeof(int));
            dt.Columns.Add(TDKH.COL_TenGop, typeof(string));
            dt.Columns.Add(TDKH.COL_MaHieuCongTac, typeof(string));
            dt.Columns.Add(TDKH.COL_DanhMucCongTac, typeof(string));
            dt.Columns.Add(TDKH.COL_DonVi, typeof(string));
            dt.Columns.Add(TDKH.COL_DBC_SoBoPhanGiongNhau, typeof(int));
            dt.Columns.Add(TDKH.COL_DBC_Dai, typeof(double));
            dt.Columns.Add(TDKH.COL_DBC_Rong, typeof(double));
            dt.Columns.Add(TDKH.COL_DBC_Cao, typeof(double));
            dt.Columns.Add(TDKH.COL_DBC_HeSoCauKien, typeof(double));
            dt.Columns.Add(TDKH.COL_DBC_KL1BoPhan, typeof(object));
            dt.Columns.Add(TDKH.COL_DBC_KhoiLuongToanBo, typeof(object));
            dt.Columns.Add(TDKH.COL_DBC_LuyKeDaThucHien, typeof(object));
            dt.Columns.Add(TDKH.COL_PhanTichVatTu, typeof(bool));
            dt.Columns.Add(TDKH.COL_HasHopDongAB, typeof(bool));
            dt.Columns.Add(TDKH.COL_IsUseMTC, typeof(bool));
            dt.Columns.Add(TDKH.COL_KhoiLuongHopDongChiTiet, typeof(double));
            dt.Columns.Add(TDKH.COL_KhoiLuongHopDongDuAn, typeof(double));
            dt.Columns.Add(TDKH.COL_MuiThiCong, typeof(string));
            dt.Columns.Add(TDKH.COL_GhiChu, typeof(string));
            dt.Columns.Add(TDKH.COL_RowCha, typeof(object));
            dt.Columns.Add(TDKH.COL_Code, typeof(string));
            dt.Columns.Add(TDKH.COL_TypeRow, typeof(string));
            dt.Columns.Add(TDKH.COL_KhoiLuongToanBo_Iscongthucmacdinh, typeof(bool));
            dt.Columns.Add(TDKH.COL_CodeMuiThiCong, typeof(string));
            dt.Columns.Add(TDKH.COL_IsCongTacPhatSinh, typeof(bool));

            return dt;
        }

        public static DataTable TienDoKinhPhi()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add(TDKH.COL_Chon, typeof(bool));
            dt.Columns.Add(TDKH.COL_STT, typeof(string));
            dt.Columns.Add(TDKH.COL_STTDocVao, typeof(string));
            dt.Columns.Add(TDKH.COL_STTND, typeof(string));
            dt.Columns.Add(TDKH.COL_MaHieuCongTac, typeof(string));
            dt.Columns.Add(TDKH.COL_DanhMucCongTac, typeof(string));
            dt.Columns.Add(TDKH.COL_DonVi, typeof(string));
            dt.Columns.Add(TDKH.COL_DBC_KhoiLuongToanBo, typeof(double));

            dt.Columns.Add(TDKH.COL_KhoiLuongNhapVao, typeof(double));
            dt.Columns.Add(TDKH.COL_KhoiLuongKeHoachGiaiDoan, typeof(double));
            dt.Columns.Add(TDKH.COL_KhoiLuongThiCongLuyKeDenKyTruoc, typeof(double));
            dt.Columns.Add(TDKH.COL_KhoiLuongThiCongLuyKeDenGiaiDoan, typeof(double));
            dt.Columns.Add(TDKH.COL_KhoiLuongThiCongGiaiDoan, typeof(double));
            dt.Columns.Add(TDKH.COL_KhoiLuongConLaiGiaiDoanSoVoiHopDong, typeof(object));
            dt.Columns.Add(TDKH.COL_KhoiLuongConLaiGiaiDoan, typeof(object));
            dt.Columns.Add(TDKH.COL_KhoiLuongBoSungGiaiDoan, typeof(object));
            



            dt.Columns.Add(TDKH.COL_KhoiLuongDaThiCong, typeof(double));
            dt.Columns.Add(TDKH.COL_KhoiLuongHopDongChiTiet, typeof(double));
            dt.Columns.Add(TDKH.COL_PhanTramThucHien, typeof(object));
            dt.Columns.Add(TDKH.COL_DonGia, typeof(long));
            dt.Columns.Add(TDKH.COL_DonGiaThiCong, typeof(long));

            
            dt.Columns.Add(TDKH.COL_NgayBatDau, typeof(object));
            dt.Columns.Add(TDKH.COL_NgayKetThuc, typeof(object));     
            dt.Columns.Add(TDKH.COL_NgayBatDauGT, typeof(object));
            dt.Columns.Add(TDKH.COL_NgayKetThucGT, typeof(object));
            dt.Columns.Add(TDKH.COL_SoNgayThucHien, typeof(object));
            dt.Columns.Add(TDKH.COL_NgayBatDauThiCong, typeof(object));
            dt.Columns.Add(TDKH.COL_NgayKetThucThiCong, typeof(object));
            dt.Columns.Add(TDKH.COL_SoNgayThiCong, typeof(object));
            dt.Columns.Add(TDKH.COL_TrangThai, typeof(string));
            dt.Columns.Add(TDKH.COL_NhanCong, typeof(object));


            dt.Columns.Add(TDKH.COL_GiaTriKeHoachGiaiDoan, typeof(object));
            dt.Columns.Add(TDKH.COL_GiaTriThiCongLuyKeDenKyTruoc, typeof(object));

            dt.Columns.Add(TDKH.COL_GiaTriThiCongLuyKeDenGiaiDoan, typeof(object));
            dt.Columns.Add(TDKH.COL_GiaTriThiCongGiaiDoan, typeof(object));
            dt.Columns.Add(TDKH.COL_GiaTriConLaiGiaiDoan, typeof(object));
            dt.Columns.Add(TDKH.COL_PhanTramGiaTriKyNay, typeof(object));
            dt.Columns.Add(TDKH.COL_PhanTramGiaTriLuyKeKyNay, typeof(object));

            dt.Columns.Add(TDKH.COL_GiaTriBoSungGiaiDoan, typeof(object));

            dt.Columns.Add(TDKH.COL_KinhPhiDuKien, typeof(object));
            dt.Columns.Add(TDKH.COL_GiaTri, typeof(object));
            dt.Columns.Add(TDKH.COL_GiaTriNhanThau, typeof(object));
            dt.Columns.Add(TDKH.COL_GiaTriHopDong, typeof(object));
            dt.Columns.Add(TDKH.COL_GiaTriThiCong, typeof(object));
            
            dt.Columns.Add(TDKH.COL_GiaTriConLaiSoVoiHopDong, typeof(object));
            dt.Columns.Add(TDKH.COL_GiaTriConLaiSoVoiKeHoach, typeof(object));

            dt.Columns.Add(TDKH.COL_LyTrinhCaoDo, typeof(string));
            dt.Columns.Add(TDKH.COL_GhiChu, typeof(string));
            dt.Columns.Add(TDKH.COL_FileDinhKem, typeof(string));

            dt.Columns.Add(TDKH.COL_RowCha, typeof(object));
            dt.Columns.Add(TDKH.COL_Code, typeof(string));
            dt.Columns.Add(TDKH.COL_CodeDMCT, typeof(string));
            dt.Columns.Add(TDKH.COL_NhanCongIsCongThucMacDinh, typeof(bool));
            dt.Columns.Add(TDKH.COL_KinhPhiDuKienIsCongThucMacDinh, typeof(bool));
            dt.Columns.Add(TDKH.COL_TypeRow, typeof(string));


            return dt;
        }
        
        public static DataTable TongHopKinhPhiTDKH()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add(TDKH.COL_Chon, typeof(bool));
            dt.Columns.Add(TDKH.COL_STT, typeof(string));
            dt.Columns.Add(TDKH.COL_STTDocVao, typeof(string));
            dt.Columns.Add(TDKH.COL_STTND, typeof(string));
            dt.Columns.Add(TDKH.COL_MaHieuCongTac, typeof(string));
            dt.Columns.Add(TDKH.COL_DanhMucCongTac, typeof(string));
            dt.Columns.Add(TDKH.COL_DonVi, typeof(string));
            dt.Columns.Add(TDKH.COL_DBC_KhoiLuongToanBo, typeof(double));
            dt.Columns.Add(TDKH.COL_KhoiLuongDaThiCong, typeof(double));

            dt.Columns.Add(TDKH.COL_KhoiLuongKeHoachGiaiDoan, typeof(object));
            dt.Columns.Add(TDKH.COL_KhoiLuongBoSungGiaiDoan, typeof(object));
            dt.Columns.Add(TDKH.COL_KhoiLuongHopDongChiTiet, typeof(double));
            dt.Columns.Add(TDKH.COL_PhanTramThucHien, typeof(object));
            dt.Columns.Add(TDKH.COL_DonGia, typeof(double));
            dt.Columns.Add(TDKH.COL_DonGiaThiCong, typeof(double));
            dt.Columns.Add(TDKH.COL_NgayBatDau, typeof(object));
            dt.Columns.Add(TDKH.COL_NgayKetThuc, typeof(object));
            dt.Columns.Add(TDKH.COL_SoNgayThucHien, typeof(object));
            dt.Columns.Add(TDKH.COL_NgayBatDauThiCong, typeof(object));
            dt.Columns.Add(TDKH.COL_NgayKetThucThiCong, typeof(object));
            dt.Columns.Add(TDKH.COL_SoNgayThiCong, typeof(object));
            dt.Columns.Add(TDKH.COL_TrangThai, typeof(string));
            dt.Columns.Add(TDKH.COL_NhanCong, typeof(object));
            dt.Columns.Add(TDKH.COL_KinhPhiDuKien, typeof(object));
            dt.Columns.Add(TDKH.COL_GiaTri, typeof(object));


            dt.Columns.Add(TDKH.COL_GiaTriThiCong, typeof(object));
            dt.Columns.Add(TDKH.COL_LyTrinhCaoDo, typeof(string));
            dt.Columns.Add(TDKH.COL_GhiChu, typeof(string));

            dt.Columns.Add(TDKH.COL_RowCha, typeof(object));
            dt.Columns.Add(TDKH.COL_Code, typeof(string));
            dt.Columns.Add(TDKH.COL_NhanCongIsCongThucMacDinh, typeof(bool));
            dt.Columns.Add(TDKH.COL_KinhPhiDuKienIsCongThucMacDinh, typeof(bool));
            dt.Columns.Add(TDKH.COL_TypeRow, typeof(string));


            return dt;
        }
        public static DataTable TienDoVatTu()
        {
            DataTable dt = new DataTable();

            //dt.Columns.Add(TDKH.COL_Chon, typeof(bool));
            dt.Columns.Add(TDKH.COL_STT, typeof(string));
            dt.Columns.Add(TDKH.COL_MaHieuCongTac, typeof(string));
            dt.Columns.Add(TDKH.COL_DanhMucCongTac, typeof(string));
            dt.Columns.Add(TDKH.COL_DonVi, typeof(string));
            dt.Columns.Add(TDKH.COL_DonGia, typeof(double));
            dt.Columns.Add(TDKH.COL_DonGiaThiCong, typeof(double));
            dt.Columns.Add(TDKH.COL_KhoiLuongKeHoachTheoCongTac, typeof(double));
            dt.Columns.Add(TDKH.COL_DBC_KhoiLuongToanBo, typeof(double));
            dt.Columns.Add(TDKH.COL_KhoiLuongHopDongTheoCongTac, typeof(double));
            dt.Columns.Add(TDKH.COL_KhoiLuongHopDongChiTiet, typeof(double));
            dt.Columns.Add(TDKH.COL_MayQuyDoi, typeof(string));

            dt.Columns.Add(TDKH.COL_KhoiLuongKeHoachGiaiDoan, typeof(object));
            dt.Columns.Add(TDKH.COL_KhoiLuongKeHoachGiaiDoanCongTac, typeof(object));
            dt.Columns.Add(TDKH.COL_KhoiLuongKeHoachGiaiDoanTheoCongTac, typeof(object));
            dt.Columns.Add(TDKH.COL_KhoiLuongThiCongLuyKeDenKyTruoc, typeof(object));
            dt.Columns.Add(TDKH.COL_KhoiLuongThiCongLuyKeDenKyTruocCongTac, typeof(object));
            dt.Columns.Add(TDKH.COL_KhoiLuongThiCongLuyKeDenKyTruocTheoCongTac, typeof(object));
            dt.Columns.Add(TDKH.COL_KhoiLuongThiCongLuyKeDenGiaiDoan, typeof(object));
            dt.Columns.Add(TDKH.COL_KhoiLuongThiCongLuyKeDenGiaiDoanCongTac, typeof(object));
            dt.Columns.Add(TDKH.COL_KhoiLuongThiCongLuyKeDenGiaiDoanTheoCongTac, typeof(object));
            dt.Columns.Add(TDKH.COL_KhoiLuongThiCongGiaiDoan, typeof(object));
            dt.Columns.Add(TDKH.COL_KhoiLuongThiCongGiaiDoanCongTac, typeof(object));
            dt.Columns.Add(TDKH.COL_KhoiLuongThiCongGiaiDoanTheoCongTac, typeof(object));
            dt.Columns.Add(TDKH.COL_KhoiLuongConLaiGiaiDoanSoVoiHopDong, typeof(object));
            dt.Columns.Add(TDKH.COL_KhoiLuongConLaiGiaiDoan, typeof(object));
            dt.Columns.Add(TDKH.COL_KhoiLuongBoSungGiaiDoan, typeof(object));

            dt.Columns.Add(TDKH.COL_NgayBatDau, typeof(object));
            dt.Columns.Add(TDKH.COL_NgayKetThuc, typeof(object));
            dt.Columns.Add(TDKH.COL_SoNgayThucHien, typeof(object));
            dt.Columns.Add(TDKH.COL_NgayBatDauThiCong, typeof(object));
            dt.Columns.Add(TDKH.COL_NgayKetThucThiCong, typeof(object));
            dt.Columns.Add(TDKH.COL_SoNgayThiCong, typeof(object));
            dt.Columns.Add(TDKH.COL_KhoiLuongDaThiCong, typeof(double));
            dt.Columns.Add(TDKH.COL_KhoiLuongDaThiCongTheoCongTac, typeof(object));


            dt.Columns.Add(TDKH.COL_GiaTriKeHoachGiaiDoan, typeof(object));
            dt.Columns.Add(TDKH.COL_GiaTriKeHoachGiaiDoanTheoCongTac, typeof(object));
            dt.Columns.Add(TDKH.COL_GiaTriThiCongLuyKeDenKyTruoc, typeof(object));
            dt.Columns.Add(TDKH.COL_GiaTriThiCongLuyKeDenKyTruocTheoCongTac, typeof(object));
            dt.Columns.Add(TDKH.COL_GiaTriThiCongLuyKeDenGiaiDoan, typeof(object));
            dt.Columns.Add(TDKH.COL_GiaTriThiCongLuyKeDenGiaiDoanTheoCongTac, typeof(object));
            dt.Columns.Add(TDKH.COL_GiaTriThiCongGiaiDoan, typeof(object));
            dt.Columns.Add(TDKH.COL_GiaTriThiCongGiaiDoanTheoCongTac, typeof(object));
            dt.Columns.Add(TDKH.COL_GiaTriConLaiGiaiDoan, typeof(object));
            dt.Columns.Add(TDKH.COL_PhanTramGiaTriKyNay, typeof(object));
            dt.Columns.Add(TDKH.COL_PhanTramGiaTriLuyKeKyNay, typeof(object));
            dt.Columns.Add(TDKH.COL_GiaTriBoSungGiaiDoan, typeof(object));

            dt.Columns.Add(TDKH.COL_GiaTriThiCong, typeof(object));
            dt.Columns.Add(TDKH.COL_KinhPhiDuKien, typeof(object));
            dt.Columns.Add(TDKH.COL_GiaTri, typeof(object));
            dt.Columns.Add(TDKH.COL_KHVT_MaTXHientruong, typeof(string));
            dt.Columns.Add(TDKH.COL_KHVT_Search, typeof(string));

            dt.Columns.Add(TDKH.COL_Code, typeof(string));
            dt.Columns.Add(TDKH.COL_TypeRow, typeof(string));
            dt.Columns.Add(TDKH.COL_RowCha, typeof(object));
            dt.Columns.Add(TDKH.COL_KHVT_DinhMucNguoiDung, typeof(object));
            dt.Columns.Add(TDKH.COL_KHVT_HeSoNguoiDung, typeof(object));

            return dt;
        }
        
        public static DataTable TienDoVatTuFullTongHop()
        {
            DataTable dt = new DataTable();

            //dt.Columns.Add(TDKH.COL_Chon, typeof(bool));
            dt.Columns.Add(TDKH.COL_STT, typeof(string));
            dt.Columns.Add(TDKH.COL_MaHieuCongTac, typeof(string));
            dt.Columns.Add(TDKH.COL_DanhMucCongTac, typeof(string));
            dt.Columns.Add(TDKH.COL_DonVi, typeof(string));
            dt.Columns.Add(TDKH.COL_DonGia, typeof(double));
            dt.Columns.Add(TDKH.COL_DonGiaThiCong, typeof(double));
            dt.Columns.Add(TDKH.COL_KhoiLuongKeHoachTheoCongTac, typeof(double));
            dt.Columns.Add(TDKH.COL_DBC_KhoiLuongToanBo, typeof(double));
            dt.Columns.Add(TDKH.COL_KhoiLuongHopDongTheoCongTac, typeof(double));
            dt.Columns.Add(TDKH.COL_KhoiLuongHopDongChiTiet, typeof(double));

            dt.Columns.Add(TDKH.COL_KhoiLuongKeHoachGiaiDoan, typeof(object));
            dt.Columns.Add(TDKH.COL_KhoiLuongKeHoachGiaiDoanCongTac, typeof(object));
            dt.Columns.Add(TDKH.COL_KhoiLuongKeHoachGiaiDoanTheoCongTac, typeof(object));
            dt.Columns.Add("DRAFT", typeof(object));
            dt.Columns.Add(TDKH.COL_KhoiLuongThiCongLuyKeDenKyTruoc, typeof(object));
            dt.Columns.Add(TDKH.COL_KhoiLuongThiCongLuyKeDenKyTruocCongTac, typeof(object));
            dt.Columns.Add(TDKH.COL_KhoiLuongThiCongLuyKeDenKyTruocTheoCongTac, typeof(object));
            dt.Columns.Add(TDKH.COL_KhoiLuongThiCongLuyKeDenGiaiDoan, typeof(object));
            dt.Columns.Add(TDKH.COL_KhoiLuongThiCongLuyKeDenGiaiDoanCongTac, typeof(object));
            dt.Columns.Add(TDKH.COL_KhoiLuongThiCongLuyKeDenGiaiDoanTheoCongTac, typeof(object));
            dt.Columns.Add(TDKH.COL_KhoiLuongThiCongGiaiDoan, typeof(object));
            dt.Columns.Add(TDKH.COL_KhoiLuongThiCongGiaiDoanCongTac, typeof(object));
            dt.Columns.Add(TDKH.COL_KhoiLuongThiCongGiaiDoanTheoCongTac, typeof(object));
            dt.Columns.Add(TDKH.COL_KhoiLuongConLaiGiaiDoanSoVoiHopDong, typeof(object));
            dt.Columns.Add(TDKH.COL_KhoiLuongConLaiGiaiDoan, typeof(object));
            dt.Columns.Add(TDKH.COL_KhoiLuongBoSungGiaiDoan, typeof(object));

            dt.Columns.Add(TDKH.COL_NgayBatDau, typeof(object));
            dt.Columns.Add(TDKH.COL_NgayKetThuc, typeof(object));
            dt.Columns.Add(TDKH.COL_SoNgayThucHien, typeof(object));
            dt.Columns.Add(TDKH.COL_NgayBatDauThiCong, typeof(object));
            dt.Columns.Add(TDKH.COL_NgayKetThucThiCong, typeof(object));
            dt.Columns.Add(TDKH.COL_SoNgayThiCong, typeof(object));


            dt.Columns.Add(TDKH.COL_KhoiLuongDaThiCong, typeof(double));
            dt.Columns.Add(TDKH.COL_KhoiLuongDaThiCongTheoCongTac, typeof(object));


            dt.Columns.Add(TDKH.COL_GiaTriKeHoachGiaiDoan, typeof(object));
            dt.Columns.Add(TDKH.COL_GiaTriKeHoachGiaiDoanTheoCongTac, typeof(object));
            dt.Columns.Add(TDKH.COL_GiaTriThiCongLuyKeDenKyTruoc, typeof(object));
            dt.Columns.Add(TDKH.COL_GiaTriThiCongLuyKeDenKyTruocTheoCongTac, typeof(object));
            dt.Columns.Add(TDKH.COL_GiaTriThiCongLuyKeDenGiaiDoan, typeof(object));
            dt.Columns.Add(TDKH.COL_GiaTriThiCongLuyKeDenGiaiDoanTheoCongTac, typeof(object));
            dt.Columns.Add(TDKH.COL_GiaTriThiCongGiaiDoan, typeof(object));
            dt.Columns.Add(TDKH.COL_GiaTriThiCongGiaiDoanTheoCongTac, typeof(object));
            dt.Columns.Add(TDKH.COL_GiaTriConLaiGiaiDoan, typeof(object));
            dt.Columns.Add(TDKH.COL_PhanTramGiaTriKyNay, typeof(object));
            dt.Columns.Add(TDKH.COL_PhanTramGiaTriLuyKeKyNay, typeof(object));
            dt.Columns.Add(TDKH.COL_GiaTriBoSungGiaiDoan, typeof(object));

            dt.Columns.Add(TDKH.COL_KhoiLuongDaNhapKho, typeof(double));
            dt.Columns.Add(TDKH.COL_GiaTriThiCong, typeof(object));
            dt.Columns.Add(TDKH.COL_KinhPhiDuKien, typeof(object));
            dt.Columns.Add(TDKH.COL_GiaTri, typeof(object));
            dt.Columns.Add(TDKH.COL_KHVT_MaTXHientruong, typeof(string));
            dt.Columns.Add(TDKH.COL_KHVT_Search, typeof(string));

            dt.Columns.Add(TDKH.COL_Code, typeof(string));
            dt.Columns.Add(TDKH.COL_TypeRow, typeof(string));
            dt.Columns.Add(TDKH.COL_RowCha, typeof(object));
            dt.Columns.Add(TDKH.COL_KHVT_DinhMucNguoiDung, typeof(object));
            dt.Columns.Add(TDKH.COL_KHVT_HeSoNguoiDung, typeof(object));

            return dt;
        }   
        public static DataTable BangKeHoachHN()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add(TDKH.COL_STT, typeof(string));
            dt.Columns.Add(TDKH.COL_DanhMucCongTac, typeof(string));
            dt.Columns.Add(TDKH.COL_DonVi, typeof(string));
            dt.Columns.Add(TDKH.COL_KhoiLuongHopDongChiTiet, typeof(double));
            dt.Columns.Add(TDKH.COL_LuyKeKyTruoc, typeof(double));
            dt.Columns.Add(TDKH.COL_ThucHienKyNay, typeof(double));
            dt.Columns.Add(TDKH.COL_LuyKeKyNay, typeof(object));
            dt.Columns.Add(TDKH.COL_ConLai, typeof(object));
            dt.Columns.Add(TDKH.COL_DonGia, typeof(double));
            dt.Columns.Add(TDKH.COL_GiaTriHD, typeof(object));
            dt.Columns.Add(TDKH.COL_GiaTriLKKT, typeof(object));
            dt.Columns.Add(TDKH.COL_GiaTriThucHien, typeof(object));
            dt.Columns.Add(TDKH.COL_GiaTriLKKN, typeof(object));
            dt.Columns.Add(TDKH.COL_GiaTriConLai, typeof(object));
            dt.Columns.Add(TDKH.COL_TyLe, typeof(object));
            dt.Columns.Add(TDKH.COL_GhiChu, typeof(object));

            dt.Columns.Add(TDKH.COL_Code, typeof(string));
            dt.Columns.Add(TDKH.COL_TypeRow, typeof(string));
            dt.Columns.Add(TDKH.COL_RowCha, typeof(object));

            return dt;
        }

        public static DataTable GiaoViecDuAn()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add(GiaoViec.COL_Chon, typeof(object));
            dt.Columns.Add(GiaoViec.COL_STT, typeof(object));
            dt.Columns.Add(GiaoViec.COL_TenCongViec, typeof(object));
            dt.Columns.Add(GiaoViec.COL_KLKeHoach, typeof(object));
            dt.Columns.Add(GiaoViec.COL_ThucHien, typeof(object));
            dt.Columns.Add(GiaoViec.COL_PhuTrach, typeof(object));
            dt.Columns.Add(GiaoViec.COL_HoTro, typeof(object));
            dt.Columns.Add(GiaoViec.COL_CoQuanLienHe, typeof(object));
            dt.Columns.Add(GiaoViec.COL_NguoiLienHe, typeof(object));
            dt.Columns.Add(GiaoViec.COL_NgayBatDauThiCong, typeof(DateTime));
            dt.Columns.Add(GiaoViec.COL_NgayKetThucThiCong, typeof(DateTime));
            dt.Columns.Add(GiaoViec.COL_SoNgay, typeof(object));
            dt.Columns.Add(GiaoViec.COL_NguoiThucHien, typeof(object));
            dt.Columns.Add(GiaoViec.COL_NoiDungThucHien, typeof(object));
            dt.Columns.Add(GiaoViec.COL_TrangThai, typeof(object));
            dt.Columns.Add(GiaoViec.COL_FileDinhKem, typeof(object));
            dt.Columns.Add(GiaoViec.COL_DonGiaKeHoach, typeof(object));
            dt.Columns.Add(GiaoViec.COL_DonGiaThiCong, typeof(object));
            dt.Columns.Add(GiaoViec.COL_ThanhTienKeHoach, typeof(object));
            dt.Columns.Add(GiaoViec.COL_LyTrinhCaoDo, typeof(object));
            dt.Columns.Add(GiaoViec.COL_CapDoQuanTrong, typeof(object));
            dt.Columns.Add(GiaoViec.COL_QuyTrinhThucHien, typeof(object));
            dt.Columns.Add(GiaoViec.COL_GhiNhatKy, typeof(object));
            dt.Columns.Add(GiaoViec.COL_NhomKy, typeof(object));
            dt.Columns.Add(GiaoViec.COL_LinkHop, typeof(object));
            dt.Columns.Add(GiaoViec.COL_GhiChu, typeof(object));
            dt.Columns.Add(GiaoViec.COL_TypeRow, typeof(object));
            dt.Columns.Add(GiaoViec.COL_RowCha, typeof(object));

            return dt;
        }

        public static DataTable BaoCaoBieu2A_BDH()
        {
            DataTable dt = new DataTable();
            
            dt.Columns.Add(TDKH.COL_STT, typeof(object));
            dt.Columns.Add(TDKH.COL_DanhMucCongTac, typeof(object));
            dt.Columns.Add(TDKH.COL_DonVi, typeof(object));
            dt.Columns.Add(TDKH.COL_KhoiLuongHopDongChiTiet, typeof(object));
            dt.Columns.Add(TDKH.COL_NgayBatDau, typeof(object));
            dt.Columns.Add(TDKH.COL_NgayKetThuc, typeof(object));
            dt.Columns.Add(TDKH.COL_LuyKeKyTruoc, typeof(object));
            dt.Columns.Add(TDKH.COL_ThucHienKyNay, typeof(object));
            dt.Columns.Add(TDKH.COL_KhoiLuongDaThiCong, typeof(object));
            dt.Columns.Add(TDKH.COL_KeHoachKyNay, typeof(object));
            dt.Columns.Add(TDKH.COL_PhanTramThucHien, typeof(object));
            dt.Columns.Add(TDKH.COL_KhoiLuongConLai, typeof(object));
            dt.Columns.Add(TDKH.COL_KeHoachKySau, typeof(object));
            dt.Columns.Add(TDKH.COL_TypeRow, typeof(object));

            return dt;
        }
        
        public static DataTable BaoCaoBieu1_BDH()
        {
            DataTable dt = new DataTable();
            
            dt.Columns.Add(TDKH.COL_TypeRow, typeof(object));
            dt.Columns.Add(TDKH.COL_CodeGop, typeof(object));
            dt.Columns.Add(TDKH.COL_Code, typeof(object));
            dt.Columns.Add(TDKH.COL_STT, typeof(object));
            dt.Columns.Add(TDKH.COL_DanhMucCongTac, typeof(string));
            dt.Columns.Add(TDKH.COL_DonVi, typeof(object));
            dt.Columns.Add(TDKH.COL_GiaTriHopDongWithCPDP, typeof(object));
            dt.Columns.Add(TDKH.COL_GiaTriHopDongWithoutCPDP, typeof(object));
            dt.Columns.Add(TDKH.COL_LuyKeHetNamTruoc, typeof(object));
            dt.Columns.Add(TDKH.COL_LuyKeKyTruoc, typeof(object));
            dt.Columns.Add(TDKH.COL_ThucHienKyNay, typeof(object));
            dt.Columns.Add(TDKH.COL_KLThanhToan, typeof(object));
            dt.Columns.Add(TDKH.COL_LuyKeTuDauNam, typeof(object));
            dt.Columns.Add(TDKH.COL_KhoiLuongDaThiCong, typeof(object));
            dt.Columns.Add(TDKH.COL_KeHoachKySau, typeof(object));
            dt.Columns.Add(TDKH.COL_KeHoachNamNay, typeof(object));                                
            dt.Columns.Add($"NT_{TDKH.COL_LuyKeHetNamTruoc}", typeof(object));
            dt.Columns.Add($"NT_{TDKH.COL_LuyKeKyTruoc}", typeof(object));
            dt.Columns.Add($"NT_{TDKH.COL_KLThanhToan}", typeof(object));
            dt.Columns.Add($"NT_{TDKH.COL_LuyKeTuDauNam}", typeof(object));
            dt.Columns.Add($"NT_{TDKH.COL_KhoiLuongDaThiCong}", typeof(object));
            dt.Columns.Add($"NT_{TDKH.COL_KeHoachKySau}", typeof(object));
            dt.Columns.Add($"NT_{TDKH.COL_KeHoachNamNay}", typeof(object));                
            dt.Columns.Add($"TT_{TDKH.COL_LuyKeHetNamTruoc}", typeof(object));
            dt.Columns.Add($"TT_{TDKH.COL_LuyKeKyTruoc}", typeof(object));
            dt.Columns.Add($"TT_{TDKH.COL_KLThanhToan}", typeof(object));
            dt.Columns.Add($"TT_{TDKH.COL_LuyKeTuDauNam}", typeof(object));
            dt.Columns.Add($"TT_{TDKH.COL_KhoiLuongDaThiCong}", typeof(object));
            dt.Columns.Add($"TT_{TDKH.COL_KeHoachKySau}", typeof(object));
            dt.Columns.Add($"TT_{TDKH.COL_KeHoachNamNay}", typeof(object));
            dt.Columns.Add(TDKH.COL_DangDo, typeof(object));
            dt.Columns.Add(TDKH.COL_RowCha, typeof(object));


            return dt;
        }

        public static DataTable BaoCaoToanDuAnOnline()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add(TDKH.COL_Code, typeof(object));;
            dt.Columns.Add(TDKH.COL_TypeRow, typeof(object));
            dt.Columns.Add(TDKH.COL_STT, typeof(object));
            dt.Columns.Add(TDKH.COL_DanhMucCongTac, typeof(object));
            dt.Columns.Add(TDKH.COL_KhoiLuongHopDongChiTiet, typeof(object));
            dt.Columns.Add(TDKH.COL_GTSXTrongKy, typeof(object));
            dt.Columns.Add(TDKH.COL_GTSXTrongThang, typeof(object));
            dt.Columns.Add(TDKH.COL_GTSXTrongNam, typeof(object));
            dt.Columns.Add(TDKH.COL_GTSXDaThiCong, typeof(object));

            return dt;
        }

        public static DataTable BaoCaoNgayOnline()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(TDKH.COL_TypeRow, typeof(object));
            dt.Columns.Add(TDKH.COL_STT, typeof(object));
            dt.Columns.Add(TDKH.COL_DanhMucCongTac, typeof(object));
            dt.Columns.Add(TDKH.COL_GiaTriHopDong, typeof(object));
            dt.Columns.Add(TDKH.COL_GTSXTrongKy, typeof(object));
            dt.Columns.Add(TDKH.COL_GTSXFromBeginOfPrj, typeof(object));
            dt.Columns.Add(TDKH.COL_GTSXTyLe, typeof(object));
            dt.Columns.Add(TDKH.COL_GTNTTrongKy, typeof(object));
            dt.Columns.Add(TDKH.COL_GTNTFromBeginOfPrj, typeof(object));
            dt.Columns.Add(TDKH.COL_GTDangDo, typeof(object));
            return dt;
        }


        public static DataTable BaoCaoTongHopDuKienThiCong()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(TDKH.COL_TypeRow, typeof(object));
            dt.Columns.Add(TDKH.COL_STT, typeof(object));;
            dt.Columns.Add(TDKH.COL_DanhMucCongTac, typeof(object));
            dt.Columns.Add(TDKH.COL_DBC_KhoiLuongToanBo, typeof(object));
            dt.Columns.Add(TDKH.COL_KhoiLuongGiaoKhoan, typeof(object));
            dt.Columns.Add(TDKH.COL_KhoiLuongConLai, typeof(object));
            dt.Columns.Add(TDKH.COL_NguoiDaiDien, typeof(object));

            return dt;
        }
        
        public static DataTable BaoCaoTongHopGiaTriThucHien()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(TDKH.COL_TypeRow, typeof(object));
            dt.Columns.Add(TDKH.COL_STT, typeof(object));;
            dt.Columns.Add(TDKH.COL_DanhMucCongTac, typeof(object));
            dt.Columns.Add(TDKH.COL_NguoiDaiDien, typeof(object));
            dt.Columns.Add(TDKH.COL_GiaoKhoanBanDau, typeof(object));
            dt.Columns.Add(TDKH.COL_GiaoKhoanBoSung, typeof(object));
            dt.Columns.Add(TDKH.COL_GiaoKhoanCatGiam, typeof(object));
            dt.Columns.Add(TDKH.COL_DBC_KhoiLuongToanBo, typeof(object));
            dt.Columns.Add(TDKH.COL_KhoiLuongThiCong, typeof(object));
            dt.Columns.Add(TDKH.COL_PhanTramThucHien, typeof(object));
            dt.Columns.Add(TDKH.COL_KhoiLuongConLai, typeof(object));
            dt.Columns.Add(TDKH.COL_KhoiLuongConLaiTong, typeof(object));
            dt.Columns.Add(TDKH.COL_LyDo, typeof(object));
            dt.Columns.Add(TDKH.COL_Dat, typeof(object));
            dt.Columns.Add(TDKH.COL_KhongDat, typeof(object));

            return dt;
        }
        
        public static DataTable BaoCaoTongHopHopDongThanhToan()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(TDKH.COL_TypeRow, typeof(object));
            dt.Columns.Add(TDKH.COL_STT, typeof(object));;
            dt.Columns.Add(TDKH.COL_DanhMucCongTac, typeof(object));
            dt.Columns.Add(TDKH.COL_GiaTriHopDongWithCPDP, typeof(object));
            dt.Columns.Add(TDKH.COL_GiaTriHopDongWithoutCPDP, typeof(object));
            dt.Columns.Add(TDKH.COL_KhoiLuongThiCong, typeof(object));

            return dt;
        }


        public static DataSet DanhSachMayInView(out DataTable dtMay, out DataTable dtNhienLieu, out DataTable dtDinhMuc)
        {
            dtMay = new DataTable();

            dtMay.Columns.Add("Code", typeof(string));
            dtMay.Columns.Add("STT", typeof(int));
            dtMay.Columns.Add("Ten", typeof(string));
            dtMay.Columns.Add("Picture", typeof(byte[]));
            dtMay.Columns.Add("DuAn", typeof(string));
            dtMay.Columns.Add("NguoiVanHanh", typeof(string));
            dtMay.Columns.Add("DinhMucCongViec", typeof(double));
            dtMay.Columns.Add("GhiChu", typeof(string));
            dtMay.Columns.Add("DonVi", typeof(string));
            dtMay.Columns.Add("ChuSoHuu", typeof(string));
            dtMay.Columns.Add("NhienLieuChinh", typeof(string));
            dtMay.Columns.Add("NhienLieuPhu", typeof(string));
            dtMay.Columns.Add("TrangThai", typeof(string));


            dtMay.Columns.Add("TenCaption", typeof(string));
            dtMay.Columns.Add("DuAnCaption", typeof(string));
            dtMay.Columns.Add("NguoiVanHanhCaption", typeof(string));
            dtMay.Columns.Add("DinhMucCongViecCaption", typeof(string));
            dtMay.Columns.Add("GhiChuCaption", typeof(string));
            dtMay.Columns.Add("DonViCaption", typeof(string));
            dtMay.Columns.Add("ChuSoHuuCaption", typeof(string));
            dtMay.Columns.Add("NhienLieuChinhCaption", typeof(string));
            dtMay.Columns.Add("NhienLieuPhuCaption", typeof(string));
            dtMay.Columns.Add("TrangThaiCaption", typeof(string));


            dtMay.Columns["TenCaption"].DefaultValue = "Tên:";
            dtMay.Columns["DuAnCaption"].DefaultValue = "Dự án:";
            dtMay.Columns["NguoiVanHanhCaption"].DefaultValue = "Người vận hành:";
            dtMay.Columns["DinhMucCongViecCaption"].DefaultValue = "Định mức công việc:";
            dtMay.Columns["GhiChuCaption"].DefaultValue = "Ghi chú:";
            dtMay.Columns["DonViCaption"].DefaultValue = "Đơn vị:";
            dtMay.Columns["ChuSoHuuCaption"].DefaultValue = "Chủ sở hữu:";
            dtMay.Columns["NhienLieuChinhCaption"].DefaultValue = "Nhiên liệu chính:";
            dtMay.Columns["NhienLieuPhuCaption"].DefaultValue = "Nhiên liệu phụ:";
            dtMay.Columns["TrangThaiCaption"].DefaultValue = "Trạng thái:";

            dtNhienLieu = new DataTable();
            dtNhienLieu.Columns.Add("Code", typeof(string));
            dtNhienLieu.Columns.Add("CodeMay", typeof(string));
            dtNhienLieu.Columns.Add("LoaiNhienLieu", typeof(string));
            dtNhienLieu.Columns.Add("STT", typeof(int));
            dtNhienLieu.Columns.Add("Ten", typeof(string));
            dtNhienLieu.Columns.Add("DonVi", typeof(string));
            dtNhienLieu.Columns.Add("MucTieuThu", typeof(double));
            
            
            dtDinhMuc = new DataTable();
            dtDinhMuc.Columns.Add("STT", typeof(int));
            dtDinhMuc.Columns.Add("Code", typeof(string));
            dtDinhMuc.Columns.Add("CodeMay", typeof(string));
            dtDinhMuc.Columns.Add("DinhMucCongViec", typeof(string));
            dtDinhMuc.Columns.Add("DonVi", typeof(string));
            dtDinhMuc.Columns.Add("MucTieuThu", typeof(string));
            dtDinhMuc.Columns.Add("GhiChu", typeof(string));

            //dtDinhMuc.Columns.Add("LoaiNhienLieu", typeof(double));

            DataSet dataSet = new DataSet();
            dataSet.Tables.Add(dtMay);
            dataSet.Tables.Add(dtNhienLieu);
            dataSet.Tables.Add(dtDinhMuc);

            dataSet.Relations.Add("Nhiên liệu", dtMay.Columns["Code"], dtNhienLieu.Columns["CodeMay"]);
            dataSet.Relations.Add("Định mức", dtMay.Columns["Code"], dtDinhMuc.Columns["CodeMay"]);
            return dataSet;
        }
        public static DataTable KhoiLuongHangNgay()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(TDKH.COL_STT, typeof(string));
            dt.Columns.Add(TDKH.COL_STTDocVao, typeof(string));
            dt.Columns.Add(TDKH.COL_STTND, typeof(string));
            dt.Columns.Add(TDKH.COL_MaHieuCongTac, typeof(string));
            dt.Columns.Add(TDKH.COL_DanhMucCongTac, typeof(string));
            dt.Columns.Add(TDKH.COL_DonVi, typeof(string));
            dt.Columns.Add(TDKH.COL_DBC_KhoiLuongToanBo, typeof(double));
            dt.Columns.Add(TDKH.COL_KhoiLuongDaThiCong, typeof(double));

            dt.Columns.Add(TDKH.COL_DonGia, typeof(double));
            dt.Columns.Add(TDKH.COL_DonGiaThiCong, typeof(double));
            dt.Columns.Add(TDKH.COL_NgayBatDau, typeof(object));
            dt.Columns.Add(TDKH.COL_NgayKetThuc, typeof(object));
            dt.Columns.Add(TDKH.COL_SoNgayThucHien, typeof(object));
            dt.Columns.Add(TDKH.COL_NgayBatDauThiCong, typeof(object));
            dt.Columns.Add(TDKH.COL_NgayKetThucThiCong, typeof(object));
            dt.Columns.Add(TDKH.COL_SoNgayThiCong, typeof(object));
            dt.Columns.Add(TDKH.COL_TrangThai, typeof(string));
            dt.Columns.Add(TDKH.COL_NhanCong, typeof(object));
            dt.Columns.Add(TDKH.COL_KinhPhiDuKien, typeof(object));
            dt.Columns.Add(TDKH.COL_GiaTri, typeof(object));


            dt.Columns.Add(TDKH.COL_GiaTriThiCong, typeof(object));
            dt.Columns.Add(TDKH.COL_LyTrinhCaoDo, typeof(string));
            dt.Columns.Add(TDKH.COL_GhiChu, typeof(string));

            dt.Columns.Add(TDKH.COL_RowCha, typeof(object));
            dt.Columns.Add(TDKH.COL_Code, typeof(string));
            dt.Columns.Add(TDKH.COL_NhanCongIsCongThucMacDinh, typeof(bool));
            dt.Columns.Add(TDKH.COL_KinhPhiDuKienIsCongThucMacDinh, typeof(bool));
            dt.Columns.Add(TDKH.COL_TypeRow, typeof(string));


            return dt;
        } 
        public static DataTable TienDoNew()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(TDKH.COL_RowCha, typeof(object));
            dt.Columns.Add(TDKH.COL_TypeRow, typeof(string));
            dt.Columns.Add("LeftText", typeof(string));
            dt.Columns.Add("Col_RightText", typeof(string));
            dt.Columns.Add("RightText", typeof(string));
            dt.Columns.Add("RangeFill", typeof(string));
            dt.Columns.Add(TDKH.COL_STT, typeof(string));
            dt.Columns.Add(TDKH.COL_DanhMucCongTac, typeof(string));
            dt.Columns.Add(TDKH.COL_NgayBatDau, typeof(object));
            dt.Columns.Add(TDKH.COL_SoNgayThucHien, typeof(object));
            dt.Columns.Add(TDKH.COL_NgayKetThuc, typeof(object)); 
            dt.Columns.Add(TDKH.COL_DonVi, typeof(string));
            dt.Columns.Add(TDKH.COL_DBC_KhoiLuongToanBo, typeof(object));
            dt.Columns.Add(TDKH.COL_GhiChu, typeof(string));
            return dt;
        }
    }
}
