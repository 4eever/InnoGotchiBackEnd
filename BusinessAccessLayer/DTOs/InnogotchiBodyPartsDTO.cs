using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessAccessLayer.DTOs
{
    public class InnogotchiBodyPartsDTO
    {
        public int InnogotchiId { get; set; }
        public int FarmId { get; set; }
        public string InnogotchiName { get; set; }
        public string Body { get; set; }
        public string Eyes { get; set; }
        public string Nose { get; set; }
        public string Mouth { get; set; }
        public int HappinessDays { get; set; }

        public int Age { get; set; }
        public string HungerLevel { get; set; }
        public string ThirstLevel { get; set; }

        //конструктор с параметрами
        public InnogotchiBodyPartsDTO(int innogotchiId, int farmId, string innogotchiName, string body, string eyes, string nose, string mouth, int happinessDays, int age, string hungerLevel, string thirstLevel)
        {
            InnogotchiId = innogotchiId;
            FarmId = farmId;
            InnogotchiName = innogotchiName;
            Body = body;
            Eyes = eyes;
            Nose = nose;
            Mouth = mouth;
            HappinessDays = happinessDays;
            Age = age;
            HungerLevel = hungerLevel;
            ThirstLevel = thirstLevel;
        }
    }
}
