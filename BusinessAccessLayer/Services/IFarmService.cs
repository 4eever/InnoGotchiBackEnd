using BusinessAccessLayer.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BusinessAccessLayer.Services
{
    public interface IFarmService
    {
        public Task<FarmDTO> CreateFarm(FarmCreateDTO farmCreateDTO);
        public Task<List<FarmUserAllDTO>> GetAllUserFarms(int userId);
        public Task<List<string>> GetCollaborators(int farmId);
        public Task<FarmStatisticDTO> GetFarmStatistic(int farmId);
        public Task<string> GetFarmName(int farmId);
    }
}
