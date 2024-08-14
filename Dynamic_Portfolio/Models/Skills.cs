using System.ComponentModel.DataAnnotations;

namespace Portfolio.Models
{
    public class Skills
    {
        [Key]
        public int Id { get; set; }


        //--------------------------Technical Skills-------------------->

        [Required]
        [Display(Name = "Programming Languages")]
        public string Programming_skills { get; set; }
        [Required]
        [Display(Name = "Web Development")]
        public string Web_Development { get; set; }

        [Required]
        [Display(Name = "Cloud Computing")]
        public string Cloud_Computing { get; set; }

        [Required]
        [Display(Name = "Data Analysis")]
        public string Data_Analysis { get; set; }



        //------------------Soft Skills--------------------->

        [Required]
        [Display(Name = "Communication")]
        public string Communication { get; set; }

        [Required]
        [Display(Name = "Teamwork")]
        public string Teamwork { get; set; }

        [Required]
        [Display(Name = "Leadership")]
        public string Leadership { get; set; }

        //-----------------------Additional Skills--------------------->
        [Required]
        [Display(Name = "Additional Skills")]
        public string Additional_Skill { get; set; }


    }
}
