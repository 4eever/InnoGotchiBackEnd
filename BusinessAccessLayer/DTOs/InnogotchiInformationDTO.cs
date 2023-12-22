using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessAccessLayer.DTOs
{
    public class InnogotchiInformationDTO
    {
        public int InnogotchiId { get; set; }
        public int FarmId { get; set; }
        public string InnogotchiName { get; set; }
        public int BodyNumber { get; set; }
        public int EyesNumber { get; set; }
        public int NoseNumber { get; set; }
        public int MouthNumber { get; set; }
        public int HappinessDays { get; set; }

        public int Age { get; set; }
        public string HungerLevel { get; set; }
        public string ThirstLevel { get; set; } 

        //конструктор с параметрами
        public InnogotchiInformationDTO(int innogotchiId, int farmId, string innogotchiName, int bodyNumber, int eyesNumber, int noseNumber, int mouthNumber, int happinessDays, int age, string hungerLevel, string thirstLevel)
        {
            InnogotchiId = innogotchiId;
            FarmId = farmId;
            InnogotchiName = innogotchiName;
            BodyNumber = bodyNumber;
            EyesNumber = eyesNumber;
            NoseNumber = noseNumber;
            MouthNumber = mouthNumber;
            HappinessDays = happinessDays;
            Age = age;
            HungerLevel = hungerLevel;
            ThirstLevel = thirstLevel;
        }
    }
}
