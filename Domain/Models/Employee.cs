using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Employee
    {
        
        public int EmployeeId { get; set; }
        public string? EmployeeName { get; set; }
        public int DepartmentId { get; set; }

    }
}
