
using System.Collections.Generic;
using WeTeam.Patterns.Repository;

namespace WeTeam.Repository.FittingRoom
{
    public interface IFittingRoomRepository : IBaseRepository<Entities.FittingRooms.FittingRoom>
    {
        IEnumerable<Entities.FittingRooms.FittingRoom> GetAllFittingRooms();
        IEnumerable<Entities.FittingRooms.FittingRoom> GetFreeFittingRooms();
        Entities.FittingRooms.FittingRoom GetFittinRoomIfFree();
    }
}
