using DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories
{
    public class UserFarmRepository : IUserFarmRepository
    {
        private readonly ApplicationContext _db;

        public UserFarmRepository(ApplicationContext db)
        {
            _db = db;
        }

        public async Task<UserFarm> GetUserFarmById(int userFarmId)
        {
            return await _db.UserFarms.FindAsync(userFarmId);
        }

        public async Task<List<UserFarm>> GetAllUserFarms()
        {
            return await _db.UserFarms.ToListAsync();
        }

        public async Task AddUserFarm(UserFarm userFarm)
        {
            _db.UserFarms.Add(userFarm);
            await _db.SaveChangesAsync();
        }

        public async Task UpdateUserFarm(UserFarm userFarm)
        {
            _db.Entry(userFarm).State = EntityState.Modified;
            await _db.SaveChangesAsync();
        }

        public async Task DeleteUserFarm(int userFarmId)
        {
            var userFarm = await _db.UserFarms.FindAsync(userFarmId);
            if (userFarm != null)
            {
                _db.UserFarms.Remove(userFarm);
                await _db.SaveChangesAsync();
            }
        }

        public async Task<UserFarm> GetUserFarmByUserIdFarmId(int userId, int farmId)
        {
            return await _db.UserFarms.FirstOrDefaultAsync(uf => uf.UserId == userId && uf.FarmId == farmId);
        }
    }
}
