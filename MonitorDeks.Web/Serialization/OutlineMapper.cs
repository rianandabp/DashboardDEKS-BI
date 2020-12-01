using DashboardDeks.Data.Models;
using DashboardDeks.Web.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DashboardDeks.Web.Serialization
{
    public class OutlineMapper
    {
        public static OutlineModel SerializeOutline(Data.Models.Outline outline)
        {

            return new OutlineModel
            {
                Id = outline.Id,
                Name = outline.Name,
                ProgressPercentage = outline.ProgressPercentage,
                TotalTask = outline.TotalTask,
                TaskComplete = outline.TaskComplete,
                ProgramId = outline.ProgramId,
                Deadline = outline.Deadline,
                ProgressNote = outline.ProgressNote,
                ProblemNote = outline.ProblemNote,
                LastUpdate = outline.LastUpdate
            };
        }

        public static Outline SerializeOutline(OutlineModel outline)
        {

            return new Outline
            {
                Id = outline.Id,
                Name = outline.Name,
                ProgressPercentage = outline.ProgressPercentage,
                TotalTask = outline.TotalTask,
                TaskComplete = outline.TaskComplete,
                ProgramId = outline.ProgramId,
                Deadline = outline.Deadline,
                ProgressNote = outline.ProgressNote,
                ProblemNote = outline.ProblemNote,
                LastUpdate = outline.LastUpdate
            };
        }
    }
}
