using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AMR_Server.Application.Meters.Commands;
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
            var meters = await Mediator.Send(new DeleteMeterCommand()
            {
              MeterId=meterId
            });
            return Ok(meters);
        }
    }
}