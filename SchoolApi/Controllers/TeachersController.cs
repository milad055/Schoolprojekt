using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SchoolApi.Interfaces;
using SchoolApi.ViewModels.TeachersView;

namespace SchoolApi.Controllers
{
    [ApiController]
    [Route("api/teachers")]
    public class TeachersController : ControllerBase
    {
        private readonly ITeachersRepository _teacherRepo;

        public TeachersController(ITeachersRepository teacherRepo)
        {
            _teacherRepo = teacherRepo;
        }


        [HttpGet("listteachers")]
        public async Task<ActionResult<List<TeacherViewModel>>> ListTeachers()
        {
            // Anropa metoden ListAllVehiclesAsync i vårt repository.
            return Ok(await _teacherRepo.ListAllTeachersAsync());
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<TeacherViewModel>> GetTeacherById(int id)
        {
            var response = await _teacherRepo.GetTeacherAsync(id);
            if (response is null) return NotFound($"We couldn't find the teachers ID: {id}");
            return Ok(response);
        }


        [HttpGet("byemail/{email}")]
        public async Task<ActionResult<TeacherViewModel>> GetTeacherByEmail(string email)
        {
            var response = await _teacherRepo.GetTeacherByEmailAsync(email);
            if (response is null) return NotFound($"We couldn't find the teachers Email: {email}");
            return Ok(response);
        }


        [HttpPost]
        public async Task<ActionResult> AddTeacherAsync(PostTeacherViewModel teacher)
        {
            try
            {
                if (await _teacherRepo.GetTeacherByEmailAsync(teacher.Email!) is not null)
                    return BadRequest($"Teacher email {teacher.Email} already exists");

                await _teacherRepo.AddTeacherAsync(teacher);

                if (await _teacherRepo.SaveAllAsync()) return StatusCode(201);
                return StatusCode(500, "The teacher could not be saved...");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTeacher(int id, PostTeacherViewModel model)
        {
            try
            {
                await _teacherRepo.UpdateTeacherAsync(id, model);
                if (await _teacherRepo.SaveAllAsync()) return NoContent();

                return StatusCode(500, $"An error has occured when updating teacher ID: {id}");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteTeacherById(int id)
        {
            try
            {
                await _teacherRepo.DeleteTeacherAsync(id);
                if (await _teacherRepo.SaveAllAsync()) return NoContent();

                return StatusCode(500, $"Something went wrong when trying to delete teacher ID: {id}");
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

    }
}