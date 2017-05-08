using ChatApplication.Mapper;
using ChatApplication.Security.Contracts;
using Nancy;
using Nancy.ModelBinding;

namespace ChatApplication.API.Modules.Room
{
    public class AuthModule : NancyModule
    {
        private readonly ISecurityService _securityService;
        private readonly IModelMapper _mapper;

        public AuthModule(ISecurityService securityService, IModelMapper mapper) : base("api/v1/auth")
        {
            _securityService = securityService;
            _mapper = mapper;
            Post["/login"] = _ =>
            {
                var login = this.Bind<LoginRequest>();
                return ValidateLogin(login);
            };
        }

        private object ValidateLogin(LoginRequest loginRequest)
        {
            var login = _securityService.LoginByNameOrDefault(loginRequest.Email);

            if (login == null) return HttpStatusCode.Unauthorized;

            var loginToken = _securityService.LoginTokenOrDefault(login, loginRequest.Password);
            var user = _mapper.UserRecordToUser(login.User);

            if (loginToken == null) return HttpStatusCode.Unauthorized;
            var encodedToken = _securityService.EncodeToken(loginToken);
            return Negotiate
                .WithModel(new { encodedToken, user })
                .WithStatusCode(HttpStatusCode.OK);
        }
    }
}