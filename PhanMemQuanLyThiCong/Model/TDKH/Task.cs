using DevExpress.XtraGantt;
using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;

namespace PhanMemQuanLyThiCong.Model.TDKH
{
    public class TaskDataItem
    {
        public TaskDataItem()
        {
            Predecessors = new List<string>();
        }
        public string STT { get; set; }
        public string ParentUID { get; set; }
        public string NhapLienKet { get; set; }
        public string MaHieuCongTac { get; set; }
        public string DonVi { get; set; }
        public string ColCode { get; set; }
        public string CodeCongTac { get; set; }
        public string SuccessorCode { get; set; }
        public string PredecessorCode { get; set; }
        public DateTime? StartDate { get; set; }
        public TimeSpan Duration { get
            {
                if (FinishDate == null)
                    return default;
               else
                return (FinishDate - StartDate).Value; 
            } }     
        public string ThoiGianThiCong
        { get
            {
                if (FinishDate == null)
                    return "";
               else
                return (1+(FinishDate - StartDate).Value.Days).ToString(); 
            } }
        public int SoNgay
        {
            get
            {
                if (!FinishDate.HasValue)
                    return 0;
                else
                    return (FinishDate - StartDate).Value.Days;
            }
        }
        public DateTime? FinishDate { get; set; }
        public DateTime? BaselineStartDate { get; set; }
        public DateTime? BaselineFinishDate { get; set; }
        public string Name { get; set; }
        public string KhoiLuongHD { get; set; }
        public string KhoiLuongKeHoach { get; set; } = "0";
        public int TyLe { get; set; }
        public double TyLePhanTram { get; set; }//Mac dinh 95%
        //1 là tỷ lệ >100
        //2 là tỷ lệ từ TyLePhanTram-100%
        //3 là tỷ lệ <TyLePhanTram
        public void Fcn_TinhToanGiaTri()
        {
            double KLKH = double.Parse(KhoiLuongKeHoach);
            if (KLKH == 0)
                TyLe = 1;
            else if (double.TryParse(KhoiLuongHD, out double KLThiCong))
            {
                if (KLThiCong / KLKH < TyLePhanTram)
                    TyLe = 3;
                else if (KLThiCong / KLKH >= 1)
                    TyLe = 1;
                else
                    TyLe = 2;
            }
            else
                TyLe = 3;
        }
        public string KhoiLuongHDParse { get 
            { if (!string.IsNullOrEmpty(KhoiLuongHD)) return Math.Round(double.Parse(KhoiLuongHD),2).ToString(); else return string.Empty; } }
        public string KhoiLuongLK { get; set; }
        public string TLHT { get; set; }
        public string GhiChu { get; set; }
        public string UID { get; set; }
        public List<string> Predecessors { get; set; }
        public string Description { get; set; }
        public double Progress { get; set; }
        public int ConstraintType { get; set; }
        public DateTime? ConstraintDate { get; set; }
        public int TaskType { get; set; }
        public int TypeSamne { get; set; }
        public bool IsSum { get; set; } = true;
        public DependencyType DependencyType { get; set; }
        //public int FinishToStart { get; set; }
        //public int FinishToFinish { get; set; }
        //public int StartToFinish { get; set; }
        //public int StartToStart { get; set; }
        public TaskDataItem(string uid, string parentUID, string name, string startDate, string duration, string finishDate, string baselineStartDate, string baselineFinishDate, double progress, List<string> predecessors, string description)
        {
            UID = uid;
            ParentUID = parentUID;
            Name = name;
            StartDate = ParseDateTime(startDate).Value;
            FinishDate = ParseDateTime(finishDate).Value;
            //Duration = XmlConvert.ToTimeSpan(duration);
            BaselineStartDate = ParseDateTime(baselineStartDate);
            BaselineFinishDate = ParseDateTime(baselineFinishDate);
            Progress = progress;
            Predecessors = predecessors;
            Description = description;
        }
        DateTime? ParseDateTime(string inputString)
        {
            if (string.IsNullOrEmpty(inputString))
                return null;
            DateTime dt;
            DateTime.TryParse(inputString, out dt);
            return dt;
        }
    }

    [XmlRoot("TaskBaselineOffset")]
    public class TaskItemBaseLineOffset
    {
        [XmlElement(Order = 1)]
        public string UID { get; set; }
        [XmlElement("StartOffset", Order = 2)]
        public long BaselineStartOffset { get; set; }
        [XmlElement("FinishOffset", Order = 3)]
        public long BaselineFinishOffset { get; set; }
    }
    [XmlRoot("TaskTimelineInfo")]
    public class TaskTimelineInfo
    {
        [XmlElement(Order = 1)]
        public string UID { get; set; }
        [XmlElement(Order = 2)]
        public bool DisplayAsBar { get; set; }
    }

    public class TaskSplitInfo
    {
        public string UID { get; set; }
        public DateTime StartDate { get; set; }
        public TimeSpan Duration { get; set; }
    }
}
