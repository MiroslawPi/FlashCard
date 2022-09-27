using FlashCard.Core.Domain;
using FlashCard.Core.Repositories;
using FlashCard.Infastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace FlashCard.Infastructure.Repositories
{
    public class CardRepository : ICardRepository
    {
        private readonly FlashCardDbContext _context;
        public CardRepository(FlashCardDbContext context)
        {
            _context = context;
        }
        public async Task<Card> GetAsync(Guid id)
        {
            //return await _context.Courses.SingleOrDefaultAsync(x => x.Id == id);
            var card = await _context.Cards.FindAsync(id);
            return card;
        }

        public async Task<IEnumerable<Card>> BrowseAsync(string name = "")
        {
            if (!string.IsNullOrWhiteSpace(name))
            {

                var cards = await _context.Cards
                                .Where( x => 
                                    x.FrontWord.ToLower().Contains(name.ToLower())
                                 || x.BackWord.ToLower().Contains(name.ToLower())
                                )
                                .ToListAsync();
                return cards;
            }

            return await _context.Cards.ToListAsync();

        }
        public async Task AddAsync(Card card)
        {
            await _context.AddAsync(card);
            await _context.SaveChangesAsync();
            await Task.CompletedTask;
        }

        public async Task UpdateAsync(Card card)
        {

            await _context.SaveChangesAsync();
            await Task.CompletedTask;

        }
        public async Task DeleteAsync(Card card)
        {
            _context.Cards.Remove(card);
            await _context.SaveChangesAsync();
            await Task.CompletedTask;
        }


    }
}
