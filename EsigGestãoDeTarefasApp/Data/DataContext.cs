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
       

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Models.Task>()
        .HasOne(t => t.Employee)            // Configura a relação um-para-muitos
        .WithMany(e => e.Tasks)             
        .HasForeignKey(t => t.EmployeeId);
        }

    }
}

