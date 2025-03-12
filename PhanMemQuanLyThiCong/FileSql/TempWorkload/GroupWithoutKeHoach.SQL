
WITH TempDt AS
(
	
	SELECT ct.Code, CodeDuAn, CodeCongTrinh, ct.CodeHangMuc, CodeNhaThau, 
	CodeNhaThauPhu, CodeToDoi, TenDuAn, TenCongTrinh, TenHangMuc, TenCongTac, AutoDivision, IsAutoSynthetic, ct.NgayBatDau, ct.NgayKetThuc, KhoiLuongKeHoach, ct.DonGia, ct.DonGiaThiCong
FROM Tbl_TDKH_NhomCongTac ct
JOIN view_CongTacWithHangMucCongTrinh p
ON ct.KhoiLuongKeHoach IS NOT NULL AND ct.NgayBatDau IS NOT NULL AND ct.NgayKetThuc IS NOT NULL AND p.CodeNhom = ct.Code
	--Condition
GROUP BY ct.Code
 ),

TempTC AS
(
SELECT ct.Code,
TOTAL(hn.KhoiLuongThiCong) AS KhoiLuongThiCong,
   hn.Ngay
   FROM TempDt ct
   JOIN Tbl_TDKH_NhomCongTac cttc
   ON ct.CodeNhaThau IS NOT NULL AND ct.Code = cttc.CodeNhomGiaoThau
   JOIN Tbl_TDKH_KhoiLuongCongViecTungNgay hn
   ON cttc.Code = hn.CodeNhom AND KhoiLuongThiCong IS NOT NULL --DateCondition
   GROUP BY ct.Code, hn.Ngay
   UNION ALL
   SELECT ct.Code,
hn.KhoiLuongThiCong,
   hn.Ngay
   FROM TempDt ct
   JOIN Tbl_TDKH_KhoiLuongCongViecTungNgay hn
   ON ct.CodeNhaThau IS NULL AND ct.Code = hn.CodeNhom AND KhoiLuongThiCong IS NOT NULL --DateCondition

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