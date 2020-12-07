using DashboardDeks.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DashboardDeks.Services.Outline
{
    public class OutlineService : IOutlineService
    {
        private readonly ProjectDbContext _db;

        public OutlineService(ProjectDbContext dbContext)
        {
            _db = dbContext;
        }

        public ServiceResponse<Data.Models.Outline> CreateOutline(Data.Models.Outline outline)
        {
            try
            {
                _db.Outlines.Add(outline);
                _db.SaveChanges();
                return new ServiceResponse<Data.Models.Outline>
                {
                    IsSuccess = true,
                    Message = "New Outline Added",
                    Time = DateTime.UtcNow,
                    Data = outline
                };
            }
            catch (Exception e)
            {
                return new ServiceResponse<Data.Models.Outline>
                {
                    IsSuccess = false,
                    Message = e.StackTrace,
                    Time = DateTime.UtcNow,
                    Data = null
                };
            }
        }

        public void DeleteByProgramId(int id)
        {
            var temp = _db.Outlines.Where(c => c.ProgramId.Equals(id)).ToList();
            foreach (var item in temp)
            {
                _db.Outlines.Remove(item);
            }
            _db.SaveChanges();
        }

        public ServiceResponse<bool> DeleteOutline(int id)
        {
            var outline = _db.Outlines.Find(id);
            var now = DateTime.UtcNow;
            if (outline == null)
            {
                return new ServiceResponse<bool>
                {
                    Time = now,
                    IsSuccess = false,
                    Message = "Outline to delete not found",
                    Data = false
                };
            }

            try
            {

                _db.Outlines.Remove(outline);
                _db.SaveChanges();
                return new ServiceResponse<bool>
                {
                    Time = now,
                    IsSuccess = true,
                    Message = "Outline deleted!",
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


        public List<Data.Models.Outline> GetAllOutlines()
        {
            return _db.Outlines.ToList();
        }

        public Data.Models.Outline GetById(int id)
        {
            return _db.Outlines.Find(id);
        }

        public List<Data.Models.Outline> GetOutlineByProgramId(int id)
        {
            return _db.Outlines.Where(c => c.ProgramId.Equals(id)).ToList();
        }

        public void SetProgressPercentage(int id)
        {
            var outline = _db.Outlines.Find(id);
            outline.ProgressPercentage = Convert.ToInt32((Convert.ToSingle(outline.TaskComplete) / Convert.ToSingle(outline.TotalTask)) * 100);
            _db.Outlines.Update(outline);
            _db.SaveChanges();
        }

        public ServiceResponse<Data.Models.Outline> UpdateOutline(Data.Models.Outline outline)
        {

            try
            {
                _db.Outlines.Update(outline);
                _db.SaveChanges();
                return new ServiceResponse<Data.Models.Outline>
                {
                    IsSuccess = true,
                    Message = "New Outline Added",
                    Time = DateTime.UtcNow,
                    Data = outline
                };
            }
            catch (Exception e)
            {
                return new ServiceResponse<Data.Models.Outline>
                {
                    IsSuccess = false,
                    Message = e.StackTrace,
                    Time = DateTime.UtcNow,
                    Data = null
                };
            }
        }
    }
}
