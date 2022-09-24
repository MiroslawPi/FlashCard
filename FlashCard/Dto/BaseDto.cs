using System.ComponentModel.DataAnnotations;

namespace FlashCard.Dto
{
    public abstract class BaseDto
    {
        [Required]
        public Guid Id { get; set; }
    }
}