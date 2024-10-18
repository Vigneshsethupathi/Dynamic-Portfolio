using System;
using System.Collections.Generic;

namespace DynamicPortfolio.Models;

public partial class Skill
{
    public int Id { get; set; }

    public string TechnicalSkills { get; set; } = null!;

    public string SoftSkills { get; set; } = null!;

    public string LanguageSkills { get; set; } = null!;

    public string InterpersonalSkills { get; set; } = null!;

    public string TechnicalWritingDocumentation { get; set; } = null!;

    public string DataScienceSkills { get; set; } = null!;

    public string HealthWellnessSkills { get; set; } = null!;

    public string AdditionalSkills { get; set; } = null!;
}
