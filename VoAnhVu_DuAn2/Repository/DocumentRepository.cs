using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VoAnhVu_DuAn2.Entities;
using VoAnhVu_DuAn2.Models;

namespace VoAnhVu_DuAn2.Repository
{
    public interface IDocumentRepository
    {
        List<DocumentModel> getAllDocument();
        DocumentModel getDocumentById(string id);
        void createDocument(DocumentEntity dt);
        void updateDocument(DocumentEntity dt);
        bool deleteDocument(string id);
        void uploadFile(string id, string url, string fileName);
        void deleteFile(string id);
    }
    public class DocumentRepository : IDocumentRepository
    {
        private readonly MyDbContext _context;
        public DocumentRepository(MyDbContext context)
        {
            _context = context;
        }

        public void createDocument(DocumentEntity dt)
        {
            try
            {
                _context.DocumentEntities.Add(dt);
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
                var dt = _context.DocumentEntities.FirstOrDefault(c => c.DocumentId == id);
                if (dt is null)
                {
                    return false;
                }
                _context.DocumentEntities.Remove(dt);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<DocumentModel> getAllDocument()
        {
            var docs = _context.DocumentEntities
                .Include(doc => doc.DocumentType)
                .Include(doc => doc.Flight)
                .Include(doc => doc.User)
                .Select(doc => new DocumentModel
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

        public DocumentModel getDocumentById(string id)
        {
            var docEntity = _context.DocumentEntities
                .Include(doc => doc.DocumentType)
                .Include(doc => doc.Flight)
                .Include(doc => doc.User)
                .FirstOrDefault(c => c.DocumentId == id);
            if (docEntity != null)
            {
                var documentModel = new DocumentModel
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

        public void updateDocument(DocumentEntity dt)
        {
            try
            {
                _context.DocumentEntities.Update(dt);
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
                var doc = _context.DocumentEntities.FirstOrDefault(u => u.DocumentId == id);
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
                var doc = _context.DocumentEntities.FirstOrDefault(u => u.DocumentId == id);
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
    }
}
