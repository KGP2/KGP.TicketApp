using Microsoft.Identity.Client;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using static KGP.TicketApp.Model.Database.Tables.User;

namespace KGP.TicketApp.Backend.Helpers
{
    public static class JwtTokenHelper
    {
        #region Public methods
        public static string CreateToken(string email, string id, string key, string issuer, Types userType)
        {
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("Id", Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Sub, email),
                    new Claim(JwtRegisteredClaimNames.Email, email),
                    new Claim(JwtRegisteredClaimNames.Jti,
                    Guid.NewGuid().ToString())
                }),
                Expires = DateTime.UtcNow.AddMinutes(30),
                Issuer = issuer,
                Audience = $"{id};{userType}",
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key)), SecurityAlgorithms.HmacSha512Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var stringToken = tokenHandler.WriteToken(token);

            return stringToken;
        }
        public static bool IsIdValid(string JwtToken, string hashedId)
        {
            return hashedId == GetIdFromToken(JwtToken);
        }

        public static bool ClientTypeValidator(IEnumerable<string> audiences, SecurityToken securityToken, TokenValidationParameters validationParameters)
        {
            return Enum.Parse<Types>(audiences.First().Split(';').Last()) == Types.Client && audiences.Count() == 1;
        }
        public static bool OrganizerTypeValidator(IEnumerable<string> audiences, SecurityToken securityToken, TokenValidationParameters validationParameters)
        {
            return Enum.Parse<Types>(audiences.First().Split(';').Last()) == Types.Organizer && audiences.Count() == 1;
        }
        #endregion

        #region Private methods
        private static string GetIdFromToken(string JwtToken)
        {
            var encodedToken = new JwtSecurityToken(JwtToken);

            return encodedToken.Audiences.First().Split(';').First();
        }
        private static Types GetUserTypeFromToken(string JwtToken)
        {
            var encodedToken = new JwtSecurityToken(JwtToken);

            return Enum.Parse<Types>(encodedToken.Audiences.First().Split(';').Last());
        }
        #endregion

    }
}
