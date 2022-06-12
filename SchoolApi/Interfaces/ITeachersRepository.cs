using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SchoolApi.ViewModels.TeachersView;

namespace SchoolApi.Interfaces
{
    public interface ITeachersRepository
    {
        public Task<List<TeacherViewModel>> ListAllTeachersAsync();
        public Task<TeacherViewModel?> GetTeacherAsync(int id);
        public Task<TeacherViewModel?> GetTeacherByEmailAsync(string Email);
        public Task AddTeacherAsync (PostTeacherViewModel teacher);
        public Task UpdateTeacherAsync (int id, PostTeacherViewModel teacher);
        public Task DeleteTeacherAsync(int id);
        public Task<bool> SaveAllAsync();
        
    }
}