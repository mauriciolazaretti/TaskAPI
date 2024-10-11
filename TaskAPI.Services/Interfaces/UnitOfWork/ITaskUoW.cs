using TaskAPI.DataAccess.Entity;
using TaskAPI.Services.Models;
using TaskAPI.Services.Models.DTOs;

namespace TaskAPI.Services.Interfaces.UnitOfWork
{
    public interface ITaskUoW
    {
        Task<List<TaskEntity>?> GetTasks(int idProject);
        Task<bool> AddTask(TaskEntity task);
        Task<bool> UpdateTask(TaskEntity newTask);
        Task<bool> DeleteTask(int id);
        Task<TaskEntity?> Find(int id);
        Task<bool> CreateHistory(List<TaskHistoryEntity>? listHistory);
        Task<ServiceResponse<List<ReportResponse>>?> GetReports();
    }
}
