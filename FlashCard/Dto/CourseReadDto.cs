﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FlashCard.Dto
{
    public partial class CourseReadDto
    {

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

    }
}
