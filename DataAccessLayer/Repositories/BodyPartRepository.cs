using DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories
{
    public class BodyPartRepository : IBodyPartRepository
    {
        private readonly  ApplicationContext _db;

        public BodyPartRepository(ApplicationContext db)
        {
            _db = db;
        }

        public async Task<BodyPart> GetBodyPartByName(string bodyPartName)
        {
            return await _db.BodyParts.FirstOrDefaultAsync(x => x.BodyPartName == bodyPartName);
        }
    }
}
