

using System.ComponentModel.DataAnnotations;

namespace Portfolio.Models
{
    public class Users
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Password")]
        public string Password { get; set; }


    }

    
}
