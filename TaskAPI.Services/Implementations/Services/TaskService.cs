using TaskAPI.DataAccess.Entity;
using TaskAPI.Services.Interfaces.Services;
using TaskAPI.Services.Interfaces.UnitOfWork;
using TaskAPI.Services.Models;
using TaskAPI.Services.Models.DTOs;

namespace TaskAPI.Services.Implementations.Services
{
    public class TaskService(ITaskUoW uow) : ITaskService
    {
        public async Task<ServiceResponse<bool>> AddTask(TaskEntity task)
        {
            var list = await uow.GetTasks(task.ProjectId);
            if (list.Count >= 20)
            {
                return new ServiceResponse<bool>("você possui 20 tasks já", false);
            }
            var b = await uow.AddTask(task);
            return new ServiceResponse<bool>(string.Empty,b);
        }

        public async Task<ServiceResponse<bool>> DeleteTask(int id)
        {
            var b = await uow.DeleteTask(id);
            return new ServiceResponse<bool>(string.Empty, b);
        }
        public async Task<List<TaskEntity>?> GetTasks(int idProject) => await uow.GetTasks(idProject);

        public async Task<ServiceResponse<bool>> UpdateTask(TaskEntity task)
        {
            var old = await uow.Find(task.Id);
            if (old is not null)
            {
                var listHistory = old.GetType().GetProperties().Select(x => {
                    var value = x.GetValue(old)?.ToString();
                    if(value is not null)
                    {
                        if(x.Name is not "Priority")
                        {
                            var valueNew = task.GetType().GetProperty(x.Name)?.GetValue(task)?.ToString();
                            if(valueNew is not null && valueNew != value)
                            {
                                var history = new TaskHistoryEntity()
                                {
                                    FieldFrom = value.ToString(),
                                    FieldModified = x.Name,
                                    ModificationDate = DateTime.UtcNow,
                                    FieldTo = valueNew?.ToString() ?? "",
                                    TaskEntity = task,
                                    User = task.User
                                };
                                return history;
                            }
                        }
                    }
                    return new TaskHistoryEntity();
                    }).Where(x => x.TaskEntity != null && x.TaskEntity.Id > 0)
                        .ToList();
                old.Status = task.Status;
                old.ExpireDate = task.ExpireDate;
                old.Title = task.Title;
                old.Description = task.Description;
                var b = await uow.UpdateTask(old);
                if(task.Commentary != string.Empty)
                {
                    listHistory.Add(new TaskHistoryEntity()
                    {
                        FieldFrom = string.Empty,
                        FieldModified = "Commentary",
                        ModificationDate = DateTime.UtcNow,
                        FieldTo = task.Commentary,
                        TaskEntity = task,
                        User = task.User
                    });
                }
                if (listHistory != null)
                {
                    await uow.CreateHistory(listHistory);
                }
                return new ServiceResponse<bool>(string.Empty, b);
            }
            return new ServiceResponse<bool>("Erro ao atualizar task", false);
        }

        public async Task<ServiceResponse<List<ReportResponse>>?> GetReports(string userType)
        {
            if(userType == "gerente")
            {
                return await uow.GetReports();
            }
            return new ServiceResponse<List<ReportResponse>>("Usuário não autorizado", []);
        }
    }
}
