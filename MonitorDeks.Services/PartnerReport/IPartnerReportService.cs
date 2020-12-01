using System;
using System.Collections.Generic;
using System.Text;

namespace DashboardDeks.Services.PartnerReport
{
    public interface IPartnerReportService
    {
        List<Data.Models.PartnerReport> GetReport(string username);
        ServiceResponse<Data.Models.PartnerReport> CreateReport(Data.Models.PartnerReport report);
        Data.Models.PartnerReport GetById(int id);
        ServiceResponse<bool> DeleteReport(int id);
    }
}
