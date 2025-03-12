using PhanMemQuanLyThiCong.Common.Namequyery;
using PhanMemQuanLyThiCong.IRepositories;
using PhanMemQuanLyThiCong.Model;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using System.Linq;
using PhanMemQuanLyThiCong.Common;
using System;
using PhanMemQuanLyThiCong.Common.Helper;

namespace PhanMemQuanLyThiCong.Repositories
{
    public class AppRolesServices : IAppRolesServices
    {
        private readonly IUnitOfWork _unitOfWork;
        public AppRolesServices(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public bool Add(AppRolesViewModel entity)
        {
            if (entity != null)
            {
               int res= _unitOfWork.Execute(Quyery.INSERTAPPROLES, entity, false, true);
                if (res > 0)
                {
                    return true;
                }
                else
                return true;
            }
            else
            {
                return false;
            }    
        }

        public bool Add(IEnumerable<AppRolesViewModel> entities)
        {
            throw new System.NotImplementedException();
        }

        public async Task<bool> AddAsync(AppRolesViewModel entity)
        {
            var data = await KeyHelper.ActAddAsync<AppRolesViewModel>(entity, RouteAPI.APPROLE_ADD);
            return data.MESSAGE_TYPECODE;
        }

        public Task<bool> AddAsync(IEnumerable<AppRolesViewModel> entities)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<AppRolesViewModel> All()
        {
            return _unitOfWork.Query<AppRolesViewModel>(Quyery.SELECTAPPROLES, null, true, true).OrderByDescending(x => x.Id);
        }

        public async Task<IEnumerable<AppRolesViewModel>> AllAsync()
        {
            List<AppRolesViewModel>  appRoleViewModels = new List<AppRolesViewModel>();
            var data = await KeyHelper.ActGetallAsync<AppRolesViewModel>(RouteAPI.APPROLE_GETALL);
            if (data.MESSAGE_TYPECODE)
            {
                appRoleViewModels = data.Dto;
            }
            return appRoleViewModels;
        }

        public void Dispose()
        {
            throw new System.NotImplementedException();
        }

        public AppRolesViewModel Find(object pksFields)
        {
            throw new System.NotImplementedException();
        }

        public Task<AppRolesViewModel> FindAsync(object pksFields)
        {
            throw new System.NotImplementedException();
        }

        public DataTable GetAll(string keyWord)
        {
            throw new System.NotImplementedException();
        }

        public bool InstertOrUpdate(AppRolesViewModel entity, object pks)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> InstertOrUpdateAsync(AppRolesViewModel entity, object pks)
        {
            throw new System.NotImplementedException();
        }

        public void Remove(object key)
        {
            throw new System.NotImplementedException();
        }

      
        public Task<bool> RemoveAsync(object key)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> RemoveAsync<Tkey>(Tkey key)
        {
            var data = await KeyHelper.ActDeleteAsync<Tkey>(key, RouteAPI.APPROLE_DELETE);
            return data.MESSAGE_TYPECODE;
        }

        public bool Update(AppRolesViewModel entity, object pks)
        {
            throw new System.NotImplementedException();
        }

        public async Task<bool> UpdateAsync(AppRolesViewModel entity, object pks)
        {
            var data = await KeyHelper.ActUpdateAsync<AppRolesViewModel>(entity, RouteAPI.APPROLE_UPDATE);
            return data.MESSAGE_TYPECODE;
        }
    }
}