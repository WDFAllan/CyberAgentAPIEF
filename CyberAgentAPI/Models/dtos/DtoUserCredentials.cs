using Microsoft.AspNetCore.Identity;

namespace CyberAgentAPI.Models.dtos
{
    public class DtoUserCredentials
    {       
        public string Email { get; set; }

        public string Password { get; set; }
    }
}
