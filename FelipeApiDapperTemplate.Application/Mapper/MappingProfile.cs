using AutoMapper;
using FelipeApiDapperTemplate.Domain.Models.DTOs;
using FelipeApiDapperTemplate.Domain.Models.Entities;

namespace FelipeApiDapperTemplate.Application.Mapper;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Customer, CustomerDTO>();
    }
}

