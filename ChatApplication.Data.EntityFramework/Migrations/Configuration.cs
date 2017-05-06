using System.Collections.Generic;
using System.Security.Cryptography;
using ChatApplication.Data.Contracts.Models;
using ChatApplication.Data.EntityFramework.ContextEF;
using ChatApplication.Infrastructure.Contracts;
using ChatApplication.Security;

namespace ChatApplication.Data.EntityFramework.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<ChatApplication.Data.EntityFramework.ContextEF.ChatContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }
        protected override void Seed(ChatContext context)
        {
            var passwordService = new PasswordService(new ConfigSettings(), new RNGCryptoServiceProvider());
            var claims = new List<ClaimRecord>
            {
                new ClaimRecord
                {
                    ClaimName = "admin",
                    ClaimId = 1
                },
                new ClaimRecord
                {
                    ClaimName = "user",
                    ClaimId = 2
                }
            };
            claims.ForEach(claim => context.Claims.Add(claim));
            context.SaveChanges();

            var users = new List<UserRecord>
            {
                new UserRecord
                {
                    Name = "User A",
                    UserId = 1
                },
                new UserRecord
                {
                    Name = "User B",
                    UserId = 2
                },
                new UserRecord
                {
                    Name = "Admin",
                    UserId = 3
                }
            };
            users.ForEach(user => context.Users.Add(user));
            context.SaveChanges();

            var userClaims = new List<UserClaimsRecord>
            {
                new UserClaimsRecord
                {
                    UserId = 1,
                    ClaimId = 2,
                },
                new UserClaimsRecord
                {
                    UserId = 2,
                    ClaimId = 2
                },
                new UserClaimsRecord
                {
                    UserId = 3,
                    ClaimId = 1
                },
                new UserClaimsRecord
                {
                    UserId = 3,
                    ClaimId = 2
                }
            };
            userClaims.ForEach(uc => context.UserClaims.Add(uc));
            context.SaveChanges();

            var logins = new List<LoginRecord>
            {
                new LoginRecord
                {
                    Password = passwordService.GeneratePasswordHash("password"),
                    Username = "UserA@gmail.com",
                    UserId = 1
                },
                new LoginRecord
                {
                    Password = passwordService.GeneratePasswordHash("password"),
                    Username = "UserB@gmail.com",
                    UserId = 2
                },
                new LoginRecord
                {
                    Password = passwordService.GeneratePasswordHash("admin"),
                    Username = "admin",
                    UserId = 3
                }
            };
            logins.ForEach(login => context.Logins.Add(login));
            context.SaveChanges();
            var rooms = new List<RoomRecord>
            {
                new RoomRecord("Senior Seminar", "University of Akron course to prepare CS students for the real world", 1),
                new RoomRecord("Operating Systems", "Take this class and learn how to write your own DOS operating system!", 2)
            };
            rooms.ForEach(room => context.Rooms.Add(room));
            context.SaveChanges();

            var messages = new List<MessageRecord>
            {
                new MessageRecord
                {
                    Text = "We are presenting our term projects this week",
                    PostedDate = DateTime.Now.AddMinutes(-30),
                    Room = rooms[0],
                    User = users[0]
                },
                new MessageRecord
                {
                    Text = "The name of Emil's project is Adaptive Chat Application",
                    PostedDate = DateTime.Now,
                    Room = rooms[0],
                    User = users[0]
                },
                new MessageRecord
                {
                    Text = "There is a project due on Friday",
                    PostedDate = DateTime.Now,
                    Room = rooms[1],
                    User = users[1]
                },
                new MessageRecord
                {
                    Text = "It is building out our own Linux shell",
                    PostedDate = DateTime.Now.AddMinutes(-45),
                    Room = rooms[1],
                    User = users[1]
                }
            };
            messages.ForEach(message => context.Messages.Add(message));
            context.SaveChanges();
        }
    }
}
