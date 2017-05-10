using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Web;
using ChatApplication.Data.Contracts.Events;
using ChatApplication.Data.Contracts.Models;
using ChatApplication.Data.Contracts.Repositories;
using ChatApplication.Data.EntityFramework.Repositories;
using ChatApplication.Infrastructure.Contracts;
using ChatApplication.Infrastructure.Contracts.Events;
using ChatApplication.Logging;
using ChatApplication.Logging.Profiling;
using ChatApplication.Service.Contracts;
using ChatApplication.Syncronization.Archive;
using Nancy.TinyIoc;

namespace ChatApplication.API.Configure
{
    public class Decorators
    {
        private readonly bool _profiling;
        private readonly bool _enableArchiving;
        private readonly bool _enableCrmSync;

        public Decorators(IApplicationSettings appSettings)
        {
            _profiling = bool.Parse(appSettings.GetValue("Logging:Profile"));
            _enableArchiving = bool.Parse(appSettings.GetValue("Repo:EnableArchiving"));
            _enableCrmSync = bool.Parse(appSettings.GetValue("CRM:EnableSync"));
        }

        public void Configure(TinyIoCContainer container)
        {
            ConfigureBaseImplementations(container);
            if (_profiling) ConfigureProfiling(container);
            ConfigureDataEventPublisher(container);
            ConfigureRequestEventPublisher(container);
        }

        private static void ConfigureBaseImplementations(TinyIoCContainer container)
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

        private static void ConfigureProfiling(TinyIoCContainer container)
        {
            container.Register<IStopwatch>((c, p) => new StopwatchAdapter(new Stopwatch()));
            container.Register<IProfiler>((c, p) => new Profiler(c.Resolve<IStopwatch>()));
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
            ));

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
        private void ConfigureDataEventPublisher(TinyIoCContainer container)
        {
            // data events
            var eventPublisher = container.Resolve<IEventPublisher>();
            if (_enableArchiving) eventPublisher.AddSubscriber(container.Resolve<IEventSubscriber>("archiveSubscriber"));
            if (_enableCrmSync) eventPublisher.AddSubscriber(container.Resolve<IEventSubscriber>("crmSyncSubscriber"));
            var impl = new RepoImplementations(container);
            // rooms
            container.Register<IRepositoryWriter<RoomRecord>>(new RepositoryEventPublishing<RoomRecord>(
                impl.RoomWriter,
                eventPublisher
            ));
            // messages
            container.Register<IRepositoryWriter<MessageRecord>>(new RepositoryEventPublishing<MessageRecord>(
                impl.MessageWriter,
                eventPublisher
            ));
            // logins
            container.Register<IRepositoryWriter<LoginRecord>>(new RepositoryEventPublishing<LoginRecord>(
                impl.LoginWriter,
                eventPublisher
            ));
        }

        private void ConfigureRequestEventPublisher(TinyIoCContainer container)
        {
            var eventPublisher = container.Resolve<IEventPublisher>();
            eventPublisher.AddSubscriber(container.Resolve<IEventSubscriber>("requestSubscriber"));
        }
    }
}