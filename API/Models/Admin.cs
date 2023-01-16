using System;
using System.Collections.Generic;

#nullable disable

namespace API.Models
{
    public partial class Admin
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string PassWord { get; set; }
    }
}
