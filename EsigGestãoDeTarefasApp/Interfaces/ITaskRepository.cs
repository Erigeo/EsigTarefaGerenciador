using System;
using EsigGestãoDeTarefasApp.Models;

namespace EsigGestãoDeTarefasApp.Interfaces
{
	public interface ITaskRepository
	{

        ICollection<Models.Task> GetAllTasks();
        Models.Task? GetTasksById(int id);
        Models.Task? GetTasksByTitle(string title);
        bool CreateTask(Models.Task task);
        bool UpdateTask(Models.Task task);
        bool DeleteTask(Models.Task task);
        bool TaskExist(int id);
        bool Save();
    }
}

