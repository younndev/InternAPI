using InternAPI.Data;
using InternAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace InternAPI.Repository.IRepository
{
    public class StudentRepository : IStudentRepository
    {

        private readonly ApplicationDbContext _db;
        public StudentRepository(ApplicationDbContext db)
        {
            _db = db;
        }



        public bool AddStudent(Student student)
        {

            _db.Student.Add(student);
            return Save();

        }

        public Student GetStudent(int id)
        {
            return _db.Student.Include(c => c.Company).FirstOrDefault(x => x.Id == id);

        }

        public Student GetStudent(string name, string lastName)
        {
            return _db.Student.Include(c => c.Company).FirstOrDefault(x => x.Name.ToLower().Trim() == name.ToLower().Trim() && x.LastName.ToLower().Trim() == lastName.ToLower().Trim());
        }

        public Student GetStudent(string mail)
        {
            return _db.Student.Include(c => c.Company).FirstOrDefault(x => x.Mail.Trim().ToLower() == mail.Trim().ToLower());
        }

        public ICollection<Student> GetStudents()
        {
            return _db.Student.Include(c => c.Company).OrderBy(x => x.Id).ToList();

        }

        public bool StudentExists(int id)
        {
            return _db.Student.Include(c => c.Company).Any(x => x.Id == id);
        }

        public bool StudentExists(string name, string lastName)
        {
            return _db.Student.Include(c => c.Company).Any(x => x.Name.Trim().ToLower() == name.Trim().ToLower() && x.LastName.Trim().ToLower() == lastName.Trim().ToLower());
        }


        public bool RemoveStudent(Student student)
        {
            _db.Student.Remove(student);
            return Save();
        }

        public bool UpdateStudent(Student student)
        {
            _db.Student.Update(student);
            return Save();

        }

        public bool Save()
        {
            return _db.SaveChanges() >= 0 ? true : false;
        }

        public bool StudentExists(string mail)
        {
            return _db.Student.Any(x => x.Mail.Trim().ToLower() == mail.Trim().ToLower());
        }

    }
}
