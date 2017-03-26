using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ChatApplication.Service.Contracts;
using Nancy;
using Nancy.ModelBinding;
using Nancy.Security;

namespace ChatApplication.API.Modules.Message
{
    public class MessageModule : NancyModule
    {
        public MessageModule(IMessageWriter writer) : base("/api/v1/messages")
        {
            this.RequiresAuthentication();

            Post["/{id:long}"] = _ =>
            {
                var messageRequest = this.Bind<CreateMessageRequest>();
                // todo: move to own mapper
                writer.Save(new Service.Contracts.Message
                {
                    UserId = Guid.Parse("c9a835b1-c108-43a1-962b-4fb5f4739f69"),
                    PostedDate = DateTime.Now.Ticks,
                    RoomId = _.id,
                    Text = messageRequest.Text,
                });
                return HttpStatusCode.OK;
            };
        }
    }
}