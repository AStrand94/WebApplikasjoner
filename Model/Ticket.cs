using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication3.Model
{
    public class Ticket
    {

        public int Id { get; set; }

        [Required]
        public virtual Order Order { get; set; }

        [Required]
        public virtual Flight Flight { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }
    }
}