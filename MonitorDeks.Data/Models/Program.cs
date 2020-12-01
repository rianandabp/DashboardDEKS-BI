using System;
using System.Collections.Generic;
using System.Text;

namespace DashboardDeks.Data.Models
{
    public class Program
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int ProgressPercentage { get; set; }
        public int TotalTask { get; set; }
        public int TaskComplete { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime LastUpdate { get; set; }

    }
}
