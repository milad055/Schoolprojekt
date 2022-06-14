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

        [HttpGet("list")]
        public async Task<ActionResult<List<CourseViewModel>>> ListCourses()
        {
            // Anropa metoden ListAllVehiclesAsync i vårt repository.
            return Ok(await _CourseRepo.ListAllCoursesAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CourseViewModel>> GetCourseById(int id)
        {
            // Anropa metoden GetVehiclesAsync i vårt repository
            var response = await _CourseRepo.GetCourseById(id);

            // Kontroller om vi inte har fått någon bil eller fordon tillbaka
            if (response is null)
                // I så fall returnera ett 404 NotFound meddelande
                return NotFound($"We could not find any corse with id: {id}");

            // Annars returnera bilen eller fordonet
            return Ok(response);
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
        }

        // Update/Put/Patch functions...
        [HttpPut("{id}")]
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