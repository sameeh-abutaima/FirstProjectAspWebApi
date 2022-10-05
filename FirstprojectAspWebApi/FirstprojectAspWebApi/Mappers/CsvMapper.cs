using AutoMapper;
using FirstprojectAspWebApi.DTOs.CSV;
using FirstprojectAspWebApi.Models;

namespace FirstprojectAspWebApi.Mappers
{
    public class CsvMapper:Profile
    {
        public CsvMapper()
        {
            CreateMap<CsvDTO, DetailsOfItem>().ReverseMap();
        }
    }
}
