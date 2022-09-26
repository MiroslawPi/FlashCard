namespace FlashCard.Infastructure.Dto
{
    public partial class CourseReadDto : BaseDto
    {
        public string Name { get; set; }

        public ICollection<CardReadDto> Cards { get; set; }
        //public ICollection<Card> Cards { get; set; }

    }
}
