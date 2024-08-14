using System.ComponentModel.DataAnnotations;

namespace Portfolio.Models
{
    public class Project
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Project Tittle")]
        public string Project_Tittle { get; set; }

        [Required]
        [Display(Name = "Domain Name")]
        public string Domain_Name { get; set; }

        [Required]
        [Display(Name = "Project LOGO ")]
        public string Project_LOGO { get; set; }

        [Required]
        [Display(Name = "Project Details")]
        public string Project_Details { get; set; }

        [Required]
        [Display(Name = "Starting Date")]
        public string Starting_Date { get; set; }

        [Required]
        [Display(Name = "Ending Date")]
        public string Ending_Date { get; set; }

        [Required]
        [Display(Name = "Using Languages")]
        public string Using_Languages { get; set; }

        [Required]
        [Display(Name = "Using Framwork")]
        public string Using_Framwork { get; set; }

        [Required]
        [Display(Name = "Using Database")]
        public string Using_Database { get; set; }

        public int UserID { get; set; }

    }




}
