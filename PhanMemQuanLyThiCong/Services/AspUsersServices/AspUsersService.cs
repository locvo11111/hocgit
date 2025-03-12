using PhanMemQuanLyThiCong.Common;
using PhanMemQuanLyThiCong.Common.Helper;
using PhanMemQuanLyThiCong.Model;
using PhanMemQuanLyThiCong.IRepositories;
using PhanMemQuanLyThiCong.Model;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using PhanMemQuanLyThiCong.Common.Constant;

namespace PhanMemQuanLyThiCong.Services.AspUsersServices
{
    public class AspUsersService : IAspUsersServices
    {
        private readonly IUnitOfWork _unitOfWork;
        public AspUsersService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public bool Add(AppUserViewModel entity)
        {
            throw new System.NotImplementedException();
        }

        public bool Add(IEnumerable<AppUserViewModel> entities)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> AddAsync(AppUserViewModel entity)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> AddAsync(IEnumerable<AppUserViewModel> entities)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<AppUserViewModel> All()
        {
            throw new System.NotImplementedException();
        }

        public async Task<IEnumerable<AppUserViewModel>> AllAsync()
        {
            List<AppUserViewModel>  appUserViewModels = new List<AppUserViewModel>();
            var data = await KeyHelper.ActGetallAsync<AppUserViewModel>(RouteAPI.APPUER_GETALL);
            if (data.MESSAGE_TYPECODE)
            {
                appUserViewModels = data.Dto;
            }
            return appUserViewModels;
        }

        public void Dispose()
        {
            throw new System.NotImplementedException();
        }

        public AppUserViewModel Find(object pksFields)
        {
            throw new System.NotImplementedException();
        }

        public Task<AppUserViewModel> FindAsync(object pksFields)
        {
            throw new System.NotImplementedException();
        }

        public DataTable GetAll(string keyWord)
        {
            throw new System.NotImplementedException();
        }

        public bool InstertOrUpdate(AppUserViewModel entity, object pks)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> InstertOrUpdateAsync(AppUserViewModel entity, object pks)
        {
            throw new System.NotImplementedException();
        }

        public void Remove(object key)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> RemoveAsync(object key)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> RemoveAsync<Tkey>(Tkey key)
        {
            throw new System.NotImplementedException();
        }

        public bool Update(AppUserViewModel entity, object pks)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> UpdateAsync(AppUserViewModel entity, object pks)
        {
            throw new System.NotImplementedException();
        }
    }
}
