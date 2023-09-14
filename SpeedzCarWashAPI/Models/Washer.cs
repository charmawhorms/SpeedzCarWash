﻿using System.ComponentModel.DataAnnotations;

namespace SpeedzCarWashAPI.Models
{
    public class Washer
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public bool Available { get; set; }
    }
}
