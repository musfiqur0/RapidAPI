using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models;

public class AllowanceType : Base
{
    public string Description { get; set; }
    public bool Default { get; set; }
    public bool Draft { get; set; }
}
