﻿using System.ComponentModel.DataAnnotations;

namespace SharedLiberary.DTOS
{
    public class LoginDto
    {
        [Required]
        [EmailAddress]
        public string Email {  get; set; }  
        [Required]  
        public string Password { get; set; }
    }
}
