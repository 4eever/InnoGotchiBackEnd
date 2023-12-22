using AutoMapper;
using BusinessAccessLayer.DTOs;
using DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessAccessLayer.Mappings
{
    public class AutoMapperFarmProfile : Profile
    {
        public AutoMapperFarmProfile()
        {
            CreateMap<FarmCreateDTO, Farm>()
            .ForMember(dest => dest.PetsAlive, opt => opt.MapFrom(src => 0))
            .ForMember(dest => dest.PetsDead, opt => opt.MapFrom(src => 0))
            .ReverseMap();

            CreateMap<Farm, FarmDTO>().ReverseMap();

            CreateMap<Farm, FarmUserAllDTO>().ReverseMap();
        }   
    }
}
