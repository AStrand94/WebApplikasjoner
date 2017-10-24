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
            throw new NotImplementedException();
        }
    }
}
