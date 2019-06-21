using System;
using System.ComponentModel.DataAnnotations;

namespace WebApplication.Models.Home
{
    public class CheckIn
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        public int Number { get; set; }

        [Required]
        public int Version { get; set; }
    }
}
