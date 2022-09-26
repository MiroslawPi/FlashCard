using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FlashCard.Infastructure.Dto
{
    public partial class CourseUpdateDto : BaseDto
    {

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

    }
}
