using System.ComponentModel.DataAnnotations;

namespace WebApplication.Models.Home
{
    public class Add
    {
        [Required]
        [Display(Name = "Name")]
        public string Name { get; set; }
    }
}
