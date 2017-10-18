using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication3.DAL;

namespace WebApplication3.BLL
{
    public class LoginBLL
    {
        private DB db = new DB();

        public bool checkLogin(string username, string password)
        {
            var algorithm = System.Security.Cryptography.SHA512.Create();

            byte[] inData = System.Text.Encoding.ASCII.GetBytes(password);
            byte[] outData = algorithm.ComputeHash(inData);

            LoginDAL loginDAL = new LoginDAL(db);
            return (loginDAL.userExists(username, outData));
        }
    }
}
