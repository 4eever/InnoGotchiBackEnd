using AutoMapper;
using BusinessAccessLayer.DTOs;
using DataAccessLayer.Entities;
using DataAccessLayer.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BusinessAccessLayer.Services
{
    public class FarmService : IFarmService
    {
        private readonly IFarmRepository _farmRepository;
        private readonly IUserFarmRepository _userFarmRepository;
        private readonly IUserRepository _userRepository;
        private readonly IInnogotchiRepository _innogotchiRepository;
        private readonly IDeadInnogotchiRepository _deadInnogotchiRepository;
        private readonly IMapper _farmMapper;

        public FarmService(IFarmRepository farmRepository, IMapper farmMapper, 
                            IUserFarmRepository userFarmRepository, IUserRepository userRepository, 
                            IInnogotchiRepository innogotchiRepository, IDeadInnogotchiRepository deadInnogotchiRepository)
        {
            _farmRepository = farmRepository;
            _farmMapper = farmMapper;
            _userFarmRepository = userFarmRepository;
            _userRepository = userRepository;
            _innogotchiRepository = innogotchiRepository;
            _deadInnogotchiRepository = deadInnogotchiRepository;
        }

        public async Task<FarmDTO> CreateFarm(FarmCreateDTO farmCreateDTO)
        {
            Farm farmEntity = _farmMapper.Map<FarmCreateDTO, Farm>(farmCreateDTO);
            await _farmRepository.AddFarm(farmEntity);

            Farm farm = await _farmRepository.GetFarmByName(farmEntity.FarmName);
            FarmDTO farmDTOResult = _farmMapper.Map<Farm, FarmDTO>(farm);

            return farmDTOResult;
        }

        public async Task<List<FarmUserAllDTO>> GetAllUserFarms(int userId)
        {
            List<UserFarm> userFarms = await _userFarmRepository.GetAllUserFarms();
            List<Farm> farms = await _farmRepository.GetAllFarms();

            var allUserFarms = (from uf in userFarms
                                join f in farms on uf.FarmId equals f.FarmId
                                where uf.UserId == userId
                                select _farmMapper.Map<Farm, FarmUserAllDTO>(f)).ToList();

            return allUserFarms;
        }

        public async Task<List<string>> GetCollaborators(int farmId)
        {
            List<UserFarm> userFarms = await _userFarmRepository.GetAllUserFarms();
            List<User> users = await _userRepository.GetAllUsers();

            var collaborators = (from uf in userFarms
                                 join u in users on uf.UserId equals u.UserId
                                 where uf.RoleId == 2 && uf.FarmId == farmId
                                 select u.UserEmail).ToList();
            return collaborators;
        }

        public async Task<FarmStatisticDTO> GetFarmStatistic(int farmId)
        {
            Farm farm = await _farmRepository.GetFarmById(farmId);
            List<Innogotchi> allInnogotchis = await _innogotchiRepository.GetAllInnogotchis();
            List<DeadInnogotchi> allDeadInnogotchis = await _deadInnogotchiRepository.GetAllDeadInnogotchies();

            var innogotchis = (from i in allInnogotchis
                               where i.FarmId == farmId
                               select i).ToList();

            var deadInnogotchis = (from di in allDeadInnogotchis
                                   where di.FarmId == farmId
                                   select di).ToList();
                              

            int SumAllInnogotchiFedPeriod = 0;
            int averageAllInnogotchiFedCount = 0;

            int SumAllInnogotchiDrinkPeriod = 0;
            int averageAllInnogotchiDrinkCount = 0;

            int SumAllInnogotchiHappinessDays = 0;
            int averageAllInnogotchiHappinessCount = 0;

            int SumAllInnogotchiAge = 0;
            int averageAllInnogotchiAgeCount = 0;

            int averageInnogotchiFedPeriod = 0;
            int averageInnogotchiDrinkPeriod = 0;

            DateTime currentDate = DateTime.Now;

            foreach (var innogotchi in innogotchis)
            {
                if (innogotchi.FedCount != 0)
                {
                    averageInnogotchiFedPeriod = innogotchi.SumFedPeriods / innogotchi.FedCount;
                    SumAllInnogotchiFedPeriod += averageInnogotchiFedPeriod;
                    averageAllInnogotchiFedCount++;
                }

                if (innogotchi.DrinkCount != 0)
                {
                    averageInnogotchiDrinkPeriod = innogotchi.SumDrinkPeriods / innogotchi.DrinkCount;
                    SumAllInnogotchiDrinkPeriod += averageInnogotchiDrinkPeriod;
                    averageAllInnogotchiDrinkCount++;
                }

                SumAllInnogotchiHappinessDays += innogotchi.HappinessDays;
                averageAllInnogotchiHappinessCount++;

                SumAllInnogotchiAge += (int)((currentDate - innogotchi.PetDOB).TotalDays / 7.0);
                averageAllInnogotchiAgeCount++;
            }

            foreach (var deadInnogotchi in deadInnogotchis)
            {
                SumAllInnogotchiAge += deadInnogotchi.DeadInnogotchiAge;
                averageAllInnogotchiAgeCount++;
            }

            int averageAllInnogotchiFedPeriod = averageAllInnogotchiFedCount != 0
                ? SumAllInnogotchiFedPeriod / averageAllInnogotchiFedCount
                : SumAllInnogotchiFedPeriod;

            int averageAllInnogotchiDrinkPeriod = averageAllInnogotchiDrinkCount != 0
                ? SumAllInnogotchiDrinkPeriod / averageAllInnogotchiDrinkCount
                : SumAllInnogotchiDrinkPeriod;

            int averageAllInnogotchiHappinessDays = averageAllInnogotchiHappinessCount != 0
                ? SumAllInnogotchiHappinessDays / averageAllInnogotchiHappinessCount
                : SumAllInnogotchiHappinessDays;

            int averageAllInnogotchiAge = averageAllInnogotchiAgeCount != 0
                ? SumAllInnogotchiAge / averageAllInnogotchiAgeCount
                : SumAllInnogotchiHappinessDays;

            FarmStatisticDTO farmStatisticDTO = new FarmStatisticDTO(farm.FarmId, farm.UserId, farm.FarmName, farm.PetsAlive, farm.PetsDead,
                                                                    averageAllInnogotchiFedPeriod, averageAllInnogotchiDrinkPeriod, averageAllInnogotchiHappinessDays, averageAllInnogotchiAge);

            return farmStatisticDTO;
        }

    }
}
