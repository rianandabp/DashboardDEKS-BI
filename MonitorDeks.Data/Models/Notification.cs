using System;
using System.Collections.Generic;
using System.Text;

namespace DashboardDeks.Data.Models
{
    public class Notification
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
