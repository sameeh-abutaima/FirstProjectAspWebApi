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
        public UserDTO UpdateProfile(UserDTO currentUserDTO,UserDTO userDTO);
        public UserLoginResponseDTO LogIn(UserLoginDTO userLoginDTO);
        public UserLoginResponseDTO SignUp(UserRegistrationDTO userRegistrationDTO);

    }
}
