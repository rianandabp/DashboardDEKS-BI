using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DashboardDeks.Web.ViewModel
{
    public class PartnerModel
    {
        [Key]
        public string UsernameId { get; set; }
        public int ProgramId { get; set; }    
        public string PartnerName { get; set; }
        public string Location { get; set; }
        public string CaretakerName { get; set; }
        public string CaretakerPhoneNumber { get; set; }
        public string Password { get; set; }
        public string UploadLink { get; set; }
    }
}
