using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebApplication3.Model
{
    public class Customer
    {
        public int Id { get; set; }

        [Required]
        public string Firstname { get; set; }

        [Required]
        public string Lastname { get; set; }

        [Required]
        [DataType(DataType.PhoneNumber)]
        public string Telephone { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        public virtual List<Order> Order { get; set; }

        public string FullInfo
        {
            get
            {
                return String.Format("{0}, {1}", Lastname, Firstname);
            }
        }
    }
}