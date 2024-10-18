using System;
using EsigGestãoDeTarefasApp.Enums;

namespace EsigGestãoDeTarefasApp.Models
{
    public class EmployeeTask
    {
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }

        public StatusEnum Status { get; set; }

        public int TaskId { get; set; }
        public Task Task { get; set; }

        public DateTime AssignedDate { get; set; }
        //public string Status { get; set; }
        public EmployeeTask()
        {
            

        }
    }
}

