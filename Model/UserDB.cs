using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApplication3.Model
{
    public class UserDB
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public byte[] Password { get; set; }
    }
}
