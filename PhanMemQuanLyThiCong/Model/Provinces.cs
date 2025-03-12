using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhanMemQuanLyThiCong.Model
{
    public class Provinces
    {
        public int id { get; set; }
        public string code { get; set; }
        public string name { get; set; }
        public string TenKhongDau { get; set; }
        public bool HaveDonGia { get; set; }
    }
} 
