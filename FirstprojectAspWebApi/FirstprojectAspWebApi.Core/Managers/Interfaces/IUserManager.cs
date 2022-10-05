using FirstprojectAspWebApi.DTOs.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstprojectAspWebApi.Core.Managers.Interfaces
{
    public interface IUserManager:IManager
    {
        public UserDTO UpdateProfile(UserDTO userDTO);
        public UserLoginResponseDTO Login(UserLoginDTO userLoginDTO);
        public UserLoginResponseDTO SignUp(UserRegistrationDTO userRegistrationDTO);

    }
}
