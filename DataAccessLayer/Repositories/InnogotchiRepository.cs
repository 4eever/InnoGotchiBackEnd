using DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories
{
    public class InnogotchiRepository : IInnogotchiRepository
    {
        public readonly ApplicationContext _db;

        public InnogotchiRepository(ApplicationContext db)
        {
            _db = db;
        }

        public async Task AddInnogotchi(Innogotchi innogotchi)
        {
            _db.Innogotchis.Add(innogotchi);
            await _db.SaveChangesAsync();
        }

        public async Task DeleteInnogotchi(int innogotchiId)
        {
            var innogotchi = await _db.Innogotchis.FindAsync(innogotchiId);
            if (innogotchi != null)
            {
                _db.Innogotchis.Remove(innogotchi);
                await _db.SaveChangesAsync();
            }
        }

        public async Task<List<Innogotchi>> GetAllInnogotchis()
        {
            return _db.Innogotchis.ToList();
        }

        public async Task<Innogotchi> GetInnogotchiById(int innogotchiId)
        {
            return await _db.Innogotchis.FindAsync(innogotchiId);
        }

        public async Task UpdateInnogotchi(Innogotchi innogotchi)
        {
            try
            {
                var existingInnogotchi = await _db.Set<Innogotchi>().FindAsync(innogotchi.InnogotchiId);

                if (existingInnogotchi != null)
                {
                    _db.Entry(existingInnogotchi).State = EntityState.Detached;
                }

                _db.Update(innogotchi);
                
                await _db.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error updating Innogotchi: {ex.Message}", ex);
            }
        }


        public async Task<Innogotchi> GetInnogotchiByName(string innogotchiName)
        {
            return await _db.Innogotchis.FirstOrDefaultAsync(i => i.InnogotchiName == innogotchiName);
        }
    }
}
