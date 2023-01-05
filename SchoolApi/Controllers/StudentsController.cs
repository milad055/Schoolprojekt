using Microsoft.AspNetCore.Mvc;
using SchoolApi.Interfaces;
using SchoolApi.ViewModels.StudentsView;

namespace SchoolApi.Controllers
{
    [ApiController]
    [Route("api/students")]
    public class StudentsController : ControllerBase
    {
        private readonly IStudentsRepository _studentRepo;
        public StudentsController(IStudentsRepository studentRepo)
        {
            _studentRepo = studentRepo;
        }


        [HttpGet("liststudents")]
        public async Task<ActionResult<List<StudentViewModel>>> ListStudents()
        {
            
            return Ok(await _studentRepo.ListAllStudentsAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<StudentViewModel>> GetStudentById(int id)
        {
            var response = await _studentRepo.GetStudentAsync(id);
            if (response is null) return NotFound($"We couldn't find the student ID: {id}");
            return Ok(response);
        }

        [HttpGet("byemail/{email}")]
        public async Task<ActionResult<StudentViewModel>> GetStudentByEmail(string email)
        {
            var response = await _studentRepo.GetStudentByEmailAsync(email);
            if (response is null) return NotFound($"We couldn't find the student Email: {email}");
            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult> AddStudentAsync(PostStudentViewModel student)
        {
            try
            {
                if (await _studentRepo.GetStudentByEmailAsync(student.Email!) is not null)
                    return BadRequest($"Student email {student.Email} already exists");

                await _studentRepo.AddStudentAsync(student);

                if (await _studentRepo.SaveAllAsync()) return StatusCode(201);
                return StatusCode(500, "The Student could not be saved...");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateStudent(int id, PostStudentViewModel model)
        {
            try
            {
                await _studentRepo.UpdateStudentAsync(id, model);
                if (await _studentRepo.SaveAllAsync()) return NoContent();

                return StatusCode(500, $"An error has occured when updating student ID: {id}");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteStudentById(int id)
        {
            try
            {
                await _studentRepo.DeleteStudentAsync(id);
                if (await _studentRepo.SaveAllAsync()) return NoContent();

                return StatusCode(500, $"Something went wrong when trying to delete Student ID: {id}");
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }



    }
}