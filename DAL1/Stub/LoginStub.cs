using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApplication3.DAL
{
    public class LoginStub : ILoginDAL
    {
        public bool userExists(string username, byte[] password)
        {
            if (username == "admin" && password.ToString() == GetCryptedPassword("admin").ToString())
            {
                return true;
            }

            return false;
        }

        private byte[] GetCryptedPassword(String password)
        {
            var algorithm = System.Security.Cryptography.SHA512.Create();

            byte[] inData = System.Text.Encoding.ASCII.GetBytes(password);
            byte[] outData = algorithm.ComputeHash(inData);

            return outData;
        }
    }
}
