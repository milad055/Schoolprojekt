using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolApi.ViewModels.TeachersView
{
    public class TeacherViewModel
    {

        public int TeacherId { get; set; }
        public string? Firstname { get; set; }
        public string? Lastname { get; set; }
        public string? Email { get; set; }
        public int TelefonNumber { get; set; }
        public string? Address { get; set; }
        public string? expertness { get; set; }
    }
}