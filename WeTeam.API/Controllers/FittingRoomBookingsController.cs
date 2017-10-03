using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;
using Hangfire;
using WeTeam.Entities.FittingRooms;
using WeTeam.Repository.FittingRoom;

namespace WeTeam.API.Controllers
{
    //[BasicAuthenticationFilter]
    [EnableCors(origins: "*", headers: "*", methods: "*")] 
    public class FittingRoomBookingsController : ApiController
    {
        private readonly IFittingRoomBookingRepository fittingRoomBookingRepository;
        private readonly IFittingRoomRepository fittingRoomRepository;

        public FittingRoomBookingsController(IFittingRoomBookingRepository fittingRoomBookingRepository,
                                                IFittingRoomRepository fittingRoomRepository)
        {
            this.fittingRoomBookingRepository = fittingRoomBookingRepository;
            this.fittingRoomRepository = fittingRoomRepository;
        }

        public async Task<FittingRoomBooking> Get(string fittingRoomBookingGuid)
        {
            var fittingRoomBooking = this.fittingRoomBookingRepository.FindByFittingRoomBookingGuid(fittingRoomBookingGuid);
            var queue = this.fittingRoomBookingRepository.GetQueue(fittingRoomBooking.Id);
            fittingRoomBooking.Queue = queue.Count();
            return fittingRoomBooking;
        }

        public async Task<string> Post([FromBody]string userNotificationId)
        {
            var fittingRoomBooking = new FittingRoomBooking(userNotificationId);
            this.fittingRoomBookingRepository.Add(fittingRoomBooking);
            var freeFittingRoom = this.fittingRoomRepository.GetFittinRoomIfFree();
            if (freeFittingRoom != null)
            {
                await fittingRoomBooking.Book(freeFittingRoom);
                RecurringJob.AddOrUpdate<IFittingRoomBookingRepository>
                    (x => x.ReviewBooking(fittingRoomBooking.Id), Cron.Daily(8, 0));
            }
            await this.fittingRoomBookingRepository.SaveChangesAsync();
            await this.fittingRoomRepository.SaveChangesAsync();
            return fittingRoomBooking.FittingRoomBookingGuid.ToString();
        }

        public async Task<string> Put([FromBody]string fittingRoomBookingGuid)
        {
            return null;
        }

        public async Task Delete([FromBody]string fittingRoomBookingGuid)
        {
            var fittingRoomBooking = this.fittingRoomBookingRepository.FindByFittingRoomBookingGuid(fittingRoomBookingGuid);
            var fittingRoomId = fittingRoomBooking.FittingRoomId;
            var fittingRoom = this.fittingRoomRepository.GetById(fittingRoomId.Value);
            fittingRoomBooking.Cancel(fittingRoom);
            this.fittingRoomBookingRepository.Delete(fittingRoomBooking);
            var nextBooking = this.fittingRoomBookingRepository.GetNextWaitingBooking();
            if (nextBooking != null)
            {
                await nextBooking.Book(fittingRoom);
                RecurringJob.AddOrUpdate<IFittingRoomBookingRepository>
                    (x => x.ReviewBooking(fittingRoomBooking.Id), Cron.Daily(8, 0));
            }
            await this.fittingRoomBookingRepository.SaveChangesAsync();
            await this.fittingRoomRepository.SaveChangesAsync();
        }
    }
}
