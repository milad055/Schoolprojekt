using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolApi.ViewModels.StudentsView
{
    public class PostStudentViewModel
    {
        [Required]
        public string? Firstname { get; set; }
        [Required]
        public string? Lastname { get; set; }
        [Required]
        public string? Email { get; set; }
        [Required]
        public int TelefonNumber { get; set; }
        [Required]
        public string? Address { get; set; }
    }
}