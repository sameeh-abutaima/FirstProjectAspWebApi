
using AutoMapper;
using FirstprojectAspWebApi.DTOs.Users;
using FirstprojectAspWebApi.Exceptions;
using FirstprojectAspWebApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace FirstprojectAspWebApi.Controllers
{
    [ApiController]
    public class UserController : ApiBaseController
    {
        private readonly FirstprojectAspWebApiDbContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IConfiguration _config;
        public UserController(FirstprojectAspWebApiDbContext context,
            IMapper mapper,
            ILogger<WeatherForecastController> logger,
            IConfiguration config)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
            _config = config;
        }

        [HttpGet]
        [Route("api/user")]
        public IActionResult Get()
        {
            return Ok(_mapper.Map<List<UserResultDTO>>(_context.Users));
        }
        [Route("api/user/reg")]
        [HttpPost]
        [AllowAnonymous]
        public IActionResult Post([FromBody]UserRegistrationDTO userRegistrationDTO)
        {
            if(_context.Users.Any(usr=> usr.Email.ToLower().Equals(userRegistrationDTO.Email.ToLower())))
            {
                return BadRequest("User Already Exist");
            }
            var hashedPassword=HashPassword(userRegistrationDTO.Password);
            var user = _context.Users.Add(
                new User
                {
                    FirstName=userRegistrationDTO.FirstName,
                    LastName=userRegistrationDTO.LastName,
                    Email=userRegistrationDTO.Email,
                    Password=hashedPassword,
                    ConfirmPassword=hashedPassword,
                    ImageUrl=string.Empty

                }).Entity;
            _context.SaveChanges();
            return Ok(user);
        }

        [HttpPost]
        [Route("api/user/login")]
        [AllowAnonymous]
        public IActionResult Post([FromBody] UserLoginDTO userLoginDTO)
        {
            var user=_context.Users.FirstOrDefault(usr=>usr.Email.ToLower().Equals(userLoginDTO.Email.ToLower()));
            if (user == null || !VerifyHashPassword(userLoginDTO.Password, user.Password)){
                throw new ServiceValidationException(300, "Invalid email or password received");
            }
            var result=_mapper.Map<UserLoginResponseDTO>(user);
            result.Token = $"Bearer {GenerateJwtToken(user)}";
            return Ok(result);
        }

        [Route("api/user/fileretrive/profilepic")]
        [HttpGet]
        public IActionResult Retrive(string filename)
        {
            var folderPath = Directory.GetCurrentDirectory();
            folderPath = $@"{folderPath}\{filename}";
            var byteArray = System.IO.File.ReadAllBytes(folderPath);
            return File(byteArray, "image/jpeg", filename);
        }

        [HttpPut]
        [Route("api/user/me")]
        [Authorize]
        public IActionResult Put(UserDTO userDTO)
        {
            var user=_context.Users.FirstOrDefault(usr=>usr.Id==LoggedInUser.Id)
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
            return Ok(_mapper.Map<UserDTO>(user));
        }

    }
}
