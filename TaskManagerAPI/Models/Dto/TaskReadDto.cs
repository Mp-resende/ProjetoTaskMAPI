using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaskManagerAPI.Models.Dto
{
    public class TaskReadDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public DateOnly? DueDate { get; set; }
        public DateOnly CreatedAt { get; set; }
        public string? AssignedTo { get; set; }
        public TaskStatus Status { get; set; } = TaskStatus.Pending;
    }
}