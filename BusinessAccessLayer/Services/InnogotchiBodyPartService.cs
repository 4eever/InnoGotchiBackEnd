using DataAccessLayer.Entities;
using DataAccessLayer.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessAccessLayer.Services
{
    public class InnogotchiBodyPartService : IInnogotchiBodyPartService
    {
        private readonly IInnogotchiBodyPartRepository _bodyPartRepository;

        public InnogotchiBodyPartService(IInnogotchiBodyPartRepository bodyPartRepository)
        {
            _bodyPartRepository = bodyPartRepository;
        }

        public async Task<string> GetBodyPartImage(int bodyPartId, int bodyPartNumber)
        {
            InnogotchiBodyPart innogotchiBodyPart = await _bodyPartRepository.GetInnogotchiBodyPartByBodyPartIdAndNumber(bodyPartId, bodyPartNumber);
            return innogotchiBodyPart.InnogotchiBodyPartImage;
        }
    }
}
