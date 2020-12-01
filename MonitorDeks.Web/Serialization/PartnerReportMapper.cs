using DashboardDeks.Data.Models;
using DashboardDeks.Web.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DashboardDeks.Web.Serialization
{
    public class PartnerReportMapper
    {
        public static PartnerReportModel SerializePartnerReport(Data.Models.PartnerReport partner)
        {
            return new PartnerReportModel
            {
                Id = partner.Id,
                CaretakerUsernameId = partner.CaretakerUsernameId,
                CreatedOn = partner.CreatedOn,
                Progress = partner.Progress,
                Description = partner.Description,
                Problem = partner.Problem,
                Solution = partner.Solution,
                Document = partner.Document

            };
        }

        public static PartnerReport SerializePartnerReport(PartnerReportModel partner)
        {
            return new PartnerReport
            {
                Id = partner.Id,
                CaretakerUsernameId = partner.CaretakerUsernameId,
                CreatedOn = partner.CreatedOn,
                Progress = partner.Progress,
                Description = partner.Description,
                Problem = partner.Problem,
                Solution = partner.Solution,
                Document = partner.Document

            };
        }
    }
}

