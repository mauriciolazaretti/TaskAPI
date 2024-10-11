using TaskAPI.DataAccess.Entity;
using TaskAPI.Services.Interfaces.Services;
using TaskAPI.Services.Interfaces.UnitOfWork;
using TaskAPI.Services.Models;

namespace TaskAPI.Services.Implementations.Services
{
    public class ProjectService(IProjectUoW projectUow, ITaskUoW taskUow) : IProjectService
    {
        public async Task<ServiceResponse<bool>> AddProject(ProjectEntity project)
        {
            var result = await projectUow.AddProject(project);
            return new ServiceResponse<bool>(string.Empty, result);
        }


        public async Task<ServiceResponse<bool>> DeleteProject(int id)
        {
            var lista = await taskUow.GetTasks(id);
            if (lista.Any(o => o.Status == DataAccess.Enum.StatusEnum.Pendente))
            {
                return new ServiceResponse<bool>("você deve concluir as tarefas pendentes antes de excluir o projeto.", false);

            }
            return new ServiceResponse<bool>("Excluído com sucesso", await projectUow.DeleteProject(id));
        }

        public Task<ProjectEntity?> Find(int id) => projectUow.Find(id);

        public Task<List<ProjectEntity>> GetProjects() => projectUow.GetProjects();

        public async Task<ServiceResponse<bool>> UpdateProject(ProjectEntity project)
        {
            var result = await projectUow.UpdateProject(project);
            return new ServiceResponse<bool>(result ? "Atualizado com sucesso" : "Erro ao atualizar tarefa", result);
        }
    }
}
