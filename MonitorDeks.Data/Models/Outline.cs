﻿using System;
using System.Collections.Generic;
using System.Text;

namespace DashboardDeks.Data.Models
{
    public class Outline
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ProgramId { get; set; }
        public int ProgressPercentage { get; set; }
        public int TotalTask { get; set; }
        public int TaskComplete { get; set; }
        public DateTime Deadline { get; set; }
        public string ProgressNote { get; set; }
        public string ProblemNote { get; set; }
        public DateTime LastUpdate { get; set; }
    }
}
