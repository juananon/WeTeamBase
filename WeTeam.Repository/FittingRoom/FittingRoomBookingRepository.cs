using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WeTeam.Entities.FittingRooms;
using WeTeam.Patterns.UnitOfWork;

namespace WeTeam.Repository.FittingRoom
{
    public class FittingRoomBookingRepository : BaseRepository<FittingRoomBooking>, IFittingRoomBookingRepository
    {
        public FittingRoomBookingRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public FittingRoomBooking FindByFittingRoomBookingGuid(string fittingRoomBookingGuid)
        {
            var guid = new Guid(fittingRoomBookingGuid);
            return GetAllBy<FittingRoomBooking>(x => x.FittingRoomBookingGuid == guid).FirstOrDefault();
        }

        public void BookIfPossible(int id)
        {
            var booking = GetById(id);
            //var freeFittingRooms = reponuevo.GetAllBy<FittingRoomBooking>(x => x.FittingRoomBookingGuid == guid)
            //GetFreeFittingRooms
        }

        public FittingRoomBooking GetNextWaitingBooking()
        {
            return GetAllBy<FittingRoomBooking>(x => x.FittingRoomBookingStatus == FittingRoomBookingStatusEnum.Waiting)
                .OrderBy(x => x.Id).FirstOrDefault();
        }

        public IEnumerable<FittingRoomBooking> GetQueue(int fittingRoomBookingId)
        {
            return GetAllBy<FittingRoomBooking>(x => x.FittingRoomBookingStatus == FittingRoomBookingStatusEnum.Waiting
                                                                                        && x.Id < fittingRoomBookingId);
        }

        public async Task ReviewBooking(int fittingRoomBookingId)
        {
            // TODO: Implementation.
        }
    }
}