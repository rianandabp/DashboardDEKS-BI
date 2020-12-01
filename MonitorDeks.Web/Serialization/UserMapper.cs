using DashboardDeks.Data.Models;
using DashboardDeks.Web.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DashboardDeks.Web.Serialization
{
    public class UserMapper
    {
        public static UserModel SerializeUser(User user)
        {
            return new UserModel
            {
                UsernameId = user.UsernameId,
                Name = user.Name,
                Password = user.Password,
                UserAccess = user.UserAccess,
                PunctualityRating = user.PunctualityRating,
                TaskDone = user.TaskDone,
                TaskLate = user.TaskLate
            };
        }

        public static User SerializeUser(UserModel user)
        {

            return new User
            {
                UsernameId = user.UsernameId,
                Name = user.Name,
                Password = user.Password,
                UserAccess = user.UserAccess,
                PunctualityRating = user.PunctualityRating,
                TaskDone = user.TaskDone,
                TaskLate = user.TaskLate
            };
        }
    }
}
