using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AMR_Server.Application.Gateways.Commands;
using AMR_Server.Application.Gateways.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AMR_Server.WebUI.Controllers
{
    [Route("Api/Gateway")]
    [ApiController]
    public class GatewayController : ApiController
    {
        [Route("GetAllGatewaysAsync")]
        [HttpGet]
        public async Task<IActionResult> GetAllGatewaysAsync(int pageSize, int currentPage, string gatewayCode, short vendorId, decimal groupId)
        {
            var gateways = await Mediator.Send(new GetAllGatewaysQuery()
            {
                PageSize = pageSize,
                CurrentPage = currentPage,
                GatewayCode = gatewayCode,
                GroupId = groupId,
                VendorId = vendorId
            });
            return Ok(gateways);
        }

        [Route("Delete")]
        [HttpGet]
        public async Task<IActionResult> Delete(string gatewayId)
        {
            var result = await Mediator.Send(new DeleteGatewayCommand()
            {
                GatewayId = gatewayId
            });
            return Ok(result);
        }

    }
}