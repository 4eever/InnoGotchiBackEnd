using BusinessAccessLayer.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessAccessLayer.Services
{
    public interface IUserFarmService
    {
        public Task<UserFarmDTO> CreateUserFarm(UserFarmDTO userFarmDTO);
        public Task<UserFarmDTO> GetUserFarm(UserFarmDTO userFarmDTO);
    }
}
