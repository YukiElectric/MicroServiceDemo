using MassTransit;
using Newtonsoft.Json;
using SharedModel;
using StudentQueryAPI.Data;

namespace StudentQueryAPI.Common
{
    public class UpdateEventConsumer : IConsumer<StudentMessage>
    {
        private readonly StudentContext _context;

        public UpdateEventConsumer(StudentContext context) {
            _context = context;
        }
        public async Task Consume(ConsumeContext<StudentMessage> context)
        {
            var jsonMessage = JsonConvert.SerializeObject(context.Message);
            Console.WriteLine($"receive update message: {jsonMessage}");
            var std = context.Message;
            try { 
                var findStudent = _context.Students!.SingleOrDefault(s => s.StudentId == std.StudentID);
                if (findStudent != null) {
                    findStudent.StudentCPA = std.StudentCPA;
                    findStudent.StudentAcademy = std.StudentAcademy;
                    findStudent.StudentName = std.StudentName;
                    findStudent.StudentClass = std.StudentClass;
                    _context.Students!.Update(findStudent);
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
