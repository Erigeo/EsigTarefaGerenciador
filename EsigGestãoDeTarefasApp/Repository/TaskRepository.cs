using System;
using EsigGestãoDeTarefasApp.Data;
using EsigGestãoDeTarefasApp.Interfaces;

namespace EsigGestãoDeTarefasApp.Repository
{
	public class TaskRepository : ITaskRepository
	{
        private readonly DataContext _context;

        public TaskRepository(DataContext context)
		{
            _context = context;
		}

        public bool CreateTask(Models.Task task)
        {

            _context.Tasks.Add(task);
            return Save();

        }

        public bool DeleteTask(Models.Task task)
        {
            _context.Tasks.Remove(task);
            return Save();
        }

        public ICollection<Models.Task> GetAllTasks()
        {
            return _context.Tasks.ToList();
        }

        public Models.Task? GetTasksById(int id)
        {
            return _context.Tasks.FirstOrDefault(t => t.Id == id);
        }

        public Models.Task? GetTasksByTitle(string title)
        {
            return _context.Tasks.FirstOrDefault(t => t.Title.Equals(title, StringComparison.OrdinalIgnoreCase));
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0;
        }

        public bool TaskExist(int id)
        {
            var saved = _context.SaveChanges();
            return saved > 0;
        }

        public bool UpdateTask(Models.Task task)
        {
            _context.Tasks.Update(task);
            return Save();
        }
    }
}

