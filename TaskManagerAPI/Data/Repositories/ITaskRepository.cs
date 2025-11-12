using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskManagerAPI.Models;

namespace TaskManagerAPI.Data.Repositories
{
    public interface ITaskRepository
    {
        Task<List<Models.TaskEntity>> GetAllTasksAsync(int pageNumber, int pageSize, Models.TaskStatus? status, string? assignedTo, DateOnly? dueBefore);
        Task<Models.TaskEntity> CreateTaskAsync(TaskEntity task);
        Task<Models.TaskEntity?> GetTaskByIdAsync(int id);
        Task<TaskEntity> UpdateTaskAsync(int id, Action<TaskEntity> applyChanges);
        Task<bool> DeleteTaskAsync(int id);
    }
}