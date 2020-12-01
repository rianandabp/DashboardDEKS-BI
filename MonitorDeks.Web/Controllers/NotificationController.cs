using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DashboardDeks.Services.Notification;
using Microsoft.Extensions.Logging;
using DashboardDeks.Web.ViewModel;
using DashboardDeks.Web.Serialization;

namespace DashboardDeks.Web.Controllers
{
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private readonly ILogger<NotificationController> _logger;
        private readonly INotificationService _notificationService;

        public NotificationController(ILogger<NotificationController> logger, INotificationService notificationService)
        {
            _notificationService = notificationService;  
            _logger = logger;
        }

        [HttpPost("/api/notification")]
        public ActionResult AddNotification([FromBody] NotificationModel notification)
        {
            _logger.LogInformation("Adding a new Notification");
    
            notification.CreatedOn = DateTime.UtcNow;
            var notificationData = NotificationMapper.SerializeNotification(notification);
            var newNotification = _notificationService.CreateNotification(notificationData);
            return Ok(newNotification);
        }

        [HttpGet("/api/notification")]
        public ActionResult GetAllNotifications()
        {
            _logger.LogInformation("Getting a notifications");
            var notifications = _notificationService.GetAllNotifications();
            var notificationModels = notifications.Select(notification => new NotificationModel
            {
                Id = notification.Id,
                Description = notification.Description,
                CreatedOn = notification.CreatedOn

            }).OrderByDescending(notification => notification.CreatedOn).ToList();

            return Ok(notificationModels);
        }

    }
}
