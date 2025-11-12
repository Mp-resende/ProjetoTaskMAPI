using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TaskManagerAPI.Models.Dto;
using TaskManagerAPI.Services;
using TaskManagerAPI.Models;

namespace TaskManagerAPI.Controllers
{
    [ApiController]
    [Route("api/tasks")] // Rota corrigida que eu sugeri antes
    public class TaskController : ControllerBase
    {
        private readonly ITaskService _service;
        public TaskController(ITaskService service) => _service = service;

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        // CORRIGIDO: Usando Models.TaskStatus?
        public async Task<IActionResult> GetAllTasks([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10, [FromQuery] Models.TaskStatus? status = null, [FromQuery] string? assignedTo = null, [FromQuery] DateOnly? dueBefore = null)
        {
            // Retorne a variável 'tasks'
            var tasks = await _service.GetAllTasksAsync(pageNumber, pageSize, status, assignedTo, dueBefore);
            return Ok(tasks);
        }

        // ... resto do arquivo sem alterações ...

        [HttpGet("{id:int}", Name = "GetTaskById")]
        [ProducesResponseType(typeof(TaskReadDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetTaskById(int id)
        {
            var task = await _service.GetTaskByIdAsync(id);
            if (task is null) return NotFound();
            return Ok(task);
        }

        [HttpPost]
        [ProducesResponseType(typeof(TaskReadDto), StatusCodes.Status201Created)]
        public async Task<IActionResult> CreateTask([FromBody] TaskCreateDto task)
        {
            var taskCreated = await _service.CreateTaskAsync(task);
            return CreatedAtRoute("GetTaskById", new{id = taskCreated.Id },taskCreated);
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteTask(int id)
        {
            var removed = await _service.DeleteTaskAsync(id);
            if (!removed) return NotFound();
            return NoContent();
        }


        [HttpPut("{id:int}")] // Adicionado :int
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateTask(int id, [FromBody] taskUpdateDto task)
        {
            var updated = await _service.UpdateTaskAsync(id, task);
            if (updated is null) return NotFound();
            return NoContent();
        }
    }
}