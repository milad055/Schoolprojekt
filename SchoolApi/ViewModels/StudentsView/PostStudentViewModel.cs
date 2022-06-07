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
        public String? Email { get; set; }
    }
}