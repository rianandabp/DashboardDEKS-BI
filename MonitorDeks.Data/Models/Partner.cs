using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DashboardDeks.Data.Models
{
    public class Partner
    {
        [Key]
        public string UsernameId { get; set; }
        public int ProgramId { get; set; }
        public string Password { get; set; }
        public string PartnerName { get; set; }
        public string Location { get; set; }
        public string CaretakerName { get; set; }
        public string CaretakerPhoneNumber { get; set; }
        public string UploadLink { get; set; }

    }
}
