using System;
using EsigGestãoDeTarefasApp.Dtos;
using EsigGestãoDeTarefasApp.Interfaces;
using EsigGestãoDeTarefasApp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Task = EsigGestãoDeTarefasApp.Models.Task;

namespace EsigGestãoDeTarefasApp.Controllers
{
    [Authorize]
    [Route("api/task/")]
    [ApiController]
    public class TaskController : Controller
	{
        private readonly ITaskRepository _taskRepository;
        private readonly TaskService _taskService;


        public TaskController(ITaskRepository taskRepository, TaskService taskService)
        {
            _taskRepository = taskRepository;
            _taskService = taskService;
        }

        // GET: api/tasks
        [HttpGet]
        public ActionResult<ICollection<Task>> GetAllTasks()
        {
            var tasks = _taskRepository.GetAllTasks();
            if (tasks == null || !tasks.Any())
                return NotFound("No tasks found");

            return Ok(tasks);
        }

        // GET: api/tasks/{id}
        [HttpGet("{id}")]
        public ActionResult<Task> GetTaskById(int id)
        {
            var task = _taskRepository.GetTasksById(id);
            if (task == null)
                return NotFound($"Task with ID {id} not found");

            return Ok(task);
        }

        // GET: api/tasks/title/{title}
        [HttpGet("title/{title}")]
        public ActionResult<Task> GetTaskByTitle(string title)
        {
            var task = _taskRepository.GetTasksByTitle(title);
            if (task == null)
                return NotFound($"Task with title '{title}' not found");

            return Ok(task);
        }

        // POST: api/tasks
        [HttpPost]
        public ActionResult CreateTask([FromBody] TaskDto task)
        {
            if (task == null)
                return BadRequest("Task object is null");

            var taskCreated = _taskService.CreateTask(task);

            if ( taskCreated == null)
                return StatusCode(500, "An error occurred while creating the task");




            return CreatedAtAction(nameof(GetTaskById), new { id = taskCreated.Id }, taskCreated);
        }

        // PUT: api/tasks/{id}
        [HttpPut("{id}")]
        public ActionResult UpdateTask(int id, [FromBody] Task task)
        {
            if (task == null || task.Id != id)
                return BadRequest("Task object is null or ID mismatch");

            if (!_taskRepository.TaskExist(id))
                return NotFound($"Task with ID {id} not found");

            if (!_taskRepository.UpdateTask(task))
                return StatusCode(500, "An error occurred while updating the task");

            return NoContent();
        }

        // DELETE: api/tasks/{id}
        [HttpDelete("{id}")]
        public ActionResult DeleteTask(int id)
        {
            var task = _taskRepository.GetTasksById(id);
            if (task == null)
                return NotFound($"Task with ID {id} not found");

            if (!_taskRepository.DeleteTask(task))
                return StatusCode(500, "An error occurred while deleting the task");

            return NoContent();
        }
    }
}


