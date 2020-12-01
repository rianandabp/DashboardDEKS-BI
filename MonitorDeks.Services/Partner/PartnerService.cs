using DashboardDeks.Data;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DashboardDeks.Services.Partner
{
    public class PartnerService : IPartnerService
    {
        private readonly ProjectDbContext _db;
        public PartnerService(ProjectDbContext dbContext)
        {
            _db = dbContext;
        }

        public ServiceResponse<Data.Models.Partner> CreatePartner(Data.Models.Partner partner)
        {
            try
            {
                _db.Partners.Add(partner);
                _db.SaveChanges();
                return new ServiceResponse<Data.Models.Partner>
                {
                    IsSuccess = true,
                    Message = "New Partner Added",
                    Time = DateTime.UtcNow,
                    Data = partner
                };
            }
            catch (Exception e)
            {
                return new ServiceResponse<Data.Models.Partner>
                {
                    IsSuccess = false,
                    Message = e.StackTrace,
                    Time = DateTime.UtcNow,
                    Data = null
                };
            }
        }

        public ServiceResponse<bool> DeletePartner(string id)
        {
            var partner = _db.Partners.Find(id);
            var now = DateTime.UtcNow;
            if (partner == null)
            {
                return new ServiceResponse<bool>
                {
                    Time = now,
                    IsSuccess = false,
                    Message = "Partner to delete not found",
                    Data = false
                };
            }

            try
            {
                _db.Partners.Remove(partner);
                _db.SaveChanges();
                return new ServiceResponse<bool>
                {
                    Time = now,
                    IsSuccess = true,
                    Message = "Partner deleted!",
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

        public List<Data.Models.Partner> GetPartner(int id)
        {
            return _db.Partners.Where(c => c.ProgramId.Equals(id)).ToList();
        }

        public Data.Models.Partner GetById(string id)
        {
            return _db.Partners.Find(id);
        }

        public ServiceResponse<Data.Models.Partner> UpdatePartner(Data.Models.Partner partner)
        {
            try
            {
                _db.Partners.Update(partner);
                _db.SaveChanges();
                return new ServiceResponse<Data.Models.Partner>
                {
                    IsSuccess = true,
                    Message = "New Partner Updated",
                    Time = DateTime.UtcNow,
                    Data = partner
                };
            }
            catch (Exception e)
            {
                return new ServiceResponse<Data.Models.Partner>
                {
                    IsSuccess = false,
                    Message = e.StackTrace,
                    Time = DateTime.UtcNow,
                    Data = null
                };
            }
        }

        public bool Authenticate(string username, string password)
        {
            var temp = _db.Partners.Find(username);
            if (temp != null)
            {
                if (temp.UsernameId.Equals(username) && temp.Password.Equals(password))
                    return true;
            }
            return false;
        }

        public List<Data.Models.Partner> GetAll()
        {
            return _db.Partners.ToList();
        }
    }
}
