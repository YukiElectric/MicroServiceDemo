using MediatR;
using StudentAPI.Data;

namespace StudentAPI.Application.Commands
{
    public class DeleteStudentCommad : IRequest<bool>
    {
        public int Id { get; set; }
        public class DeleteStudentCommadHandler : IRequestHandler<DeleteStudentCommad, bool>
        {
            private readonly StudentContext _context;

            public DeleteStudentCommadHandler(StudentContext context) {
                _context = context;
            }
            public async Task<bool> Handle(DeleteStudentCommad request, CancellationToken cancellationToken)
            {
                var deleteStudent = _context.Students!.SingleOrDefault(s => s.StudentId == request.Id);
                if (deleteStudent == null) return false;
                _context.Students!.Remove(deleteStudent);
                await _context.SaveChangesAsync();
                return true;
            }
        }
    }
}
