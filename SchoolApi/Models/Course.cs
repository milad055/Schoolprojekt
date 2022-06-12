using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using SchoolApi.Models;

namespace schoolApi.Models
{
    public class Course
    {
        [Key]
        public int Id { get; set; }
        
        public int CourseNumber { get; set; }
        public string? Title { get; set; }
        public string? CourseLength { get; set; }
        public string? Category { get; set; }
        public string? Description { get; set; }
        public string? Details { get; set; }

        // public ICollection<Students> Students { get; set; } = new List<Students>();
        // public ICollection<Teachers> Teachers { get; set; } = new List<Teachers>();
    }
}