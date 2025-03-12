using Microsoft.AspNetCore.Http;
using PhanMemQuanLyThiCong.Common.Helper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace PhanMemQuanLyThiCong.Model
{
    public class FileViewModel
    {
        public FileViewModel() { }
        public FileViewModel(string path) { FilePath = path; }
        public string FilePath { get; set; }
        public string Table { get; set; }
        public string Code { get; set; }
        public string FileNameInDb { get; set; }
        public byte[] Content { get; set; } = null;
        public string Checksum;
        public bool isEdited = true;
        public string FileName
        {
            get
            {
                return Path.GetFileName(FilePath);
            }
        }

        public string FileNameDisplay
        {
            get
            {
                return FileNameInDb ?? FileName;
            }
        }

        public string FileNameWithoutExtension
        {
            get 
            { 
                return Path.GetFileNameWithoutExtension(FileNameDisplay); 
            }
        }

        [JsonIgnore]
        public string DisplayReportName
        {
            get
            {
                return FileNameWithoutExtension.Replace("_ONLINE_", "").Replace("_OFFLINE_", "");
            }
        }
    }
}
