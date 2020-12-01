using DashboardDeks.Services.PartnerReport;
using DashboardDeks.Web.Serialization;
using DashboardDeks.Web.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DashboardDeks.Web.Controllers
{
    [ApiController]
    public class PartnerReportController : ControllerBase
    {
        private readonly IPartnerReportService _partnerReportService;
        private readonly ILogger<PartnerReportController> _logger;

        public PartnerReportController(ILogger<PartnerReportController> logger, IPartnerReportService partnerReportService)
        {
            _partnerReportService = partnerReportService;
            _logger = logger;
        }

        [HttpPost("/api/partner/report")]
        public ActionResult CreatePartnerReport([FromBody] PartnerReportModel partnerReport)
        {
            _logger.LogInformation("Adding a new partner report");
            partnerReport.CreatedOn = DateTime.Now;
            var partnerReportData = PartnerReportMapper.SerializePartnerReport(partnerReport);
            var newPartnerReport = _partnerReportService.CreateReport(partnerReportData);
            return Ok(newPartnerReport);
        }

        [HttpGet("/api/partner/report/{username}")]
        public ActionResult GetPartnerReport(string username)
        {
            _logger.LogInformation("Getting a partner report");
            var partnerReport = _partnerReportService.GetReport(username);
            var partnerReportModels = partnerReport.Select(partner => new PartnerReportModel
            {
                Id = partner.Id,
                CaretakerUsernameId = partner.CaretakerUsernameId,
                CreatedOn = partner.CreatedOn,
                Progress = partner.Progress,
                Description = partner.Description,
                Problem = partner.Problem,
                Solution = partner.Solution,
                Document = partner.Document

            }).ToList();

            return Ok(partnerReportModels);
        }

        [HttpDelete("/api/partner/report/{id}")]
        public ActionResult DeleteReport(int id)
        {
            _logger.LogInformation("Deleting a report");
            var response = _partnerReportService.DeleteReport(id);
            return Ok(response);
        }
    }
}
