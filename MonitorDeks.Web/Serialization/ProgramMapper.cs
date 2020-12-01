using DashboardDeks.Web.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DashboardDeks.Data.Models;

namespace DashboardDeks.Web.Serialization
{
    public class ProgramMapper
    {
        public static ProgramModel SerializeProgram(Data.Models.Program program)
        {
            

            return new ProgramModel
            {
                Id = program.Id,
                Name = program.Name,
                ProgressPercentage = program.ProgressPercentage,
                TotalTask = program.TotalTask,
                TaskComplete = program.TaskComplete,
                Description = program.Description,
                StartDate = program.StartDate,
                EndDate = program.EndDate,
                LastUpdate = program.LastUpdate
            };
        }

        public static Data.Models.Program SerializeProgram(ProgramModel program)
        {

            return new Data.Models.Program 
            {
                Id = program.Id,
                Name = program.Name,
                ProgressPercentage = program.ProgressPercentage,
                TotalTask = program.TotalTask,
                TaskComplete = program.TaskComplete,
                Description = program.Description,
                StartDate = program.StartDate,
                EndDate = program.EndDate,
                LastUpdate = program.LastUpdate
            };
        }
    }
}
