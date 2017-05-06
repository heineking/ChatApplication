using System;
using System.Linq;
using ChatApplication.API.Extensions;
using ChatApplication.API.User;
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
                long roomId = p.roomId;
                var room = reader.GetRoom(roomId);
                if (room != null)
                {
                    return Negotiate
                        .WithStatusCode(HttpStatusCode.OK)
                        .WithModel(new { roomId, room });
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

            /**
             * Create Room
             */
            Post["/"] = _ =>
            {
                this.RequiresAuthentication();
                var model = this.Bind<CreateRoomRequest>();
                if (model == null) return HttpStatusCode.BadRequest;
                var chatUser = (UserIdentity)Context.CurrentUser;
                writer.CreateRoom(model.Name, model.Description, chatUser.UserId);
                return HttpStatusCode.OK;
            };
            /**
             * Add a message to the room
             */
            Post["/{roomId:long}"] = p =>
            {
                this.RequiresAuthentication();
                long roomId = p.roomId;
                var chatUser = (UserIdentity) Context.CurrentUser;
                var messageRequest = this.Bind<CreateMessageRequest>();
                var message = new Message
                {
                    UserId = chatUser.UserId,
                    UserName = chatUser.UserName,
                    PostedDate = DateTime.Now,
                    RoomId = roomId,
                    Text = messageRequest.Text,
                };
                writer.AddMessage(message);
                return Negotiate
                    .WithStatusCode(HttpStatusCode.OK)
                    .WithModel(new {roomId, message});
            };
            /**
             * Delete room
             */
            Delete["/{roomId:long}"] = p =>
            {
                this.RequiresAuthentication();
                /* only admins can delete a room */
                var chatUser = (UserIdentity)Context.CurrentUser;
                if (!chatUser.Claims.ToList().Contains("admin")) return HttpStatusCode.Unauthorized;

                long roomId = p.roomId;
                writer.DeleteRoom(roomId);
                return Negotiate
                    .WithStatusCode(HttpStatusCode.OK)
                    .WithModel(new { roomId });
            };
        }
    }
}