using PhanMemQuanLyThiCong.Common;
using PhanMemQuanLyThiCong.Common.Constant;
using PhanMemQuanLyThiCong.Common.Helper;
using PhanMemQuanLyThiCong.Common.SQLite;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using VChatCore.Dto;

namespace PhanMemQuanLyThiCong.Services.ThongTinTongHopDuAnServices
{
    public class ThongTinTongHopDuAnsServices : IThongTinTongHopDuAnServices
    {

        public bool Add(ThongTinTHDAViewModel entity)
        {
            throw new System.NotImplementedException();
        }

        public bool Add(IEnumerable<ThongTinTHDAViewModel> entities)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> AddAsync(ThongTinTHDAViewModel entity)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> AddAsync(IEnumerable<ThongTinTHDAViewModel> entities)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<ThongTinTHDAViewModel> All()
        {
            throw new System.NotImplementedException();
        }

        public async Task<IEnumerable<ThongTinTHDAViewModel>> AllAsync()
        {
            List<ThongTinTHDAViewModel>  thongTinTongHopDuAnDtos = new List<ThongTinTHDAViewModel>();
            var data = await KeyHelper.ActGetallAsync<ThongTinTHDAViewModel>(RouteAPI.THONGTINTONGHOPDUAN_GETALL);
            if (data.MESSAGE_TYPECODE)
            {
                thongTinTongHopDuAnDtos = data.Dto;
            }
            return thongTinTongHopDuAnDtos;
        }

        public void Dispose()
        {
            throw new System.NotImplementedException();
        }

        public ThongTinTHDAViewModel Find(object pksFields)
        {
            throw new System.NotImplementedException();
        }

        public Task<ThongTinTHDAViewModel> FindAsync(object pksFields)
        {
            throw new System.NotImplementedException();
        }

        public DataTable GetAll(string keyWord)
        {
            throw new System.NotImplementedException();
        }

        public bool InstertOrUpdate(ThongTinTHDAViewModel entity, object pks)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> InstertOrUpdateAsync(ThongTinTHDAViewModel entity, object pks)
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

        public bool Update(ThongTinTHDAViewModel entity, object pks)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> UpdateAsync(ThongTinTHDAViewModel entity, object pks)
        {
            throw new System.NotImplementedException();
        }
    }
}