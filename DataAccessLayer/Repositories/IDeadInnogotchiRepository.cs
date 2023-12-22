using DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories
{
    public interface IDeadInnogotchiRepository
    {
        Task<DeadInnogotchi> GetDeadInnogotchiById(int deadInnogotchiId);
        Task<List<DeadInnogotchi>> GetAllDeadInnogotchies();
        Task AddDeadInnogotchi(DeadInnogotchi deadInnogotchi);
        Task UpdateDeadInnogotchi(DeadInnogotchi deadInnogotchi);
        Task DeleteDeadInnogotchi(int deadInnogotchiId);
    }
}
