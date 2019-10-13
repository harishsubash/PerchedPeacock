using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System;
using static PerchedPeacock.Contracts.PerchedPeacockParking;
using System.Net;
using static PerchedPeacock.Contracts.PerchedPeacockParking.V1;

namespace PerchedPeacock.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParkingLotsController : ControllerBase
    {
        private readonly ParkingLotApplicationService _applicationService;

        public ParkingLotsController(
            ParkingLotApplicationService applicationService)
        {
            _applicationService = applicationService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
            => await ResponseAsync(_applicationService.GetParkingLots());

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetOne(Guid id)
            =>  Ok(await _applicationService.Load(id));

        [HttpPost]
        public async Task<IActionResult> Post(V1.CreateParking request)
             => await ResponseAsync(_applicationService.CreateParking(request));

        private async Task<IActionResult> ResponseAsync<T>(Task<T> result)
        {
            try
            {
                return Ok(await result);
            }
            catch (Exception ex)
            {
                return BadRequest($"{ex.Message}");
            }
        }
    }

}