using FlashCard.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlashCard.Core.Repositories
{
    public interface ICourseRepository
    {

        Task<Course> GetAsync(Guid id);
        Task<IEnumerable<Course>> BrowseAsync(string name = "");
        Task AddAsync(Course course);
        Task DeleteAsync(Course course);
        Task UpdateAsync(Course course);
    }
}
