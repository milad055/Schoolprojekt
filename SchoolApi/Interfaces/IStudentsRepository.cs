using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SchoolApi.ViewModels.StudentsView;

namespace SchoolApi.Interfaces
{
    public interface IStudentsRepository
    {
        public Task<List<StudentViewModel>> ListAllStudentsAsync();
        public Task<StudentViewModel?> GetStudentAsync(int id);
        public Task<StudentViewModel?> GetStudentByEmailAsync(string Email);
        public Task AddStudentAsync (PostStudentViewModel student);
        public Task UpdateStudentAsync (int id, PostStudentViewModel student);
        public Task DeleteStudentAsync(int id);
        public Task<bool> SaveAllAsync();

        
    }
}