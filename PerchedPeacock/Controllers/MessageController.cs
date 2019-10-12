using System;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PerchedPeacock.Controllers
{
    [Produces("application/json")]
    [Route("api/messages")]
    [Authorize]
    public class MessageController : Controller
    {
        [HttpGet]
        public JsonResult Get()
        {
            var principal = HttpContext.User.Identity as ClaimsIdentity;

            var login = principal.Claims
                .SingleOrDefault(c => c.Type == ClaimTypes.NameIdentifier)
                ?.Value;

            return Json(new
            {
                messages = new dynamic[]
                {
                new { Date = DateTime.Now, Text = "I am a Robot." },
                }
            });
        }
    }
}