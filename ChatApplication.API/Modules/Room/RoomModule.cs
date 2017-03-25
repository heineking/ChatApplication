using System;
using ChatApplication.Service.Contracts;
using Nancy;
using Nancy.ModelBinding;
using Nancy.Security;

namespace ChatApplication.API.Modules.Room
{
    public class RoomModule : NancyModule
    {
        public RoomModule(IRoomReader reader, IRoomWriter writer) : base("/api/v1/rooms")
        {
            this.RequiresAuthentication();
            
            Get["/all"] = _ =>
            {
                var rooms = reader.GetAllRooms();
                if (rooms != null)
                {
                    return Negotiate
                        .WithStatusCode(HttpStatusCode.OK)
                        .WithModel(new {rooms});
                }
                return HttpStatusCode.BadRequest;
            };
            Get["/{roomId:long}"] = p =>
            {
                var room = reader.GetRoomMessages(p.roomId);
                if (room != null)
                {
                    return Negotiate
                        .WithStatusCode(HttpStatusCode.OK)
                        .WithModel(new {room});
                }
                return HttpStatusCode.BadRequest;
            };
            Get["/{roomId:long}/{dateTime:long}"] = p =>
            {
                var roomMessagesFromDate = reader.GetRoomMessagesFromDate(p.roomId, p.dateTime);
                if (roomMessagesFromDate != null)
                {
                    return Negotiate
                        .WithStatusCode(HttpStatusCode.OK)
                        .WithModel(new {messages = roomMessagesFromDate});
                }
                return HttpStatusCode.BadRequest;
            };
            Post["/create"] = _ =>
            {
                var model = this.Bind<CreateRoomRequest>();
                if (model == null) return HttpStatusCode.BadRequest;
                writer.CreateRoom(model.Name);
                return HttpStatusCode.OK;
            };
        }
    }
}