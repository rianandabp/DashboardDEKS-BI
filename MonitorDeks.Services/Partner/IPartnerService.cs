using System;
using System.Collections.Generic;
using System.Text;

namespace DashboardDeks.Services.Partner
{
    public interface IPartnerService
    {
        List<Data.Models.Partner> GetAll();
        List<Data.Models.Partner> GetPartner(int id);
        ServiceResponse<Data.Models.Partner> CreatePartner(Data.Models.Partner partner);
        ServiceResponse<Data.Models.Partner> UpdatePartner(Data.Models.Partner partner);
        ServiceResponse<bool> DeletePartner(string id);
        Data.Models.Partner GetById(string id);
        bool Authenticate(string username, string password);
    }
}
