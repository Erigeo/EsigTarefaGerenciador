using System;
using EsigGestãoDeTarefasApp.Models;

namespace EsigGestãoDeTarefasApp.Interfaces
{
	public interface IEmployeeTaskRepository
	{
        ICollection<EmployeeTask> GetAllEmployeeTask();
        ICollection<EmployeeTask> GetEmployeeTaskByEmployeeId(int id);
        ICollection<EmployeeTask> GetEmployeeTaskByTaskId(int id);
        EmployeeTask? GetEmployeeTaskByIds(int taskId, int employeeId);
        bool CreateEmployeeTask(EmployeeTask employee);
        bool UpdateEmployeeTask(EmployeeTask employee);
        bool DeleteEmployeeTask(EmployeeTask employee);
        bool Save();
    }
}

