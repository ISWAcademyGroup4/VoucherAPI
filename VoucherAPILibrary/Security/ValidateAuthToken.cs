using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using static VoucherAPILibrary.Helpers.AppSettings;

namespace VoucherAPILibrary.Security
{
    public class ValidateAuthToken
    {
        private List<User> _users = new List<User>
        {
            new User { Id = 1, Firstname = "Stephen", Lastname = "Enunwah", Username = "temigray", Password = "welcome12@" }
        };

        private readonly Appsettings _appsettings;

        public ValidateAuthToken(IOptions<Appsettings> appsettings)
        {
            _appsettings = appsettings.Value;
        }

        public User Authenticate(string username, string password)
        {
            var user = _users.SingleOrDefault(x => x.Username == username && x.Password == password);

            if (user == null)
            {
                return null;
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appsettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Firstname.ToString() + " " + user.Lastname.ToString()),
                    new Claim(ClaimTypes.Role, "User")
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)

            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            user.Token = tokenHandler.WriteToken(token);

            user.Password = null;

            return user;

        }

        public IEnumerable<User> GetAll()
        {
            return _users.Select(x =>
            {
                x.Password = null;
                return x;
            });
        }

        public string Authorize(string token)
        {
            var jwtHandler = new JwtSecurityTokenHandler();
            var jwtInput = token;
            var readableToken = jwtHandler.CanReadToken(jwtInput);

            if (readableToken != true)
                throw new Exception("Not Readable");
            else
            {
                var tokenInput = jwtHandler.ReadJwtToken(jwtInput);
                var headers = tokenInput.Header;

                var jwtHeader = "{";
                foreach (var h in headers)
                {
                    jwtHeader += '"' + h.Key + "\":\"" + h.Value + "\",";
                }
                jwtHeader = "}";

                var claims = tokenInput.Claims;
                var jwtPayload = "{";
                foreach (Claim c in claims)
                {
                    jwtPayload += '"' + c.Type + "\":\"" + c.Value + "\",";
                }
                jwtPayload += "}";

                return jwtHeader + ", /n" + jwtPayload;
            }
        }
    }
}
