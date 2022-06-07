using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using schoolApi.Models;
using SchoolApi.Models;

namespace SchoolApi.Data
{
    public class CourseContext : IdentityDbContext
    {
        public DbSet<Course> Courses => Set<Course>();
        public DbSet<Students> Students => Set<Students>();
        public DbSet<Teachers> Teachers => Set<Teachers>();
        public CourseContext(DbContextOptions options) : base(options) { }



    }
}