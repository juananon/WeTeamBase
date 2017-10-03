using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Threading.Tasks;
using WeTeam.Infrastucture.Notification;

namespace WeTeam.Entities.FittingRooms
{
    public class FittingRoomBooking
    {
        public int Id { get; set; }
        public Guid FittingRoomBookingGuid { get; set; }
        public FittingRoomBookingStatusEnum FittingRoomBookingStatus { get; set; }
        public DateTime? Time { get; set; }
        public int? FittingRoomId { get; set; }
        public virtual FittingRoom FittingRoom { get; set; }
        public string UserNotificationId { get; set; }
        [NotMapped]
        public int Queue { get; set; }

        public FittingRoomBooking()
        {
        }

        public FittingRoomBooking(string userNotificationId)
        {
            this.New(userNotificationId);
        }


        private void New(string userNotificationId)
        {
            this.FittingRoomBookingGuid = Guid.NewGuid();
            this.UserNotificationId = userNotificationId;
            this.Time = DateTime.Now;
            this.FittingRoomBookingStatus = FittingRoomBookingStatusEnum.Waiting;
        }

        public void Cancel(FittingRoom fittingRoom)
        {
            this.FittingRoomBookingStatus = FittingRoomBookingStatusEnum.Cancelled;
            this.FittingRoomId = null;
            fittingRoom.Cancel(fittingRoom);
        }

        public async Task Book(int fittingRoomId)
        {
            this.FittingRoomBookingStatus = FittingRoomBookingStatusEnum.InUse;
            this.FittingRoomId = fittingRoomId;
            this.FittingRoom.Book();
            await EventNotifier.Notify(this.UserNotificationId, this.FittingRoom.Localization);
        }

        public async Task Book(FittingRoom fittingRoom)
        {
            this.FittingRoomBookingStatus = FittingRoomBookingStatusEnum.InUse;
            this.FittingRoomId = fittingRoom.Id;
            fittingRoom.Book();
            await EventNotifier.Notify(this.UserNotificationId, fittingRoom.Localization);
        }
    }
}
