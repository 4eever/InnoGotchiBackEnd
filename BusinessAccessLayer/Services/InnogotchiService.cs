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
using System.Web.Mvc;

namespace BusinessAccessLayer.Services
{
    public class InnogotchiService : IInnogotchiService
    {
        private readonly IFarmRepository _farmRepository;
        private readonly IDeadInnogotchiRepository _deadInnogotchiRepository;
        private readonly IInnogotchiRepository _innogotchiRepository;
        private readonly IUserFarmRepository _userFarmRepository;
        private readonly IInnogotchiBodyPartRepository _innogotchiBodyPartRepository;
        private readonly IMapper _innogotchiMapper;

        public InnogotchiService(IInnogotchiRepository innogotchiRepository, IMapper inngotchiMapper, IUserFarmRepository userFarmRepository, IDeadInnogotchiRepository deadInnogotchiRepository, IFarmRepository farmRepository, IInnogotchiBodyPartRepository innogotchiBodyPartRepository)
        {
            _innogotchiRepository = innogotchiRepository;
            _innogotchiMapper = inngotchiMapper;
            _userFarmRepository = userFarmRepository;
            _deadInnogotchiRepository = deadInnogotchiRepository;
            _farmRepository = farmRepository;
            _innogotchiBodyPartRepository = innogotchiBodyPartRepository;
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

        public async Task<List<InnogotchiBodyPartsDTO>> GetFarmInnogotchies(int farmId)
                           {
            List<Innogotchi> innogotchis = await _innogotchiRepository.GetAllInnogotchis();
            List<Innogotchi> farmInnogotchis = (innogotchis.Where(i => i.FarmId == farmId).ToList());
            var innogotchiesWithUpdatedhappinessDays = await UpdateHappinessDays(farmInnogotchis);

            var innogotchiInformationDTOs = new List<InnogotchiBodyPartsDTO>();

            foreach (var farmInnogotchi in innogotchiesWithUpdatedhappinessDays)
            {
                InnogotchiBodyPartsDTO innogotchiInformationDTO = await InnogotchiToInogotchiInformationDTO(farmInnogotchi);
                innogotchiInformationDTOs.Add(innogotchiInformationDTO);
            }

            return innogotchiInformationDTOs;
        }

        public async Task<List<InnogotchiBodyPartsDTO>> GetAllInnogotchies(int userId)
        {
            List<UserFarm> userFarms = await _userFarmRepository.GetAllUserFarms();
            List<Innogotchi> innogotchis = await _innogotchiRepository.GetAllInnogotchis();
            var innogotchiesWithUpdatedhappinessDays = await UpdateHappinessDays(innogotchis);

            var allInnogotchies = (from uf in userFarms
                                        join i in innogotchiesWithUpdatedhappinessDays on uf.FarmId equals i.FarmId
                                        where uf.UserId == userId && uf.RoleId == 2
                                        select i).ToList();

            var innogotchiInformationDTOs = new List<InnogotchiBodyPartsDTO>();

            foreach (var farmInnogotchi in allInnogotchies)
            {
                InnogotchiBodyPartsDTO innogotchiInformationDTO = await InnogotchiToInogotchiInformationDTO(farmInnogotchi);
                innogotchiInformationDTOs.Add(innogotchiInformationDTO);
            }

            return innogotchiInformationDTOs;
        }

        public async Task Feed(int innogotchiId)
        {
            Innogotchi innogotchi = await _innogotchiRepository.GetInnogotchiById(innogotchiId);
            InnogotchiDTO innogotchiDTO = _innogotchiMapper.Map<Innogotchi, InnogotchiDTO>(innogotchi);

            int hungryPeriod = (int)((DateTime.Now - innogotchiDTO.FedLastTime).TotalDays);

            innogotchiDTO.SumFedPeriods += hungryPeriod;
            innogotchiDTO.FedCount += 1;
            innogotchiDTO.FedLastTime = DateTime.Now;

            Innogotchi innogotchiResult = _innogotchiMapper.Map<InnogotchiDTO, Innogotchi>(innogotchiDTO);
            await _innogotchiRepository.UpdateInnogotchi(innogotchiResult);
        }

        public async Task Drink(int innogotchiId)
        {
            Innogotchi innogotchi = await _innogotchiRepository.GetInnogotchiById(innogotchiId);
            InnogotchiDTO innogotchiDTO = _innogotchiMapper.Map<Innogotchi, InnogotchiDTO>(innogotchi);

            int thirstyPeriod = (int)((DateTime.Now - innogotchiDTO.DrintLastTime).TotalDays);

            innogotchiDTO.SumDrinkPeriods += thirstyPeriod;
            innogotchiDTO.DrinkCount += 1;
            innogotchiDTO.DrintLastTime = DateTime.Now;


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

        private async Task<InnogotchiBodyPartsDTO> InnogotchiToInogotchiInformationDTO(Innogotchi innogotchi)
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

            string body = (await _innogotchiBodyPartRepository.GetInnogotchiBodyPartByBodyPartIdAndNumber(1, innogotchi.BodyNumber)).InnogotchiBodyPartImage;
            string eyes = (await _innogotchiBodyPartRepository.GetInnogotchiBodyPartByBodyPartIdAndNumber(2, innogotchi.EyesNumber)).InnogotchiBodyPartImage;
            string mouth = (await _innogotchiBodyPartRepository.GetInnogotchiBodyPartByBodyPartIdAndNumber(3, innogotchi.MouthNumber)).InnogotchiBodyPartImage;
            string nose = (await _innogotchiBodyPartRepository.GetInnogotchiBodyPartByBodyPartIdAndNumber(4, innogotchi.NoseNumber)).InnogotchiBodyPartImage;

            var innogotchiInformationDTO = new InnogotchiBodyPartsDTO(innogotchi.InnogotchiId, innogotchi.FarmId, innogotchi.InnogotchiName, body,
                                                                        eyes, mouth, nose, innogotchi.HappinessDays,
                                                                        age, hungerLevel, thirstLevel);

            return innogotchiInformationDTO;
        }

        private async Task<List<Innogotchi>> UpdateHappinessDays(List<Innogotchi> innogotchies)
        {
            foreach (var innogotchi in innogotchies)
            {
                int hungryPeriod = (int)((DateTime.Now - innogotchi.FedLastTime).TotalDays);
                int thirstyPeriod = (int)((DateTime.Now - innogotchi.DrintLastTime).TotalDays);
                double lastCheckHappinessDays = (DateTime.Now - innogotchi.LastCheckHappinessDays).TotalDays;

                if (
                    (0 < hungryPeriod && hungryPeriod < 2 && 0 < thirstyPeriod && thirstyPeriod < 2 && lastCheckHappinessDays > 1 && lastCheckHappinessDays < 2) ||
                    (((1 < hungryPeriod && hungryPeriod < 2) || (2 < hungryPeriod && hungryPeriod < 3)) && ((1 < thirstyPeriod && thirstyPeriod < 2) || (2 < thirstyPeriod && thirstyPeriod < 3)) && 2 < lastCheckHappinessDays && lastCheckHappinessDays < 3) ||
                    ((hungryPeriod > 3 || thirstyPeriod > 3) && lastCheckHappinessDays > 3)
                    )
                {
                    innogotchi.HappinessDays += 1;
                }

                if(lastCheckHappinessDays > 1) innogotchi.LastCheckHappinessDays = DateTime.Now;

                await _innogotchiRepository.UpdateInnogotchi(innogotchi);
            }

            return innogotchies;
        }
    }
}
