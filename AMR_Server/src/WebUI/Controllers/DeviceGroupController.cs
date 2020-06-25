using System.Threading.Tasks;
using AMR_Server.Application.DeviceGroups.Queries;
using Microsoft.AspNetCore.Mvc;

namespace AMR_Server.WebUI.Controllers
{
    [Route("Api/DeviceGroup")]
    [ApiController]
    public class DeviceGroupController : ApiController
    {
        //[Route("GetDeviceGroupDetailsByIdAsync/{id}")]
        //[HttpGet]
        //public async Task<IActionResult> GetDeviceGroupDetailsByIdAsync(int id)
        //{
        //    var group=await Mediator.Send(new GetDeviceGroupDetailsByIdQuery() {groupId=id });
        //    return Ok(group);
        //}

        [Route("GetDeviceGroupLite")]
        [HttpGet]
        public async Task<IActionResult> GetDeviceGroupLite(int id)
        {
            var groups = await Mediator.Send(new GetDeviceGroupLiteQuery());
            return Ok(groups);
        }

        

    }
}