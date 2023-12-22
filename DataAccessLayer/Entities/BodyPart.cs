using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Entities
{
    public class BodyPart
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BodyPartId { get; set; }

        [Required]
        public string BodyPartName { get; set; }

        public ICollection<InnogotchiBodyPart> InnogotchiBodyParts { get; set; }
    }
}
