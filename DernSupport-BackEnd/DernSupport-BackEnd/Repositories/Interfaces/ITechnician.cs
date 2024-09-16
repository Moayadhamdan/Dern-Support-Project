using DernSupport_BackEnd.Models;
using DernSupport_BackEnd.Models.DTO;
using Microsoft.AspNetCore.Mvc;

namespace DernSupport_BackEnd.Repositories.Interfaces
{
    public interface ITechnician
    {
        Task<Technician> CreateTechnician(Technician technician);
        Task<List<Technician>> GetAllTechnicians();
        Task<Technician> GetTechnicianById(int technicianId);

        Task<Technician> UpdateTechnician(int id, Technician technician);

        Task DeleteTechnician(int id);

        Task<List<Project>> GetProjectsForTechnician(int technicianId);

        Task<TechnicianTask> SubmitARequest(TechnicianDTO technicianDTO);

        Task<TechnicianTasksResult> GetAllSubmittedTasks();
        Task<List<TechnicianTask>> GetAllSubmittedTasksByUser(string UserId);


        Task<TechnicianTask> UpdateRequestStatus(int taskId, string status);
    }
}
