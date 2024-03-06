using MassTransit;
using Microsoft.AspNetCore.Mvc;
using SharedModel;
using StudentAPI.Common;
using StudentAPI.Models;
using StudentAPI.Repositories;

namespace StudentAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IStudentRepository student;
        private readonly IPublishEndpoint _publishEndPoint;
        private readonly IRequestClient<MessageConsumer> _client;

        public StudentController(IStudentRepository studentRepository,IPublishEndpoint publishEndpoint, IRequestClient<MessageConsumer> client)
        {
            student = studentRepository;
            _publishEndPoint = publishEndpoint;
            _client = client;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllStudents(int page, int limit)
        {
            var token = HttpContext.Request.Headers["Authorization"].ToString();
            //var result = await CalllAPI.getAuth("authen", token);
            //await _publishEndPoint.Publish<MessageConsumer>(new {token});
            var response = await _client.GetResponse<MessageConsumer>(new { token });
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
            var result = await CalllAPI.getAuth("authen", token);
            if (result == null)
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
            return BadRequest(result);
        }

        [HttpGet("search")]
        public async Task<IActionResult> GetStudentByCondition(string condition)
        {
            var token = HttpContext.Request.Headers["Authorization"].ToString();
            //var result = await CalllAPI.getAuth("authen", token);
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

        [HttpPost]
        public async Task<IActionResult> AddNewStudent(StudentModel model)
        {
            var token = HttpContext.Request.Headers["Authorization"].ToString();
            var result = await CalllAPI.getAuth("authen", token);
            if (result == null)
            {
                try
                {
                    await student.AddStudentAsync(model);
                    return Ok(new
                    {
                        status = true,
                        message = "Add student to database sucessfully"
                    });
                }
                catch (Exception e)
                {
                    return BadRequest(result);
                }
            }
            return BadRequest(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateStudent(int id, StudentModel model)
        {
            var token = HttpContext.Request.Headers["Authorization"].ToString();
            var result = await CalllAPI.getAuth("authen", token);
            if (result == null)
            {
                try
                {
                    await student.UpdateStudentAsync(id, model);
                    return Ok(new
                    {
                        status = true,
                        message = "Update student to database sucessfully"
                    });
                }
                catch (Exception e)
                {
                    return BadRequest(result);
                }
            }
            return BadRequest(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudent(int id)
        {
            var token = HttpContext.Request.Headers["Authorization"].ToString();
            var result = await CalllAPI.getAuth("authen", token);
            if (result == null)
            {
                try
                {
                    await student.DeleteStudentAsync(id);
                    return Ok(new
                    {
                        status = true,
                        message = "Delete student from database sucessfully"
                    });
                }
                catch (Exception e)
                {
                    return BadRequest(result);
                }
            }
            return BadRequest(result);
        }
    }
}
