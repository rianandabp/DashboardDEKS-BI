using DashboardDeks.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using DashboardDeks.Services.Outline;

namespace DashboardDeks.Services.Program
{
    public class ProgramService : IProgramService
    {
        private readonly ProjectDbContext _db;

        public ProgramService (ProjectDbContext dbContext)
        {
            _db = dbContext;
        }

        public ServiceResponse<Data.Models.Program> CreateProgram(Data.Models.Program program)
        {
            try
            {
                _db.Programs.Add(program);
                _db.SaveChanges();
                return new ServiceResponse<Data.Models.Program>
                {
                    IsSuccess = true,
                    Message = "New Program Added",
                    Time = DateTime.UtcNow,
                    Data = program
                };
            }
            catch (Exception e)
            {
                return new ServiceResponse<Data.Models.Program>
                {
                    IsSuccess = false,
                    Message = e.StackTrace,
                    Time = DateTime.UtcNow,
                    Data = null
                };
            }
        }

        public ServiceResponse<bool> DeleteProgram(int id)
        {
            var program = _db.Programs.Find(id);
            var now = DateTime.UtcNow;
            if (program == null)
            {
                return new ServiceResponse<bool>
                {
                    Time = now,
                    IsSuccess = false,
                    Message = "Program to delete not found",
                    Data = false
                };
            }

            try
            {
                _db.Programs.Remove(program);
                _db.SaveChanges();
                return new ServiceResponse<bool>
                {
                    Time = now,
                    IsSuccess = true,
                    Message = "Program deleted!",
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

        public List<Data.Models.Program> GetAllPrograms()
        {
            return _db.Programs.ToList();
        }

        public Data.Models.Program GetById(int id)
        {
            return _db.Programs.Find(id);
        }

        public void SetProgressPercentage(int id)
        {
            var program = _db.Programs.Find(id);

            if (program.TotalTask != 0) program.ProgressPercentage = Convert.ToInt32((Convert.ToSingle(program.TaskComplete) / Convert.ToSingle(program.TotalTask)) * 100);
            else if (program.TotalTask == 0) program.ProgressPercentage = 0;

            _db.Programs.Update(program);
            _db.SaveChanges();
        }

        public ServiceResponse<bool> UpdateProgram(Data.Models.Program program)
        {
            
            var now = DateTime.UtcNow;
            if (program == null)
            {
                return new ServiceResponse<bool>
                {
                    Time = now,
                    IsSuccess = false,
                    Message = "Program to update not found",
                    Data = false
                };
            }

            try
            {
                _db.Programs.Update(program);

                _db.SaveChanges();
                return new ServiceResponse<bool>
                {
                    Time = now,
                    IsSuccess = true,
                    Message = "Program description updated!",
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
    }
}
