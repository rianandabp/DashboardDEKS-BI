using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DashboardDeks.Web.ViewModel
{
    public class PartnerReportModel
    {
        public int Id { get; set; }
        public string CaretakerUsernameId { get; set; }
        public DateTime CreatedOn { get; set; }
        public int Progress { get; set; }
        public string Description { get; set; }
        public string Problem { get; set; }
        public string Solution { get; set; }
        public string Document { get; set; }
    }
}
