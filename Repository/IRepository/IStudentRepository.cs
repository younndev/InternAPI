using InternAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InternAPI.Repository.IRepository
{
    public interface IStudentRepository
    {
        ICollection<Student> GetStudents();
        Student GetStudent(int id);
        Student GetStudent(string name, string lastName);
        Student GetStudent(string mail);

        bool AddStudent(Student student);
        bool UpdateStudent(Student student);
        bool RemoveStudent(Student student);

        bool StudentExists(int id);
        bool StudentExists(string name, string lastName);
        bool StudentExists(string mail);
        bool Save();




    }
}
