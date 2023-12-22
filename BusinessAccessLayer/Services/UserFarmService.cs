using AutoMapper;
using BusinessAccessLayer.DTOs;
using DataAccessLayer.Entities;
using DataAccessLayer.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessAccessLayer.Services
{
    public class UserFarmService : IUserFarmService
    {
        private readonly IUserFarmRepository _userFarmRepository;
        private readonly IMapper _userFarmMapper;

        public UserFarmService(IUserFarmRepository userFarmRepository, IMapper mapper)
        {
            _userFarmRepository = userFarmRepository;
            _userFarmMapper = mapper;
        }

        public async Task<UserFarmDTO> CreateUserFarm(UserFarmDTO userFarmDTO)
        {
            UserFarm userFarmEntity = _userFarmMapper.Map<UserFarmDTO, UserFarm>(userFarmDTO);
            await _userFarmRepository.AddUserFarm(userFarmEntity);

            UserFarm userFarm = await _userFarmRepository.GetUserFarmByUserIdFarmId(userFarmEntity.UserId, userFarmEntity.FarmId);
            UserFarmDTO userFarmDTOResult = _userFarmMapper.Map<UserFarm, UserFarmDTO>(userFarm);

            return userFarmDTOResult;
        }

        public async Task<UserFarmDTO> GetUserFarm(UserFarmDTO userFarmDTO)
        {
            UserFarm userFarm = await _userFarmRepository.GetUserFarmByUserIdFarmId(userFarmDTO.UserId, userFarmDTO.FarmId);

            UserFarmDTO userFarmDTOResult = _userFarmMapper.Map<UserFarm, UserFarmDTO>(userFarm);
            return userFarmDTOResult;
        }
    }
}
