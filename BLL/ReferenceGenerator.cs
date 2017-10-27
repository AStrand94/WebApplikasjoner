using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Web;
using WebApplication3.DAL;

namespace WebApplication3.BLL
{
    [ExcludeFromCodeCoverage]
    public class ReferenceGenerator : IReferenceGenerator
    {
        private static readonly String CHARACTERS = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        private static readonly int LENGTH = 6;

        public String getReferenceNumber()
        {
            var referenceNumber = new StringBuilder();
            Random r = new Random();


            for(var i = 0; i < LENGTH; i++)
            {
                var character = CHARACTERS[r.Next(0, CHARACTERS.Length)];
                referenceNumber.Append(character);
            }
            string s = referenceNumber.ToString();

            if(!new OrderDAL().OrdersHasReferenceNumber(s))
            {
                return referenceNumber.ToString();
            }
            else
            {
                return getReferenceNumber();
            }
        }
    }
}