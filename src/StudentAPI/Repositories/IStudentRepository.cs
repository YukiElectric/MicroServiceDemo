using StudentAPI.Models;

namespace StudentAPI.Repositories
{
    public interface IStudentRepository
    {
        public Task<List<StudentModel>> GetAllStudentAsync(int page = 1, int limit = 10);
        public Task<StudentModel> GetStudentByIDAsync(int id);
        public Task<List<StudentModel>> GetAllStudentByCondition(string cojndition = "");
        public Task AddStudentAsync(StudentModel model);
        public Task UpdateStudentAsync(int id, StudentModel model);
        public Task DeleteStudentAsync(int id);
    }
}
