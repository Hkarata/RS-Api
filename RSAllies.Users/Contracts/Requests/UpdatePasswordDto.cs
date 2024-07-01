using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSAllies.Users.Contracts.Requests
{
    public record UpdatePasswordDto
    {
        public Guid UserId { get; set; }
        public string Password { get; set; } = string.Empty;
    }
}
