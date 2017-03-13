using ChatApplication.Service.Contracts;
using Nancy;
using Nancy.Security;

namespace ChatApplication.API.Modules
{
    public class RoomModule : NancyModule
    {
        public RoomModule(IRoomReader reader) : base("/api/v1/rooms")
        {
            this.RequiresAuthentication();

            Get["/all"] = _ => reader.GetAllRooms();
            Get["/{roomId:long}"] = p => reader.GetRoomMessages(p.roomId);
            Get["/{roomId:long}/{dateTime:long}"] = p => reader.GetRoomMessagesFromDate(p.roomId, p.dateTime);

        }
    }
}