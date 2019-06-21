using System;
using System.ComponentModel.DataAnnotations;

namespace WebApplication.Models.Home
{
    public class ChangeName
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required]
        public int Version { get; set; }
    }
}
