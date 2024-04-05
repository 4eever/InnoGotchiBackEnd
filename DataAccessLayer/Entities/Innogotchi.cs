using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Entities
{
    public class Innogotchi
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int InnogotchiId { get; set; }

        [Required]
        public string InnogotchiName { get; set; }

        [Required]
        public int BodyNumber { get; set; }

        [Required]
        public int EyesNumber { get; set; }

        [Required]
        public int NoseNumber { get; set; }

        [Required]
        public int MouthNumber { get; set; }

        [Required]
        public DateTime PetDOB { get; set; }

        [Required]
        public DateTime FedLastTime { get; set; }

        [Required]
        public int SumFedPeriods { get; set; }

        [Required]
        public int FedCount { get; set; }

        [Required]
        public DateTime DrintLastTime { get; set; }

        [Required]
        public int SumDrinkPeriods { get; set; }

        [Required]
        public int DrinkCount { get; set; }

        [Required]
        public int HappinessDays { get; set; }

        [Required]
        public DateTime LastCheckHappinessDays { get; set; }

        // Внешний ключ для связи с Farm
        [Required]
        public int FarmId { get; set; }

        [ForeignKey("FarmId")]
        public Farm Farm { get; set; }
    }
}
