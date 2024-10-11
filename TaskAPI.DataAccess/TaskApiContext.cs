using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using TaskAPI.DataAccess.Entity;

namespace TaskAPI.DataAccess;

public class TaskApiContext : DbContext
{
    public TaskApiContext() { }
    public TaskApiContext(DbContextOptions<TaskApiContext> options) : base(options) {
        this.Database.EnsureCreated();
    }
    public virtual DbSet<TaskEntity> Tasks { get; set; }

    public virtual DbSet<ProjectEntity> Projects { get; set; }

    public virtual DbSet<TaskHistoryEntity> TaskHistory { get; set; }
}