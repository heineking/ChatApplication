using System;
using System.Data.Entity;
using System.Diagnostics;
using System.EnterpriseServices.Internal;
using ChatApplication.API.Extensions;
using ChatApplication.API.PubSub;
using ChatApplication.API.User;
using ChatApplication.Data.Contracts;
using ChatApplication.Data.Contracts.Events;
using ChatApplication.Data.Contracts.Models;
using ChatApplication.Data.Contracts.Persistence;
using ChatApplication.Data.Contracts.Repositories;
using ChatApplication.Data.EntityFramework.ContextEF;
using ChatApplication.Data.EntityFramework.Events;
using ChatApplication.Data.EntityFramework.Logging;
using ChatApplication.Data.EntityFramework.Persistence;
using ChatApplication.Data.EntityFramework.Repositories;
using ChatApplication.Infrastructure.Contracts;
using ChatApplication.Logging;
using ChatApplication.Logging.Profiling;
using ChatApplication.Mapper;
using ChatApplication.Password;
using ChatApplication.Security;
using ChatApplication.Security.Contracts;
using ChatApplication.Service;
using ChatApplication.Service.Contracts;
using JWT;
using JWT.Algorithms;
using JWT.Serializers;
using Nancy;
using Nancy.Authentication.Stateless;
using Nancy.Bootstrapper;
using Nancy.TinyIoc;
using Newtonsoft.Json;

namespace ChatApplication.API
{
    public class Bootstrapper : DefaultNancyBootstrapper
    {
        protected override void RequestStartup(TinyIoCContainer container, IPipelines pipelines, NancyContext context)
        {
            var securityService = container.Resolve<ISecurityService>();
            // TODO: remove this to own class
            var configuration = new StatelessAuthenticationConfiguration(ctx =>
            {
                var jwt = ctx.Request.Headers.Authorization;
                try
                {
                    var loginToken = securityService.DecodeToken(jwt);
                    if (loginToken != null)
                    {
                        return new UserIdentity
                        {
                            UserName = loginToken.LoginName,
                            UserId = loginToken.UserId,
                            Claims = loginToken.Claims,
                            Name = loginToken.UserName
                        };
                    }
                }
                catch (Exception)
                {
                    return null;
                }
                return null;
            });

            StatelessAuthentication.Enable(pipelines, configuration);

            pipelines.EnableCORS();
        }

        protected override void ApplicationStartup(TinyIoCContainer container, IPipelines pipelines)
        {
            // set up the log4net
            log4net.Config.XmlConfigurator.Configure();
            Log4NetExtensions.Configure();

            // set up json.net config
            BootstraperExtensions.ConfigureJsonNet();
        }

        protected override void ConfigureApplicationContainer(TinyIoCContainer container)
        {
            base.ConfigureApplicationContainer(container);

            /* ef logger */
            container.Register<EntityFrameworkLogger>();

            /* pubsub */
            container.Register<Publisher>();

            /* app helper classes */
            container.Register<IApplicationSettings, ConfigSettings>();
            container.Register<JsonSerializer, CustomJsonSerializer>();

            /* security */
            container.Register<IJwtAlgorithm, HMACSHA256Algorithm>();
            container.Register<IJsonSerializer, JsonNetSerializer>();
            container.Register<IJwtEncoder, JwtEncoder>();
            container.Register<IDateTimeProvider, UtcDateTimeProvider>();
            container.Register<IJwtValidator, JwtValidator>();
            container.Register<IJwtDecoder, JwtDecoder>();
        }

        protected override void ConfigureRequestContainer(TinyIoCContainer container, NancyContext context)
        {
            base.ConfigureRequestContainer(container, context);
            var appSettings = container.Resolve<ConfigSettings>();
            var connStr = appSettings.GetConnection("context");
            var profile = bool.Parse(appSettings.GetValue("Logging:Profile"));
            var traceEf = bool.Parse(appSettings.GetValue("Logging:EF:Trace"));


            // register the dependencies
            container.Register<DbContext>(new ChatContext(connStr));

            /* set up the ef logger */
            if (traceEf)
            {
                var dbContext = container.Resolve<DbContext>();
                var entityFrameworkLogger = container.Resolve<EntityFrameworkLogger>();
                dbContext.Database.Log = s => entityFrameworkLogger.Log(s);
            }

            /* model mappers */
            container.Register<IModelMapper, ModelMapper>();

            /* profiler */
            container.Register<IStopwatch>((c, p) => new StopwatchAdapter(new Stopwatch()));
            container.Register<IProfiler>((c, p) => new Profiler(c.Resolve<IStopwatch>()));

            /* repositories */

            // readers
            container.Register<IRepositoryReader<RoomRecord>, RepositoryEF<RoomRecord>>();
            container.Register<IRepositoryReader<MessageRecord>, RepositoryEF<MessageRecord>>();
            container.Register<IRepositoryReader<UserRecord>, RepositoryEF<UserRecord>>();
            container.Register<ILoginReader, LoginRespositoryEntityFramework>();

            // writers
            container.Register<IRepositoryWriter<RoomRecord>, RepositoryEF<RoomRecord>>("roomWriter");
            container.Register<IRepositoryWriter<MessageRecord>, RepositoryEF<MessageRecord>>();
            container.Register<IRepositoryWriter<UserRecord>, RepositoryEF<UserRecord>>();
            container.Register<IRepositoryWriter<LoginRecord>, LoginRespositoryEntityFramework>();

            /* pub sub */
            var publisher = container.Resolve<Publisher>();
            publisher.ConfigureEntityFrameworkDecorator(container);

            // repositories
            container.Register<IRoomRepository, RoomRepository>();
            container.Register<IUserRepository, UserRepository>();
            container.Register<IMessageRepository, MessageRepository>();
            container.Register<ILoginRepository, LoginRepository>();

            /* uow */
            container.Register<IUnitOfWork, EntityFrameworkUnitOfWork>();
            container.Register<ILoginUnitOfWork, LoginUnitOfWork>();

            /* services */
            container.Register<IRoomReader, RoomService>();
            container.Register<IRoomWriter, RoomService>();
            container.Register<IPasswordService, PasswordService>();

            /* register the base security service then tie it into the decorator */
            container.Register<ISecurityService, SecurityService>("baseService");

            container.Register<ISecurityService>(new BruteForceDecorator(
                container.Resolve<ISecurityService>("baseService"),
                container.Resolve<ILoginUnitOfWork>(),
                container.Resolve<IApplicationSettings>())
                );
        }
    }
}