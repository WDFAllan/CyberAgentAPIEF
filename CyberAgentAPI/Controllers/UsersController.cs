using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CyberAgentAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using CyberAgentAPI.Models.dtos;

namespace CyberAgentAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly CyberAgentContext _context;
        private readonly JWTSettings _jwtsettings;

        public UsersController(CyberAgentContext context,IOptions<JWTSettings> jwtsettings)
        {
            _context = context;
            _jwtsettings = jwtsettings.Value;
        }



        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            return await _context.Users.ToListAsync();
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        // GET: api/Users/5
        [HttpGet("GetUser")]
        public async Task<ActionResult<User>> GetUser()
        {

            string emailAddress = HttpContext.User.Identity.Name;

            var user = await _context.Users
                   .Where(user => user.Email == emailAddress)
                   .FirstOrDefaultAsync();

            user.Password = null;

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        //[HttpPost("Login")]
        //public async Task<ActionResult<string>> Login(DtoUser request)
        //{
        //    var user = await _context.Users
        //           .Where(user => user.Email == request.Email)
        //           .FirstOrDefaultAsync();

        //    if(user == null)
        //    {
        //        return BadRequest("user not found");
        //    }

        //    if (request.Email != user.Email)
        //    {
        //        return BadRequest("request null");
        //    }

        //    string token = CreateToken(user);
        //    return Ok(token);

        //}

        //private string CreateToken(User user)
        //{
        //    List<Claim> claims = new List<Claim>()
        //    {
        //        new Claim(ClaimTypes.Name, user.Email)
        //    };
        //    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes());
        //}

        //// Get: api/Users
        //[HttpPost("Login")]
        //public async Task<ActionResult<UserWithToken>> Login([FromBody] User user)
        //{
        //    user = await _context.Users
        //           .Where(u => u.Email == user.Email && u.Password==user.Password)
        //           .FirstOrDefaultAsync();

        //    UserWithToken userWithToken = new UserWithToken(user);

        //    if (userWithToken == null)
        //    {
        //        return NotFound();
        //    }

        //    // sign your token here here..
        //    var tokenHandler = new JwtSecurityTokenHandler();
        //    var key = Encoding.ASCII.GetBytes(_jwtsettings.SecretKey);
        //    var tokenDescriptor = new SecurityTokenDescriptor
        //    {
        //        Subject = new ClaimsIdentity(new Claim[]
        //        {
        //            new Claim(ClaimTypes.Name, user.Email)
        //        }),
        //        Expires = DateTime.UtcNow.AddMonths(6),
        //        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
        //        SecurityAlgorithms.HmacSha256Signature)
        //    };
        //    var token = tokenHandler.CreateToken(tokenDescriptor);
        //    userWithToken.AccessToken = tokenHandler.WriteToken(token);
            
        //    return userWithToken;
        //}

    // PUT: api/Users/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, User user)
        {
            if (id != user.UserId)
            {
                return BadRequest();
            }

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Users
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<User>> PostUser(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUser", new { id = user.UserId }, user);
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.UserId == id);
        }
    }
}
