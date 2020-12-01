using DashboardDeks.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DashboardDeks.Services.Notification
{
    public class NotificationService : INotificationService
    {
        private readonly ProjectDbContext _db;
        

        public NotificationService(ProjectDbContext dbContext)
        {
            _db = dbContext;
        }

        public ServiceResponse<Data.Models.Notification> CreateNotification(Data.Models.Notification notification)
        {
            try
            {
                _db.Notifications.Add(notification);
                _db.SaveChanges();
                return new ServiceResponse<Data.Models.Notification>
                {
                    IsSuccess = true,
                    Message = "New Notification Added",
                    Time = DateTime.UtcNow,
                    Data = notification
                };
            }
            catch (Exception e)
            {
                return new ServiceResponse<Data.Models.Notification>
                {
                    IsSuccess = false,
                    Message = e.StackTrace,
                    Time = DateTime.UtcNow,
                    Data = null
                };
            }

        }

        public List<Data.Models.Notification> GetAllNotifications()
        {

            return _db.Notifications.OrderBy(date => date.CreatedOn).ToList();
        }

        public Data.Models.Notification GetById(int id)
        {
            return _db.Notifications.Find(id);
        }

    }
}
