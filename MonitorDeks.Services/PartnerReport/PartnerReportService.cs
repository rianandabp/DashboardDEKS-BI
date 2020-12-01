using DashboardDeks.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace DashboardDeks.Services.PartnerReport
{
    public class PartnerReportService : IPartnerReportService
    {
        private readonly ProjectDbContext _db;
        public PartnerReportService(ProjectDbContext dbContext)
        {
            _db = dbContext;
        }
        public ServiceResponse<Data.Models.PartnerReport> CreateReport(Data.Models.PartnerReport report)
        {
            try
            {
                _db.PartnerReports.Add(report);
                _db.SaveChanges();
                return new ServiceResponse<Data.Models.PartnerReport>
                {
                    IsSuccess = true,
                    Message = "New Partner Report Added",
                    Time = DateTime.UtcNow,
                    Data = report
                };
            }
            catch (Exception e)
            {
                return new ServiceResponse<Data.Models.PartnerReport>
                {
                    IsSuccess = false,
                    Message = e.StackTrace,
                    Time = DateTime.UtcNow,
                    Data = null
                };
            }
        }

        public List<Data.Models.PartnerReport> GetReport(string username)
        {
            return _db.PartnerReports.Where(c => c.CaretakerUsernameId.Equals(username)).ToList();
        }

        public Data.Models.PartnerReport GetById(int id)
        {
            return _db.PartnerReports.Find(id);
        }

        public ServiceResponse<bool> DeleteReport(int id)
        {
            var report = _db.PartnerReports.Find(id);
            var now = DateTime.UtcNow;
            if (report == null)
            {
                return new ServiceResponse<bool>
                {
                    Time = now,
                    IsSuccess = false,
                    Message = "Report to delete not found",
                    Data = false
                };
            }

            try
            {
                _db.PartnerReports.Remove(report);
                _db.SaveChanges();
                return new ServiceResponse<bool>
                {
                    Time = now,
                    IsSuccess = true,
                    Message = "Report deleted!",
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
    }
}
