using Microsoft.AspNetCore.Http;
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
        #region Fields
        public const string Client = "Client";
        public const string Organizer = "Organizer";
        #endregion

        #region Public methods
        public static (string token, CookieOptions options) CreateToken(string email, string id, string key, string issuer, Types userType, string host)
        {
            var expDate = DateTime.UtcNow.AddHours(12);

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
                Expires = expDate,
                Issuer = issuer,
                Audience = $"{id};{userType}",
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key)), SecurityAlgorithms.HmacSha512Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var stringToken = tokenHandler.WriteToken(token);
            var options = new CookieOptions()
            {
                Expires = expDate,
            };
            if (host == "localhost")
                options.Domain = null;

            return (stringToken, options);
        }

        public static bool IsIdValid(this string JwtToken, string hashedId)
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

        public static string GetIdFromToken(string JwtToken)
        {
            var encodedToken = new JwtSecurityToken(JwtToken);

            return encodedToken.Audiences.First().Split(';').First();
        }
        #endregion

        #region Private methods
        private static Types GetUserTypeFromToken(string JwtToken)
        {
            var encodedToken = new JwtSecurityToken(JwtToken);

            return Enum.Parse<Types>(encodedToken.Audiences.First().Split(';').Last());
        }
        #endregion

    }
}
