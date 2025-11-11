using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskManagerAPI.Data.Repositories;
using TaskManagerAPI.Models;

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
    }
}