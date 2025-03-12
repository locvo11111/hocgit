using PhanMemQuanLyThiCong.Function;
using PhanMemQuanLyThiCong.IRepositories;
using PhanMemQuanLyThiCong.Repositories;
//using PhanMemQuanLyThiCong.Services.AspUsersServices;
//using PhanMemQuanLyThiCong.Services.ThongTinTongHopDuAnServices;
using Unity;

namespace PhanMemQuanLyThiCong
{
    public class ConfigUnity
    {
        /// <summary>
        /// Gets the container.
        /// </summary>
        /// <value>
        /// The container.
        /// </value>
        public static UnityContainer Container { get; private set; } = new UnityContainer();

        /// <summary>
        /// Registers this instance.
        /// </summary>
        public static void Register()
        {

            Container.RegisterType<IMovement, Movement>();
            Container.RegisterType<IUnitOfWork, UnitOfWork>();
            Container.RegisterType<IDataRepository, DataRepository>();
            //Container.RegisterType<IThongTinTongHopDuAnServices, ThongTinTongHopDuAnsServices>();
            //Container.RegisterType<IAspUsersServices, AspUsersService>();
            Container.RegisterType<IChatService, ChatService>();

            //Container.RegisterType<IChatHubRepsitory, ChatHubRepsitory>();
        }
    }
}