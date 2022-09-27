using FlashCard.Core.Domain;
using FlashCard.Infastructure.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlashCard.Core.Repositories
{
    public interface ICourseService
    {

        Task<CourseReadDto> GetAsync(Guid id);
        Task<IEnumerable<CourseReadDto>> BrowseAsync(string name = "");
        Task AddAsync(CourseCreateDto courseCreateDto);
        Task UpdateAsync(CourseUpdateDto courseUpdateDto);
        Task DeleteAsync(Guid id);

    }
}
