using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DernSupport_BackEnd.Data;
using DernSupport_BackEnd.Models;
using DernSupport_BackEnd.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using DernSupport_BackEnd.Models.DTO;

namespace DernSupport_BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TechniciansController : ControllerBase
    {
        private readonly ITechnician _technician;
        public TechniciansController(ITechnician technician)
        {
            _technician = technician;
        }

        // GET: api/Technicians
        [Route("/technicians/GetAllTechnicians")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Technician>>> Gettechnicians()
        {
            return await _technician.GetAllTechnicians();
        }

        // GET: api/Technicians/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Technician>> GetTechnician(int id)
        {

            return await _technician.GetTechnicianById(id);
        }

        // PUT: api/Technicians/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTechnician(int id, Technician technician)
        {
            var updateTechnician = await _technician.UpdateTechnician(id, technician);
            return Ok(updateTechnician);
        }

        // POST: api/Technicians
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Technician>> PostTechnician(Technician technician)
        {
            var newTechnician = await _technician.CreateTechnician(technician);
            return Ok(newTechnician);
        }

        // DELETE: api/Technicians/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTechnician(int id)
        {
            var deletedTechnician = _technician.DeleteTechnician(id);
            return Ok(deletedTechnician);
        }

        [HttpGet("{id}/allProjects")]
        public async Task<ActionResult<List<Project>>> GetProjectsForTechnician(int id)
        {
            var projects = await _technician.GetProjectsForTechnician(id);
            return Ok(projects);
        }


        [Authorize(Roles = "User")]
        [HttpPost("SubmitRequest")]
        public async Task<TechnicianTask> SubmitARequest(TechnicianDTO technicianDTO)
        {
            var tec = new TechnicianDTO()
            {
                TechnicianTaskId = technicianDTO.TechnicianTaskId,
                TechnicianId = 1,
                userId = technicianDTO.userId,
                Title = technicianDTO.Title,
                Description = technicianDTO.Description,
                Status = technicianDTO.Status
            };
            var newTask = await _technician.SubmitARequest(tec);
            return newTask;
        }


        [Authorize(Roles = "Admin")]
        [HttpGet("GetTechniciansRequests")]
        public async Task<ActionResult<TechnicianTasksResult>> GetAllTasks()
        {
            var tasksResult = await _technician.GetAllSubmittedTasks();
            return Ok(tasksResult);
        }


        [Authorize(Roles = "User")]
        [HttpGet("GetTechniciansRequestsByUser")]
        public async Task<ActionResult<List<TechnicianTask>>> GetAllSubmittedTasksByUser(string UserId)
        {
            var tasks = await _technician.GetAllSubmittedTasksByUser(UserId);
            return Ok(tasks);
        }


        [Authorize(Roles = "Admin")]
        [HttpPut("UpdateRequestStatus/{taskId}")]
        public async Task<IActionResult> UpdateRequestStatus(int taskId, [FromBody] string status)
        {
            var updatedTask = await _technician.UpdateRequestStatus(taskId, status);
            return Ok(new { message = "Request status approved", task = updatedTask });

        }

    }
}
