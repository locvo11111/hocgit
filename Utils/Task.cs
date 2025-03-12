using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;

namespace DevExpress.XtraGantt.Demos {
    public class TaskDataItem {
        public TaskDataItem() {
            Predecessors = new List<string>();
        }
        public string ParentUID { get; set; }
        public DateTime StartDate { get; set; }
        public TimeSpan Duration { get; set; }
        public DateTime FinishDate { get; set; }
        public DateTime? BaselineStartDate { get; set; }
        public DateTime? BaselineFinishDate { get; set; }
        public string Name { get; set; }
        public string UID { get; set; }
        public List<string> Predecessors { get; set; }
        public string Resources { get; set; }
        public double Progress { get; set; }
        public int ConstraintType { get; set; }
        public DateTime? ConstraintDate { get; set; }
        public TaskDataItem(string uid, string parentUID, string name, string startDate, string duration, string finishDate, string baselineStartDate, string baselineFinishDate, double progress, List<string> predecessors, string resources) {
            UID = uid;
            ParentUID = parentUID;
            Name = name;
            StartDate = ParseDateTime(startDate).Value;
            FinishDate = ParseDateTime(finishDate).Value;
            Duration = XmlConvert.ToTimeSpan(duration);
            BaselineStartDate = ParseDateTime(baselineStartDate);
            BaselineFinishDate = ParseDateTime(baselineFinishDate);
            Progress = progress;
            Predecessors = predecessors;
            Resources = resources;
        }
        DateTime? ParseDateTime(string inputString) {
            if(string.IsNullOrEmpty(inputString))
                return null;
            DateTime dt;
            DateTime.TryParse(inputString, out dt);
            return dt;
        }
    }

    [XmlRoot("TaskBaselineOffset")]
    public class TaskItemBaseLineOffset {
        [XmlElement(Order = 1)]
        public string UID { get; set; }
        [XmlElement("StartOffset", Order = 2)]
        public long BaselineStartOffset { get; set; }
        [XmlElement("FinishOffset", Order = 3)]
        public long BaselineFinishOffset { get; set; }
    }
    [XmlRoot("TaskTimelineInfo")]
    public class TaskTimelineInfo {
        [XmlElement(Order = 1)]
        public string UID { get; set; }
        [XmlElement(Order = 2)]
        public bool DisplayAsBar { get; set; }
    }

    public class TaskSplitInfo {
        public string UID { get; set; }
        public DateTime StartDate { get; set; }
        public TimeSpan Duration { get; set; }
    }
}
