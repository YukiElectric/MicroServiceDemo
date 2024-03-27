using MassTransit;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SharedModel;
using StudentAPI.Application.Commands;
using StudentAPI.Application.Queries;
using StudentAPI.Common;

namespace StudentAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IRequestClient<MessageConsumer> _client;
        private readonly IMediator _mediator;

        public StudentController(IRequestClient<MessageConsumer> client, IMediator mediator)
        {
            _client = client;
            _mediator = mediator;
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
                        data = await _mediator.Send(new GetAllStudentQuery { page = page, limit = limit}),
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
                var found = await _mediator.Send(new GetStudentQuery { Id = id });
                Console.WriteLine(found == null);
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
                var found = await _mediator.Send(new SearchStudentQuery { condition = condition });
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

        [HttpPost]
        public async Task<IActionResult> AddNewStudent(CreateStudentCommand command)
        {
            var token = HttpContext.Request.Headers["Authorization"].ToString();
            var response = await _client.GetResponse<MessageConsumer>(new { token });
            if (response.Message.status)
            {
                var newStudent = await _mediator.Send(command);
                if(newStudent != 0)
                {
                    return Ok(new
                    {
                        status = true,
                        message = $"new student with id {newStudent} create sucessfully"
                    });
                }
                return BadRequest(new
                {
                    status = false,
                    message = "Has error to create new student"
                });
            }
            return BadRequest(new
            {
                status = false,
                message = "Permission denied"
            });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateStudent(int id, UpdateStudentCommand command)
        {
            var token = HttpContext.Request.Headers["Authorization"].ToString();
            var response = await _client.GetResponse<MessageConsumer>(new { token });
            if (response.Message.status)
            {
                command.Id = id;
                var updateStudent = await _mediator.Send(command);
                if (updateStudent) return Ok(new
                {
                    status = true,
                    message = $"Update student with id {id} successfully"
                });
                return BadRequest(new
                {
                    status = false,
                    message = "Can't find student or has error when update"
                });
            }
            return BadRequest(new
            {
                status = false,
                message = "Permission denied"
            });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudent(int id)
        {
            var token = HttpContext.Request.Headers["Authorization"].ToString();
            var response = await _client.GetResponse<MessageConsumer>(new { token });
            if (response.Message.status)
            {
                var deleteStudent = await _mediator.Send(new DeleteStudentCommad { Id = id });
                if (deleteStudent) return Ok(new { 
                    status = true,
                    message = $"Delete student with id {id} successfully"
                });
                return BadRequest(new
                {
                    status = false,
                    message = "Can't find student or has error when delete"
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
