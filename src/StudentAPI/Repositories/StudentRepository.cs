using AutoMapper;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using SharedModel;
using StudentAPI.Data;
using StudentAPI.Models;

namespace StudentAPI.Repositories
{
    public class StudentRepository : IStudentRepository
    {
        private readonly StudentContext context;
        private readonly IMapper _mapper;
        private readonly ISendEndpointProvider _sendEndPoint;

        public StudentRepository(StudentContext studentContext, IMapper mapper, ISendEndpointProvider sendEndpoint)
        {
            context = studentContext;
            _mapper = mapper;
            _sendEndPoint = sendEndpoint;
        }
        public async Task AddStudentAsync(StudentModel model)
        {
            var newStudent = _mapper.Map<Student>(model);
            context.Students!.Add(newStudent);
            await context.SaveChangesAsync();
            var endPoint = await _sendEndPoint.GetSendEndpoint(new Uri("exchange:create-student-request"));
            await endPoint.Send<StudentMessage>(model);
        }

        public async Task UpdateStudentAsync(int id, StudentModel model)
        {
            var findStudent = context.Students!.SingleOrDefault(s => s.StudentId == id);
            if (findStudent != null)
            {
                findStudent.StudentCPA = model.StudentCPA;
                findStudent.StudentAcademy = model.StudentAcademy;
                findStudent.StudentName = model.StudentName;
                findStudent.StudentClass = model.StudentClass;
                context.Students!.Update(findStudent);
                await context.SaveChangesAsync();
            }
            var endPoint = await _sendEndPoint.GetSendEndpoint(new Uri("exchange:update-student-request"));
            await endPoint.Send<StudentMessage>(new
            {
                StudentID = id,
                model.StudentName,
                model.StudentCPA,
                model.StudentClass,
                model.StudentAcademy
            });
        }


        public async Task DeleteStudentAsync(int id)
        {
            var deleteStudent = context.Students!.SingleOrDefault(s => s.StudentId == id);
            context.Students!.Remove(deleteStudent);
            await context.SaveChangesAsync();
            var endPoint = await _sendEndPoint.GetSendEndpoint(new Uri("exchange:delete-student-request"));
            await endPoint.Send<StudentMessage>(new {StudentID = id});
        }
    }
}
