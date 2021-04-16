using InternAPI.Models;
using InternAPI.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InternAPI.Controllers
{


    [Route("api/student")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IStudentRepository _studentrepo;

        public StudentController(IStudentRepository studentrepo)
        {
            _studentrepo = studentrepo;
        }

        [HttpGet]
        public IActionResult GetStudents()
        {
            var StudentList = _studentrepo.GetStudents();
            return Ok(StudentList);
        }


        [HttpGet("byId/id={id:int}")]
        public IActionResult GetStudent(int id)
        {
            var student = _studentrepo.GetStudent(id);
            if (student is null)
                return StatusCode(404);
            return Ok(student);
        }


        [HttpGet("byMail/mail={mail}")]
        public IActionResult GetStudent(string mail)
        {
            var student = _studentrepo.GetStudent(mail);
            if (student is null)
                return StatusCode(404);
            return Ok(student);
        }

        [HttpGet("byName/name={name}&surname={surname}")]
        public IActionResult GetStudent(string name, string surname)
        {
            var student = _studentrepo.GetStudent(name, surname);
            if (student is null)
                return StatusCode(404);
            return Ok(student);
        }

        [HttpPost]
        public IActionResult AddStudent([FromBody] Student student)
        {
            if (student is null)
                return BadRequest(ModelState);

            if (!_studentrepo.StudentExists(student.Mail))
            {
                ModelState.AddModelError("", $"A student already exists with mail {student.Mail}");
                return StatusCode(404, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_studentrepo.AddStudent(student))
            {
                ModelState.AddModelError("", $"Something wrong happened when adding student {student.Mail}");
            }
            return Ok(student);
        }


        [HttpPatch]
        public IActionResult UpdateStudent([FromBody] Student student)
        {
            if (student is null)
                return BadRequest(ModelState);

            if (!_studentrepo.StudentExists(student.Id))
            {
                ModelState.AddModelError("", $"Student doesnt exist with id {student.Id}");
                return StatusCode(404, ModelState);
            }

            if (!_studentrepo.UpdateStudent(student))
            {
                ModelState.AddModelError("", $"Something went wrong when updating student with id {student.Id}");
                return StatusCode(404, ModelState);
            }
            return NoContent();

        }

        [HttpDelete("{studentId:int}")]
        public IActionResult RemoveStudent(int studentId)
        {
            if (!_studentrepo.StudentExists(studentId))
            {
                ModelState.AddModelError("", $"Student with id of {studentId} doesn't exist");
            }

            var student = _studentrepo.GetStudent(studentId);

            if (!_studentrepo.RemoveStudent(student))
            {
                ModelState.AddModelError("", $"Something went wrong when deleting student with id {studentId}");
                return StatusCode(404, ModelState);
            }

            return NoContent();
        }
    }
}



