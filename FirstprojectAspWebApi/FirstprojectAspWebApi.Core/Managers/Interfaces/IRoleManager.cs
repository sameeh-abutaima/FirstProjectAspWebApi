﻿using FirstprojectAspWebApi.DTOs.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstprojectAspWebApi.Core.Managers.Interfaces
{
    public interface IRoleManager
    {
        public bool CheckAccess(UserDTO userDTO);
    }
}
