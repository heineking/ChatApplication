using System;
using System.Configuration;
using System.Data.Entity;
using System.Net;
using System.Security.Claims;
using System.Security.Principal;
using ChatApplication.API.Extensions;
using ChatApplication.API.User;
using ChatApplication.Data.Contracts;
using ChatApplication.Data.Contracts.Models;
using ChatApplication.Data.Contracts.Persistence;
using ChatApplication.Data.Contracts.Repositories;
using ChatApplication.Data.Dapper.Repositories;
using ChatApplication.Data.EntityFramework.ContextEF;
using ChatApplication.Data.EntityFramework.Persistence;
using ChatApplication.Data.EntityFramework.Repositories;
using ChatApplication.DocumentStorage;
using ChatApplication.Infrastructure.Contracts;
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
                            UserId = loginToken.UserId
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
            // your customization goes here
        }

        protected override void ConfigureApplicationContainer(TinyIoCContainer container)
        {
            base.ConfigureApplicationContainer(container);

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

            // register the dependencies
            container.Register<DbContext>(new ChatContext(connStr));

            /* model mappers */
            container.Register<IModelMapper, ModelMapper>();

            /* repositories */

            // readers
            container.Register<IRepositoryReader<RoomRecord>, RepositoryEF<RoomRecord>>("roomReader");
            container.Register<IRepositoryReader<MessageRecord>, RepositoryEF<MessageRecord>>();
            container.Register<IRepositoryReader<UserRecord>, RepositoryEF<UserRecord>>();
            container.Register<ILoginReader, LoginRespositoryEntityFramework>();

            // writers
            container.Register<IRepositoryWriter<RoomRecord>, RepositoryEF<RoomRecord>>("roomWriter");
            container.Register<IRepositoryWriter<MessageRecord>, RepositoryEF<MessageRecord>>();
            container.Register<IRepositoryWriter<UserRecord>, RepositoryEF<UserRecord>>();
            container.Register<IRepositoryWriter<LoginRecord>, LoginRespositoryEntityFramework>();

            // register decorators
            container.Register<IRepositoryReader<RoomRecord>>(new RepositoryLogging<RoomRecord>(
                container.Resolve<IRepositoryReader<RoomRecord>>("roomReader"),
                container.Resolve<IRepositoryWriter<RoomRecord>>("roomWriter")
               ));
            container.Register<IRepositoryWriter<RoomRecord>>(new RepositoryLogging<RoomRecord>(
                container.Resolve<IRepositoryReader<RoomRecord>>("roomReader"),
                container.Resolve<IRepositoryWriter<RoomRecord>>("roomWriter")
                ));

            // repositories
            container.Register<IRoomRepository, RoomRepository>();
            container.Register<IUserRepository, UserRepository>();
            container.Register<IMessageRepository, MessageRepository>();
            container.Register<ILoginRepository, LoginRepository>();

            /* uow */
            container.Register<IUnitOfWork, EntityFrameworkUnitOfWork>();
            container.Register<ILoginUnitOfWork, LoginUnitOfWork>();

            /* services */
            container.Register<IRoomReader, MongoRoomStorage>();
            container.Register<IRoomWriter, MongoRoomStorage>();

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