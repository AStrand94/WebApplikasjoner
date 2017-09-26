using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace WebApplication3.Models
{
    public class ReferenceGenerator
    {
        private static readonly String CHARACTERS = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        private static readonly int LENGTH = 6;

        public String getReferenceNumber(DB db)
        {
            var referenceNumber = new StringBuilder();
            Random r = new Random();


            for(var i = 0; i < LENGTH; i++)
            {
                var character = CHARACTERS[r.Next(0, CHARACTERS.Length)];
                referenceNumber.Append(character);
            }
            string s = referenceNumber.ToString();

            if(!db.Orders.Where(o => o.Reference.Equals(s)).Any())
            {
                return referenceNumber.ToString();
            }
            else
            {
                return getReferenceNumber(db);
            }
        }
    }
}