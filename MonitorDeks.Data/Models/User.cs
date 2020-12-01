using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DashboardDeks.Data.Models
{
    public class User
    {
        [Key]
        public string UsernameId { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string UserAccess { get; set; }
        public float PunctualityRating { get; set; }
        public int TaskDone { get; set; }
        public int TaskLate { get; set; }

    }
}
