
WITH TempDt AS
(
	
	SELECT Code, CodeDuAn, CodeCongTrinh, CodeHangMuc, CodeNhaThau, 
	CodeNhaThauPhu, CodeToDoi, TenDuAn, TenCongTrinh, TenHangMuc, TenCongTac, AutoDivision, IsAutoSynthetic, NgayBatDau, NgayKetThuc, KhoiLuongToanBo, DonGia, DonGiaThiCong
	FROM view_CongTacWithHangMucCongTrinh ct
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
CASE WHEN AutoDivision = 0 THEN 0 ELSE (ct.KhoiLuongToanBo - COALESCE(SUM(hn.KhoiLuongKeHoach), 0))/NULLIF(julianday(ct.NgayKetThuc) - julianday(ct.NgayBatDau) + 1 - COUNT(CASE WHEN hn.KhoiLuongKeHoach IS NOT NULL AND hn.Ngay BETWEEN ct.NgayBatDau AND ct.NgayKetThuc THEN hn.Code END), 0) END AS AverageKhoiLuongKeHoach,
CASE WHEN AutoDivision = 0 THEN 0 ELSE  MAX(julianday(MIN(d.DateValue, ct.NgayKetThuc)) - julianday(ct.NgayBatDau) + 1 - COUNT(CASE WHEN hn.Ngay <= DateValue AND hn.KhoiLuongKeHoach IS NOT NULL THEN hn.Ngay END), 0) END AS NumberOfAutoDate,
SUM(CASE WHEN DateValue = hn.Ngay THEN hn.KhoiLuongKeHoach END) AS KhoiLuongKeHoach,
SUM(CASE WHEN DateValue = hn.Ngay THEN hn.KhoiLuongThiCong END) AS KhoiLuongThiCong
FROM TempDt ct 
LEFT JOIN dates d ON  AutoDivision = '1' AND DateValue >=ct.NgayBatDau AND DateValue<=ct.NgayKetThuc --DateCondition
LEFT JOIN Tbl_TDKH_KhoiLuongCongViecTungNgay hn ON hn.CodeCongTacTheoGiaiDoan = ct.Code AND Ngay = DateValue
GROUP BY DateValue, ct.Code, AutoDivision, ct.KhoiLuongToanBo, ct.NgayBatDau, ct.NgayKetThuc
UNION ALL
SELECT Ngay AS DateValue, ct.Code,
0 AS AverageKhoiLuongKeHoach,
0 AS NumberOfAutoDate,
hn.KhoiLuongKeHoach,
hn.KhoiLuongThiCong
FROM TempDt ct 
JOIN Tbl_TDKH_KhoiLuongCongViecTungNgay hn ON hn.CodeCongTacTheoGiaiDoan = ct.Code AND Ngay < NgayBatDau AND Ngay IN (SELECT * FROM dates)  --DateCondition
GROUP BY ct.Code, AutoDivision, ct.KhoiLuongToanBo, ct.NgayBatDau, ct.NgayKetThuc

UNION ALL
SELECT Ngay AS DateValue, ct.Code,
0 AS AverageKhoiLuongKeHoach,
0 AS NumberOfAutoDate,
hn.KhoiLuongKeHoach,
hn.KhoiLuongThiCong
FROM TempDt ct 
JOIN Tbl_TDKH_KhoiLuongCongViecTungNgay hn ON hn.CodeCongTacTheoGiaiDoan = ct.Code AND Ngay > NgayKetThuc AND Ngay IN (SELECT * FROM dates)  --DateCondition
GROUP BY ct.Code, AutoDivision, ct.KhoiLuongToanBo, ct.NgayBatDau, ct.NgayKetThuc
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
JOIN Tbl_TDKH_KhoiLuongCongViecTungNgay hn
ON ct.Code = hn.CodeCongTacTheoGiaiDoan AND c.DateValue IS NOT NULL AND hn.Ngay = c.DateValue
GROUP BY ct.CodeGiaoThau, c.DateValue
UNION ALL
SELECT ct.CodeGiaoThau AS Code,hn.Ngay,
hn.KhoiLuongThiCong, 
hn.KhoiLuongKeHoach
FROM ctacs c
JOIN view_CongTacKeHoachThiCong ct
ON (c.CodeNhaThau IS NOT NULL AND c.Code = ct.CodeGiaoThau)
LEFT JOIN Tbl_TDKH_KhoiLuongCongViecTungNgay hn
ON ct.Code = hn.CodeCongTacTheoGiaiDoan AND c.DateValue IS NULL
GROUP BY ct.CodeGiaoThau, hn.Ngay
),
KLS AS
(
SELECT ct.Code AS ParentCode, ct.DateValue AS Ngay, ct.DonGia, ct.DonGiaThiCong,
ct.CodeDuAn, ct.TenDuAn, ct.CodeCongTrinh, ct.TenCongTrinh, ct.CodeHangMuc, ct.TenHangMuc,
COALESCE(tc.KhoiLuongThiCong, ct.KhoiLuongThiCong) AS KhoiLuongThiCong, 
CASE WHEN ct.AutoDivision = 0 AND ct.IsAutoSynthetic = 1 THEN tc.KhoiLuongKeHoach 
ELSE CASE WHEN ct.DateValue BETWEEN NgayBatDau AND NgayKetThuc THEN
COALESCE(ct.KhoiLuongKeHoach, ct.AverageKhoiLuongKeHoach) ELSE 0 END END AS KhoiLuongKeHoach
FROM ctacs ct
LEFT JOIN tempTC tc ON ct.Code = tc.Code AND ct.DateValue IS NOT NULL AND ct.DateValue = tc.DateValue

UNION ALL

SELECT ct.Code AS ParentCode, tc.DateValue AS Ngay, ct.DonGia, ct.DonGiaThiCong,
ct.CodeDuAn, ct.TenDuAn, ct.CodeCongTrinh, ct.TenCongTrinh, ct.CodeHangMuc, ct.TenHangMuc,
COALESCE(tc.KhoiLuongThiCong, ct.KhoiLuongThiCong) AS KhoiLuongThiCong, 
CASE WHEN ct.AutoDivision = 0 AND ct.IsAutoSynthetic = 1 THEN tc.KhoiLuongKeHoach 
ELSE CASE WHEN ct.DateValue BETWEEN NgayBatDau AND NgayKetThuc THEN
COALESCE(ct.KhoiLuongKeHoach, ct.AverageKhoiLuongKeHoach) ELSE 0 END END AS KhoiLuongKeHoach
FROM ctacs ct
JOIN tempTC tc ON ct.Code = tc.Code AND ct.DateValue IS NULL AND tc.DateValue IS NOT NULL
)
SELECT ParentCode, Ngay,
KhoiLuongKeHoach, KhoiLuongThiCong,
ct.CodeDuAn, ct.TenDuAn, ct.CodeCongTrinh, ct.TenCongTrinh, ct.CodeHangMuc, ct.TenHangMuc,
KhoiLuongKeHoach*DonGia AS ThanhTienKeHoach,
KhoiLuongThiCong*DonGia AS ThanhTienThiCong
FROM KLS ct