using InternAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InternAPI.Repository.IRepository
{
    public interface ICompanyRepository
    {
        ICollection<Company> GetCompanies();
        Company GetCompany(int id);
        Company GetCompany(string name);

        bool CreateCompany(Company company);
        bool UpdateCompany(Company company);
        bool RemoveCompany(Company company);

        bool CompanyExists(int id);
        bool CompanyExists(string name);
        bool Save();

    }
}
