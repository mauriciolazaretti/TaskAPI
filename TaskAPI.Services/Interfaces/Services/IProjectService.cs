using TaskAPI.DataAccess.Entity;
using TaskAPI.Services.Models;

namespace TaskAPI.Services.Interfaces.Services
{
    public interface IProjectService
    {
        Task<List<ProjectEntity>> GetProjects();
        Task<ServiceResponse<bool>> AddProject(ProjectEntity project);
        Task<ServiceResponse<bool>> UpdateProject(ProjectEntity project);
        Task<ServiceResponse<bool>> DeleteProject(int id);
        Task<ProjectEntity?> Find(int id);
    }
}
