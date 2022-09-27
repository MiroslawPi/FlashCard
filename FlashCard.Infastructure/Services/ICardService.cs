using FlashCard.Core.Domain;
using FlashCard.Infastructure.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlashCard.Core.Repositories
{
    public interface ICardService
    {

        Task<CardReadDto> GetAsync(Guid id);
        Task<IEnumerable<CardReadDto>> BrowseAsync(string name = "");
        Task AddAsync(CardCreateDto cardCreateDto);
        Task UpdateAsync(CardUpdateDto cardUpdateDto);
        Task DeleteAsync(Guid id);
    }
}
