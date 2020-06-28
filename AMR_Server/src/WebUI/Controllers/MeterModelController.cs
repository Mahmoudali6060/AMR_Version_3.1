using System.Threading.Tasks;
using AMR_Server.Application.MeterModels.Queries;
using Microsoft.AspNetCore.Mvc;

namespace AMR_Server.WebUI.Controllers
{
    [Route("Api/MeterModel")]
    [ApiController]
    public class MeterModelController : ApiController
    {
        [Route("GetAllMeterModelLite")]
        [HttpGet]
        public async Task<IActionResult> GetAllMeterModelLite(int id)
        {
            var meterModels = await Mediator.Send(new GetMeterModelLiteQuery());
            return Ok(meterModels);
        }
    }
}