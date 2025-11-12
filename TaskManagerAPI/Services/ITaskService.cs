using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskManagerAPI.Models.Dto;

namespace TaskManagerAPI.Services
{
    public interface ITaskService
    {
        Task<List<TaskReadDto>> GetAllTasksAsync(int pageNumber = 1, int pageSize = 10, Models.TaskStatus? status = null, string? assignedTo = null, DateOnly? dueBefore = null);
        Task<TaskReadDto> CreateTaskAsync(TaskCreateDto taskCreateDto);
        Task<TaskReadDto?> GetTaskByIdAsync(int id);
        Task<TaskReadDto?> UpdateTaskAsync(int id, taskUpdateDto taskUpdateDto);
        Task<bool> DeleteTaskAsync(int id);
    }
}