using StudentQueryAPI.Models;

namespace StudentQueryAPI.Repositories
{
    public interface IStudentRepository
    {
        public Task<List<StudentModel>> GetAllStudentAsync(int page, int limit);
        public Task<StudentModel> GetStudentByIDAsync(int id);
        public Task<List<StudentModel>> GetAllStudentByCondition(string cojndition = "");
    }
}
