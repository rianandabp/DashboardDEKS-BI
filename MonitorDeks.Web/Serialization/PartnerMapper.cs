using DashboardDeks.Data.Models;
using DashboardDeks.Web.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DashboardDeks.Web.Serialization
{
    public class PartnerMapper
    {
        public static PartnerModel SerializePartner(Data.Models.Partner partner)
        {
            return new PartnerModel
            {
                UsernameId = partner.UsernameId,
                ProgramId = partner.ProgramId,
                PartnerName = partner.PartnerName,
                Password = partner.Password,
                Location = partner.Location,
                CaretakerName = partner.CaretakerName,
                CaretakerPhoneNumber = partner.CaretakerPhoneNumber,
                UploadLink = partner.UploadLink

            };
        }

        public static Partner SerializePartner(PartnerModel partner)
        {
            return new Partner
            {
                UsernameId = partner.UsernameId,
                ProgramId = partner.ProgramId,
                Password = partner.Password,
                PartnerName = partner.PartnerName,
                Location = partner.Location,
                CaretakerName = partner.CaretakerName,
                CaretakerPhoneNumber = partner.CaretakerPhoneNumber,
                UploadLink = partner.UploadLink

            };
        }
    }
}
