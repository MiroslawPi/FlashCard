using FlashCard.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FlashCard.Dto
{
    public partial class CourseReadDto : BaseDto
    {
        public string Name { get; set; }

        public ICollection<CardReadDto> Cards { get; set; }
        //public ICollection<Card> Cards { get; set; }

    }
}
