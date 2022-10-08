using AutoMapper;
using FirstprojectAspWebApi.Core.Managers.Interfaces;
using FirstprojectAspWebApi.DTOs.Users;
using FirstprojectAspWebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstprojectAspWebApi.Core.Managers
{

    public class RoleManager : IRoleManager
    {
        #region dependencyInjection
        private readonly FirstprojectAspWebApiDbContext _context;
        private readonly IMapper _mapper;
        public RoleManager(FirstprojectAspWebApiDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        #endregion dependencyInjection
        public bool CheckAccess(UserDTO userDTO)
        {
            var isAdmin = _context.Users.Any(usr => usr.Id == userDTO.Id && usr.IsAdmin);
            return isAdmin;
        }
    }
}
