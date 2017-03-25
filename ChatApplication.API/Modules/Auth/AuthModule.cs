using ChatApplication.Security.Contracts;
using Nancy;
using Nancy.Cookies;
using Nancy.ModelBinding;

namespace ChatApplication.API.Modules.Room
{
    public class AuthModule : NancyModule
    {
        private readonly ISecurityService _securityService;

        public AuthModule(ISecurityService securityService) : base("api/v1/auth")
        {
            _securityService = securityService;
            Post["/login"] = _ => {
                var login = this.Bind<LoginRequest>();
                return ValidateLogin(login);
            };
        }

        private object ValidateLogin(LoginRequest loginRequest)
        {
            var loginToken = _securityService.LoginTokenOrDefault(loginRequest.Email, loginRequest.Password);
            if (loginToken == null) return HttpStatusCode.Unauthorized;
            var encodedToken = _securityService.EncodeToken(loginToken);
            return Negotiate
                .WithModel(encodedToken)
                .WithStatusCode(HttpStatusCode.OK);
        }
    }
}