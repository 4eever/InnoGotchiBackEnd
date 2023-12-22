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
    public class AutoMapperUserProfile : Profile
    {
        public AutoMapperUserProfile()
        {
            CreateMap<UserSignUpDTO, User>().ReverseMap();

            CreateMap<UserLogInDTO, User>().ReverseMap();

            CreateMap<UserDTO, User>().ReverseMap();
        }
    }
}
