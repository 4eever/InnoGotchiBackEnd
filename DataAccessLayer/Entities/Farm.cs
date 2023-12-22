using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace DataAccessLayer.Entities
{
    public class Farm
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int FarmId { get; set; }

        [Required]
        public string FarmName { get; set; }

        [Required]
        public int PetsAlive { get; set; }

        [Required]
        public int PetsDead { get; set; }

        // Свойство UserId для связи с пользователем
        [Required]
        public int UserId { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; }

        public ICollection<Innogotchi> Innogotchis { get; set; }

        public ICollection<UserFarm> UserFarms { get; set; }

        public ICollection<DeadInnogotchi> DeadInnogotchis { get; set; }
    }
}
