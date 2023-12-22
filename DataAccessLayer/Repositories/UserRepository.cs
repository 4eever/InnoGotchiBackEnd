using DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IApplicationContext _db;

        public UserRepository(IApplicationContext db)
        {
            _db = db;
        }

        public async Task AddUser(User user)
        {
            _db.Users.Add(user);
            await _db.SaveChangesAsync();
        }

        public async Task DeleteUser(int userId)
        {
            var user = await _db.Users.FindAsync(userId);
            if (user != null)
            {
                _db.Users.Remove(user);
                await _db.SaveChangesAsync();
            }
        }

        public async Task<List<User>> GetAllUsers()
        {
            return await _db.Users.ToListAsync();
        }

        public async Task<User> GetUserById(int userId)
        {
            return await _db.Users.FindAsync(userId);
        }

        public async Task UpdateUser(User user)
        {
            var existingUser = await _db.Users.FindAsync(user.UserId);
            if (existingUser != null)
            {
                existingUser.UserPassword = user.UserPassword;
                existingUser.UserFirstName = user.UserFirstName;
                existingUser.UserLastName = user.UserLastName;
                existingUser.UserAvatar = user.UserAvatar;

                _db.Users.Update(existingUser);

                await _db.SaveChangesAsync();
            }
        }




        public async Task<User> GetUserByEmail(string email)
        {
            return await _db.Users.FirstOrDefaultAsync(user => user.UserEmail == email);
        }
    }
}
