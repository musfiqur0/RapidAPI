using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class EmployeeAddEditDto
    {
        public int Id { get; set; } = 0;
        public string Name { get; set; }
        public string Code { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Department { get; set; } = string.Empty;
        public string Designation { get; set; } = string.Empty;
        public decimal Salary { get; set; }
        public string Manager { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
        public short StatusId { get; set; }
        public bool Default { get; set; }
        public string Action { get; set; } = string.Empty;
    }
}
