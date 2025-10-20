using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Zain.Application.Contracts;
using Zain.Application.Features;
using Zain.Domain.Entities;
using Zain.Domain.TokenEntities;
using static Zain.Application.Features.Account.AccountDto;

namespace Zain.Persistance.Repositories
{
    public class AuthService : IAuthService
    {
        private readonly JWT jwt;
        private readonly UserManager<ApplicationUser> userManager;

        public AuthService(IOptions<JWT> jwt, UserManager<ApplicationUser> userManager)
        {
            this.jwt = jwt.Value;
            this.userManager = userManager;
        }
        public async Task<GeneralResponse<AccountResponseDto>> RegisterAsync(AccountRegisterDto request,string type)
        {
            if (await userManager.FindByEmailAsync(request.Email) is not null)
                return  GeneralResponse<AccountResponseDto>.FailResponse("Email Is already Recoerd");
            if (await userManager.FindByNameAsync(request.PhoneNumber) is not null)
                return GeneralResponse<AccountResponseDto>.FailResponse("Number Is already Recoerd");


            var user = new ApplicationUser()
            {
                FullName = request.Name,
                PhoneNumber = request.PhoneNumber,
                Email = request.Email,
                UserName = request.Email,
                TypeUser = type
            };


            var result = await userManager.CreateAsync(user, request.Password);

            if (!result.Succeeded)
            {
                var errors = String.Empty;

                foreach (var error in result.Errors)
                {
                    errors += $"{error.Description},";
                }
                return GeneralResponse<AccountResponseDto>.FailResponse(errors);
            }
           

            var us = await userManager.FindByEmailAsync(request.Email);

            if (type == "client")
                await userManager.AddToRoleAsync(us, "client");
            else
                await userManager.AddToRoleAsync(us, "user");




            //   await userManager.UpdateAsync(us);

            var jwtSecurityToken = await CreateJwtToken(us);
            var role = await userManager.GetRolesAsync(user);

             var roles = await userManager.GetRolesAsync(us);
            var response = new AccountResponseDto
            {
                Email = request.Email,
                PhoneNumber = request.PhoneNumber,
                Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
                FullName = user.FullName,
                Type = user.TypeUser,
                ExpiresOn = jwtSecurityToken.ValidTo,
                Roles = role.ToList()


                //RefreshToken = generateRefreshToken.Token,
                //RefreshTokenExpiration = generateRefreshToken.ExpiresOn
            };

            return GeneralResponse<AccountResponseDto>.SuccessResponse(response);
        }

        public async Task<GeneralResponse<AccountResponseDto>> GetTokenAsync(AccountGetTokenDto request)
        {
            var user = await userManager.FindByEmailAsync(request.Email);

            if (user is null || !await userManager.CheckPasswordAsync(user, request.Password))
            {
                return GeneralResponse<AccountResponseDto>.FailResponse("Email Or Password is incorect!");
            }

            var role = await userManager.GetRolesAsync(user);

            var response = new AccountResponseDto();

            var jwtSecurityToken = await CreateJwtToken(user);
            response.Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
            response.Email = user.Email;
            response.ExpiresOn = jwtSecurityToken.ValidTo;
            response.Roles = role.ToList();
            response.FullName = user.FullName;
            response.PhoneNumber = user.PhoneNumber;
            response.Type = user.TypeUser;


            //if (user.RefreshTokens.Any(act => act.IsActive))
            //{
            //    var ActiveToken = user.RefreshTokens.First(act => act.IsActive);
            //    response.RefreshToken = ActiveToken.Token;
            //    response.RefreshTokenExpiration = ActiveToken.ExpiresOn;

            //}
            //else
            //{
            //    var newRefreshToken = await GenerateRefreshToken();

            //    response.RefreshToken = newRefreshToken.Token;
            //    response.RefreshTokenExpiration = newRefreshToken.ExpiresOn;

            //    user.RefreshTokens.Add(newRefreshToken);
            //    await userManager.UpdateAsync(user);
            //}

            return GeneralResponse<AccountResponseDto>.SuccessResponse(response);
        }
        //public async Task<AccountResponse> CheckOrCreateRefreshTokenAsync(string refreshToken)
        //{

        //    var user = await userManager.Users.SingleOrDefaultAsync(tok => tok.RefreshTokens.Any(rt => rt.Token == refreshToken));

        //    var response = new AccountResponse();
        //    if (user == null)
        //    {
        //        response.Message = "Invalid token";
        //        return response;
        //    }

        //    var exictToken = user.RefreshTokens.Single(t => t.Token == refreshToken);

        //    if (!exictToken.IsActive)
        //    {
        //        response.Message = "Inactive token";
        //        return response;
        //    }

        //    // make old refresh token Revoked
        //    exictToken.RevokedOn = DateTime.UtcNow;

        //    // Add the new Token 
        //    var newRefreshToken = await GenerateRefreshToken();
        //    user.RefreshTokens.Add(newRefreshToken);
        //    await userManager.UpdateAsync(user);

        //    // Generate Token Authorization
        //    var jwtToken = await CreateJwtToken(user);
        //    response.Token = new JwtSecurityTokenHandler().WriteToken(jwtToken);

        //    // complete attributes of AuthModel
        //    response.IsAuthenticed = true;
        //    response.Email = user.Email;
        //    response.RefreshToken = newRefreshToken.Token;
        //    response.RefreshTokenExpiration = newRefreshToken.ExpiresOn;


        //    return response;

        //}


        #region Token
        //private async Task<RefreshToken> GenerateRefreshToken()
        //{
        //    var randomNumber = new byte[32];

        //    using var Generator = new RNGCryptoServiceProvider();
        //    Generator.GetBytes(randomNumber);

        //    return new RefreshToken
        //    {
        //        Token = Convert.ToBase64String(randomNumber),
        //        ExpiresOn = DateTime.UtcNow.AddMinutes(jwt.DurationInMinutes),
        //        CreatedOn = DateTime.UtcNow
        //    };
        //}

        private async Task<JwtSecurityToken> CreateJwtToken(ApplicationUser user)
        {
            var userClaims = await userManager.GetClaimsAsync(user);
            var roles = await userManager.GetRolesAsync(user);
            var roleClaims = new List<Claim>();

            foreach (var role in roles)
                roleClaims.Add(new Claim("roles", role));

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("uid", user.Id)
            }
            .Union(userClaims)
            .Union(roleClaims);

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt.Key));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: jwt.Issuer,
                audience: jwt.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddHours(jwt.DurationInMinutes),
                signingCredentials: signingCredentials);

            return jwtSecurityToken;
        }
        #endregion
    }
}
