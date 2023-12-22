using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessAccessLayer.DTOs
{
    public class FarmCreateDTO
    {
        public int? FarmId { get; set; }
        public int UserId { get; set; }
        public string FarmName { get; set; }
    }
}
