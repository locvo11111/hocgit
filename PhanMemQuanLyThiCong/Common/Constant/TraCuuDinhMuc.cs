using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhanMemQuanLyThiCong.Common.Constant
{
    public class TraCuuDinhMuc
    {
        public const string col_Id = "Id";
        public const string col_ParentId = "ParentId";
        public const string col_Chon = "Chon";
        public const string col_KeysMatch = "KeysMatch";
        public const string col_KeyCount = "KeyCount";
        public const string col_DMLenght = "DinhMucLenght";
        public const string col_UuTien = "UuTien";
        public const string col_ChenhLechFi = "ChenhLechfi";
        public const string col_ChenhLechCao = "ChenhLechcao";
        public const string col_ChenhLechK = "ChenhLechk";
        public const string col_ChenhLechDay = "ChenhLechday";
        public const string col_ChenhLechSau = "ChenhLechsau";
        public const string col_ChenhLechRong = "ChenhLechrong";
        public const string col_ChenhLechDamNen = "ChenhLechdamnen";

        public static string[] colsName = 
        {
            col_ChenhLechCao,
            col_ChenhLechK,
            col_ChenhLechDay,
            col_ChenhLechSau,
            col_ChenhLechRong,
            col_ChenhLechDamNen,
            col_ChenhLechFi,
        };
    }
}
