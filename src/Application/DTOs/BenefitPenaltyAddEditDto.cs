using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs;

public class BenefitPenaltyAddEditDto
{
    public int Id { get; set; } = 0;
    public int TypeId { get; set; }
    public int SubjectId { get; set; }
    public string Criteria { get; set; }
    public DateTime Date { get; set; }
    public int DriverId { get; set; }
    public int FormalityId { get; set; }
    public string Description { get; set; } = string.Empty;
}
