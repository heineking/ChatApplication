using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChatApplication.Infrastructure.Contracts;
using Newtonsoft.Json;

namespace ChatApplication.Data.Contracts.Models
{
    public class LoginRecord
    {
        [JsonLogging]
        public long UserId { get; set; }
        [JsonLogging]
        public string Username { get; set; }
        [JsonIgnore]
        public string Password { get; set; }
        [JsonLogging]
        public int LoginAttempts { get; set; }

        public virtual UserRecord User { get; set; }
    }
}
