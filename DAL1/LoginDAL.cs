using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApplication3.DAL
{
    public class LoginDAL
    {
        private DB db;

        public LoginDAL(DB db)
        {
            this.db = db;
        }

        public bool userExists(string username, byte[] password)
        {
            return db.Users.Any(user => user.Username == username && user.Password == password);
        }
    }
}
