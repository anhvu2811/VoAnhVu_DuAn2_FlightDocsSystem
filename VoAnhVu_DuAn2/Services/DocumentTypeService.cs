using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VoAnhVu_DuAn2.Entities;
using VoAnhVu_DuAn2.Models;

namespace VoAnhVu_DuAn2.Services
{
    public interface IDocumentTypeService
    {
        List<DocumentTypeModel> getAllDocumentType();
        void createDocumentType(DocumentTypeEntity dt);
        void updateDocumentType(DocumentTypeEntity dt);
        bool deleteDocumentType(string id);
    }
    public class DocumentTypeService : IDocumentTypeService
    {
        private readonly MyDbContext _context;
        public DocumentTypeService(MyDbContext context)
        {
            _context = context;
        }

        public void createDocumentType(DocumentTypeEntity dt)
        {
            try
            {
                _context.DocumentTypeEntities.Add(dt);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool deleteDocumentType(string id)
        {
            try
            {
                var dt = _context.DocumentTypeEntities.FirstOrDefault(c => c.DocumentTypeId == id);
                if (dt is null)
                {
                    return false;
                }
                _context.DocumentTypeEntities.Remove(dt);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<DocumentTypeModel> getAllDocumentType()
        {
            var types = _context.DocumentTypeEntities
                .Include(type => type.GroupPermission)
                .Select(type => new DocumentTypeModel
                {
                    DocumentTypeId = type.DocumentTypeId,
                    DocumentTypeName = type.DocumentTypeName,
                    GroupPermission = type.GroupPermission
                }).ToList();
            return types;
        }

        public void updateDocumentType(DocumentTypeEntity dt)
        {
            try
            {
                _context.DocumentTypeEntities.Update(dt);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
