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
using ChatApplication.Syncronization.Archive;
using Nancy.TinyIoc;

namespace ChatApplication.API.Configure
{
    public class Decorators
    {
        private readonly bool _profiling;
        private readonly bool _publishEvents;

        public Decorators(IApplicationSettings appSettings)
        {
            _profiling = bool.Parse(appSettings.GetValue("Logging:Profile"));
            _publishEvents = bool.Parse(appSettings.GetValue("Repo:EnableEvents"));
        }

        public void Configure(TinyIoCContainer container)
        {
            if (_profiling || _publishEvents)
            {
                ConfigureBaseImplementations(container);
                if (_profiling) ConfigureProfiling(container);
                if (_publishEvents) ConfigureEvents(container);
            }
            else
            {
                ConfigureWithoutDecorators(container);
            }
        }

        private void ConfigureWithoutDecorators(TinyIoCContainer container)
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

        private void ConfigureBaseImplementations(TinyIoCContainer container)
        {
            // readers
            container.Register<IRepositoryReader<RoomRecord>, RepositoryEF<RoomRecord>>("roomReader");
            container.Register<IRepositoryReader<MessageRecord>, RepositoryEF<MessageRecord>>("messageReader");
            container.Register<IRepositoryReader<UserRecord>, RepositoryEF<UserRecord>>("userReader");
            container.Register<ILoginReader, LoginRespositoryEntityFramework>("loginReader");

            // writers
            container.Register<IRepositoryWriter<RoomRecord>, RepositoryEF<RoomRecord>>("roomWriter");
            container.Register<IRepositoryWriter<MessageRecord>, RepositoryEF<MessageRecord>>("messageWriter");
            container.Register<IRepositoryWriter<UserRecord>, RepositoryEF<UserRecord>>("userWriter");
            container.Register<IRepositoryWriter<LoginRecord>, LoginRespositoryEntityFramework>("loginWriter");
        }
        private void ConfigureProfiling(TinyIoCContainer container)
        {
            /* DECORATOR IMPLEMENTATIONS */
            // Room
            container.Register<IRepositoryReader<RoomRecord>>(new RepositoryProfiler<RoomRecord>(
                container.Resolve<IRepositoryReader<RoomRecord>>("roomReader"),
                container.Resolve<IRepositoryWriter<RoomRecord>>("roomWriter"),
                container.Resolve<IProfiler>()
            ));
            container.Register<IRepositoryWriter<RoomRecord>>(new RepositoryProfiler<RoomRecord>(
                container.Resolve<IRepositoryReader<RoomRecord>>("roomReader"),
                container.Resolve<IRepositoryWriter<RoomRecord>>("roomWriter"),
                container.Resolve<IProfiler>()
            ), "roomWriterProfiler");

            // message
            container.Register<IRepositoryReader<MessageRecord>>(new RepositoryProfiler<MessageRecord>(
                container.Resolve<IRepositoryReader<MessageRecord>>("messageReader"),
                container.Resolve<IRepositoryWriter<MessageRecord>>("messageWriter"),
                container.Resolve<IProfiler>()
            ));

            container.Register<IRepositoryWriter<MessageRecord>>(new RepositoryProfiler<MessageRecord>(
                container.Resolve<IRepositoryReader<MessageRecord>>("messageReader"),
                container.Resolve<IRepositoryWriter<MessageRecord>>("messageWriter"),
                container.Resolve<IProfiler>()
            ));

            // login
            container.Register<IRepositoryWriter<LoginRecord>>(new RepositoryProfiler<LoginRecord>(
                container.Resolve<ILoginReader>("loginReader"),
                container.Resolve<IRepositoryWriter<LoginRecord>>("loginWriter"),
                container.Resolve<IProfiler>()
            ));

            container.Register<ILoginReader>(new LoginRepositoryProfiler(
                container.Resolve<ILoginReader>("loginReader"),
                container.Resolve<IProfiler>()
            ));

            // users
            container.Register<IRepositoryWriter<UserRecord>>(new RepositoryProfiler<UserRecord>(
                container.Resolve<IRepositoryReader<UserRecord>>("userReader"),
                container.Resolve<IRepositoryWriter<UserRecord>>("userWriter"),
                container.Resolve<IProfiler>()
            ));
            container.Register<IRepositoryReader<UserRecord>>(new RepositoryProfiler<UserRecord>(
                container.Resolve<IRepositoryReader<UserRecord>>("userReader"),
                container.Resolve<IRepositoryWriter<UserRecord>>("userWriter"),
                container.Resolve<IProfiler>()
            ));
        }
        private void ConfigureEvents(TinyIoCContainer container)
        {
            var entityFrameworkPublisher = container.Resolve<EntityFrameworkPublisher>();
            
            /* add subscribers */
            entityFrameworkPublisher.AddSubscriber(new Archive());

            /* register the decorators */
            var roomWriter = container.ResolveAll<IRepositoryWriter<RoomRecord>>().Last();
            var messageWriter = container.ResolveAll<IRepositoryWriter<MessageRecord>>().Last();

            container.Register<IRepositoryWriter<RoomRecord>>(new RepositoryEventPublishing<RoomRecord>(
                roomWriter,
                entityFrameworkPublisher
            ));

            container.Register<IRepositoryWriter<MessageRecord>>(new RepositoryEventPublishing<MessageRecord>(
                messageWriter,
                entityFrameworkPublisher
            ));
        }
    }
}