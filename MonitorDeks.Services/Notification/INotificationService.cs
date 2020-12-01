using System;
using System.Collections.Generic;
using System.Text;

namespace DashboardDeks.Services.Notification
{
    public interface INotificationService
    {
        List<Data.Models.Notification> GetAllNotifications();
        ServiceResponse<Data.Models.Notification> CreateNotification(Data.Models.Notification notification);
        Data.Models.Notification GetById(int id);
    }
}
