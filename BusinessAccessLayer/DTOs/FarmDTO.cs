using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessAccessLayer.DTOs
{
    public class FarmDTO
    {
        public int FarmId { get; set; } //изменить на int? вслучае надобности
        public int UserId { get; set; }
        public string FarmName { get; set; }
        public int PetsAlive { get; set; }
        public int PetsDead { get; set; }
    }
}
