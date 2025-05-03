using System;
using Microsoft.EntityFrameworkCore;
using RadiologyHIS.API.Models;

namespace RadiologyHIS.API.Data.Service;

public class UserService : IUserService
{
    private readonly HISContext _hisContext;
    public UserService(HISContext hisContext){
        _hisContext = hisContext;
    }

    public async Task<IEnumerable<User>> GetAllUsersAsync(){
        return await _hisContext.Users.ToListAsync();
    }

    public async Task<User?> GetUserByIdAsync(int id){
        return await _hisContext.Users.FindAsync(id);
    }

    public async Task<User> AddUserAsync(User user){
        _hisContext.Users.Add(user);
        await _hisContext.SaveChangesAsync();
        return user;
    }

        public async Task<User?> UpdateUserAsync(int id, User user)
        {
            var existingUser = await _hisContext.Users.FindAsync(id);
            if (existingUser == null)
                return null;

            existingUser.Name = user.Name;
            existingUser.Email = user.Email;
            existingUser.PasswordHash = user.PasswordHash;
            existingUser.Role = user.Role;
            existingUser.ProfileImage = user.ProfileImage;

            await _hisContext.SaveChangesAsync();
            return existingUser;
        }

        public async Task<bool> DeleteUserAsync(int id){
            var user = await _hisContext.Users.FindAsync(id);
            if (user == null){
                return false;
            }

            _hisContext.Users.Remove(user);
            await _hisContext.SaveChangesAsync();
            return true;
        }
}
