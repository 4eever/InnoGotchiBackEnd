using DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessAccessLayer.Services
{
    public interface IInnogotchiBodyPartService
    {
        public Task<string> GetBodyPartImage(int bodyPartId, int bodyPartNumber);
    }
}
