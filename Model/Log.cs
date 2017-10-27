using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApplication3.Model
{
    [ExcludeFromCodeCoverage]
    public class Log
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Entity { get; set; }

        [Required]
        [StringLength(14)]
        public string Type { get; set; }

        [Required]
        public string Description { get; set; }
    }
}
