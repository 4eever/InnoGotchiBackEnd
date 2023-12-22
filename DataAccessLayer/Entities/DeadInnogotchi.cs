using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Entities
{
    public class DeadInnogotchi
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DeadInnogotchiId { get; set; } //unique

        [Required]
        public string DeadInnogotchiName { get; set; }

        [Required]
        public int DeadInnogotchiAge { get; set; }

        [Required]
        public int FarmId { get; set; }

        [ForeignKey("FarmId")]
        public Farm Farm { get; set; }

    }
}
