using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChatApplication.Data.Contracts.Models;

namespace ChatApplication.Data.EntityFramework.ContextEF
{
    public class ChatInitializer : DropCreateDatabaseAlways<ChatContext>
    {
        protected override void Seed(ChatContext context)
        {
            var users = new List<UserRecord>
            {
                new UserRecord
                {
                    Name = "Foo Bar",
                    UserId = Guid.NewGuid()
                },
                new UserRecord
                {
                    Name = "Bar Baz",
                    UserId = Guid.NewGuid()
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
                    Room = rooms[0],
                    User = users[0]
                },
                new MessageRecord
                {
                    Text = "Dogs drool",
                    Room = rooms[0],
                    User = users[0]
                },
                new MessageRecord
                {
                    Text = "Dogs rule",
                    Room = rooms[1],
                    User = users[1]
                },
                new MessageRecord
                {
                    Text = "Cats drool",
                    Room = rooms[1],
                    User = users[1]
                }
            };
            messages.ForEach(message => context.Messages.Add(message));
            context.SaveChanges();
        }
    }
}
