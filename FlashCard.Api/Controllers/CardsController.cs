using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using FlashCard.Infastructure.Data;
using FlashCard.Infastructure.Dto;
using FlashCard.Core.Domain;
using FlashCard.Core.Repositories;

namespace FlashCard.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CardsController : ControllerBase
    {
        private readonly ICardService _cardService;

        public CardsController(ICardService cardService)
        {
            _cardService = cardService;
        }

        // GET: api/Cards/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CardReadDto>> GetCard(Guid id)
        {
            var cardReadDto = await _cardService.GetAsync(id);

            if (cardReadDto == null)
            {
                return NotFound();
            }

            return Ok(cardReadDto);
        }

        // GET: api/Cards
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CardReadDto>>> GetCards(string? name)
        {
            var cardReadDtos = await _cardService.BrowseAsync(name);
            return Ok(cardReadDtos);
        }

        // POST: api/Cards
        [HttpPost]
        public async Task<ActionResult<CardCreateDto>> PostCard(CardCreateDto cardCreateDto)
        {
            await _cardService.AddAsync(cardCreateDto);

            //return CreatedAtAction(nameof(GetCard), new { id = card.Id }, card);
            return Ok(NoContent());
        }

        // PUT: api/Cards/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCard(Guid id, CardUpdateDto cardUpdateDto)
        {
            if (id != cardUpdateDto.Id)
            {
                return BadRequest();
            }

            var cardReadDto = await _cardService.GetAsync(id);

            if(cardReadDto == null)
                {
                    return NotFound();
                }

            await _cardService.UpdateAsync(cardUpdateDto);

            return Ok(NoContent());
        }

        // DELETE: api/Cards/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCard(Guid id)
        {
            var cardReadDto = await _cardService.GetAsync(id);
            if (cardReadDto == null)
            {
                return NotFound();
            }

            await _cardService.DeleteAsync(id);

            return NoContent();
        }

    }
}
