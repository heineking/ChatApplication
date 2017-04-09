using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChatApplication.Data.Contracts;
using ChatApplication.Data.Contracts.Models;
using ChatApplication.Data.Contracts.Persistence;
using ChatApplication.Security.Contracts;

namespace ChatApplication.Security
{
    public class BruteForceDecorator : ISecurityService
    {
        private readonly ISecurityService _security;
        private readonly ILoginUnitOfWork _uow;
        private readonly int _maxAttempts;

        public BruteForceDecorator(ISecurityService security, ILoginUnitOfWork uow, IApplicationSettings appSettings)
        {
            _security = security;
            _uow = uow;
            _maxAttempts = int.Parse(appSettings.GetValue("Login:MaxAttempts"));
        }
        public LoginRecord LoginByNameOrDefault(string name)
        {
            var loginRecord = _security.LoginByNameOrDefault(name);

            // return null if the record has reached the max attempts or we didn't
            // find the login record
            if (loginRecord == null) return null;
            if (loginRecord.LoginAttempts >= _maxAttempts) return null;
            
            // we found a login in record so let's increment the attempt and
            // return it.
            loginRecord.LoginAttempts += 1;
            return loginRecord;
        }

        public LoginToken LoginTokenOrDefault(LoginRecord login, string password)
        {
            // there was no login so immediately return
            if (login == null) return null;

            // call down to the delegate which handles verifying the password and
            // generating the token from our API key.
            var token = _security.LoginTokenOrDefault(login, password);
            if (token != null) return token;

            // The password wasn't valid so we need to save the login attempt
            _uow.Logins.Update(login);
            _uow.SaveChanges();
            return null;
        }

        public string EncodeToken(LoginToken token)
        {
            return _security.EncodeToken(token);
        }

        public LoginToken DecodeToken(string jwt)
        {
            return _security.DecodeToken(jwt);
        }
    }
}
