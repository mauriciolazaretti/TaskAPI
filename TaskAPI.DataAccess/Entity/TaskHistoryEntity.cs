using System.ComponentModel.DataAnnotations;

namespace TaskAPI.DataAccess.Entity
{
    public class TaskHistoryEntity
    {
        [Key]
        public int Id { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime ModificationDate { get; set; }
        public string FieldModified { get; set; } = string.Empty;
        public string FieldTo { get; set; } = string.Empty;
        public string FieldFrom { get; set; } = string.Empty;
        public string User { get; set; } = string.Empty;
        public TaskEntity? TaskEntity { get; set; }
    }
}