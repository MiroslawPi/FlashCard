using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using FlashCard.Infastructure.Data;
using FlashCard.Infastructure.Dto;
using FlashCard.Core.Domain;
using FlashCard.Core.Repositories;
using FlashCard.Infastructure.Services;
using System.Xml.Linq;

namespace FlashCard.Api.Controllers
{
    //[Route("api/[controller]")]
    [Route("[controller]")]
    [ApiController]
    public class CoursesController : ControllerBase
    {
        private readonly ICourseService _courseService;

        public CoursesController(ICourseService courseService)
        {
            _courseService = courseService;
        }

        // GET: api/Courses/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CourseReadDto>> GetCourse(Guid id)
        {
            var couseReadDto = await _courseService.GetAsync(id);

            if (couseReadDto == null)
            {
                return NotFound();
            }

            return Ok(couseReadDto);
        }

        // GET: api/Courses
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CourseReadDto>>> GetCourses(string? name)
        {
            var courseReadDtos = await _courseService.BrowseAsync(name);
            return Ok(courseReadDtos);
        }

        // POST: api/Courses
        [HttpPost]
        public async Task<ActionResult<CourseCreateDto>> PostCourse(CourseCreateDto courseCreateDto)
        {
            await _courseService.AddAsync(courseCreateDto);

            //return CreatedAtAction(nameof(GetCard), new { id = card.Id }, card);
            return Ok(NoContent());
        }

        // PUT: api/Courses/5
        //[HttpPut("{id}")]
        [HttpPut]
        //public async Task<IActionResult> PutCourse(Guid id, CourseUpdateDto courseUpdateDto)
        public async Task<IActionResult> PutCourse(CourseUpdateDto courseUpdateDto)
        {
            //if (id != courseUpdateDto.Id)
            //{
            //    return BadRequest();
            //}

            //var cardReadDto = await _courseService.GetAsync(id);
            var cardReadDto = await _courseService.GetAsync(courseUpdateDto.Id);

            if (cardReadDto == null)
            {
                return NotFound();
            }

            await _courseService.UpdateAsync(courseUpdateDto);

            return Ok(NoContent());
        }

        // DELETE: api/Courses/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCourse(Guid id)
        {
            var courseReadDto = await _courseService.GetAsync(id);
            if (courseReadDto == null)
            {
                return NotFound();
            }

            await _courseService.DeleteAsync(id);

            return NoContent();
        }

    }
}
