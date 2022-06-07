using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolApi.ViewModels
{
    public class PostCourseViewModel
    {
        [Required]
        public string? Title { get; set; }
        [Required]
        [RegularExpression(@"^[0-9]{4,4}$", ErrorMessage = "Course number must be 4 digit.")] 
        public int CourseNumber { get; set; }
    }
}