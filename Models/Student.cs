using System;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace InternAPI.Models
{
    public class Student
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public string LastName { get; set; }

        public string Faculty { get; set; }

        public string Department { get; set; }

        public string Address { get; set; }

        public string Phone { get; set; }

        public string Mail { get; set; }

        public string Password { get; set; }

        public Company Company { get; set; }

        public string InternshipDepartment { get; set; }

        public string Advisor { get; set; }

        public int InsuranceNo { get; set; }
        
        [NotMapped]
        public string Token { get; set; }

    }
}
