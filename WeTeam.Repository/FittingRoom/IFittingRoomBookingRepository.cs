
using System.Collections.Generic;
using System.Threading.Tasks;
using WeTeam.Entities.FittingRooms;
using WeTeam.Patterns.Repository;

namespace WeTeam.Repository.FittingRoom
{
    public interface IFittingRoomBookingRepository : IBaseRepository<FittingRoomBooking>
    {
        FittingRoomBooking FindByFittingRoomBookingGuid(string fittingRoomBookingGuid);
        FittingRoomBooking GetNextWaitingBooking();
        IEnumerable<FittingRoomBooking> GetQueue(int fittingRoomBookingId);
        Task ReviewBooking(int fittingRoomBookingId);
    }
}
