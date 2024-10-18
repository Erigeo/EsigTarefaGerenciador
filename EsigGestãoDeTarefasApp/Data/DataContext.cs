using System;
using EsigGestãoDeTarefasApp.Models;
using Microsoft.EntityFrameworkCore;

namespace EsigGestãoDeTarefasApp.Data
{
	public class DataContext : DbContext
	{
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Models.Task> Tasks { get; set; }
        public DbSet<EmployeeTask> EmployeeTasks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EmployeeTask>()
                .HasKey(et => new { et.EmployeeId, et.TaskId });

            // Relacionamento muitos-para-muitos: um Employee pode ter várias EmployeeTasks
            modelBuilder.Entity<EmployeeTask>()
                .HasOne(et => et.Employee)
                .WithMany()
                .HasForeignKey(et => et.EmployeeId);

            // Relacionamento muitos-para-muitos: uma Task pode ter várias EmployeeTasks
            modelBuilder.Entity<EmployeeTask>()
                .HasOne(et => et.Task)
                .WithMany()
                .HasForeignKey(et => et.TaskId);
        }

    }
}

