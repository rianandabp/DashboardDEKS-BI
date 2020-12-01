using DashboardDeks.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DashboardDeks.Services.Document
{
    
    public class DocumentService : IDocumentService
    {
        private readonly ProjectDbContext _db;

        public DocumentService(ProjectDbContext dbContext)
        {
            _db = dbContext;
        }

        public ServiceResponse<Data.Models.Document> CreateDocument(Data.Models.Document document)
        {
            try
            {
                _db.Documents.Add(document);
                _db.SaveChanges();

                return new ServiceResponse<Data.Models.Document>
                {
                    IsSuccess = true,
                    Message = "New Document Added",
                    Time = DateTime.UtcNow,
                    Data = document
                };
            }
            catch (Exception e)
            {
                return new ServiceResponse<Data.Models.Document>
                {
                    IsSuccess = false,
                    Message = e.StackTrace,
                    Time = DateTime.UtcNow,
                    Data = null
                };
            }
        }

        public Data.Models.Document GetById(int id)
        {
            return _db.Documents.Find(id);
        }

        public List<Data.Models.Document> GetByProgramId(int id)
        {
            return _db.Documents.Where(c => c.ProgramId.Equals(id)).ToList();
        }

        public List<Data.Models.Document> GetByTaskId(int id)
        {
            return _db.Documents.Where(c => c.TaskId.Equals(id)).ToList();
        }

        public List<Data.Models.Document> GetByUsername(string username)
        {
            return _db.Documents.Where(c => c.UsernameId.Equals(username)).ToList();
        }

        public ServiceResponse<Data.Models.Document> UpdateDocument(Data.Models.Document document)
        {
            try
            {

                _db.Documents.Update(document);
                _db.SaveChanges();

                return new ServiceResponse<Data.Models.Document>
                {
                    IsSuccess = true,
                    Message = "New Document Updated",
                    Time = DateTime.UtcNow,
                    Data = document
                };
            }
            catch (Exception e)
            {
                return new ServiceResponse<Data.Models.Document>
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
