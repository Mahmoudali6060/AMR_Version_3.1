using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AMR_Server.Application.Privileges.Queries.GetUserPrivileges;
using Microsoft.AspNetCore.Mvc;

namespace AMR_Server.WebUI.Controllers
{
    [Route("Api/Privilege")]
    [ApiController]
    public class PrivilegeController : ApiController
    {
        [Route("GetUserPrivileges")]
        [HttpGet]
        public async Task<IActionResult> GetUserPrivileges(int userId)
        {
            var privileges = await Mediator.Send(new GetUserPrivilegesQuery()
            {
                UserId = userId
            });
            return Ok(privileges);
        }
    }
}