using Microsoft.EntityFrameworkCore;
using TaskAPI.DataAccess;
using TaskAPI.DataAccess.Entity;
using TaskAPI.DataAccess.Enum;
using TaskAPI.Services.Interfaces.UnitOfWork;
using TaskAPI.Services.Models;
using TaskAPI.Services.Models.DTOs;

namespace TaskAPI.Services.Implementations.UnitOfWork
{
    public class TaskUoW(TaskApiContext context) : ITaskUoW
    {
        public async Task<bool> AddTask(TaskEntity task)
        {
            await context.AddAsync(task);
            return (await context.SaveChangesAsync()) > 0;
        }

        public async Task<bool> CreateHistory(List<TaskHistoryEntity>? listHistory)
        {
            if (listHistory == null)
                return false;
            foreach (var entity in listHistory)
            {
                if(entity == null) continue;
                entity.TaskEntity = await this.Find(entity?.TaskEntity?.Id ?? 0);
            }
            await context.TaskHistory.AddRangeAsync(listHistory);
            return (await context.SaveChangesAsync()) > 0;
        }

        public async Task<bool> DeleteTask(int id)
        {
            var task = await context.Tasks.FindAsync(id);
            if (task is not null)
            {
                context.Tasks.Remove(task);
                return await context.SaveChangesAsync() > 0;
            }
            return false;
        }

        public async Task<TaskEntity?> Find(int id) => await context.Tasks.FindAsync(id);

        public async Task<ServiceResponse<List<ReportResponse>>?> GetReports()
        {
            var status = StatusEnum.Concluida.ToString();
            var grpBy = await context.TaskHistory.Where(x => x.FieldTo == status && x.FieldModified == "Status" && DateTime.UtcNow.AddDays(-30) <= x.ModificationDate )
                .GroupBy(x => x.User)
                .ToListAsync();
            var total = await context.Tasks.CountAsync();
            return new ServiceResponse<List<ReportResponse>>(string.Empty, grpBy.Select(x =>
            {
                var count = x.ToList().Count;
                return new ReportResponse(x.Key, (Convert.ToDecimal(count) / Convert.ToDecimal(total)));
            }).ToList());
            

        }

        public async Task<List<TaskEntity>?> GetTasks(int idProject)
        {
            var list = await context.Tasks.Where(o => o.ProjectId == idProject).ToListAsync();
            return list;
        }

        public async Task<bool> UpdateTask( TaskEntity newTask)
        {
            
            if (newTask is not null)
            {
                context.Tasks.Update(newTask);
                return await context.SaveChangesAsync() > 0;
            }
            return false;
        }

    }
}
