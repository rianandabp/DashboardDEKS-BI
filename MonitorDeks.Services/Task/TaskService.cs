using DashboardDeks.Data;
using DashboardDeks.Services.Outline;
using DashboardDeks.Services.Program;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DashboardDeks.Services.Task
{
    public class TaskService : ITaskService
    {
        private readonly ProjectDbContext _db;
        private readonly IOutlineService _outlineService;
        private readonly IProgramService _programService;
        public TaskService (ProjectDbContext dbContext,
            IOutlineService outlineService, IProgramService programService)
        {
            _db = dbContext;
            _outlineService = outlineService;
            _programService = programService;
        }

        public ServiceResponse<Data.Models.Task> CreateTask(Data.Models.Task task)
        {
            try
            {
                _db.Tasks.Add(task);

                var temp = _db.Outlines.Find(task.OutlineId);
                var tempProgram = _db.Programs.Find(task.ProgramId);

                temp.TotalTask += 1;
                tempProgram.TotalTask += 1;

                _db.Outlines.Update(temp);
                _db.Programs.Update(tempProgram);

                _outlineService.SetProgressPercentage(task.OutlineId);
                _programService.SetProgressPercentage(task.ProgramId);

                _db.SaveChanges();

              

                return new ServiceResponse<Data.Models.Task>
                {
                    IsSuccess = true,
                    Message = "New Task Added",
                    Time = DateTime.UtcNow,
                    Data = task
                };
            }
            catch (Exception e)
            {
                return new ServiceResponse<Data.Models.Task>
                {
                    IsSuccess = false,
                    Message = e.StackTrace,
                    Time = DateTime.UtcNow,
                    Data = null
                };
            }
        }

        public ServiceResponse<bool> DeleteTask(int id)
        {
            var task = _db.Tasks.Find(id);
            var now = DateTime.UtcNow;
            if (task == null)
            {
                return new ServiceResponse<bool>
                {
                    Time = now,
                    IsSuccess = false,
                    Message = "Task to delete not found",
                    Data = false
                };
            }

            try
            {
                _db.Tasks.Remove(task);

                var temp = _db.Outlines.Find(task.OutlineId);
                var tempProgram = _db.Programs.Find(task.ProgramId);

                temp.TotalTask -= 1;
                tempProgram.TotalTask -= 1;

                if (task.Status)
                {
                    temp.TaskComplete -= 1;
                    tempProgram.TaskComplete -= 1;
                }

                _db.Outlines.Update(temp);
                _db.Programs.Update(tempProgram);

                _outlineService.SetProgressPercentage(task.OutlineId);
                _programService.SetProgressPercentage(task.ProgramId);

                _db.SaveChanges();
                

                return new ServiceResponse<bool>
                {
                    Time = now,
                    IsSuccess = true,
                    Message = "Task deleted!",
                    Data = true
                };
            }
            catch (Exception e)
            {
                return new ServiceResponse<bool>
                {
                    Time = now,
                    IsSuccess = false,
                    Message = e.StackTrace,
                    Data = false
                };
            }
        }

        public List<Data.Models.Task> GetAllTask()
        {
            return _db.Tasks.OrderBy(c => c.LastUpdate).ToList();
        }


        public Data.Models.Task GetById(int id)
        {
            return _db.Tasks.Find(id);
        }

        public List<Data.Models.Task> GetTaskByProgramId(int id)
        {
            return _db.Tasks.Where(c => c.ProgramId.Equals(id)).ToList();
        }

        public ServiceResponse<bool> UpdateTaskStatus(int id)
        {
            var task = _db.Tasks.Find(id);
            var now = DateTime.UtcNow;
            if (task == null)
            {
                return new ServiceResponse<bool>
                {
                    Time = now,
                    IsSuccess = false,
                    Message = "Task to update not found",
                    Data = false
                };
            }

            try
            {
                if (task.Status == false) task.Status = true;
                else task.Status = false;

                var temp = _db.Outlines.Find(task.OutlineId);
                var tempProgram = _db.Programs.Find(task.ProgramId);

                temp.TaskComplete += 1;
                tempProgram.TaskComplete += 1;

                _db.Outlines.Update(temp);
                _db.Programs.Update(tempProgram);

                _outlineService.SetProgressPercentage(task.OutlineId);
                _programService.SetProgressPercentage(task.ProgramId);

                task.LastUpdate = now;
                _db.Tasks.Update(task);
                _db.SaveChanges();
                return new ServiceResponse<bool>
                {
                    Time = now,
                    IsSuccess = true,
                    Message = "Task updated!",
                    Data = true
                };
            }
            catch (Exception e)
            {
                return new ServiceResponse<bool>
                {
                    Time = now,
                    IsSuccess = false,
                    Message = e.StackTrace,
                    Data = false
                };
            }
        }

        public ServiceResponse<Data.Models.Task> UpdateTask(Data.Models.Task task)
        {
            try
            {

                var temp = _db.Tasks.Find(task.Id);
                temp.Name = task.Name;
                temp.Deadline = task.Deadline;
                temp.AssignedTo = task.AssignedTo;
                temp.ProgressNote = task.ProgressNote;
                temp.ProblemNote = task.ProblemNote;

                _db.Tasks.Update(temp);
                _db.SaveChanges();

                return new ServiceResponse<Data.Models.Task>
                {
                    IsSuccess = true,
                    Message = "New Task Updated",
                    Time = DateTime.UtcNow,
                    Data = task
                };
            }
            catch (Exception e)
            {
                return new ServiceResponse<Data.Models.Task>
                {
                    IsSuccess = false,
                    Message = e.StackTrace,
                    Time = DateTime.UtcNow,
                    Data = null
                };
            }
        }

        public List<Data.Models.Task> GetTaskByOutlineId(int id)
        {
            return _db.Tasks.Where(c => c.OutlineId.Equals(id)).ToList();
        }

        public int TotalTask()
        {
            return _db.Tasks.Count();
        }

        public void DeleteTaskByOutlineId(int id)
        {
            var temp = _db.Tasks.Where(c => c.OutlineId.Equals(id)).ToList();
            foreach(var item in temp)
            {
                _db.Tasks.Remove(item);
            }
            _db.SaveChanges();
        }

        public void DeleteTaskByProgramId(int id)
        {
            var temp = _db.Tasks.Where(c => c.ProgramId.Equals(id)).ToList();
            foreach (var item in temp)
            {
                _db.Tasks.Remove(item);
            }
            _db.SaveChanges();
        }
    }
}
