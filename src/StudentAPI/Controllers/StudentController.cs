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

        public StudentController(IStudentRepository studentRepository)
        {
            student = studentRepository;
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
