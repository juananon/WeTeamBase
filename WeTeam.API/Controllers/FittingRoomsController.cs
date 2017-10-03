using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;
using WeTeam.Entities.FittingRooms;
using WeTeam.Repository.FittingRoom;

namespace WeTeam.API.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")] 
    public class FittingRoomsController : ApiController
    {
        private readonly IFittingRoomRepository fittingRoomRepository;

        public FittingRoomsController(IFittingRoomRepository fittingRoomRepository)
        {
            this.fittingRoomRepository = fittingRoomRepository;
        }
        
        public async Task<IEnumerable<FittingRoom>> Get()
        {
            return this.fittingRoomRepository.GetAllFittingRooms();
        }
    }
}
