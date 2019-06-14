﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FontaineVerificationProject.Dtos
{
    public class RegisterDto
    {
        [Required]
        [StringLength(50, MinimumLength = 6, ErrorMessage = "UserName must be at least 6 characters.")]
        public string UserName { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 6, ErrorMessage = "User must be at least 6 characters.")]
        public string UserLog { get; set; }
        [Required]
        [StringLength(64, MinimumLength = 8, ErrorMessage = "You must provide password between 8 and 20 characters.")]
        public string Password { get; set; }
    }
}
