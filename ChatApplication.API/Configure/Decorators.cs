using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using ChatApplication.Data.Contracts.Models;
using ChatApplication.Data.Contracts.Repositories;
using ChatApplication.Data.EntityFramework.Events;
using ChatApplication.Data.EntityFramework.Repositories;
using ChatApplication.Infrastructure.Contracts;
using ChatApplication.Logging.Profiling;
using ChatApplication.Service.Contracts;
using ChatApplication.Syncronization.Archive;
using Nancy.TinyIoc;

namespace ChatApplication.API.Configure
{
    public class Decorators
    {
        private readonly bool _profiling;
        private readonly bool _publishEvents;
        private readonly bool _enableArchiving;

        public Decorators(IApplicationSettings appSettings)
        {
            _profiling = bool.Parse(appSettings.GetValue("Logging:Profile"));
            _publishEvents = bool.Parse(appSettings.GetValue("Repo:EnableEvents"));
            _enableArchiving = bool.Parse(appSettings.GetValue("Repo:EnableArchiving"));
        }

        public void Configure(TinyIoCContainer container)
        {
            ConfigureBaseImplementations(container);
            if (_profiling) ConfigureProfiling(container);
            if (_publishEvents) ConfigureEvents(container);
        }

        private void ConfigureBaseImplementations(TinyIoCContainer container)
        {
            // readers
            container.Register<IRepositoryReader<RoomRecord>, RepositoryEF<RoomRecord>>();
            container.Register<IRepositoryReader<MessageRecord>, RepositoryEF<MessageRecord>>();
            container.Register<IRepositoryReader<UserRecord>, RepositoryEF<UserRecord>>();
            container.Register<ILoginReader, LoginRespositoryEntityFramework>();

            // writers
            container.Register<IRepositoryWriter<RoomRecord>, RepositoryEF<RoomRecord>>();
            container.Register<IRepositoryWriter<MessageRecord>, RepositoryEF<MessageRecord>>();
            container.Register<IRepositoryWriter<UserRecord>, RepositoryEF<UserRecord>>();
            container.Register<IRepositoryWriter<LoginRecord>, LoginRespositoryEntityFramework>();
        }
        private void ConfigureProfiling(TinyIoCContainer container)
        {
            var impl = new RepoImplementations(container);

            container.Register<IRepositoryReader<RoomRecord>>(new RepositoryProfiler<RoomRecord>(
                impl.RoomReader,
                impl.RoomWriter,
                container.Resolve<IProfiler>()
            ));
            container.Register<IRepositoryWriter<RoomRecord>>(new RepositoryProfiler<RoomRecord>(
                impl.RoomReader,
                impl.RoomWriter,
                container.Resolve<IProfiler>()
            ), "roomWriterProfiler");

            // message
            container.Register<IRepositoryReader<MessageRecord>>(new RepositoryProfiler<MessageRecord>(
                impl.MessageReader,
                impl.MessageWriter,
                container.Resolve<IProfiler>()
            ));

            container.Register<IRepositoryWriter<MessageRecord>>(new RepositoryProfiler<MessageRecord>(
                impl.MessageReader,
                impl.MessageWriter,
                container.Resolve<IProfiler>()
            ));

            // login
            container.Register<IRepositoryWriter<LoginRecord>>(new RepositoryProfiler<LoginRecord>(
                impl.LoginReader,
                impl.LoginWriter,
                container.Resolve<IProfiler>()
            ));

            container.Register<ILoginReader>(new LoginRepositoryProfiler(
                impl.LoginReader,
                container.Resolve<IProfiler>()
            ));

            // users
            container.Register<IRepositoryWriter<UserRecord>>(new RepositoryProfiler<UserRecord>(
                impl.UserReader,
                impl.UserWriter,
                container.Resolve<IProfiler>()
            ));
            container.Register<IRepositoryReader<UserRecord>>(new RepositoryProfiler<UserRecord>(
                impl.UserReader,
                impl.UserWriter,
                container.Resolve<IProfiler>()
            ));
        }
        private void ConfigureEvents(TinyIoCContainer container)
        {
            var entityFrameworkPublisher = container.Resolve<EntityFrameworkPublisher>();
            
            /* add subscribers */
            if (_enableArchiving) entityFrameworkPublisher.AddSubscriber(new Archive());


            /* register the decorators */
            var impl = new RepoImplementations(container);

            /* enable events */
            container.Register<IRepositoryWriter<RoomRecord>>(new RepositoryEventPublishing<RoomRecord>(
                impl.RoomWriter,
                entityFrameworkPublisher
            ));

            container.Register<IRepositoryWriter<MessageRecord>>(new RepositoryEventPublishing<MessageRecord>(
                impl.MessageWriter,
                entityFrameworkPublisher
            ));

            container.Register<IRepositoryWriter<LoginRecord>>(new RepositoryEventPublishing<LoginRecord>(
                impl.LoginWriter,
                entityFrameworkPublisher
            ));
        }
    }
}