using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskAPI.DataAccess.Entity;
using TaskAPI.Services.Implementations.Services;
using TaskAPI.Services.Interfaces.UnitOfWork;

namespace TaskAPI.Tests.Services
{
    public class ProjectServiceTests
    {
        private readonly Mock<IProjectUoW> _projectUowMock;
        private readonly Mock<ITaskUoW> _taskUowMock;

        public ProjectServiceTests()
        {
            _projectUowMock = new Mock<IProjectUoW>();
            _taskUowMock = new Mock<ITaskUoW>();
        }

        [Fact]
        public async Task AddProject_Success()
        {
            // Arrange
            var project = new ProjectEntity { Name = "Test Project" };
            _projectUowMock.Setup(p => p.AddProject(project)).ReturnsAsync(true);

            var projectService = new ProjectService(_projectUowMock.Object, _taskUowMock.Object);

            // Act
            var response = await projectService.AddProject(project);

            // Assert
            Assert.True(response.Value);
            Assert.Equal(string.Empty, response.Message);
        }
        [Fact]
        public async Task DeleteProject_Success()
        {
            // Arrange
            var projectId = 1;
            _taskUowMock.Setup(t => t.GetTasks(projectId)).ReturnsAsync(new List<TaskEntity>());
            _projectUowMock.Setup(p => p.DeleteProject(projectId)).ReturnsAsync(true);

            var projectService = new ProjectService(_projectUowMock.Object, _taskUowMock.Object);

            // Act
            var response = await projectService.DeleteProject(projectId);

            // Assert
            Assert.True(response.Value);
            Assert.Equal("Excluído com sucesso", response.Message);
        }

        [Fact]
        public async Task DeleteProject_Fail()
        {
            // Arrange
            var projectId = 1;
            var pendingTask = new TaskEntity { Status = DataAccess.Enum.StatusEnum.Pendente };
            _taskUowMock.Setup(t => t.GetTasks(projectId)).ReturnsAsync(new List<TaskEntity> { pendingTask });

            var projectService = new ProjectService(_projectUowMock.Object, _taskUowMock.Object);

            // Act
            var response = await projectService.DeleteProject(projectId);

            // Assert
            Assert.False(response.Value);
            Assert.Equal("você deve concluir as tarefas pendentes antes de excluir o projeto.", response.Message);
        }

        [Fact]
        public async Task FindProject_Success()
        {
            // Arrange
            var projectId = 1;
            var project = new ProjectEntity { Id = projectId };
            _projectUowMock.Setup(p => p.Find(projectId)).ReturnsAsync(project);

            var projectService = new ProjectService(_projectUowMock.Object, _taskUowMock.Object);

            // Act
            var foundProject = await projectService.Find(projectId);

            // Assert
            Assert.NotNull(foundProject);
            Assert.Equal(projectId, foundProject.Id);
        }

        [Fact]
        public async Task GetProjects_Success()
        {
            // Arrange
            var projects = new List<ProjectEntity> { new ProjectEntity(), new ProjectEntity() };
            _projectUowMock.Setup(p => p.GetProjects()).ReturnsAsync(projects);

            var projectService = new ProjectService(_projectUowMock.Object, _taskUowMock.Object);

            // Act
            var retrievedProjects = await projectService.GetProjects();

            // Assert
            Assert.NotNull(retrievedProjects);
            Assert.Equal(2, retrievedProjects.Count);
        }

        [Fact]
        public async Task UpdateProject_Success()
        {
            // Arrange
            var project = new ProjectEntity { Id = 1, Name = "Updated Project" };
            _projectUowMock.Setup(p => p.UpdateProject(project)).ReturnsAsync(true);

            var projectService = new ProjectService(_projectUowMock.Object, _taskUowMock.Object);
            //act
            var update = await projectService.UpdateProject(project);
            //assert
            Assert.NotNull(update);
            Assert.True(update.Value);
            Assert.Equal("Atualizado com sucesso", update.Message);
        }

        [Fact]
        public async Task UpdateProject_Fail()
        {
            // Arrange
            var project = new ProjectEntity { Id = 0, Name = "Updated Project" };
            _projectUowMock.Setup(p => p.UpdateProject(project)).ReturnsAsync(false);

            var projectService = new ProjectService(_projectUowMock.Object, _taskUowMock.Object);
            //act
            var update = await projectService.UpdateProject(project);
            //assert
            Assert.NotNull(update);
            Assert.False(update.Value);
            Assert.Equal("Erro ao atualizar tarefa", update.Message);
        }
    }
}
