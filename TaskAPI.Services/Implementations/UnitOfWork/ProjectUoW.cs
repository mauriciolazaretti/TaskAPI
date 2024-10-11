using Microsoft.EntityFrameworkCore;
using TaskAPI.DataAccess;
using TaskAPI.DataAccess.Entity;
using TaskAPI.Services.Interfaces.UnitOfWork;

namespace TaskAPI.Services.Implementations.UnitOfWork
{
    public class ProjectUoW(TaskApiContext context) : IProjectUoW
    {
        public async Task<bool> AddProject(ProjectEntity project)
        {
            await context.Projects.AddAsync(project);
            return await context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteProject(int id)
        {
            var tasks = await context.Tasks.Where(x => x.ProjectId == id).ToListAsync();
            var history = await context.TaskHistory.Where(x => x.TaskEntity != null &&
                tasks.Select(o => o.Id).Contains( x.TaskEntity.Id)).ToListAsync();
            context.TaskHistory.RemoveRange(history);
            await context.SaveChangesAsync();
            context.Tasks.RemoveRange(tasks);
            await context.SaveChangesAsync();
            var proj = await context.Projects.FindAsync(id);
            context.Projects.Remove(proj);
            return await context.SaveChangesAsync() > 0;
        }

        public async Task<ProjectEntity?> Find(int id) => await context.Projects.FindAsync(id);

        public async Task<List<ProjectEntity>> GetProjects() => await context.Projects.ToListAsync();

        public async Task<bool> UpdateProject(ProjectEntity project)
        {
            context.Projects.Update(project);
            return await context.SaveChangesAsync() > 0;
        }
    }
}
