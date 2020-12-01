using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DashboardDeks.Services.Task;
using DashboardDeks.Web.ViewModel;
using DashboardDeks.Web.Serialization;
using DashboardDeks.Data.Models;
using DashboardDeks.Services.Outline;

namespace DashboardDeks.Web.Controllers
{
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly ILogger<TaskController> _logger;
        private readonly ITaskService _taskService;

        public TaskController(ILogger<TaskController> logger, ITaskService taskService)
        {
            _taskService = taskService;
            _logger = logger;
        }

        [HttpPost("/api/task")]
        public ActionResult CreateTask([FromBody] TaskModel task)
        {
            _logger.LogInformation("Creating a new task");

            task.LastUpdate = DateTime.UtcNow;
            var taskData = TaskMapper.SerializeTask(task);
            var newTask = _taskService.CreateTask(taskData);
            return Ok(newTask);
        }

        [HttpPatch("/api/task/{id}")]
        public ActionResult UpdateTaskStatus(int id)
        {
            _logger.LogInformation("Task status has been updated");
            _taskService.UpdateTaskStatus(id);
            return Ok();
        }

        [HttpGet("/api/task")]
        public ActionResult GetTasks()
        {
            _logger.LogInformation("Getting a new task");
            var tasks = _taskService.GetAllTask();
            var taskModels = tasks.Select(task => new TaskModel
            {
                Id = task.Id,
                ProgramId = task.ProgramId,
                OutlineId = task.OutlineId,
                AssignedTo = task.AssignedTo,
                Name = task.Name,
                Status = task.Status,
                Deadline = task.Deadline,
                ProgressNote = task.ProgressNote,
                ProblemNote = task.ProblemNote,
                LastUpdate = task.LastUpdate
                
            }).OrderByDescending(task => task.Deadline).ToList();

            return Ok(taskModels);
        }

        [HttpGet("/api/task/{id}")]
        public ActionResult GetTaskByProgramId(int id)
        {
            _logger.LogInformation("Getting a new task");
            var tasks = _taskService.GetTaskByProgramId(id);
            var taskModels = tasks.Select(task => new TaskModel
            {
                Id = task.Id,
                ProgramId = task.ProgramId,
                OutlineId = task.OutlineId,
                AssignedTo = task.AssignedTo,
                Name = task.Name,
                Status = task.Status,
                Deadline = task.Deadline,
                ProgressNote = task.ProgressNote,
                ProblemNote = task.ProblemNote,
                LastUpdate = task.LastUpdate

            }).OrderBy(task => task.Deadline).ToList();

            return Ok(taskModels);
        }

        [HttpGet("/api/task/count")]
        public ActionResult GetTaskTotal()
        {
            _logger.LogInformation("Getting total task");
            int total = _taskService.TotalTask();
            return Ok(total);
        }

        [HttpGet("/api/task/outline/{id}")]
        public ActionResult GetTaskByOutlineId(int id)
        {
            _logger.LogInformation("Getting a new task");
            var tasks = _taskService.GetTaskByOutlineId(id);
            var taskModels = tasks.Select(task => new TaskModel
            {
                Id = task.Id,
                ProgramId = task.ProgramId,
                OutlineId = task.OutlineId,
                AssignedTo = task.AssignedTo,
                Name = task.Name,
                Status = task.Status,
                Deadline = task.Deadline,
                ProgressNote = task.ProgressNote,
                ProblemNote = task.ProblemNote,
                LastUpdate = task.LastUpdate

            }).OrderBy(task => task.Deadline).ToList();

            return Ok(taskModels);
        }

        [HttpDelete("/api/task/{id}")]
        public ActionResult DeleteTask(int id)
        {
            _logger.LogInformation("Deleting a task");
            var response = _taskService.DeleteTask(id);
            return Ok(response);
        }

        [HttpPatch("/api/task")]
        public ActionResult UpdateTask([FromBody] TaskModel task)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            task.LastUpdate = DateTime.UtcNow;
            var taskData = TaskMapper.SerializeTask(task);
            var newTask = _taskService.UpdateTask(taskData);
            return Ok(newTask);
        }
    }
}
