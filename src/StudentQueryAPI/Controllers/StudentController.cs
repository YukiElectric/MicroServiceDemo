using MassTransit;
using Microsoft.AspNetCore.Mvc;
using SharedModel;
using StudentQueryAPI.Repositories;

namespace StudentQueryAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IStudentRepository student;
        private readonly IRequestClient<MessageConsumer> _client;
        public StudentController(IStudentRepository studentRepository, IRequestClient<MessageConsumer> requestClient)
        {
            student = studentRepository;
            _client = requestClient;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllStudents(int page, int limit)
        {
            var token = HttpContext.Request.Headers["Authorization"].ToString();
            var response = await _client.GetResponse<MessageConsumer>(new { token });
            page = page <= 0 ? 1 : page;
            limit = limit <= 0 ? 10 : limit;
            if (response.Message.status)
            {
                return Ok(new
                {
                    status = true,
                    filter = new
                    {
                        page,
                        limit,
                    },
                    docs = new
                    {
                        data = await student.GetAllStudentAsync(page, limit),
                    }
                });
            }
            return BadRequest(new
            {
                status = false,
                message = "Permission denied"
            });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetStudentById(int id)
        {
            var token = HttpContext.Request.Headers["Authorization"].ToString();
            var response = await _client.GetResponse<MessageConsumer>(new { token });
            if (response.Message.status)
            {
                var found = await student.GetStudentByIDAsync(id);
                return found == null ? NotFound(new
                {
                    status = false,
                    message = "Cann't not found this id"
                }) : Ok(new
                {
                    status = true,
                    data = found
                });
            }
            return BadRequest(new
            {
                status = false,
                message = "Permission denied"
            });
        }

        [HttpGet("search")]
        public async Task<IActionResult> GetStudentByCondition(string condition)
        {
            var token = HttpContext.Request.Headers["Authorization"].ToString();
            var response = await _client.GetResponse<MessageConsumer>(new { token });
            if (response.Message.status)
            {
                var found = await student.GetAllStudentByCondition(condition);
                return found == null ? NotFound(new
                {
                    status = false,
                    message = "Cann't not found this condition"
                }) : Ok(new
                {
                    status = true,
                    data = found
                });
            }
            return BadRequest(new
            {
                status = false,
                message = "Permission denied"
            });
        }
    }
}
