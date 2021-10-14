using CoreJWT.Example.Helpers;
using CoreJWT.Example.IServices;
using CoreJWT.Example.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CoreJWT.Example.Services
{
    public class UserInfoService : IUserInfoService
    {
        private static List<UserInfo> _users = new List<UserInfo>
        {
           new UserInfo{ Id = Guid.NewGuid(), FullName = "Orçun Or",  UserName = "orcnor",  Password = "test"}

        };


        private readonly AppSettings _appSettings;

        public UserInfoService(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
        }
        public UserInfo Authenticate(string username, string password)
        {
            var user = _users.FirstOrDefault(x => x.UserName == username && x.Password == password);

            if (user == null)
            {
                return null;
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new System.Security.Claims.ClaimsIdentity(new Claim[] 
                { 
                    new Claim(ClaimTypes.Name,user.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            user.Token = tokenHandler.WriteToken(token);

            return user;
        }

        public IEnumerable<UserInfo> GetAll()
        {
            return _users;
        }

        public void SignUp(UserInfo user)
        {
            if (!_users.Any(x => x.UserName == user.UserName))
            {

                _users.Add(user);
            }
           
        }
    }
}
