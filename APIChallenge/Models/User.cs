using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace APIChallenge.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public Security Security { get; set; }

        public User()
        {
            Security = new Security();
        }

        public bool UserIsValid(ref string message)
        {
            bool isValid = true;

            if (Email == "")
            {
                message += "Email,";
                isValid = false;
            }
            if (Password == "")
            {
                message += "Password,";
                isValid = false;
            }

            if (message.LastIndexOf(",") != -1)
            {
                message = message.Remove(message.LastIndexOf(","));
            }

            return isValid;
        }

        public string HashStringPassword(string password)
        {
            byte[] salt = new byte[128 / 8];

            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }

            Security.SaltPassword = salt;

            return HashString(password, salt);
        }

        public string HashString(string value, byte[] salt)
        {
            value = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: value,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA512,
                iterationCount: 10000,
                numBytesRequested: 512 / 8
            ));

            return value;
        }
    }
}
