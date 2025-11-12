using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TaskManagerAPI.Models;

namespace TaskManagerAPI.Data.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private readonly AppDbContext _db;
        public TaskRepository(AppDbContext db) => _db = db;
        public async Task<List<TaskEntity>> GetAllTasksAsync()
        {
            var tasks = await _db.Tasks.ToListAsync();
            return tasks;
        }

        public async Task<TaskEntity> CreateTaskAsync(TaskEntity task)
        {
            _db.Tasks.Add(task);
            await _db.SaveChangesAsync();
            return task;
        }

        public async Task<TaskEntity?> GetTaskByIdAsync(int id)
        {
            var task = await _db.Tasks.FindAsync(id);
            return task;
        }

        public async Task<TaskEntity> UpdateTaskAsync(int id, Action<TaskEntity> applyChanges)
        {
            var task = await GetTaskByIdAsync(id);
            if (task is null) return null!;
            applyChanges(task);
            await _db.SaveChangesAsync();
            return task;
        }
        
        public async Task<bool> DeleteTaskAsync(int id)
        {
            var task = await GetTaskByIdAsync(id);
            if (task is null) return false;
            _db.Tasks.Remove(task);
            await _db.SaveChangesAsync();
            return true;
        }
    }
}