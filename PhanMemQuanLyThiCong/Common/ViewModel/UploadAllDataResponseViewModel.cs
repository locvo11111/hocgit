using PhanMemQuanLyThiCong.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhanMemQuanLyThiCong.Common.ViewModel
{
    public class UploadAllDataResponseViewModel
    {
        public List<FileViewModel> Files { get; set; }
        public List<ChangedCodeViewModel> ChangedCodes { get; set; }
        public SQLiteAllDataViewModel AllDataNeedUpdate { get; set; }
    }
}
