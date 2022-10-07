using AutoMapper;
using FirstprojectAspWebApi.Core.Managers.Interfaces;
using FirstprojectAspWebApi.DbModels.Models;
using FirstprojectAspWebApi.DTOs.Users;
using FirstprojectAspWebApi.Exceptions;
using FirstprojectAspWebApi.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace FirstprojectAspWebApi.Core.Managers
{
    public class UserManager : IUserManager
    {
        #region dependencyInjection
        private readonly FirstprojectAspWebApiDbContext _context;
        private readonly IMapper _mapper;
        private readonly IConfiguration _config;
        public UserManager(FirstprojectAspWebApiDbContext context,IMapper mapper,IConfiguration config)
        {
            _context = context;
            _mapper = mapper;
            _config = config;
        }

        #endregion dependencyInjection


        #region public
        public UserLoginResponseDTO LogIn(UserLoginDTO userLoginDTO)
        {
            var user = _context.Users.FirstOrDefault(usr => usr.Email.ToLower().Equals(userLoginDTO.Email.ToLower()));
            if (user == null || !VerifyHashPassword(userLoginDTO.Password, user.Password))
            {
                throw new ServiceValidationException(300, "Invalid email or password received");
            }
            var result = _mapper.Map<UserLoginResponseDTO>(user);
            result.Token = $"Bearer {GenerateJwtToken(user)}";
            return result;
        }

        public UserLoginResponseDTO SignUp(UserRegistrationDTO userRegistrationDTO)
        {
            if (_context.Users.Any(usr => usr.Email.ToLower().Equals(userRegistrationDTO.Email.ToLower())))
            {
                throw new ServiceValidationException("user already exist");
            }
            var hashedPassword = HashPassword(userRegistrationDTO.Password);
            var user = _context.Users.Add(
                new User
                {
                    FirstName = userRegistrationDTO.FirstName,
                    LastName = userRegistrationDTO.LastName,
                    Email = userRegistrationDTO.Email,
                    Password = hashedPassword,
                    ConfirmPassword = hashedPassword,
                    ImageUrl = string.Empty

                }).Entity;
            _context.SaveChanges();
            var result = _mapper.Map<UserLoginResponseDTO>(user);
            result.Token = $"Bearer {GenerateJwtToken(user)}";
            return result;
        }

        public UserDTO UpdateProfile(UserDTO currentUserDTO,UserDTO userDTO)
        {
            var user = _context.Users.FirstOrDefault(usr => usr.Id == currentUserDTO.Id)
                ?? throw new ServiceValidationException("User Not Found");
            var url = "";
            if (!string.IsNullOrWhiteSpace(userDTO.ImageString))
            {
                url = Helper.Helper.SaveImage(userDTO.ImageString, "profileimages");
            }
            if (!string.IsNullOrWhiteSpace(url))
            {
                var baseURL = "http://localhost:38197/";
                user.ImageUrl = @$"{baseURL}/api/v1/user/fileretrive/profilepic?filename={url}";
            }
            _context.SaveChanges();
            return _mapper.Map<UserDTO>(user);
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
