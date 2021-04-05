using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Security.Hashing
{
    public class HashingHelper
    {
        public static PasswordHolder CreatePasswordHash(string password)
        {
            var passwordHolder = new PasswordHolder();
            using (var hmac = new HMACSHA512())
            {
                passwordHolder.PasswordSalt = hmac.Key;
                passwordHolder.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                return passwordHolder;
            }
        }

        public static bool VerifyPasswordHash(string password,PasswordHolder passwordHolder)
        {
            using (var hmac=new HMACSHA512(passwordHolder.PasswordSalt))
            {
                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i]!=passwordHolder.PasswordHash[i])
                    {
                        return false;
                    }
                }
            }
            return true;
        }
    }
}
