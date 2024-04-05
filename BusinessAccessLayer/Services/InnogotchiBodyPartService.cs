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
        private readonly IInnogotchiBodyPartRepository _innogotchibodyPartRepository;
        private readonly IBodyPartRepository _bodyPartRepository;

        public InnogotchiBodyPartService(IInnogotchiBodyPartRepository innogotchibodyPartRepository, IBodyPartRepository bodyPartRepository)
        {
            _innogotchibodyPartRepository = innogotchibodyPartRepository;
            _bodyPartRepository = bodyPartRepository;
        }

        public async Task<string?> GetBodyPartImage(string bodyPartName, int bodyPartNumber)
        {
            BodyPart bodyPart = await _bodyPartRepository.GetBodyPartByName(bodyPartName);

            InnogotchiBodyPart innogotchiBodyPart = await _innogotchibodyPartRepository.GetInnogotchiBodyPartByBodyPartIdAndNumber(bodyPart.BodyPartId, bodyPartNumber);

            if (innogotchiBodyPart == null)
            {
                return null;
            }
            else
            {
                return innogotchiBodyPart.InnogotchiBodyPartImage;
            }
        }
    }
}
