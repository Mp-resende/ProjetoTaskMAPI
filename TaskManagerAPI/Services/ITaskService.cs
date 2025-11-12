using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskManagerAPI.Models.Dto;

namespace TaskManagerAPI.Services
{
    public interface ITaskService
    {
        Task<List<Models.TaskEntity>> GetAllTasksAsync();
        Task<TaskReadDto> CreateTaskAsync(TaskCreateDto taskCreateDto);
        Task<TaskReadDto?> GetTaskByIdAsync(int id);
        Task<TaskReadDto?> UpdateTaskAsync(int id, taskUpdateDto taskUpdateDto);
        Task<bool> DeleteTaskAsync(int id);
    }
}