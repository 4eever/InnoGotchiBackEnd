using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessAccessLayer.DTOs
{
    public class UserEmailFarmDTO
    {
        public string UserEmail { get; set; }
        public int FarmId { get; set; }
        public int? RoleId { get; set; }
    }
}
