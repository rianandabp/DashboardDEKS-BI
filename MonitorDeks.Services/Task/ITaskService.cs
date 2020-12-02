using System;
using System.Collections.Generic;
using System.Text;

namespace DashboardDeks.Services.Task
{
    public interface ITaskService
    {
        List<Data.Models.Task> GetAllTask();
        List<Data.Models.Task> GetTaskByProgramId(int id);
        List<Data.Models.Task> GetTaskByOutlineId(int id);
        ServiceResponse<Data.Models.Task> CreateTask(Data.Models.Task task);
        ServiceResponse<Data.Models.Task> UpdateTask(Data.Models.Task task);
        ServiceResponse<bool> DeleteTask(int id);
        ServiceResponse<bool> UpdateTaskStatus(int id);
        Data.Models.Task GetById(int id);
        int TotalTask();
        void DeleteTaskByOutlineId(int id);
        void DeleteTaskByProgramId(int id);
    }
}
