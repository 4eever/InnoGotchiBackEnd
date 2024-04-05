using DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories
{
    public interface IUserFarmRepository
    {
        Task<UserFarm> GetUserFarmById(int userFarmId);
        Task<List<UserFarm>> GetAllUserFarms();
        Task AddUserFarm(UserFarm userFarm);
        Task UpdateUserFarm(UserFarm userFarm);
        Task DeleteUserFarm(int userFarmId);

        Task<UserFarm> GetUserFarmByUserIdFarmId(int userId, int farmId);
    }
}
