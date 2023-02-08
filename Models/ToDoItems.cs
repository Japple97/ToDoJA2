using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace ToDoJA2.Models
{
    public class ToDoItems
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "To Do")]
        public string? Name { get; set; }

        [Required]
        public string? AppUserId { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime DateCreated { get; set; }

        [DataType(DataType.Date)]
        public DateTime DueDate { get; set; }

        public bool IsCompleted { get; set; }

        //ToDoItems should belong to singler user
        public virtual AppUser? AppUser { get; set; }
        //Has a collection of accessories that MAY be necessary to complete the ToDoItem

        public virtual ICollection<Accessories> Accessories { get; set; } = new HashSet<Accessories>();



    }
}
