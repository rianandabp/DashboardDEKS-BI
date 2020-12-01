using DashboardDeks.Data;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DashboardDeks.Services.User
{
    public class UserService : IUserService
    {
        private readonly ProjectDbContext _db;
        public UserService(ProjectDbContext dbContext)
        {
            _db = dbContext;
        }

        public bool Authenticate(string username, string password)
        {
            var temp = _db.Users.Find(username);
            if (temp != null)
            {
                if (temp.UsernameId.Equals(username) && temp.Password.Equals(password))
                    return true;
            }
            return false;
        }


        public ServiceResponse<Data.Models.User> CreateUser(Data.Models.User user)
        {
            try
            {
                _db.Users.Add(user);
                _db.SaveChanges();
                return new ServiceResponse<Data.Models.User>
                {
                    IsSuccess = true,
                    Message = "New User Added",
                    Time = DateTime.UtcNow,
                    Data = user
                };
            }
            catch (Exception e)
            {
                return new ServiceResponse<Data.Models.User>
                {
                    IsSuccess = false,
                    Message = e.StackTrace,
                    Time = DateTime.UtcNow,
                    Data = null
                };
            }
        }

        public ServiceResponse<bool> DeleteUser(string username)
        {
            var user = _db.Users.Find(username);
            var now = DateTime.UtcNow;
            if (user == null)
            {
                return new ServiceResponse<bool>
                {
                    Time = now,
                    IsSuccess = false,
                    Message = "User to delete not found",
                    Data = false
                };
            }

            try
            {
                _db.Users.Remove(user);
                _db.SaveChanges();
                return new ServiceResponse<bool>
                {
                    Time = now,
                    IsSuccess = true,
                    Message = "User deleted!",
                    Data = true
                };
            }
            catch (Exception e)
            {
                return new ServiceResponse<bool>
                {
                    Time = now,
                    IsSuccess = false,
                    Message = e.StackTrace,
                    Data = false
                };
            }
        }

        public List<Data.Models.User> GetAllUsers()
        {
            return _db.Users.ToList();
        }


        public Data.Models.User GetByUsername(string username)
        {
            var temp = _db.Users.Find(username);
            temp.Password = null;
            return temp;
        }

        public void ResetStat()
        {

            var temp = _db.Users.ToList();

            foreach(var item in temp)
            {

                item.TaskDone = 0;
                item.TaskLate = 0;

                _db.Users.Update(item);
                _db.SaveChanges();
            }
        }

        public ServiceResponse<bool> UpdatePunctualityRating(string username)
        {
            throw new NotImplementedException();
        }

        public void UpdateStat(string username, string df)
        {
            var temp = _db.Users.Find(username);
            if (df == "1") temp.TaskDone += 1;
            else if (df == "2") temp.TaskLate += 1;

            _db.Users.Update(temp);
            _db.SaveChanges();
        }

        public ServiceResponse<Data.Models.User> UpdateUser(Data.Models.User user)
        {

            try
            {
                _db.Users.Update(user);
                _db.SaveChanges();
                return new ServiceResponse<Data.Models.User>
                {
                    IsSuccess = true,
                    Message = "New User Updated",
                    Time = DateTime.UtcNow,
                    Data = user
                };
            }
            catch (Exception e)
            {
                return new ServiceResponse<Data.Models.User>
                {
                    IsSuccess = false,
                    Message = e.StackTrace,
                    Time = DateTime.UtcNow,
                    Data = null
                };
            }
        }
    }
}
