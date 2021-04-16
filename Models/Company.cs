using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace InternAPI.Models
{
    public class Company
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public string Sector { get; set; }

        public string Address { get; set; }

        public string Phone { get; set; }

        public string Mail { get; set; }

        public string Website { get; set; }
    }
}
