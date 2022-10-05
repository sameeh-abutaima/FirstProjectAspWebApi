using FirstprojectAspWebApi.Core.Managers.Interfaces;
using FirstprojectAspWebApi.DTOs.Users;
using FirstprojectAspWebApi.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace FirstprojectAspWebApi.Core.Managers
{
    public class UserManager : IUserManager
    {
        #region dependency injection
        private readonly IConfiguration _config;
        public UserManager(IConfiguration config)
        {
            _config = config;
        }

        #endregion dependency injection


        #region public
        public UserLoginResponseDTO Login(UserLoginDTO userLoginDTO)
        {
            throw new NotImplementedException();
        }

        public UserLoginResponseDTO SignUp(UserRegistrationDTO userRegistrationDTO)
        {
            throw new NotImplementedException();
        }

        public UserDTO UpdateProfile(UserDTO userDTO)
        {
            throw new NotImplementedException();
        }
        #endregion public

        #region private
        private static string HashPassword(string paswrd)
            => BCrypt.Net.BCrypt.HashPassword(paswrd);

        private static bool VerifyHashPassword(string password, string hashedPassword)
            => BCrypt.Net.BCrypt.Verify(password, hashedPassword);

        private string GenerateJwtToken(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credential = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub,$"{user.FirstName} {user.LastName}"),
                new Claim(JwtRegisteredClaimNames.Email,user.Email),
                new Claim("Id",user.Id.ToString()),
                new Claim("FirstName",user.FirstName),
                new Claim("DateOfJoining",user.CreatedAt.ToString("yyyy-MM-dd")),
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString())

            };
            var issuer = "test.com";
            var token = new JwtSecurityToken(issuer, issuer, claims, expires: DateTime.Now, signingCredentials: credential);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        #endregion private
    }
}
