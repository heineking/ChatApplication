using System.Diagnostics;
using ChatApplication.Data.Contracts.Models;
using ChatApplication.Data.Contracts.Repositories;
using ChatApplication.Data.Contracts.Repositories.Decorators;
using ChatApplication.Data.EntityFramework.Repositories;
using ChatApplication.Infrastructure.Contracts;
using ChatApplication.Infrastructure.Contracts.Events;
using ChatApplication.Logging;
using ChatApplication.Logging.Profiling;
using Nancy.TinyIoc;

namespace ChatApplication.API.Configure
{
    public class Decorators
    {
        private readonly bool _profiling;

        public Decorators(IApplicationSettings appSettings)
        {
            _profiling = bool.Parse(appSettings.GetValue("Logging:Profile"));
        }

        public void Configure(TinyIoCContainer container)
        {
            ConfigureBaseImplementations(container);
            ConfigureDataEventPublisher(container);
            ConfigureRequestEventPublisher(container);
            if (_profiling) ConfigureProfiling(container);
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
            var profiler = container.Resolve<IProfiler>();
            var impl = new RepoImplementations(container);

            container.Register<IRepositoryReader<RoomRecord>>(new RepositoryProfiler<RoomRecord>(
                impl.RoomReader,
                impl.RoomWriter,
                profiler
            ));
            container.Register<IRepositoryWriter<RoomRecord>>(new RepositoryProfiler<RoomRecord>(
                impl.RoomReader,
                impl.RoomWriter,
                profiler
            ));

            // message
            container.Register<IRepositoryReader<MessageRecord>>(new RepositoryProfiler<MessageRecord>(
                impl.MessageReader,
                impl.MessageWriter,
                profiler
            ));

            container.Register<IRepositoryWriter<MessageRecord>>(new RepositoryProfiler<MessageRecord>(
                impl.MessageReader,
                impl.MessageWriter,
                profiler
            ));

            // login
            container.Register<IRepositoryWriter<LoginRecord>>(new RepositoryProfiler<LoginRecord>(
                impl.LoginReader,
                impl.LoginWriter,
                profiler
            ));

            container.Register<ILoginReader>(new LoginRepositoryProfiler(
                impl.LoginReader,
                profiler
            ));

            // users
            container.Register<IRepositoryWriter<UserRecord>>(new RepositoryProfiler<UserRecord>(
                impl.UserReader,
                impl.UserWriter,
                profiler
            ));
            container.Register<IRepositoryReader<UserRecord>>(new RepositoryProfiler<UserRecord>(
                impl.UserReader,
                impl.UserWriter,
                profiler
            ));
        }
        private void ConfigureDataEventPublisher(TinyIoCContainer container)
        {
            // data events
            var eventPublisher = container.Resolve<IEventPublisher>();
            eventPublisher.AddSubscriber(container.Resolve<IEventSubscriber>("outDataSync"));

            var impl = new RepoImplementations(container);

            container.Register<IRepositoryReader<RoomRecord>>(new RepositoryEventPublisher<RoomRecord>(
                impl.RoomReader,
                impl.RoomWriter,
                eventPublisher
            ));
            container.Register<IRepositoryWriter<RoomRecord>>(new RepositoryEventPublisher<RoomRecord>(
                impl.RoomReader,
                impl.RoomWriter,
                eventPublisher
            ));

            // message
            container.Register<IRepositoryReader<MessageRecord>>(new RepositoryEventPublisher<MessageRecord>(
                impl.MessageReader,
                impl.MessageWriter,
                eventPublisher
            ));

            container.Register<IRepositoryWriter<MessageRecord>>(new RepositoryEventPublisher<MessageRecord>(
                impl.MessageReader,
                impl.MessageWriter,
                eventPublisher
            ));

            // login
            container.Register<IRepositoryWriter<LoginRecord>>(new RepositoryEventPublisher<LoginRecord>(
                impl.LoginReader,
                impl.LoginWriter,
                eventPublisher
            ));


            // users
            container.Register<IRepositoryWriter<UserRecord>>(new RepositoryEventPublisher<UserRecord>(
                impl.UserReader,
                impl.UserWriter,
                eventPublisher
            ));
            container.Register<IRepositoryReader<UserRecord>>(new RepositoryEventPublisher<UserRecord>(
                impl.UserReader,
                impl.UserWriter,
                eventPublisher
            ));
        }

        private static void ConfigureRequestEventPublisher(TinyIoCContainer container)
        {
            var eventPublisher = container.Resolve<IEventPublisher>();
            eventPublisher.AddSubscriber(container.Resolve<IEventSubscriber>("requestSubscriber"));
        }
    }
}