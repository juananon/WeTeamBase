using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WeTeam.API.Controllers;
using WeTeam.Entities.FittingRooms;
using WeTeam.Infrastucture.Notification;
using WeTeam.Infrastucture.Rest;
using WeTeam.Patterns.EF;
using WeTeam.Patterns.UnitOfWork;
using WeTeam.Repository;
using WeTeam.Repository.FittingRoom;
using WeTeam.Repository.Test;
using WeTeam.Repository.UnitOfWork;
using WeTeam.Tests.IOC;

namespace WeTeam.Tests.Integrations.FittingRooms
{
    [TestClass]
    public class FittingRoomBookingTest
    {
        [TestMethod]
        public async Task Given_a_post_request_API_should_create_a_booking()
        {
            var container = UnityConfig.GetConfiguredContainer();
            var fittingRoomBookingRepository = container.Resolve<FittingRoomBookingRepository>();
            var fittingRoomRepository = container.Resolve<FittingRoomRepository>();
            var unitOfWork = container.Resolve<UnitOfWork>();
            var controller = new FittingRoomBookingsController(fittingRoomBookingRepository, fittingRoomRepository);
            await controller.Post("5B49D9BF-5EDB-4CC6-8701-43469E0A9FCC");
        }

        [TestMethod]
        public async Task Given_a_delete_request_API_should_cancel_a_booking_and_liberate_fitting_room()
        {
            var container = UnityConfig.GetConfiguredContainer();
            var fittingRoomBookingRepository = container.Resolve<FittingRoomBookingRepository>();
            var fittingRoomRepository = container.Resolve<FittingRoomRepository>();
            var unitOfWork = container.Resolve<UnitOfWork>();
            var controller = new FittingRoomBookingsController(fittingRoomBookingRepository, fittingRoomRepository);
            await controller.Delete("5B49D9BF-5EDB-4CC6-8701-43469E0A9FCC");
        }

        [TestMethod]
        public async Task test()
        {
            try
            {
                await EventNotifier.Notify("dzrl-i1e64c:APA91bHt9h6162Dl6ds_xfed1J2PrUOXQLUvcFudA5pTh-hCan9M4nSdAw3qNip6d-H5rBkoz9av1Cgq6i9msv8rLzo6IV57Q-qsrbkIXYCOgfgkiapr0VIjGf4UTv6kESq8Hn0alwpZ", "1");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            
        }
    }
}
