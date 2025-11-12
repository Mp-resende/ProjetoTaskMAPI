using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaskManagerAPI.Models.Dto
{
    public class taskUpdateDto
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public DateOnly? DueDate { get; set; }
        public string? AssignedTo { get; set; }
        public TaskStatus? Status { get; set; }
    }
}