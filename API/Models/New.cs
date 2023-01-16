using System;
using System.Collections.Generic;

#nullable disable

namespace API.Models
{
    public partial class New
    {
        public int NewId { get; set; }
        public string NewName { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
    }
}
