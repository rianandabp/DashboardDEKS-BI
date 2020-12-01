using DashboardDeks.Data.Models;
using DashboardDeks.Web.ViewModel;

namespace DashboardDeks.Web.Serialization
{
    public class DocumentMapper
    {
        public static DocumentModel SerializeDocument(Document document)
        {
            return new DocumentModel
            {
                Id = document.Id,
                ProgramId = document.ProgramId,
                TaskId = document.TaskId,
                UsernameId = document.UsernameId,
                Name = document.Name,
                Link = document.Link,
                Description = document.Description,
                UploadedOn = document.UploadedOn
            };
        }

        public static Document SerializeDocument(DocumentModel document)
        {
            return new Document
            {
                Id = document.Id,
                ProgramId = document.ProgramId,
                TaskId = document.TaskId,
                UsernameId = document.UsernameId,
                Name = document.Name,
                Link = document.Link,
                Description = document.Description,
                UploadedOn = document.UploadedOn
            };
        }
    }
}
