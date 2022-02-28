using System;
using System.Collections.Generic;

#nullable disable

namespace CyberAgentAPI.Models
{
    public partial class User
    {
        public int UserId { get; set; }
        public bool IsAdmin { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
