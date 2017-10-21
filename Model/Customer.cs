using System;
using System.Collections.Generic;

namespace WebApplication3.Model
{
    public class Customer
    {
        public int Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Telephone { get; set; }
        public string Email { get; set; }
        public virtual List<Order> Order { get; set; }
        public string FullInfo
        {
            get
            {
                return String.Format("{0} {1}, {2}, {3}", Firstname, Lastname, Telephone, Email);
            }
        }
    }
}