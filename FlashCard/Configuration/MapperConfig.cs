using AutoMapper;
using FlashCard.Data;
using FlashCard.Dto;

namespace FlashCard.Configuration
{
    public class MapperConfig : Profile
    {
        public MapperConfig()
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
