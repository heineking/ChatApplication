using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChatApplication.Infrastructure.Contracts;

namespace ChatApplication.Data.Contracts.Models
{
    public class UserClaimsRecord
    {
        [JsonLogging]
        public long UserClaimsId { get; set; }
        [JsonLogging]
        public long UserId { get; set; }
        [JsonLogging]
        public long ClaimId { get; set; }
        public virtual UserRecord User { get; set;}
        public virtual ClaimRecord Claim { get; set; }
    }
}
