using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DashboardDeks.Services.Program;
using DashboardDeks.Web.ViewModel;
using DashboardDeks.Web.Serialization;
using DashboardDeks.Services.Notification;
using DashboardDeks.Data.Models;
using Microsoft.EntityFrameworkCore;
using DashboardDeks.Services.Outline;

namespace DashboardDeks.Web.Controllers
{
    [ApiController]
    public class ProgramController : ControllerBase
    {
        private readonly ILogger<ProgramController> _logger;
        private readonly IProgramService _programService;

        public ProgramController(ILogger<ProgramController> logger, IProgramService programService)
        {
            _programService = programService;
            _logger = logger;
        }

        [HttpPost("/api/program")]
        public ActionResult AddProgram([FromBody] ProgramModel program)
        {
            _logger.LogInformation("Adding a new Program");

            program.LastUpdate = DateTime.UtcNow;
            var programData = ProgramMapper.SerializeProgram(program);
            var newProgram = _programService.CreateProgram(programData);
            return Ok(newProgram);
        }

        [HttpPatch("/api/program")]
        public ActionResult UpdateProgram([FromBody] ProgramModel program)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            program.LastUpdate = DateTime.UtcNow;
            var programData = ProgramMapper.SerializeProgram(program);
            var newProgram = _programService.UpdateProgram(programData);
            return Ok(newProgram);

        }

        [HttpPatch("/api/program/progress/{id}")]
        public ActionResult UpdateProgramProgress(int id)
        {
            _programService.SetProgressPercentage(id);
            return Ok();
        }

        [HttpGet("/api/program")]
        public ActionResult GetProgram()
        {
            _logger.LogInformation("Getting a programs");
            var programs = _programService.GetAllPrograms();
            var programModels = programs.Select(program => new ProgramModel
            {
                Id = program.Id,
                Name = program.Name,
                ProgressPercentage = program.ProgressPercentage,
                TotalTask = program.TotalTask,
                TaskComplete = program.TaskComplete,
                Description = program.Description,
                StartDate = program.StartDate,
                EndDate = program.EndDate,
                LastUpdate = program.LastUpdate

            }).OrderByDescending(program => program.LastUpdate).ToList();

            return Ok(programModels);
        }

        [HttpGet("/api/program/{id}")]
        public ActionResult GetProgramById(int id)
        {
            _logger.LogInformation("Getting a programs");
            var program = _programService.GetById(id);
            var programModels = ProgramMapper.SerializeProgram(program);

            return Ok(programModels);
        }

        [HttpDelete("/api/program/{id}")]
        public ActionResult DeleteProgram(int id)
        {
            _logger.LogInformation("Deleting a program");
            var response = _programService.DeleteProgram(id);
            return Ok(response);
        }
        
    }

    
}
