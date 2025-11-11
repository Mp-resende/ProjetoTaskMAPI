using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaskManagerAPI.Data.Repositories
{
    public interface ITaskRepository
    {
        Task<List<Models.TaskEntity>> GetAllTasksAsync();
    }
}