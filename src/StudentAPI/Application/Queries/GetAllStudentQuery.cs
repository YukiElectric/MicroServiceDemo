using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using StudentAPI.Data;
using StudentAPI.Models;

namespace StudentAPI.Application.Queries
{
    public class GetAllStudentQuery : IRequest<IEnumerable<StudentModel>>
    {
        public int page { get; set; }
        public int limit { get; set; }
        public class GetAllStudentQueryHandler : IRequestHandler<GetAllStudentQuery, IEnumerable<StudentModel>>
        {
            private readonly StudentContext _context;
            private readonly IMapper _mapper;

            public GetAllStudentQueryHandler(StudentContext context, IMapper mapper) {
                _context = context;
                _mapper = mapper;
            }
            public async Task<IEnumerable<StudentModel>> Handle(GetAllStudentQuery request, CancellationToken cancellationToken)
            {
                var skip = request.limit * (request.page - 1);
                var students = await _context.Students!.Take(request.limit).Skip(skip).ToListAsync();
                return _mapper.Map<List<StudentModel>>(students);
            }
        }
    }
}
