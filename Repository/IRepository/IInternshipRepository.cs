using InternAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InternAPI.Repository.IRepository
{
    public interface IInternshipRepository
    {
        ICollection<Student> GetByCompanyId(int id);
        ICollection<Student> GetByFaculty(string facultyName);
        ICollection<Student> GetByAdvisor(string advisorName);

    }
}
