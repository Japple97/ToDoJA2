using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ToDoJA2.Models
{
    public class AppUser : IdentityUser
    {

        [Required]
        [Display(Name = "First Name")]
        [StringLength(50, ErrorMessage = "The {0} must be at least {2} and max {1} characters long.", MinimumLength = 2)]
        public string? FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        [StringLength(50, ErrorMessage = "The {0} must be at least {2} and max {1} characters long.", MinimumLength = 2)]
        public string? LastName { get; set;}

        [NotMapped]
        public string? FullName { get { return $"{FirstName} {LastName}"; } }

        //AppUser listed items

        public virtual ICollection<ToDoItems> ToDoItems { get; set; } = new HashSet<ToDoItems>();

        public virtual ICollection<Accessories> Accessories { get; set; } = new HashSet<Accessories>();

    }
}
