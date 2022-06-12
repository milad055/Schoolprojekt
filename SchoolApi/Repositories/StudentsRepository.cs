using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using SchoolApi.Data;
using SchoolApi.Interfaces;
using SchoolApi.Models;
using SchoolApi.ViewModels.StudentsView;

namespace SchoolApi.Repositories
{
    public class StudentsRepository : IStudentsRepository
    {
        private readonly CourseContext _context;
        private readonly IMapper _mapper;
        public StudentsRepository(CourseContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
            
        }

        public async Task<List<StudentViewModel>> ListAllStudentsAsync()
        {
            return await _context.Students.ProjectTo<StudentViewModel>(_mapper.ConfigurationProvider).ToListAsync();

        }

        public async Task<StudentViewModel?> GetStudentAsync(int id)
        {
            return await _context.Students.Where(o => o.Id == id).ProjectTo<StudentViewModel>(_mapper.ConfigurationProvider).SingleOrDefaultAsync();
        }
        
        public async Task<StudentViewModel?> GetStudentByEmailAsync(string email)
        {
            return await _context.Students.Where(o => o.Email!.ToLower() == email.ToLower()).ProjectTo<StudentViewModel>(_mapper.ConfigurationProvider).SingleOrDefaultAsync();
            
        }

        public async Task AddStudentAsync(PostStudentViewModel student)
        {
            var NewStudent = _mapper.Map<Students>(student);
            await _context.Students.AddAsync(NewStudent);
        }
       
        public async Task UpdateStudentAsync(int id, PostStudentViewModel student)
        {
            var Student = await _context.Students.FindAsync(id);
            if (student is null) throw new Exception($"We could not find the Student ID:");
        }

        public async Task DeleteStudentAsync(int id)
        {
           var response = await _context.Students.FindAsync(id);
           if(response is not null) _context.Students.Remove(response);

        }

        public async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

    }
}