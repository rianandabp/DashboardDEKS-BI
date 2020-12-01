using System;
using System.Collections.Generic;
using System.Text;

namespace DashboardDeks.Services.Program
{
    public interface IProgramService
    {
        List<Data.Models.Program> GetAllPrograms();
        ServiceResponse<Data.Models.Program> CreateProgram(Data.Models.Program program);
        ServiceResponse<bool> DeleteProgram(int id);
        ServiceResponse<bool> UpdateProgram(Data.Models.Program program);
        Data.Models.Program GetById(int id);
        void SetProgressPercentage(int id);

    }
}
