using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhanMemQuanLyThiCong.Model
{
    public class UnsavedProject
    {
        public int STT { get;set; }
        public string FullPath { get; set; }
        public string FileName
        {
            get
            {
                return Path.GetFileName(FullPath);
            }
        }
        public DateTime Modified { get; set; }
    }
}
