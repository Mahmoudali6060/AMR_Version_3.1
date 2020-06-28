using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AMR_Server.Application.Meters.Commands;
using AMR_Server.Application.Meters.Commands.UpdateMeter;
using AMR_Server.Application.Meters.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AMR_Server.WebUI.Controllers
{
    [Route("Api/Meter")]
    [ApiController]
    public class MeterController : ApiController
    {
        [Route("GetAllMetersAsync")]
        [HttpGet]
        public async Task<IActionResult> GetAllMetersAsync(int pageSize, int currentPage, string HCN, short vendorId, decimal groupId)
        {
            var meters = await Mediator.Send(new GetAllMetersQuery()
            {
                PageSize = pageSize,
                CurrentPage = currentPage,
                HCN = HCN,
                GroupId = groupId,
                VendorId = vendorId
            });
            return Ok(meters);
        }

        [Route("Delete")]
        [HttpGet]
        public async Task<IActionResult> Delete(string meterId)
        {
            return Ok(await Mediator.Send(new DeleteMeterCommand()
            {
                MeterId = meterId
            }));
        }

        [Route("Update")]
        [HttpPost]
        public async Task<IActionResult> Update(UpdateMeterCommand command)
        {
            return Ok(await Mediator.Send(command));
        }
    }
}