using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataLayer
{
    public class User
    {
        public User()
        {
            Orders = new HashSet<Order>();
        }
        [Key]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string EmailAddress { get; set; }
        [Required]

        public ICollection<Order> Orders { get; set; }
    }
}