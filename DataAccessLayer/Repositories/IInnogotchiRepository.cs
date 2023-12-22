using DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories
{
    public interface IInnogotchiRepository
    {
        Task<Innogotchi> GetInnogotchiById(int innogotchiId);
        Task<List<Innogotchi>> GetAllInnogotchis();
        Task AddInnogotchi(Innogotchi innogotchi);
        Task UpdateInnogotchi(Innogotchi innogotchi);
        Task DeleteInnogotchi(int innogotchiId);
        Task<Innogotchi> GetInnogotchiByName(string innogotchiName);  
    }
}
