using Microsoft.EntityFrameworkCore;
using Moq;
using System.Threading.Tasks;
using TaskAPI.DataAccess;
using TaskAPI.DataAccess.Entity;
using TaskAPI.DataAccess.Enum;
using TaskAPI.Services.Implementations.Services;
using TaskAPI.Services.Implementations.UnitOfWork;
using TaskAPI.Services.Interfaces.UnitOfWork;
using TaskAPI.Services.Models;
using TaskAPI.Services.Models.DTOs;

namespace TaskAPI.Tests.Services
{
    public class TaskServiceTests
    {
        private readonly Mock<ITaskUoW> _mockUow;
        public TaskServiceTests()
        {
            _mockUow = new Mock<ITaskUoW>();
        }
        [Fact]
        public async Task DeleteTask_Success()
        {
            //arrange
            var task = new TaskEntity(1, "", "", DateTime.Now, StatusEnum.EmAndamento, PriorityEnum.Alta, 1, new ProjectEntity());

            _mockUow.Setup(x => x.DeleteTask(It.IsAny<int>()))
                .ReturnsAsync(true);
            //act
            var service = new TaskService(_mockUow.Object);
            var result = await service.DeleteTask(1);
            //assert
            Assert.NotNull(result);
            Assert.True(result.Value);
            Assert.Empty(result.Message);
        }
        [Fact]
        public async Task DeleteTask_Fail()
        {
            //arrange
            var task = new TaskEntity(1, "", "", DateTime.Now, StatusEnum.EmAndamento, PriorityEnum.Alta, 1, new ProjectEntity());
            _mockUow.Setup(x => x.DeleteTask(It.IsAny<int>()))
                .ReturnsAsync(false);
            var service = new TaskService(_mockUow.Object);
            //act
            var result = await service.DeleteTask(1);
            //assert
            Assert.NotNull(result);
            Assert.False(result.Value);
            Assert.Empty(result.Message);

        }
        [Fact]
        public async Task GetTasks_Success()
        {
            //arrange
            var tasks = new List<TaskEntity>()
            {
                new(1,"title","descricao",DateTime.Now,StatusEnum.Concluida, PriorityEnum.Baixa,1, new ProjectEntity()),
                new(2,"title","descricao",DateTime.Now,StatusEnum.Concluida, PriorityEnum.Baixa,1, new ProjectEntity())
            };
            _mockUow.Setup(x => x.GetTasks(It.IsAny<int>()))
                .ReturnsAsync(tasks);

            //act
            var service = new TaskService(_mockUow.Object);
            var result = await service.GetTasks(1);


            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
            Assert.Equal(1, result[0].Id);
            Assert.Equal("title", result[0].Title);
            Assert.Equal("descricao", result[0].Description);
            Assert.Equal(StatusEnum.Concluida, result[0].Status);
            Assert.Equal(PriorityEnum.Baixa, result[0].Priority);
            Assert.NotNull(result[0].Project);

        }
        [Fact]
        public async Task GetTasks_Fail_Empty()
        {
            //arrange
            List<TaskEntity> tasks = new List<TaskEntity>();
            _mockUow.Setup(x => x.GetTasks(It.IsAny<int>()))
                .ReturnsAsync(tasks);

            //act
            var service = new TaskService(_mockUow.Object);
            var result = await service.GetTasks(1);

            //assert
            Assert.NotNull(result);
            Assert.Empty(result);

        }
        [Fact]
        public async Task GetTasks_Fail_Null()
        {
            //arrange
            List<TaskEntity>? tasks = null;
            _mockUow.Setup(x => x.GetTasks(It.IsAny<int>()))
                .ReturnsAsync(tasks);

            //act
            var service = new TaskService(_mockUow.Object);
            var result = await service.GetTasks(1);

            //assert
            Assert.Null(result);

        }
        [Fact]
        public async Task AddTask_Success()
        {
            //arrange
            var tasks = new List<TaskEntity>()
            {
                new(1,"title","descricao",DateTime.Now,StatusEnum.Concluida, PriorityEnum.Baixa,1, new ProjectEntity()),
                new(2,"title","descricao",DateTime.Now,StatusEnum.Concluida, PriorityEnum.Baixa,1, new ProjectEntity()),
                new(2,"title","descricao",DateTime.Now,StatusEnum.Concluida, PriorityEnum.Baixa,1, new ProjectEntity()),
                new(2,"title","descricao",DateTime.Now,StatusEnum.Concluida, PriorityEnum.Baixa,1, new ProjectEntity()),
                new(2,"title","descricao",DateTime.Now,StatusEnum.Concluida, PriorityEnum.Baixa,1, new ProjectEntity()),
                new(2,"title","descricao",DateTime.Now,StatusEnum.Concluida, PriorityEnum.Baixa,1, new ProjectEntity()),
                new(2,"title","descricao",DateTime.Now,StatusEnum.Concluida, PriorityEnum.Baixa,1, new ProjectEntity()),
                new(2,"title","descricao",DateTime.Now,StatusEnum.Concluida, PriorityEnum.Baixa,1, new ProjectEntity()),
                new(2,"title","descricao",DateTime.Now,StatusEnum.Concluida, PriorityEnum.Baixa,1, new ProjectEntity()),
                new(2,"title","descricao",DateTime.Now,StatusEnum.Concluida, PriorityEnum.Baixa,1, new ProjectEntity()),
                new(2,"title","descricao",DateTime.Now,StatusEnum.Concluida, PriorityEnum.Baixa,1, new ProjectEntity()),
                new(2,"title","descricao",DateTime.Now,StatusEnum.Concluida, PriorityEnum.Baixa,1, new ProjectEntity())
            };
            var entity = new TaskEntity(1, "title", "descricao", DateTime.Now, StatusEnum.Concluida, PriorityEnum.Baixa, 1, new ProjectEntity());
            _mockUow.Setup(x => x.AddTask(It.IsAny<TaskEntity>()))
                .ReturnsAsync(true);
            _mockUow.Setup(x => x.GetTasks(It.IsAny<int>()))
                .ReturnsAsync(tasks);
            //act
            var service = new TaskService(_mockUow.Object);
            var result = await service.AddTask(entity);


            Assert.NotNull(result);
            Assert.True(result.Value);
            Assert.Empty(result.Message);
        }
        [Fact]
        public async Task UpdateTask_Success()
        {
            //arrange
            var task = new TaskEntity(1, "aaa", "", DateTime.Now, StatusEnum.EmAndamento, PriorityEnum.Alta, 1, new ProjectEntity());
            var task2 = new TaskEntity(1, "bbbb", "aaa", DateTime.Now, StatusEnum.Pendente, PriorityEnum.Alta, 1, new ProjectEntity());
            _mockUow.Setup(x => x.UpdateTask(It.IsAny<TaskEntity>()))
                .ReturnsAsync(true);
            _mockUow.Setup(x => x.Find(It.IsAny<int>()))
                .ReturnsAsync(task2);
            task.Title = string.Empty;
            _mockUow.Setup(x => x.CreateHistory(It.IsAny<List<TaskHistoryEntity>>())).ReturnsAsync(true);
            //act
            var service = new TaskService(_mockUow.Object);
            var b = await service.UpdateTask(task);
            //assert
            Assert.NotNull(b);
            Assert.True(b.Value);
            Assert.Empty(b.Message);
        }
        [Fact]
        public async Task UpdateTask_Fail()
        {
            //arrange
            var task = new TaskEntity(1, "", "", DateTime.Now, StatusEnum.EmAndamento, PriorityEnum.Alta, 1, new ProjectEntity());
            _mockUow.Setup(x => x.UpdateTask(It.IsAny<TaskEntity>()))
                .ReturnsAsync(false);
            _mockUow.Setup(x => x.Find(It.IsAny<int>()))
                .ReturnsAsync((TaskEntity?)null);
            _mockUow.Setup(x => x.CreateHistory(It.IsAny<List<TaskHistoryEntity>>())).ReturnsAsync(true);
            //act
            var service = new TaskService(_mockUow.Object);
            var b = await service.UpdateTask(task);
            //assert
            Assert.NotNull(b);
            Assert.False(b.Value);
            Assert.NotEmpty(b.Message);
        }
        [Fact]
        public async Task AddTask_Fail()
        {
            //arrange
            var tasks = new List<TaskEntity>()
            {
                new(1,"title","descricao",DateTime.Now,StatusEnum.Concluida, PriorityEnum.Baixa,1, new ProjectEntity()),
                new(2,"title","descricao",DateTime.Now,StatusEnum.Concluida, PriorityEnum.Baixa,1, new ProjectEntity()),
                new(2,"title","descricao",DateTime.Now,StatusEnum.Concluida, PriorityEnum.Baixa,1, new ProjectEntity()),
                new(2,"title","descricao",DateTime.Now,StatusEnum.Concluida, PriorityEnum.Baixa,1, new ProjectEntity()),
                new(2,"title","descricao",DateTime.Now,StatusEnum.Concluida, PriorityEnum.Baixa,1, new ProjectEntity()),
                new(2,"title","descricao",DateTime.Now,StatusEnum.Concluida, PriorityEnum.Baixa,1, new ProjectEntity()),
                new(2,"title","descricao",DateTime.Now,StatusEnum.Concluida, PriorityEnum.Baixa,1, new ProjectEntity()),
                new(2,"title","descricao",DateTime.Now,StatusEnum.Concluida, PriorityEnum.Baixa,1, new ProjectEntity()),
                new(2,"title","descricao",DateTime.Now,StatusEnum.Concluida, PriorityEnum.Baixa,1, new ProjectEntity()),
                new(2,"title","descricao",DateTime.Now,StatusEnum.Concluida, PriorityEnum.Baixa,1, new ProjectEntity()),
                new(2,"title","descricao",DateTime.Now,StatusEnum.Concluida, PriorityEnum.Baixa,1, new ProjectEntity()),
                new(1,"title","descricao",DateTime.Now,StatusEnum.Concluida, PriorityEnum.Baixa,1, new ProjectEntity()),
                new(2,"title","descricao",DateTime.Now,StatusEnum.Concluida, PriorityEnum.Baixa,1, new ProjectEntity()),
                new(2,"title","descricao",DateTime.Now,StatusEnum.Concluida, PriorityEnum.Baixa,1, new ProjectEntity()),
                new(2,"title","descricao",DateTime.Now,StatusEnum.Concluida, PriorityEnum.Baixa,1, new ProjectEntity()),
                new(2,"title","descricao",DateTime.Now,StatusEnum.Concluida, PriorityEnum.Baixa,1, new ProjectEntity()),
                new(2,"title","descricao",DateTime.Now,StatusEnum.Concluida, PriorityEnum.Baixa,1, new ProjectEntity()),
                new(2,"title","descricao",DateTime.Now,StatusEnum.Concluida, PriorityEnum.Baixa,1, new ProjectEntity()),
                new(2,"title","descricao",DateTime.Now,StatusEnum.Concluida, PriorityEnum.Baixa,1, new ProjectEntity()),
                new(2,"title","descricao",DateTime.Now,StatusEnum.Concluida, PriorityEnum.Baixa,1, new ProjectEntity()),
                new(2,"title","descricao",DateTime.Now,StatusEnum.Concluida, PriorityEnum.Baixa,1, new ProjectEntity())
            };
            var entity = new TaskEntity(1, "title", "descricao", DateTime.Now, StatusEnum.Concluida, PriorityEnum.Baixa, 1, new ProjectEntity());
            _mockUow.Setup(x => x.AddTask(It.IsAny<TaskEntity>()))
                .ReturnsAsync(true);
            _mockUow.Setup(x => x.GetTasks(It.IsAny<int>()))
                .ReturnsAsync(tasks);
            //act
            var service = new TaskService(_mockUow.Object);
            var result = await service.AddTask(entity);

            Assert.NotNull(result);
            Assert.False(result.Value);
            Assert.NotEmpty(result.Message);

        }
        [Fact]
        public async Task GetReports_AsGerente_ShouldReturnReports()
        {
            // Arrange
            var reports = new List<ReportResponse> { new ReportResponse("teste",10.00m) };
            var response = new ServiceResponse<List<ReportResponse>>(string.Empty, reports);
            _mockUow.Setup(u => u.GetReports()).ReturnsAsync(response);

            // Act
            var service = new TaskService(_mockUow.Object);
            var responseService = await service.GetReports("gerente");

            // Assert
            Assert.NotNull(responseService);
            Assert.NotEmpty(responseService.Value);
            Assert.NotEmpty(responseService.Value);
        }

        [Fact]
        public async Task GetReports_AsNonGerente_ShouldReturnUnauthorized()
        {
            // Arrange
            var userType = "normal_user";
            // Act
            var service = new TaskService(_mockUow.Object);
            var responseService = await service.GetReports(userType);

            // Assert
            Assert.NotNull(responseService);
            Assert.Empty(responseService.Value);
            Assert.Equal("Usuário não autorizado", responseService.Message);
        }

        [Fact]
        public async Task GetReports_WithEmptyUserType_ShouldReturnUnauthorized()
        {
            // Arrange
            string userType = string.Empty;

            var service = new TaskService(_mockUow.Object);
            var responseService = await service.GetReports(userType);

            // Assert
            Assert.NotNull(responseService);
            Assert.Empty(responseService.Value);
            Assert.Equal("Usuário não autorizado", responseService.Message);
        }
    }
}
