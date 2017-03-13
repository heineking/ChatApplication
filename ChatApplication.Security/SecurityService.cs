using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChatApplication.Security.Contracts;
using ChatApplication.Data.Contracts;
using ChatApplication.Data.Contracts.Models;
using ChatApplication.Data.Contracts.Persistence;
using ChatApplication.Data.Contracts.Repositories;
using ChatApplication.Infrastructure.Contracts;
using JWT;
using Newtonsoft.Json;

namespace ChatApplication.Security
{
    public class SecurityService : ISecurityService
    {
        private readonly string _key;
        private readonly IJwtDecoder _decoder;
        private readonly IJwtEncoder _encoder;
        private readonly int _exp;
        private readonly ILoginReader _loginReader;
        private readonly IModelMapper _mapper;

        public SecurityService(IApplicationSettings appSettings, ILoginReader loginReader, IJwtEncoder encoder, IJwtDecoder decoder, IModelMapper mapper)
        {
            _key = appSettings.GetValue("ApiKey");
            _decoder = decoder;
            _encoder = encoder;
            _exp = int.Parse(appSettings.GetValue("Token:ExpHrs"));
            _loginReader = loginReader;
            _mapper = mapper;
        }
        public LoginToken ValidateLogin(string username, string password)
        {
            var loginRecord = _loginReader.ValidateLogin(username, password);
            if (loginRecord == null)
            {
                return null;
            }
            // todo: this should be moved out of the service. Violates SRP
            return new LoginToken
            {
                Exp = DateTime.Now.AddHours(_exp).Ticks.ToString(),
                Iss = "issuer",
                LoginName = loginRecord.Login,
                UserId = loginRecord.UserId
            };
        }

        public string EncodeToken(LoginToken token)
        {
            return _encoder.Encode(token, _key);
        }

        public LoginToken DecodeToken(string jwt)
        {
            var decoded = _decoder.Decode(jwt, _key, verify: true);
            return JsonConvert.DeserializeObject<LoginToken>(decoded);
        }
    }
}
