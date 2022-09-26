using System;
using System.Collections.Generic;

namespace FlashCard.Core.Domain
{
    public partial class Course
    {
        public Course()
        {
            Cards = new HashSet<Card>();
        }

        public Guid Id { get; set; }
        public string? Name { get; set; }
        public DateTime Created { get; set; }

        public virtual ICollection<Card> Cards { get; set; }
    }
}
