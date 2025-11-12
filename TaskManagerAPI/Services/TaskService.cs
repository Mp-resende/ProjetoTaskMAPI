using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskManagerAPI.Data.Repositories;
using TaskManagerAPI.Models;
using TaskManagerAPI.Models.Dto;

namespace TaskManagerAPI.Services
{
    public class TaskService : ITaskService
    {
        private readonly ITaskRepository _repo;
        public TaskService(ITaskRepository repo) => _repo = repo;

        public async Task<List<TaskReadDto>> GetAllTasksAsync(int pageNumber = 1, int pageSize = 10, TaskStatus? status = null, string? assignedTo = null, DateOnly? dueBefore = null)
        {
            if (pageNumber < 1) pageNumber = 1;
            if (pageSize < 1) pageSize = 10;

            var tasks = await _repo.GetAllTasksAsync(pageNumber, pageSize, status, assignedTo, dueBefore);
            return tasks.Select(MapToDto).ToList();
        }

        public async Task<TaskReadDto> CreateTaskAsync(TaskCreateDto taskCreateDto)
        {
            if (taskCreateDto is null) throw new ArgumentNullException(nameof(taskCreateDto));

            var entity = new TaskEntity
            {
                Title = taskCreateDto.Title,
                Description = taskCreateDto.Description,
                DueDate = taskCreateDto.DueDate,
                CreatedAt = DateOnly.FromDateTime(DateTime.UtcNow),
                AssignedTo = taskCreateDto.AssignedTo,
                Status = taskCreateDto.Status
            };

            var createdTask = await _repo.CreateTaskAsync(entity);
            return MapToDto(createdTask);
        }

        public async Task<TaskReadDto?> GetTaskByIdAsync(int id)
        {
            var task = await _repo.GetTaskByIdAsync(id);
            return task is null ? null : MapToDto(task);
        }

        public async Task<TaskReadDto?> UpdateTaskAsync(int id, taskUpdateDto taskUpdateDto)
        {
            if (taskUpdateDto is null) return null;

            TaskEntity? updatedTask = await _repo.UpdateTaskAsync(id, task =>
            {
                if (taskUpdateDto.Title is not null)
                    task.Title = taskUpdateDto.Title;
                if (taskUpdateDto.Description is not null)
                    task.Description = taskUpdateDto.Description;
                if (taskUpdateDto.DueDate is not null)
                    task.DueDate = taskUpdateDto.DueDate.Value;
                if (taskUpdateDto.AssignedTo is not null)
                    task.AssignedTo = taskUpdateDto.AssignedTo;
                if (taskUpdateDto.Status is not null)
                    task.Status = taskUpdateDto.Status.Value;
            });

            return updatedTask is null ? null : MapToDto(updatedTask);
        }

        public async Task<bool> DeleteTaskAsync(int id)
            => await _repo.DeleteTaskAsync(id);

        private static TaskReadDto MapToDto(TaskEntity t) =>
            new TaskReadDto
            {
                Id = t.Id,
                Title = t.Title,
                Description = t.Description,
                DueDate = t.DueDate,
                CreatedAt = t.CreatedAt,
                AssignedTo = t.AssignedTo,
                Status = t.Status
            };
    }
}