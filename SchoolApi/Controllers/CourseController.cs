using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using schoolApi.Models;
using SchoolApi.Data;
using SchoolApi.Interfaces;
using SchoolApi.ViewModels;

namespace schoolApi.Controllers
{
    [ApiController]
    [Route("api/courses")]
    public class CourseController : ControllerBase
    {
        public ICourseRepository _CourseRepo;
        public CourseController(ICourseRepository courseRepo)
        {
            _CourseRepo = courseRepo;

        }

        [HttpGet()]
        public async Task<ActionResult<List<CourseViewModel>>> ListCourses()
        {
            // Anropa metoden ListAllVehiclesAsync i vårt repository.
            return Ok(await _CourseRepo.ListAllCoursesAsync());
        }

        [HttpGet("courseNumber/{courseNum}")]
        public async Task<ActionResult<CourseViewModel>> GetCourseByNumber(int courseNum)
        {
            var response = await _CourseRepo.GetCourseByNumberAsync(courseNum);
            if (response is null) return NotFound($"We couldn't find the course number: {courseNum}");
            return Ok(response);
        }

        [HttpPost()]
        public async Task<ActionResult> AddCourseAsync(PostCourseViewModel model)
        {
            try
            {
                if (await _CourseRepo.GetCourseByNumberAsync(model.CourseNumber) is not null)
                    return BadRequest($"Course number {model.CourseNumber} already exists!");

                await _CourseRepo.AddCourseAsync(model);

                if (await _CourseRepo.SaveAllAsync()) return StatusCode(201);
                return StatusCode(500, "The course could not be saved...");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
            // Interesting note
            //I couldn't create a course number that started with 0. 
            //001 didn't work
        }

        // Update/Put/Patch functions.........................................
        [HttpPut("{id}")]
        //Question. How do I update a course by searching for the title?
        //  query .Where(c => c.Title == title) didn't work. 
        public async Task<IActionResult> UpdateCourse(int id, PostCourseViewModel model)
        {
            try
            {
                await _CourseRepo.UpdateCourseAsync(id, model);
                if (await _CourseRepo.SaveAllAsync()) return NoContent();

                return StatusCode(500, $"An error has occured when updating Course ID: {id}");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCourse(int id)
        {
            try
            {
                await _CourseRepo.DeleteCourseAsync(id);
                if (await _CourseRepo.SaveAllAsync()) return NoContent();

                return StatusCode(500, $"Something went wrong when trying to delete Coures ID: {id}");
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }
    }
}