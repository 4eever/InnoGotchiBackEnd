using BusinessAccessLayer.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace BusinessAccessLayer.Services
{
    public interface IInnogotchiService
    {
        public Task<InnogotchiCreateDTO> CreateInnogotchi(InnogotchiCreateDTO innogotchiCreateDTO);
        public Task<List<InnogotchiBodyPartsDTO>> GetFarmInnogotchies (int farmId);
        public Task<List<InnogotchiBodyPartsDTO>> GetAllInnogotchies(int farmId);
        public Task Feed(int innogotchiId);
        public Task Drink(int innogotchiId);
        public Task Dead(int innogotchiId);
    }
}
