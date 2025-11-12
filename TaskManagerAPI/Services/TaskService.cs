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
        public async Task<List<TaskEntity>> GetAllTasksAsync()
        {
            var tasks = await _repo.GetAllTasksAsync();
            return tasks;
        }
        public async Task<TaskReadDto> CreateTaskAsync(TaskCreateDto taskCreateDto)
        {
            var TaskEntity = new TaskEntity
            {
                Title = taskCreateDto.Title,
                Description = taskCreateDto.Description,
                DueDate = taskCreateDto.DueDate,
                CreatedAt = DateOnly.FromDateTime(DateTime.UtcNow),
                AssignedTo = taskCreateDto.AssignedTo,
                Status = taskCreateDto.Status
            };
            var createdTask = await _repo.CreateTaskAsync(TaskEntity);
            var taskReadDto = new TaskReadDto
            {
                Id = createdTask.Id,
                Title = createdTask.Title,
                Description = createdTask.Description,
                DueDate = createdTask.DueDate,
                CreatedAt = createdTask.CreatedAt,
                AssignedTo = createdTask.AssignedTo,
                Status = createdTask.Status
            };
            return taskReadDto;
        }
        public async Task<TaskReadDto?> GetTaskByIdAsync(int id)
        {
            var task = await _repo.GetTaskByIdAsync(id);
            if (task is null) return null;
            var taskReadDto = new TaskReadDto
            {
                Id = task.Id,
                Title = task.Title,
                Description = task.Description,
                DueDate = task.DueDate,
                CreatedAt = task.CreatedAt,
                AssignedTo = task.AssignedTo,
                Status = task.Status
            };
            return taskReadDto;
        }
        public async Task<TaskReadDto?> UpdateTaskAsync(int id, taskUpdateDto taskUpdateDto)
        {
            TaskEntity? updatedTask = await _repo.UpdateTaskAsync(id, task =>
            {
                if (taskUpdateDto.Title is not null)
                    task.Title = taskUpdateDto.Title;
                if (taskUpdateDto.Description is not null)
                    task.Description = taskUpdateDto.Description;
                if (taskUpdateDto.DueDate is not null)
                    task.DueDate = taskUpdateDto.DueDate;
                if (taskUpdateDto.AssignedTo is not null)
                    task.AssignedTo = taskUpdateDto.AssignedTo;
                if (taskUpdateDto.Status is not null)
                    task.Status = taskUpdateDto.Status.Value;
            });
            if (updatedTask is null) return null;
            var taskReadDto = new TaskReadDto
            {
                Id = updatedTask.Id,
                Title = updatedTask.Title,
                Description = updatedTask.Description,
                DueDate = updatedTask.DueDate,
                CreatedAt = updatedTask.CreatedAt,
                AssignedTo = updatedTask.AssignedTo,
                Status = updatedTask.Status
            };
            return taskReadDto;
        }
        public async Task<bool> DeleteTaskAsync(int id)
        {
            var result = await _repo.DeleteTaskAsync(id);
            return result;
        }
    }
}