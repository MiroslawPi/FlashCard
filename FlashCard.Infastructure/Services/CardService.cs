using AutoMapper;
using FlashCard.Core.Domain;
using FlashCard.Core.Repositories;
using FlashCard.Infastructure.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlashCard.Infastructure.Services
{
    public class CardService : ICardService
    {
        private readonly ICardRepository _cardRepository;
        private readonly IMapper _mapper;

        public CardService(ICardRepository cardRepository, IMapper mapper)
        {
            _cardRepository = cardRepository;
            _mapper = mapper;
        }
        public async Task<CardReadDto> GetAsync(Guid id)
        {
            var card = await _cardRepository.GetAsync(id);
            var cardDto = _mapper.Map<CardReadDto>(card);
            return cardDto;
        }
        public async Task<IEnumerable<CardReadDto>> BrowseAsync(string name = "")
        {
            var cards = await _cardRepository.BrowseAsync(name);
            var cardDtos = _mapper.Map<IEnumerable<CardReadDto>>(cards);

            return cardDtos;
        }

        public async Task AddAsync(CardCreateDto cardCreateDto)
        {

            var card = _mapper.Map<Card>(cardCreateDto);
            await _cardRepository.AddAsync(card);
        }
        public async Task UpdateAsync(CardUpdateDto cardUpdateDto)
        {
            var card = await _cardRepository.GetAsync(cardUpdateDto.Id);
            _mapper.Map(cardUpdateDto, card);
            await _cardRepository.UpdateAsync(card);
        }
        public async Task DeleteAsync(Guid id)
        {
            var card = await _cardRepository.GetAsync(id);
            await _cardRepository.DeleteAsync(card);
        }
    }
}
