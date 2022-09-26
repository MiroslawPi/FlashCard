using System.ComponentModel.DataAnnotations;

namespace FlashCard.Infastructure.Dto
{
    public partial class CardReadDto : BaseDto
    {
        public string FrontWord { get; set; }

        public string BackWord { get; set; }

        public string FrontDescription { get; set; }

        public string BackDescription { get; set; }

    }
}
