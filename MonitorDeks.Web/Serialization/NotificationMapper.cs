using DashboardDeks.Data.Models;
using DashboardDeks.Web.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DashboardDeks.Web.Serialization
{
    public class NotificationMapper
    {
        public static NotificationModel SerializeNotification(Notification notification)
        {
            return new NotificationModel
            {
                Id = notification.Id,
                Description = notification.Description,
                CreatedOn = notification.CreatedOn

            };
        }

        public static Notification SerializeNotification(NotificationModel notification)
        {
            return new Notification
            {
                Id = notification.Id,
                Description = notification.Description,
                CreatedOn = notification.CreatedOn
            };
        }
    }
}
