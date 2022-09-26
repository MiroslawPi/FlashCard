using System;
using System.Collections.Generic;

namespace FlashCard.Core.Domain
{
    public partial class Card
    {
        public Guid Id { get; set; }
        public string? FrontWord { get; set; }
        public string? BackWord { get; set; }
        public string? FrontDescription { get; set; }
        public string? BackDescription { get; set; }
        public DateTime Created { get; set; }
        public Guid? CourseId { get; set; }

        public virtual Course? Course { get; set; }
    }
}
