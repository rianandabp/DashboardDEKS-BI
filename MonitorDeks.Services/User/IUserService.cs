using System;
using System.Collections.Generic;
using System.Text;

namespace DashboardDeks.Services.User
{
    public interface IUserService
    {
        List<Data.Models.User> GetAllUsers();
        ServiceResponse<Data.Models.User> CreateUser(Data.Models.User user);
        ServiceResponse<Data.Models.User> UpdateUser(Data.Models.User user);
        ServiceResponse<bool> DeleteUser(string username);
        ServiceResponse<bool> UpdatePunctualityRating(string username);
        Data.Models.User GetByUsername(string username);
        bool Authenticate(string username, string password);
        void ResetStat();
        void UpdateStat(string username, string df);
    }
}
