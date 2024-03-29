using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using SchoolApi.Data;
using SchoolApi.Interfaces;
using SchoolApi.Models;
using SchoolApi.ViewModels.TeachersView;

namespace SchoolApi.Repositories
{
    public class TeachersRepository : ITeachersRepository
    {
        private readonly IMapper _mapper;
        private readonly CourseContext _context;
        public TeachersRepository(CourseContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
            
        }


         public async Task<List<TeacherViewModel>> ListAllTeachersAsync()
        {
            return await _context.Teachers.ProjectTo<TeacherViewModel>(_mapper.ConfigurationProvider).ToListAsync();

        }
        
        public async Task<TeacherViewModel?> GetTeacherAsync(int id)
        {
            return await _context.Teachers.Where(t => t.Id == id).ProjectTo<TeacherViewModel>(_mapper.ConfigurationProvider).SingleOrDefaultAsync();
        }
       
        public async Task<TeacherViewModel?> GetTeacherByEmailAsync(string Email)
        {
             return await _context.Teachers.Where(o => o.Email!.ToLower() == Email.ToLower()).ProjectTo<TeacherViewModel>(_mapper.ConfigurationProvider).SingleOrDefaultAsync();
        }

        
        public async Task AddTeacherAsync(PostTeacherViewModel teacher)
        {
            var NewTeacher = _mapper.Map<Teachers>(teacher);
            await _context.Teachers.AddAsync(NewTeacher);
        }

         public async Task UpdateTeacherAsync(int id, PostTeacherViewModel model)
        {
            var teacher = await _context.Teachers.FindAsync(id);
            if (teacher is null) throw new Exception($"We could not find the Student ID:");

            _mapper.Map<PostTeacherViewModel, Teachers>(model, teacher);
            _context.Teachers.Update(teacher);
        }
        public async Task DeleteTeacherAsync(int id)
        {
            var response = await _context.Teachers.FindAsync(id);
           if(response is not null) _context.Teachers.Remove(response);
            
        }



        public async Task<bool> SaveAllAsync()
        {
           return await _context.SaveChangesAsync() > 0;
        }

    }
}