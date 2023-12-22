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
    public class AutoMapperUserFarmProfile : Profile
    {
        public AutoMapperUserFarmProfile()
        {
            CreateMap<UserFarmDTO, UserFarm>().ReverseMap();
        }
    }
}
