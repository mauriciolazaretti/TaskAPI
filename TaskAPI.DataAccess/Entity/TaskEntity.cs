using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TaskAPI.DataAccess.Enum;

namespace TaskAPI.DataAccess.Entity
{
    public class TaskEntity
    {
        public TaskEntity() { }
        public TaskEntity(int id, string title, string description, DateTime expireDate, StatusEnum status, PriorityEnum priority, int projectId, ProjectEntity? project)
        {
            Id = id;
            Title = title;
            Description = description;
            ExpireDate = expireDate;
            Status = status;
            Priority = priority;
            ProjectId = projectId;
            Project = project;
        }

        [Key]
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        [DataType(DataType.DateTime)]
        public DateTime ExpireDate { get; set; }
        public StatusEnum Status { get; set; }
        public PriorityEnum Priority { get; set; }
        [ForeignKey("Project")]
        public int ProjectId { get; set; }
        [NotMapped]
        public string User { get; set; } = string.Empty;
        [NotMapped]
        public string Commentary { get; set; } = string.Empty;
        public virtual ProjectEntity? Project { get; set; }
    }
}
