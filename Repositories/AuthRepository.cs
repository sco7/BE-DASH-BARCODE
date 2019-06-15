using FontaineVerificationProject.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FontaineVerificationProject.Repositories
{
    public class AuthRepository: IAuthRepository
    {
        private readonly FontaineContext _context;

        public AuthRepository(FontaineContext context)
        {
            _context = context;
        }

        public async Task<User> Login(string user, string password)
        {
            var currentUser = await _context.User.FirstOrDefaultAsync(x => x.UserName == user);
            if (currentUser == null)
                return null;

            if (!VerifyPasswordHash(password, currentUser.Password, currentUser.Salt))
                return null;

            return currentUser; // auth successful
        }

        public async Task<User> Register(User user, string password)
        {
            byte[] passwordHash, salt;
            CreatePasswordHash(password, out passwordHash, out salt);
            user.Password = passwordHash;
            user.Salt = salt;

            await _context.User.AddAsync(user);
            await _context.SaveChangesAsync();

            return user;
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] salt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(salt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != passwordHash[i]) return false;
                }
            }
            return true;
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] salt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                salt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        public async Task<bool> UserExists(string userName)
        {
            if (await _context.User.AnyAsync(x => x.UserName == userName))
                return true;
            return false;
        }
    }
}