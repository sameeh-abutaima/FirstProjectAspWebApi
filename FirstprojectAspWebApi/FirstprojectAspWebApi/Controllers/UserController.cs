using FirstprojectAspWebApi.Core.Managers.Interfaces;
using FirstprojectAspWebApi.DTOs.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.IO;

namespace FirstprojectAspWebApi.Controllers
{
    [ApiController]
    public class UserController : ApiBaseController
    {
        private readonly ILogger<UserController> _logger;
        private readonly IUserManager _userManager;
        public UserController(ILogger<UserController> logger,IUserManager userManager)
        {
            _logger = logger;
            _userManager = userManager;
        }

        [HttpGet]
        [Route("api/user")]
        public IActionResult Get()
        {
            //_mapper.Map<List<UserResultDTO>>(_context.Users)
            return Ok();
        }
        [Route("api/user/SignUp")]
        [HttpPost]
        [AllowAnonymous]
        public IActionResult SignUp([FromBody]UserRegistrationDTO userRegistrationDTO)
        {
            var res=_userManager.SignUp(userRegistrationDTO);
            return Ok(res);
        }

        [HttpPost]
        [Route("api/user/LogIn")]
        [AllowAnonymous]
        public IActionResult LogIn([FromBody] UserLoginDTO userLoginDTO)
        {
            var res=_userManager.LogIn(userLoginDTO);
            return Ok(res);
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
        [Route("api/user/UpdateProfile")]
        [Authorize]
        public IActionResult UpdateProfile(UserDTO userDTO)
        {
            var res = _userManager.UpdateProfile(LoggedInUser, userDTO);
            return Ok(res);
        }

    }
}
