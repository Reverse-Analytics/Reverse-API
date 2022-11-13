﻿using System.ComponentModel.DataAnnotations;

namespace ReverseAnalytics.Domain.DTOs.UserAccount
{
    public class UserAccountForCreateDto
    {
        [Required(ErrorMessage = "Login is required.")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Login is required.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare(nameof(Password), ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }
}
