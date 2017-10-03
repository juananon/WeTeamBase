using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace WeTeam.Entities.FittingRooms
{
    public class FittingRoom
    {
        public int Id { get; set; }
        public string Localization { get; set; }
        public FittingRoomStatusEnum FittingRoomStatus { get; set; }


        [NotMapped]
        public Guid? FittingRoomBookingGuid { get; set; }


        public void Cancel(FittingRoom fittingRoom)
        {
            fittingRoom.FittingRoomStatus = FittingRoomStatusEnum.Free;
        }

        public void Book()
        {
            this.FittingRoomStatus = FittingRoomStatusEnum.Busy;
        }

        public void Take()
        {
            this.FittingRoomStatus = FittingRoomStatusEnum.Busy;
        }
    }
}
