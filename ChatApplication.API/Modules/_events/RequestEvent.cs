using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ChatApplication.Infrastructure.Contracts.Events;
using Nancy;
using Newtonsoft.Json;

namespace ChatApplication.API.Modules._events
{
    public class RequestEvent<TRequest> : IEvent
    {
        public NancyContext Context { get; }
        public TRequest Request { get; }
        public string Name { get; }

        public RequestEvent(NancyContext context, string name, TRequest request)
        {
            Context = context;
            Name = name;
            Request = request;
        }
    }
}