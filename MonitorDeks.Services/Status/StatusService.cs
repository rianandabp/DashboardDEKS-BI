using DashboardDeks.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace DashboardDeks.Services.Status
{
    public class StatusService : IStatusService
    {
        private readonly ProjectDbContext _db;

        public StatusService(ProjectDbContext dbContext)
        {
            _db = dbContext;
        }

        public bool GetStatus()
        {
            return _db.ResetStatuses.Find(1).Status;
        }

        public void UpdateStatus()
        {
            var temp = _db.ResetStatuses.Find(1);
            if (temp.Status) temp.Status = false;
            else temp.Status = true;

            _db.ResetStatuses.Update(temp);
            _db.SaveChanges();
        }
    }
}
