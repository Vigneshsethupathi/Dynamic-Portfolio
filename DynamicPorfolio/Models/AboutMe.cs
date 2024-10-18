using System;
using System.Collections.Generic;

namespace DynamicPortfolio.Models;

public partial class AboutMe
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string FatherName { get; set; } = null!;

    public DateOnly DateOfBirth { get; set; }

    public string Education { get; set; } = null!;

    public string Nationality { get; set; } = null!;

    public string? Profession { get; set; }

    public decimal YearsOfExperience { get; set; }

    public string LanguagesSpoken { get; set; } = null!;

    public string? Hobbies { get; set; }

    public string CurrentRole { get; set; } = null!;

    public string? CareerAmbition { get; set; }

    public int MobileNumber { get; set; }

    public string Certifications { get; set; } = null!;

    public int SessionId { get; set; }
}
