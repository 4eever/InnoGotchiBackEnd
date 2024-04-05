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
    public class AutoMapperInnogotchiProfile : Profile
    {
        public AutoMapperInnogotchiProfile()
        {
            CreateMap<InnogotchiCreateDTO, Innogotchi>()
            .ForMember(dest => dest.PetDOB, opt => opt.MapFrom(src => DateTime.Now))
            .ForMember(dest => dest.FedLastTime, opt => opt.MapFrom(src => DateTime.Now))
            .ForMember(dest => dest.SumFedPeriods, opt => opt.MapFrom(src => 0))
            .ForMember(dest => dest.FedCount, opt => opt.MapFrom(src => 0))
            .ForMember(dest => dest.DrintLastTime, opt => opt.MapFrom(src => DateTime.Now))
            .ForMember(dest => dest.SumDrinkPeriods, opt => opt.MapFrom(src => 0))
            .ForMember(dest => dest.DrinkCount, opt => opt.MapFrom(src => 0))
            .ForMember(dest => dest.HappinessDays, opt => opt.MapFrom(src => 0))
            .ForMember(dest => dest.LastCheckHappinessDays, opt => opt.MapFrom(src => DateTime.Now))
            .ReverseMap();

            CreateMap<InnogotchiDTO, Innogotchi>().ReverseMap();
        }
    }
}
