using CyberAgentAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CyberAgentAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {

        [HttpGet]
        public IEnumerable<User> Get()
        {
            using (var context = new cyberAgentContext())
            {
                //get All User
                return context.Users.ToList();


            }
        }
    }
}
