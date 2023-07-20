using Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class User : IEntity
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public byte[]? PasswordHash { get; set; }
        public bool Status { get; set; }

        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenEndDate { get; set; }

        
        public string? PasswordResetToken { get; set; }
        public DateTime? ResetTokenExpire { get; set; }


        public List<UserOperationClaim> UserOperationClaims { get; set; }
    }
}
