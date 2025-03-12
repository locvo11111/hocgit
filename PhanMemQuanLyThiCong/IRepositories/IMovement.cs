using PhanMemQuanLyThiCong.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PhanMemQuanLyThiCong.IRepositories
{
    public interface IMovement
    {
        Task<bool> InsertCommads();

        Task<bool> InsertTypeAccounts();

        Task<bool> InsertFunctionTypes();

        Task<bool> InsertFunctions();

        Task<bool> InsertKeyStores();

        Task<bool> InsertPermissions();

        Task<bool> InsertUsers();

        Task<bool> InsertUserApproves();
    }
}