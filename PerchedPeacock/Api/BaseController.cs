using Microsoft.AspNetCore.Mvc;
using PerchedPeacock.Core;
using System;
using System.Threading.Tasks;

namespace PerchedPeacock.Api
{
    public class BaseController : Controller
    {
        public async Task<IActionResult> ResponseAsync(object result)
        {
            try
            {
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest($"{ex.Message}");
            }
        }
    }
}
