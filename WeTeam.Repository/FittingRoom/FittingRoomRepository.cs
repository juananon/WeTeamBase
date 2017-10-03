using System.Collections.Generic;
using System.Linq;
using WeTeam.Entities.FittingRooms;
using WeTeam.Patterns.UnitOfWork;

namespace WeTeam.Repository.FittingRoom
{
    public class FittingRoomRepository : BaseRepository<Entities.FittingRooms.FittingRoom>, IFittingRoomRepository
    {
        private readonly IFittingRoomBookingRepository fittingRoomBookingRepository;

        public FittingRoomRepository(IUnitOfWork unitOfWork, IFittingRoomBookingRepository fittingRoomBookingRepository) : base(unitOfWork)
        {
            this.fittingRoomBookingRepository = fittingRoomBookingRepository;
        }
        public IEnumerable<Entities.FittingRooms.FittingRoom> GetAllFittingRooms()
        {
            var fittingRoms = GetAll();
            foreach (var fittingRoom in fittingRoms)
            {
                var fittingRoomBooking = this.fittingRoomBookingRepository
                    .GetAllBy<FittingRoomBooking>(x => x.FittingRoomId == fittingRoom.Id).FirstOrDefault();
                fittingRoom.FittingRoomBookingGuid = fittingRoomBooking?.FittingRoomBookingGuid;
            }
            return fittingRoms;
        }

        public IEnumerable<Entities.FittingRooms.FittingRoom> GetFreeFittingRooms()
        {
            var fittingRoms = GetAllBy<Entities.FittingRooms.FittingRoom>(x => x.FittingRoomStatus == FittingRoomStatusEnum.Free);
            foreach (var fittingRoom in fittingRoms)
            {
                var fittingRoomBooking = this.fittingRoomBookingRepository
                    .GetAllBy<FittingRoomBooking>(x => x.FittingRoomId == fittingRoom.Id).FirstOrDefault();
                fittingRoom.FittingRoomBookingGuid = fittingRoomBooking?.FittingRoomBookingGuid;
            }
            return fittingRoms;
        }

        public Entities.FittingRooms.FittingRoom GetFittinRoomIfFree()
        {
            return GetAllBy<Entities.FittingRooms.FittingRoom>(x => x.FittingRoomStatus == FittingRoomStatusEnum.Free)
                                                                                                    .FirstOrDefault();
        }
    }
}