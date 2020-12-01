using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DashboardDeks.Services.Document;
using DashboardDeks.Web.ViewModel;
using DashboardDeks.Web.Serialization;
using DashboardDeks.Services.Notification;
using DashboardDeks.Data.Models;

namespace DashboardDeks.Web.Controllers
{
    [ApiController]
    public class DocumentController : ControllerBase
    {
        private readonly ILogger<DocumentController> _logger;
        private readonly IDocumentService _documentService;

        public DocumentController(ILogger<DocumentController> logger, IDocumentService documentService)
        {
            _documentService = documentService;
            _logger = logger;
        }

        [HttpPost("/api/document")]
        public ActionResult AddDocument([FromBody] DocumentModel document)
        {
            _logger.LogInformation("Adding a new document");

            document.UploadedOn = DateTime.UtcNow;
            var documentData = DocumentMapper.SerializeDocument(document);
            var newDocument = _documentService.CreateDocument(documentData);
            return Ok(newDocument);
        }

        [HttpGet("/api/document/{id}")]
        public ActionResult GetDocumentByProgram(int id)
        {
            _logger.LogInformation("Getting a documents");
            var document = _documentService.GetByProgramId(id);
            var documentModel = document.Select(document => new DocumentModel
            {
                Id = document.Id,
                ProgramId = document.ProgramId,
                TaskId = document.TaskId,
                UsernameId = document.UsernameId,
                Name = document.Name,
                Link = document.Link,
                Description = document.Description,
                UploadedOn = document.UploadedOn

            }).ToList();

            return Ok(documentModel);
        }

        [HttpGet("/api/document/task/{id}")]
        public ActionResult GetDocumentByTask(int id)
        {
            _logger.LogInformation("Getting a documents");
            var document = _documentService.GetByTaskId(id);
            var documentModel = document.Select(document => new DocumentModel
            {
                Id = document.Id,
                ProgramId = document.ProgramId,
                TaskId = document.TaskId,
                UsernameId = document.UsernameId,
                Name = document.Name,
                Link = document.Link,
                Description = document.Description,
                UploadedOn = document.UploadedOn

            }).ToList();

            return Ok(documentModel);
        }

        [HttpPatch("/api/document")]
        public ActionResult UpdateDocument([FromBody] DocumentModel document)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var documentData = DocumentMapper.SerializeDocument(document);
            var newDocument = _documentService.UpdateDocument(documentData);
            return Ok(newDocument);
        }



    }
}
