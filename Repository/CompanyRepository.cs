using System.Collections.Generic;
using System.Linq;
using InternAPI.Data;
using InternAPI.Models;
using InternAPI.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace InternAPI.Repository
{
    public class CompanyRepository : ICompanyRepository
    {

        private readonly ApplicationDbContext _db;
        public CompanyRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        public bool CompanyExists(int id)
        {
            return _db.Companies.Any(x => x.Id == id);
        }

        public bool CompanyExists(string name)
        {
            return _db.Companies.Any(x => EF.Functions.FreeText(x.Name, name));
        }

        public bool CreateCompany(Company company)
        {
            _db.Companies.Add(company);
            return Save();
        }

        public ICollection<Company> GetCompanies()
        {
            return _db.Companies.ToList();
        }

        public Company GetCompany(int id)
        {
            return _db.Companies.FirstOrDefault(x => x.Id == id);
        }

        public Company GetCompany(string name)
        {
            return _db.Companies.FirstOrDefault(x => EF.Functions.FreeText(x.Name, name));
        }

        public bool RemoveCompany(Company company)
        {
            _db.Companies.Remove(company);
            return Save();
        }

        public bool Save()
        {
            return _db.SaveChanges() >= 0 ? true : false;
        }

        public bool UpdateCompany(Company company)
        {
            _db.Companies.Update(company);
            return Save();
        }
    }
}