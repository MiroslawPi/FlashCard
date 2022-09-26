using AutoMapper;
using FlashCard.Core.Domain;
using FlashCard.Infastructure.Dto;

namespace FlashCard.Configuration
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<CourseCreateDto,Course>().ReverseMap();
            CreateMap<CourseReadDto, Course>().ReverseMap();
            CreateMap<CourseUpdateDto, Course>().ReverseMap();
            CreateMap<CardCreateDto, Card>().ReverseMap();
            CreateMap<CardReadDto, Card>().ReverseMap();
            CreateMap<CardUpdateDto, Card>().ReverseMap();
        }
    }
}
