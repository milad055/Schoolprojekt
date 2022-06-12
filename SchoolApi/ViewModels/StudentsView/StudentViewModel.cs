using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolApi.ViewModels.StudentsView
{
    public class StudentViewModel
    {
        public int StudentId { get; set; }
        public string? Firstname { get; set; }
        public string? Lastname { get; set; }
        public string? Email { get; set; }
        public int TelefonNumber { get; set; }
        public string? Address { get; set; }
    }
}