using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaskManagerAPI.Services
{
    public interface ITaskService
    {
        Task<List<Models.TaskEntity>> GetAllTasksAsync();
    }
}