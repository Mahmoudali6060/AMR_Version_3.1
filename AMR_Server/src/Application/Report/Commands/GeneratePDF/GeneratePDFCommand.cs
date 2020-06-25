using AMR_Server.Application.Common.Interfaces;
using AMR_Server.Domain.Entities;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using DinkToPdf.Contracts;
using DinkToPdf;
using System.IO;

namespace AMR_Server.Application.Report.Commands.GeneratePDF
{
    public class GeneratePDFCommand : IRequest<string>
    {
        public string Html { get; set; }
    }

    public class GeneratePDFCommandHandler : IRequestHandler<GeneratePDFCommand, string>
    {
        private readonly IAmrDbContext _context;
        private readonly IIdentityService _identityService;
        private readonly IConverter _converter;

        public GeneratePDFCommandHandler(IAmrDbContext context, IIdentityService identityService, IConverter converter)
        {
            _context = context;
            _identityService = identityService;
            _converter = converter;
        }

        public async Task<string> Handle(GeneratePDFCommand request, CancellationToken cancellationToken)
        {
            try
            {
                string reportName = DateTime.Now.ToString("MM_dd_yyyy_hh_mm_ss") + ".pdf";
                var globalSettings = new GlobalSettings
                {
                    ColorMode = ColorMode.Color,
                    Orientation = Orientation.Portrait,
                    PaperSize = PaperKind.A4,
                    Margins = new MarginSettings { Top = 10 },
                    DocumentTitle = "PDF Report",
                    Out = Path.Combine(Directory.GetCurrentDirectory(), "PdfReports", reportName)  //USE THIS PROPERTY TO SAVE PDF TO A PROVIDED LOCATION
                };

                var objectSettings = new ObjectSettings
                {
                    PagesCount = true,
                    HtmlContent = request.Html,
                    //Page = "https://www.facebook.com/", //USE THIS PROPERTY TO GENERATE PDF CONTENT FROM AN HTML PAGE
                    WebSettings = { DefaultEncoding = "utf-8", UserStyleSheet = Path.Combine(Directory.GetCurrentDirectory(), "assets/css", "styles.css") },
                    HeaderSettings = { FontName = "Arial", FontSize = 9, },
                    FooterSettings = { FontName = "Arial", FontSize = 9, Right = "Page [page] of [toPage]", Line = true }
                };

                var pdf = new HtmlToPdfDocument()
                {
                    GlobalSettings = globalSettings,
                    Objects = { objectSettings }
                };

                _converter.Convert(pdf); //IF WE USE Out PROPERTY IN THE GlobalSettings CLASS, THIS IS ENOUGH FOR CONVERSION

                //var file = _converter.Convert(pdf);
                //return Ok("Successfully created PDF document.");
                //return File(file, "application/pdf", "EmployeeReport.pdf");
                //return File(file, "application/pdf");

                return "PdfReports/" + reportName;
            }
            catch (Exception ex)
            {
                return null;
            }

        }
    }
}
