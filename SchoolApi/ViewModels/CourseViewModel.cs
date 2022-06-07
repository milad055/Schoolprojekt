using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolApi.ViewModels
{
    public class CourseViewModel
    {
        public int CourseId { get; set; }
         public int CourseNumber { get; set; }
        public string? Title { get; set; }
        public string? CourseLength { get; set; }
        
    }
}