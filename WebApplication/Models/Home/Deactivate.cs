using System;
using System.ComponentModel.DataAnnotations;

namespace WebApplication.Models.Home
{
    public class Deactivate
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        public int Version { get; set; }
    }
}
