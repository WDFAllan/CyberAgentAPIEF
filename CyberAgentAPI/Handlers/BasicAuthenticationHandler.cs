using CyberAgentAPI.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Linq;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace CyberAgentAPI.Handlers
{
    public class BasicAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        private readonly CyberAgentContext _context;


        public BasicAuthenticationHandler(
        IOptionsMonitor<AuthenticationSchemeOptions> options,
        ILoggerFactory logger,
        UrlEncoder encoder,
        ISystemClock clock,
        CyberAgentContext context)
        : base(options, logger, encoder, clock)
        {
            _context = context;
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if(!Request.Headers.ContainsKey("Authorization"))
                return AuthenticateResult.Fail("Authorization Header not found");
            
            try
            {
                var authenticationHeaderValue = AuthenticationHeaderValue.Parse(Request.Headers["Authorization"]);
                var bytes = Convert.FromBase64String(authenticationHeaderValue.Parameter);
                string[] credential = Encoding.UTF8.GetString(bytes).Split(":");
                string emailAddress = credential[0];
                string password = credential[1];

                User user = _context.Users
                    .Where(user => user.Email == emailAddress && user.Password == password)
                    .FirstOrDefault();
                if(user == null)
                {
                    return AuthenticateResult.Fail("Invalid Mail or password");

                }
                else
                {
                    var claims = new[] { new Claim(ClaimTypes.Name, user.Email) };

                    var identity = new ClaimsIdentity(claims, Scheme.Name);

                    var principal = new ClaimsPrincipal(identity);

                    var ticket = new AuthenticationTicket(principal, Scheme.Name);

                    return AuthenticateResult.Success(ticket);
                }

            }
            catch (Exception)
            {
                return AuthenticateResult.Fail("Error has occured");
            }
            return AuthenticateResult.Fail("");
        }
    }
}
