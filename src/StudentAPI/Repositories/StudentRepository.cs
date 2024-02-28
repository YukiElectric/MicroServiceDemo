using AutoMapper;
using Microsoft.EntityFrameworkCore;
using StudentAPI.Data;
using StudentAPI.Models;

namespace StudentAPI.Repositories
{
    public class StudentRepository : IStudentRepository
    {
        private readonly StudentContext context;
        private readonly IMapper _mapper;

        public StudentRepository(StudentContext studentContext, IMapper mapper)
        {
            context = studentContext;
            _mapper = mapper;
        }
        public async Task AddStudentAsync(StudentModel model)
        {
            var newStudent = _mapper.Map<Student>(model);
            context.Students!.Add(newStudent);
            await context.SaveChangesAsync();
        }

        public async Task DeleteStudentAsync(int id)
        {
            var deleteStudent = context.Students!.SingleOrDefault(s => s.StudentId == id);
            context.Students!.Remove(deleteStudent);
            await context.SaveChangesAsync();
        }

        public async Task<List<StudentModel>> GetAllStudentAsync(int page = 1, int limit = 10)
        {
            var skip = limit * (page - 1);
            var students = await context.Students!.Take(limit).Skip(skip).ToListAsync();
            return _mapper.Map<List<StudentModel>>(students);
        }

        public async Task<List<StudentModel>> GetAllStudentByCondition(string condition = "")
        {
            if (string.IsNullOrWhiteSpace(condition))
            {
                var students = await context.Students!.ToListAsync();
                return _mapper.Map<List<StudentModel>>(students);
            }
            var student = await context.Students!.Where(e =>
                EF.Property<String>(e, "StudentClass").Contains(condition) ||
                EF.Property<String>(e, "StudentName").Contains(condition) ||
                EF.Property<String>(e, "StudentAcademy").Contains(condition) ||
                EF.Property<Double>(e, "StudentCPA").Equals(condition)
            ).ToListAsync();
            return _mapper.Map<List<StudentModel>>(student);
        }

        public async Task<StudentModel> GetStudentByIDAsync(int id)
        {
            var student = await context.Students!.FindAsync(id);
            return _mapper.Map<StudentModel>(student);
        }

        public async Task UpdateStudentAsync(int id, StudentModel model)
        {
            var findStudent = context.Students!.SingleOrDefault(s => s.StudentId == id);
            var updateStudent = _mapper.Map<Student>(model);
            context.Students!.Update(updateStudent);
            await context.SaveChangesAsync();
        }
    }
}
