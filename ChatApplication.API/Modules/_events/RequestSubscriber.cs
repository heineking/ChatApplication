using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using ChatApplication.API.Modules.Room;
using ChatApplication.Infrastructure.Contracts;
using ChatApplication.Infrastructure.Contracts.Events;
using ChatApplication.Logging.JsonNet;
using log4net;
using Newtonsoft.Json;

namespace ChatApplication.API.Modules._events
{
    public class RequestSubscriber : IEventSubscriber
    {
        private readonly ILog _log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public void Subscribe<TEvent>(TEvent @event) where TEvent : IEvent
        {
            if (@event.Name.Equals(RequestName.UnauthorizedLogin)) Unauthorized(@event as RequestEvent<LoginRequest>);
        }

        public void Unauthorized<TRequest>(RequestEvent<TRequest> requestEvent)
        {
            var json = JsonConvert.SerializeObject(requestEvent.Request, new JsonSerializerSettings
            {
                ContractResolver = LoggingContractResolver<TRequest>.Instance()
            });
            _log.Warn($"function=[{MethodBase.GetCurrentMethod().Name}]; request=[{json}]; id_address=[{requestEvent.Context.Request.UserHostAddress}];");
        }
    }
}