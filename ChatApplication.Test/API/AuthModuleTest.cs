using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using ChatApplication.API.Modules.Room;
using ChatApplication.Data.Contracts.Models;
using ChatApplication.Data.Contracts.Repositories;
using ChatApplication.Security.Contracts;
using Moq;
using Nancy;
using Nancy.Testing;
using NUnit.Framework;

namespace ChatApplication.Test.API
{

    [TestFixture]
    public class AuthModuleTest
    {
        private Browser _browser;
        private Func<Action<BrowserContext>, BrowserResponse> _loginAction;

        [SetUp]
        public void SetUp()
        {
            var mockedSecurityService = new Mock<ISecurityService>();
            mockedSecurityService.Setup(s => s.LoginTokenOrDefault(new LoginRecord(), "wrong")).Returns(() => null);
            mockedSecurityService.Setup(s => s.LoginTokenOrDefault(new LoginRecord(), "password")).Returns(() => new LoginToken());

            _browser = new Browser(cfg =>
            {
                
                cfg.Module<AuthModule>();
                cfg.Dependency(mockedSecurityService.Object);
            });
            _loginAction = action => _browser.Post("/api/v1/auth/login", action);
        }

        [Test]
        public void Should_Return_Unauthorized_When_Login_Is_Invalid()
        {
            // arrange
            Action<BrowserContext> postWithInvalidCredentials = bctx =>
            {
                bctx.HttpRequest();
                bctx.FormValue("email", "invalid");
                bctx.FormValue("password", "wrong");
            };

            // act
            var result = _loginAction(postWithInvalidCredentials);

            // assert
            Assert.AreEqual(HttpStatusCode.Unauthorized, result.StatusCode);
        }

        [Test]
        public void Should_Return_Ok_When_Login_Is_Valid()
        {
            // arrange
            Action<BrowserContext> postWithValidCredentials = bctx =>
            {
                bctx.HttpRequest();
                bctx.Accept("application/json");
                bctx.FormValue("email", "user");
                bctx.FormValue("password", "password");
            };

            // act
            var result = _loginAction(postWithValidCredentials);

            // assert
            Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);
        }
    }
}
