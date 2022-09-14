using InterviewMaster.Domain.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace InterviewMaster.Application.Services
{
    public class IdentityService

    // Singh, S. 2022. Implement JWT Authentication in Asp.net Core 5 Web API [Token Base Auth]. Available at: https://codepedia.info/jwt-authentication-in-aspnet-core-web-api-token [Accessed: 8 May 2022].
    {
        private readonly IIdentityRepository identityRepository;
        private readonly JwtSecurityTokenHandler tokenHandler;
        private readonly byte[] tokenkey;


        public IdentityService(IIdentityRepository identityRepository, IConfiguration configuration)
        {
            this.identityRepository = identityRepository;
            this.tokenHandler = new JwtSecurityTokenHandler();
            this.tokenkey = Encoding.ASCII.GetBytes(configuration["Jwt:Key"]);
        }
        public string getToken(HttpContext httpContext)
        {
            StringValues bearerToken;
            bool tokenExists = httpContext.Request.Headers.TryGetValue("Authorization", out bearerToken);

            if (tokenExists)
            {
                var token = bearerToken.ToString().Split(" ")[1];

                GetUserIdFromToken(token.ToString());
                return token;
            }
            return null;
        }
        public string? Authenticate(Credentials credentials)
        {
            var userIdentity = identityRepository.GetUserIdentity(credentials);

            if (userIdentity == null)
            {
                return null;
            }
            else
            {
                var passwordIsVerified = AreEqual(credentials.Password, userIdentity.PasswordHash, userIdentity.PasswordSalt);
                if (passwordIsVerified)
                {
                    return GenerateToken(userIdentity);
                }
                else
                {
                    return null;
                }
            }
        }

        public UserAuth GenerateUserIdentityFromCredentials(Credentials credentials)
        {
            var salt = CreateSalt();
            var hashedPassword = GenerateHash(credentials.Password, salt);

            var userIdentity = new UserAuth()
            {
                Email = credentials.Email,
                PasswordHash = hashedPassword,
                PasswordSalt = salt,
            };

            return userIdentity;
        }

        public string GetUserIdFromToken(string token)
        {
            var tokenClaims = tokenHandler.ReadJwtToken(token).Claims;
            var userId = tokenClaims.First(x => x.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/sid").Value;
            return userId;
        }

        public async Task<bool> isAuthorised(string token)
        {
            var validationParameters = new TokenValidationParameters()
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(tokenkey)
            };
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
        public string GenerateToken(UserAuth userAuth)
        {
            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(new Claim[]{
            new Claim(ClaimTypes.Sid, userAuth.Id),

                }),

                Expires = DateTime.UtcNow.AddHours(1),

                SigningCredentials = new SigningCredentials(
            new SymmetricSecurityKey(tokenkey), SecurityAlgorithms.HmacSha256Signature)

            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
        private string CreateSalt()
        {
            //Generate a cryptographic random number.
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            byte[] buff = new byte[64];
            rng.GetBytes(buff);
            return Convert.ToBase64String(buff);
        }

        private string GenerateHash(string input, string salt)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(input + salt);
            SHA256Managed sHA256ManagedString = new SHA256Managed();
            byte[] hash = sHA256ManagedString.ComputeHash(bytes);
            return Convert.ToBase64String(hash);
        }

        private bool AreEqual(string plainTextInput, string hashedInput, string salt)
        {
            string newHashedPin = GenerateHash(plainTextInput, salt);
            return newHashedPin.Equals(hashedInput);
        }
    }
}
