using InternAPI.Models;
using InternAPI.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;

namespace InternAPI.Controllers
{
    [Route("api/company")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private readonly ICompanyRepository _companyRepo;

        public CompanyController(ICompanyRepository companyRepo)
        {
            _companyRepo = companyRepo;
        }

        [HttpGet]
        public IActionResult GetCompanies()
        {
            var companyList = _companyRepo.GetCompanies();
            return Ok(companyList);
        }

        [HttpGet("byId/id={companyId:int}")]
        public IActionResult GetCompany(int companyId)
        {
            var company = _companyRepo.GetCompany(companyId);
            if (company is null)
                return StatusCode(404);
            return Ok(company);
        }

        [HttpGet("byName/id={companyName}")]
        public IActionResult GetCompany(string companyName)
        {
            var company = _companyRepo.GetCompany(companyName);
            if (company is null)
                return StatusCode(404);
            return Ok(company);
        }

        [HttpPost]
        public IActionResult CreateCompany([FromBody] Company company)
        {
            if (company is null)
                return BadRequest(ModelState);

            if (!_companyRepo.CompanyExists(company.Name))
            {
                ModelState.AddModelError("", $"A company already exists with name {company.Name}");
                return StatusCode(404, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_companyRepo.CreateCompany(company))
            {
                ModelState.AddModelError("", $"Something wrong happened when adding company {company.Name}");
            }
            return Ok(company);
        }

        [HttpPatch]
        public IActionResult UpdateCompany([FromBody] Company company)
        {
            if (company is null)
                return BadRequest(ModelState);

            if (!_companyRepo.CompanyExists(company.Id))
            {
                ModelState.AddModelError("", $"Company doesnt exist with id {company.Id}");
                return StatusCode(404, ModelState);
            }

            if (!_companyRepo.UpdateCompany(company))
            {
                ModelState.AddModelError("", $"Something went wrong when updating company with id {company.Id}");
                return StatusCode(404, ModelState);
            }
            return NoContent();
        }

        [HttpDelete("{companyId:int}")]
        public IActionResult RemoveCompany(int companyId)
        {
            if (!_companyRepo.CompanyExists(companyId))
            {
                ModelState.AddModelError("", $"Company with id of {companyId} doesn't exist");
            }

            var company = _companyRepo.GetCompany(companyId);

            if (!_companyRepo.RemoveCompany(company))
            {
                ModelState.AddModelError("", $"Something went wrong when deleting company with id {companyId}");
                return StatusCode(404, ModelState);
            }
            return NoContent();
        }
    }
}