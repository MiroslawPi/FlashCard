using System.ComponentModel.DataAnnotations;

namespace FlashCard.Dto
{
    public partial class CardUpdateDto : BaseDto
    {
        [Required]
        [StringLength(200)]
        public string FrontWord { get; set; }

        [Required]
        [StringLength(200)]
        public string BackWord { get; set; }

        [StringLength(1000)]
        public string FrontDescription { get; set; }

        [StringLength(1000)]
        public string BackDescription { get; set; }

    }
}
