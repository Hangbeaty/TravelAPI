using System;
using System.Collections.Generic;

#nullable disable

namespace API.Models
{
    public partial class Category
    {
        public Category()
        {
            Tours = new HashSet<Tour>();
        }

        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string Description { get; set; }

        public virtual ICollection<Tour> Tours { get; set; }
    }
}
