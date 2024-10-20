using System;
using EsigGestãoDeTarefasApp.Dtos;
using EsigGestãoDeTarefasApp.Enums;
using EsigGestãoDeTarefasApp.Interfaces;
using EsigGestãoDeTarefasApp.Repository;
using Microsoft.EntityFrameworkCore;

namespace EsigGestãoDeTarefasApp.Services
{
	public class TaskService
	{

        private readonly ITaskRepository _taskRepository;
        
        private readonly IEmployeeRepository _employeeRepository;
        

        public TaskService(ITaskRepository taskRepository, IEmployeeRepository employeeRepository
			)
		{
			_taskRepository = taskRepository;
            _employeeRepository = employeeRepository;

		}

        public Models.Task CreateTask(TaskDto taskDto)
        {
            

            var emp = _employeeRepository.GetEmployeeById(taskDto.EmployeeId);

            if(emp == null)
            {
                return null;
            }

           
            

        var task = new Models.Task
        {
            Title = taskDto.Title,
            Description = taskDto.Description,
            Priority = taskDto.Priority,
            Status = taskDto.Status,
            Deadline = taskDto.Deadline,
            EmployeeId = taskDto.EmployeeId,
            Employee = emp
        };

            _taskRepository.CreateTask(task);
            return task;
        }

        public bool UpdateTask(int id, TaskDto taskDto) {
            var existingTask = _taskRepository.GetTasksById(id);
            if (existingTask == null) return false;

            existingTask.Title = taskDto.Title;
            existingTask.Description = taskDto.Description;
            existingTask.Priority = taskDto.Priority;
            existingTask.Status = taskDto.Status;
            existingTask.Deadline = taskDto.Deadline;
            existingTask.EmployeeId = taskDto.EmployeeId;

            return _taskRepository.UpdateTask(existingTask);


        }



        public IEnumerable<TaskResponseAllDto> GetAllTasks()
        {
            // Recupera todas as tasks
            var tasks = _taskRepository.GetAllTasks();

            // Recupera todos os employees
            var employees = _employeeRepository.GetEmployees();

            // Mapeia cada task, associando o objeto Employee ao invés do nome
            return tasks.Select(t => new TaskResponseAllDto
            {
                Id = t.Id,
                Title = t.Title,
                Description = t.Description,
                Priority = t.Priority,
                Status = t.Status,
                Deadline = t.Deadline,
               
                Employee = employees
             .Where(e => e.Id == t.EmployeeId)
             .Select(e => new EmployeeDto
             {
                 Id = e.Id,
                 FirstName = e.FirstName,
                 LastName = e.LastName,
                 Email = e.Email,
                 Role = (RoleEnum)e.Role 
             }).FirstOrDefault() 
            }).ToList();
        }

    }





}

