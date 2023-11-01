using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VoAnhVu_DuAn2.Data;
using VoAnhVu_DuAn2.DTO;
using VoAnhVu_DuAn2.Models;

namespace VoAnhVu_DuAn2.Repository
{
    public interface IDocumentRepository
    {
        List<DocumentDTO> getAllDocument();
        DocumentDTO getDocumentById(string id);
        void createDocument(DocumentModel dt);
        void updateDocument(DocumentModel dt);
        bool deleteDocument(string id);
        void uploadFile(string id, string url, string fileName);
        void deleteFile(string id);
        int CountDocumentsForFlight(string flightId);
    }
    public class DocumentRepository : IDocumentRepository
    {
        private readonly MyDbContext _context;
        public DocumentRepository(MyDbContext context)
        {
            _context = context;
        }

        public void createDocument(DocumentModel dt)
        {
            try
            {
                _context.DocumentModels.Add(dt);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool deleteDocument(string id)
        {
            try
            {
                var dt = _context.DocumentModels.FirstOrDefault(c => c.DocumentId == id);
                if (dt is null)
                {
                    return false;
                }
                _context.DocumentModels.Remove(dt);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<DocumentDTO> getAllDocument()
        {
            var docs = _context.DocumentModels
                .Include(doc => doc.DocumentType)
                .Include(doc => doc.Flight)
                .Include(doc => doc.User)
                .Select(doc => new DocumentDTO
                {
                    DocumentId = doc.DocumentId,
                    DocumentName = doc.DocumentName,
                    FileUpLoad = doc.FileUpLoad,
                    Version = doc.Version,
                    CreateDate = doc.CreateDate,
                    Note = doc.Note,
                    DocumentType = doc.DocumentType,
                    Flight = doc.Flight,
                    User = doc.User
                }).ToList();
            return docs;
        }

        public DocumentDTO getDocumentById(string id)
        {
            var docEntity = _context.DocumentModels
                .Include(doc => doc.DocumentType)
                .Include(doc => doc.Flight)
                .Include(doc => doc.User)
                .FirstOrDefault(c => c.DocumentId == id);
            if (docEntity != null)
            {
                var documentModel = new DocumentDTO
                {
                    DocumentId = docEntity.DocumentId,
                    DocumentName = docEntity.DocumentName,
                    FileUpLoad = docEntity.FileUpLoad,
                    Version = docEntity.Version,
                    CreateDate = docEntity.CreateDate,
                    Note = docEntity.Note,
                    DocumentType = docEntity.DocumentType,
                    Flight = docEntity.Flight,
                    User = docEntity.User
                };
                return documentModel;
            }
            return null;
        }

        public void updateDocument(DocumentModel dt)
        {
            try
            {
                _context.DocumentModels.Update(dt);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void uploadFile(string id, string url, string fileName)
        {
            try
            {
                var doc = _context.DocumentModels.FirstOrDefault(u => u.DocumentId == id);
                if (doc != null)
                {
                    doc.FileUpLoad = url;
                    doc.DocumentName = fileName;
                    _context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void deleteFile(string id)
        {
            try
            {
                var doc = _context.DocumentModels.FirstOrDefault(u => u.DocumentId == id);
                if (doc != null)
                {
                    doc.FileUpLoad = null;
                    doc.DocumentName = null;
                    _context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int CountDocumentsForFlight(string flightId)
        {
            return _context.DocumentModels.Count(d => d.FlightId == flightId);
        }

    }
}
