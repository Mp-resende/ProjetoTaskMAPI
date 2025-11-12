using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore; // Necessário para .AsQueryable(), .Where(), etc.
using TaskManagerAPI.Models;

namespace TaskManagerAPI.Data.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private readonly AppDbContext _db;
        public TaskRepository(AppDbContext db) => _db = db;

        // CORRIGIDO: Implementação do método com filtros e paginação
        public async Task<List<TaskEntity>> GetAllTasksAsync(int pageNumber, int pageSize, Models.TaskStatus? status, string? assignedTo, DateOnly? dueBefore)
        {
            // Começamos com um IQueryable para construir a consulta
            IQueryable<TaskEntity> query = _db.Tasks.AsQueryable();

            // 1. Aplicar Filtros (requisito funcional)
            if (status.HasValue)
            {
                query = query.Where(t => t.Status == status.Value);
            }

            if (!string.IsNullOrWhiteSpace(assignedTo))
            {
                // Usando EF.Functions.ILike para busca case-insensitive (específico do PostgreSQL)
                query = query.Where(t => t.AssignedTo != null && 
                                         EF.Functions.ILike(t.AssignedTo, $"%{assignedTo}%"));
            }

            if (dueBefore.HasValue)
            {
                // Filtra por data limite ATÉ (inclusive) o dia especificado
                query = query.Where(t => t.DueDate.HasValue && t.DueDate.Value <= dueBefore.Value);
            }

            // 2. Aplicar Paginação (desafio extra)
            var tasks = await query
                .OrderByDescending(t => t.CreatedAt) // Ordenar para paginação consistente
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

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