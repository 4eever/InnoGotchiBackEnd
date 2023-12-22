using DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories
{
    public interface IInnogotchiBodyPartRepository
    {
        Task<InnogotchiBodyPart> GetInnogotchiBodyPartByBodyPartIdAndNumber(int bodyPartId, int bodyPartnumber);
    }
}
