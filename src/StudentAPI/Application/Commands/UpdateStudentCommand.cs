using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using StudentAPI.Data;
using System.ComponentModel.DataAnnotations;

namespace StudentAPI.Application.Commands
{
    public class UpdateStudentCommand : IRequest<bool>
    {
        public int Id { get; set; }
        [MaxLength(100)]
        public string StudentName { get; set; }

        [MaxLength(100)]
        public string StudentClass { get; set; }

        [MaxLength(100)]
        public string StudentAcademy { get; set; }

        [Range(0, 4.0)]
        public double StudentCPA { get; set; } = 0;
        public class UpdateStudentCommandHandler : IRequestHandler<UpdateStudentCommand, bool>
        {
            
            private readonly StudentContext _context;

            public UpdateStudentCommandHandler(StudentContext context) {
                _context = context;
            }
            public async Task<bool> Handle(UpdateStudentCommand request, CancellationToken cancellationToken)
            {
                var findStudent = await _context.Students!.SingleOrDefaultAsync(s => s.StudentId == request.Id);
                if (findStudent == null) return false;
                findStudent.StudentName = request.StudentName;
                findStudent.StudentCPA = request.StudentCPA;
                findStudent.StudentAcademy = request.StudentAcademy;
                findStudent.StudentClass = request.StudentClass;
                _context.Students!.Update(findStudent);
                await _context.SaveChangesAsync();
                return true;
            }
        }
    }
}
