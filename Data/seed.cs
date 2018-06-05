using System.Collections.Generic;
using Datingapp.API.Models;
using Newtonsoft.Json;

namespace Datingapp.API.Data
{
    public class seed
    {
        private readonly DataContext _context;
        public seed(DataContext context)
        {
            this._context = context;

        }

        public void SeedUsers()
        {
            // context.User.RemoveRange(context.User);
            // context.SaveChanges();

            //Seed User
            var userData = System.IO.File.ReadAllText("Data/userSeedData.json");
            var users = JsonConvert.DeserializeObject<List<User>>(userData);
            foreach (var user in users)
            {
                //create password hash
                byte[] passwordhash, passwordSalt;
                createPasswordHash("password", out passwordhash, out passwordSalt);
                user.PasswordHash = passwordhash;
                user.PasswordSalt = passwordSalt;
                user.UserName = user.UserName.ToLower();
                _context.User.Add(user);

            }
            _context.SaveChanges();
        }
        private void createPasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }
    }
}