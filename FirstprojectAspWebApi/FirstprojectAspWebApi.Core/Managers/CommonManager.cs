using AutoMapper;
using FirstprojectAspWebApi.Core.Managers.Interfaces;
using FirstprojectAspWebApi.DTOs.Users;
using FirstprojectAspWebApi.Exceptions;
using FirstprojectAspWebApi.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstprojectAspWebApi.Core.Managers
{

    public class CommonManager : ICommonManager
    {
        #region dependencyInjection
        private readonly FirstprojectAspWebApiDbContext _context;
        private readonly IMapper _mapper;
        public CommonManager(FirstprojectAspWebApiDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        #endregion dependencyInjection
        public UserDTO GetUserRole(UserDTO userDTO)
        {
            var dbuser = _context.Users
                                .FirstOrDefault(usr => usr.Id == userDTO.Id)
                                ?? throw new ServiceValidationException("Invalid User Id Received");
            return _mapper.Map<UserDTO>(dbuser);
        }
    }
}
