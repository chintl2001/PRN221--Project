using System;
using System.Collections.Generic;

namespace Final_Project.Models
{
    public partial class Comment
    {
        public int Id { get; set; }
        public int? UserId { get; set; }
        public int? BlogId { get; set; }
        public string? Comment1 { get; set; }
        public DateTime? Date { get; set; }
        public int? Status { get; set; } = 0;

        public virtual Blog? Blog { get; set; }
        public virtual User? User { get; set; }
    }
}
