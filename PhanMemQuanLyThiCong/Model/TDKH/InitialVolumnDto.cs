using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhanMemQuanLyThiCong.Model.TDKH
{
    public class InitialVolumnDto
    {
        public  Guid Id { get; set; }
        public  Guid ProjectId { get; set; }
        public  DateTime From{ get; set; }
        public  DateTime To{ get; set; }
        public  double? Volume { get; set; }
        public string ProjectName { get; set; }
        public StateEnum State { get; set; }

    }
    public enum StateEnum
    {
        DOING,
        PLANNING
    }
}
