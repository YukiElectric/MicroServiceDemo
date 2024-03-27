using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using StudentAPI.Data;
using StudentAPI.Models;

namespace StudentAPI.Application.Queries
{
    public class SearchStudentQuery : IRequest<IEnumerable<StudentModel>>
    {
        public string condition { get; set; }
        public class SearchStudentQueryHandler : IRequestHandler<SearchStudentQuery, IEnumerable<StudentModel>>
        {
            private readonly StudentContext _context;
            private readonly IMapper _mapper;

            public SearchStudentQueryHandler(StudentContext context, IMapper mapper) {
                _context = context;
                _mapper = mapper;
            }
            public async Task<IEnumerable<StudentModel>> Handle(SearchStudentQuery query, CancellationToken cancellationToken)
            {
                if (string.IsNullOrWhiteSpace(query.condition))
                {
                    var students = await _context.Students!.ToListAsync();
                    return _mapper.Map<List<StudentModel>>(students);
                }
                var student = await _context.Students!.Where(e =>
                    EF.Property<String>(e, "StudentClass").Contains(query.condition) ||
                    EF.Property<String>(e, "StudentName").Contains(query.condition) ||
                    EF.Property<String>(e, "StudentAcademy").Contains(query.condition) ||
                    EF.Property<Double>(e, "StudentCPA").Equals(query.condition)
                ).ToListAsync();
                return _mapper.Map<List<StudentModel>>(student);
            }
        }
    }
}
