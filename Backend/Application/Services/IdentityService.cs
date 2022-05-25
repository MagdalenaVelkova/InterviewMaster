using InterviewMaster.Domain.Identity;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace InterviewMaster.Application.Services
{
    public class IdentityService


    {
        private readonly IUserProfileService userProfileService;
        private readonly JwtSecurityTokenHandler tokenHandler;
        private readonly byte[] tokenkey;
        

        public IdentityService(IUserProfileService userProfileService)
        {
            this.userProfileService = userProfileService;
            this.tokenHandler = new JwtSecurityTokenHandler();
            // var key = ConfigurationManager.GetSection("JwtKey").ToString();
            this.tokenkey = Encoding.ASCII.GetBytes("VrFPOluCe4zkUnnFA8Q5gIxSh6T7u6MO");
        }
        public string? Authenticate(Credentials credentials)
        {
            // var passwordHash = credentials.PasswordHash
            var userId = userProfileService.GetUserId(credentials);

            if (userId == null)
            {
                return null;
            }
            else
            { 
            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(new Claim[]{
            new Claim(ClaimTypes.Sid, userId),

                }),

                Expires = DateTime.UtcNow.AddHours(1),

                SigningCredentials = new SigningCredentials(
            new SymmetricSecurityKey(tokenkey), SecurityAlgorithms.HmacSha256Signature)

            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
            }
        }

        public string GetUserIdFromToken(string token)
        {
            var tokenClaims = tokenHandler.ReadJwtToken(token).Claims;
            var userId = tokenClaims.First(x => x.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/sid").Value;
            return userId;
        }

        public async Task<bool> isAuthorised(string token)
        {
            var validationParameters = new TokenValidationParameters();
            var tokenValidationResult = await tokenHandler.ValidateTokenAsync(token, validationParameters);

            if (tokenValidationResult.IsValid)
            {
                return true;
            }
            else
            {
                return false;
            }

        }
    }
}
