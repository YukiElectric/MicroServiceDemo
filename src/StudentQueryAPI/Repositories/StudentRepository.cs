using AutoMapper;
using Microsoft.EntityFrameworkCore;
using StudentQueryAPI.Data;
using StudentQueryAPI.Models;

namespace StudentQueryAPI.Repositories
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

        public async Task<List<StudentModel>> GetAllStudentAsync(int page, int limit)
        {
            var skip = limit * (page - 1);
            var students = await context.Students!.Take(limit).Skip(skip).ToListAsync();
            return _mapper.Map<List<StudentModel>>(students);
        }
    }
}
