using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ChatApplication.Data.Contracts.Events;
using ChatApplication.Data.Contracts.Models;
using ChatApplication.Data.Contracts.Repositories;
using ChatApplication.Data.EntityFramework.Events;
using ChatApplication.Data.EntityFramework.Repositories;
using ChatApplication.Syncronization.Archive;
using Nancy.TinyIoc;

namespace ChatApplication.API.PubSub
{
    public class Publisher
    {
        private readonly Archive _archive;

        public Publisher()
        {
            _archive = new Archive();
        }

        public void ConfigureEntityFrameworkDecorator(TinyIoCContainer container)
        {
            var entityFrameworkPublisher = container.Resolve<EntityFrameworkPublisher>();
            entityFrameworkPublisher.AddSubscriber(_archive);
            container.Register<IRepositoryWriter<RoomRecord>>(new RepositoryEventPublishing<RoomRecord>(
                container.Resolve<IRepositoryWriter<RoomRecord>>("roomWriter"),
                entityFrameworkPublisher
            ));
        }
    }
}