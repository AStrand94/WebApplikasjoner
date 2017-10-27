using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApplication3.DAL
{
    [ExcludeFromCodeCoverage]
    public class LoginDAL : ILoginDAL
    {
        public bool userExists(string username, byte[] password)
        {
            using (DB db = new DB()) {
                return db.Users.Any(user => user.Username == username && user.Password == password);
            }
        }
    }
}
