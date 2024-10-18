using System;
using EsigGestãoDeTarefasApp.Dtos;
using EsigGestãoDeTarefasApp.Interfaces;
using EsigGestãoDeTarefasApp.Models;
using EsigGestãoDeTarefasApp.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EsigGestãoDeTarefasApp.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeTaskController : Controller
    {
        


        private readonly IEmployeeTaskRepository _employeeTaskRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly ITaskRepository _taskRepository;

        public EmployeeTaskController(IEmployeeTaskRepository employeeTaskRepository, ITaskRepository taskRepository, IEmployeeRepository employeeRepository)
        {
            _employeeTaskRepository = employeeTaskRepository;
            _employeeRepository = employeeRepository;
            _taskRepository = taskRepository;
        }

        // GET: api/employeetasks
        [HttpGet]
        public ActionResult<ICollection<EmployeeTask>> GetAllEmployeeTasks()
        {
            var employeeTasks = _employeeTaskRepository.GetAllEmployeeTask();
            if (employeeTasks == null || !employeeTasks.Any())
                return NotFound("No employee tasks found");

            return Ok(employeeTasks);
        }

        // GET: api/employeetasks/employee/{employeeId}
        [HttpGet("employee/{employeeId}")]
        public ActionResult<ICollection<EmployeeTask>> GetEmployeeTasksByEmployeeId(int employeeId)
        {
            var employeeTasks = _employeeTaskRepository.GetEmployeeTaskByEmployeeId(employeeId);
            if (employeeTasks == null || !employeeTasks.Any())
                return NotFound("No tasks found for this employee");

            return Ok(employeeTasks);
        }

        // GET: api/employeetasks/task/{taskId}
        [HttpGet("task/{taskId}")]
        public ActionResult<ICollection<EmployeeTask>> GetEmployeeTasksByTaskId(int taskId)
        {
            var employeeTasks = _employeeTaskRepository.GetEmployeeTaskByTaskId(taskId);
            if (employeeTasks == null || !employeeTasks.Any())
                return NotFound("No employees found for this task");

            return Ok(employeeTasks);
        }

        // POST: api/employeetasks
        [HttpPost]
        public ActionResult CreateEmployeeTask([FromBody] EmployeeTaskDto dto)
        {
            if (dto == null)
                return BadRequest("EmployeeTask object is null");

            // Recupera o Employee e o Task do banco de dados
            var employee = _employeeRepository.GetEmployeeById(dto.EmployeeId);
            var task = _taskRepository.GetTasksById(dto.TaskId);

            if (employee == null)
                return NotFound($"No employee found with ID {dto.EmployeeId}");

            if (task == null)
                return NotFound($"No task found with ID {dto.TaskId}");

            // Cria o EmployeeTask com os dados recuperados
            var newEmployeeTask = new EmployeeTask
            {
                EmployeeId = employee.Id,
                TaskId = task.Id,
                AssignedDate = DateTime.Now // Definindo a data atual
            };

            // Tenta salvar o novo EmployeeTask
            if (!_employeeTaskRepository.CreateEmployeeTask(newEmployeeTask))
                return StatusCode(500, "An error occurred while creating the employee task");

            return CreatedAtAction(nameof(GetEmployeeTasksByEmployeeId), new { employeeId = newEmployeeTask.EmployeeId }, newEmployeeTask);
        }

        // PUT: api/employeetasks
        [HttpPut]
        public ActionResult UpdateEmployeeTask([FromBody] EmployeeTask employeeTask)
        {
            if (employeeTask == null)
                return BadRequest("EmployeeTask object is null");

            if (!_employeeTaskRepository.UpdateEmployeeTask(employeeTask))
                return NotFound("EmployeeTask not found");

            return NoContent();
        }

        // DELETE: api/employeetasks/{id}
        [HttpDelete("{id}")]
        public ActionResult DeleteEmployeeTask([FromBody] EmployeeTaskDto dto)
        {
            if (dto == null)
                return BadRequest("EmployeeTask object is null");

            var employeeTask = _employeeTaskRepository.GetEmployeeTaskByIds(dto.EmployeeId, dto.TaskId);
            if (employeeTask == null)
                return NotFound("EmployeeTask not found");

            if (!_employeeTaskRepository.DeleteEmployeeTask(employeeTask))
                return StatusCode(500, "An error occurred while deleting the employee task");

            return NoContent();
        }
    }
}


