namespace InternAPI.Controllers
{
    using System.Threading.Tasks;
    using InternAPI.Repository.IRepository;
    using Microsoft.AspNetCore.Mvc;

    [Route("api/internship")]
    [ApiController]
    public class InternshipController : ControllerBase
    {
        private readonly IInternshipRepository _internRepo;

        public InternshipController(IInternshipRepository repo)
        {
            _internRepo = repo;
        }


        [HttpGet("byId/id={companyId:int}")]

        public IActionResult GetStudent(int companyId)
        {
            var student = _internRepo.GetByCompanyId(companyId);
            if (student is null)
                return StatusCode(404);
            return Ok(student);
        }
        [HttpGet("byAdvisor/advisor={advisorName}")]

        public IActionResult GetAdvisor(string advisorName)
        {
            var student = _internRepo.GetByAdvisor(advisorName);
            if (student is null)
                return StatusCode(404);
            return Ok(student);
        }
        [HttpGet("byFaculty/faculty={facultyName}")]

        public IActionResult GetFaculty(string facultyName)
        {
            var student = _internRepo.GetByFaculty(facultyName);
            if (student is null)
                return StatusCode(404);
            return Ok(student);
        }
    }
}