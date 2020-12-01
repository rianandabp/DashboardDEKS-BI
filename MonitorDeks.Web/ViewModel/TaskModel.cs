using DashboardDeks.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DashboardDeks.Web.ViewModel
{
    public class TaskModel
    {
        public int Id { get; set; }
        public int OutlineId { get; set; }
        public int ProgramId { get; set; }
        public string AssignedTo { get; set; }
        public string Name { get; set; }
        public bool Status { get; set; }
        public DateTime Deadline { get; set; }
        public string ProgressNote { get; set; }
        public string ProblemNote { get; set; }
        public DateTime LastUpdate { get; set; }
    }
}
