using AutoMapper;
using MediatR;
using StudentAPI.Data;
using StudentAPI.Models;
using System.ComponentModel.DataAnnotations;

namespace StudentAPI.Application.Commands
{
    public class CreateStudentCommand : IRequest<int>
    {
        [MaxLength(100)]
        public string StudentName { get; set; }

        [MaxLength(100)]
        public string StudentClass { get; set; }

        [MaxLength(100)]
        public string StudentAcademy { get; set; }

        [Range(0, 4.0)]
        public double StudentCPA { get; set; } = 0;
        public class CreateStudentCommandHandler : IRequestHandler<CreateStudentCommand, int>
        {
            private readonly StudentContext _context;
            private readonly IMapper _mapper;

            public CreateStudentCommandHandler(IMapper mapper, StudentContext context) {
                _context = context;
                _mapper = mapper;
            }
            public async Task<int> Handle(CreateStudentCommand request, CancellationToken cancellationToken)
            {
                try
                {
                    var newStudent = _mapper.Map<Student>(request);
                    _context.Students!.Add(newStudent);
                    await _context.SaveChangesAsync();
                    return newStudent.StudentId;
                }
                catch (Exception ex)
                {
                    return 0;
                }
            }
        }
    }
}
