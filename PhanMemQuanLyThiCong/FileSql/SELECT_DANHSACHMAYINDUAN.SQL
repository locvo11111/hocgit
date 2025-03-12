SELECT
	m.Code,
	m.Ten 
FROM
	Tbl_MTC_DuAnInMay AS da
	INNER JOIN Tbl_MTC_DanhSachMay AS m ON da.CodeMay = m.Code 
WHERE
	da.CodeDuAn = @CodeDuAn