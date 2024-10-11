using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskAPI.DataAccess.Entity;
using TaskAPI.Services.Interfaces.Services;

namespace TaskAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController(IProjectService projectService) : ControllerBase
    {
        [HttpGet("list")]
        public async Task<IActionResult> List()
        {
            var result = await projectService.GetProjects();
            return Ok(result);
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ProjectEntity entity)
        {
            var r = await projectService.AddProject(entity);
            return r.Value ? Ok() : BadRequest(r);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var r = await projectService.DeleteProject(id);
            return r.Value ? Ok() : BadRequest(r);
        }
    }
}
