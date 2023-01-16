using System;
using System.Collections.Generic;

#nullable disable

namespace API.Models
{
    public partial class Tour
    {
        public Tour()
        {
            Orders = new HashSet<Order>();
        }

        public int TourId { get; set; }
        public string TourName { get; set; }
        public int? CategoryId { get; set; }
        public int? PlaceId { get; set; }
        public string Description { get; set; }
        public int? Price { get; set; }
        public int? PriceDiscount { get; set; }
        public string CategoryName { get; set; }
        public string Image { get; set; }
        public string PlaceName { get; set; }
        public string Time { get; set; }

        public virtual Category Category { get; set; }
        public virtual Place Place { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }
}
