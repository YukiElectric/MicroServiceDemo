using MassTransit;
using Newtonsoft.Json;
using SharedModel;
using StudentQueryAPI.Data;

namespace StudentQueryAPI.Common
{
    public class CreateEventConsumer : IConsumer<StudentMessage>
    {
        private readonly StudentContext _context;

        public CreateEventConsumer(StudentContext context)
        {
            _context = context;
        }
        public async Task Consume(ConsumeContext<StudentMessage> context)
        {
            var jsonMessage = JsonConvert.SerializeObject(context.Message);
            Console.WriteLine($"receive create message: {jsonMessage}");
            var s = context.Message;
            Student newStudent = new (){ 
                StudentName = s.StudentName,
                StudentAcademy = s.StudentAcademy,
                StudentClass = s.StudentClass,
                StudentCPA = s.StudentCPA
            };
            _context.Students!.Add(newStudent);
            await _context.SaveChangesAsync();
        }
    }
}
