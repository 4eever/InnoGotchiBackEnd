using DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessAccessLayer.DTOs
{
    public class UserFarmDTO
    {
        public int UserId { get; set; }
        public int FarmId { get; set; }
        public int? RoleId { get; set; }

        public UserFarmDTO(int userId, int farmId, int? roleId)
        {
            UserId = userId;
            FarmId = farmId;
            RoleId = roleId;
        }
    }
}
