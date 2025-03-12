using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhanMemQuanLyThiCong.Model.Excel
{
    public class CategoryReadExcel
    {
        public int Id { get; set; }
        public string CategoryName { get; set; }
        public string SheetNameSoure { get; set; }
        //public virtual List<InfoReadExcel> InfoReadExcels { get; set; }
        
    }
}
