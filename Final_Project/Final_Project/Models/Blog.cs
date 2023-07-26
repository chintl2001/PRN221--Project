using System;
using System.Collections.Generic;

namespace Final_Project.Models
{
    public partial class Blog
    {
        public Blog()
        {
            Comments = new HashSet<Comment>();
        }

        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Image { get; set; }
        public string? Content { get; set; }
        public string? Author { get; set; }
        public DateTime? PublishDate { get; set; }
        public string? ShortContent { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }
    }
}
