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
        public static void CreatePasswordHash
            (string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac=new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));   
            }
        }

        /*Salt olmadan password hashlemek için bu metodu kullandım. Salt olmadan hashleme yapmak için
         ShA512 kullandım. Hmac salt key olmadan kullanılmıyor.*/
        public static void CreatePasswordHash
            (string password, out byte[] passwordHash)
        {
            using (SHA512 sha512Hash=SHA512.Create())
            {
                //passwordSalt = hmac.Key;
                passwordHash = sha512Hash.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }

        public static bool VerifyPasswordHash
            (string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var computedHash=hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != passwordHash[i])
                    {
                        return false;
                    }
                }
                return true;
            }
        }

        public static bool VerifyPasswordHash
            (string password, byte[] passwordHash)
        {
            using (SHA512 sha512Hash = SHA512.Create())
            {
                var computedHash = sha512Hash.ComputeHash(Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != passwordHash[i])
                    {
                        return false;
                    }
                }
                return true;
            }
        }
    }
}
