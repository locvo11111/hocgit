using DevExpress.Skins;
using DevExpress.Utils;
using DevExpress.Utils.Drawing;
using DevExpress.Utils.Svg;
using DevExpress.XtraEditors.Controls;
using PhanMemQuanLyThiCong.Model;
using PhanMemQuanLyThiCong.Model.TDKH;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows.Forms;
using System.Xml.Serialization;
using VChatCore.Model;

namespace PhanMemQuanLyThiCong.KanbanModule {
    public class KanbanHelper {
        public static BindingList<TaskRecord> LoadTasks() {
            string file = FilesHelper.FindingFileName(Application.StartupPath, "Data\\KanbanModuleData\\KanbanTasks.xml");
            var _tasks = new BindingList<TaskRecord>();
            using(var reader = new StreamReader(file)) {
                XmlSerializer deserializer = new XmlSerializer(typeof(TaskList), new XmlRootAttribute("DocumentElement"));
                var taskList = (TaskList)deserializer.Deserialize(reader);
                _tasks = taskList.List;
            }
            LoadImages(_tasks);
            return _tasks;
        }
        static void LoadImages(BindingList<TaskRecord> taskList) {
            //foreach(var task in taskList) {
            //    if(!String.IsNullOrEmpty(task.ImagePath)) {
            //        string file = FilesHelper.FindingFileName(Application.StartupPath, "Data\\KanbanModuleData\\Images\\" + task.ImagePath, false);
            //        if(File.Exists(file))
            //            task.AttachedImage = Image.FromFile(file);
            //    }
            //}
        }
        public static DataTable LoadChecklist() {
            string file = FilesHelper.FindingFileName(Application.StartupPath, "Data\\KanbanModuleData\\KanbanChecklist.xml");
            var checklist = new DataTable();
            checklist.TableName = "TaskChecklist";
            checklist.Columns.Add("TaskID", typeof(Guid));
            checklist.Columns.Add("Caption", typeof(String));
            checklist.Columns.Add("Checked", typeof(Boolean));
            if(!String.IsNullOrEmpty(file) && System.IO.File.Exists(file)) {
                checklist.ReadXml(file);
            }
            return checklist;
        }
        public static DataTable LoadMembers() {
            string file = FilesHelper.FindingFileName(Application.StartupPath, "Data\\KanbanModuleData\\KanbanMembers.xml");
            var members = new DataTable();
            members.TableName = "TaskMembers";
            members.Columns.Add("TaskID", typeof(Guid));
            members.Columns.Add("MemberID", typeof(Int32));
            if(!String.IsNullOrEmpty(file) && System.IO.File.Exists(file)) {
                members.ReadXml(file);
            }
            return members;
        }
        public static DataTable LoadEmployees() { 
            string DBFileName = string.Empty;
            string connectionString = string.Empty;
            DBFileName = FilesHelper.FindingFileName(Application.StartupPath, "Data\\nwind.xml");
            if(String.IsNullOrEmpty(DBFileName))
                return null;
            DataSet ds = new DataSet();
            ds.ReadXml(DBFileName);
            var table = ds.Tables["Employees"];
            table.PrimaryKey = new DataColumn[] { table.Columns["EmployeeID"] };
            return table;
        }
        //public static void UpdateMembersGlyph(List<User> employees, ISkinProvider skinProvider, int size) {
        //    for(int i = 0; i < employees.Count(); i++) {
        //        User user = employees[i];
        //        char a = user.FullName.Split(' ').First().First();
        //        char b = user.FullName.Split(' ').Last().First();
        //        string text = string.Format("{0}{1}", a, b);
        //        byte[] glyphbytes;

        //        Bitmap glyph = GlyphPainter.CreateRoundedStubGlyph(skinProvider, new Size(size, size), text);
        //        using(MemoryStream ms = new MemoryStream()) {
        //            glyph.Save(ms, ImageFormat.Png);
        //            glyphbytes = ms.ToArray();
        //            ms.Close();
        //        }
        //        user.Avatar = glyphbytes;
        //    }
        //}

        public static Image CreateFullnameGlyph(string FullName, ISkinProvider skinProvider, int size)
        {

            //var photoBytes = 
                    Image image = null;

                    char a = (FullName).Split(' ').First().First();
                    char b = (FullName).Split(' ').Last().First();
                    string text = string.Format("{0}{1}", a, b);
                    image = (Image)GlyphPainter.CreateRoundedStubGlyph(skinProvider, new Size(size, size), text);
 
            //image = Image.FromStream(stream);
            //stream.Close();
            //}
            return image;
            //user.AvatarArr = stream.ToArray();

        }

        public static Image CreateMemberGlyph(AppUserViewModel user, ISkinProvider skinProvider, int size)
        {

            //var photoBytes = 
            Image image= null;
            //using (MemoryStream stream = new MemoryStream())
            //{
                using (WebClient client = new WebClient())
                {
                    try
                    {
                        if (user.Avatar is null)
                            throw new ArgumentException();
                        string url = $"{CusHttpClient.InstanceTBT.BaseAddress}img?key={user.Avatar}";

                        image = Image.FromStream(client.OpenRead(url));

                        //return stream;
                    }
                    catch (Exception ex) 
                    {
                        Debug.WriteLine(ex.Message.ToUpper() );
                        char a = (user.FullName.Trim()).Split(' ').First().First();
                        char b = (user.FullName.Trim()).Split(' ').Last().First();
                        string text = string.Format("{0}{1}", a, b);
                        image = (Image)GlyphPainter.CreateRoundedStubGlyph(skinProvider, new Size(size, size), text);
                        //{
                        //    glyph.Save(stream, ImageFormat.Png);
                            
                        //}
                    }
                }
                //image = Image.FromStream(stream);
                //stream.Close();
            //}
            return image;
            //user.AvatarArr = stream.ToArray();
            
        }

        public static TaskRecord CreateNewTask() {
            return new TaskRecord();
        }
    }

    public enum TaskStep { Planned, Doing, Done }
    public enum TaskLabel 
    {
        [Display(Name = "Không nhãn")]
        None,
        [Display(Name = "Khẩn cấp")]
        Red,
        [Display(Name = "Nhãn vàng")]
        Yellow,
        [Display(Name = "Nhãn xanh")]
        Green
    }

    

    [XmlRoot("DocumentElement")]
    public class TaskList {
        public TaskList() {
            List = new BindingList<TaskRecord>();
        }
        [XmlElement("Tasks")]
        public BindingList<TaskRecord> List { get; set; }
    }
}
