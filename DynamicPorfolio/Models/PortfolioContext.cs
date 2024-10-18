using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace DynamicPortfolio.Models;

public partial class PortfolioContext : DbContext
{
    public PortfolioContext()
    {
    }

    public PortfolioContext(DbContextOptions<PortfolioContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AboutMe> AboutMes { get; set; }

    public virtual DbSet<Project> Projects { get; set; }

    public virtual DbSet<Skill> Skills { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("server=DESKTOP-N1R9AT0\\SQLEXPRESS;Initial Catalog=Portfolio;Integrated Security=True;Trust Server Certificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AboutMe>(entity =>
        {
            entity.ToTable("About Me");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.CareerAmbition)
                .HasMaxLength(50)
                .HasColumnName("Career Ambition");
            entity.Property(e => e.Certifications).HasMaxLength(50);
            entity.Property(e => e.CurrentRole)
                .HasMaxLength(50)
                .HasColumnName("Current Role");
            entity.Property(e => e.DateOfBirth).HasColumnName("Date_Of_Birth");
            entity.Property(e => e.FatherName)
                .HasMaxLength(50)
                .HasColumnName("Father_Name");
            entity.Property(e => e.Hobbies).HasMaxLength(50);
            entity.Property(e => e.LanguagesSpoken)
                .HasMaxLength(50)
                .HasColumnName("Languages Spoken");
            entity.Property(e => e.MobileNumber).HasColumnName("Mobile Number");
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.Nationality).HasMaxLength(50);
            entity.Property(e => e.YearsOfExperience)
                .HasColumnType("decimal(18, 0)")
                .HasColumnName("Years of Experience");
        });

        modelBuilder.Entity<Project>(entity =>
        {
            entity.ToTable("projects");

            entity.Property(e => e.DomainName).HasColumnName("Domain_Name");
            entity.Property(e => e.EndingDate).HasColumnName("Ending_Date");
            entity.Property(e => e.ProjectDetails).HasColumnName("Project_Details");
            entity.Property(e => e.ProjectLogo).HasColumnName("Project_LOGO");
            entity.Property(e => e.ProjectTittle).HasColumnName("Project_Tittle");
            entity.Property(e => e.StartingDate).HasColumnName("Starting_Date");
            entity.Property(e => e.UsingDatabase).HasColumnName("Using_Database");
            entity.Property(e => e.UsingFramwork).HasColumnName("Using_Framwork");
            entity.Property(e => e.UsingLanguages).HasColumnName("Using_Languages");
        });

        modelBuilder.Entity<Skill>(entity =>
        {
            entity.ToTable("skills");

            entity.Property(e => e.AdditionalSkills).HasColumnName("Additional_Skills");
            entity.Property(e => e.DataScienceSkills).HasColumnName("DataScience_Skills");
            entity.Property(e => e.HealthWellnessSkills).HasColumnName("Health_&_Wellness_Skills");
            entity.Property(e => e.InterpersonalSkills).HasColumnName("Interpersonal_Skills");
            entity.Property(e => e.LanguageSkills).HasColumnName("Language_Skills");
            entity.Property(e => e.SoftSkills).HasColumnName("Soft_Skills");
            entity.Property(e => e.TechnicalSkills).HasColumnName("Technical_Skills");
            entity.Property(e => e.TechnicalWritingDocumentation).HasColumnName("Technical Writing_&_Documentation");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("users");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
