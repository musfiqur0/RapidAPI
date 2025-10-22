using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs;

public class GroupAddEditDto
{
    public int Id { get; set; } = 0;
    public string Name { get; set; }
    public string Code { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public short StatusId { get; set; }
    public bool Default { get; set; } = true;
    public string Action { get; set; } = string.Empty;
}
