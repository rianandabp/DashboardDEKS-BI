using System;
using System.Collections.Generic;
using System.Text;

namespace DashboardDeks.Services.Document
{
    public interface IDocumentService
    {
        ServiceResponse<Data.Models.Document> CreateDocument(Data.Models.Document document);
        ServiceResponse<Data.Models.Document> UpdateDocument(Data.Models.Document document);
        Data.Models.Document GetById(int id);
        List<Data.Models.Document> GetByProgramId(int id);
        List<Data.Models.Document> GetByTaskId(int id);
        List<Data.Models.Document> GetByUsername(string username);
        void DeleteByTaskId(int id);
        void DeleteByProgramId(int id);
    }
}
