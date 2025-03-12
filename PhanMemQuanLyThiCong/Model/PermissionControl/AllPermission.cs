using DevExpress.Mvvm.POCO;
using PhanMemQuanLyThiCong.Common.Constant;
using PhanMemQuanLyThiCong.Common.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace PhanMemQuanLyThiCong.Model.PermissionControl
{
    public class AllPermission
    {
        public AllPermission() 
        {
            HaveInitProjectPermission = BaseFrom.IsFullAccess;
        }

        public bool HaveInitProjectPermission { get; set; }
        public List<string> DASOffline { get; set; } = new List<string>();
        public bool HaveCreateProjectPermission
        {
            get
            {
                return TabsInCreate.Contains(FunctionCode.EXTERNALCREATEPROJECT.GetEnumDescription());
            }
        }
        public List<PermissionInTabsViewModel> PermissionInTabs { get; set; } = new List<PermissionInTabsViewModel>();
        public List<PermissionInTasksViewModel> PermissionInTasks { get; set; } = new List<PermissionInTasksViewModel>();
        public List<UserInRoleViewModel> uirs { get; set; } = new List<UserInRoleViewModel>();
        public List<UserInProjectViewModel> uips { get; set; } = new List<UserInProjectViewModel>();
        public List<UserInContractorViewModel> uics { get; set; } = new List<UserInContractorViewModel>();

        #region List Tab In Each Command
        [JsonIgnore]
        public List<string> TabsUnHide
        {
            get
            {
                return PermissionInTabs
                    .Select(x =>  x.TabName).Distinct().ToList();
            }
        }

        [JsonIgnore]
        public List<string> TabsInView
        {
            get { return PermissionInTabs/*.Where(x => x.CmdCode == nameof(CommandCode.View))*/
                    .Select(x => x.TabName).Distinct().ToList(); }
        }        

        [JsonIgnore]
        public List<string> TabsInEdit
        {
            get { return PermissionInTabs.Where(x => x.CmdCode == nameof(CommandCode.Edit) || x.CmdCode == nameof(CommandCode.Add))
                    .Select(x => x.TabName).Distinct().ToList(); }
        }        

        [JsonIgnore]
        public List<string> TabsInDelete
        {
            get { return PermissionInTabs.Where(x => x.CmdCode == nameof(CommandCode.Delete))
                    .Select(x => x.TabName).Distinct().ToList(); }
        }        

        [JsonIgnore]
        public List<string> TabsInCreate
        {
            get { return PermissionInTabs.Where(x => x.CmdCode == nameof(CommandCode.Add))
                    .Select(x => x.TabName).Distinct().ToList(); }
        }

        [JsonIgnore]
        public List<string> TabsInApprove
        {
            get
            {
                return PermissionInTabs.Where(x => x.CmdCode == nameof(CommandCode.Approve))
                    .Select(x => x.TabName).Distinct().ToList();
            }
        }
        #endregion

        #region List Task In Each Command
        [JsonIgnore]
        public List<string> AllTask
        {
            get
            {
                return PermissionInTasks
                    .Select(x => x.TaskId).Distinct().ToList();
            }
        }


        [JsonIgnore]
        public List<string> TasksInView
        {
            get
            {
                return PermissionInTasks/*.Where(x => x.CmdCode == nameof(CommandCode.View))*/
                    .Select(x => x.TaskId).Distinct().ToList();
            }
        }
        [JsonIgnore]
        public List<string> TasksInEdit
        {
            get
            {
                return PermissionInTasks.Where(x => x.CmdCode == nameof(CommandCode.Edit))
                    .Select(x => x.TaskId).Distinct().ToList();
            }
        }

        [JsonIgnore]
        public List<string> TasksInDelete
        {
            get
            {
                return PermissionInTasks.Where(x => x.CmdCode == nameof(CommandCode.Delete))
                    .Select(x => x.TaskId).Distinct().ToList();
            }
        }

        [JsonIgnore]
        public List<string> TasksInCreate
        {
            get
            {
                return PermissionInTasks.Where(x => x.CmdCode == nameof(CommandCode.Add))
                    .Select(x => x.TaskId).Distinct().ToList();
            }
        }

        [JsonIgnore]
        public List<string> TasksInApprove
        {
            get
            {
                return PermissionInTasks.Where(x => x.CmdCode == nameof(CommandCode.Approve))
                    .Select(x => x.TaskId).Distinct().ToList();
            }
        }

        #endregion

        #region List Project
        [JsonIgnore]
        public List<string> AllProject
        {
            get
            {
                var ls = uips.Select(x => x.ProjectId).Distinct().ToList();
                ls.AddRange(DASOffline);
                return ls;
            }
        }

        [JsonIgnore]
        public List<string> AllProjectThatUserIsAdmin
        {
            get
            {
                return uips.Where(x => x.IsAdmin).Select(x => x.ProjectId).Distinct().ToList();
            }
        }
        #endregion

        #region List Contractor
        [JsonIgnore]
        public List<string> AllContractor
        {
            get
            {
                return uics.Select(x => x.CodeAutoGen).Distinct().ToList();
            }
        }

        [JsonIgnore]
        public List<string> AllContractorThatUserIsAdmin
        {
            get
            {
                return uics.Where(x => x.IsAdmin).Select(x => x.CodeAutoGen).Distinct().ToList();
            }
        }
        #endregion       
        
        #region List Role
        [JsonIgnore]
        public List<Guid> AllRole
        {
            get
            {
                return uirs.Select(x => x.RoleId).Distinct().ToList();
            }
        }

        [JsonIgnore]
        public List<Guid> AllRoleThatUserIsAdmin
        {
            get
            {
                return uirs.Where(x => x.IsAdmin).Select(x => x.RoleId).Distinct().ToList();
            }
        }
        #endregion
    }
}
