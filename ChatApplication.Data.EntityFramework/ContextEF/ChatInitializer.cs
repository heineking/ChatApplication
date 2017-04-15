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
                    Name = "User A",
                    UserId = 1
                },
                new UserRecord
                {
                    Name = "User B",
                    UserId = 2
                }
            };
            users.ForEach(user => context.Users.Add(user));
            context.SaveChanges();

            var rooms = new List<RoomRecord>
            {
                new RoomRecord("Senior Seminar", "blah", 1),
                new RoomRecord("Operating Systems", "bar", 2)
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
