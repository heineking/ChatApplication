using System.Collections.Generic;
using ChatApplication.Data.Contracts.Models;
using ChatApplication.Data.EntityFramework.ContextEF;

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
            var users = new List<UserRecord>
            {
                new UserRecord
                {
                    Name = "Foo Bar",
                    UserId = Guid.NewGuid(),
                    Login = new LoginRecord
                    {
                        Login = "foo.bar@gmail.com",
                        Password = "secret1"
                    }
                },
                new UserRecord
                {
                    Name = "Bar Baz",
                    UserId = Guid.NewGuid(),
                    Login = new LoginRecord
                    {
                        Login = "bar.baz@gmail.com",
                        Password = "secret2"
                    }
                }
            };
            users.ForEach(user => context.Users.Add(user));
            context.SaveChanges();

            var rooms = new List<RoomRecord>
            {
                new RoomRecord("cats"),
                new RoomRecord("dogs")
            };
            rooms.ForEach(room => context.Rooms.Add(room));
            context.SaveChanges();

            var messages = new List<MessageRecord>
            {
                new MessageRecord
                {
                    Text = "Cats rule",
                    PostedDate = DateTime.Now.AddMinutes(-30),
                    Room = rooms[0],
                    User = users[0]
                },
                new MessageRecord
                {
                    Text = "Dogs drool",
                    PostedDate = DateTime.Now,
                    Room = rooms[0],
                    User = users[0]
                },
                new MessageRecord
                {
                    Text = "Dogs rule",
                    PostedDate = DateTime.Now,
                    Room = rooms[1],
                    User = users[1]
                },
                new MessageRecord
                {
                    Text = "Cats drool",
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
