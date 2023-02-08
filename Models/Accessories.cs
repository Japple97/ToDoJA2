using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace ToDoJA2.Models
{
    public class Accessories
    {
        public int Id { get; set; }

        [Required]
        public string? Name { get; set; }

        [Required]
        public string? AppUserId { get; set; }

        //Collection of ToDoItems accessories belongs to
        public virtual AppUser? AppUser { get; set; }
        public virtual ICollection<ToDoItems> ToDoItems { get; set; } = new HashSet<ToDoItems>();

    }
}
