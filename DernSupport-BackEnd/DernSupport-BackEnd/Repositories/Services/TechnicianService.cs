using DernSupport_BackEnd.Data;
using DernSupport_BackEnd.Models;
using DernSupport_BackEnd.Models.DTO;
using DernSupport_BackEnd.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DernSupport_BackEnd.Repositories.Services
{
    public class TechnicianService : ITechnician
    {
        private UserManager<ApplicationUser> _userManager;
        private readonly DernSupportDbContext _context;

        public TechnicianService(DernSupportDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public async Task<Technician> CreateTechnician(Technician technician)
        {
            _context.technician.Add(technician);
            await _context.SaveChangesAsync();

            return technician;
        }

        public async Task DeleteTechnician(int id)
        {
            var getTechnician = await GetTechnicianById(id);
            _context.Entry(getTechnician).State = EntityState.Deleted;
            await _context.SaveChangesAsync();

        }

        public async Task<List<Technician>> GetAllTechnicians()
        {
            var allTechnicians = await _context.technician.ToListAsync();
            return allTechnicians;
        }

        public async Task<Technician> GetTechnicianById(int technicianId)
        {
            var technician = await _context.technician.FindAsync(technicianId);
            return technician;
        }

        public async Task<Technician> UpdateTechnician(int id, Technician technician)
        {
            var exsitingtechnician = await _context.technician.FindAsync(id);
            exsitingtechnician = technician;
            await _context.SaveChangesAsync();

            return technician;
        }

        public async Task<List<Project>> GetProjectsForTechnician(int technicianId)
        {
            var projetcsForTechnician = await _context.TechnicianProjectsDBset
                .Where(ep => ep.TechnicianId == technicianId)
                .Select(ep => ep.Project)
                .ToListAsync();

            return projetcsForTechnician;

        }

        public async Task<TechnicianTask> SubmitARequest(TechnicianDTO technicianDTO)
        {
            var user = await _userManager.FindByIdAsync(technicianDTO.userId);
            var tec = new TechnicianTask()
            {
                TechnicianTaskId = technicianDTO.TechnicianTaskId,
                TechnicianId = 1,
                userId = technicianDTO.userId,
                UserName = user.UserName,
                Title = technicianDTO.Title,
                Description = technicianDTO.Description,
                Status = technicianDTO.Status
            };
            _context.TechnicianTasksDb.Add(tec);
            await _context.SaveChangesAsync();
            return tec;
        }

        //async Task<TechnicianTask> ITechnician.SubmitARequest(TechnicianTask technicianTask)
        //{
        //    _context.TechnicianTasksDb.Add(technicianTask);

        //    await _context.SaveChangesAsync();

        //    return technicianTask;
        //}

        public async Task<TechnicianTasksResult> GetAllSubmittedTasks()
        {
            // Fetch all technician tasks from the database
            var tasks = await _context.TechnicianTasksDb.ToListAsync();

            // List to store DTOs
            var technicianDtos = new List<TechnicianDTO>();

            // Fetch user details for each task
            foreach (var task in tasks)
            {
                var user = await _userManager.FindByIdAsync(task.userId);
                if (user != null)
                {
                    technicianDtos.Add(new TechnicianDTO
                    {
                        TechnicianTaskId = task.TechnicianTaskId,
                        TechnicianId = task.TechnicianId,
                        userId = task.userId,
                        Title = task.Title,
                        Description = task.Description,
                        Status = task.Status,
                        UserName = user.UserName // Map the UserName
                    });
                }
            }

            return new TechnicianTasksResult
            {
                Tasks = technicianDtos,
                UserNames = technicianDtos.Select(t => t.UserName).Distinct().ToList()
            };
        }






        public async Task<List<TechnicianTask>> GetAllSubmittedTasksByUser(string UserId)
        {
            return await _context.TechnicianTasksDb.Where(a => a.userId == UserId).ToListAsync();
        }


        public async Task<TechnicianTask> UpdateRequestStatus(int taskId, string status)
        {

            var task = await _context.TechnicianTasksDb.FindAsync(taskId);
            if (task == null)
            {
                throw new Exception("Task not found");
            }
            task.Status = status;
            await _context.SaveChangesAsync();

            return task;
        }

        
    }
}
