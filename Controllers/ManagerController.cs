using InternAPI.Models;
using InternAPI.Repository.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InternAPI.Controllers
{
    [Route("api/manager")]
    [ApiController]
    public class ManagerController : ControllerBase
    {
        private readonly IManagerRepository _managerRepo;

        public ManagerController(IManagerRepository managerRepo)
        {
            _managerRepo = managerRepo;
        }

        /// <summary>
        /// Get all managers
        /// </summary>
        /// <returns>Manager List</returns>
        [HttpGet]
        public IActionResult GetManagers()
        {
            var managerList = _managerRepo.GetManagers();
            return Ok(managerList);
        }

        /// <summary>
        /// Find manager by id
        /// </summary>
        /// <param name="managerId">Manager ID</param>
        /// <returns>Manager</returns>
        [HttpGet("byId/{managerId:int}")]
        public IActionResult GetManager(int managerId)
        {
            var manager = _managerRepo.GetManager(managerId);
            if (manager is null)
                return StatusCode(404);
            return Ok(manager);
        }

        /// <summary>
        /// Find manager by mail address
        /// </summary>
        /// <param name="mail">Manager mail</param>
        /// <returns>Manager</returns>
        [HttpGet("byMail/{mail}")]
        public IActionResult GetManager(string mail)
        {
            var manager = _managerRepo.GetManager(mail);
            if (manager is null)
                return StatusCode(404);
            return Ok(manager);
        }

        /// <summary>
        /// Create a new manager
        /// </summary>
        /// <param name="manager">Manager Object</param>
        /// <returns>Manager</returns>
        [HttpPost]
        public IActionResult CreateManager([FromBody] Manager manager)
        {
            if (manager is null)
                return BadRequest(ModelState);

            if (!_managerRepo.ManagerExists(manager.Mail))
            {
                ModelState.AddModelError("", $"A manager already exists with mail {manager.Mail}");
                return StatusCode(404, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_managerRepo.CreateManager(manager))
            {
                ModelState.AddModelError("", $"Something wrong happened when adding manager {manager.Mail}");
            }
            return Ok(manager);
        }

        /// <summary>
        /// Update a manager
        /// </summary>
        /// <param name="manager">Manager Object</param>
        /// <returns></returns>
        [HttpPatch]
        public IActionResult UpdateManager([FromBody] Manager manager)
        {
            if (manager is null)
                return BadRequest(ModelState);

            if (!_managerRepo.ManagerExists(manager.Id))
            {
                ModelState.AddModelError("", $"Manager doesnt exist with id {manager.Id}");
                return StatusCode(404, ModelState);
            }

            if (!_managerRepo.UpdateManager(manager))
            {
                ModelState.AddModelError("", $"Something went wrong when updating manager with id {manager.Id}");
                return StatusCode(404, ModelState);
            }
            return NoContent();

        }
        /// <summary>
        /// Delete manager by manager ID
        /// </summary>
        /// <param name="managerId">Manager ID</param>
        /// <returns></returns>
        [HttpDelete("{managerId:int}")]
        public IActionResult RemoveManager(int managerId)
        {
            if (!_managerRepo.ManagerExists(managerId))
            {
                ModelState.AddModelError("", $"Manager with id of {managerId} doesn't exist");
            }

            var manager = _managerRepo.GetManager(managerId);

            if (!_managerRepo.RemoveManager(manager))
            {
                ModelState.AddModelError("", $"Something went wrong when deleting manager with id {managerId}");
                return StatusCode(404, ModelState);
            }

            return NoContent();
        }
    }
}
