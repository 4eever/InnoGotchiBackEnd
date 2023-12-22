using AutoMapper;
using BusinessAccessLayer.DTOs;
using DataAccessLayer.Entities;
using DataAccessLayer.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization.Formatters;
using System.Text;
using System.Threading.Tasks;

namespace BusinessAccessLayer.Services
{
    public class InnogotchiService : IInnogotchiService
    {
        private readonly IFarmRepository _farmRepository;
        private readonly IDeadInnogotchiRepository _deadInnogotchiRepository;
        private readonly IInnogotchiRepository _innogotchiRepository;
        private readonly IUserFarmRepository _userFarmRepository;
        private readonly IMapper _innogotchiMapper;

        public InnogotchiService(IInnogotchiRepository innogotchiRepository, IMapper inngotchiMapper, IUserFarmRepository userFarmRepository, IDeadInnogotchiRepository deadInnogotchiRepository, IFarmRepository farmRepository)
        {
            _innogotchiRepository = innogotchiRepository;
            _innogotchiMapper = inngotchiMapper;
            _userFarmRepository = userFarmRepository;
            _deadInnogotchiRepository = deadInnogotchiRepository;
            _farmRepository = farmRepository;
        }

        public async Task<InnogotchiCreateDTO> CreateInnogotchi(InnogotchiCreateDTO innogotchiCreateDTO)
        {
            Innogotchi innogotchiEntity = _innogotchiMapper.Map<InnogotchiCreateDTO, Innogotchi>(innogotchiCreateDTO);
            await _innogotchiRepository.AddInnogotchi(innogotchiEntity);

            Innogotchi innogotchi = await _innogotchiRepository.GetInnogotchiByName(innogotchiCreateDTO.InnogotchiName);
            InnogotchiCreateDTO innogotchiDTOResult = _innogotchiMapper.Map<Innogotchi, InnogotchiCreateDTO>(innogotchi);

            Farm farm = await _farmRepository.GetFarmById(innogotchi.FarmId);
            farm.PetsAlive++;
            await _farmRepository.UpdateFarm(farm);

            return innogotchiDTOResult;
        }

        public async Task<List<InnogotchiInformationDTO>> GetFarmInnogotchies(int farmId)
                           {
            List<Innogotchi> innogotchis = await _innogotchiRepository.GetAllInnogotchis();
            List<Innogotchi> farmInnogotchis = innogotchis.Where(i => i.FarmId == farmId).ToList();

            var innogotchiInformationDTOs = new List<InnogotchiInformationDTO>();

            foreach (var farmInnogotchi in farmInnogotchis)
            {
                var innogotchiInformationDTO = InnogotchiToInogotchiInformationDTO(farmInnogotchi);
                innogotchiInformationDTOs.Add(innogotchiInformationDTO);
            }

            return innogotchiInformationDTOs;
        }

        public async Task<List<InnogotchiInformationDTO>> GetAllInnogotchies(int userId)
        {
            List<UserFarm> userFarms = await _userFarmRepository.GetAllUserFarms();
            List<Innogotchi> innogotchis = await _innogotchiRepository.GetAllInnogotchis();

            var allInnogotchies = (from uf in userFarms
                                   join i in innogotchis on uf.FarmId equals i.FarmId
                                   where uf.UserId == userId && uf.RoleId ==2
                                   select InnogotchiToInogotchiInformationDTO(i)).ToList();

            return allInnogotchies;
        }

        public async Task Feed(int innogotchiId)
        {
            Innogotchi innogotchi = await _innogotchiRepository.GetInnogotchiById(innogotchiId);
            InnogotchiDTO innogotchiDTO = _innogotchiMapper.Map<Innogotchi, InnogotchiDTO>(innogotchi);

            int hungryPeriod = (int)((DateTime.Now - innogotchiDTO.FedLastTime).TotalDays);
            int thirstyPeriod = (int)((DateTime.Now - innogotchiDTO.DrintLastTime).TotalDays);

            innogotchiDTO.SumFedPeriods += hungryPeriod;
            innogotchiDTO.FedCount += 1;
            innogotchiDTO.FedLastTime = DateTime.Now;

            if (1 < thirstyPeriod && thirstyPeriod < 2)
            {
                innogotchiDTO.HappinessDays += 1;
            }

            Innogotchi innogotchiResult = _innogotchiMapper.Map<InnogotchiDTO, Innogotchi>(innogotchiDTO);
            await _innogotchiRepository.UpdateInnogotchi(innogotchiResult);
        }

        public async Task Drink(int innogotchiId)
        {
            Innogotchi innogotchi = await _innogotchiRepository.GetInnogotchiById(innogotchiId);
            InnogotchiDTO innogotchiDTO = _innogotchiMapper.Map<Innogotchi, InnogotchiDTO>(innogotchi);

            int hungryPeriod = (int)((DateTime.Now - innogotchiDTO.FedLastTime).TotalDays);
            int thirstyPeriod = (int)((DateTime.Now - innogotchiDTO.DrintLastTime).TotalDays);

            innogotchiDTO.SumDrinkPeriods += thirstyPeriod;
            innogotchiDTO.DrinkCount += 1;
            innogotchiDTO.DrintLastTime = DateTime.Now;

            if (1 < hungryPeriod && hungryPeriod < 2)
            {
                innogotchiDTO.HappinessDays += 1;
            }

            Innogotchi innogotchiResult = _innogotchiMapper.Map<InnogotchiDTO, Innogotchi>(innogotchiDTO);
            await _innogotchiRepository.UpdateInnogotchi(innogotchiResult);
        }

        public async Task Dead(int innogotchiId)
        {
            Innogotchi innogotchi = await _innogotchiRepository.GetInnogotchiById(innogotchiId);
            var deadInnogotchi = new DeadInnogotchi();

            deadInnogotchi.DeadInnogotchiName = innogotchi.InnogotchiName;
            deadInnogotchi.DeadInnogotchiAge = (int)((DateTime.Now - innogotchi.PetDOB).TotalDays / 7.0);
            deadInnogotchi.FarmId = innogotchi.FarmId;
            await _deadInnogotchiRepository.AddDeadInnogotchi(deadInnogotchi);

            await _innogotchiRepository.DeleteInnogotchi(innogotchiId);

            Farm farm = await _farmRepository.GetFarmById(innogotchi.FarmId);
            farm.PetsDead++;
            farm.PetsAlive--;
            await _farmRepository.UpdateFarm(farm);
        }

        public InnogotchiInformationDTO InnogotchiToInogotchiInformationDTO(Innogotchi innogotchi)
        {
            int age = (int)((DateTime.Now - innogotchi.PetDOB).TotalDays / 7.0); //если меньше 7 дней, то в фронт части будет писаться newborn

            int hungerLevelDays = (int)((DateTime.Now - innogotchi.FedLastTime).TotalDays);

            string hungerLevel = hungerLevelDays switch
            {
                < 1 => "Full",
                < 2 => "Normal",
                < 3 => "Hunger",
                _ => "Dead"
            };

            int thirstLevelDays = (int)((DateTime.Now - innogotchi.DrintLastTime).TotalDays);

            string thirstLevel = thirstLevelDays switch
            {
                < 1 => "Full",
                < 2 => "Normal",
                < 3 => "Thirst",
                _ => "Dead"
            };

            var innogotchiInformationDTO = new InnogotchiInformationDTO(innogotchi.InnogotchiId, innogotchi.FarmId, innogotchi.InnogotchiName, innogotchi.BodyNumber,
                                                                        innogotchi.EyesNumber, innogotchi.NoseNumber, innogotchi.MouthNumber, innogotchi.HappinessDays,
                                                                        age, hungerLevel, thirstLevel);

            return innogotchiInformationDTO;
        }
    }
}
