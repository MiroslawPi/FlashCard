using AutoMapper;
using FlashCard.Core.Domain;
using FlashCard.Core.Repositories;
using FlashCard.Infastructure.Data;
using FlashCard.Infastructure.Dto;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlashCard.Infastructure.Repositories
{
    public class CourseRepository : ICourseRepository
    {
        private readonly FlashCardDbContext _context;
        public CourseRepository(FlashCardDbContext context)
        {
            _context = context;
        }
        public async Task<Course> GetAsync(Guid id)
        {
            //return await _context.Courses.SingleOrDefaultAsync(x => x.Id == id);
            var course = await _context.Courses.FindAsync(id);
            return course;
        }

        public async Task<IEnumerable<Course>> BrowseAsync(string name = "")
        {
            if (!string.IsNullOrWhiteSpace(name))
            {
                //var courses = await _context.Courses
                //                .Where(x => x.Name.ToLowerInvariant()
                //                .Contains(name.ToLowerInvariant())).Include(x => x.Cards).ToListAsync();                
                var courses = await _context.Courses
                                .Where(x => x.Name.ToLower().Contains(name.ToLower()))
                                .Include(x => x.Cards).ToListAsync();
                return courses;
            }
            
            return await _context.Courses.Include(x => x.Cards).ToListAsync();
            
        }
        public async Task AddAsync(Course course)
        {
            await _context.AddAsync(course);
            await _context.SaveChangesAsync();
            await Task.CompletedTask;
        }

        public async Task UpdateAsync(Course course)
        {

            await _context.SaveChangesAsync();
            await Task.CompletedTask;

        }
        public async Task DeleteAsync(Course course)
        {
            _context.Courses.Remove(course);
            await _context.SaveChangesAsync();
            await Task.CompletedTask;
        }


    }
}
