using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using schoolApi.Models;
using SchoolApi.Data;
using SchoolApi.Interfaces;
using SchoolApi.ViewModels;

namespace SchoolApi.Repositories
{
    public class CourseRepository : ICourseRepository
    {
        private readonly CourseContext _context;
        private readonly IMapper _mapper;

        public CourseRepository(CourseContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<List<CourseViewModel>> ListAllCoursesAsync()
        {
            return await _context.Courses.ProjectTo<CourseViewModel>(_mapper.ConfigurationProvider).ToListAsync();

        }

        public async Task<CourseViewModel?> GetCourseByNumberAsync(int courseNumber)
        {
            return await _context.Courses.Where(o => o.CourseNumber == courseNumber)
            .ProjectTo<CourseViewModel>(_mapper.ConfigurationProvider)
            .SingleOrDefaultAsync();
        }

        public async Task AddCourseAsync(PostCourseViewModel model)
        {
            var newCourse = _mapper.Map<Course>(model);
            await _context.Courses.AddAsync(newCourse);
        }


        public async Task UpdateCourseAsync(int id, PostCourseViewModel model)
        {
            var course = await _context.Courses.FindAsync(id);
            if (course is null) throw new Exception($"We could not find Course ID: {id}");

            _mapper.Map<PostCourseViewModel, Course>(model, course);
            _context.Courses.Update(course);
        }

        public async Task DeleteCourseAsync(int id)
        {
            var response = await _context.Courses.FindAsync(id);
            if (response is not null) _context.Courses.Remove(response);
        }

        public async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

    }

}