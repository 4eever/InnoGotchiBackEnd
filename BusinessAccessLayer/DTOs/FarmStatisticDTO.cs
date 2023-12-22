using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessAccessLayer.DTOs
{
    public class FarmStatisticDTO
    {
        public int FarmId { get; set; } //изменить на int? вслучае надобности
        public int UserId { get; set; }
        public string FarmName { get; set; }
        public int PetsAlive { get; set; }
        public int PetsDead { get; set; }
        public int AverageFeedPeriod { get; set; }
        public int AverageDrinkPeriod { get; set; }
        public int AverageHappinessDays { get; set; }
        public int AveragePetsAge { get; set; }

        //конструктор с параметрами
        public FarmStatisticDTO(int farmId, int userId, string farmName, int petsAlive, int petsDead, int averageFeedPeriod, int averageDrinkPeriod, int averageHappinessDays, int averagePetsAge)
        {
            FarmId = farmId;
            UserId = userId;
            FarmName = farmName;
            PetsAlive = petsAlive;
            PetsDead = petsDead;
            AverageFeedPeriod = averageFeedPeriod;
            AverageDrinkPeriod = averageDrinkPeriod;
            AverageHappinessDays = averageHappinessDays;
            AveragePetsAge = averagePetsAge;
        }

    }
}
