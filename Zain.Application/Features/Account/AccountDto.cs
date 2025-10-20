using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zain.Application.Features.Account
{
    public class AccountDto
    {
        public class AccountRegisterDto
        {
            public string Name { get; set; }
            public string PhoneNumber { get; set; }
            public string Email { get; set; }
            public string Password { get; set; }
        //    public string RoleName { get; set; }
        }

        public class AccountResponseDto
        {
            public string FullName { get; set; }
            public string Email { get; set; }
            public string PhoneNumber { get; set; }
            public List<string>? Roles { get; set; }
            public string Token { get; set; }
            public DateTime ExpiresOn { get; set; }
            public string Type { get; set; }

        
            //// [JsonIgnore]
            //public string? RefreshToken { get; set; }
            //public DateTime? RefreshTokenExpiration { get; set; }
        }

        public class AccountGetTokenDto
        {
            public string Email { get; set; }
            public string Password { get; set; }
        }
    }
}
