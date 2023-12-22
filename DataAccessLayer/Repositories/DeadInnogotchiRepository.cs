using DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories
{
    public class DeadInnogotchiRepository : IDeadInnogotchiRepository
    {
        private readonly ApplicationContext _db;

        public DeadInnogotchiRepository(ApplicationContext db)
        {
            _db = db;
        }

        public async Task AddDeadInnogotchi(DeadInnogotchi deadInnogotchi)
        {
            _db.DeadInnogotchis.Add(deadInnogotchi);
            await _db.SaveChangesAsync();
        }

        public async Task DeleteDeadInnogotchi(int deadInnogotchiId)
        {
            var deadInnogotchi = await _db.DeadInnogotchis.FindAsync(deadInnogotchiId);
            if (deadInnogotchi != null)
            {
                _db.DeadInnogotchis.Remove(deadInnogotchi);
                await _db.SaveChangesAsync();
            }
        }

        public async Task<List<DeadInnogotchi>> GetAllDeadInnogotchies()
        {
            return await _db.DeadInnogotchis.ToListAsync();
        }

        public async Task<DeadInnogotchi> GetDeadInnogotchiById(int deadInnogotchiId)
        {
            return await _db.DeadInnogotchis.FindAsync(deadInnogotchiId);
        }

        public async Task UpdateDeadInnogotchi(DeadInnogotchi deadInnogotchi)
        {
            _db.Entry(deadInnogotchi).State = EntityState.Modified;
            await _db.SaveChangesAsync();
        }
    }
}
