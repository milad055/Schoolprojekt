using SchoolApi.ViewModels;

namespace SchoolApi.Interfaces
{
    public interface ICourseRepository
    {
        public Task<List<CourseViewModel>> ListAllCoursesAsync();
        public Task<CourseViewModel?> GetCourseByNumberAsync(int courseNumber);
        public Task AddCourseAsync(PostCourseViewModel model);
        public Task UpdateCourseAsync(int id, PostCourseViewModel model);
        public Task DeleteCourseAsync(int id);
        public Task<bool> SaveAllAsync();

    }
}