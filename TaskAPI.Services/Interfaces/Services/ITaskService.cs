using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskAPI.DataAccess.Entity;
using TaskAPI.Services.Models;
using TaskAPI.Services.Models.DTOs;

namespace TaskAPI.Services.Interfaces.Services
{
    public interface ITaskService
    {
        Task<List<TaskEntity>?> GetTasks(int idProject);
        Task<ServiceResponse<bool>> AddTask(TaskEntity task);
        Task<ServiceResponse<bool>> UpdateTask(TaskEntity task);
        Task<ServiceResponse<bool>> DeleteTask(int id);

        Task<ServiceResponse<List<ReportResponse>?>> GetReports(string userType);
    }
}
