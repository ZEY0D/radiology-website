using RadiologyHIS.API.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace RadiologyHIS.API.Data.Services
{
    public class AuthService : IAuthService
    {
        private readonly HISContext _hisContext;

        public AuthService(HISContext context)
        {
            _hisContext = context;
        }



        public async Task<bool> SignUpAsync(SignUpRequest signUpRequest)
        {
            // Check if email exists before
            var existingUser = await _hisContext.Users.FirstOrDefaultAsync(u => u.Email == signUpRequest.Email);
            // existingUser must be null to sign it up
            if (existingUser != null)
                return false;

            // Create User
            var user = new User
            {
                Name = signUpRequest.Name,
                Email = signUpRequest.Email,
                PasswordHash = HashPassword(signUpRequest.Password),
                Role = "patient",   // doctors cannot sign up, they're created by admins
            };

            _hisContext.Users.Add(user);
            await _hisContext.SaveChangesAsync();

            // Create Patient Profile adding extra data
            var patient = new Patient
            {
                UserId = user.Id,
                DateOfBirth = signUpRequest.DateOfBirth.ToUniversalTime(),
                Gender = signUpRequest.Gender,
            };

            _hisContext.Patients.Add(patient);
            await _hisContext.SaveChangesAsync();

            return true;
        }





public async Task<User?> LoginAsync(LoginRequest loginRequest)
{
    var user = await _hisContext.Users
        .FirstOrDefaultAsync(u => u.Email == loginRequest.Email && u.Role == loginRequest.Role); // filter by role too

    if (user == null)
        return null;

    if (!VerifyPassword(loginRequest.Password, user.PasswordHash))
        return null;

    return user;
}


        // Helper Methods
        private string HashPassword(string password)
        {
            using var sha = SHA256.Create();
            var bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(bytes);
        }

        private bool VerifyPassword(string password, string hashedPassword)
        {
            var hashOfInput = HashPassword(password);
            return hashOfInput == hashedPassword;
        }
    }
}
