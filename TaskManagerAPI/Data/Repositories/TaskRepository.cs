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
    }
}