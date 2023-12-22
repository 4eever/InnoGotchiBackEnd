using DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories
{
    public class InnogotchiBodyPartRepository : IInnogotchiBodyPartRepository
    {
        public readonly ApplicationContext _db;

        public InnogotchiBodyPartRepository(ApplicationContext db)
        {
            _db = db;
        }

        public async Task<InnogotchiBodyPart> GetInnogotchiBodyPartByBodyPartIdAndNumber(int bodyPartId, int bodyPartnumber)
        {
            return await _db.InnogotchiBodyParts.FirstOrDefaultAsync(i => i.BodyPartId == bodyPartId && i.InnogotchiBodyPartNumber == bodyPartnumber);
        }
    }
}
