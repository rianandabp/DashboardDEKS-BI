using System;
using System.Collections.Generic;
using System.Text;

namespace DashboardDeks.Services.Status
{
    public interface IStatusService
    {
        void UpdateStatus();
        bool GetStatus();
    }
}
