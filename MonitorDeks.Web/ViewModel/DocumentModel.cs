using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DashboardDeks.Data.Models;
namespace DashboardDeks.Web.ViewModel
{
    public class DocumentModel
    {
        public int Id { get; set; }
        public int ProgramId { get; set; }
        public int TaskId { get; set; }
        public string UsernameId { get; set; }
        public string Name { get; set; }
        public string Link { get; set; }
        public string Description { get; set; }
        public DateTime UploadedOn { get; set; }
    }
}
