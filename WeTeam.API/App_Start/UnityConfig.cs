using Microsoft.Practices.Unity;
using System.Web.Http;
using Unity.WebApi;
using WeTeam.Patterns.EF;
using WeTeam.Patterns.UnitOfWork;
using WeTeam.Repository;
using WeTeam.Repository.FittingRoom;
using WeTeam.Repository.Test;
using WeTeam.Repository.UnitOfWork;

namespace WeTeam.API
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();
            RegisterTypes(container);

            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }

        private static void RegisterTypes(UnityContainer container)
        {
            container.RegisterType<IUnitOfWork, UnitOfWork>();
            container.RegisterType<IDbContext, Context>();
            container.RegisterType<IContextContainer, ContextContainer>();
            container.RegisterType<IDummyRepository, DummyRepository>();
            container.RegisterType<IFittingRoomBookingRepository, FittingRoomBookingRepository>();
            container.RegisterType<IFittingRoomRepository, FittingRoomRepository>();
        }

        public static IUnityContainer GetConfiguredContainer()
        {
            var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();
            RegisterTypes(container);
            return container;
        }
    }
}