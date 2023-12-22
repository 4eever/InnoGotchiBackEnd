using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Entities
{
    public class InnogotchiBodyPart
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int InnogotchiBodyPartId { get; set; }

        [Required]
        public int InnogotchiBodyPartNumber { get; set; }

        [Required]
        public string InnogotchiBodyPartImage { get; set; }

        [Required]
        public int BodyPartId { get; set; }

        [ForeignKey("BodyPartId")]
        public BodyPart BodyPart { get; set; }


    }
}
