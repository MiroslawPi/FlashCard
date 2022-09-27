using FlashCard.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlashCard.Core.Repositories
{
    public interface ICardRepository
    {

        Task<Card> GetAsync(Guid id);
        Task<IEnumerable<Card>> BrowseAsync(string name = "");
        Task AddAsync(Card card);
        Task UpdateAsync(Card card);
        Task DeleteAsync(Card card);
        
    }
}
