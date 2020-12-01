using DashboardDeks.Services.Partner;
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
    public class PartnerController : ControllerBase
    {
        private readonly IPartnerService _partnerService;
        private readonly ILogger<PartnerController> _logger;

        public PartnerController(ILogger<PartnerController> logger, IPartnerService partnerService)
        {
            _partnerService = partnerService;
            _logger = logger;
        }

        [HttpPost("/api/partner")]
        public ActionResult CreatePartner([FromBody] PartnerModel partner)
        {
            _logger.LogInformation("Adding a new partner");

            var partnerData = PartnerMapper.SerializePartner(partner);
            var newPartner = _partnerService.CreatePartner(partnerData);
            return Ok(newPartner);
        }

        [HttpGet("/api/partner/auth/{token}")]
        public bool Auth(string token)
        {
            _logger.LogInformation("Authenticate a partners");
            string username = token.Substring(0, token.IndexOf('|'));
            string password = token.Substring(token.IndexOf('|') + 1, (token.Length - 1) - (token.IndexOf('|')));
            bool result = _partnerService.Authenticate(username, password);
            return result;
        }

        [HttpDelete("/api/partner/{id}")]
        public ActionResult DeletePartner(string id)
        {
            _logger.LogInformation("Deleting a partner");
            var response = _partnerService.DeletePartner(id);
            return Ok(response);
        }

        [HttpPatch("/api/partner")]
        public ActionResult UpdatePartner([FromBody] PartnerModel partner)
        {
            _logger.LogInformation("Update partner complete...");

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var partnerData = PartnerMapper.SerializePartner(partner);
            var response = _partnerService.UpdatePartner(partnerData);
            return Ok(response);
        }


        [HttpGet("/api/partner/{id}")]
        public ActionResult GetPartner(int id)
        {
            _logger.LogInformation("Getting a partners");
            var partners = _partnerService.GetPartner(id);
            var partnerModels = partners.Select(partner => new PartnerModel
            {
                UsernameId = partner.UsernameId,
                ProgramId = partner.ProgramId,
                Password = null,
                PartnerName = partner.PartnerName,
                Location = partner.Location,
                CaretakerName = partner.CaretakerName,
                CaretakerPhoneNumber = partner.CaretakerPhoneNumber,
                UploadLink = partner.UploadLink

            }).ToList();

            return Ok(partnerModels);
        }

        [HttpGet("/api/partner/by/{id}")]
        public ActionResult GetPartnerById(string id)
        {
            _logger.LogInformation("Getting a partners");
            var partners = _partnerService.GetById(id);
            var partnerModels = PartnerMapper.SerializePartner(partners);

            return Ok(partnerModels);
        }

        [HttpGet("/api/partner")]
        public ActionResult GetAll()
        {
            _logger.LogInformation("Getting a all partner");
            var partners = _partnerService.GetAll();
            var partnerModels = partners.Select(partner => new PartnerModel
            {
                UsernameId = partner.UsernameId,
                ProgramId = partner.ProgramId,
                Password = null,
                PartnerName = partner.PartnerName,
                Location = partner.Location,
                CaretakerName = partner.CaretakerName,
                CaretakerPhoneNumber = partner.CaretakerPhoneNumber,
                UploadLink = partner.UploadLink
            }).ToList();

            return Ok(partnerModels);
        }
    }
}
