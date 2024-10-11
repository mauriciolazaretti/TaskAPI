using TaskAPI.DataAccess.Entity;

namespace TaskAPI.Services.Interfaces.UnitOfWork
{
    public interface IProjectUoW
    {
        Task<List<ProjectEntity>> GetProjects();
        Task<bool> AddProject(ProjectEntity project);
        Task<bool> UpdateProject(ProjectEntity project);
        Task<bool> DeleteProject(int id);
        Task<ProjectEntity?> Find(int id);
    }
}
