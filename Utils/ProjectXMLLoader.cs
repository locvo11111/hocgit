using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using DevExpress.XtraRichEdit.Internal;

namespace DevExpress.XtraGantt.Demos {
    public static class ProjectXMLLoader {
        public static XElement GetElement(this XElement element, string name) {
            return element.Element(XName.Get(name, element.GetDefaultNamespace().NamespaceName));
        }
        public static IEnumerable<XElement> GetElements(this XElement element, string name) {
            return element.Elements(XName.Get(name, element.GetDefaultNamespace().NamespaceName));
        }
        public static void TryUpdateDates(IList<TaskDataItem> tasks) {
            if(tasks != null && tasks.Count > 0) {
                DateTime actualProjectStart = CalcActualProjectStart();
                //specific for dataSource
                DateTime projectStart = tasks[0].StartDate;
                for(int i = 0; i < tasks.Count; i++) {
                    var task = tasks[i];
                    task.StartDate = actualProjectStart + TimeSpan.FromTicks(task.StartDate.Ticks - projectStart.Ticks);
                    task.FinishDate = actualProjectStart + TimeSpan.FromTicks(task.FinishDate.Ticks - projectStart.Ticks);
                    if(task.UID == "4") {
                        task.ConstraintType = (int)ConstraintType.StartNoEarlierThan;
                        task.ConstraintDate = task.StartDate + TimeSpan.FromDays(1);
                    }
                    if(task.UID == "9") {
                        task.ConstraintType = (int)ConstraintType.StartNoLaterThan;
                        task.ConstraintDate = task.StartDate;
                    }
                    if(task.UID == "14") {
                        task.ConstraintType = (int)ConstraintType.MustFinishOn;
                        task.ConstraintDate = task.FinishDate;
                    }
                    if(task.UID == "21") {
                        task.ConstraintType = (int)ConstraintType.FinishNoEarlierThan;
                        task.ConstraintDate = task.FinishDate;
                    }
                    if(task.BaselineStartDate.HasValue)
                        task.BaselineStartDate = actualProjectStart + TimeSpan.FromTicks(task.BaselineStartDate.Value.Ticks - projectStart.Ticks);
                    if(task.BaselineFinishDate.HasValue)
                        task.BaselineFinishDate = actualProjectStart + TimeSpan.FromTicks(task.BaselineFinishDate.Value.Ticks - projectStart.Ticks);
                }
                //
            }
        }
        static DateTime CalcActualProjectStart() {
            DateTime now = DateTime.Now;
            return now + TimeSpan.FromDays(4 - (int)now.DayOfWeek);
        }

        class ResourceInfo {
            public string UID { get; set; }
            public string Name { get; set; }
        }
        static string GetParentWBS(string wbs) {
            if(string.IsNullOrEmpty(wbs))
                return string.Empty;
            var wbsPaths = wbs.Split('.');
            return wbsPaths.Length == 1 ? "0" : string.Join(".", wbsPaths.Take(wbsPaths.Length - 1));
        }
        static string GetParentUID(string wbs, Dictionary<string, TaskDataItem> itemsByWBS) {
            var parentWBS = GetParentWBS(wbs);
            TaskDataItem parentItem;
            if(itemsByWBS.TryGetValue(parentWBS, out parentItem))
                return parentItem.UID;
            return null;
        }
        static IEnumerable<ResourceInfo> ReadResources(XElement projectElement) {
            return projectElement
                        .GetElement("Resources")
                        .Elements()
                        .Where(x => x.GetElement("Name") != null)
                        .Select(x => new ResourceInfo() { UID = x.GetElement("UID").Value, Name = x.GetElement("Name").Value })
                        .ToArray();
        }
        static List<string> ReadPredecessorsUID(XElement taskElement) {
            var predecessors = taskElement.GetElements("PredecessorLink");
            if(predecessors == null)
                return null;
            return predecessors
                         .Select(x => x.GetElement("PredecessorUID").Value)
                         .ToList()
                         /*.AsReadOnly()*/;
        }
        static IEnumerable<string> GetResoucesFromTask(XElement projectElement, string taskUID, IEnumerable<ResourceInfo> resources) {
            return projectElement
                        .GetElement("Assignments")
                        .Elements()
                        .Where(x => taskUID.Equals(x.GetElement("TaskUID").Value))
                        .Select(x => resources.SingleOrDefault(res => res.UID.Equals(x.GetElement("ResourceUID").Value)))
                        .Where(x => x != null && !string.IsNullOrEmpty(x.Name))
                        .Select(x => x.Name);
        }
        static TaskDataItem CreateTask(XElement projectElement, XElement taskNode, Dictionary<string, TaskDataItem> itemsByWBS, IEnumerable<ResourceInfo> resources) {
            var wbs = taskNode.GetElement("WBS").Value;
            var uid = taskNode.GetElement("UID").Value;
            var baseline = taskNode.GetElement("Baseline");
            var task = new TaskDataItem(
                uid,
                GetParentUID(wbs, itemsByWBS),
                taskNode.GetElement("Name").Value,
                taskNode.GetElement("Start").Value,
                taskNode.GetElement("Duration").Value,
                taskNode.GetElement("Finish").Value,
                baseline != null ? baseline.GetElement("Start").Value : null,
                baseline != null ? baseline.GetElement("Finish").Value : null,
                Convert.ToDouble(taskNode.GetElement("PercentComplete").Value, CultureInfo.InvariantCulture)/* / 100d*/,
                ReadPredecessorsUID(taskNode),
                string.Join(", ", GetResoucesFromTask(projectElement, uid, resources)));
            itemsByWBS.Add(wbs, task);
            return task;
        }
        static List<TaskDataItem> ReadTasks(XElement projectElement) {
            var itemsByWBS = new Dictionary<string, TaskDataItem>();
            var resources = ReadResources(projectElement);
            return projectElement
                    .GetElement("Tasks")
                    .Elements()
                    .Select(item => CreateTask(projectElement, item, itemsByWBS, resources))
                    .ToList();
        }
        public static IList<TaskDataItem> LoadModel(Stream xmlStream) {
            var root = XDocument.Load(xmlStream).Root;
            IList<TaskDataItem> tasks = ReadTasks(root);
            return tasks;
        }
    }
}
