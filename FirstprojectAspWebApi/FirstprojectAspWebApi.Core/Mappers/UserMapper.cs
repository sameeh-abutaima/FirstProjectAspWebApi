using AutoMapper;
using FirstprojectAspWebApi.DbModels.Models;
using FirstprojectAspWebApi.DTOs.Users;

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
