using StudentAPI.Models;

namespace StudentAPI.Repositories
{
    public interface IStudentRepository
    {
        public Task AddStudentAsync(StudentModel model);
        public Task UpdateStudentAsync(int id, StudentModel model);
        public Task DeleteStudentAsync(int id);
    }
}
