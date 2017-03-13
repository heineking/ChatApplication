using ChatApplication.Security.Contracts;
using Nancy;
using Nancy.ModelBinding;

namespace ChatApplication.API.Modules
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
            var loginToken = _securityService.ValidateLogin(loginRequest.Email, loginRequest.Password);
            if (loginToken != null)
            {
                return _securityService.EncodeToken(loginToken);
            }
            return HttpStatusCode.BadRequest;
        }
    }
}