using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using GP.DB;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GamblingPlace.Controllers
{
    [Produces("application/json")]
    [Route("api/Users")]

    public class UsersController : Controller
    {
        public IActionResult Get()
        {
            var context = new GPDbContext();

            return Ok(context.Users.ToList());
        }
    }
}