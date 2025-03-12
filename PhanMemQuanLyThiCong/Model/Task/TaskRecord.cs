//using DevExpress.XtraGrid.Demos;
using PhanMemQuanLyThiCong.Common.Helper;
using PhanMemQuanLyThiCong.KanbanModule;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using VChatCore.Model;

namespace PhanMemQuanLyThiCong.Model.TDKH
{
    public class TaskRecord
    {
        public TaskRecord()
        {
            Label = TaskLabel.None;
        }
        public bool Check { get; set; } = false;
        public Guid Code { get; set; }
        public string Caption { get; set; }//tên công tác
        public string Description { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        //public string ImagePath { get; set; }
        public List<Guid> Users { get; set; } = new List<Guid>();
        public List<SubTask> SubTasks { get; set; } = new List<SubTask>();
        public List<string> Files { get; set; } = new List<string>();
        public List<FileViewModel> FilesVM { get; set; } = new List<FileViewModel>();

        public string ProgressTC_KH
        { 
            get
            {
                
                return $"{SubTasks.Count(x => x.IsDone)}/{SubTasks.Count}";
            }
        }

        public int SoFileDinhKem
        {
            get
            {
                return FilesVM.Count;
            }
        }

        public double Progress { get; set; }
        public double ProgressCal { get { return SubTasks.Count == 0 ? 0 : Math.Round((double)SubTasks.Count(x => x.IsDone) / (double)SubTasks.Count, 2); } }

        //public Image AttachedImage { get; set; }
        [XmlIgnore]
        public TaskStep Step { get; set; } //Thực hiện đến bước nào
        [XmlIgnore]
        public TaskLabel Label { get; set; } //Nhãn tình trạng xanh đỏ vàng
        //[XmlElement("Status")]
        public int StepCore
        {
            get { return (int)Step; }
            set { Step = (TaskStep)value; }
        }
        //[XmlElement("Label")]
        public int LabelCore
        {
            get { return (int)Label; }
            set { Label = (TaskLabel)value; }
        }

        //public string labelName
        //{
        //    get { return Label.GetEnumDisplayName(); }
        //    set { Label = (TaskLabel)value; }
        //}
    }
}
