using System;
using EsigGestãoDeTarefasApp.Dtos;
using EsigGestãoDeTarefasApp.Interfaces;
using EsigGestãoDeTarefasApp.Repository;

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

    }



}

