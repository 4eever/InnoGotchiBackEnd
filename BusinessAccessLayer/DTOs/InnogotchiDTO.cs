using DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessAccessLayer.DTOs
{
    public class InnogotchiDTO
    {
        public int InnogotchiId { get; set; }
        public string InnogotchiName { get; set; }
        public int BodyNumber { get; set; }
        public int EyesNumber { get; set; }
        public int NoseNumber { get; set; }
        public int MouthNumber { get; set; }
        public DateTime PetDOB { get; set; }
        public DateTime FedLastTime { get; set; }
        public int SumFedPeriods { get; set; }
        public int FedCount { get; set; }
        public DateTime DrintLastTime { get; set; }
        public int SumDrinkPeriods { get; set; }
        public int DrinkCount { get; set; }
        public int HappinessDays { get; set; }
        public int FarmId { get; set; }
    }
}
