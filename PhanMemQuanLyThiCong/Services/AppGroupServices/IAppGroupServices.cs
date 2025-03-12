using PhanMemQuanLyThiCong.Model;
using System.Collections.Generic;

namespace PhanMemQuanLyThiCong.IRepositories
{
    public interface IAppGroupServices: IGenericRepository<AppGroupViewModel>
    {
        List<AppUserGroupViewModel> AddAppUserGroups(List<string> liststring);
        List<AppDuAnGoupViewModel> AddAppDuAnGoups(List<string> liststring);
        List<AppRoleGroupViewModel> AddAppRoleGoups(List<string> liststring);
    }
}