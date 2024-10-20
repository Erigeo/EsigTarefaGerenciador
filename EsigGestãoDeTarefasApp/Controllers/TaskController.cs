using System;
using EsigGestãoDeTarefasApp.Dtos;
using EsigGestãoDeTarefasApp.Interfaces;
using EsigGestãoDeTarefasApp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
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
        [SwaggerOperation(Summary = "Recebe todas as tasks")]
        public ActionResult<ICollection<Task>> GetAllTasks()
        {
            var tasks = _taskService.GetAllTasks();
            if (tasks == null || !tasks.Any())
                return NotFound("No tasks found");

            return Ok(tasks);
        }

        // GET: api/tasks/{id}
        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Recebe uma task pelo Id")]
        public ActionResult<Task> GetTaskById(int id)
        {
            var task = _taskRepository.GetTasksById(id);
            if (task == null)
                return NotFound($"Task with ID {id} not found");

            return Ok(task);
        }

        // GET: api/tasks/title/{title}
        [HttpGet("title/{title}")]
        [SwaggerOperation(Summary = "Recebe uma Task baseado no title")]
        public ActionResult<Task> GetTaskByTitle(string title)
        {
            var task = _taskRepository.GetTasksByTitle(title);
            if (task == null)
                return NotFound($"Task with title '{title}' not found");

            return Ok(task);
        }

        // POST: api/tasks
        [HttpPost]
        [SwaggerOperation(Summary = "Cria uma nova Task")]
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
        [SwaggerOperation(Summary = "Atualiza a Task")]
        public ActionResult UpdateTask(int id, [FromBody] TaskDto taskDto)
        {
            Console.WriteLine("nosso task dto:" + taskDto.ToString());
            if (taskDto == null)
                return BadRequest("Task object is null or ID mismatch");

            if (!_taskRepository.TaskExist(id))
                return NotFound($"Task with ID abacaxi {id} not found");

            if (!_taskService.UpdateTask(id, taskDto))
                return StatusCode(500, "An error occurred while updating the task");

            return NoContent();
        }

        // DELETE: api/tasks/{id}
        [HttpDelete("{id}")]
        [SwaggerOperation(Summary = "Remove a Task pelo id")]
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


