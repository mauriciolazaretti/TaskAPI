using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using TaskAPI.DataAccess.Entity;
using TaskAPI.Services.Interfaces.Services;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Model;

namespace TaskAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController(ITaskService service, IValidator<TaskEntity> validator) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAsync([FromQuery]int project)
        {
            return Ok(await service.GetTasks(project));
        }
        [HttpPost]
        public async Task<IActionResult> Save([FromBody]TaskEntity taskEntity)
        {
            var validation = await validator.ValidateAsync(taskEntity);
            if (!validation.IsValid)
            {
                return BadRequest( validation.ToDictionary());
            }
            var r = await service.AddTask(taskEntity);
            return r.Value ? Ok() : BadRequest(r);
        }
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] TaskEntity taskEntity)
        {
            var validation = await validator.ValidateAsync(taskEntity);
            if (!validation.IsValid)
            {
                return BadRequest(validation.ToDictionary());
            }
            var r = await service.UpdateTask(taskEntity);
            return r.Value ? Ok() : BadRequest(r);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var r = await service.DeleteTask(id);
            return r.Value ? Ok() : BadRequest(r);
        }
        [HttpGet("report")]
        public async Task<IActionResult> GetReports([FromQuery] string userType)
        {
            var r = await service.GetReports(userType);
            return r.Message == string.Empty ? Ok(r.Value) : BadRequest(r);
        }
    }
}
