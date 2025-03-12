
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
 dates(DateValue) AS 
(
SELECT * FROM 
(
VALUES --Dates
) 
),
ctacsAll AS 
(
SELECT DateValue, ct.Code,
 (ct.KhoiLuongKeHoach - COALESCE(SUM(hn.KhoiLuongKeHoach), 0))/NULLIF(julianday(ct.NgayKetThuc) - julianday(ct.NgayBatDau) + 1 - COUNT(CASE WHEN hn.KhoiLuongKeHoach IS NOT NULL AND hn.Ngay BETWEEN ct.NgayBatDau AND ct.NgayKetThuc THEN hn.Code END), 0) AS AverageKhoiLuongKeHoach,
MAX(julianday(MIN(d.DateValue, ct.NgayKetThuc)) - julianday(ct.NgayBatDau) + 1 - COUNT(CASE WHEN hn.Ngay <= DateValue AND hn.KhoiLuongKeHoach IS NOT NULL THEN hn.Ngay END), 0) AS NumberOfAutoDate,
SUM(CASE WHEN DateValue = hn.Ngay THEN hn.KhoiLuongKeHoach END) AS KhoiLuongKeHoach,
SUM(CASE WHEN DateValue = hn.Ngay THEN hn.KhoiLuongThiCong END) AS KhoiLuongThiCong
FROM TempDt ct 
LEFT JOIN dates d ON  DateValue >=ct.NgayBatDau AND DateValue<=ct.NgayKetThuc --DateCondition
LEFT JOIN Tbl_TDKH_KHVT_KhoiLuongHangNgay hn ON hn.CodeVatTu = ct.Code AND Ngay = DateValue
GROUP BY DateValue, ct.Code, AutoDivision, ct.KhoiLuongKeHoach, ct.NgayBatDau, ct.NgayKetThuc
),
ctacs AS
(
SELECT ct.Code, ct.DateValue, ct.AverageKhoiLuongKeHoach, ct.NumberOfAutoDate, 
v.NgayBatDau, v.NgayKetThuc, v.CodeNhaThau, v.CodeNhaThauPhu, v.CodeToDoi,
v.CodeDuAn, v.TenDuAn,
v.CodeCongTrinh, v.TenCongTrinh,
v.CodeHangMuc, v.TenHangMuc, v.DonGia, v.DonGiaThiCong, 
v.AutoDivision, v.IsAutoSynthetic,
ct.KhoiLuongKeHoach, ct.KhoiLuongThiCong
FROM ctacsAll ct
JOIN TempDt v
ON ct.Code = v.Code
)
,
tempTC AS
(
SELECT ct.CodeGiaoThau AS Code,c.DateValue,
hn.KhoiLuongThiCong, 
hn.KhoiLuongKeHoach
FROM ctacs c
JOIN view_CongTacKeHoachThiCong ct
ON (c.CodeNhaThau IS NOT NULL AND c.Code = ct.CodeGiaoThau)
JOIN Tbl_TDKH_KHVT_KhoiLuongHangNgay hn
ON ct.Code = hn.CodeVatTu AND c.DateValue IS NOT NULL AND hn.Ngay = c.DateValue
GROUP BY ct.CodeGiaoThau, c.DateValue
UNION ALL
SELECT ct.CodeGiaoThau AS Code,hn.Ngay,
hn.KhoiLuongThiCong, 
hn.KhoiLuongKeHoach
FROM ctacs c
JOIN view_CongTacKeHoachThiCong ct
ON (c.CodeNhaThau IS NOT NULL AND c.Code = ct.CodeGiaoThau)
LEFT JOIN Tbl_TDKH_KHVT_KhoiLuongHangNgay hn
ON ct.Code = hn.CodeVatTu AND c.DateValue IS NULL
GROUP BY ct.CodeGiaoThau, hn.Ngay
),
KLS AS
(
SELECT ct.Code AS ParentCode, ct.DateValue AS Ngay, ct.DonGia, ct.DonGiaThiCong,
ct.CodeDuAn, ct.TenDuAn, ct.CodeCongTrinh, ct.TenCongTrinh, ct.CodeHangMuc, ct.TenHangMuc,
COALESCE(tc.KhoiLuongThiCong, ct.KhoiLuongThiCong) AS KhoiLuongThiCong, 
CASE WHEN ct.DateValue BETWEEN NgayBatDau AND NgayKetThuc THEN
COALESCE(ct.KhoiLuongKeHoach, ct.AverageKhoiLuongKeHoach) ELSE 0 END AS KhoiLuongKeHoach
FROM ctacs ct
LEFT JOIN tempTC tc ON ct.Code = tc.Code AND ct.DateValue IS NOT NULL AND ct.DateValue = tc.DateValue

UNION ALL

SELECT ct.Code AS ParentCode, tc.DateValue AS Ngay, ct.DonGia, ct.DonGiaThiCong,
ct.CodeDuAn, ct.TenDuAn, ct.CodeCongTrinh, ct.TenCongTrinh, ct.CodeHangMuc, ct.TenHangMuc,
COALESCE(tc.KhoiLuongThiCong, ct.KhoiLuongThiCong) AS KhoiLuongThiCong, 
CASE WHEN ct.DateValue BETWEEN NgayBatDau AND NgayKetThuc THEN
COALESCE(ct.KhoiLuongKeHoach, ct.AverageKhoiLuongKeHoach) ELSE 0 END AS KhoiLuongKeHoach
FROM ctacs ct
JOIN tempTC tc ON ct.Code = tc.Code AND ct.DateValue IS NULL AND tc.DateValue IS NOT NULL
)
SELECT ParentCode, Ngay,
KhoiLuongKeHoach, KhoiLuongThiCong,
ct.CodeDuAn, ct.TenDuAn, ct.CodeCongTrinh, ct.TenCongTrinh, ct.CodeHangMuc, ct.TenHangMuc,
KhoiLuongKeHoach*DonGia AS ThanhTienKeHoach,
KhoiLuongThiCong*DonGia AS ThanhTienThiCong
FROM KLS ct