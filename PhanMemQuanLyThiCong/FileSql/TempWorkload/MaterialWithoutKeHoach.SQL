
WITH TempDt AS
(
	
		SELECT ct.Code, CodeDuAn, CodeCongTrinh, CodeHangMuc, CodeNhaThau, 
	CodeNhaThauPhu, CodeToDoi, TenDuAn, ctr.Ten AS TenCongTrinh, hm.Ten AS TenHangMuc, ct.VatTu AS TenCongTac, 1 AS  AutoDivision, IsAutoSynthetic, ct.NgayBatDau, ct.NgayKetThuc, ct.KhoiLuongKeHoach, DonGia, DonGiaThiCong
	FROM Tbl_TDKH_KHVT_VatTu ct
	JOIN Tbl_ThongTinHangMuc hm
	ON ct.CodeHangMuc = hm.Code
	JOIN Tbl_ThongTinCongTrinh ctr
	ON hm.CodeCongTrinh = ctr.Code
	JOIN Tbl_ThongTinDuAn da
	ON ctr.CodeDuAn = da.Code
	--Condition
 ),

TempTC AS
(
SELECT ct.Code,
TOTAL(hn.KhoiLuongThiCong) AS KhoiLuongThiCong,
   hn.Ngay
   FROM TempDt ct
   JOIN view_VatTuKeHoachThiCong cttc
   ON ct.CodeNhaThau IS NOT NULL AND ct.Code = cttc.CodeGiaoThau
   JOIN Tbl_TDKH_KHVT_KhoiLuongHangNgay hn
   ON cttc.Code = hn.CodeVatTu AND KhoiLuongThiCong IS NOT NULL --DateCondition
   GROUP BY ct.Code, hn.Ngay
   UNION ALL
   SELECT ct.Code,
hn.KhoiLuongThiCong,
   hn.Ngay
   FROM TempDt ct
   JOIN Tbl_TDKH_KHVT_KhoiLuongHangNgay hn
   ON ct.CodeNhaThau IS NULL AND ct.Code = hn.CodeVatTu AND KhoiLuongThiCong IS NOT NULL --DateCondition

),
KLS AS
(
SELECT ct.Code AS ParentCode, Ngay, ct.DonGia, ct.DonGiaThiCong,
ct.CodeDuAn, ct.TenDuAn, ct.CodeCongTrinh, ct.TenCongTrinh, ct.CodeHangMuc, ct.TenHangMuc,
tc.KhoiLuongThiCong
FROM TempDt ct
LEFT JOIN tempTC tc ON ct.Code = tc.Code

)
SELECT ParentCode, Ngay,
KhoiLuongThiCong,
ct.CodeDuAn, ct.TenDuAn, ct.CodeCongTrinh, ct.TenCongTrinh, ct.CodeHangMuc, ct.TenHangMuc,
KhoiLuongThiCong*DonGia AS ThanhTienThiCong
FROM KLS ct GROUP BY ParentCode, Ngay