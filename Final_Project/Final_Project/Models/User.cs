using System;
using System.Collections.Generic;

namespace Final_Project.Models
{
    public partial class User
    {
        public User()
        {
            Orders = new HashSet<Order>();
        }

        public int UserId { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }
        public string? Fullname { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}
