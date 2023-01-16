using System;
using System.Collections.Generic;

#nullable disable

namespace API.Models
{
    public partial class Place
    {
        public Place()
        {
            Tours = new HashSet<Tour>();
        }

        public int PlaceId { get; set; }
        public string PlaceName { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }

        public virtual ICollection<Tour> Tours { get; set; }
    }
}
