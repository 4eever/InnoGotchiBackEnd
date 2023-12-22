using DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories
{
    public class FarmRepository : IFarmRepository
    {
        private readonly ApplicationContext _db;

        public FarmRepository(ApplicationContext db)
        {
            _db = db;
        }

        public async Task AddFarm(Farm farm)
        {
            _db.Farms.Add(farm);
            await _db.SaveChangesAsync();
        }

        public async Task DeleteFarm(int farmId)
        {
            var farm = await _db.Farms.FindAsync(farmId);
            if (farm != null)
            {
                _db.Farms.Remove(farm);
                await _db.SaveChangesAsync();
            }
        }

        public async Task<List<Farm>> GetAllFarms()
        {
            return await _db.Farms.ToListAsync();
        }

        public async Task<Farm> GetFarmById(int farmId)
        {
            return await _db.Farms.FindAsync(farmId);
        }

        public async Task UpdateFarm(Farm farm)
        {
            _db.Entry(farm).State = EntityState.Modified;
            await _db.SaveChangesAsync();
        }   

        public async Task<Farm> GetFarmByName(string farmName)
        {
            return await _db.Farms.FirstOrDefaultAsync(f => f.FarmName == farmName);
        }

        public async Task<Farm> GetFarmByUserId(int userId)
        {
            return await _db.Farms.FirstOrDefaultAsync(f => f.UserId == userId);
        }
    }
}
