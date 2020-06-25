using AMR_Server.Application.Account.Commands.Register;
using AMR_Server.Application.Report.Commands.GeneratePDF;
using DinkToPdf.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace AMR_Server.WebUI.Controllers
{
    [Route("Api/Report")]
    [ApiController]
    public class ReportController : ApiController
    {
        [HttpPost]
        [Route("CreatePDF")]
        public async Task<ActionResult<string>> CreatePDF(TemplateDto template)
        {
            return await Mediator.Send(new GeneratePDFCommand { Html = template.Html });
        }
    }
}
