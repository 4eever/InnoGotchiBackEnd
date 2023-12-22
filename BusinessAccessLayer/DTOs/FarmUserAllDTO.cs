using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessAccessLayer.DTOs
{
    public class FarmUserAllDTO
    {
        public int FarmId { get; set; }
        public int UserId { get; set; }
        public string FarmName { get; set; }
        public int PetsAlive { get; set; }
    }
}
