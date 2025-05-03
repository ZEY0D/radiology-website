using System;
using RadiologyHIS.API.Models;

namespace RadiologyHIS.API.Data.Service;

public interface IUserService
{
     Task<IEnumerable<User>> GetAllUsersAsync();
     Task<User?> GetUserByIdAsync(int id);

    Task<User> AddUserAsync(User user);
    Task<User?> UpdateUserAsync(int id,User user);
    Task<bool> DeleteUserAsync(int id);
}
