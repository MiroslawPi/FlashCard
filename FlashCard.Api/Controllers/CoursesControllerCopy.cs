using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using FlashCard.Infastructure.Data;
using FlashCard.Infastructure.Dto;
using FlashCard.Core.Domain;

namespace FlashCard.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoursesControllerCopy : ControllerBase
    {
        private readonly FlashCardDbContext _context;
        private readonly IMapper mapper;

        public CoursesControllerCopy(FlashCardDbContext context, IMapper mapper)
        {
            _context = context;
            this.mapper = mapper;
        }

        // GET: api/Courses/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CourseReadDto>> GetCourse(Guid id)
        {
            var course = await _context.Courses.FindAsync(id);

            if (course == null)
            {
                return NotFound();
            }

            //return course;
            var courseDto = mapper.Map<CourseReadDto>(course);
            return Ok(courseDto);
        }

        // GET: api/Courses
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CourseReadDto>>> GetCourses()
        {
            //return await _context.Courses.ToListAsync();

            //var courses = await _context.Courses.Include(x => x.Cards).ToListAsync();
            //var coursesDto = mapper.Map<IEnumerable<CourseReadDto>>(courses);
            //return Ok(coursesDto)

            var coursesDto = await _context.Courses
                          .Include(x => x.Cards)
                          .ProjectTo<CourseReadDto>(mapper.ConfigurationProvider).ToListAsync();
;            
            return Ok(coursesDto);
        }

        // POST: api/Courses
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        //public async Task<ActionResult<Course>> PostCourse(Course course)
        public async Task<ActionResult<CourseCreateDto>> PostCourse(CourseCreateDto courseDto)
        {
            //_context.Courses.Add(course);

            var course = mapper.Map<Course>(courseDto);
            await _context.Courses.AddAsync(course);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                //if (CourseExists(course.Id))
                if (await CourseExists(course.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            //return CreatedAtAction("GetCourse", new { id = course.Id }, course);
            return CreatedAtAction(nameof(GetCourse), new { id = course.Id }, course);
        }

        // PUT: api/Courses/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        //public async Task<IActionResult> PutCourse(Guid id, Course course)
        public async Task<IActionResult> PutCourse(Guid id, CourseUpdateDto courseUpdateDto)
        {
            if (id != courseUpdateDto.Id)
            {
                return BadRequest();
            }

            var course = await _context.Courses.FindAsync(id);

            if (course == null)
            {
                return NotFound();
            }

            mapper.Map(courseUpdateDto, course);

            _context.Entry(course).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                //if (!CourseExists(id))
                if (!await CourseExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(NoContent());
        }

        // DELETE: api/Courses/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCourse(Guid id)
        {
            var course = await _context.Courses.FindAsync(id);
            if (course == null)
            {
                return NotFound();
            }

            _context.Courses.Remove(course);
            await _context.SaveChangesAsync();

            return Ok(NoContent());
        }

        //private bool CourseExists(Guid id)
        private async Task<bool> CourseExists(Guid id)
        {
            //return _context.Courses.Any(e => e.Id == id)
            return await _context.Courses.AnyAsync(e => e.Id == id);
        }
    }
}
