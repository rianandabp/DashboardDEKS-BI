using DashboardDeks.Web.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DashboardDeks.Web.Serialization
{
    public class TaskMapper
    {
        public static TaskModel SerializeTask(Data.Models.Task task)
        {
            return new TaskModel
            {
                Id = task.Id,
                ProgramId = task.ProgramId,
                OutlineId = task.OutlineId,
                Name = task.Name,
                Status = task.Status,
                Deadline = task.Deadline,
                AssignedTo = task.AssignedTo,
                ProgressNote = task.ProgressNote,
                ProblemNote = task.ProblemNote,
                LastUpdate = task.LastUpdate
            };
        }

        public static Data.Models.Task SerializeTask(TaskModel task)
        {

            return new Data.Models.Task
            {
                Id = task.Id,
                ProgramId = task.ProgramId,
                OutlineId = task.OutlineId,
                Name = task.Name,
                Status = task.Status,
                Deadline = task.Deadline,
                AssignedTo = task.AssignedTo,
                ProgressNote = task.ProgressNote,
                ProblemNote = task.ProblemNote,
                LastUpdate = task.LastUpdate
            };
        }
    }
}
