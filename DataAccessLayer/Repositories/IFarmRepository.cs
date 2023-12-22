using DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories
{
    public interface IFarmRepository
    {
        Task<Farm> GetFarmById(int farmId);
        Task<List<Farm>> GetAllFarms();
        Task AddFarm(Farm farm);
        Task UpdateFarm(Farm farm);
        Task DeleteFarm(int farmId);
        Task<Farm> GetFarmByName(string farmName);
        Task<Farm> GetFarmByUserId(int userId);
    }
}
