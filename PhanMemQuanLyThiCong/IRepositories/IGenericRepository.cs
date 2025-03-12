using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace PhanMemQuanLyThiCong.IRepositories
{
    public interface IGenericRepository<TEntity> : IDisposable where TEntity : class
    {
        IEnumerable<TEntity> All();

        Task<IEnumerable<TEntity>> AllAsync();

        TEntity Find(object pksFields);

        Task<TEntity> FindAsync(object pksFields);

        bool Add(TEntity entity);

        Task<bool> AddAsync(TEntity entity);

        bool Add(IEnumerable<TEntity> entities);

        Task<bool> AddAsync(IEnumerable<TEntity> entities);

        void Remove(object key);

        Task<bool> RemoveAsync(object key);
        Task<bool> RemoveAsync<Tkey>(Tkey key);

        bool Update(TEntity entity, object pks);

        Task<bool> UpdateAsync(TEntity entity, object pks);

        bool InstertOrUpdate(TEntity entity, object pks);

        Task<bool> InstertOrUpdateAsync(TEntity entity, object pks);

        DataTable GetAll(string keyWord);
    }
}