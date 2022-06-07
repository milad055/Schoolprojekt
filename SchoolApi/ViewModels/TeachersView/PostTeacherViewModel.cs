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
        public string? Email { get; set; }
        [Required]
        public string? expertnesses { get; set; }
    }
}