using System;
using System.Collections.Generic;
using System.Text;

namespace DashboardDeks.Services.Outline
{
    public interface IOutlineService
    {
        List<Data.Models.Outline> GetAllOutlines();
        List<Data.Models.Outline> GetOutlineByProgramId(int id);
        ServiceResponse<Data.Models.Outline> CreateOutline(Data.Models.Outline outline);
        ServiceResponse<Data.Models.Outline> UpdateOutline(Data.Models.Outline outline);
        ServiceResponse<bool> DeleteOutline(int id);
        Data.Models.Outline GetById(int id);
        void SetProgressPercentage(int id);
        void DeleteByProgramId(int id);
    }
}
