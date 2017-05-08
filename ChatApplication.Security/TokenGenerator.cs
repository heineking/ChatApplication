using System;
using System.Linq;
using ChatApplication.Data.Contracts.Models;
using ChatApplication.Infrastructure.Contracts;
using ChatApplication.Security.Contracts;

namespace ChatApplication.Security
{
    public class TokenGenerator : ITokenGenerator
    {
        private readonly int _exp;
        private readonly string _iss;

        public TokenGenerator(IApplicationSettings appSettings)
        {
            _exp = int.Parse(appSettings.GetValue("Token:ExpHrs"));
            _iss = appSettings.GetValue("Token:Iss");
        }

        public LoginToken CreateLoginToken(LoginRecord loginRecord)
        {
            return new LoginToken
            {
                Exp = DateTime.Now.AddHours(_exp),
                Iss = _iss,
                /* user information */
                LoginName = loginRecord.Username,
                UserId = loginRecord.UserId,
                Claims = loginRecord.User.UserClaims.Select(uc => uc.Claim.ClaimName).ToList(),
                UserName = loginRecord.User.Name
            };
        }
    }
}
