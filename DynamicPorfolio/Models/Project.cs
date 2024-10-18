using System;
using System.Collections.Generic;

namespace DynamicPortfolio.Models;

public partial class Project
{
    public int Id { get; set; }

    public string ProjectTittle { get; set; } = null!;

    public string DomainName { get; set; } = null!;

    public string ProjectLogo { get; set; } = null!;

    public string ProjectDetails { get; set; } = null!;

    public string StartingDate { get; set; } = null!;

    public string EndingDate { get; set; } = null!;

    public string UsingLanguages { get; set; } = null!;

    public string UsingFramwork { get; set; } = null!;

    public string UsingDatabase { get; set; } = null!;

    public int SessionId { get; set; }
}
