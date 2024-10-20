using System;
using EsigGestãoDeTarefasApp.Enums;
using EsigGestãoDeTarefasApp.Models;

namespace EsigGestãoDeTarefasApp.Dtos
{
	public class TaskDto
	{
        
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? Priority { get; set; }
        public StatusEnum? Status { get; set; }
        public DateTime? Deadline { get; set; }
        public int EmployeeId { get; set; }
    }
}

