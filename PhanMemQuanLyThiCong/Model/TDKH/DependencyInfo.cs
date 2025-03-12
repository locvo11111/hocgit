using DevExpress.XtraGantt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhanMemQuanLyThiCong.Model.TDKH
{
    internal class DependencyInfo
    {
        string PredecessorCode { get; set; }
        string SuccessorCode { get; set; }
        DependencyType DependencyType { get; set; }
        TimeSpan TimeLag { get; set; }
        public DependencyInfo(string predecessorCode, string successorCode, DependencyType dependencyType, TimeSpan time)
        {
            PredecessorCode = predecessorCode;
            SuccessorCode = successorCode;
            DependencyType = dependencyType;
            TimeLag = time;
        }
    }
}
