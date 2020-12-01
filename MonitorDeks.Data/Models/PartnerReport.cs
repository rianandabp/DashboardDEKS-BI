using System;
using System.Collections.Generic;
using System.Text;

namespace DashboardDeks.Data.Models
{
    public class PartnerReport
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
