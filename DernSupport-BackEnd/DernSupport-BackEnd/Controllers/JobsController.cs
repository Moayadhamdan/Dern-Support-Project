
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DernSupport_BackEnd.Data;
using DernSupport_BackEnd.Models;

namespace DernSupport_BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobsController : ControllerBase
    {
        private readonly DernSupportDbContext _context;

        public JobsController(DernSupportDbContext context)
        {
            _context = context;
        }

        [HttpGet("getAllJobs")]
        public async Task<IActionResult> GetAllJobs()
        {
            var jobs = await _context.JobsDb.ToListAsync();
            return Ok(jobs);
        }

        [HttpPost("AddJob")]
        public async Task<IActionResult> AddJob([FromBody] Jobs newJob)
        {
            _context.JobsDb.Add(newJob);
            await _context.SaveChangesAsync();
            return Ok(newJob);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> EditJob(int id, [FromBody] Jobs updatedJob)
        {
            var job = await _context.JobsDb.FindAsync(id);
            if (job == null)
            {
                return NotFound("Job not found.");
            }

            job.Title = updatedJob.Title;
            job.Description = updatedJob.Description;
            job.ScheduledDate = updatedJob.ScheduledDate;
            job.Priority = updatedJob.Priority;
            job.Cost = updatedJob.Cost;


            await _context.SaveChangesAsync();
            return Ok(job);
        }
    }
}
