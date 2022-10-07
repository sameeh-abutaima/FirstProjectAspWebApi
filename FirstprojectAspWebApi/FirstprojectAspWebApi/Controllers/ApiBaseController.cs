using FirstprojectAspWebApi.DTOs.Users;
using FirstprojectAspWebApi.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using System.Linq;

namespace FirstprojectAspWebApi.Controllers
{

    public class ApiBaseController : Controller
    {
        private UserDTO _loggedInUser;
        protected UserDTO LoggedInUser
        {
            get
            {
                if(_loggedInUser != null)return _loggedInUser;
                Request.Headers.TryGetValue("Authorization", out StringValues Token);
                if (string.IsNullOrWhiteSpace(Token))
                {
                    _loggedInUser = null;
                    return _loggedInUser;
                }
                var ClaimId = User.Claims.FirstOrDefault(c => c.Type == "Id");
                //int.TryParse(ClaimId.Value, out int id);
                if(ClaimId==null || !int.TryParse(ClaimId.Value,out int id))
                {
                    throw new ServiceValidationException(401, "Invalid or Expired Token");
                }
                return new UserDTO
                {
                    Id = id,
                    FirstName = "Sameeh",
                    LastName = "Abutaima",
                    Email = "sameeh@gmail.com"

                };
            }
        }
        public ApiBaseController()
        {

        }
    }
}
