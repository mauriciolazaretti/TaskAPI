using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskAPI.DataAccess.Entity
{
    public class ProjectEntity
    {
        public ProjectEntity() { }
        [Key]
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public virtual List<TaskEntity> Tasks { get; set; } = new List<TaskEntity>();
    }
}
