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
        public string? Email { get; set; }
        public string? expertnesses { get; set; }
    }
}