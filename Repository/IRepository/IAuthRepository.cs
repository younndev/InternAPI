using InternAPI.Models;

namespace InternAPI.Repository.IRepository
{
    public interface IAuthRepository
    {
        string Authenticate(string mail, string password);
        Student RegisterStudent(Student student);
        bool IsUnique(string mail);
    }
}