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

        /// <summary>
        /// Returns the LoginRecord associated with the passed in username. It also updates
        /// the loginAttempts on the record to keep track of how many login attempts were made for
        /// the record.
        /// </summary>
        /// <param name="name">The name of a login record</param>
        /// <returns>LoginRecord or Null</returns>
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
        /// <summary>
        /// Validates the passed in LoginRecord against the passed in password. Will update
        /// the LoginRecord attempts if the password was incorrect.
        /// </summary>
        /// <param name="login">LoginRecord to get the associated LoginToken</param>
        /// <param name="password">Password associated with the LoginRecord</param>
        /// <returns>LoginToken or Null</returns>
        public LoginToken LoginTokenOrDefault(LoginRecord login, string password)
        {
            // there was no login so immediately return
            if (login == null) return null;

            // call down to the delegate which handles verifying the password and
            // generating the token from our API key.
            var token = _security.LoginTokenOrDefault(login, password);
            // reset the login attempts if had a valid password
            if (token != null) login.LoginAttempts = 0;
            
            // sync the login attempts
            _uow.Logins.Update(login);
            _uow.SaveChanges();
            return token;
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
