using System.Collections.Generic;
using System.Linq;
using InternAPI.Data;
using InternAPI.Models;
using InternAPI.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace InternAPI.Repository
{
    public class InternshipRepository : IInternshipRepository
    {
        private readonly ApplicationDbContext _db;
        public InternshipRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public ICollection<Student> GetByFaculty(string facultyName)
        {
            return _db.Student.Include(c => c.Company).Where(x => x.Faculty.ToLower() == facultyName.ToLower()).ToList();
        }

        public ICollection<Student> GetByAdvisor(string advisorName)
        {
            return _db.Student.Include(c => c.Company).Where(x => x.Advisor.ToLower() == advisorName.ToLower()).ToList();
        }

        public ICollection<Student> GetByCompanyId(int id)
        {
            return _db.Student.Where(x => x.Company.Id == id).ToList();
        }

        public bool Save()
        {
            return _db.SaveChanges() >= 0 ? true : false;
        }


    }
}