using System;
using ChatApplication.Data.Contracts;
using ChatApplication.Data.Contracts.Models;
using ChatApplication.Data.Contracts.Persistence;
using ChatApplication.Security.Contracts;
using ChatApplication.User.Contracts;

namespace ChatApplication.User
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _uow;
        private readonly IPasswordService _passwordService;

        public UserService(IUnitOfWork uow, IPasswordService passwordService)
        {           
            _uow = uow;
            _passwordService = passwordService;
        }

        public LoginRecord ValidateLogin(LoginRecord login, string password)
        {
            var isValid = _passwordService.ValidatePassword(login.Password, password);
            return isValid ? login : null;
        }

        public void Register(Registration registration)
        {
            var user = new UserRecord
            {
                Name = registration.Name,
                Login = new LoginRecord
                {
                    Password = _passwordService.GeneratePasswordHash(registration.Password),
                    Username = registration.Username
                }
            };
            _uow.Users.Add(user);
            _uow.SaveChanges();
        }

    }
}
