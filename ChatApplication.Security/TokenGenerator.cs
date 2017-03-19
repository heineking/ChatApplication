using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChatApplication.Data.Contracts;
using ChatApplication.Data.Contracts.Models;
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
                Exp = DateTime.Now.AddHours(_exp).Ticks.ToString(),
                Iss = _iss,
                LoginName = loginRecord.Login,
                UserId = loginRecord.UserId
            };
        }
    }
}
