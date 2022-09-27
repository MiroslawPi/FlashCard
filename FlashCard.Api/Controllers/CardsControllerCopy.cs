using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using FlashCard.Infastructure.Data;
using FlashCard.Infastructure.Dto;
using FlashCard.Core.Domain;

namespace FlashCard.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CardsControllerCopy : ControllerBase
    {
        private readonly FlashCardDbContext _context;
        private readonly IMapper _mapper;

        public CardsControllerCopy(FlashCardDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Cards/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CardReadDto>> GetCard(Guid id)
        {
            var card = await _context.Cards.FindAsync(id);

            if (card == null)
            {
                return NotFound();
            }

            var cardDto = _mapper.Map<CardReadDto>(card);

            return Ok(cardDto);
        }

        // GET: api/Cards
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CardReadDto>>> GetCards()
        {
            //return await _context.Cards.ToListAsync();
            var cards = await _context.Cards.ToListAsync();
            var cardsDto = _mapper.Map<IEnumerable<CardReadDto>>(cards);
            return Ok(cardsDto);
        }

        // POST: api/Cards
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CardCreateDto>> PostCard(CardCreateDto cardDto)
        {
            var card = _mapper.Map<Card>(cardDto);
            await _context.Cards.AddAsync(card);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (CardExists(card.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction(nameof(GetCard), new { id = card.Id }, card);
        }

        // PUT: api/Cards/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCard(Guid id, CardUpdateDto cardUpdateDto)
        {
            if (id != cardUpdateDto.Id)
            {
                return BadRequest();
            }

            var card = await _context.Cards.FindAsync(id);

            if(card == null)
                {
                    return NotFound();
                }

            _mapper.Map(cardUpdateDto, card);

            _context.Entry(card).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CardExists(id))
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

        // DELETE: api/Cards/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCard(Guid id)
        {
            var card = await _context.Cards.FindAsync(id);
            if (card == null)
            {
                return NotFound();
            }

            _context.Cards.Remove(card);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CardExists(Guid id)
        {
            return _context.Cards.Any(e => e.Id == id);
        }
    }
}
