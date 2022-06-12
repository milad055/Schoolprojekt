using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolApi.ViewModels.TeachersView
{
    public class PostTeacherViewModel
    {
        [Required]
        public string? FirstName { get; set; }
        [Required]
        public string? LastName { get; set; }
        [Required]
        public string? Email { get; set; }
        [Required]
        public int TelefonNumber { get; set; }
        [Required]
        public string? Address { get; set; }
        [Required]
        public string? expertness { get; set; }
    }
}