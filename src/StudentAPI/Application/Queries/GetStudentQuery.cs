using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using StudentAPI.Data;
using StudentAPI.Models;

namespace StudentAPI.Application.Queries
{
    public class GetStudentQuery : IRequest<StudentModel>
    {
        public int Id { get; set; }
        public class GetStudentQueryHandler : IRequestHandler<GetStudentQuery, StudentModel>
        {
            private readonly StudentContext _context;
            private readonly IMapper _mapper;

            public GetStudentQueryHandler(StudentContext context, IMapper mapper) {
                _context = context;
                _mapper = mapper;
            }
            public async Task<StudentModel> Handle(GetStudentQuery request, CancellationToken cancellationToken)
            {
                var student = await _context.Students!.FindAsync(request.Id);
                return _mapper.Map<StudentModel>(student);
            }
        }
    }
}
