using System;
using System.Collections.Generic;

#nullable disable

namespace CyberAgentAPI.Models
{
    public partial class User
    {
        public User()
        {
            Answers = new HashSet<Answers>();
        }

        public int UserId { get; set; }
        public bool IsAdmin { get; set; }
        public string Email { get; set; }
        public byte[] Password { get; set; }
        public byte[] PasswordSalt { get; set; }

        public virtual ICollection<Answers> Answers { get; set; }
    }
}
