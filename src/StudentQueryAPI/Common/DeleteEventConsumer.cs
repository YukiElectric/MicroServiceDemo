using MassTransit;
using Newtonsoft.Json;
using SharedModel;
using StudentQueryAPI.Data;

namespace StudentQueryAPI.Common
{
    public class DeleteEventConsumer : IConsumer<StudentMessage>
    {
        private readonly StudentContext _context;

        public DeleteEventConsumer(StudentContext context) {
            _context = context;
        }
        public async Task Consume(ConsumeContext<StudentMessage> context)
        {
            var jsonMessage = JsonConvert.SerializeObject(context.Message);
            Console.WriteLine($"receive delete message: {jsonMessage}");
            var std = context.Message;
            try
            {
                var deleteStudent = _context.Students!.SingleOrDefault(s => s.StudentId == std.StudentID);
                _context.Students!.Remove(deleteStudent);
                await _context.SaveChangesAsync();
            }catch (Exception ex)
            {
                throw;
            }
        }
    }
}
