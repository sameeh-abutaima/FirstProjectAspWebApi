using AutoMapper;
using FirstprojectAspWebApi.DTOs.Users;
using FirstprojectAspWebApi.Models;

namespace FirstprojectAspWebApi.Mappers
{
    public class UserMapper:Profile
    {
        public UserMapper()
        {
            CreateMap<UserResultDTO, User>().ReverseMap();
            CreateMap<UserRegistrationDTO, User>().ReverseMap();
            CreateMap<UserLoginResponseDTO, User>().ReverseMap();
            CreateMap<UserDTO, User>().ReverseMap();
        }
    }
}
