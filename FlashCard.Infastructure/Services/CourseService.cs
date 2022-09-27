using AutoMapper;
using FlashCard.Core.Domain;
using FlashCard.Core.Repositories;
using FlashCard.Infastructure.Dto;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlashCard.Infastructure.Services
{
    public class CourseService : ICourseService
    {
        private readonly ICourseRepository _courseRepository;
        private readonly IMapper _mapper;

        public CourseService(ICourseRepository courseRepository, IMapper mapper)
        {
            _courseRepository = courseRepository;
            _mapper = mapper;
        }
        public async Task<CourseReadDto> GetAsync(Guid id)
        {
            var course = await _courseRepository.GetAsync(id);
            var courseDto = _mapper.Map<CourseReadDto>(course);
            return courseDto;
        }
        public async Task<IEnumerable<CourseReadDto>> BrowseAsync(string name = "")
        {
            var courses = await _courseRepository.BrowseAsync(name);
            var coursesDto = _mapper.Map<IEnumerable<CourseReadDto>>(courses);

            return coursesDto;
        }

        public async Task AddAsync(CourseCreateDto courseCreateDto)
        {
            // var course = new Course()
            //{
            //    Name = courseCreateDto.Name
            //};
            var course = _mapper.Map<Course>(courseCreateDto);
            await _courseRepository.AddAsync(course);
        }
        public async Task UpdateAsync(CourseUpdateDto courseUpdateDto)
        {
            var course = await _courseRepository.GetAsync(courseUpdateDto.Id);
            _mapper.Map(courseUpdateDto, course);
            await _courseRepository.UpdateAsync(course);
        }
        public async Task DeleteAsync(Guid id)
        {
            var course = await _courseRepository.GetAsync(id);
            await _courseRepository.DeleteAsync(course);
        }

        
    }
}
