using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessAccessLayer.DTOs
{
    public class InnogotchiCreateDTO
    {
        public int? InnogotchiId { get; set; }
        public int FarmId { get; set; }
        public string InnogotchiName { get; set; }
        public int BodyNumber { get; set; }
        public int EyesNumber { get; set; }
        public int NoseNumber { get; set; }
        public int MouthNumber { get; set; }
    }
}
