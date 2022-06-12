using AutoMapper;
using schoolApi.Models;
using SchoolApi.Models;
using SchoolApi.ViewModels;
using SchoolApi.ViewModels.StudentsView;
using SchoolApi.ViewModels.TeachersView;

namespace SchoolApi.Helpers
{
    public class AutoMappersProfiles : Profile
    {
        public AutoMappersProfiles()
        {
            // från > till...
            CreateMap<PostCourseViewModel, Course>();
            CreateMap<Course, CourseViewModel>()
            .ForMember(dest => dest.CourseId, options => options.MapFrom(src => src.Id));


            CreateMap<PostStudentViewModel, Students>();
            CreateMap<Students, StudentViewModel>()
            .ForMember(dest => dest.StudentId, option => option.MapFrom (src => src.Id)); 
            CreateMap<StudentViewModel, Students>();


            CreateMap<PostTeacherViewModel, Teachers>();
             CreateMap<Teachers, TeacherViewModel>()
            .ForMember(dest => dest.TeacherId, option => option.MapFrom (src => src.Id)); 
            CreateMap<TeacherViewModel, Teachers>();
            
        }
        
    }
}