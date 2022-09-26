using System.ComponentModel.DataAnnotations;

namespace FlashCard.Infastructure.Dto
{
    public abstract class BaseDto
    {
        [Required]
        public Guid Id { get; set; }
    }
}