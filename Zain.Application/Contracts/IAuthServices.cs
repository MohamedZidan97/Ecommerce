
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zain.Application.Features;
using static Zain.Application.Features.Account.AccountDto;

namespace Zain.Application.Contracts
{
    public interface IAuthService
    {
        Task<GeneralResponse<AccountResponseDto>> RegisterAsync(AccountRegisterDto request, string type);

        Task<GeneralResponse<AccountResponseDto>> GetTokenAsync(AccountGetTokenDto request);



        // Task<string> AddRoleAsync(AddRoleM roleM);

        //Task<AccountGeneralResponse> CheckOrCreateRefreshTokenAsync(string refreshToken);

        // if you want Revoked for token is active, use this method
        //Task<bool> RevokedTokenAsync(string refreshToken);
    }
}
