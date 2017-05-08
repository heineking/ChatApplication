using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChatApplication.Infrastructure.Contracts;

namespace ChatApplication.Data.Contracts.Models
{
    public class ClaimRecord
    {
        [JsonLogging]
        public long ClaimId { get; set; }
        [JsonLogging]
        public string ClaimName { get; set; }
        public List<UserClaimsRecord> UserClaims { get; set; }
    }
}
