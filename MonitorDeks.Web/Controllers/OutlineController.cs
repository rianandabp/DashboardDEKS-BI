using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DashboardDeks.Services.Outline;
using DashboardDeks.Web.ViewModel;
using DashboardDeks.Web.Serialization;
using DashboardDeks.Services.Notification;
using DashboardDeks.Data.Models;

namespace DashboardDeks.Web.Controllers
{
    [ApiController]
    public class OutlineController : ControllerBase
    {
        private readonly ILogger<OutlineController> _logger;
        private readonly IOutlineService _outlineService;

        public OutlineController(ILogger<OutlineController> logger, IOutlineService outlineService)
        {
            _outlineService = outlineService;
            _logger = logger;
        }

        [HttpPost("/api/outline")]
        public ActionResult AddOutline([FromBody] OutlineModel outline)
        {
            _logger.LogInformation("Adding a new Outline");

            outline.LastUpdate = DateTime.UtcNow;
            var outlineData = OutlineMapper.SerializeOutline(outline);
            var newOutline = _outlineService.CreateOutline(outlineData);
            return Ok(newOutline);
        }

        [HttpGet("/api/outline")]
        public ActionResult GetOutline()
        {
            _logger.LogInformation("Getting a outlines");
            var outlines = _outlineService.GetAllOutlines();
            var outlineModels = outlines.Select(outline => new OutlineModel
            {
                Id = outline.Id,
                Name = outline.Name,
                ProgramId = outline.ProgramId,
                ProgressPercentage = outline.ProgressPercentage,
                TotalTask = outline.TotalTask,
                TaskComplete = outline.TaskComplete,
                Deadline = outline.Deadline,
                ProgressNote = outline.ProgressNote,
                ProblemNote = outline.ProblemNote,
                LastUpdate = outline.LastUpdate

            }).OrderByDescending(outline => outline.LastUpdate).ToList();

            return Ok(outlineModels);
        }

        [HttpGet("/api/outline/{id}")]
        public ActionResult GetOutlineByProgramId(int id)
        {
            _logger.LogInformation("Getting a outlines");
            var outlines = _outlineService.GetOutlineByProgramId(id);
            var outlineModels = outlines.Select(outline => new OutlineModel
            {
                Id = outline.Id,
                Name = outline.Name,
                ProgressPercentage = outline.ProgressPercentage,
                TotalTask = outline.TotalTask,
                TaskComplete = outline.TaskComplete,
                ProgramId = outline.ProgramId,
                Deadline = outline.Deadline,
                ProgressNote = outline.ProgressNote,
                ProblemNote = outline.ProblemNote,
                LastUpdate = outline.LastUpdate

            }).OrderBy(outline => outline.LastUpdate).ToList();

            return Ok(outlineModels);
        }

        [HttpDelete("/api/outline/{id}")]
        public ActionResult DeleteOutline(int id)
        {
            _logger.LogInformation("Deleting a outline");
            var response = _outlineService.DeleteOutline(id);
            return Ok(response);
        }

        [HttpPatch("/api/outline")]
        public ActionResult UpdateOutline([FromBody] OutlineModel outline)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            outline.LastUpdate = DateTime.UtcNow;
            var outlineData = OutlineMapper.SerializeOutline(outline);
            var newOutline = _outlineService.UpdateOutline(outlineData);
            return Ok(newOutline);
        }
    }
}
