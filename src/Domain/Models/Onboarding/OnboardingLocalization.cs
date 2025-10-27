using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models;

public class OnboardingLocalization:Base
{
    public int LanguageId { get; set; }
    public Language Language { get; set; }
    public int OnboardingId { get; set; }
    public Onboarding Onboarding { get; set; }
}
