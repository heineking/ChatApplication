using System;
using ChatApplication.API.Extensions;
using ChatApplication.Service.Contracts;
using Nancy;
using Nancy.ModelBinding;
using Nancy.Security;
using Newtonsoft.Json.Converters;

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
                var roomMessagesFromDate = reader.GetRoomMessagesFromDate(p.roomId, DateTimeJavascript.ToCsharpDateTime(p.dateTime));
                if (roomMessagesFromDate != null)
                {
                    return Negotiate
                        .WithStatusCode(HttpStatusCode.OK)
                        .WithModel(new {messages = roomMessagesFromDate});
                }
                return HttpStatusCode.BadRequest;
            };
            Post["/{roomId:long}"] = p =>
            {
                var messageRequest = this.Bind<CreateMessageRequest>();
                writer.AddMessage(new Message
                {
                    UserId = Guid.Parse("c9a835b1-c108-43a1-962b-4fb5f4739f69"),
                    PostedDate = DateTime.Now,
                    RoomId = p.roomId,
                    Text = messageRequest.Text,
                });
                return HttpStatusCode.OK;
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